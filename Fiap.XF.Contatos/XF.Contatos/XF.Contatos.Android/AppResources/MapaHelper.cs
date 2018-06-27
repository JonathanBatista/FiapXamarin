using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Geolocation;
using XF.Contatos.Droid.AppResources;
using XF.Contatos.Global;

[assembly: Dependency(typeof(MapaHelper))]
namespace XF.Contatos.Droid.AppResources
{
    public class MapaHelper : IMapaHelper
    {
        public async Task<bool> AbrirLocalizacao()
        {
            var context = MainApplication.CurrentContext as Activity;
            if (context == null)
                return false;

            var locationPermission = Manifest.Permission.ReadContacts;

            if (context.CheckSelfPermission(locationPermission) == (int)Permission.Granted)
            {
                var locator = new Geolocator(context) { DesiredAccuracy = 50 };

                var position = await locator.GetPositionAsync(timeout: 10000);

                var geoString = $"geo:{position.Latitude},{position.Longitude}?q={position.Latitude},{position.Longitude}(Label+Name)";

                var geoUri = Android.Net.Uri.Parse(geoString);

                var mapIntent = new Intent(Intent.ActionView, geoUri);

                context.StartActivity(mapIntent);
                return true;
            }

            return false;
        }
    }
}