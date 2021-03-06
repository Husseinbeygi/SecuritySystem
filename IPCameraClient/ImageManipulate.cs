using OpenCvSharp;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace IPCameraClient
{
    public static class ImageManipulate
    {

        public static Bitmap PutText(this Bitmap img, string Text, Font font, int x, int y)
        {
            Graphics grPhoto = Graphics.FromImage(img);

            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            float yPosFromBottom = y;
            float xCenterOfImg = x;
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(255, 255, 255, 255));

            grPhoto.DrawString(Text,
                font,
                semiTransBrush2,
                new PointF(xCenterOfImg + 1, yPosFromBottom + 1),
                StrFormat);

            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            grPhoto.DrawString(Text,
                font,
                semiTransBrush,
                new PointF(xCenterOfImg, yPosFromBottom),
                StrFormat);

            return img;


        }



        public static Bitmap PutImage(this Bitmap img, Image waterMark, int x, int y, int watermarkwidth, int watermarkheight)
        {
            using (var gr = Graphics.FromImage(img))
            {
                gr.DrawImage(waterMark, new Rectangle(x, y, watermarkwidth, watermarkheight));
            }
            return img;
        }



    }
}
