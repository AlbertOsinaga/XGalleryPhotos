using System;
using Xamarin.Forms;
using XGaleryPhotos.Models;
using XGaleryPhotos.ViewModels;

namespace XGaleryPhotos.Views
{
    public partial class PhotoDisplayPage : ContentPage
    {
        private PhotoDisplayViewModel _viewModel;

        public PhotoDisplayPage(MediaFile mediaFile)
        {
            InitializeComponent();

            _viewModel = new PhotoDisplayViewModel(mediaFile);
            BindingContext = _viewModel; 
            Photo.Source = mediaFile.Path;
        }

        public async void btnEliminarFoto_Clicked(object sender, EventArgs args)
        {
            var ok = await DisplayAlert("Confirmación!", "Quiere eliminar esta foto?", "Si", "No");
            if (ok)
            {
                _viewModel.EliminarFotoCommand.Execute(null);   
            }
        }
    }
}
