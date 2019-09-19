namespace XWebServices
{
    public static class WbsGlobals
    {
        public const string WbsSeguridadRequestUri = @"https://servicios.lbc.bo/WSBSELBC/generales/ConsultaUsuarioSistema.asmx";
        public const string WbsSeguridadSoapAction = "http://tempuri.org/ConsultarUsuarioSistema";
        public const string WbsSeguridadWebMethod = "ConsultarUsuarioSistema";
        public const string WbsSeguridadWebNamespace = "http://tempuri.org/";
        public const string WbsSeguridadSistema = "EXT101";

        public const string WbsFlujosRequestUri = @"http://nilus.lbc.bo/WSOnBase/OnBaseWSAuth.asmx";
        public const string WbsFlujosSoapAction = "http://tempuri.org/ObtenerInformacionSolicitudOnBase";
        public const string WbsFlujosWebMethod = "ObtenerInformacionSolicitudOnBase";
        public const string WbsFlujosWebNamespace = "http://tempuri.org/";
        public const string WbsFlujoSistemaOrigen = "ICRL";

        public const string WbsFotosUpdateRequestUri = @"http://nilus.lbc.bo/WSOnBase/OnBaseWSAuth.asmx";
        public const string WbsFotosUpdateSoapAction = "http://tempuri.org/ImportarDocumento";
        public const string WbsFotosUpdateWebMethod = "ImportarDocumento";
        public const string WbsFotosUpdateWebNamespace = "http://tempuri.org/";
        public const string WbsFotosUpdateSistemaOrigen = "ICRL";

        public const string WbsUsuario = "icrlUser";
        public const string WbsPassword = "icrl19Prod";
    }
}
