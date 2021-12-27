using IPCameraClient;
using OpenCvSharp;
using System.IO;
using Xunit;

namespace IPCameraClientTest
{
    public class ImageManipulateTest
    {
        [Fact]
        public void WhenGiveAByteArray_ThenReturnMat()
        {
            //Arrange
            string filename = "Assets//img//test.jpg";
            byte[] img = MakeImgAsByteArray(filename);
            //Act
            Mat a = ImageManipulate.ConvertToMat(img);
            
            //Assert
            Assert.Equal(typeof(Mat), a.GetType());
            Assert.NotNull(a);
        }

        private static byte[] MakeImgAsByteArray(string filename)
        {
            return File.ReadAllBytes(filename);
        }
    }
}