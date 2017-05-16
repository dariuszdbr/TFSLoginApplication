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
            SqlManager.dataAdapter = new SqlDataAdapter();
            SqlManager.dataAdapter.SelectCommand = new SqlCommand("SELECT * FROM Users WHERE Name = @Name AND LastName = @LastName AND Login = @Login", SqlManager.Connection);
            SqlManager.dataAdapter.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
            SqlManager.dataAdapter.SelectCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLastName.Text;
            SqlManager.dataAdapter.SelectCommand.Parameters.Add("@Login", SqlDbType.NVarChar).Value = txtUserLogin.Text;
            SqlManager.dataTable = new DataTable();


            SqlManager.dataAdapter.Fill(SqlManager.dataTable);
            if (SqlManager.dataTable.Rows.Count > 0)
            {
                dgRestorePassword.ItemsSource = SqlManager.dataTable.DefaultView;
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
