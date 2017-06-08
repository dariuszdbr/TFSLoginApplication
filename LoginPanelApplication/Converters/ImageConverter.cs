using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LoginPanelApplication.Converters
{
    class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("Target type must be System.Windows.Media.ImageSource.");

            try
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri("/LoginPanelApplication;component/Images/person" + value + ".jpg", UriKind.Relative);
                img.EndInit();
                return img;
            }
            catch (Exception)
            {
                return DependencyProperty.UnsetValue;
            }
        }
    

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
