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

        private void btmSetPassword_Click(object sender, RoutedEventArgs e)
        {
            bool equal = false;
            bool correct = false;

            if (txtPassword.Text.Equals(txtRepeat.Text))
                equal = true;
            else MessageBox.Show("Password must be equals", "Password status", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            if (Password.Check(txtPassword.Text))
                correct = true;
            else MessageBox.Show("Password should contain at least 8 characters with one uppercase letter, one lowercase letter and one digit", "Password correctness ", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            if(equal && correct)
            {
                LinqManager.loggedInUser.ChangePassword = false;
                LinqManager.loggedInUser.LoginDatas.First().Password = txtPassword.Text;
                LinqManager.usersDataContext.SubmitChanges();
                this.Close();
            }
        }
    }
}
