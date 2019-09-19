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
        public RepositoryService()
        {
            AddMediaFile(new MediaFile());
        }

        public Flujo GetFlujoByNro(string flujoNro)
        {
            Flujo flujo = new Flujo();

            IXWebService wbs = new XWebServiceKeywords();
            WbsFlujos webFlujos = new WbsFlujos(wbs);
            SolicitudOnBase solicitud = webFlujos.ConsultarFlujo(flujoNro, WbsGlobals.WbsFotosUpdateSistemaOrigen);
            flujo.EsValido = solicitud.EsValido;
            flujo.Mensaje = solicitud.Mensaje;
            flujo.Cliente = solicitud.Nombre;
            flujo.FlujoNro = solicitud.NroSolicitud;
            flujo.Placa = solicitud.Placa;
            flujo.CodigoEstado = solicitud.CodigoEstado;

            return flujo;
        }

        public void AddMediaFile(MediaFile mediaFile)
        {
            DeleteMediaFile();
            Globals.MediaFile = mediaFile;
        }

        public void DeleteMediaFile()
        {
            Globals.MediaFile = null;
            GC.Collect();
        }

        public MediaFile GetMediaFile()
        {
            return Globals.MediaFile;
        }

        public void UpdateFotos(Flujo flujo, string usuarioSistema)
        {
            IXWebService wbs = new XWebService();
            //IXWebService wbs = new XWebServiceSpecial();
            WbsFotosUpdate webFotosUpdate = new WbsFotosUpdate(wbs);
            string[] fotos = null;
            if(flujo.Fotos != null)
                fotos = (from f in flujo.Fotos select f.ImgString).ToArray();
            RespuestaUpdate respuestaUpdate = webFotosUpdate.UpdateFotos(flujo.FlujoNro, flujo.TipoDocumento, flujo.DocumentoNro,
                                                    usuarioSistema, "jpg", WbsGlobals.WbsFlujoSistemaOrigen, fotos);
            flujo.EsValido = respuestaUpdate.EsValido;
            flujo.Mensaje = respuestaUpdate.Mensaje;
            flujo.CodigoEstado = respuestaUpdate.CodigoEstado;
        }
    }
}
