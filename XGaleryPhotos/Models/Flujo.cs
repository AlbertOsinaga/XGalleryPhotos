using System.Collections.Generic;

namespace XGaleryPhotos.Models
{
    public class Flujo
    {
        public int FlujoId { get; set; }
        public string FlujoNro { get; set; }
        public string Cliente { get; set; }
        public string Placa { get; set; }
        public string TipoDocumento { get; set; }
        public int DocumentoNro { get; set; }

        public string Mensaje { get; set; }
        public bool EsValido { get; set; }
        public string DatosAdicionales { get; set; }
        public string ListaMensajes { get; set; }

        public List<Foto> Fotos { get; set; } 
    }
}
