using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.AplicativoFiap.App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaProfessoresView : ContentPage
	{

        public ListaProfessoresView ()
		{
			InitializeComponent ();
            
            
		}


        // 
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}