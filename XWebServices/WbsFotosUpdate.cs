using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using XWebServices.Interfaces;
using XWebServices.Models;

namespace XWebServices
{
    public class WbsFotosUpdate
    {
        public IXWebService WebService { get; set; }

        public WbsFotosUpdate(IXWebService webService)
        {
            WebService = webService;
        }

        public RespuestaUpdate UpdateFotos(string flujoNro, string tipoDocumento, int nroRC,
                                    string usuarioSistema, string extensionArchivo, string sistemaOrigen,
                                    params string[] images64)
        {
            RespuestaUpdate respuestaUpdate = new RespuestaUpdate();
            if (WebService == null)
            {
                respuestaUpdate.EsValido = false;
                respuestaUpdate.Mensaje = "WEB SERVICE NO CREADO";
                respuestaUpdate.CodigoEstado = 99;
                return respuestaUpdate;
            }

            WebService.RequestUri = WbsGlobals.WbsFotosUpdateRequestUri;
            WebService.SoapAction = WbsGlobals.WbsFotosUpdateSoapAction;
            WebService.WebMethod = WbsGlobals.WbsFotosUpdateWebMethod;
            WebService.WebNamespace = WbsGlobals.WbsFotosUpdateWebNamespace;

            List<string> parametros = new List<string>()
            {
                "<documento>",
                                        $"nroSolicitud:{flujoNro}",
                                        $"tipoDocumento:{tipoDocumento}",
                                        $"extensionArchivo:{extensionArchivo}",
                                        "<keywords>",
                                            "<KeywordOnBaseEntity>",
                                                $"nombre:LBC UserName WS",
                                                $"valor:{usuarioSistema}",
                                            "</KeywordOnBaseEntity>",
                                            "<KeywordOnBaseEntity>",
                                                $"nombre:No. de RC",
                                                $"valor:{nroRC}",
                                            "</KeywordOnBaseEntity>",
                                        "</keywords>",
                                    "</documento>",
                                    "<archivosBase64>"
            };
            if (images64 != null && images64.Length > 0)
            {
                foreach (string image in images64)
                    parametros.Add($"string:{image}");
            }
            else
            {
                parametros.Add($"string:{string.Empty}");
            }

            parametros.Add("</archivosBase64>");
            parametros.Add($"SistemaOrigen:{sistemaOrigen}");

            try
            {
                Dictionary<string, object> fields = WebService.Invoke(parametros.ToArray());

                if (fields == null || fields.Count == 0)
                {
                    respuestaUpdate.EsValido = false;
                    respuestaUpdate.Mensaje = "ERROR EN WEBSERVICE";
                    respuestaUpdate.CodigoEstado = 98;
                    return respuestaUpdate;
                }

                respuestaUpdate.CodigoEstado = 1;
                foreach (var field in fields)
                {
                    if (field.Key == "Mensaje" && field.Value != null)
                    {
                        respuestaUpdate.Mensaje = (string)field.Value;
                    }
                    if (field.Key == "EsValido")
                    {
                        respuestaUpdate.EsValido = field.Value != null && ((string)field.Value).ToLower() == "true";
                    }
                }
            }
            catch (XmlException)
            {
                respuestaUpdate.EsValido = false;
                respuestaUpdate.Mensaje = "NO HAY CONEXION A INTERNET";
                respuestaUpdate.CodigoEstado = 90;
            }
            catch (WebException)
            {
                respuestaUpdate.EsValido = false;
                respuestaUpdate.Mensaje = "No hay conexión con el servidor, por favor intente más tarde!";
                respuestaUpdate.CodigoEstado = 97;
            }
            catch (Exception)
            {
                respuestaUpdate.EsValido = false;
                respuestaUpdate.Mensaje = "SERVIDOR NO RESPONDE";
                respuestaUpdate.CodigoEstado = 99;
            }

            return respuestaUpdate;
        }
    }
}
