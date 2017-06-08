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


        public async System.Threading.Tasks.Task LoginAsync()
        {
            var metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (LinqManager.usersDataContext.LoginDatas.Any(user => user.Login.Equals(txtLogin.Text)))
            {
                LinqManager.loggedInUser = LinqManager.usersDataContext.LoginDatas.First(user => user.Login.Equals(txtLogin.Text)).User;

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
                    await metroWindow.ShowMessageAsync( "Account status","Your Account has been blocked, please contact with the admin");
                }

                else
                {
                    await metroWindow.ShowMessageAsync("Inncorrect Login or Password","Please enter a valid login or password");
                    if (!LinqManager.loggedInUser.Role)
                    {
                        LinqManager.loggedInUser.FailedLoginCount++;
                        if (LinqManager.loggedInUser.FailedLoginCount == 3)
                        {
                            LinqManager.loggedInUser.Status = false;
                            await metroWindow.ShowMessageAsync("Account status","Your Account has been blocked due to providing inncorect password, please contact with the admin");
                        }
                        LinqManager.usersDataContext.SubmitChanges();
                    }
                }
            }
            else
            {               
                var result = await metroWindow.ShowMessageAsync("Inncorrect Login or Password", "Please enter a valid login or password");
                if (result == MessageDialogResult.Affirmative)
                {
                    txtLogin.Focus();
                    txtLogin.Clear();
                    txtPassword.Clear();
                }
            }
        }


        private async void btnLogin_ClickAsync(object sender, RoutedEventArgs e)
        {
            await LoginAsync();
        }

        private async void Grid_KeyDownAsync(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await LoginAsync();
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

        public TextBox TestTxtLogin => txtLogin;
        public PasswordBox TestTxtPassword => txtPassword;
        public int TestUserID => userId; public async void GetSqlLoginForTestAsync()
        { await LoginAsync(); }
    }
}

