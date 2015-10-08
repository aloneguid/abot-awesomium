using System;
using System.ServiceModel;
using System.Threading;

namespace Abot.Plugins.Awesomium.Remote
{
   public class PageDownloaderIpcServer<TServerImpl> : IDisposable where TServerImpl : IRemotePageDownloader
   {
      private ServiceHost _host;

      public PageDownloaderIpcServer()
      {
         //WCF will call back on the thread it's created. Because we don't want it to call back to UI thread
         //we launch it on a random thread.
         var thread = new Thread(() =>
         {
            _host = new ServiceHost(typeof(TServerImpl), new Uri("net.pipe://localhost"));
            _host.AddServiceEndpoint(typeof(IRemotePageDownloader), CreateBinding(), "Awesomium");
            _host.Open();

         });
         thread.Start();
      }

      public static IRemotePageDownloader CreateClient()
      {
         var pipeFactory = new ChannelFactory<IRemotePageDownloader>(CreateBinding(),
            new EndpointAddress("net.pipe://localhost/Awesomium"));

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
