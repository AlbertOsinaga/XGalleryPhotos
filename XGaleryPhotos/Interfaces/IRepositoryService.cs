using XGaleryPhotos.Models;
namespace XGaleryPhotos.Interfaces
{
    public interface IRepositoryService
    {
        Flujo GetFlujoByNro(string flujoNro);

        void AddUser(string username);
        void DeleteUser();
        User GetUser();

        void AddMediaFile(MediaFile mediaFile);
        void DeleteMediaFile();
        MediaFile GetMediaFile();
    }
}
