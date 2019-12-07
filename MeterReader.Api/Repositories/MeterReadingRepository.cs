using System;
using MeterReader.Api.Domain;
using MeterReader.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReader.Api.Repositories
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private SqlDbContext _context;

        public MeterReadingRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<bool> TryStoreReading(MeterReading reading)
        {
            try
            {
                var count = await _context.MeterReadings
                    .CountAsync(mr => mr.AccountId == reading.AccountId && mr.MeterReadingDateTime == reading.MeterReadingDateTime);

                var accountExists = await _context.Accounts
                    .FindAsync(reading.AccountId);

                if (count == 0 && accountExists != null)
                {
                    Console.WriteLine($"Inserting reading { reading }");
                    _context.MeterReadings.Add(reading);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}