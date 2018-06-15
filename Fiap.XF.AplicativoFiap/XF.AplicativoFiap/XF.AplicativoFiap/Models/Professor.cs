namespace XF.AplicativoFiap.Models
{
    public class Professor
    {
        public int Id { get; set; }

        private string nome;

        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                App.ProfessorVM.OnSalvarNovoProfessorCmd.NovoCanExecuteChanged();                
            }
        }

        public string Titulo { get; set; }
    }
}
