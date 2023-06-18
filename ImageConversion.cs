using System;
using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessor;
using ImageProcessor.Plugins.WebP;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace RecortadorDeImagenes
{
    public static class ImageConversion
    {

        public static Bitmap LoadWebPImage(string filePath)
        {
            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false)) 
            {
                using (Image image = imageFactory.Load(filePath)
                                      .Format(new WebPFormat())
                                      .Quality(100)
                                      .BackgroundColor(Color.Transparent)
                                      .Image)
                {
                    Bitmap bitmap = new Bitmap(image);
                    return bitmap;
                    
                }
            }   
        }

        public static Bitmap LoadPNG (string filePath)
        {
            
            using (Bitmap originalImage=new Bitmap (filePath))
            {
                Bitmap bitmap=new Bitmap(originalImage.Width,originalImage.Height, PixelFormat.Format32bppArgb);
                for (int i = 0; i < originalImage.Height; i++)
                {
                    for (int j = 0; j < originalImage.Width; j++)
                    {
                        Color pixelColor = originalImage.GetPixel(j, i);

                        if (pixelColor.A == 0)
                        {
                            bitmap.SetPixel(j, i, Color.White);
                        }
                        else
                        {
                            bitmap.SetPixel(j, i, pixelColor);
                        }
                    }
                }
                return bitmap;
            }
            
        } 

        public static Bitmap LoadBitmapAccordingFormat(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            switch (extension)
            {
                case ".jpg":
                    Bitmap LoadJPG = new Bitmap(filePath);
                    return LoadJPG;

                case ".png":
                    return LoadPNG(filePath);
                    
                case ".webp":
                    return LoadWebPImage(filePath);
                    
                default:
                    return null;                
                    
            }
        }
    }
}
