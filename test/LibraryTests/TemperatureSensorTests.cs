using System.Collections.Generic;
using NUnit.Framework;
using Observer;

namespace LibraryTests
{
    /// <summary>
    /// Para hacer pruebas con un <see cref="TemperatureSensor"/> necesitamos un objeto <see cref="IObserver"/> por
    /// eso hacemos que la propia clase de prueba implemente esa interfaz.
    /// </summary>
    public class TemperatureSensorTests : IObserver<Temperature>
    {
        private TemperatureSensor sensor;
        private IList<Temperature> readings = new List<Temperature>();

        [SetUp]
        public void Setup()
        {
            sensor = new TemperatureSensor();
        }

        /// <summary>
        /// Este método debería ser invocado cuando se ejecute el método <see cref="TemperatureSensor.GetTemperature()"/>
        /// sólo mientras este observador esté suscrito a los cambios.
        /// </summary>
        public void Update(Temperature value)
        {
            TestContext.Out.WriteLine($"The temperature is {value.Degrees}°C at {value.Date:g}");
            readings.Add(value);
        }

        /// <summary>
        /// Este caso prueba el método <see cref="TemperatureSensor.GetTemperature()"/>. Cuando una instancia de esta
        /// clase de prueba está suscrita a los cambios en <see cref="TemperatureSensor"/> debería recibir notificaciones
        /// de los cambios y se debería ejecutar el método <see cref="TemperatureSensorTests.Update(Temperature)"/>, donde
        /// se agregan las temperaturas que van cambiando a la lista <see cref="TemperatureSensorTests.readings"/>. La
        /// primera parte de prueba verifica que todas las temperaturas en <see cref="TemperatureSensor.SampleData"/> hayan
        /// sido agregadas a la lista mientras este observador está suscrito. Luego la segunda parte de la prueba verifica que
        /// no se invoque el méotodo <see cref="TemperatureSensorTests.Update(Temperature)"/> cuando este observador no está
        /// suscrito.
        /// </summary>
        [Test]
        public void GetTemperature()
        {
            sensor.Subscribe(this);
            sensor.GetTemperature();

            foreach (Temperature temperature in this.readings)
            {
                Assert.That(sensor.SampleData, Contains.Item(temperature.Degrees));
            }

            sensor.Unsubscribe(this);
            readings.Clear();
            sensor.GetTemperature();

            Assert.That(readings, Is.Empty);
        }
    }
}