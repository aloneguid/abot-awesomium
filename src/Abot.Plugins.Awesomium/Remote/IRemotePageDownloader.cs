using System.ServiceModel;

namespace Abot.Plugins.Awesomium.Remote
{
   [ServiceContract]
   public interface IRemotePageDownloader
   {
      [OperationContract]
      string DownloadPage(string url);
   }
}
