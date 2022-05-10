namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {        
            ISubject<Temperature> sensor = new TemperatureSensor();
            TemperatureReporter reporter = new TemperatureReporter();
            reporter.StartReporting(sensor);
            sensor.GetTemperature();
        }
    }
}