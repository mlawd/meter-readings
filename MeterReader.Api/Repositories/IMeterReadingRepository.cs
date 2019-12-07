using System.Threading.Tasks;
using MeterReader.Api.Models;

namespace MeterReader.Api.Repositories
{
    public interface IMeterReadingRepository
    {
        Task<bool> TryStoreReading(MeterReading reading);
        Task Commit();
    }
}