using Xunit;
using MeterReader.Api.Controllers;
using Moq;
using MeterReader.Api.Services;
using Microsoft.AspNetCore.Http;
using MeterReader.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeterReader.Test
{
    public class UploadControllerTest
    {
        [Fact]
        public async void ReturnsTheNumberOfFailedAndSuccessfulInsertions()
        {
            var serviceMock = new Mock<IFileService>();
            serviceMock
                .Setup(s => s.ProcessFile(It.IsAny<IFormFile>()))
                .ReturnsAsync(new ProcessResult
                {
                    SuccessfulEntries = 2,
                    FailedEntries = 3
                });

            var controller = new UploadController(serviceMock.Object);

            var respose = await controller.Post(new Mock<IFormFile>().Object);

            Assert.IsType<OkObjectResult>(respose);

            var data = (respose as OkObjectResult).Value as ProcessResult;

            Assert.Equal(2, data.SuccessfulEntries);
            Assert.Equal(3, data.FailedEntries);
        }

        [Fact]
        public async void ReturnsBadRequestIfNoFileProvided()
        {
            var serviceMock = new Mock<IFileService>();

            var controller = new UploadController(serviceMock.Object);

            var response = await controller.Post(null);

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal("CSV must be passed", (response as BadRequestObjectResult).Value);
        }
    }
}
