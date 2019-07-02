using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;
using XGaleryPhotos.Repositories;

using XGaleryPhotos.Views;

namespace XGaleryPhotos.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private Flujo _flujo;
        public Flujo Flujo
        {
            get { return _flujo; }
            set
            {
                _flujo = value;
                NotifyPropertyChanged();
            }
        }

        IMultiMediaPickerService _multiMediaPickerService;

        public IRepository Repository;
        public string[] TiposDocumental = { "RE - Inspección", "RE - RC Atención", "RE - Seguimiento" }; 

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<XGaleryPhotos.Models.MediaFile> Media { get; set; }

        public ICommand AddPhotoCommand { get; set; }
        public ICommand BuscarFlujoCommand { get; set; }
        public ICommand EnviarOnBaseCommand { get; set; }
        public ICommand PhotoTappedCommand { get; set; }
        public ICommand SelectImagesCommand { get; set; }

        public MainViewModel(IMultiMediaPickerService multiMediaPickerService)
        {
            //if(PropertyChanged == null) { }

            _multiMediaPickerService = multiMediaPickerService;
            Repository = new MockRepository();

            AddPhotoCommand = new Command((obj) =>
            {
                Type tipo = obj.GetType();
                Plugin.Media.Abstractions.MediaFile mediaFile = obj as Plugin.Media.Abstractions.MediaFile;
                if (mediaFile == null)
                    return;

                if (Media == null)
                    Media = new ObservableCollection<XGaleryPhotos.Models.MediaFile>();

                Media.Add(new MediaFile()
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = MediaFileType.Image,
                    Path = mediaFile.Path,
                    PreviewPath = mediaFile.Path
                }
                );
            });

            BuscarFlujoCommand = new Command((obj) =>
            {
                string flujoNro = obj as string;
                Flujo = Repository.GetFlujoByNro(flujoNro);
            });

            EnviarOnBaseCommand = new Command((obj) =>
            {
                throw new NotImplementedException();
            });

            PhotoTappedCommand = new Command((obj) =>
            {
                var mediaSelected = obj as XGaleryPhotos.Models.MediaFile ;
                (App.Current.MainPage as NavigationPage).PushAsync(new PhotoDisplayPage(mediaSelected));
            });

            SelectImagesCommand = new Command(async (obj) =>
            {
                var hasPermission = await CheckPermissionsAsync();
                if (hasPermission)
                {
                    if (Media == null)
                        Media = new ObservableCollection<XGaleryPhotos.Models.MediaFile>();
                    await _multiMediaPickerService.PickPhotosAsync();
                }
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

        public string ValidaDatosEnvio()
        {
            string respuesta = "OK";

            if (string.IsNullOrWhiteSpace(Flujo.FlujoNro))
            {
                respuesta = "Nro. de Flujo en blanco no es válido!";
                return respuesta;
            }
            if (string.IsNullOrWhiteSpace(Flujo.Cliente))
            {
                respuesta = "Cliente en blanco no es válido!";
                return respuesta;
            }
            if (string.IsNullOrWhiteSpace(Flujo.Placa))
            {
                respuesta = "Placa en blanco no es válida!";
                return respuesta;
            }
            if (string.IsNullOrWhiteSpace(Flujo.TipoDocumental))
            {
                respuesta = "Tipo Documental en blanco no es válido!";
                return respuesta;
            }
            if (Array.IndexOf<string>(TiposDocumental, Flujo.TipoDocumental) < 0)
            {
                respuesta = "Tipo Documental no es válido!";
                return respuesta;
            }
            if (Array.IndexOf<string>(TiposDocumental, Flujo.TipoDocumental) == 1)
            {
                if (Flujo.TipoDocumentalNumero < 1 || Flujo.TipoDocumentalNumero > 20)
                {
                    respuesta = "Número de Tipo Documental no está en rango 1 a 20!";
                    return respuesta;
                }
            }
            if(Media == null || Media.Count == 0)
            {
                respuesta = "No hay fotos para enviar!";
                return respuesta;
            }

            return respuesta;
        }
    }
}
