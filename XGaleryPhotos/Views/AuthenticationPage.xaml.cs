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
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                DisplayAlert("AUTENTICACION", "Por favor ingrese su usuario", "OK");
                return;
            }

            if (string.IsNullOrEmpty(txtClave.Text))
            {
                DisplayAlert("AUTENTICACION", "Por favor ingrese su clave", "OK");
                return;
            }

            try
            {
                //  App.AuthenticationViewModel.LoginCommand.Execute(null);
                App.AuthenticateService.Authenticate(txtUsuario.Text, txtClave.Text);
                User userAuth = App.AuthenticateService.AuthenticatedUser;

                if (!App.AuthenticateService.IsUserAuthenticated())
                {
                    if (userAuth == null)
                        DisplayAlert("ERROR EN AUTENTICACION", "(userAuth null)", "OK");
                    else if (int.TryParse(userAuth.CodigoEstado, out int codigo) && codigo >= 90)
                        DisplayAlert("AUTENTICACION NO FUE POSIBLE", $"{userAuth.Estado} ({userAuth.CodigoEstado})", "OK");
                    else
                        DisplayAlert("USUARIO NO AUTENTICADO", "Usuario o Clave inválida!", "OK");
                    return;
                }

                App.FlujoViewModel.Usuario = userAuth;
                App.NavegacionPage.PopAsync();
                App.NavegacionPage.PushAsync(App.FlujoPage);
            }
            catch (System.Exception ex)
            {
                DisplayAlert("USUARIO NO AUTENTICADO", $"Usuario no pudo ser autenticado!\nEx({ex.GetType()}-{ex.Message})", "OK");
            }
        }

        void btnCerrarApp_Clicked(object sender, System.EventArgs e)
        {
            var closer = DependencyService.Get<ICloseApplicatonService>();
            closer?.CloseApplication();
        }
    }
}
