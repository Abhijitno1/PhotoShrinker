using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShrinker
{
    public class ImageResizer
    {
        //Code Ref: https://www.codeproject.com/Articles/191424/Resizing-an-Image-On-The-Fly-using-NET
        public static Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            return DoResize(image, newWidth, newHeight);
        }

        public static void ShrinkImage(string fromFilePath, string toFilePath, Double sizePercent)
        {
            Image image = Image.FromFile(fromFilePath);
            int newWidth = (int)(image.Width * sizePercent / 100);
            int newHeight = (int)(image.Height * sizePercent / 100);

            Image newImage = DoResize(image, newWidth, newHeight);
            newImage.Save(toFilePath, ImageFormat.Jpeg);
        }

        private static Image DoResize(Image image, int newWidth, int newHeight)
        {
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public static void ShrinkImagesInFolder(string fromFolder, string toFolder)
        {

        }

        private static void TraverseFolder(DirectoryInfo inputFolder, string outputFolder)
        {
            foreach (var file in inputFolder.EnumerateFiles())
            {
                if (!new string[] { ".jpg", ".bmp" }.Contains(file.Extension?.ToLower())) continue;
                if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

                var outputFilePath = Path.Combine(outputFolder, file.Name);
                ShrinkImage(file.FullName, outputFilePath, 50);
            }
            foreach (var subfolder in inputFolder.EnumerateDirectories())
            {
                var outputFolderPath = Path.Combine(outputFolder, subfolder.Name);
                TraverseFolder(subfolder, outputFolderPath);
            }
        }

    }
}
