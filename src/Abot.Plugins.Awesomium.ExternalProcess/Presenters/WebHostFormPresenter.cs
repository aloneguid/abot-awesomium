using Abot.Plugins.Awesomium.ExternalProcess.Views;

namespace Abot.Plugins.Awesomium.ExternalProcess.Presenters
{
   internal class WebHostFormPresenter
   {
      private readonly IWebHostFormView _view;

      public WebHostFormPresenter(IWebHostFormView view)
      {
         _view = view;
      }
   }
}
