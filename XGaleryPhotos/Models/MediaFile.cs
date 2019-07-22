namespace XGaleryPhotos.Models
{
    public class MediaFile
    {
        public string Id { get; set; }
        public string OnBasePath { get; set; }
        public string PreviewPath { get; set; }
        public string Path { get; set; }
        public MediaFileType Type { get; set; }
    }
}
