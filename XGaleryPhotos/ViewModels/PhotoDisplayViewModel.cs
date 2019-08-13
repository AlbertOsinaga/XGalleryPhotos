using System;
using System.Windows.Input;
using Xamarin.Forms;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;

namespace XGaleryPhotos.ViewModels
{
    public class PhotoDisplayViewModel
    {
        public ICommand RegresarCommand { get; set; }
        public ICommand EliminarFotoCommand { get; set; }
        public MediaFile MediaFile { get ; set; }

        public PhotoDisplayViewModel()
        {
            RegresarCommand = new Command(() =>
            {
                App.NavegacionPage.PopAsync();
            });

            EliminarFotoCommand = new Command(() =>
            {
                MediaFile = App.RepositoryService.GetMediaFile();

                var media = Globals.FlujoViewModel.Media;
                int i;
                for (i = 0; i < media.Count; i++)
                {
                    if (MediaFile.Id == media[i].Id)
                        break;
                }
                media.RemoveAt(i);

                App.NavegacionPage.PopAsync();
            });

        }
    }
}
