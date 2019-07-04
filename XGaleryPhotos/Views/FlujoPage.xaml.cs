﻿using System;
using System.ComponentModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace XGaleryPhotos
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class FlujoPage : ContentPage
    {
        public FlujoPage()
        {
            InitializeComponent();
            BindingContext = App.FlujoViewModel;
            pckTipoDocumental.ItemsSource = App.FlujoViewModel.TiposDocumental;
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
            Button button = sender as Button;
            if (button.Text.Contains("Nuevo"))
            {
                App.FlujoViewModel.Flujo = null;
                App.FlujoViewModel.Media = null;

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
                DisplayAlert("", "Introduzca el Nro. de Flujo!", "OK");
                return;
            }

            App.FlujoViewModel.BuscarFlujoCommand.Execute(txtNroFlujo.Text);
            if (App.FlujoViewModel.Flujo == null)
            {
                DisplayAlert("", "Nro. de Flujo no encontrado!", "OK");
                return;
            }
            else
            {
                btnBuscarFlujo.Text = "Nuevo Flujo";
                btnFotosGaleria.IsEnabled = true;
                btnTomarFoto.IsEnabled = true;
                btnEnviarOnBase.IsEnabled = true;
                txtNroFlujo.IsEnabled = false;
            }
        }

        void btnFotosGaleria_Clicked(object sender, System.EventArgs e)
        {
            App.FlujoViewModel.SelectImagesCommand.Execute(null);
        }

        async void btnTomarFoto_Clicked(object sender, EventArgs e)
        {
            var opciones_almacenamiento = new StoreCameraMediaOptions()
            {
                SaveToAlbum = true,
                Name = "MyPhoto"
            };

            var photo = await CrossMedia.Current.TakePhotoAsync(opciones_almacenamiento);
            App.FlujoViewModel.AddPhotoCommand.Execute(photo);
        }

        async void btnEnviarOnBase_Clicked(object sender, System.EventArgs e)
        {
            string respuesta = App.FlujoViewModel.ValidaDatosEnvio();
            if ( respuesta != "OK")
            {
                await DisplayAlert("VALIDACION", respuesta, "OK");
                return;
            }

            bool Ok = await DisplayAlert("CONFIRMACION", "Desea enviar estas fotos al Sistema OnBase?", "SI", "NO");
            if (Ok)
            {
                App.FlujoViewModel.SavePhotos();
                await DisplayAlert("", "Fotos enviadas exitosamente!", "OK");
                Resetear();
            }
        }

        private void Resetear()
        {
            App.FlujoViewModel.Flujo = null;
            App.FlujoViewModel.Media = null;

            btnBuscarFlujo.Text = "Nuevo Flujo";
            btnFotosGaleria.IsEnabled = false;
            btnTomarFoto.IsEnabled = false;
            btnEnviarOnBase.IsEnabled = false;
            txtNroFlujo.IsEnabled = true;
            txtNroFlujo.Text = string.Empty;
        }
    }
}