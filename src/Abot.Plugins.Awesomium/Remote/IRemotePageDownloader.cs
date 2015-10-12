using System.ServiceModel;

namespace Abot.Plugins.Awesomium.Remote
{
   [ServiceContract]
   public interface IRemotePageDownloader
   {
      [OperationContract]
      DownloadedPage DownloadPage(string url);
   }
}
