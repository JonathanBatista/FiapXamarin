using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using XF.AplicativoFiap.App.Models;
using XF.AplicativoFiap.App.Repositories;
using XF.AplicativoFiap.App.ViewModels.VMCommands;
using XF.AplicativoFiap.App.Views;

namespace XF.AplicativoFiap.App.ViewModels
{
    public class ProfessoresViewModel
    {
        private readonly ProfessorRepository _professorRepository = new ProfessorRepository();

        public ObservableCollection<Professor> Professores { get; set; }

        public Professor Professor { get; private set; }

        public ICommand OnNovoProfessorCmd { get; set; }

        public DetalheProfessorCommand OnProfessorDetalhesCmd { get; set; }

        public RemoverProfessorCommand OnRemoverProfessorCmd { get; set; }

        public ProfessoresViewModel()
        {
            OnNovoProfessorCmd = new Command(OnNovoProfessor);
            OnProfessorDetalhesCmd = new DetalheProfessorCommand(this);
            OnRemoverProfessorCmd = new RemoverProfessorCommand(this);
            Professores = new ObservableCollection<Professor>();
        }

        public ProfessoresViewModel(Professor novoProfessor)
        {
            OnNovoProfessorCmd = new Command(OnNovoProfessor);
            OnProfessorDetalhesCmd = new DetalheProfessorCommand(this);
            OnRemoverProfessorCmd = new RemoverProfessorCommand(this);
            Professor = novoProfessor;
        }


        #region Commands methods

        private async void OnNovoProfessor()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(
                new ProfessorView()
                {
                    BindingContext = new ProfessoresViewModel(new Professor())
                });
        }

        public async void VisualizarDetalhesProfessor(Professor professor)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(
                new ProfessorDetalheView()
                {
                    BindingContext = new ProfessoresViewModel(professor)
                });
        }

        public async void EditarProfessorAtual(Professor professor)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(
                new ProfessorView()
                {
                    BindingContext = new ProfessoresViewModel(professor)
                });
        }
        #endregion


        private async void CarregarProfessores()
        {
            var professoresData = await ProfessorRepository.GetProfessoresSqlAzureAsync();

            if (professoresData != null)
            {
                foreach (var prof in professoresData)
                    Professores.Add(prof);
            }
        }   

        public async void NovoProfessor(Professor professor)
        {           
            await ProfessorRepository.PostProfessorSqlAzureAsync(professor);
        }

        public async void EditarProfessor(Professor professor)
        {
            await ProfessorRepository.PostProfessorSqlAzureAsync(professor);
        }

        public async void RemoverProfessor(Professor professorDeletado)
        {
            // TODO: IMPLEMENTAR
            await ProfessorRepository.DeleteProfessorSqlAzureAsync(professorDeletado.Id.ToString());
        }
    }
}
