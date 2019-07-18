using System.Collections.Generic;
using XWebServices.Models;
using XWebServices.Interfaces;

namespace XWebServices
{
    public class WbsSeguridad
    {
        public IXWebService WebService { get; set; }

        public WbsSeguridad(IXWebService webService)
        {
            WebService = webService;
        }

        public UsuarioSistema ConsultarUsuarioSistema(string user, string sistema, string password)
        {
            UsuarioSistema usuario = new UsuarioSistema();
            usuario.Estado = "NO WEBSERVICE";

            if (WebService == null)
                return usuario;

            WebService.RequestUri = @"http://desarrollo.lbc.bo/Servicios/generales/ConsultaUsuarioSistema.asmx";
            WebService.SoapAction = "http://tempuri.org/ConsultarUsuarioSistema";
            WebService.WebMethod = "ConsultarUsuarioSistema";
            WebService.WebNamespace = "http://tempuri.org/";

            Dictionary<string, object> fields = WebService.Invoke($"usuario:{user}", $"sistema:{sistema}", $"password:{password}");
            if (fields == null || fields.Count == 0)
            {
                usuario.Estado = "WEBSERVICE NO RESPONDE";
                return usuario;
            }

            foreach (var field in fields)
            {
                if (field.Key == "CodigoEstado")
                    usuario.CodigoEstado = (string) field.Value;
                if (field.Key == "Estado")
                    usuario.Estado = (string)field.Value;
                if (field.Key == "Usuario")
                    usuario.Usuario = (string)field.Value;
                if (field.Key == "NombreLargo")
                    usuario.NombreLargo = (string)field.Value;
                if (field.Key == "Nombres")
                    usuario.Nombres = (string)field.Value;
                if (field.Key == "Apellidos")
                    usuario.Apellidos = (string)field.Value;
                if (field.Key == "Correo")
                    usuario.Correo = (string)field.Value;
                if (field.Key == "CodigoSucursal")
                    usuario.CodigoSucursal = (string)field.Value;
                if (field.Key == "Sucursal")
                    usuario.Sucursal = (string)field.Value;
                if (field.Key == "Roles")
                    usuario.Roles = (string)field.Value;
            }

            return usuario;
        }
    }
}
