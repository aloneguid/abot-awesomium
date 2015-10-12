using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using Abot.Core;
using Abot.Plugins.Awesomium.Remote;
using Abot.Poco;

namespace Abot.Plugins.Awesomium
{
   public class AwesomiumPageRequester : IPageRequester
   {
      private readonly DirectoryInfo _processDir;
      private const string ExternalExeName = "procsomium.exe";
      private const string ExternalProcessName = "procsomium";
      private static readonly ConcurrentDictionary<int, Process> RunningProcessIdToProcess = new ConcurrentDictionary<int, Process>();

      private Process _extProcess;
      private IRemotePageDownloader _downloader;

      static AwesomiumPageRequester()
      {
         ShutdownDeadProcesses();
      }

      public AwesomiumPageRequester() : this(NetPath.ExecDirInfo)
      {
         ShutdownDeadProcesses();         
      }

      public AwesomiumPageRequester(DirectoryInfo processDir)
      {
         _processDir = processDir;
      }

      public CrawledPage MakeRequest(Uri uri)
      {
         HealthCheck();

         var result = new CrawledPage(uri)
         {
            RequestStarted = DateTime.UtcNow,
            DownloadContentStarted = DateTime.UtcNow
         };

         try
         {
            DownloadedPage page = _downloader.DownloadPage(uri.ToString());
            result.Content = new PageContent {Text = page.Html};
         }
         finally
         {
            result.RequestCompleted = DateTime.UtcNow;
            result.DownloadContentCompleted = DateTime.UtcNow;
         }

         return result;
      }

      private void HealthCheck()
      {
         //check if the process was initialised at all
         if(_extProcess == null)
         {
            string path = Path.Combine(_processDir.FullName, ExternalExeName);
            _extProcess = Process.Start(path);
            //can become null if the exe file doesn't exist in the target directory
            if(_extProcess == null) throw new InvalidOperationException("could not launch " + ExternalProcessName + ", please check that all the files are present");
            RunningProcessIdToProcess[_extProcess.Id] = _extProcess;
         }

         if(_downloader == null)
         {
            _downloader = PageDownloaderIpcServer<IRemotePageDownloader>.CreateClient();
         }
      }

      public CrawledPage MakeRequest(Uri uri, Func<CrawledPage, CrawlDecision> shouldDownloadContent)
      {
         //I don't really know what to do with the shouldDownloadContent delegate yet, probably it's not
         //relevant in Awesomium context

         return MakeRequest(uri);
      }

      private static void ShutdownDeadProcesses()
      {
         foreach(Process p in Process.GetProcessesByName(ExternalProcessName))
         {
            if(!RunningProcessIdToProcess.ContainsKey(p.Id))
            {
               try
               {
                  p.Kill();
               }
               catch(Exception ex) //todo: log this
               {

               }
               finally
               {
                  Process extra;
                  RunningProcessIdToProcess.TryRemove(p.Id, out extra);
               }
            }
         }
      }

      public void Dispose()
      {
         Process extra;
         RunningProcessIdToProcess.TryRemove(_extProcess.Id, out extra);

         ShutdownDeadProcesses();
      }
   }
}
