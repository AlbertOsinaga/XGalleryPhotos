using System;
using Xamarin.Forms;
using XGaleryPhotos.Models;
using XGaleryPhotos.ViewModels;

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
            Photo.Source = App.RepositoryService.GetMediaFile().Path;
        }

        public async void btnEliminarFoto_Clicked(object sender, EventArgs args)
        {
            var ok = await DisplayAlert("Confirmación!", "Quiere eliminar esta foto?", "Si", "No");
            if (ok)
            {
                App.PhotoDisplayViewModel.EliminarFotoCommand.Execute(null);   
            }
        }
    }
}
