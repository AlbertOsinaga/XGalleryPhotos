using DLToolkit.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Services;
using XGaleryPhotos.Views;
using XGaleryPhotos.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XGaleryPhotos
{
    public partial class App : Application
    {
        #region Static Members

        // Services
        public static IAuthenticateService AuthenticateService { get; set; }
        public static IMultiMediaPickerService MultiMediaPickerService { get; set; }
        public static INavigationService NavigationService { get; set; }
        public static IRepositoryService RepositoryService { get; set; }

        // Views
        public static AuthenticationPage AuthenticationPage { get; set; }
        public static FlujoPage FlujoPage { get; set; }                 // MainPage
        public static NavigationPage NavegacionPage { get; set; }       // Navigation
        public static PhotoDisplayPage PhotoDisplayPage { get; set; }

        // ViewModels
        public static AuthenticationViewModel AuthenticationViewModel { get; set; }
        public static FlujoViewModel FlujoViewModel { get; set; }
        public static PhotoDisplayViewModel PhotoDisplayViewModel { get; set; }

        static App()
        {
            // Services
            AuthenticateService = new AuthenticateService();
            NavigationService = new NavigationService();
            RepositoryService = new MockRepositoryService();

        }

        #endregion

        public App()
        {
            InitializeComponent();

            // ViewModels
            AuthenticationViewModel = new AuthenticationViewModel();
            FlujoViewModel = new FlujoViewModel();
            PhotoDisplayViewModel = new PhotoDisplayViewModel();

            // Views
            AuthenticationPage = new AuthenticationPage();
            FlujoPage = new FlujoPage();
            NavegacionPage = new NavigationPage();
            PhotoDisplayPage = new PhotoDisplayPage();

            FlowListView.Init();
            if(AuthenticateService.IsUserAuthenticated())
                App.NavegacionPage.PushAsync(FlujoPage);
            else
                App.NavegacionPage.PushAsync(AuthenticationPage);
            this.MainPage = NavegacionPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
