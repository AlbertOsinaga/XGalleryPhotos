namespace XWebServices
{
    public static class WbsGlobals
    {
        public const string WbsSeguridadRequestUri = @"http://desarrollo.lbc.bo/Servicios/generales/ConsultaUsuarioSistema.asmx";
        public const string WbsSeguridadSoapAction = "http://tempuri.org/ConsultarUsuarioSistema";
        public const string WbsSeguridadWebMethod = "ConsultarUsuarioSistema";
        public const string WbsSeguridadWebNamespace = "http://tempuri.org/";

        public const string WbsFlujosRequestUri = @"http://desanilus.lbc.bo/Nilus/WsOnbase/OnBaseWS.asmx";
        public const string WbsFlujosSoapAction = "http://tempuri.org/ObtenerInformacionSolicitudOnBase";
        public const string WbsFlujosWebMethod = "ObtenerInformacionSolicitudOnBase";
        public const string WbsFlujosWebNamespace = "http://tempuri.org/";

        public const string WbsFotosUpdateRequestUri = @"http://desanilus.lbc.bo/Nilus/WsOnbase/OnBaseWS.asmx";
        public const string WbsFotosUpdateSoapAction = "http://tempuri.org/ImportarDocumento";
        public const string WbsFotosUpdateWebMethod = "ImportarDocumento";
        public const string WbsFotosUpdateWebNamespace = "http://tempuri.org/";
    }
}
