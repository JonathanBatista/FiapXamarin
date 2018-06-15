using System;
using System.Windows.Input;
using XF.AplicativoFiap.Models;


namespace XF.AplicativoFiap.ViewModels.VMCommands
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

        public bool CanExecute(object parameter) => ((parameter != null) && ((Professor)parameter).Id != 0);
        public void Execute(object parameter)
        {
            var prof = parameter as Professor;

            //TODO IMPLEMENTAR
            professorVM.VisualizarDetalhesProfessor(prof);            
        }
    }
}
