using XGaleryPhotos.Models;
namespace XGaleryPhotos.Interfaces
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Authenticate(string userName, string password);
        bool IsUserAuthenticated();
    }
}
