using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using System.Linq;
using Android.Content;
using Android.Graphics;
using System.IO;
using Android.Provider;

namespace XF.Contatos.Droid
{
    [Activity(Label = "XF.Contatos", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        static string[] permissions =
            {
                Manifest.Permission.ReadContacts,
                Manifest.Permission.CallPhone,
                Manifest.Permission.Camera,
                Manifest.Permission.AccessCoarseLocation,
                Manifest.Permission.AccessFineLocation, 
                Manifest.Permission.WriteExternalStorage
            };

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            
            global::Xamarin.Forms.Forms.Init(this, bundle);

            var requestId = new Random().Next(int.MaxValue);
            this.RequestPermissions(permissions, requestId);
        }
        
        

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (permissions.Length == grantResults.Count(x => x == 0))
            {
                LoadApplication(new App());
            }

            return;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            var keys = data.Extras.KeySet();
            
            if (requestCode == 1001 && resultCode == Result.Ok)
            {

               
                //File file = new File(Android.OS.Environment.GetExternalStoragePublicDirectory().getPath(), "photo.jpg");
                try
                {
                    Bitmap bitmap = MediaStore.Images.Media.GetBitmap(this.ContentResolver, data.Data);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                        byte[] array = stream.ToArray();
                    }

                }
                catch (Java.IO.IOException e)
                {
                    //Exception Handling
                }

                var arrayBytes = data.Extras.GetByteArray("MediaFile");

                var path = data.Extras.GetString("path");
                var action = data.Extras.Get("action");
                var isphoto = data.Extras.GetBoolean("IsPhoto");
            }


            

            // base.OnActivityResult(requestCode, resultCode, data);           

        }
    }
}

