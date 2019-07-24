using System;
using Xamarin.Forms;
using XGaleryPhotos.Helpers;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;

namespace XGaleryPhotos.Views
{
    public partial class PhotoDisplayPage : ContentPage
    {
        public PhotoDisplayPage()
        {
            InitializeComponent();

            BindingContext = App.PhotoDisplayViewModel;
            ResetSource();
        }

        public void ResetSource()
        {
            MediaFile mediaFile = App.RepositoryService.GetMediaFile();
            if (mediaFile != null && mediaFile.Path != null && mediaFile.OnBasePath == null)
            {
                string watermark = App.IncluirWatermarkEnFotosTomadas ? WatermarkHelper.ArmaWatermark() : null;
                IHelperImageService helperImageService = DependencyService.Get<IHelperImageService>();
                mediaFile.Path = helperImageService.StretchImage(mediaFile.Path, 1, 90, string.Empty, watermark);
            }


            Photo.Source = mediaFile.Path;
        }

        public async void btnEliminarFoto_Clicked(object sender, EventArgs args)
        {
            try
            {
                var ok = await DisplayAlert("Confirmación!", "Quiere eliminar esta foto?", "Si", "No");
                if (ok)
                {
                    App.PhotoDisplayViewModel.EliminarFotoCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("EXCEPCION", $"Excepción en 'Eliminar Foto'!\nEx({ex.GetType()}-{ex.Message})", "OK");
            }
        }
    }
}
