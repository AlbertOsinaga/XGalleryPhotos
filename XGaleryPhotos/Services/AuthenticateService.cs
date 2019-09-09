using XGaleryPhotos.Helpers;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;
using XWebServices;
using XWebServices.Models;
using XWebServices.Services;

namespace XGaleryPhotos.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private User UserAuthenticated { get; set; }
        private string Sistema { get; set; }
        private WbsSeguridad wbsSeguridad { get; set; }

        public AuthenticateService()
        {
            UserAuthenticated = new User();
            UserAuthenticated.CodigoEstado = "0";  // "0" entra a Autenticacion, "1" no entra
            Sistema = "EXT101";
            wbsSeguridad = new WbsSeguridad(new XWebService());
            //wbsSeguridad = new WbsSeguridad(new XWebServiceSpecial());
        }

        public User Authenticate(string userName, string password)
        {
            //UserAuthenticated = new User();
            UserAuthenticated.CodigoEstado = "0";
            UserAuthenticated.Estado = "CONSULTA USUARIO NO RESPONDE";

            //if(!ConnectivityHelper.IsConnectedToInternet)
            //{
            //    UserAuthenticated.CodigoEstado = "98";
            //    UserAuthenticated.Estado = "NO HAY CONEXION DE INTERNET";
            //    return UserAuthenticated;
            //}

            UsuarioSistema UsuarioAuthenticated = wbsSeguridad.ConsultarUsuarioSistema(userName, Sistema, password);
            if (UsuarioAuthenticated == null)
            {
                UserAuthenticated.Estado = "CONSULTA USUARIO DEVUELVE UsuarioSistema null";
                return UserAuthenticated;
            }

            UserAuthenticated.CodigoEstado = UsuarioAuthenticated.CodigoEstado;
            UserAuthenticated.Estado = UsuarioAuthenticated.Estado;
            UserAuthenticated.UserName = UsuarioAuthenticated.Usuario;
            UserAuthenticated.FirstName = UsuarioAuthenticated.Nombres;
            UserAuthenticated.LastName = UsuarioAuthenticated.Apellidos;
            UserAuthenticated.Email = UsuarioAuthenticated.Correo;

            return UserAuthenticated;
        }

        public bool IsUserAuthenticated()
        {
            return UserAuthenticated != null && UserAuthenticated.CodigoEstado == "1";
        }

        public User AuthenticatedUser => UserAuthenticated;
    }
}
