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
            //         new AddressBook (this); on Android

            if (!await book.RequestPermission())
            {
                Console.WriteLine("Permissão negada pelo usuário!");
                return false;
            }

            foreach (Contact contact in book.OrderBy(c => c.LastName))
            {
                Console.WriteLine("{0} {1}", contact.FirstName, contact.LastName);
            }

            return true;
        }


        private void publishList(AddressBook currentBook)
        {
            // TODO: obter

            MessagingCenter.Send<IContatoHelper, List<Contato>>(this, "obtercontatos", new List<Contato>());
        }
    }
}