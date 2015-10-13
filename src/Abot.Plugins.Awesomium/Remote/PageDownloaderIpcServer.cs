using System;
using System.ServiceModel;
using System.Threading;

namespace Abot.Plugins.Awesomium.Remote
{
   public class PageDownloaderIpcServer<TServerImpl> : IDisposable where TServerImpl : IRemotePageDownloader
   {
      private readonly string _channelId;
      private ServiceHost _host;

      public PageDownloaderIpcServer(string channelId)
      {
         _channelId = channelId;
         //WCF will call back on the thread it's created. Because we don't want it to call back to UI thread
         //we launch it on a random thread.
         var thread = new Thread(() =>
         {
            _host = new ServiceHost(typeof(TServerImpl), new Uri("net.pipe://localhost/" + channelId));
            _host.AddServiceEndpoint(typeof(IRemotePageDownloader), CreateBinding(), "Awesomium");
            _host.Open();

         });
         thread.Start();
      }

      public static IRemotePageDownloader CreateClient(string channelId)
      {
         var pipeFactory = new ChannelFactory<IRemotePageDownloader>(CreateBinding(),
            new EndpointAddress("net.pipe://localhost/" + channelId + "/Awesomium"));

         return pipeFactory.CreateChannel();
      }

      private static NetNamedPipeBinding CreateBinding()
      {
         var binding = new NetNamedPipeBinding();
         binding.MaxReceivedMessageSize = int.MaxValue;
         return binding;
      }

      public void Dispose()
      {
         _host.Close();
      }
   }
}
