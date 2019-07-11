using System.Collections.Generic;
using System.Net;

namespace XWebServices.Interfaces
{
    public interface IXWebService
    {
        string RequestUri { get; set; }
        string SoapAction { get; set; }
        string WebMethod { get; set; }
        string WebNamespace { get; set; }
        string XmlnsXsi { get; set; }
        string XmlnsXsd { get; set; }
        string XmlnsSoap { get; set; }

        Dictionary<string, object> Invoke(params string[] args); 
    }
}
