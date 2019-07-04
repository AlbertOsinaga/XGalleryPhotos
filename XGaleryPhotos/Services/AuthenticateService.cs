using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;

namespace XGaleryPhotos.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private User User { get; set; }

        public AuthenticationResponse Authenticate(string userName, string password)
        {
            this.User = App.RepositoryService.GetUser();
            AuthenticationResponse response = new AuthenticationResponse
            {
                User = this.User
            };
            response.IsAuthenticated = this.User != null;
            return response;
        }

        public bool IsUserAuthenticated()
        {
            return this.User != null;
        }
    }
}
