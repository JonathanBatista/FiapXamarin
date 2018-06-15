using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.AplicativoFiap.ViewModels;

namespace XF.AplicativoFiap.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaProfessoresView : ContentPage
	{

        public ListaProfessoresView ()
		{
			InitializeComponent ();

            

        }

        protected override void OnAppearing()
        {
            var viewModel = (ProfessoresViewModel)BindingContext;
            viewModel.CarregarProfessores(true);
        }
        
    }
}