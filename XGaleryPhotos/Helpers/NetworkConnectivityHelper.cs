using Xamarin.Forms;
using XGaleryPhotos.Interfaces;

namespace XGaleryPhotos.Helpers
{
    public static class NetworkConnectivityHelper
    {
        public static bool IsNetworkConnected
        {
            get
            {
                var networkConnection = DependencyService.Get<INetworkConnection>();
                if (networkConnection == null)
                    return true;
                networkConnection.CheckNetworkConnection();
                return networkConnection.IsConnected;
            }
        }
    }
}
