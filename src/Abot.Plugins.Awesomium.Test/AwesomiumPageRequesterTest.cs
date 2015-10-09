using System;
using System.IO;
using System.Reflection;
using Abot.Core;
using Abot.Poco;
using Aloneguid.Classificator.Crawler.Abot;
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
            var page1 = _requester.MakeRequest(new Uri("http://bbc.co.uk"));
            var page2 = _requester.MakeRequest(new Uri("http://lookers.co.uk"));
            var page3 = _requester.MakeRequest(new Uri("http://autotrader.co.uk"));
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
