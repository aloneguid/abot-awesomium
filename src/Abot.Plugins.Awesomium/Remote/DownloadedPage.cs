using System.Runtime.Serialization;

namespace Abot.Plugins.Awesomium.Remote
{
   [DataContract]
   public class DownloadedPage
   {
      [DataMember]
      public string Html { get; set; }
   }
}
