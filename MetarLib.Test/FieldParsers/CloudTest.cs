using System.Linq;
using MetarLib.Enums;
using MetarLib.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class CloudTest : IFieldParserTestBase
    {
        public CloudTest() : base(new CloudParser()) {}

        [TestMethod]
        public void Metar_with_cloud()
        {
            var metar = GetMetar("FEW026");

            Assert.IsNotNull(metar.Clouds);
            Assert.AreEqual(1, metar.Clouds.Length);

            var cloud = metar.Clouds.Single();

            Assert.AreEqual(CloudCoverage.Few, cloud.Coverage);
            Assert.AreEqual(2600, cloud.Altitude);
            Assert.AreEqual(CloudConvectivity.None, cloud.Convectivity);
        }
        
        [TestMethod]
        public void Metar_with_cloud_and_empty_convectivity()
        {
            var metar = GetMetar("BKN127///");

            Assert.IsNotNull(metar.Clouds);
            Assert.AreEqual(1, metar.Clouds.Length);

            var cloud = metar.Clouds.Single();

            Assert.AreEqual(CloudCoverage.Broken, cloud.Coverage);
            Assert.AreEqual(12700, cloud.Altitude);
            Assert.AreEqual(CloudConvectivity.None, cloud.Convectivity);
        }
        
        [TestMethod]
        public void Metar_with_cloud_and_towering_cumulus()
        {
            var metar = GetMetar("SCT046TCU");

            Assert.IsNotNull(metar.Clouds);
            Assert.AreEqual(1, metar.Clouds.Length);

            var cloud = metar.Clouds.Single();

            Assert.AreEqual(CloudCoverage.Scattered, cloud.Coverage);
            Assert.AreEqual(4600, cloud.Altitude);
            Assert.AreEqual(CloudConvectivity.ToweringCumulus, cloud.Convectivity);
        }
        
        [TestMethod]
        public void Metar_with_no_significant_cloud()
        {
            var metar = GetMetar("NSC");

            Assert.IsNotNull(metar.Clouds);
            Assert.AreEqual(1, metar.Clouds.Length);

            var cloud = metar.Clouds.Single();

            Assert.AreEqual(CloudCoverage.NoSignificantCloud, cloud.Coverage);
            Assert.AreEqual(0, cloud.Altitude);
            Assert.AreEqual(CloudConvectivity.None, cloud.Convectivity);
        }
        
        [TestMethod]
        public void Metar_with_no_cloud_detected()
        {
            var metar = GetMetar("NCD");

            Assert.IsNotNull(metar.Clouds);
            Assert.AreEqual(1, metar.Clouds.Length);

            var cloud = metar.Clouds.Single();

            Assert.AreEqual(CloudCoverage.NoCloudDetected, cloud.Coverage);
            Assert.AreEqual(0, cloud.Altitude);
            Assert.AreEqual(CloudConvectivity.None, cloud.Convectivity);
        }
    }
}