using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using XWebServices.Interfaces;

namespace XWebServices.Services
{
    public class XWebServiceKeywords : XWebService
    {
        #region IXWebService

        public override Dictionary<string, object> Invoke(params string[] args)
        {
            Dictionary<string, object> dicResult = new Dictionary<string, object>();

            using (WebResponse response = InvokeWebService(args))
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    var ServiceResult = rd.ReadToEnd();

                    XmlDocument soapEnvelope = new XmlDocument();
                    soapEnvelope.LoadXml(ServiceResult);
                    XmlNodeList esValido = soapEnvelope.GetElementsByTagName("EsValido");
                    if (esValido != null && esValido.Count > 0)
                        dicResult.Add(esValido[0].Name, esValido[0].InnerText);
                    XmlNodeList nroSolicitud = soapEnvelope.GetElementsByTagName("nroSolicitud");
                    if (nroSolicitud != null && nroSolicitud.Count > 0)
                        dicResult.Add(nroSolicitud[0].Name, nroSolicitud[0].InnerText);
                    XmlNodeList mensaje = soapEnvelope.GetElementsByTagName("Mensaje");
                    if (mensaje != null && mensaje.Count > 0)
                        dicResult.Add(mensaje[0].Name, mensaje[0].InnerText);
                    XmlNodeList idKeywords = soapEnvelope.GetElementsByTagName("idKeyword");
                    foreach (XmlNode idkeyword in idKeywords)
                    {
                        if(idkeyword.InnerText == "103")
                        {
                            XmlNode nombre = idkeyword.NextSibling;
                            if (nombre != null && nombre.Name == "nombre" && nombre.InnerText == "Nombres/Razon Social")
                            {
                                XmlNode valor = nombre.NextSibling;
                                if (valor != null && valor.Name == "valor")
                                    dicResult.Add("Nombre", valor.InnerText);
                            }
                        }
                        if (idkeyword.InnerText == "263")
                        {
                            XmlNode nombre = idkeyword.NextSibling;
                            if (nombre != null && nombre.Name == "nombre" && nombre.InnerText == "No. de Placa")
                            {
                                XmlNode valor = nombre.NextSibling;
                                if (valor != null && valor.Name == "valor")
                                    dicResult.Add("Placa", valor.InnerText);
                            }
                        }
                    }
                }
            }

            return dicResult;
        }

        #endregion
    }
}
