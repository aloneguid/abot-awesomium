using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using Abot.Core;
using Abot.Plugins.Awesomium.Remote;
using Abot.Poco;
using Aloneguid.Support.Collections;

namespace Abot.Plugins.Awesomium
{
   public class AwesomiumPageRequester : IPageRequester
   {
      private readonly DirectoryInfo _processDir;
      private readonly ObjectPool<AwesomiumContainer> _awesomiumWorkerPool;
      private static readonly string ChannelPrefix = Guid.NewGuid().ToString();
      private static int _channelCounter = 0;

      static AwesomiumPageRequester()
      {
         AwesomiumContainer.ShutdownDeadProcesses();
      }

      public AwesomiumPageRequester() : this(NetPath.ExecDirInfo)
      {
      }

      public AwesomiumPageRequester(DirectoryInfo processDir)
      {
         _processDir = processDir;
         _awesomiumWorkerPool = new ObjectPool<AwesomiumContainer>(
            () => new AwesomiumContainer(_processDir.FullName, $"{ChannelPrefix}-{_channelCounter++}"),
            (c) => c.Dispose(),
            10,
            TimeSpan.FromHours(1));
      }

      public CrawledPage MakeRequest(Uri uri)
      {
         var result = new CrawledPage(uri)
         {
            RequestStarted = DateTime.UtcNow,
            DownloadContentStarted = DateTime.UtcNow
         };

         AwesomiumContainer container = null;
         try
         {
            container = _awesomiumWorkerPool.GetInstance();
            DownloadedPage page = container.Download(uri.ToString());
            result.Content = new PageContent {Text = page.Html};
         }
         finally
         {
            _awesomiumWorkerPool.ReleaseInstance(container);
            result.RequestCompleted = DateTime.UtcNow;
            result.DownloadContentCompleted = DateTime.UtcNow;
         }

         return result;
      }

      public CrawledPage MakeRequest(Uri uri, Func<CrawledPage, CrawlDecision> shouldDownloadContent)
      {
         //I don't really know what to do with the shouldDownloadContent delegate yet, probably it's not
         //relevant in Awesomium context

         return MakeRequest(uri);
      }

      public void Dispose()
      {
         _awesomiumWorkerPool.Dispose();

         AwesomiumContainer.ShutdownDeadProcesses();
      }
   }
}
