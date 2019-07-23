namespace XGaleryPhotos.Interfaces
{
    public interface IHelperImageService
    {
        string StretchImage(string path, float scaleFactor, int quality, string fileNameAdded, string watermark);
    }
}
