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
        int AdminID;

        public AdminPanel(int userid)
        {
            InitializeComponent();
            AdminID = userid;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            string update = "UPDATE Users SET LogoutDate = @LogoutDate WHERE UserID = @Id";
            SqlManager.updateCommand = new SqlCommand(update, SqlManager.Connection);
            SqlParameter paramUserId = new SqlParameter("@Id", AdminID);
            SqlManager.updateCommand.Parameters.Add(paramUserId);

            SqlManager.Connection.Open();
            SqlManager.updateCommand.Parameters.AddWithValue("@LogoutDate", DateTime.Now);
            SqlManager.updateCommand.ExecuteNonQuery();
            SqlManager.Connection.Close();

            PageSwitcher.Navigate(new LoginPanel());
        }

        private void btnUserManager_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Navigate(new UserManager(AdminID));
        }
    }
}
