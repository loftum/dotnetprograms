using FakeItEasy;
using NUnit.Framework;
using StuffLibrary.Common.Configuration;
using StuffLibrary.Lib.RottenTomatoes;
using StuffLibrary.UnitTesting.TestData;

namespace StuffLibrary.UnitTesting.Core.RottenTomatoes
{
    [TestFixture]
    public class ApiUrlsTest
    {
        private ApiUrls _urls;

        [SetUp]
        public void Setup()
        {
            var settings = A.Fake<IStuffLibrarySettings>();
            A.CallTo(() => settings.RottenTomatoesApiKey).Returns(Some.ApiKey);
            _urls = new ApiUrls(settings);
        }

        [Test]
        public void MovieSearchUrl_ContainsQueryAndPageNumberAndPageSize()
        {
            var movieSearch = _urls.Movies(new SearchParamters("banan", 13, 25)).ToString();

            Assert.That(movieSearch, Is.EqualTo("http://api.rottentomatoes.com/api/public/v1.0/movies.json?apikey=ApiKey&q=banan&page=13&page_limit=25"));
        }
    }
}