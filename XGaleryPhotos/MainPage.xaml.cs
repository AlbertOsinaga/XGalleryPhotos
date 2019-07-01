using System;
using System.ComponentModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.ViewModels;

namespace XGaleryPhotos
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainViewModel _mainViewModel;

        public MainPage(IMultiMediaPickerService multiMediaPickerService)
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel(multiMediaPickerService);
            BindingContext = _mainViewModel;
            pckTipoDocumental.ItemsSource = _mainViewModel.TiposDocumental;
            pckTipoDocumental.SelectedIndex = 0;
        }

        private async void btnTomarFoto_Clicked(object sender, EventArgs e)
        {
            var opciones_almacenamiento = new StoreCameraMediaOptions()
            {
                SaveToAlbum = true,
                Name = "MyPhoto"
            };

            var photo = await CrossMedia.Current.TakePhotoAsync(opciones_almacenamiento);
            _mainViewModel.AddPhotoCommand.Execute(photo);
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
    }
}
