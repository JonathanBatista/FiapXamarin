using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.AplicativoFiap.App.ViewModels;
using XF.AplicativoFiap.App.Views;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace XF.AplicativoFiap.App
{
	public partial class App : Application
	{

        public static ProfessoresViewModel ProfessorVM { get; set; }

        public App ()
		{
			InitializeComponent();
            InitializeApplication();

			MainPage = new NavigationPage(new ListaProfessoresView() { BindingContext = ProfessorVM });
		}

        private void InitializeApplication()
        {
            if (ProfessorVM == null)
            {
                ProfessorVM = new ProfessoresViewModel();
            }
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
