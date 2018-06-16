using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Recursos.GPS
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoordenadaView : ContentPage
	{

        public CoordenadaView ()
		{
			InitializeComponent ();
            subscribeCoordenada();
        }

        public async void btnCoordenada_Clicked(object sender, EventArgs e)
        {
            var gps = DependencyService.Get<ILocalizacao>();
            gps.GetCoordenada();
        }


        private void subscribeCoordenada()
        {
            MessagingCenter.Subscribe<ILocalizacao, Coordenada>(this, "coordenada",
                (sender, param) =>
                {
                    lblLatitude.Text = param.Latitude;
                    lblLongitude.Text = param.Longitude;
                });
        }

    }
}