using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Views;

namespace XGaleryPhotos.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        IMultiMediaPickerService _multiMediaPickerService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<XGaleryPhotos.Models.MediaFile> Media { get; set; }
        public ICommand SelectImagesCommand { get; set; }
        public ICommand DisplayPhotoCommand { get; set; }
        public ICommand PhotoTappedCommand { get; set; }

        public MainViewModel(IMultiMediaPickerService multiMediaPickerService)
        {
            _multiMediaPickerService = multiMediaPickerService;
            SelectImagesCommand = new Command(async (obj) =>
            {
                var hasPermission = await CheckPermissionsAsync();
                if (hasPermission)
                {
                    Media = new ObservableCollection<XGaleryPhotos.Models.MediaFile>();
                    await _multiMediaPickerService.PickPhotosAsync();
                }
            });

            DisplayPhotoCommand = new Command(() =>
            {
                (App.Current.MainPage as NavigationPage).PushAsync(new PhotoDisplayPage());
            });

            //SelectVideosCommand = new Command(async (obj) =>
            //{
            //    var hasPermission = await CheckPermissionsAsync();
            //    if (hasPermission)
            //    {

            //        Media = new ObservableCollection<XGaleryPhotos.Models.MediaFile>();

            //        await _multiMediaPickerService.PickVideosAsync();

            //    }
            //});

            PhotoTappedCommand = new Command((obj) =>
            {
                var mediaSelected = obj as XGaleryPhotos.Models.MediaFile ;
                int i;
                for (i = 0; i < Media.Count; i++)
                {
                    if (mediaSelected.Id == Media[i].Id)
                        break;
                }
                Media.RemoveAt(i);
            });

            _multiMediaPickerService.OnMediaPicked += (s, a) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Media.Add(a);

                });

            };
        }

        async Task<bool> CheckPermissionsAsync()
        {
            var retVal = false;
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Storage))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Need Storage permission to access to your photos.", "Ok");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Plugin.Permissions.Abstractions.Permission.Storage });
                    status = results[Plugin.Permissions.Abstractions.Permission.Storage];
                }

                if (status == PermissionStatus.Granted)
                {
                    retVal = true;

                }
                else if (status != PermissionStatus.Unknown)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Permission Denied. Can not continue, try again.", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await App.Current.MainPage.DisplayAlert("Alert", "Error. Can not continue, try again.", "Ok");
            }

            return retVal;

        }
    }
}
