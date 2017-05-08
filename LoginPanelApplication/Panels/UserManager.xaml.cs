using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for UserManager.xaml
    /// </summary>
    public partial class UserManager : Page
    {
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\users\darek\documents\visual studio 2017\Projects\LoginPanelApplication\LoginPanelApplication\SqlUserDatabase.mdf;Integrated Security=True;MultipleActiveResultSets=True";
        SqlConnection connection = new SqlConnection(connectionString); 
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt;
        DataSet ds;

        int AdminID;


        public UserManager(int userId)
        {
            InitializeComponent();
            DisplayUsersDatabase();
            AdminID = userId;
        }

        private void DisplayUsersDatabase()
        {
            string select = "SELECT * FROM Users";
            adapter.SelectCommand = new SqlCommand(select, connection);
            dt = new DataTable();
            adapter.Fill(dt);

            DataGridManager.ItemsSource = dt.DefaultView;
          
           connection.Close();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text.Length < 1 || txtLastName.Text.Length < 1 || (permissionAdmin.IsChecked == false && permissionEmployee.IsChecked == false))
            {
                MessageBox.Show("Please fill Name, Last Name, and select permission", "Name, Surname and Permission Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtName.Focus();
                txtName.Clear();
                txtLastName.Clear();
                txtPassword.Clear();
            }
            else
            {

                var AdminStatus = (permissionAdmin.IsChecked == true) ? true : false;
                var password = (txtPassword.Text.Length > 0) ? txtPassword.Text : txtName.Text + "123";
                var login = txtName.Text[0].ToString().ToLower() + txtLastName.Text.ToLower();

                string insert = "INSERT INTO Users VALUES(@Name, @LastName, @Login, @Password, @Status, @LoginDate, @LogoutDate, @WorkingTime, @DateOfEmployment)";
                connection = new SqlConnection(connectionString);
                adapter = new SqlDataAdapter();
                adapter.InsertCommand = new SqlCommand(insert, connection);
                adapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
                adapter.InsertCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLastName.Text;
                adapter.InsertCommand.Parameters.Add("@Login", SqlDbType.NVarChar).Value = login;
                adapter.InsertCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                adapter.InsertCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = AdminStatus;
                adapter.InsertCommand.Parameters.Add("@LoginDate", SqlDbType.DateTime).Value = DBNull.Value;
                adapter.InsertCommand.Parameters.Add("@LogoutDate", SqlDbType.DateTime).Value = DBNull.Value;
                adapter.InsertCommand.Parameters.Add("@WorkingTime", SqlDbType.DateTime).Value = DBNull.Value;
                adapter.InsertCommand.Parameters.Add("@DateOfEmployment", SqlDbType.DateTime).Value = DateTime.Now;

                connection.Open();
                adapter.InsertCommand.ExecuteNonQuery();
                
                connection.Close();
                DisplayUsersDatabase();
            }

            txtLastName.Clear();
            txtName.Clear();
            txtPassword.Clear();
            txtName.Focus();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Navigate(new AdminPanel(AdminID));
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure?\nThere is no undo once data is deleted!", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                var selectedIndex = DataGridManager.SelectedIndex;
                adapter = new SqlDataAdapter();
                adapter.DeleteCommand = new SqlCommand("DELETE FROM Users WHERE UserID = @Id", connection);
                adapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.Int).Value = dt.Rows[selectedIndex]["UserID"];
                connection.Open();
                adapter.DeleteCommand.ExecuteNonQuery();
                connection.Close();
                DisplayUsersDatabase();
            }
            else
            {
                MessageBox.Show("Deletion canceled");
            }
        }
    }
}
