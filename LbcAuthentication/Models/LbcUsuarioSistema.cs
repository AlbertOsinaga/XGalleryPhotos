using System.Collections.Generic;

namespace LbcAuthentication.Models
{
    public class LbcUsuarioSistema
    {
        public int CodigoEstado { get; set; }
        public LbcEstado Estado { get; set; }
        public string Usuario { get; set; }
        public string NombreLargo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string CodigoSucursal { get; set; }
        public string Sucursal { get; set; }

        public List<LbcRol> Roles { get; set; }
    }
}
