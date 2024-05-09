using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace WpfPrototype.additionalLogic
{
    internal class ImageComparator
    {
        public static double CompareImagesAndReturnPercentageOfSimilarity(Image firstImage, Image secondImage)
        {
            double similarityPercentage = 0.0;
            if (firstImage != null && secondImage != null && ((firstImage.Width > firstImage.Height) == (secondImage.Width > secondImage.Height)))
            {
                Image newFirstImage = new Bitmap(firstImage);
                Image newSecondImage = new Bitmap(secondImage);
            
                MakeImagesTheSameSize(newFirstImage, newSecondImage);

                firstImage = MakeImageBlackAndWhite(newFirstImage);
                secondImage = MakeImageBlackAndWhite(newSecondImage);


                similarityPercentage = CompareTwoImagesAndReturnSimilarityPercentage(newFirstImage, newSecondImage);
            }
            return similarityPercentage;
        }

        private static void MakeImagesTheSameSize(Image firstImage, Image secondImage)
        {
            firstImage = ResizeImage(firstImage, new Size(100, 160));
            secondImage = ResizeImage(secondImage, new Size(100, 160));
        }

        private unsafe static double CompareTwoImagesAndReturnSimilarityPercentage(Image firstImage, Image secondImage)
        {
            double similarityPercentage = 0.0;
            if (firstImage.Width == secondImage.Width && firstImage.Height == secondImage.Height)
            {

                int countOfSamePixels = 0;
                Bitmap firstBitmap = new Bitmap(firstImage);
                Bitmap secondBitmap = new Bitmap(secondImage);
                BitmapData firstBitmapData = firstBitmap.LockBits(new Rectangle(0, 0, firstImage.Width, firstImage.Height), ImageLockMode.ReadWrite, firstBitmap.PixelFormat);
                BitmapData secondBitmapData = secondBitmap.LockBits(new Rectangle(0, 0, secondImage.Width, secondImage.Height), ImageLockMode.ReadWrite, secondBitmap.PixelFormat);
                byte* scan0FirstImage = (byte*)firstBitmapData.Scan0.ToPointer();
                int strideFirstImage = firstBitmapData.Stride / 4;

                byte* scan0SecondImage = (byte*)secondBitmapData.Scan0.ToPointer();
                int strideSecondImage = secondBitmapData.Stride / 4;


                for (int i = 0; i < firstBitmapData.Width; ++i)
                {
                    for (int j = 0; j < firstBitmapData.Height; ++j)
                    {
                        UInt32 pixelColourFirstImage = scan0FirstImage[(i * strideFirstImage) + j];
                        UInt32 pixelColourSecondImage = scan0SecondImage[(i * strideSecondImage) + j];
                        if (pixelColourFirstImage == pixelColourSecondImage) { countOfSamePixels = countOfSamePixels + 1; }
                    }
                }

                firstBitmap.UnlockBits(firstBitmapData);
                secondBitmap.UnlockBits(secondBitmapData);
                similarityPercentage = (countOfSamePixels * 100) / (firstImage.Width * firstImage.Height);
            }
            return similarityPercentage;
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
