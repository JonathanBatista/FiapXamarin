using System;
using System.Windows.Input;
using XF.AplicativoFiap.Models;

namespace XF.AplicativoFiap.ViewModels.VMCommands
{
    public class EditarProfessorCommand : ICommand
    {
        private ProfessoresViewModel professorVM;

        public EditarProfessorCommand(ProfessoresViewModel paramVM)
        {
            professorVM = paramVM;
        }

        public event EventHandler CanExecuteChanged;

        public void EditCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => (parameter != null);

        public void Execute(object parameter)
        {
            professorVM.EditarProfessorAtual(parameter as Professor);
        }
    }
}
