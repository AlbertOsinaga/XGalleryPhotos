using System.Collections.Generic;
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

            WebService.RequestUri = @"http://desanilus.lbc.bo/Nilus/WsOnbase/OnBaseWS.asmx";
            WebService.SoapAction = "http://tempuri.org/ImportarDocumento";
            WebService.WebMethod = "ImportarDocumento";
            WebService.WebNamespace = "http://tempuri.org/";

            List<string> parametros = new List<string>()
            {
                "<documento>",
                                        $"nroSolicitud:{flujoNro}",
                                        $"tipoDocumento:{tipoDocumento}",
                                        $"extensionArchivo:{extensionArchivo}",
                                        "<keywords>",
                                            "<KeywordOnBaseEntity>",
                                                //$"idKeywords:{string.Empty}",
                                                $"nombre:LBC UserName WS",
                                                $"valor:{usuarioSistema}",
                                            "</KeywordOnBaseEntity>",
                                            "<KeywordOnBaseEntity>",
                                                //$"idKeywords:{string.Empty}",
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
            return respuestaUpdate;
        }
    }
}
