using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using XF.Contatos.Global;
using XF.Contatos.Models;

namespace XF.Contatos.ViewModels
{
    public class ContatoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Contato> ListaContatos { get; set; } = new ObservableCollection<Contato>();

        // https://github.com/xamarin/Xamarin.Mobile

        public ContatoViewModel()
        {
            CarregarContatos();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
