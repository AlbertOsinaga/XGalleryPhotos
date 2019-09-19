using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using XWebServices.Interfaces;

namespace XWebServices.Services
{
    public class XWebService : IXWebService
    {
        #region IXWebService

        public string RequestUri { get; set; }
        public string SoapAction { get; set; }
        public string WebMethod { get; set; }
        public string WebNamespace { get; set; }
        public string XmlnsXsi { get; set; }
        public string XmlnsXsd { get; set; }
        public string XmlnsSoap { get; set; }

        public virtual Dictionary<string, object> Invoke(params string[] args)
        {
            Dictionary<string, object> dicResult = new Dictionary<string, object>();

            using(WebResponse response = InvokeWebService(args))
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    var ServiceResult = rd.ReadToEnd();

                    XmlDocument soapEnvelope = new XmlDocument();
                    soapEnvelope.LoadXml(ServiceResult);
                    XmlNodeList resultados = soapEnvelope.GetElementsByTagName($"{WebMethod}Response");
                    if (resultados.Count == 0)
                        return dicResult;

                    XmlNode resultRoot = resultados[0].FirstChild;

                    XmlNode result = resultRoot?.FirstChild;
                    while(result != null)
                    {
                        dicResult.Add(result.Name, result.InnerText);
                        result = result.NextSibling;
                    }
                }
            }

            return dicResult;
        }

        #endregion

        public XWebService()
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
            if(RequestUri.Contains("https"))
                Req.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            //return HttpWebRequest    
            return Req;
        }
        protected virtual string CreateSoapEnvelope(params string[] args)
        {
            string usuario = WbsGlobals.WbsUsuario;
            string password = WbsGlobals.WbsPassword;

            string soapEnvelope =
                $@"<soap:Envelope xmlns:xsi = ""{XmlnsXsi}"" xmlns:xsd = ""{XmlnsXsd}"" xmlns:soap = ""{XmlnsSoap}"">" +
                $@"<soap:Header><UsuarioAuth xmlns=""{WebNamespace}"">" +
                $@"<Usuario>{usuario}</Usuario><Password>{password}</Password>" +
                $@"</UsuarioAuth></soap:Header>" +
                $@"<soap:Body><{WebMethod} xmlns = ""{WebNamespace}"">";

            foreach (var arg in args)
            {
                string[] partes = arg.Split(':');
                if (partes.Length == 1)
                {
                    if (arg.Length > 1)
                    {
                        if (arg[0] == '<')
                        {
                            soapEnvelope += $"{arg}";
                            continue;
                        }
                    }
                    if (!string.IsNullOrEmpty(arg))
                        soapEnvelope += $"<{arg}></{arg}>";
                }
                else if (partes.Length == 2)
                    soapEnvelope += $"<{partes[0]}>{partes[1]}</{partes[0]}>";
            }

            soapEnvelope += $@"</{WebMethod}></soap:Body></soap:Envelope>";

            return soapEnvelope;
        }

        protected WebResponse InvokeWebService(params string[] args)
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
