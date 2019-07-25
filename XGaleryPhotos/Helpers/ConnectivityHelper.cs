using Plugin.Connectivity;

namespace XGaleryPhotos.Helpers
{
    public static class ConnectivityHelper
    {
        public static bool IsConnectedToInternet
        {
            get
            {
                return CrossConnectivity.Current.IsConnected;
            }
        }
    }
}
