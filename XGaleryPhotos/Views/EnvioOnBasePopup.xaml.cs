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
            CancelacionEnvio();
        }

        private void btnOk_Clicked(object sender, EventArgs e)
        {
            lblProgreso.Text = "Enviando fotos a ONBASE...";
            lblWarning.Text = "Este proceso puede demorar... Espere por favor";
            btnNo.IsVisible = false;
            btnOk.IsVisible = false;
            actIndicator.IsVisible = true;
            actIndicator.IsRunning = true;
            EjecucionEnvio();
        }

        void CancelacionEnvio()
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() => Thread.Sleep(100)).ContinueWith(t => PopupNavigation.Instance.PopAsync(true), scheduler);
        }

        void EjecucionEnvio()
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() => EnvioOnBase()).ContinueWith(t => { PopupNavigation.Instance.PopAsync(true);
                VerificacionEnvio(); }, scheduler);
        }

        private void EnvioOnBase()
        {
            Globals.FlujoViewModelInstance.SavePhotos();
            Globals.FlujoViewModelInstance.EnviarOnBaseCommand.Execute(null);
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
