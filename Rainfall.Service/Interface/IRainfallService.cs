using Rainfall.Model;

namespace Rainfall.Service.Interface
{
    public interface IRainfallService
    {
        Task<RainfallReadingResponse> GetRainfallReadingsAsync(string stationId, int? count = 10);
    }
}
