namespace Observer
{
    public class TemperatureReporter : IObserver<Temperature>
    {
        private bool first = true;

        private Temperature last;

        private ISubject<Temperature> provider;

        public void StartReporting(ISubject<Temperature> provider)
        {
            this.provider = provider;
            this.first = true;
            this.provider.Subscribe(this);
        }

        public void StopReporting()
        {
            if (this.provider != null) this.provider.Unsubscribe(this);
        }

        public void Update(Temperature value)
        {
            System.Console.WriteLine($"The temperature is {value.Degrees}°C at {value.Date:g}");
            if (first)
            {
                last = value;
                first = false;
            }
            else
            {
                System.Console.WriteLine($"   Change: {value.Degrees - last.Degrees}° in " +
                    $"{value.Date.ToUniversalTime() - last.Date.ToUniversalTime():g}");
            }
        }
    }
}