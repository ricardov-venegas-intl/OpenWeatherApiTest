using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenWeatherApiTest.OpenWeatherMapApi.Implementation;

using System;

namespace OpenWeatherApiTest.Tests
{
    /// <summary>
    /// OpenWeatherMapClient Tests
    /// </summary>
    [TestClass]
    public class OpenWeatherMapClientTests
    {
        /// <summary>
        /// Validates argument on the OpenWeatherMapClient constructor 
        /// </summary>
        [TestMethod]
        public void ConstrucorArgumentValidation()
        {

            Assert.ThrowsException<ArgumentNullException>(() => new OpenWeatherMapClient(null));
        }
    }
}
