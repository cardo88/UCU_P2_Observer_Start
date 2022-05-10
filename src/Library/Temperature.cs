using System;

namespace Observer
{
    public struct Temperature
    {
        public Temperature(decimal temperature, DateTime dateAndTime)
        {
            this.Degrees = temperature;
            this.Date = dateAndTime;
        }

        public decimal Degrees { get; }
        public DateTime Date { get; }
    }
}