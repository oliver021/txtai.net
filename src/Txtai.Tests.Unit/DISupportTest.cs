namespace Txtai.Tests.Unit
{
    using Microsoft.Extensions.DependencyInjection;

    public class DISupportTest
    {
        private const string SmapleUrlBase = "https://localhost:8080/";
        IServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            var collection = new ServiceCollection();

            collection.AddTxtai(SmapleUrlBase);

            serviceProvider = collection.BuildServiceProvider();
        }

        [Test]
        public void TestInjectionSetup()
        {
            Assert.IsNotNull(serviceProvider.GetService<Workflow>());
            Assert.IsNotNull(serviceProvider.GetService<Labels>());
            Assert.IsNotNull(serviceProvider.GetService<Embeddings>());
            Assert.IsNotNull(serviceProvider.GetService<Segmentation>());
        }

        [Test]
        public void TestCorrectlyUrl()
        {
            var labels = serviceProvider.GetService<Labels>();

            if (labels is null)
            {
                // crash testing
                Assert.Fail();
            }

            Assert.That(labels.GetHttpClient().BaseAddress.ToString(), Is.EqualTo(SmapleUrlBase));
        }
    }
}