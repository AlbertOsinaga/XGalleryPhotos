using System;
using System.IO;

namespace XGaleryPhotos.Helpers
{
    public static class Base64Helper
    {
        public static string MediaPathToCode64(string mediaPath)
        {
            string base64String = string.Empty;

            // provide read access to the file
            FileStream fs = new FileStream(mediaPath, FileMode.Open, FileAccess.Read);

            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];

            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

            //Close the File Stream
            fs.Close();

            base64String = Convert.ToBase64String(ImageData);

            return base64String;
        }
    }
}
