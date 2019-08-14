using XGaleryPhotos.Models;

namespace XGaleryPhotos.Interfaces
{
    public interface IRepositoryService
    {
        Flujo GetFlujoByNro(string flujoNro);

        void AddMediaFile(MediaFile mediaFile);
        void DeleteMediaFile();
        MediaFile GetMediaFile();

        void UpdateFotos(Flujo flujo, string usuarioSistema);
    }
}
