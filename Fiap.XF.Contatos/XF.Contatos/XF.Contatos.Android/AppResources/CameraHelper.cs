using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Media;
using XF.Contatos.Droid.AppResources;
using XF.Contatos.Global;

[assembly: Dependency(typeof(CameraHelper))]
namespace XF.Contatos.Droid.AppResources
{
    public class CameraHelper : ICameraHelper
    {
        public bool AbrirCamera()
        {
            var context = MainApplication.CurrentContext as Activity;
            if (context == null)
                return false;

            var picker = new MediaPicker(context);
            if (!picker.IsCameraAvailable)
                Console.WriteLine("No camera!");
            else
            {
                try
                {
                    var file = picker.GetTakePhotoUI(new StoreCameraMediaOptions
                    {                        
                        Name = "test.jpg",
                        Directory = Android.OS.Environment.DirectoryPictures
                    });

                    Uri uri = new Uri($"file:///sdcard/photo.jpg");
                    file.PutExtra(MediaStore.ExtraOutput, "file:///sdcard/photo.jpg");

                    context.StartActivityForResult(file, 1001);
                    return true;
                }
                catch (System.OperationCanceledException)
                {
                    Console.WriteLine("Canceled");
                }
            }

            return false;
        }

        /*
         Intent intent = new Intent (MediaStore.ActionImageCapture);
    App._file = new File (App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
    intent.PutExtra (MediaStore.ExtraOutput, Uri.FromFile (App._file));
    StartActivityForResult (intent, 0);
         
         */
    }
}