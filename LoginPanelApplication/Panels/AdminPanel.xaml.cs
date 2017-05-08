using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);

            SqlCommand updateCommand = new SqlCommand(update, connection);
            SqlParameter paramUserId = new SqlParameter("@Id", AdminID);
            updateCommand.Parameters.Add(paramUserId);

            connection.Open();
            updateCommand.Parameters.AddWithValue("@LogoutDate", DateTime.Now);
            updateCommand.ExecuteNonQuery();
            connection.Close();

            PageSwitcher.Navigate(new LoginPanel());
        }

        private void btnUserManager_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Navigate(new UserManager(AdminID));
        }
    }
}
