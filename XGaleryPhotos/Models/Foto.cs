namespace XGaleryPhotos.Models
{
    public class Foto
    {
        public int FotoId { get; set; }
        public string Path { get; set; }
        public string ImgString { get; set; }

        public int FlujoId { get; set; }
        public Flujo Flujo { get; set; }
    }
}
