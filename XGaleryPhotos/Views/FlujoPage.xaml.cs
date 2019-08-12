using System;
using System.ComponentModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using XGaleryPhotos.ViewModels;

namespace XGaleryPhotos
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class FlujoPage : ContentPage
    {
        private FlujoViewModel FlujoViewModel { get; set; }

        public FlujoPage(FlujoViewModel flujoViewModel)
        {
            InitializeComponent();

            FlujoViewModel = flujoViewModel; 
            BindingContext = flujoViewModel;
            pckTipoDocumental.ItemsSource = FlujoViewModel.TiposDocumento;
            lblEtiqUsuario.Text = "Usuario:";

            // Resizing
            if (DeviceDisplay.MainDisplayInfo.Width <= 480)
            {
                btnBuscarFlujo.FontSize = App.FontSizeMicro;
                btnFotosGaleria.FontSize = App.FontSizeMicro;
                btnTomarFoto.FontSize = App.FontSizeMicro;
                btnEnviarOnBase.FontSize = App.FontSizeMicro;
                lblEtiqUsuario.Text = string.Empty;
            }
        }

        void pckTipoDocumental_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (pckTipoDocumental.SelectedIndex == 1)
            {
                lblNumero.IsVisible = true;
                txtNumero.IsVisible = true;
                lblNumeroLimites.IsVisible = true;
            }
            else
            {
                lblNumero.IsVisible = false;
                txtNumero.IsVisible = false;
                lblNumeroLimites.IsVisible = false;
            }
        }

        void btnBuscarFlujo_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button.Text.Contains("Nuevo"))
                {
                    FlujoViewModel.Flujo = null;
                    FlujoViewModel.Media = null;

                    button.Text = "Buscar Flujo";
                    btnFotosGaleria.IsEnabled = false;
                    btnTomarFoto.IsEnabled = false;
                    btnEnviarOnBase.IsEnabled = false;
                    pckTipoDocumental.SelectedIndex = -1;
                    txtNumero.Text = "1";
                    txtNroFlujo.IsEnabled = true;
                    txtNroFlujo.Focus();

                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNroFlujo.Text))
                {
                    DisplayAlert("PROCESAMIENTO DE FLUJOS", "Por favor introduzca No. de Flujo!", "OK");
                    return;
                }

                FlujoViewModel.BuscarFlujoCommand.Execute(txtNroFlujo.Text);
                
                if (FlujoViewModel.Flujo == null)
                {
                    DisplayAlert("PROCESAMIENTO DE FLUJOS", "Flujo nulo!", "OK");
                    return;
                }

                if(FlujoViewModel.Flujo.CodigoEstado == 1 && !FlujoViewModel.Flujo.EsValido)
                {
                    DisplayAlert("PROCESAMIENTO DE FLUJOS", "Nro. de Flujo no encontrado!", "OK");
                    return;
                }

                if (FlujoViewModel.Flujo.CodigoEstado >= 90)
                {
                    DisplayAlert("PROCESAMIENTO DE FLUJOS",
                        $"{FlujoViewModel.Flujo.Mensaje} ({FlujoViewModel.Flujo.CodigoEstado})", "OK");
                    return;
                }

                btnBuscarFlujo.Text = "Nuevo Flujo";
                btnFotosGaleria.IsEnabled = true;
                btnTomarFoto.IsEnabled = true;
                btnEnviarOnBase.IsEnabled = true;
                txtNroFlujo.IsEnabled = false;

            }
            catch (Exception ex)
            {
                DisplayAlert("EXCEPCION", $"Excepción en 'Buscar Flujo'!\nEx({ex.GetType()}-{ex.Message})", "OK");
            }
        }

        void btnFotosGaleria_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                FlujoViewModel.SelectImagesCommand.Execute(null);
            }
            catch (Exception ex)
            {
                DisplayAlert("EXCEPCION", $"Excepción en 'Fotos de Galería'!\nEx({ex.GetType()}-{ex.Message})", "OK");
            }
        }

        async void btnTomarFoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var opciones_almacenamiento = new StoreCameraMediaOptions()
                {
                    SaveToAlbum = true,
                    Name = "MyPhoto"
                };

                var photo = await CrossMedia.Current.TakePhotoAsync(opciones_almacenamiento);
                FlujoViewModel.AddPhotoCommand.Execute(photo);
            }
            catch (Exception ex)
            {
                await DisplayAlert("EXCEPCION", $"Excepción en 'Tomar Foto'!\nEx({ex.GetType()}-{ex.Message})", "OK");
            }
        }

        async void btnEnviarOnBase_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                string respuesta = FlujoViewModel.ValidaDatosEnvio();
                if (respuesta != "OK")
                {
                    await DisplayAlert("VALIDACION", respuesta, "OK");
                    return;
                }

                bool Ok = await DisplayAlert("CONFIRMACION", "Desea enviar estas fotos al Sistema OnBase?", "SI", "NO");
                if (Ok)
                {
                    FlujoViewModel.SavePhotos();
                    FlujoViewModel.EnviarOnBaseCommand.Execute(null);
                    if (FlujoViewModel.Flujo == null)
                    {
                        await DisplayAlert("ONBASE", "FLUJO NULO!", "OK");
                        return;
                    }

                    if (FlujoViewModel.Flujo.CodigoEstado >= 90)
                    {
                        await DisplayAlert("ONBASE",
                            $"{FlujoViewModel.Flujo.Mensaje} ({FlujoViewModel.Flujo.CodigoEstado})", "OK");
                        return;
                    }
                    if (FlujoViewModel.Flujo.CodigoEstado == 1)
                    {
                        if (FlujoViewModel.Flujo.EsValido)
                            await DisplayAlert("ONBASE", "Fotos enviadas exitosamente!", "OK");
                        else
                            await DisplayAlert("ONBASE", FlujoViewModel.Flujo.Mensaje, "OK");
                    }

                    Resetear();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("EXCEPCION", $"Excepción en 'Enviar Fotos'!\nEx({ex.GetType()}-{ex.Message})", "OK");
            }
        }

        private void Resetear()
        {
            FlujoViewModel.Flujo = null;
            FlujoViewModel.Media = null;

            btnBuscarFlujo.Text = "Nuevo Flujo";
            btnFotosGaleria.IsEnabled = false;
            btnTomarFoto.IsEnabled = false;
            btnEnviarOnBase.IsEnabled = false;
            txtNroFlujo.IsEnabled = false;
            txtNroFlujo.Text = string.Empty;
        }
    }
}