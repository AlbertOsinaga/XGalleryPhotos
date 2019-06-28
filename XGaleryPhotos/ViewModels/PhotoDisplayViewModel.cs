using System.Windows.Input;
using Xamarin.Forms;
using XGaleryPhotos.Models;

namespace XGaleryPhotos.ViewModels
{
    public class PhotoDisplayViewModel
    {
        public ICommand RegresarCommand { get; set; }
        public ICommand EliminarFotoCommand { get; set; }
        public MediaFile MediaFile { get; set; }

        public PhotoDisplayViewModel(MediaFile mediaFile)
        {
            MediaFile = mediaFile;

            RegresarCommand = new Command(() =>
            {
                (App.Current.MainPage as NavigationPage).PopAsync();
            });

            EliminarFotoCommand = new Command(() =>
            {
                var navigationPage = App.Current.MainPage as NavigationPage;
                var mainPage = navigationPage.RootPage as XGaleryPhotos.MainPage;

                var media = (mainPage.BindingContext as XGaleryPhotos.ViewModels.MainViewModel).Media;
                int i;
                for (i = 0; i < media.Count; i++)
                {
                    if (MediaFile.Id == media[i].Id)
                        break;
                }
                media.RemoveAt(i);

                navigationPage.PopAsync();
            });

        }
    }
}
