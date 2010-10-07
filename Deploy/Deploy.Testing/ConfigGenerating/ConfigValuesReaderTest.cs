using Deploy.Lib.ConfigGenerating;
using Deploy.Lib.Readers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Deploy.Testing.ConfigGenerating
{
    [TestFixture]
    public class ConfigValuesReaderTest
    {
        private MockRepository _mockRepository;
        private ConfigValuesReader _configValuesReader;
        private IFileReader _fileReaderMock;
        private const string SomeConfigFilePath = "path";

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository();
            _fileReaderMock = _mockRepository.Stub<IFileReader>();
            _configValuesReader = new ConfigValuesReader(_fileReaderMock);
        }

        [Test]
        public void ShouldReadConfigValues()
        {
            var lines = new[]
                            {
                                "key1=value1",
                                "key2=\"value2\""
                            };
            using (_mockRepository.Record())
            {
                SetupResult.For(_fileReaderMock.ReadLines(SomeConfigFilePath)).Return(lines);
            }
            using (_mockRepository.Playback())
            {
                var dictionary = _configValuesReader.GetValues(SomeConfigFilePath);
                Assert.That(dictionary.Count, Is.EqualTo(2));
                Assert.That(dictionary["key1"], Is.EqualTo("value1"));
                Assert.That(dictionary["key2"], Is.EqualTo("\"value2\""));    
            }
        }
    }
}
