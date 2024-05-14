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
            int minX = firstImage.Width;
            int minY = firstImage.Height;
            if (minX > secondImage.Width) { minX = secondImage.Width; }
            if (minY > secondImage.Height) { minY = secondImage.Height; }
            if (minX < minY)
            {
                if (minX > 100) { minX = 100; }
                if (minY > 160) { minY = 160; }
                firstImage = ResizeImage(firstImage, new Size(minX, minY));
                secondImage = ResizeImage(secondImage, new Size(minX, minY));
            }else
            {
                if (minX > 160) { minX = 160; }
                if (minY > 100) { minY = 100; }
                firstImage = ResizeImage(firstImage, new Size(minY, minX));
                secondImage = ResizeImage(secondImage, new Size(minY, minX));
            }
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
            using (Graphics gr = Graphics.FromImage(img))
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
                ia.SetThreshold((float)0.8); 
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
