using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XGaleryPhotos.Helpers;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;

using XGaleryPhotos.Views;

namespace XGaleryPhotos.ViewModels
{
    public class FlujoViewModel : INotifyPropertyChanged
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

        private User _usuario;
        public User Usuario
        {
            get { return _usuario; }
            set
            {
                _usuario = value;
                NotifyPropertyChanged();
            }
        }

        private IMultiMediaPickerService MultiMediaPickerService { get; set; }
        private IRepositoryService RepositoryService { get; set; }
        public string[] TiposDocumento = { "RE - Inspeccion", "RE - RC Atencion", "RE - Seguimiento" }; 

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

        public FlujoViewModel(User usuario)
        {
            Globals.FlujoViewModelInstance = this;
            Usuario = usuario;
            MultiMediaPickerService = Globals.MultiMediaPickerService;
            RepositoryService = Globals.RepositoryService;

            AddPhotoCommand = new Command((obj) =>
            {
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
                Flujo = RepositoryService.GetFlujoByNro(flujoNro);
            });

            EnviarOnBaseCommand = new Command((obj) =>
            {
                Globals.RepositoryService.UpdateFotos(Flujo, Usuario.UserName);
            });

            PhotoTappedCommand = new Command((obj) =>
            {
                var mediaSelected = obj as XGaleryPhotos.Models.MediaFile ;
                Globals.RepositoryService.AddMediaFile(mediaSelected);
                Globals.PhotoDisplayPage.ResetSource();
                Globals.NavegacionPage.PushAsync(Globals.PhotoDisplayPage);
            });

            SelectImagesCommand = new Command(async (obj) =>
            {
                var hasPermission = await CheckPermissionsAsync();
                if (hasPermission)
                {
                    if (Media == null)
                        Media = new ObservableCollection<XGaleryPhotos.Models.MediaFile>();
                    await MultiMediaPickerService.PickPhotosAsync();
                }
            });

            MultiMediaPickerService.OnMediaPicked += (s, a) =>
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

        public void SavePhotos()
        {
            Flujo.Fotos = new List<Foto>();
            int i = 0;
            foreach (var mediaFile in Media)
            {
                if(mediaFile.Path != null)
                {
                    if (mediaFile.OnBasePath == null)
                    {
                        string watermark = Globals.IncluirWatermarkEnFotosTomadas ? WatermarkHelper.ArmaWatermark() : null;
                        var helperImageService = DependencyService.Get<IHelperImageService>();
                        mediaFile.OnBasePath = helperImageService.StretchImage(mediaFile.Path, 1,
                                                                        Globals.PorcentajeCompresion,
                                                                        "-ONBASE", watermark);
                    }
                    var foto = new Foto
                    {
                        Flujo = this.Flujo,
                        FlujoId = Flujo.FlujoId,
                        FotoId = ++i,
                        Path = mediaFile.OnBasePath,
                        ImgString = Base64Helper.MediaPathToCode64(mediaFile.OnBasePath)
                    };

                    Flujo.Fotos.Add(foto);
                }
            }
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
            if (string.IsNullOrWhiteSpace(Flujo.TipoDocumento))
            {
                respuesta = "Tipo Documento en blanco no es válido!";
                return respuesta;
            }
            if (Array.IndexOf<string>(TiposDocumento, Flujo.TipoDocumento) < 0)
            {
                respuesta = "Tipo Documento no es válido!";
                return respuesta;
            }
            if (Array.IndexOf<string>(TiposDocumento, Flujo.TipoDocumento) == 1)
            {
                if (Flujo.DocumentoNro < 1 || Flujo.DocumentoNro > 20)
                {
                    respuesta = "Número de Documento RC no está en el rango de 1 a 20!";
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
