using Microsoft.VisualStudio.TestTools.UnitTesting;
using RtspClientCore.Utils;

namespace RtspClientCore.UnitTests.Utils
{
    [TestClass]
    public class RandomGeneratorFactoryTests
    {
        [TestMethod]
        public void CreateGenerator_CreateTwoGeneratorsAndCheckSeeds_SeedsDifferent()
        {
            var random1 = RandomGeneratorFactory.CreateGenerator();
            var random2 = RandomGeneratorFactory.CreateGenerator();

            Assert.AreNotEqual(random1.Next(), random2.Next());
        }
    }
}