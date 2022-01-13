using System;
using RtspClientCore.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RtspClientCore.UnitTests.Utils
{
    [TestClass]
    public class ArraySegmentExtensionsTests
    {
        [TestMethod]
        public void SubSegment_OffsetSet_ReturnsCorrectSubSegment()
        {
            var segment = new ArraySegment<byte>(new byte[128], 10, 100);

            ArraySegment<byte> subSegment = segment.SubSegment(10);

            Assert.AreEqual(20, subSegment.Offset);
            Assert.AreEqual(90, subSegment.Count);
        }
    }
}