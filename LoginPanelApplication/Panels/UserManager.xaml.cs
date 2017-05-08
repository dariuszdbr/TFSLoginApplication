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
        SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt;

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

            btnClear_Click(sender, e);
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

        private void DataGridManager_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedIndex = DataGridManager.SelectedIndex;
            txtName.Text = dt.Rows[selectedIndex]["Name"].ToString();
            txtLastName.Text = dt.Rows[selectedIndex]["LastName"].ToString();
            txtPassword.Text = dt.Rows[selectedIndex]["Password"].ToString();

            txtPassword.Focus();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            var selectedIndex = DataGridManager.SelectedIndex;

            if (selectedIndex != -1)
            {
                string update = "Update Users SET Password = @Password , Name = @Name , LastName = @LastName WHERE UserID = @ID";
                adapter.UpdateCommand = new SqlCommand(update, connection);
                adapter.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int).Value = dt.Rows[selectedIndex]["UserID"];
                adapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text.Trim();
                adapter.UpdateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLastName.Text.Trim();
                adapter.UpdateCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = txtPassword.Text.Trim();

                if (txtPassword.Text.Length < 8)
                {
                    MessageBox.Show("The password should contain a minimum of 8 characters", "Too short password");
                    txtPassword.Focus();
                    txtPassword.SelectAll();
                }
                else
                {
                    connection.Open();
                    adapter.UpdateCommand.ExecuteNonQuery();
                    connection.Close();

                    if (MessageBox.Show("Record(s) has been sucessfully updated", "Update report", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                    {
                        DisplayUsersDatabase();
                        btnClear_Click(sender, e);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please first press the mouse button twice on the selected user in the table and then make changes.", "Update report", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtLastName.Clear();
            txtName.Clear();
            txtPassword.Clear();
            txtName.Focus();
        }
    }
}
