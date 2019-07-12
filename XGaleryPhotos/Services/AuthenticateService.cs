using XGaleryPhotos.Interfaces;
using XWebServices;
using XWebServices.Services;

namespace XGaleryPhotos.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private bool UserAuthenticated { get; set; }
        private string Sistema { get; set; }
        private WbsSeguridad wbsSeguridad { get; set; }

        public AuthenticateService()
        {
            Sistema = "EXT101";
            wbsSeguridad = new WbsSeguridad(new XWebService());
        }

        public bool Authenticate(string usuario, string password)
        {
            UserAuthenticated = wbsSeguridad.ConsultarUsuarioSistema(usuario, Sistema, password);
            return UserAuthenticated;
        }

        public bool IsUserAuthenticated()
        {
            return UserAuthenticated;
        }
    }
}
