using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
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
            if (WebService == null)
                return null;

            SolicitudOnBase solicitud = new SolicitudOnBase();

            WebService.RequestUri = WbsGlobals.WbsFlujosRequestUri;
            WebService.SoapAction = WbsGlobals.WbsFlujosSoapAction;
            WebService.WebMethod = WbsGlobals.WbsFlujosWebMethod;
            WebService.WebNamespace = WbsGlobals.WbsFlujosWebNamespace;

            try
            {
                Dictionary<string, object> fields = WebService.Invoke($"NumeroSolicitud:{nroSolicitud}",
                                                                        $"SistemaOrigen:{sistemaOrigen}");

                solicitud.CodigoEstado = 1;

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
            }
            catch (XmlException)
            {
                solicitud.EsValido = false;
                solicitud.Mensaje = "NO HAY CONEXION A INTERNET";
                solicitud.CodigoEstado = 90;
            }
            catch (WebException)
            {
                solicitud.EsValido = false;
                solicitud.Mensaje = "No hay conexión con el servidor, por favor intente más tarde!";
                solicitud.CodigoEstado = 97;
            }
            catch (Exception)
            {
                solicitud.EsValido = false;
                solicitud.Mensaje = "SERVIDOR NO RESPONDE";
                solicitud.CodigoEstado = 99;
            }

            return solicitud;
        }
    }
}

