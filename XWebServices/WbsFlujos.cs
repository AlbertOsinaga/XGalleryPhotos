using System.Collections.Generic;

using XWebServices.Interfaces;
using XWebServices.Models;

namespace XWebServices
{
    public class WbsFlujos
    {
        public IXWebService WebService { get; set; }

        public WbsFlujos(IXWebService webService)
        {
            WebService = webService;
        }

        public SolicitudOnBase ConsultarFlujo(string nroSolicitud, string sistemaOrigen)
        {
            SolicitudOnBase solicitud = new SolicitudOnBase();
            if (WebService == null)
                return null;

            WebService.RequestUri = @"http://desanilus.lbc.bo/Nilus/WsOnbase/OnBaseWS.asmx";
            WebService.SoapAction = "http://tempuri.org/ObtenerInformacionSolicitudOnBase";
            WebService.WebMethod = "ObtenerInformacionSolicitudOnBase";
            WebService.WebNamespace = "http://tempuri.org/";

            Dictionary<string, object> fields = WebService.Invoke($"NumeroSolicitud:{nroSolicitud}",
                                                                    $"SistemaOrigen:{sistemaOrigen}");
            if (fields == null || fields.Count == 0)
                return null;

            foreach (var field in fields)
            {
                if (field.Key == "EsValido")
                    solicitud.EsValido = field.Value != null && ((string)field.Value).ToLower() == "true";
                if (field.Key == "Mensaje" && field.Value != null)
                    solicitud.Mensaje = (string)field.Value;
                if (field.Key == "nroSolicitud" && field.Value != null)
                    solicitud.NroSolicitud = (string)field.Value;
                if (field.Key == "Nombre" && field.Value != null)
                    solicitud.Nombre = (string)field.Value;
                if (field.Key == "Placa" && field.Value != null)
                    solicitud.Placa = (string)field.Value;
            }

            return solicitud;
        }
    }
}

