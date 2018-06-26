using System;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace XF.Contatos.Converters
{
    public class ByteToImageFieldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;

                byte[] decodedByteArray = System.Convert.FromBase64String(System.Convert.ToBase64String(imageAsBytes, 0, imageAsBytes.Length));
                
                retSource = ImageSource.FromStream(() => new MemoryStream(decodedByteArray));
            }
            return retSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
