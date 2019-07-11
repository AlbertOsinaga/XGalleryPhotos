using System.Collections.Generic;

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

        public bool ConsultarUsuarioSistema(string usuario, string sistema, string password)
        {
            if (WebService == null)
                return false;

            WebService.RequestUri = @"http://desarrollo.lbc.bo/Servicios/generales/ConsultaUsuarioSistema.asmx";
            WebService.SoapAction = "http://tempuri.org/ConsultarUsuarioSistema";
            WebService.WebMethod = "ConsultarUsuarioSistema";
            WebService.WebNamespace = "http://tempuri.org/";

            Dictionary<string, object> fields = WebService.Invoke($"usuario:{usuario}", $"sistema:{sistema}", $"password:{password}");
            if (fields == null || fields.Count == 0)
                return false;

            foreach (var field in fields)
            {
                if (field.Key == "CodigoEstado" && (string)field.Value == "1")
                    return true;
            }

            return false;
        }
    }
}
