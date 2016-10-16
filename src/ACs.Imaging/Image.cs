using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using AForge.Imaging.Filters;

namespace ACs.Imaging
{
    public class Image
    {
        public Bitmap Bitmap { get; private set; }

        public Image(Stream stream)
        {
            Bitmap = new Bitmap(stream);
        }

        public Image Resize(int width, int height)
        {
            var result = new Bitmap(width, height);

            //use a graphics object to draw the resized image into the bitmap
            using (var graphics = Graphics.FromImage(result))
            {
                //set the resize quality modes to high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //draw the image into the target bitmap
                graphics.DrawImage(Bitmap, 0, 0, result.Width, result.Height);
            }

            Bitmap = result;
            //return the resulting bitmap
            return this;
        }

        public Image Crop(int x, int y, int width, int height)
        {
            var crop = new Crop(new Rectangle(x, y, width, height));
            Bitmap = crop.Apply(Bitmap);

            return this;
        }

        public Stream ToStream(ImageFormat imageFormat)
        {
            var stream = new MemoryStream();

            Bitmap.Save(stream, imageFormat);

            if (stream.CanSeek)
                stream.Seek(0, SeekOrigin.Begin);

            return stream;

        }

    }
}
