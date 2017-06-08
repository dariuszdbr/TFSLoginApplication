using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for RestorePassword.xaml
    /// </summary>
    public partial class PasswordRecveryPanel 
    {
        public PasswordRecveryPanel()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Navigate(new LoginPanel());
        }

        private async void btnFindPassword_ClickAsync(object sender, RoutedEventArgs e)
        {

            var person = (from user in LinqManager.usersDataContext.Users
                          where user.Name == txtName.Text && user.LastName == txtLastName.Text && user.LoginDatas.First().Login.Equals(txtUserLogin.Text)
                          select new { UserID = user.UserID, Name = user.Name, LastName = user.LastName, Login = user.LoginDatas.First().Login, Password = user.LoginDatas.First().Password });
            //var findPerson = LinqManager.usersDataContext.Users.Where( p => p.Name == txtName.Text && p.LastName == txtLastName.Text /*&& p.Login == txtUserLogin.Text */)
            //    .First()
            //    .LoginDatas.Where(x => x.Login.Equals(txtUserLogin))
            //    .First().Password;

            if (person.Any())
            {
                dgRestorePassword.ItemsSource = person;
            }
            else
            {
                var metroWindow = (MetroWindow)Application.Current.MainWindow;
                var result = await metroWindow.ShowMessageAsync("Search Result",
                    "User not found.\nPlease make sure you entered the correct data.");
                if (result == MessageDialogResult.Affirmative)
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
