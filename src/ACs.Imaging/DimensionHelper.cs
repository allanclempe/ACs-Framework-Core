using System.Drawing;

namespace ACs.Imaging
{
    public static class DimensionHelper
    {
        public static int GetProportionalHeight(Bitmap bitmap, int width)
        {
            return (width * bitmap.Height) / bitmap.Width;
        }

        public static int GetProportionalWidth(Bitmap bitmap, int height)
        {
            return (height * bitmap.Width) / bitmap.Height;
        }

        public static int GetProportionalWidth(Image image, int height)
        {
            return GetProportionalWidth(image.Bitmap, height);
        }

        public static int GetProportionalHeight(Image image, int width)
        {
            return GetProportionalHeight(image.Bitmap, width);

        }

    }
}
