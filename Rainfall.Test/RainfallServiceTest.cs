using Moq;
using Rainfall.Service;
using Rainfall.Service.DomainModels;
using Rainfall.Test.Helpers;
using Rainfall.Test.MockDataObjects;

namespace Rainfall.Test
{
    public class RainfallServiceTest
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory;

        public RainfallServiceTest()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        }


        [Fact]
        public void GetAsync_ShouldReturnRainfallReadingResponse()
        {
            ShouldReturnRainfallReadingResponse().Wait();
        }


        private async Task ShouldReturnRainfallReadingResponse()
        {
            //Arrange:
            var mockData = RainfallReadingMockData.GetTestData;
            var mockHandler = HttpClientHelper.GetResults<FloodMonitoringReadingsDomain>(mockData);
            var mockHttpClient = new HttpClient(mockHandler.Object)
            {
                BaseAddress = new Uri("http://environment.data.gov.uk/flood-monitoring/")
            };
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(mockHttpClient);

            var rainfallService = new RainfallService(_mockHttpClientFactory.Object);

            //Act:
            var results = await rainfallService.GetRainfallReadingsAsync("");

            //Assert
            Assert.NotNull(results);
            Assert.True(results.Readings?.Count > 0);
        }
    }
}