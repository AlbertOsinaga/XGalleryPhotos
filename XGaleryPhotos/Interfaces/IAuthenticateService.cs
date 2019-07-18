using XGaleryPhotos.Models;

namespace XGaleryPhotos.Interfaces
{
    public interface IAuthenticateService
    {
        User  Authenticate(string usuario, string password);
        bool IsUserAuthenticated();
        User AuthenticatedUser { get; }
    }
}
