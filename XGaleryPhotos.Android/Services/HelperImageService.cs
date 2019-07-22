using Android.Graphics;
using System;
using System.IO;
using Xamarin.Forms;
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

        public string StretchImage(string path, float scaleFactor, int quality)
        {
            byte[] imageBytes;

            var originalImage = BitmapFactory.DecodeFile(path);
            var width = (originalImage.Width * scaleFactor);
            var height = (originalImage.Height * scaleFactor);
            var scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);

            using (var ms = new MemoryStream())
            {
                scaledImage.Compress(Bitmap.CompressFormat.Jpeg, quality, ms);
                imageBytes = ms.ToArray();
            }


            originalImage?.Dispose();
            GC.Collect();

            var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
            var ext = System.IO.Path.GetExtension(path) ?? string.Empty;

            var strechedPath = FileHelper.GetOutputPath(MediaFileType.Image, TemporalDirectoryName, $"{fileName}-ONBASE{ext}");
            File.WriteAllBytes(strechedPath, imageBytes);

            return strechedPath;
        }
    }
}
