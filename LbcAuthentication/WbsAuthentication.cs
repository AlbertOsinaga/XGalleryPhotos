using System;
using System.IO;
using System.Net;
using System.Xml;

namespace LbcAuthentication
{
    public class WbsAuthentication
    {
        public HttpWebRequest CreateSOAPWebRequest()
        {
            //Making Web Request    
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://desarrollo.lbc.bo/Servicios/generales/ConsultaUsuarioSistema.asmx");
            //SOAPAction    
            Req.Headers.Add(@"SOAPAction:http://tempuri.org/ConsultarUsuarioSistema");
            //Content_type    
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method    
            Req.Method = "POST";
            //return HttpWebRequest    
            return Req;
        }

        public void InvokeService(string usuario, string sistema, string password)
        {
            //Calling CreateSOAPWebRequest method    
            HttpWebRequest request = CreateSOAPWebRequest();

            XmlDocument SOAPReqBody = new XmlDocument();
            //SOAP Body Request
            string strRequest = $@"<soap:Envelope xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance""
                                                xmlns:xsd = ""http://www.w3.org/2001/XMLSchema""
                                                xmlns:soap = ""http://schemas.xmlsoap.org/soap/envelope/"">
                                    <soap:Body>
                                        <ConsultarUsuarioSistema xmlns = ""http://tempuri.org/"">
                                            <usuario>{usuario}</usuario>
                                            <sistema>{sistema}</sistema>
                                            <password>{password}</password>
                                        </ConsultarUsuarioSistema>
                                    </soap:Body>
                                </soap:Envelope>";

            SOAPReqBody.LoadXml(strRequest);

            using (Stream stream = request.GetRequestStream())
            {
                SOAPReqBody.Save(stream);
            }

            //Geting response from request    
            using (WebResponse response = request.GetResponse())
            {
                WebHeaderCollection headers = response.Headers;
                foreach(object header in headers)
                {
                    string key = header as string;
                    string head = headers[key];
                }

                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    //reading stream    
                    var ServiceResult = rd.ReadToEnd();
                    //writting stream result on console    
                    Console.WriteLine(ServiceResult);
                    Console.ReadLine();
                }
            }
        }
    }
}
