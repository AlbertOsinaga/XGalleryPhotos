using System;
using Xamarin.Forms;
using XGaleryPhotos.Helpers;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;
using XGaleryPhotos.ViewModels;

namespace XGaleryPhotos.Views
{
    public partial class PhotoDisplayPage : ContentPage
    {
        public PhotoDisplayPage(PhotoDisplayViewModel photoDisplayViewModel)
        {
            InitializeComponent();

            BindingContext = photoDisplayViewModel;
            ResetSource();
        }

        public void ResetSource()
        {
            MediaFile mediaFile = Globals.RepositoryService.GetMediaFile();
            if (mediaFile != null && mediaFile.Path != null && mediaFile.OnBasePath == null)
            {
                string watermark = Globals.IncluirWatermarkEnFotosTomadas ? WatermarkHelper.ArmaWatermark() : null;
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
                    PhotoDisplayViewModel photoDisplayViewModel = BindingContext as PhotoDisplayViewModel;
                    photoDisplayViewModel?.EliminarFotoCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("EXCEPCION", $"Excepción en 'Eliminar Foto'!\nEx({ex.GetType()}-{ex.Message})", "OK");
            }
        }
    }
}
