namespace XGaleryPhotos.Interfaces
{
    public interface IAuthenticateService
    {
        bool Authenticate(string usuario, string password);
        bool IsUserAuthenticated();
    }
}
