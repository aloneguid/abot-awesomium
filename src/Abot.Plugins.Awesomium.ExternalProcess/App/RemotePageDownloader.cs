using System;
using Abot.Plugins.Awesomium.Remote;

namespace Abot.Plugins.Awesomium.ExternalProcess.App
{
   //see this for more info: https://web.archive.org/web/20141027055124/http://tech.pro/tutorial/855/wcf-tutorial-basic-interprocess-communication
   public class RemotePageDownloader : IRemotePageDownloader
   {
      public string DownloadPage(string url)
      {
         throw new NotImplementedException();
      }
   }
}
