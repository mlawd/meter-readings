using System.Threading.Tasks;
using MeterReader.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeterReader.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UploadController : ControllerBase
    {
        private IFileService _service;

        public UploadController(IFileService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile csv)
        {
            if (csv is null)
            {
                return BadRequest("CSV must be passed");
            }

            var result = await _service.ProcessFile(csv);

            return Ok(result);
        }
    }
}