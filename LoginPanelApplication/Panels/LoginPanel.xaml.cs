using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using System.Linq;


namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for LoginPanel.xaml
    /// </summary>
    public partial class LoginPanel : Page
    {
        int userId = 0;

        public LoginPanel()
        {
            InitializeComponent();
        }

        private void Login()
        {
                               
            if (LinqManager.usersDataContext.Users.Any(user => user.Login.Contains(txtLogin.Text) && user.Password.Contains(txtPassword.Password)))
            {
                LinqManager.loggedInUser = LinqManager.usersDataContext.Users.Where(user => user.Login.Contains(txtLogin.Text) && user.Password.Contains(txtPassword.Password)).First();

                if (LinqManager.loggedInUser.Status == true)
                {
                    LinqManager.loggedInUser.LoginDate = DateTime.Now;
                    LinqManager.usersDataContext.SubmitChanges();
                    PageSwitcher.Navigate(new AdminPanel());                     // Switch to AdminPanel
                }
                else
                {
                    LinqManager.loggedInUser.LoginDate = DateTime.Now;
                    LinqManager.usersDataContext.SubmitChanges();
                    PageSwitcher.Navigate(new EmployeePanel());                  // Switch to EmployeePanel
                }
            }
            else
            {
                if (MessageBox.Show("Please enter a valid login and password", "Inncorrect Login or Password", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                {
                    txtLogin.Focus();
                    txtLogin.Clear();
                    txtPassword.Clear();
                }
            }
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();   
        }

        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Window.Close();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PageSwitcher.Navigate(new PasswordRecveryPanel());
        }

        public TextBox TestTxtLogin { get { return txtLogin; } }
        public PasswordBox TestTxtPassword { get { return txtPassword; } }
        public int TestUserID { get { return userId; } }
        public void GetSqlLoginForTest() { Login(); }
    }
}

