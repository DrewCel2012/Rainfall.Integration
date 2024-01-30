using Rainfall.Service.DomainModels;

namespace Rainfall.Test.MockDataObjects
{
    internal class RainfallReadingMockData
    {
        public static FloodMonitoringReadingsDomain GetTestData
        {
            get
            {
                return new FloodMonitoringReadingsDomain
                {
                    items = new List<Item>
                    {
                        new() { dateTime = new DateTime(), value = 111 },
                        new() { dateTime = new DateTime(), value = 222 },
                        new() { dateTime = new DateTime(), value = 333 }
                    }
                };
            }
        }
    }
}
