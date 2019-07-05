using XGaleryPhotos.Models;
namespace XGaleryPhotos.Interfaces
{
    public interface IRepositoryService
    {
        Flujo GetFlujoByNro(string flujoNro);

        void AddUser(string username, string password);
        void DeleteUser(int userId);
        User GetUser(string username, string password);

        void AddMediaFile(MediaFile mediaFile);
        void DeleteMediaFile();
        MediaFile GetMediaFile();
    }
}
