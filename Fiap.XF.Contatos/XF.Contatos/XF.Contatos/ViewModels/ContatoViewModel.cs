using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Contatos.Global;
using XF.Contatos.Models;

namespace XF.Contatos.ViewModels
{
    public class ContatoViewModel
    {
        public ObservableCollection<Contato> ListaContatos { get; set; } = new ObservableCollection<Contato>();

        // https://github.com/xamarin/Xamarin.Mobile
        

        public ContatoViewModel()
        {
            CarregarContatos();
        }

        public async void Discar(Contato contato)
        {
           var result = await App.Current.MainPage.
                DisplayAlert("Ligando...", $"Deseja ligar para o número {contato.Numero} ?", "Discar", "Cancelar");

            if (result)
            {
                var contatoHelper = DependencyService.Get<IContatoHelper>();
                contatoHelper.LigarParaContato(contato);           
            }
        }

        private void CarregarContatos()
        {
            MessagingCenter.Subscribe<IContatoHelper, List<Contato>>(this, "obtercontatos",
                (sender, contatos) =>
                {
                    foreach (var cont in contatos.OrderBy(x => x.Nome))
                        ListaContatos.Add(cont);
                });
        }
    }
}
