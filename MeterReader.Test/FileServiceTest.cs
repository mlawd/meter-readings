using System.IO;
using Xunit;
using Moq;
using MeterReader.Api.Repositories;
using Microsoft.AspNetCore.Http;
using MeterReader.Api.Models;
using System;
using MeterReader.Api.Services;

namespace MeterReader.Test
{
    public class FileServiceTest
    {
        [Fact]
        public async void ReturnsTheCorrectNumberOfSuccessfulAndFailedEntries()
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(m => m.OpenReadStream()).Returns(
                File.OpenRead("../../../files/test.csv")
            );

            var repoMock = new Mock<IMeterReadingRepository>();
            repoMock.Setup(m => m.TryStoreReading(It.Is<MeterReading>(m => m.AccountId == 1))).ReturnsAsync(true);
            repoMock.Setup(m => m.TryStoreReading(It.Is<MeterReading>(m => m.AccountId == 2))).ReturnsAsync(false);
            repoMock.Setup(m => m.TryStoreReading(It.Is<MeterReading>(m => m.AccountId == 3))).ThrowsAsync(new Exception());
            
            var service = new FileService(repoMock.Object);
            var result = await service.ProcessFile(fileMock.Object);

            Assert.Equal(1, result.SuccessfulEntries);
            Assert.Equal(3, result.FailedEntries);
        }
    }
}