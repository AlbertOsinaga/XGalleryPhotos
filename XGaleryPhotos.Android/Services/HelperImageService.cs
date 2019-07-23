using Android.Graphics;
using System;
using System.IO;
using Xamarin.Forms;
using XGaleryPhotos.Droid.Helpers;
using XGaleryPhotos.Droid.Services;
using XGaleryPhotos.Helpers;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;

[assembly: Dependency(typeof(HelperImageService))]
namespace XGaleryPhotos.Droid.Services
{
    public class HelperImageService : IHelperImageService
    {
        const string TemporalDirectoryName = "TmpMedia";

        public string StretchImage(string path, float scaleFactor, int quality, string fileNameAdded, string watermark = null)
        {
            byte[] imageBytes;

            var originalImage = BitmapFactory.DecodeFile(path);
            var width = (originalImage.Width * scaleFactor);
            var height = (originalImage.Height * scaleFactor);
            var scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);

            Bitmap watermarkedImage = scaledImage;
            if (watermark != null)
                watermarkedImage = ImageHelpers.Watermark(scaledImage, watermark);

            using (var ms = new MemoryStream())
            {
                watermarkedImage.Compress(Bitmap.CompressFormat.Jpeg, quality, ms);
                imageBytes = ms.ToArray();
            }


            originalImage?.Dispose();
            scaledImage?.Dispose();
            watermarkedImage?.Dispose();
            GC.Collect();

            var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            var ext = System.IO.Path.GetExtension(path) ?? string.Empty;

            var strechedPath = FileHelper.GetOutputPath(MediaFileType.Image, TemporalDirectoryName,
                                                            $"{fileName}-{fileNameAdded}{ext}");
            File.WriteAllBytes(strechedPath, imageBytes);

            return strechedPath;
        }
    }
}
