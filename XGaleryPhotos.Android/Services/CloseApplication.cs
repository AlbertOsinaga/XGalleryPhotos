using Android.App;
using Java.Lang;
using Xamarin.Forms;
using XGaleryPhotos.Droid.Services;
using XGaleryPhotos.Interfaces;

[assembly: Dependency(typeof(CloseApplication))]
namespace XGaleryPhotos.Droid.Services
{
    public class CloseApplication : ICloseApplicatonService
    {
        void ICloseApplicatonService.CloseApplication()
        {
            var activity = (Activity) Forms.Context;
            activity.Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}
