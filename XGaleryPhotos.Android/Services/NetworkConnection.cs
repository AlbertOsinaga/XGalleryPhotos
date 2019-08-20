using Android.App;
using Android.Content;
using Android.Net;
using XGaleryPhotos.Droid.Services;
using XGaleryPhotos.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkConnection))]
namespace XGaleryPhotos.Droid.Services
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }

        public void CheckNetworkConnection()
        {
            var connectivityManager =
                (ConnectivityManager) Application.Context.GetSystemService(Context.ConnectivityService);

            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            IsConnected = (activeNetworkInfo != null && activeNetworkInfo.IsConnected) ? true : false; 
        }
    }
}
