using System;
using System.Windows.Forms;
using Abot.Plugins.Awesomium.ExternalProcess.Presenters;
using Abot.Plugins.Awesomium.ExternalProcess.Views;

namespace Abot.Plugins.Awesomium.ExternalProcess.Forms
{
   public partial class WebHostTab : UserControl, IWebHostTabView
   {
      public WebHostTab()
      {
         InitializeComponent();
         Presenter = new WebHostTabPresenter(this);
      }

      public WebHostTabPresenter Presenter { get; }

      public void OpenUrl(string url)
      {
         Invoke((Action)(() =>
         {
            AddressText.Text = url;
            WebBrowserControl.Source = new Uri(url);
         }));
      }

      private void Awesomium_Windows_Forms_WebControl_DocumentReady(object sender, global::Awesomium.Core.DocumentReadyEventArgs e)
      {
         //Presenter.OnHtmlAvailable(WebBrowserControl.HTML);
      }

      private void Awesomium_Windows_Forms_WebControl_LoadingFrameComplete(object sender, global::Awesomium.Core.FrameEventArgs e)
      {
         if(!e.IsMainFrame) return;

         Presenter.OnHtmlAvailable(WebBrowserControl.HTML);
      }

      private void Awesomium_Windows_Forms_WebControl_LoadingFrameFailed(object sender, global::Awesomium.Core.LoadingFrameFailedEventArgs e)
      {

      }
   }
}
