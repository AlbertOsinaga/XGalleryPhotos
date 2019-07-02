using XGaleryPhotos.Models;
namespace XGaleryPhotos.Interfaces
{
    public interface IRepository
    {
        Flujo GetFlujoByNro(string flujoNro);
    }
}
