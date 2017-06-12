using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private async void ShowMessageBox(string title, string content)
        {
            var _metroWindow = (MetroWindow)Application.Current.MainWindow;
            await _metroWindow.ShowMessageAsync(title, content);
        }

        private void btmSetPassword_Click(object sender, RoutedEventArgs e)
        {
            bool equal = true;
            bool correct = true;

            if (!txtPassword.Text.Equals(txtRepeat.Text))
            {
                equal = false;
                ShowMessageBox("Password status", "Password must be equals");
            }

            else if (!Password.Check(txtPassword.Text))
            {
                correct = false;
                ShowMessageBox("Password correctness",
                    "Password should contain at least 8 characters with one uppercase letter, one lowercase letter and one digit");
            }

            if (equal && correct)
            {
                LinqManager.loggedInUser.ChangePassword = false;
                LinqManager.loggedInUser.LoginDatas.First().Password = txtPassword.Text;
                LinqManager.usersDataContext.SubmitChanges();
                this.Close();
            }
        }
    }
}
