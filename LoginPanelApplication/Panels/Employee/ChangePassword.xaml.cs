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

        private async void btmSetPassword_ClickAsync(object sender, RoutedEventArgs e)
        {
            bool equal = false;
            bool correct = false;
            
            if (txtPassword.Text.Equals(txtRepeat.Text))
                equal = true;
            else
            {      
                await this.ShowMessageAsync("Password status", "Password must be equals");
            }


            if (Password.Check(txtPassword.Text))
                correct = true;
            else
                await this.ShowMessageAsync("Password correctness",
                    "Password should contain at least 8 characters with one uppercase letter, one lowercase letter and one digit");

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
