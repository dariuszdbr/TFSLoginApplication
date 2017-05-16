using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for UserManager.xaml
    /// </summary>
    public partial class UserManager : Page
    {
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

            SqlManager.Connection.Open();
            SqlManager.dataAdapter = new SqlDataAdapter();
            SqlManager.dataAdapter.SelectCommand = new SqlCommand(select, SqlManager.Connection);
            SqlManager.dataTable = new DataTable();
            SqlManager.dataAdapter.Fill(SqlManager.dataTable);

            DataGridManager.ItemsSource = SqlManager.dataTable.DefaultView;
          
           SqlManager.Connection.Close();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text.Length < 1 || txtLastName.Text.Length < 1 || (permissionAdmin.IsChecked == false && permissionEmployee.IsChecked == false) || (txtPassword.Text.Length > 0 && txtPassword.Text.Length < 8 ))
            {
                MessageBox.Show("Please fill Name, Last Name, and select permission\nRemember, the password can be automatically  generated only if you leave the Password field blank. " +
                                "Otherwise the password should contain at least 8 characters with one uppercase letter and one digit",
                                "Name, Surname and Permission Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtName.Focus();
                btnClear_Click(sender,e);
            }
            else
            {

                var AdminStatus = (permissionAdmin.IsChecked == true) ? true : false;
                var password = (IsPasswordCorrect(txtPassword.Text)) ? txtPassword.Text : txtName.Text + "123";
                var login = GenerateLogin(txtName.Text, txtLastName.Text);

                string insert = "INSERT INTO Users VALUES(@Name, @LastName, @Login, @Password, @Status, @LoginDate, @LogoutDate, @WorkingTime, @DateOfEmployment)";
                SqlManager.dataAdapter = new SqlDataAdapter();
                SqlManager.dataAdapter.InsertCommand = new SqlCommand(insert, SqlManager.Connection);
                SqlManager.dataAdapter.InsertCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text;
                SqlManager.dataAdapter.InsertCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLastName.Text;
                SqlManager.dataAdapter.InsertCommand.Parameters.Add("@Login", SqlDbType.NVarChar).Value = login;
                SqlManager.dataAdapter.InsertCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                SqlManager.dataAdapter.InsertCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = AdminStatus;
                SqlManager.dataAdapter.InsertCommand.Parameters.Add("@LoginDate", SqlDbType.DateTime).Value = DBNull.Value;
                SqlManager.dataAdapter.InsertCommand.Parameters.Add("@LogoutDate", SqlDbType.DateTime).Value = DBNull.Value;
                SqlManager.dataAdapter.InsertCommand.Parameters.Add("@WorkingTime", SqlDbType.DateTime).Value = DBNull.Value;
                SqlManager.dataAdapter.InsertCommand.Parameters.Add("@DateOfEmployment", SqlDbType.DateTime).Value = DateTime.Now;

                SqlManager.Connection.Open();
                SqlManager.dataAdapter.InsertCommand.ExecuteNonQuery();
                
                SqlManager.Connection.Close();
                DisplayUsersDatabase();
            }

            btnClear_Click(sender, e);
        }

        public bool IsPasswordCorrect(string password)
        {
            byte IsOneUppercaseLetter = 0;
            byte IsOneDigit = 0;
            byte isOneLowercaseLetter = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] >= 65 && password[i] <= 90)
                    IsOneUppercaseLetter++;

                if (password[i] >= 40 && password[i] <= 57)
                    IsOneDigit++;

                if (password[i] >= 99 && password[i] <= 122)
                    isOneLowercaseLetter++;

                if (IsOneDigit > 0 && IsOneUppercaseLetter > 0 && isOneLowercaseLetter > 0) return true;
            }
            return (IsOneUppercaseLetter > 0 && IsOneDigit > 0 && isOneLowercaseLetter > 0);
        }

        private string GenerateLogin(string userName, string lastName)
        {
            bool acces = false;
            string login = string.Empty;
            int number = 0;
            do
            {
                login = (number > 0) ? userName[0].ToString().ToLower() + lastName.ToLower() + number : userName[0].ToString().ToLower() + lastName.ToLower();

                SqlManager.dataAdapter = new SqlDataAdapter();
                SqlManager.dataAdapter.SelectCommand = new SqlCommand("SELECT * FROM Users WHERE Login = @Login", SqlManager.Connection);
                SqlManager.dataAdapter.SelectCommand.Parameters.Add("@Login", SqlDbType.NVarChar).Value = login;
                SqlManager.dataTable = new DataTable();
                SqlManager.dataAdapter.Fill(SqlManager.dataTable);

                if (SqlManager.dataTable.Rows.Count > 0)
                {
                    number++;
                }

                else
                {
                    acces = true;
                }

            } while (!acces);

            return login;
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Navigate(new AdminPanel(AdminID));
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = DataGridManager.SelectedIndex;
            if (selectedIndex != -1)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?\nThere is no undo once data is deleted!", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {

                    SqlManager.dataAdapter = new SqlDataAdapter();
                    SqlManager.dataAdapter.DeleteCommand = new SqlCommand("DELETE FROM Users WHERE UserID = @Id", SqlManager.Connection);
                    SqlManager.dataAdapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.Int).Value = SqlManager.dataTable.Rows[selectedIndex]["UserID"];
                    SqlManager.Connection.Open();
                    SqlManager.dataAdapter.DeleteCommand.ExecuteNonQuery();
                    SqlManager.Connection.Close();
                    DisplayUsersDatabase();
                }
                else
                {
                    MessageBox.Show("Deletion canceled");
                }
            }
            else
                MessageBox.Show("Please select first the user You want to Delete", "Delete result", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DataGridManager_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedIndex = DataGridManager.SelectedIndex;
            txtName.Text = SqlManager.dataTable.Rows[selectedIndex]["Name"].ToString();
            txtLastName.Text = SqlManager.dataTable.Rows[selectedIndex]["LastName"].ToString();
            txtPassword.Text = SqlManager.dataTable.Rows[selectedIndex]["Password"].ToString();

            txtPassword.Focus();
            txtPassword.SelectAll();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            var selectedIndex = DataGridManager.SelectedIndex;

            if (selectedIndex != -1)
            {
                string update = "Update Users SET Password = @Password , Name = @Name , LastName = @LastName WHERE UserID = @ID";
                SqlManager.dataAdapter.UpdateCommand = new SqlCommand(update, SqlManager.Connection);
                SqlManager.dataAdapter.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int).Value = SqlManager.dataTable.Rows[selectedIndex]["UserID"];
                SqlManager.dataAdapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtName.Text.Trim();
                SqlManager.dataAdapter.UpdateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLastName.Text.Trim();
                SqlManager.dataAdapter.UpdateCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = txtPassword.Text.Trim();

                if (txtPassword.Text.Length < 8)
                {
                    MessageBox.Show("The password should contain a minimum of 8 characters", "Too short password");
                    txtPassword.Focus();
                    txtPassword.SelectAll();
                }
                else
                {
                    SqlManager.Connection.Open();
                    SqlManager.dataAdapter.UpdateCommand.ExecuteNonQuery();
                    SqlManager.Connection.Close();

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
