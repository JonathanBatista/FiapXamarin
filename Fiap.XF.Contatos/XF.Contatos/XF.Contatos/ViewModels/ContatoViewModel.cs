using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Contatos.Global;
using XF.Contatos.Models;
using XF.Contatos.Views;

namespace XF.Contatos.ViewModels
{
    public class ContatoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Contato> ListaContatos { get; set; } = new ObservableCollection<Contato>();
        public List<Contato> ContatosFiltrados { get; set; } = new List<Contato>();

        public Contato ContatoSelecionado { get; set; }

        private string _pesquisarPorNome;
        public string PesquisarPorNome
        {
            get { return _pesquisarPorNome; }
            set
            {
                if (value == _pesquisarPorNome) return;

                _pesquisarPorNome = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PesquisarPorNome)));
                AplicarFiltro();
            }
        }

        public ICommand OnContatoDetalhesCmd { get; private set; }

        public ICommand OnDiscarContatoCmd { get; private set; }

        public ICommand OnTakePhotoCmd { get; private set; }
        public ContatoViewModel()
        {
            CarregarContatos();
            OnContatoDetalhesCmd = new Command<Contato>(ExibirDetalhes);
            OnDiscarContatoCmd = new Command<Contato>(Discar);
            OnTakePhotoCmd = new Command(TirarFoto);
        }

        
        private async void ExibirDetalhes(Contato contato)
        {
            ContatoSelecionado = contato;
            await App.Current.MainPage.Navigation.PushAsync(new ContatoDetalheView() { BindingContext = this });
        }

        public async void Discar(Contato contato)
        {
           var result = await App.Current.MainPage.
                DisplayAlert("Ligando...", $"Deseja ligar para {contato.Numero} número do contato {contato.Nome}?", "Discar", "Cancelar");

            if (result)
            {
                var contatoHelper = DependencyService.Get<IContatoHelper>();
                contatoHelper.LigarParaContato(contato);           
            }
        }

        private void TirarFoto()
        {
            var cameraHelper = DependencyService.Get<ICameraHelper>();
            cameraHelper.AbrirCamera();
        }
        
        private void CarregarContatos()
        {
            MessagingCenter.Subscribe<IContatoHelper, List<Contato>>(this, "obtercontatos",
                (sender, contatos) =>
                {
                    ContatosFiltrados = contatos.OrderBy(x => x.Nome).ToList();
                    AplicarFiltro();
                });


            MessagingCenter.Subscribe<ICameraHelper, byte[]>(this, "obternovaimagem",
                (sender, newImage) =>
                {
                    if (ContatoSelecionado != null)
                    {
                        ContatoSelecionado.ThumbnailBytes = newImage;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContatoSelecionado)));
                    }
                });
        }

        private void AplicarFiltro()
        {
            if (PesquisarPorNome == null) PesquisarPorNome = "";

            var resultado = ContatosFiltrados.Where(n => n.Nome.Normalizar()
                                .Contains(_pesquisarPorNome.Normalizar().Trim())).ToList();

            var removerDaLista = ListaContatos.Except(resultado).ToList();
            foreach (var item in removerDaLista)
            {
                ListaContatos.Remove(item);
            }

            for (int index = 0; index < resultado.Count; index++)
            {
                var item = resultado[index];
                if (index + 1 > ListaContatos.Count || !ListaContatos[index].Equals(item))
                    ListaContatos.Insert(index, item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void EventPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
