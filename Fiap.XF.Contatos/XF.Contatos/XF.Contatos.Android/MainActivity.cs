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
using Xamarin.Media;
using Xamarin.Forms;
using XF.Contatos.Global;
using XF.Contatos.Droid.AppResources;
using Java.Nio;

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

        protected async override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            var keys = data.Extras.KeySet();

            if (requestCode == 800 && resultCode == Result.Ok)
            {
                MediaFile file = await data.GetMediaFileExtraAsync(this);
                Stream st = file.GetStream();
                byte[] array;
                using (MemoryStream stream = new MemoryStream())
                {
                    st.CopyTo(stream);
                    Bitmap bmp = Bitmap.CreateBitmap(1200, 1200, Bitmap.Config.Argb8888);
                    bmp.Compress(Bitmap.CompressFormat.Jpeg, 50, stream);
                    array = stream.ToArray();

                    var novoBmp = Bitmap.CreateScaledBitmap(BitmapFactory.DecodeByteArray(array, 0, array.Length), 1200, 1200, false);

                    using (var newStream = new MemoryStream())
                    {
                        novoBmp.Compress(Bitmap.CompressFormat.Jpeg, 50, newStream);
                        array = newStream.ToArray();
                    }
                }

                MessagingCenter.Send<ICameraHelper, byte[]>(new CameraHelper(), "obternovaimagem", array);
            }
        }

    }
}

