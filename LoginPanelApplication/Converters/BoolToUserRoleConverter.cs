using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LoginPanelApplication.Converters
{
    class BoolToUserRoleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool role = (bool)value;
            if (role) return "Admin";
            else return "Employee";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = (string)value;
            if (status == "Admin") return true;
            else return false;
        }
    }
}
