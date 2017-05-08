using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for RestorePassword.xaml
    /// </summary>
    public partial class PasswordRecveryPanel : Page
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
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("SELECT * FROM Users WHERE Name = @Name AND LastName = @LastName AND Login = @Login", connection);
            adapter.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
            adapter.SelectCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLastName.Text;
            adapter.SelectCommand.Parameters.Add("@Login", SqlDbType.NVarChar).Value = txtUserLogin.Text;
            DataTable dt = new DataTable();


            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dgRestorePassword.ItemsSource = dt.DefaultView;
            }
            else
            {
                if (MessageBox.Show("User not found.\nPlease make sure you entered the correct data.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
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
