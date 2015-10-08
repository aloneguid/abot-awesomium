using System;
using System.Threading;
using Abot.Plugins.Awesomium.ExternalProcess.Views;
using Abot.Plugins.Awesomium.Remote;

namespace Abot.Plugins.Awesomium.ExternalProcess.Presenters
{
   public class WebHostTabPresenter : IDisposable
   {
      private readonly IWebHostTabView _view;
      private readonly PageDownloaderIpcServer<ServerDelegate> _server;
      private readonly ManualResetEvent _htmlAvailableEvent = new ManualResetEvent(false);
      private string _lastHtml;

      public class ServerDelegate : IRemotePageDownloader
      {
         public static WebHostTabPresenter ParentInstance { get; set; }

         public string DownloadPage(string url)
         {
            string html = ParentInstance.DownloadHtml(url);
            return html;
         }
      }

      public WebHostTabPresenter(IWebHostTabView view)
      {
         _view = view;
         ServerDelegate.ParentInstance = this;
         _server = new PageDownloaderIpcServer<ServerDelegate>();
      }

      public string DownloadHtml(string address)
      {
         _htmlAvailableEvent.Reset();
         _view.OpenUrl(address);
         _htmlAvailableEvent.WaitOne();

         return _lastHtml;
      }

      public void OnHtmlAvailable(string html)
      {
         _lastHtml = html;
         _htmlAvailableEvent.Set();
      }

      public void Dispose()
      {
         _server.Dispose();
      }
   }
}
