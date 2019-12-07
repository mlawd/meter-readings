using System.Text.RegularExpressions;
using MeterReader.Api.Models;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using System.IO;
using System;
using MeterReader.Api.Repositories;
using System.Threading.Tasks;

namespace MeterReader.Api.Services
{
    public class FileService : IFileService
    {
        private IMeterReadingRepository _mrRepo;

        public FileService(IMeterReadingRepository mrRepo)
        {
            _mrRepo = mrRepo;
        }

        public async Task<ProcessResult> ProcessFile(IFormFile file)
        {
            using TextReader stream = new StreamReader(file.OpenReadStream());

            using var csv = new CsvReader(stream);

            var result = new ProcessResult { FailedEntries = 0, SuccessfulEntries = 0 };

            while (csv.Read())
            {
                try
                {
                    var record = csv.GetRecord<CsvMeterReading>();

                    var validReading = new Regex("^\\d{5}$").IsMatch(record.MeterReadValue);

                    if (validReading && await _mrRepo.TryStoreReading(MeterReading.From(record)))
                    {
                        result.SuccessfulEntries++;
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing row { 1 + result.FailedEntries + result.SuccessfulEntries }");
                    Console.WriteLine(ex);
                }

                result.FailedEntries++;
            }

            await _mrRepo.Commit();

            return result;
        }
    }
}