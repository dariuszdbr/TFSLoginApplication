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
            if (LinqManager.usersDataContext.LoginDatas.Any(user => user.Login.Equals(txtLogin.Text)))
            {
                LinqManager.loggedInUser = LinqManager.usersDataContext.LoginDatas.Where(user => user.Login.Equals(txtLogin.Text)).First().User;

                if (LinqManager.loggedInUser.LoginDatas.Any(x => x.Password.Equals(txtPassword.Password)) && LinqManager.loggedInUser.Role)
                {
                    PageSwitcher.Navigate(new AdminPanel());                     // Switch to AdminPanel
                }
                else if (LinqManager.loggedInUser.LoginDatas.Any(x => x.Password.Equals(txtPassword.Password)) && !LinqManager.loggedInUser.Role && LinqManager.loggedInUser.Status)
                {
                    LinqManager.logInfo = new Loginfo() { UserID = LinqManager.loggedInUser.UserID, LoginDate = DateTime.Now, LogoutDate = null };
                    LinqManager.loggedInUser.In = LinqManager.logInfo.LoginDate;
                    LinqManager.loggedInUser.Loginfos.Add(LinqManager.logInfo);
                    LinqManager.loggedInUser.FailedLoginCount = 0;
                    LinqManager.usersDataContext.SubmitChanges();
                    PageSwitcher.Navigate(new EmployeePanel());                  // Switch to EmployeePanel
                }

                else if (LinqManager.loggedInUser.LoginDatas.Any(x => x.Password.Equals(txtPassword.Password)) && !LinqManager.loggedInUser.Role && !LinqManager.loggedInUser.Status)
                {
                    MessageBox.Show("Your Account has been blocked, please contact with the admin", "Account status", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                else
                {
                    MessageBox.Show("Please enter a valid login or password", "Inncorrect Login or Password", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    if (!LinqManager.loggedInUser.Role)
                    {
                        LinqManager.loggedInUser.FailedLoginCount++;
                        if (LinqManager.loggedInUser.FailedLoginCount == 3)
                        {
                            LinqManager.loggedInUser.Status = false;
                            MessageBox.Show("Your Account has been blocked due to providing inncorect password, please contact with the admin", "Account status", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        LinqManager.usersDataContext.SubmitChanges();
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Please enter a valid login or password", "Inncorrect Login or Password", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
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

