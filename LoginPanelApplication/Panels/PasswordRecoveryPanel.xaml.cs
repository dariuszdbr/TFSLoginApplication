using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for RestorePassword.xaml
    /// </summary>
    public partial class PasswordRecveryPanel : Page
    {
        public PasswordRecveryPanel()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Navigate(new LoginPanel());
        }

        private void btnFindPassword_Click(object sender, RoutedEventArgs e)
        {

            //var person =( from user in LinqManager.usersDataContext.Users
            //              where user.Login == txtUserLogin.Text && user.Name == txtName.Text && user.LastName == txtLastName.Text
            //              select user );
            var findPerson = LinqManager.usersDataContext.Users.Where( p => p.Name == txtName.Text && p.LastName == txtLastName.Text /*&& p.Login == txtUserLogin.Text */);

            if (findPerson != null)
            {
                dgRestorePassword.ItemsSource = findPerson;
            }
            else
            {
                if (MessageBox.Show("User not found.\nPlease make sure you entered the correct data.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                {
                    txtLastName.Clear();
                    txtName.Clear();
                    txtUserLogin.Clear();
                    txtUserLogin.Focus();
                }
            }
        }
    }
}
