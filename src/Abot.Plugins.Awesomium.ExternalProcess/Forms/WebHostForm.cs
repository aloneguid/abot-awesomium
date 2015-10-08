using System.Windows.Forms;
using Abot.Plugins.Awesomium.ExternalProcess.Presenters;
using Abot.Plugins.Awesomium.ExternalProcess.Views;

namespace Abot.Plugins.Awesomium.ExternalProcess.Forms
{
   public partial class WebHostForm : Form, IWebHostFormView
   {
      private readonly WebHostFormPresenter _presenter;

      public WebHostForm()
      {
         InitializeComponent();
         _presenter = new WebHostFormPresenter(this);
      }
   }
}
