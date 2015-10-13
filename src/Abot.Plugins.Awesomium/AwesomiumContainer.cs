using System;
using System.Diagnostics;
using System.IO;
using Abot.Plugins.Awesomium.Remote;

namespace Abot.Plugins.Awesomium
{
   class AwesomiumContainer : IDisposable
   {
      private readonly string _channelId;
      private const string ExternalExeName = "procsomium.exe";
      private const string ExternalProcessName = "procsomium";
      private readonly string _processPath;
      private int _processId = -1;
      private IRemotePageDownloader _remote;

      public AwesomiumContainer(string processDir, string channelId)
      {
         _channelId = channelId;
         if (processDir == null) throw new ArgumentNullException(nameof(processDir));

         _processPath = Path.Combine(processDir, ExternalExeName);
      }

      public DownloadedPage Download(string uri)
      {
         return RemoteInterface.DownloadPage(uri);
      }

      public void Dispose()
      {
         if (_processId != -1)
         {
            try
            {
               Process p = Process.GetProcessById(_processId);
               p.Kill();
               _processId = -1;
            }
            catch (ArgumentException)
            {
               //process is not running
            }
            catch (InvalidOperationException)
            {
               //another misery, MSDN: The process was not started by this object.
            }
         }
      }

      private IRemotePageDownloader RemoteInterface
      {
         get
         {
            if (_processId == -1)
            {
               _processId = Process.Start(_processPath, _channelId).Id;
            }

            if (_remote == null)
            {
               _remote = PageDownloaderIpcServer<IRemotePageDownloader>.CreateClient(_channelId);
            }

            return _remote;
         }
      }

      public static void ShutdownDeadProcesses()
      {
         foreach (Process p in Process.GetProcessesByName(ExternalProcessName))
         {
            try
            {
               p.Kill();
            }
            catch (Exception ex) //todo: log this
            {

            }
         }
      }
   }
}
