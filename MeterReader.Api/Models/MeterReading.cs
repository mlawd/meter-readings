using System;
using CsvHelper.Configuration.Attributes;

namespace MeterReader.Api.Models
{
    public class MeterReading
    {
        public int MeterReadingId { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public int MeterReadValue { get; set; }

        public static MeterReading From(CsvMeterReading csvReading)
        {
            return new MeterReading
            {
                AccountId = csvReading.AccountId,
                MeterReadingDateTime = csvReading.MeterReadingDateTime,
                MeterReadValue = int.Parse(csvReading.MeterReadValue)
            };
        }

        public override string ToString()
        {
            return $"{ AccountId } - { MeterReadValue } - { MeterReadingDateTime }";
        }
    }
}