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
            App.AuthenticateService.Authenticate(txtUsuario.Text, txtClave.Text);
            User userAuth = App.AuthenticateService.AuthenticatedUser;

            if (!App.AuthenticateService.IsUserAuthenticated())
            {
                if(userAuth == null)
                    DisplayAlert("ERROR EN AUTENTICACION", "(userAuth null)", "OK");
                else if (userAuth.CodigoEstado == "99")
                    DisplayAlert("ERROR EN AUTENTICACION", userAuth.Estado, "OK");
                else
                    DisplayAlert("USUARIO NO AUTENTICADO", "Usuario o Clave inválida!", "OK");
                return;
            }

            App.FlujoViewModel.Usuario = userAuth;
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
