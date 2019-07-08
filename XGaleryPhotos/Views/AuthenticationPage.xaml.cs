using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;
using Xamarin.Forms;

namespace XGaleryPhotos.Views
{
    public partial class AuthenticationPage : ContentPage
    {
        public AuthenticationPage()
        {
            InitializeComponent();

            BindingContext = App.AuthenticationViewModel;
        }

        void btnLogin_Clicked(object sender, System.EventArgs e)
        {
            //  App.AuthenticationViewModel.LoginCommand.Execute(null);
            AuthenticationResponse authenticationResponse =
                App.AuthenticateService.Authenticate(txtUsuario.Text, txtClave.Text);
            if (!authenticationResponse.IsAuthenticated)
            {
                DisplayAlert("ERROR", "Usuario o Clave inválida!", "OK");
                return;
            }

            App.FlujoViewModel.Usuario = authenticationResponse.User.UserName;
            App.NavegacionPage.PopAsync();
            App.NavegacionPage.PushAsync(App.FlujoPage);
        }

        void btnCerrarApp_Clicked(object sender, System.EventArgs e)
        {
            var closer = DependencyService.Get<ICloseApplicatonService>();
            closer?.CloseApplication();
        }
    }
}
