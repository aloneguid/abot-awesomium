using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Abot.Core;
using Abot.Poco;
using NUnit.Framework;

namespace Abot.Plugins.Awesomium.Test
{
   [TestFixture]
   public class AwesomiumPageRequesterTest
   {
      private DirectoryInfo _buildDir;
      private IPageRequester _requester;

      [TestFixtureSetUp]
      public void SetUp()
      {
         _requester = new AwesomiumPageRequester(BuildDir);
      }

      [TestFixtureTearDown]
      public void TearDown()
      {
         _requester.Dispose();
      }

      [Test]
      public void DownloadPage_Google_Downloads()
      {
         CrawledPage page = _requester.MakeRequest(new Uri("http://google.co.uk"));

         Assert.IsNotNull(page);
      }

      [Test]
      public void DownloadPage_ThreeOneAfterAnother_Downloads()
      {
         _requester.MakeRequest(new Uri("http://bbc.co.uk"));
         _requester.MakeRequest(new Uri("http://lookers.co.uk"));
         _requester.MakeRequest(new Uri("http://autotrader.co.uk"));
      }

      [Test]
      public void DownloadPage_InThreads_DownloadsAll()
      {
         Task<CrawledPage>[] tasks = new[]
         {
            NewThreadDownload(new Uri("http://bbc.co.uk")),
            NewThreadDownload(new Uri("http://lookers.co.uk")),
            NewThreadDownload(new Uri("http://autotrader.co.uk"))
         };

         Task.WaitAll(tasks);
      }

      private Task<CrawledPage> NewThreadDownload(Uri url)
      {
         return Task.Factory.StartNew(() => _requester.MakeRequest(url), TaskCreationOptions.LongRunning);
      }

      protected DirectoryInfo BuildDir
      {
         get
         {
            return _buildDir ??
                   (_buildDir = new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Directory);
         }
      }
   }
}
