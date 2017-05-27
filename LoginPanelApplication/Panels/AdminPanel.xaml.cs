using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            //LinqManager.logInfo.LogoutDate = DateTime.Now;
            //LinqManager.usersDataContext.SubmitChanges();
            PageSwitcher.Navigate(new LoginPanel());
        }

        private void btnUserManager_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Navigate(new UserManager());
        }
    }
}
