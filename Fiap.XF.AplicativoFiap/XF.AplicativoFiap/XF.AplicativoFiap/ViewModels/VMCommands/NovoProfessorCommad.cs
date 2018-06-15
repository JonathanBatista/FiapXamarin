using System;
using System.Windows.Input;
using XF.AplicativoFiap.Models;

namespace XF.AplicativoFiap.ViewModels.VMCommands
{
    public class NovoProfessorCommad : ICommand
    {
        private ProfessoresViewModel professorVM;

        public NovoProfessorCommad(ProfessoresViewModel paramVM) => professorVM = paramVM;        

        public event EventHandler CanExecuteChanged;

        public void NovoCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => ((parameter != null)/* && (!string.IsNullOrWhiteSpace(((Professor)parameter).Nome))*/);

        public void Execute(object parameter)
        {
            var prof = parameter as Professor;
            
            professorVM.NovoProfessor(prof);
        } 
    }
}
