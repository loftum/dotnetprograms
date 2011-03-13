using System.Linq;
using System.Net;
using System.Xml.Linq;
using NUnit.Framework;
using ZenTester.Lib.Parsing;

namespace ZenTesting
{
    [TestFixture]
    public class ZenParserTest
    {
        private const string BaseUrl = "https://agilezen.com/api/v1";
        private const string ApiKey = "";

        private ZenParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new ZenParser();
        }

        [Test]
        public void Should()
        {
            const string relativeUrl = "";
            var request = (HttpWebRequest) WebRequest.Create(BaseUrl + relativeUrl);
            request.Headers["X-Zen-ApiKey"] = ApiKey;
            request.ContentType = "application/xml";
            var response = request.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                var document = XDocument.Load(stream);
                var stories = from s in document.Descendants("story")
                            select _parser.Parse(s);

                Assert.That(stories.Count(), Is.EqualTo(1));
                var story = stories.First();
                Assert.That(story.Id, Is.EqualTo(61));
                Assert.That(story.Comments.Count(), Is.EqualTo(11));
            }
        }
    }
}