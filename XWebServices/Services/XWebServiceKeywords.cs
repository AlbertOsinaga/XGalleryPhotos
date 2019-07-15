using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using XWebServices.Interfaces;

namespace XWebServices.Services
{
    public class XWebServiceKeywords : IXWebService
    {
        #region IXWebService

        public string RequestUri { get; set; }
        public string SoapAction { get; set; }
        public string WebMethod { get; set; }
        public string WebNamespace { get; set; }
        public string XmlnsXsi { get; set; }
        public string XmlnsXsd { get; set; }
        public string XmlnsSoap { get; set; }

        public Dictionary<string, object> Invoke(params string[] args)
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

        public XWebServiceKeywords()
        {
            XmlnsXsi = "http://www.w3.org/2001/XMLSchema-instance";
            XmlnsXsd = "http://www.w3.org/2001/XMLSchema";
            XmlnsSoap = "http://schemas.xmlsoap.org/soap/envelope/";
        }

        private HttpWebRequest CreateSOAPWebRequest()
        {
            //Making Web Request    
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(RequestUri);
            //SOAPAction    
            Req.Headers.Add($@"SOAPAction:{SoapAction}");
            //Content_type    
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method    
            Req.Method = "POST";
            //return HttpWebRequest    
            return Req;
        }
        private string CreateSoapEnvelope(params string[] args)
        {
            string soapEnvelope =
                $@"<soap:Envelope xmlns:xsi = ""{XmlnsXsi}"" xmlns:xsd = ""{XmlnsXsd}"" xmlns:soap = ""{XmlnsSoap}""><soap:Body><{WebMethod} xmlns = ""{WebNamespace}"">";

            foreach (var arg in args)
            {
                string[] partes = arg.Split('=', ':');
                if (partes.Length == 1)
                    soapEnvelope += $"<{arg}></{arg}>";
                else if (partes.Length == 2)
                    soapEnvelope += $"<{partes[0]}>{partes[1]}</{partes[0]}>";
            }

            soapEnvelope += $@"</{WebMethod}></soap:Body></soap:Envelope>";

            return soapEnvelope;
        }
        private WebResponse InvokeWebService(params string[] args)
        {
            HttpWebRequest request = CreateSOAPWebRequest();
            XmlDocument soapReq = new XmlDocument();
            string strSoapReq = CreateSoapEnvelope(args);
            soapReq.LoadXml(strSoapReq);

            using (Stream stream = request.GetRequestStream())
            {
                soapReq.Save(stream);
            }

            return request.GetResponse();
        }
    }
}
