using System;
using System.Windows.Input;
using XF.AplicativoFiap.App.Models;


namespace XF.AplicativoFiap.App.ViewModels.VMCommands
{
    public class DetalheProfessorCommand : ICommand
    {
        private ProfessoresViewModel professorVM;

        public DetalheProfessorCommand(ProfessoresViewModel paramVM)
        {
            professorVM = paramVM;
        }

        public event EventHandler CanExecuteChanged;

        public void DetalheCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => (parameter != null);
        public void Execute(object parameter)
        {
            var prof = parameter as Professor;

            //TODO IMPLEMENTAR
            professorVM.VisualizarDetalhesProfessor(prof);            
        }
    }
}
