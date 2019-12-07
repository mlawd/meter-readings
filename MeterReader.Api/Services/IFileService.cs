using System.Threading.Tasks;
using MeterReader.Api.Models;
using Microsoft.AspNetCore.Http;

namespace MeterReader.Api.Services
{
    public interface IFileService
    {
        Task<ProcessResult> ProcessFile(IFormFile file);
    }
}