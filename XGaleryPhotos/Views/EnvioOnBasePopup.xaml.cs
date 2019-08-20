using System;
using System.Threading;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using XGaleryPhotos;
using XGaleryPhotos.Helpers;
using XGaleryPhotos.ViewModels;

namespace XGaleryPhotos.Views
{
    public partial class EnvioOnBasePopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public EnvioOnBasePopup()
        {
            InitializeComponent();
        }

        private void btnNo_Clicked(object sender, EventArgs e)
        {
            MakeShortWebServiceCall();
        }

        private void btnOk_Clicked(object sender, EventArgs e)
        {
            lblProgreso.Text = "Enviando fotos a ONBASE...";
            lblWarning.Text = "Este proceso puede demorar... Espere por favor";
            actIndicator.IsVisible = true;
            actIndicator.IsRunning = true;
            MakeLongWebServiceCall();
        }

        void MakeShortWebServiceCall()
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() => Thread.Sleep(100)).ContinueWith(t => PopupNavigation.Instance.PopAsync(true), scheduler);
        }

        void MakeLongWebServiceCall()
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() => EnvioOnBase()).ContinueWith(t => { PopupNavigation.Instance.PopAsync(true);
                VerificacionEnvio(); }, scheduler);
        }

        private void EnvioOnBase()
        {
            // Conexión a la red de datos
            //if (!NetworkConnectivityHelper.IsNetworkConnected)
            //{
            //    await DisplayAlert("ONBASE", "Conexión a Wifi o a red de datos no disponible...", "OK");
            //    return;
            //}

            Globals.FlujoViewModelInstance.SavePhotos();
            Globals.FlujoViewModelInstance.EnviarOnBaseCommand.Execute(null);

            //if (Globals.FlujoViewModelInstance.Flujo == null)
            //{
            //    DisplayAlert("ONBASE", "FLUJO NULO!", "OK");
            //    return;
            //}

            //if (Globals.FlujoViewModelInstance.Flujo.CodigoEstado >= 90)
            //{
            //    DisplayAlert("ONBASE",
            //        $"{Globals.FlujoViewModelInstance.Flujo.Mensaje} ({Globals.FlujoViewModelInstance.Flujo.CodigoEstado})", "OK");
            //    return;
            //}

            //if (Globals.FlujoViewModelInstance.Flujo.CodigoEstado == 1)
            //{
            //    if (Globals.FlujoViewModelInstance.Flujo.EsValido)
            //    {
            //        DisplayAlert("ONBASE", "Fotos enviadas exitosamente!", "OK");
            //        // Resetear();
            //    }
            //    else
            //        DisplayAlert("ONBASE", Globals.FlujoViewModelInstance.Flujo.Mensaje, "OK");
            //}
        }

        private void VerificacionEnvio()
        {
            if (Globals.FlujoViewModelInstance.Flujo == null)
            {
                DisplayAlert("ONBASE", "FLUJO NULO!", "OK");
                return;
            }

            if (Globals.FlujoViewModelInstance.Flujo.CodigoEstado >= 90)
            {
                DisplayAlert("ONBASE",
                    $"{Globals.FlujoViewModelInstance.Flujo.Mensaje} ({Globals.FlujoViewModelInstance.Flujo.CodigoEstado})", "OK");
                return;
            }

            if (Globals.FlujoViewModelInstance.Flujo.CodigoEstado == 1)
            {
                if (Globals.FlujoViewModelInstance.Flujo.EsValido)
                {
                    DisplayAlert("ONBASE", "Fotos enviadas exitosamente!", "OK");
                    Globals.FlujoPageInstance.Resetear();
                }
                else
                    DisplayAlert("ONBASE", Globals.FlujoViewModelInstance.Flujo.Mensaje, "OK");
            }
        }
    }
}
