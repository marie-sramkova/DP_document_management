using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace WpfPrototype.additionalLogic
{
    internal class ImageComparator
    {
        public static double CompareImagesAndReturnPercentageOfSimilarity(Image firstImage, Image secondImage)
        {
            Image newFirstImage = new Bitmap(firstImage);
            Image newSecondImage = new Bitmap(secondImage);
            double similarityPercentage = 0.0;
            if (firstImage != null && secondImage != null)
            {
                MakeImagesTheSameSize(newFirstImage, newSecondImage);
            }
            return similarityPercentage;
        }

        private static void MakeImagesTheSameSize(Image firstImage, Image secondImage)
        {
            int minX = firstImage.Width;
            int minY = firstImage.Height;
            if (secondImage.Width > minX) { minX = secondImage.Width; }
            if (secondImage.Height > minY) { minY = secondImage.Height; }
            if (firstImage.Width > minX || firstImage.Height > minY)
            {
                firstImage = ResizeImage(firstImage, new Size(minX, minY));
            }
            if (secondImage.Width > minX || secondImage.Height > minY)
            {
                secondImage = ResizeImage(secondImage, new Size(minX, minY));
            }
            firstImage = ResizeImage(firstImage, new Size(50, 80));
            secondImage = ResizeImage(secondImage, new Size(50, 80));

            firstImage = MakeImageBlackAndWhite(firstImage);
            secondImage = MakeImageBlackAndWhite(secondImage);

            firstImage.Save("D:\\sramk\\Documents\\vysoka_skola\\diplomka\\zkusebniSlozka\\newImage2.jpg");
            secondImage.Save("D:\\sramk\\Documents\\vysoka_skola\\diplomka\\zkusebniSlozka\\newImage.jpg");

        }

        private static Image MakeImageBlackAndWhite(Image img)
        {
            using (Graphics gr = Graphics.FromImage(img)) // SourceImage is a Bitmap object
            {
                var gray_matrix = new float[][] {
                new float[] { 0.299f, 0.299f, 0.299f, 0, 0 },
                new float[] { 0.587f, 0.587f, 0.587f, 0, 0 },
                new float[] { 0.114f, 0.114f, 0.114f, 0, 0 },
                new float[] { 0,      0,      0,      1, 0 },
                new float[] { 0,      0,      0,      0, 1 }
            };

                var ia = new System.Drawing.Imaging.ImageAttributes();
                ia.SetColorMatrix(new System.Drawing.Imaging.ColorMatrix(gray_matrix));
                ia.SetThreshold((float)0.8); // Change this threshold as needed
                var rc = new Rectangle(0, 0, img.Width, img.Height);
                gr.DrawImage(img, rc, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
            }
            return img;
        }

        private static Image ResizeImage(Image image, Size size)
        {
            image = (Image)(new Bitmap(image, size));
            return image;
        }
    }
}
