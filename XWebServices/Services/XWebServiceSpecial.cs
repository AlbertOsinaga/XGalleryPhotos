namespace XWebServices.Services
{
    public class XWebServiceSpecial : XWebService
    {
        protected override string CreateSoapEnvelope(params string[] args)
        {
            string soapEnvelope =
                $@"<soap:Envelope xmlns:xsi = ""{XmlnsXsi}"" xmlns:xsd = ""{XmlnsXsd}"" xmlns:soap = ""{XmlnsSoap}""><soap:Body><{WebMethod} xmlns = ""{WebNamespace}"">";

            foreach (var arg in args)
            {
                string[] partes = arg.Split(':');
                if (partes.Length == 1)
                {
                    if(arg.Length > 1)
                    {
                        if(arg[0] == '<')
                        {
                            soapEnvelope += $"{arg}";
                            continue;
                        }
                    }
                    if(!string.IsNullOrEmpty(arg))
                        soapEnvelope += $"<{arg}></{arg}>";
                }
                else if (partes.Length == 2)
                    soapEnvelope += $"<{partes[0]}>{partes[1]}</{partes[0]}>";
            }

            soapEnvelope += $@"</{WebMethod}></soap:Body></soap:Envelope>";

            return soapEnvelope;
        }
    }
}
