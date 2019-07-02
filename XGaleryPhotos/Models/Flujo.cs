using System.Collections.Generic;

namespace XGaleryPhotos.Models
{
    public class Flujo
    {
        public int FlujoId { get; set; }
        public string FlujoNro { get; set; }
        public string Cliente { get; set; }
        public string Placa { get; set; }
        public string TipoDocumental { get; set; }
        public int TipoDocumentalNumero { get; set; }

        public List<Foto> Fotos { get; set; } 
    }
}
