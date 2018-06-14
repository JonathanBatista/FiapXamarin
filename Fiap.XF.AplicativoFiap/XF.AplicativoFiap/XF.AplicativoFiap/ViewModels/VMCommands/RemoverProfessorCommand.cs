using System;
using System.Windows.Input;
using XF.AplicativoFiap.Models;

namespace XF.AplicativoFiap.ViewModels.VMCommands
{
    public class RemoverProfessorCommand : ICommand
    {
        private ProfessoresViewModel professorVM;

        public RemoverProfessorCommand(ProfessoresViewModel paramVM)
        {
            professorVM = paramVM;
        }

        public event EventHandler CanExecuteChanged;

        public void RemoverCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => (parameter != null);

        public void Execute(object parameter)
        {
            professorVM.RemoverProfessor(parameter as Professor);
        }
    }
}
