using System;
using System.Collections.Generic;
using System.Linq;
using XGaleryPhotos.Interfaces;
using XGaleryPhotos.Models;
using XWebServices;
using XWebServices.Interfaces;
using XWebServices.Models;
using XWebServices.Services;

namespace XGaleryPhotos.Services
{
    public class RepositoryService : IRepositoryService
    {
        public static User User { get; set; }
        public static MediaFile MediaFile { get; set; }
        public static List<User> Users { get; set; }

        public RepositoryService()
        {
            Users = new List<User>();
            Users.Add(new User { Id = 1, UserName = "juan", Password = "1234" });
            Users.Add(new User { Id = 2, UserName = "maria", Password = "4321" });
            Users.Add(new User { Id = 3, UserName = "felipe", Password = "5678" });

            AddMediaFile(new MediaFile());
        }

        public Flujo GetFlujoByNro(string flujoNro)
        {
            Flujo flujo = new Flujo();

            IXWebService wbs = new XWebServiceKeywords();
            WbsFlujos webFlujos = new WbsFlujos(wbs);
            SolicitudOnBase solicitud = webFlujos.ConsultarFlujo(flujoNro, "ICRL");
            flujo.EsValido = solicitud.EsValido;
            flujo.Mensaje = solicitud.Mensaje;
            flujo.Cliente = solicitud.Nombre;
            flujo.FlujoNro = solicitud.NroSolicitud;
            flujo.Placa = solicitud.Placa;
            flujo.CodigoEstado = solicitud.CodigoEstado;

            return flujo;
        }

        public void AddUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return;
            Users.Add(new User { Id = Users.Count + 1, UserName = "felipe", Password = "5678" });
        }

        public void DeleteUser(int userId)
        {
            User userDel = Users.Find((u) => u.Id == userId);
            if (userDel != null)
                Users.Remove(userDel);
        }

        public User GetUser(string username, string password = null)
        {
            User userLog = null;
            if (password == null)
                userLog = Users.Find((u) => u.UserName == username);
            else
                userLog = Users.Find((u) => u.UserName == username && u.Password == password);
            return userLog;
        }

        public void AddMediaFile(MediaFile mediaFile)
        {
            DeleteMediaFile();
            MockRepositoryService.MediaFile = mediaFile;
        }

        public void DeleteMediaFile()
        {
            MockRepositoryService.MediaFile = null;
            GC.Collect();
        }

        public MediaFile GetMediaFile()
        {
            return MockRepositoryService.MediaFile;
        }

        public void UpdateFotos(Flujo flujo, string usuarioSistema)
        {
            IXWebService wbs = new XWebServiceSpecial();
            WbsFotosUpdate webFotosUpdate = new WbsFotosUpdate(wbs);
            string[] fotos = null;
            if(flujo.Fotos != null)
                fotos = (from f in flujo.Fotos select f.ImgString).ToArray();
            RespuestaUpdate respuestaUpdate = webFotosUpdate.UpdateFotos(flujo.FlujoNro, flujo.TipoDocumento, flujo.DocumentoNro,
                                                    usuarioSistema, "jpg", "ICRL", fotos);
            flujo.EsValido = respuestaUpdate.EsValido;
            flujo.Mensaje = respuestaUpdate.Mensaje;
            flujo.CodigoEstado = respuestaUpdate.CodigoEstado;
        }
    }
}
