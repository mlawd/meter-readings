using System;
using CsvHelper.Configuration.Attributes;

namespace MeterReader.Api.Models
{
    public class CsvMeterReading
    {
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set;}
    }
}