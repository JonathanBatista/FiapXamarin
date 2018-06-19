using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Xamarin.Contacts;
using Xamarin.Forms;
using XF.Contatos.Droid.AppResources;
using XF.Contatos.Global;
using XF.Contatos.Models;

[assembly: Dependency(typeof(ContatoHelper))]
namespace XF.Contatos.Droid.AppResources
{
    public class ContatoHelper : IContatoHelper
    {
        public async Task<bool> GetContatoListAsync()
        {
            var context = MainApplication.CurrentContext as Activity;
            if (context == null) return false;
            var book = new AddressBook(context);

            if (!await book.RequestPermission())
            {
                Console.WriteLine("Permissão negada pelo usuário!");
                return false;
            }

            publishList(book.ToList());
            return true;
        }


        private void publishList(List<Contact> contactList)
        {
            var contatos = new List<Contato>();

            foreach (var cont in contactList)
            {
                contatos.Add(new Contato
                {
                    Nome = cont.DisplayName,
                    Numero = cont.Phones.FirstOrDefault()?.Number
                });
            }

            MessagingCenter.Send<IContatoHelper, List<Contato>>(this, "obtercontatos", contatos);
        }
    }
}