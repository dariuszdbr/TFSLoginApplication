using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for EmployeePanel.xaml
    /// </summary>
    public partial class EmployeePanel : Page
    {
        readonly MetroWindow _metroWindow = (MetroWindow) Application.Current.MainWindow;
        public EmployeePanel()
        {
            InitializeComponent();
 
            SetLabelContent();
            hasToChangePasswordAsync();
        }

        private async void hasToChangePasswordAsync()
        {
            if (LinqManager.loggedInUser.ChangePassword)
            {
                
                var result = await _metroWindow.ShowMessageAsync("Set new password",
                    "You are login for the first time, or your password has been changed. You have to set new password");
                if (result == MessageDialogResult.Affirmative)
                {
                    ChangePassword setNew = new ChangePassword();
                    setNew.ShowDialog();
                }
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LinqManager.loggedInUser.Out = DateTime.Now;
            LinqManager.logInfo.Hours = LinqManager.loggedInUser.Out - LinqManager.loggedInUser.In;
            LinqManager.logInfo.LogoutDate = LinqManager.loggedInUser.Out;
            LinqManager.usersDataContext.SubmitChanges();

            PageSwitcher.Navigate(new LoginPanel());
        }

        private void SetLabelContent()
        {
            string loginDate = LinqManager.logInfo.LoginDate.ToString();
            string name = LinqManager.loggedInUser.Name;
            string login = LinqManager.usersDataContext.LoginDatas.First(x => x.UserID.Equals(LinqManager.loggedInUser.UserID)).Login;

            lblCurrentUser.Content = "Login date: " + loginDate + ". Have a nice day :). \n";
            lblHello.Content = "Welcome " + name + " (" + login + ")";
        }

        private void btnChecAttendance_Click(object sender, RoutedEventArgs e)
        {
            User loggedInUser = LinqManager.loggedInUser;
            UserDetails details = new UserDetails(loggedInUser);
            details.ShowDialog();
        }
    }
}
