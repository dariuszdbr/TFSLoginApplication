using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Threading.Tasks;
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

        private void btnFindPassword_Click(object sender, RoutedEventArgs e)
        {
            var person = (from user in LinqManager.usersDataContext.Users
                          where user.Name == txtName.Text && user.LastName == txtLastName.Text && user.LoginDatas.First().Login.Equals(txtUserLogin.Text)
                          select new { UserID = user.UserID, Name = user.Name, LastName = user.LastName, Login = user.LoginDatas.First().Login, Password = user.LoginDatas.First().Password });

            if (person.Any())
            {
                dgRestorePassword.ItemsSource = person;
            }
            else
            {
                ShowMessageBox("Search Result", "User not found.\nPlease make sure you entered the correct data.");

                txtLastName.Clear();
                txtName.Clear();
                txtUserLogin.Clear();
                txtUserLogin.Focus();
            }
        }

        private static async void ShowMessageBox(string title, string content)
        {
            var metroWindow = (MetroWindow) Application.Current.MainWindow;
            await metroWindow.ShowMessageAsync(title,content);
        }
    }
}
