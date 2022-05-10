using System.IO;
using NUnit.Framework;
using Observer;

namespace LibraryTests
{
    /// <summary>
    /// Para hacer pruebas con un <see cref="TemperatureReporter"/> necesitamos un objeto <see cref="IObservable"/> por
    /// eso hacemos que la propia clase de prueba implemente esa interfaz.
    /// </summary>
    public class TemperatureReporterTests : ISubject<Temperature>
    {
        private TemperatureReporter reporter;
        private bool isSubscribed = false;

        [SetUp]
        public void Setup()
        {
            reporter = new TemperatureReporter();
        }

        /// <summary>
        /// Este método debería ser invocado cuando se ejecute el método
        /// <see cref="TemperatureReporter.StartReporting(ISubject)"/>. El objeto <see cref="IObserver"/> que está
        /// interesado en los cambios es <see cref="ObservableTests.reporter"/>.
        /// </summary>
        public void Subscribe(IObserver<Temperature> observer)
        {
            Assert.That(observer, Is.EqualTo(this.reporter));
            this.isSubscribed = true;
        }

        /// <summary>
        /// Este método debería ser invocado cuando se ejecute el método
        /// <see cref="TemperatureReporter.StopReporting(ISubject)"/>. El objeto <see cref="IObserver"/> que está
        /// interesado en los cambios es <see cref="ObservableTests.reporter"/>.
        /// </summary>
        public void Unsubscribe(IObserver<Temperature> observer)
        {
            Assert.That(observer, Is.EqualTo(this.reporter));
            this.isSubscribed = false;
        }

        /// <summary>
        /// Prueba que este objeto <see cref="IObservable"/> sea
        /// </summary>
        [Test]
        public void StartAndStopReportingTest()
        {
            Assert.That(this.isSubscribed, Is.False);

            reporter.StartReporting(this);
            Assert.That(this.isSubscribed, Is.True);

            reporter.StopReporting();
            Assert.That(this.isSubscribed, Is.False);
        }

        /// <summary>
        /// Prueba el comportamiento de las actualizaciones recibidas por <see cref="TemperatureReporter"/>. Como el método
        /// <see cref="TemperatureReporter.Update(Temperature)"/> escribe en la consola, para "ver" lo que seescribe en la
        /// consola es necesario "redirigir" la salida en la consola utilizando <see cref="System.Console.SetOut(TextWriter)"/>
        /// a un <see cref="StringWriter"/>.
        /// </summary>
        [Test]
        public void UpdateTestWhileReporting()
        {
            Temperature t1 = new Temperature(14.6m, new System.DateTime(1900, 01, 01, 01, 0, 0));
            Temperature t2 = new Temperature(14.7m, new System.DateTime(1900, 09, 01, 01, 0, 1));
            Temperature t3 = new Temperature(14.9m, new System.DateTime(1900, 09, 01, 01, 0, 2));

            using (StringWriter sw = new StringWriter())
            {
                System.Console.SetOut(sw);

                this.reporter.Update(t1);
                string expected = string.Format($"The temperature is {t1.Degrees}°C at {t1.Date:g}\n");
                Assert.That(sw.ToString(), Is.EqualTo(expected));

                this.reporter.Update(t2);
                decimal degressDelta = t2.Degrees - t1.Degrees;
                System.TimeSpan timeDelta = t2.Date.ToUniversalTime() - t1.Date.ToUniversalTime();
                expected += string.Format($"The temperature is {t2.Degrees}°C at {t2.Date:g}\n");
                expected += string.Format($"   Change: {degressDelta}° in {timeDelta:g}\n");
                Assert.That(sw.ToString(), Is.EqualTo(expected));

                this.reporter.Update(t3);
                degressDelta = t3.Degrees - t1.Degrees;
                timeDelta = t3.Date.ToUniversalTime() - t1.Date.ToUniversalTime();
                expected += string.Format($"The temperature is {t3.Degrees}°C at {t3.Date:g}\n");
                expected += string.Format($"   Change: {degressDelta}° in {timeDelta:g}\n");
                Assert.That(sw.ToString(), Is.EqualTo(expected));
            }
        }
    }
}












