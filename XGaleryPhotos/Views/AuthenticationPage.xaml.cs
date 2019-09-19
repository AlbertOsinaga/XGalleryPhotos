using System.Net;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using XGaleryPhotos.Helpers;
using XGaleryPhotos.ViewModels;

namespace XGaleryPhotos.Views
{
    public partial class AuthenticationPage : ContentPage
    {
        public AuthenticationPage(AuthenticationViewModel authenticationViewModel)
        {
            InitializeComponent();

            BindingContext = authenticationViewModel;

            lblSize.IsVisible = Globals.DimensionesDeviceVisible;
            if (lblSize.IsVisible)
            {
                lblSize.Text = DeviceDisplay.MainDisplayInfo.Width.ToString() +
                                " X " + DeviceDisplay.MainDisplayInfo.Height.ToString();
            }
            lblAppTitulo.Text = Globals.AppTitulo;
            lblVersionName.Text = $"Versión: {Globals.AppVersionName}";
            txtUsuario.Text = string.Empty;
            txtClave.Text = string.Empty;
        }

        void btnLogin_Clicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                DisplayAlert("AUTENTICACION", "Por favor ingrese Usuario", "OK");
                return;
            }

            if (string.IsNullOrEmpty(txtClave.Text))
            {
                DisplayAlert("AUTENTICACION", "Por favor ingrese su Clave", "OK");
                return;
            }

            try
            {
                if(!NetworkConnectivityHelper.IsNetworkConnected)
                {
                    DisplayAlert("AUTENTICACION", "Conexión a Wifi o a red de datos no disponible...", "OK");
                    return;
                }

                Globals.AuthenticateService.Authenticate(txtUsuario.Text, txtClave.Text);
                User userAuth = Globals.AuthenticateService.AuthenticatedUser;

                if (!Globals.AuthenticateService.IsUserAuthenticated())
                {
                    if (userAuth == null)
                        DisplayAlert("ERROR EN AUTENTICACION", "(userAuth null)", "OK");
                    else if (int.TryParse(userAuth.CodigoEstado, out int codigo) && codigo >= 90)
                        DisplayAlert("AUTENTICACION NO FUE POSIBLE", $"{userAuth.Estado} ({userAuth.CodigoEstado})", "OK");
                    else
                        DisplayAlert("USUARIO NO AUTENTICADO", "Usuario o Clave inválida!", "OK");
                    return;
                }

                txtUsuario.Text = string.Empty;
                txtClave.Text = string.Empty;
                Globals.NavegacionPageInstance.PopAsync();
                Globals.NavegacionPageInstance.PushAsync(new FlujoPage(new FlujoViewModel(
                                                    Globals.AuthenticateService.AuthenticatedUser)));
            }
            catch (WebException wex)
            {
                if(wex == null) { }
                DisplayAlert("USUARIO NO PUDO SER AUTENTICADO", "No hay conexión con el servidor, por favor intente más tarde!", "OK");
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
