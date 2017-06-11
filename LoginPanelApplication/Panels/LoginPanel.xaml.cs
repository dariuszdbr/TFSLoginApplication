using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using System.Linq;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

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

        public void Login(string login, string password)
        {
            if (LinqManager.usersDataContext.LoginDatas.Any(user => user.Login.Equals(login)))
            {
                LinqManager.loggedInUser = LinqManager.usersDataContext.LoginDatas.First(user => user.Login.Equals(login)).User;

                if (LinqManager.loggedInUser.LoginDatas.Any(x => x.Password.Equals(password)) && LinqManager.loggedInUser.Role)
                {
                    PageSwitcher.Navigate(new AdminPanel());                     // Switch to AdminPanel                  
                }
                else if (LinqManager.loggedInUser.LoginDatas.Any(x => x.Password.Equals(password)) && !LinqManager.loggedInUser.Role && LinqManager.loggedInUser.Status)
                {
                    LinqManager.logInfo = new Loginfo() { UserID = LinqManager.loggedInUser.UserID, LoginDate = DateTime.Now, LogoutDate = null };
                    LinqManager.loggedInUser.In = LinqManager.logInfo.LoginDate;
                    LinqManager.loggedInUser.Loginfos.Add(LinqManager.logInfo);
                    LinqManager.loggedInUser.FailedLoginCount = 0;
                    LinqManager.usersDataContext.SubmitChanges();
                    PageSwitcher.Navigate(new EmployeePanel());                  // Switch to EmployeePanel

                }

                else if (LinqManager.loggedInUser.LoginDatas.Any(x => x.Password.Equals(password)) && !LinqManager.loggedInUser.Role && !LinqManager.loggedInUser.Status)
                {
                    ShowMessageBox("Account status", "Your Account has been blocked, please contact with the admin");
                }

                else
                {
                    ShowMessageBox("Inncorrect Login or Password", "Please enter a valid login or password");
                    if (!LinqManager.loggedInUser.Role)
                    {
                        LinqManager.loggedInUser.FailedLoginCount++;
                        if (LinqManager.loggedInUser.FailedLoginCount == 3)
                        {
                            LinqManager.loggedInUser.Status = false;
                            ShowMessageBox("Account status", "Your Account has been blocked due to providing inncorect password, please contact with the admin");
                        }
                        LinqManager.usersDataContext.SubmitChanges();
                    }
                }
            }
            else
            {
                ShowMessageBox("Inncorrect Login or Password", "Please enter a valid login or password");

                txtLogin.Focus();
                txtLogin.Clear();
                txtPassword.Clear();
            }
        }

        private async void ShowMessageBox(string title, string content)
        {
            var _metroWindow = (MetroWindow)Application.Current.MainWindow;
            await _metroWindow.ShowMessageAsync(title, content);
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login(txtLogin.Text, txtPassword.Password);
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login(txtLogin.Text, txtPassword.Password);
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
    }
}

