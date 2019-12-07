using System;
using System.Collections.Generic;
using System.Linq;
using MeterReader.Api.Domain;
using MeterReader.Api.Models;
using MeterReader.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace MeterReader.Test
{
    public class MeterReadingRepositoryTest
    {
        [Fact]
        public async void ReturnsTrueIfAdded()
        {
            var readings = new List<MeterReading>();
            var accounts = new List<Account>();

            var context = new Mock<SqlDbContext>(
                new DbContextOptions<SqlDbContext>()
            );

            var queryableAccounts = accounts.AsQueryable().BuildMockDbSet();
            queryableAccounts.Setup(q => q.FindAsync(1)).ReturnsAsync(
                new Account { AccountId = 1 }
            );

            context.Setup(m => m.Accounts)
                .Returns(queryableAccounts.Object);

            context.Setup(m => m.MeterReadings)
                .Returns(readings.AsQueryable().BuildMockDbSet().Object);

            var repo = new MeterReadingRepository(context.Object);

            var result = await repo.TryStoreReading(new MeterReading
            {
                AccountId = 1,
                MeterReadValue = 1,
                MeterReadingDateTime = new DateTime(2019, 1, 1)
            });

            Assert.True(result);
        }

        [Fact]
        public async void DoesNotAddIfAccountDoesNotExist()
        {
            var readings = new List<MeterReading>();
            var accounts = new List<Account>
            {
                new Account { AccountId = 2 }
            };

            var context = new Mock<SqlDbContext>(
                new DbContextOptions<SqlDbContext>()
            );


            var queryableAccounts = accounts.AsQueryable().BuildMockDbSet();

            context.Setup(m => m.Accounts)
                .Returns(queryableAccounts.Object);

            context.Setup(m => m.MeterReadings)
                 .Returns(readings.AsQueryable().BuildMockDbSet().Object);

            var repo = new MeterReadingRepository(context.Object);

            var result = await repo.TryStoreReading(new MeterReading
            {
                AccountId = 1,
                MeterReadValue = 1,
                MeterReadingDateTime = new DateTime(2019, 1, 1)
            });

            Assert.False(result);
        }

        [Fact]
        public async void IfMeterReadingExists()
        {
            var readings = new List<MeterReading>()
            {
                new MeterReading { AccountId = 1, MeterReadingDateTime = new DateTime(2019, 1, 1), MeterReadValue = 1 }
            };

            var accounts = new List<Account>
            {
                new Account { AccountId = 2 }
            };

            var context = new Mock<SqlDbContext>(
                new DbContextOptions<SqlDbContext>()
            );


            var queryableAccounts = accounts.AsQueryable().BuildMockDbSet();

            context.Setup(m => m.Accounts)
                .Returns(queryableAccounts.Object);

            context.Setup(m => m.MeterReadings)
                 .Returns(readings.AsQueryable().BuildMockDbSet().Object);

            var repo = new MeterReadingRepository(context.Object);

            var result = await repo.TryStoreReading(new MeterReading
            {
                AccountId = 1,
                MeterReadValue = 1,
                MeterReadingDateTime = new DateTime(2019, 1, 1)
            });

            Assert.False(result);
        }
    }
}