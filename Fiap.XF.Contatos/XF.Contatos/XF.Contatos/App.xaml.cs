using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XF.Contatos.ViewModels;
using XF.Contatos.Views;

namespace XF.Contatos
{
	public partial class App : Application
	{

        public static ContatoViewModel ContatoVM { get; set; } 

        public App ()
		{
			InitializeComponent();
            ContatoVM = new ContatoViewModel();
            MainPage = new NavigationPage(new ContatosView() { BindingContext = ContatoVM });
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
