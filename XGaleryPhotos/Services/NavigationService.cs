using XGaleryPhotos.Interfaces;
namespace XGaleryPhotos.Services
{
    public class NavigationService : INavigationService
    {
        public void Initialize()
        {
            if (App.AuthenticateService.IsUserAuthenticated())
            { }
        }
    }
}
