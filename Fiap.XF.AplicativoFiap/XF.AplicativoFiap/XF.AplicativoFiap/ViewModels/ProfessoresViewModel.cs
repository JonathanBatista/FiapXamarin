﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XF.AplicativoFiap.Models;
using XF.AplicativoFiap.Repositories;
using XF.AplicativoFiap.ViewModels.VMCommands;
using XF.AplicativoFiap.Views;

namespace XF.AplicativoFiap.ViewModels
{
    public class ProfessoresViewModel : INotifyPropertyChanged
    {
        private readonly ProfessorRepository _professorRepository = new ProfessorRepository();       

        public ObservableCollection<Professor> Professores { get; set; } = new ObservableCollection<Professor>();

        public Professor ProfessorModel { get; set; }

        public ICommand OnNovoProfessorCmd { get; set; }

        public ICommand OnCancelarCmd { get; set; }

        public DetalheProfessorCommand OnProfessorDetalhesCmd { get; private set; }

        public RemoverProfessorCommand OnRemoverProfessorCmd { get; private set; }

        public NovoProfessorCommad OnSalvarNovoProfessorCmd { get; private set; }

        public ProfessoresViewModel()
        {            
            CarregarProfessores(false);
            carregarCommands();
        }

        public ProfessoresViewModel(Professor novoProfessor)
        {
            ProfessorModel = novoProfessor;
            carregarCommands();            
        }

        private void carregarCommands()
        {
            OnNovoProfessorCmd = new Command(OnNovoProfessor);
            OnCancelarCmd = new Command(OnCancelar);
            OnProfessorDetalhesCmd = new DetalheProfessorCommand(this);
            OnRemoverProfessorCmd = new RemoverProfessorCommand(this);
            OnSalvarNovoProfessorCmd = new NovoProfessorCommad(this);            
        }


        #region Commands methods

        private async void OnNovoProfessor()
        {
            await App.Current.MainPage.Navigation.PushAsync(
                new ProfessorView()
                {
                    BindingContext = new ProfessoresViewModel(new Professor())
                });
        }

        private async void OnCancelar()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public async void VisualizarDetalhesProfessor(Professor professor)
        {
            await App.Current.MainPage.Navigation.PushAsync(
                new ProfessorDetalheView()
                {
                    BindingContext = new ProfessoresViewModel(professor)
                });
        }

        public async void EditarProfessorAtual(Professor professor)
        {
            await App.Current.MainPage.Navigation.PushAsync(
                new ProfessorView()
                {
                    BindingContext = new ProfessoresViewModel(professor)
                });
        }
        #endregion


        public async void CarregarProfessores(bool updateBase)
        {            
            var professoresData = await ProfessorRepository.GetProfessoresSqlAzureAsync(updateBase);

            if (professoresData != null)
            {
                Professores.Clear();
                foreach (var prof in professoresData)
                    Professores.Add(prof);
            }
        }   

        public async void NovoProfessor(Professor professor)
        {
            if (string.IsNullOrWhiteSpace(professor.Nome) || string.IsNullOrWhiteSpace(professor.Titulo))
            {
                await Application.Current.MainPage.DisplayAlert("Alerta!", "Todos os campos são obrigatórios!", "OK");
                return;
            }

            professor.Id = (int)((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds / 516);

            var result = await ProfessorRepository.PostProfessorSqlAzureAsync(professor);

            if (result)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        public async void EditarProfessor(Professor professor)
        {
            if (string.IsNullOrWhiteSpace(professor.Nome) || string.IsNullOrWhiteSpace(professor.Titulo))
            {
                await App.Current.MainPage.DisplayAlert("Ooops!", "O item selecionado não é válido!", "Ok");
                return;
            }

            await ProfessorRepository.PostProfessorSqlAzureAsync(professor);
        }

        public async void RemoverProfessor(Professor professorDeletado)
        {
            // TODO: IMPLEMENTAR

            if (professorDeletado.Id == 0 || !(Professores.Any(x => x.Id == professorDeletado.Id)))
            {
                await App.Current.MainPage.DisplayAlert("Ooops!", "O item selecionado p/ remoção não é válido!", "Ok");
                return;
            }
            var result = await ProfessorRepository.DeleteProfessorSqlAzureAsync(professorDeletado.Id.ToString());

            if (result)
            {
                CarregarProfessores(true);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void EventPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }            
    }
}