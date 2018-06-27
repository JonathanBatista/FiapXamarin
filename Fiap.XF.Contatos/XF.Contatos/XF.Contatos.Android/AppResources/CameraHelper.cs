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
using Java.IO;
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
                System.Console.WriteLine("No camera!");
            else
            {
                try
                {
                    string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    var file = picker.GetTakePhotoUI(new StoreCameraMediaOptions
                    {                        
                        Name = "test.jpg",
                        Directory = $"~{path}"
                    });
                    context.StartActivityForResult(file, 800);
                    return true;
                }
                catch (System.OperationCanceledException)
                {
                    System.Console.WriteLine("Canceled");
                }
            }

            return false;
        }
    }
}