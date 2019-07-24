using System;
using System.IO;
using Android.Graphics;
using Android.Media;

namespace XGaleryPhotos.Droid.Helpers
{
    public static class ImageHelpers
    {
        public static Color ColorWatermark = Color.Purple; 

        public static byte[] RotateImage(string path,float scaleFactor,int quality = 90, string watermark = null)
        {
            byte[] imageBytes;

            var originalImage = BitmapFactory.DecodeFile(path);
            var rotation = GetRotation(path);
            var width = (originalImage.Width * scaleFactor);
            var height = (originalImage.Height *scaleFactor);
            var scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);

            Bitmap rotatedImage = scaledImage;
            if (rotation != 0)
            {
                var matrix = new Matrix();
                matrix.PostRotate(rotation);
                rotatedImage = Bitmap.CreateBitmap(scaledImage, 0, 0, scaledImage.Width, scaledImage.Height, matrix, true);
                scaledImage.Recycle();
                scaledImage.Dispose();
            }

            Bitmap watermarkedImage = rotatedImage;
            if (watermark != null)
                watermarkedImage = Watermark(rotatedImage, watermark);

            using (var ms = new MemoryStream())
            {
                watermarkedImage.Compress(Bitmap.CompressFormat.Jpeg, quality, ms);
                imageBytes = ms.ToArray();
            }
      
           
            originalImage?.Dispose();
            rotatedImage?.Dispose();
            watermarkedImage?.Dispose();
            GC.Collect();

            return imageBytes;
        }

        static int GetRotation(string filePath)
        {
            using (var ei = new ExifInterface(filePath))
            {
                var orientation = (Android.Media.Orientation)ei.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);

                switch (orientation)
                {
                    case Android.Media.Orientation.Rotate90:
                        return 90;
                    case Android.Media.Orientation.Rotate180:
                        return 180;
                    case Android.Media.Orientation.Rotate270:
                        return 270;
                    default:
                        return 0;
                }
            }
        }

        public static Bitmap Watermark(Bitmap image, string watermark)
        {
            Bitmap bmpresult = Bitmap.CreateBitmap(image.Width, image.Height, image.GetConfig());
            Canvas canvas = new Canvas(bmpresult);
            canvas.DrawBitmap(image, 0, 0, null);
            Paint paintText = new Paint(PaintFlags.AntiAlias);
            paintText.Color = ColorWatermark;
            paintText.TextSize = Math.Max(32, image.Height/32);
            paintText.SetTypeface(Typeface.Monospace);
            canvas.DrawText(watermark, Math.Max(40, image.Width / 32 + 40),
                                        Math.Max(40, image.Height / 32 + 40),
                                        paintText);


            //paintText.SetShadowLayer(10f, 10f, 10f, Color.Black);
            //Rect rectText = new Rect();
            //paintText.GetTextBounds(watermark, 0, watermark.Length, rectText);
            //paintText.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcOver));
            //paintText.Alpha = 1;
            //paintText.AntiAlias = true;
            //paintText.UnderlineText = true;

            return bmpresult;
        }
    }
}
