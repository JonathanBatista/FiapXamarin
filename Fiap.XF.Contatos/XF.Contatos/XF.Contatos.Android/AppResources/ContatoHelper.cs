using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Telephony;
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

            var contactsPermission = Manifest.Permission.ReadContacts;            

            if (context.CheckSelfPermission(contactsPermission) == (int) Permission.Granted)
            {
                var book = new AddressBook(context);
                if (!await book.RequestPermission())
                {
                    Console.WriteLine("Permissão negada pelo usuário!");
                    return false;
                }

                publishList(book.ToList());
                return true;
            }

            return false;
        }

        public bool LigarParaContato(Contato contato)
        {
            var context = MainApplication.CurrentContext as Activity;
            if (context == null) return false;

            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Android.Net.Uri.Parse("tel:" + contato.Numero));

            if (IsIntentAvailable(context, intent))
            {
                context.StartActivity(intent);
                return true;
            }

            return false;
        }

        public static bool IsIntentAvailable(Context context, Intent intent)
        {
            var packageManager = context.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                .Union(packageManager.QueryIntentActivities(intent, 0));

            if (list.Any()) return true;

            var manager = TelephonyManager.FromContext(context);
            return manager.PhoneType != Android.Telephony.PhoneType.None;
        }

        private void publishList(List<Contact> contactList)
        {
            var contatos = new List<Contato>();

            
            foreach (var cont in contactList)
            {
                byte[] thumb = { };
                var contThumbnail = cont.GetThumbnail();

                if (contThumbnail != null)
                {
                    using (var memStream = new MemoryStream())
                    {
                        Bitmap scaledThumb = Bitmap.CreateScaledBitmap(cont.GetThumbnail(), 1200, 1200, true);
                        scaledThumb.Compress(Bitmap.CompressFormat.Jpeg, 50, memStream);
                        thumb = memStream.ToArray();
                    }
                }               
                
                contatos.Add(new Contato
                {
                    Nome = cont.DisplayName,
                    Numero = cont.Phones.FirstOrDefault()?.Number,
                    ThumbnailBytes = thumb
                });
            }

            MessagingCenter.Send<IContatoHelper, List<Contato>>(this, "obtercontatos", contatos);
        }
    }
}