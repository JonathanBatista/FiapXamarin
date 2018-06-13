using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Recursos.PassParameter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalheView : ContentPage
	{
		public DetalheView ()
		{
			InitializeComponent ();
		}

        private void btnVoltar_Clicked(object sender, EventArgs args)
        {
            Navigation.PopModalAsync();
        }
    }
}