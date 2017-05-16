using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;

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

        private void Login()
        {
            string select = "Select * From Users WHERE Login = @Login and Password = @Password collate SQL_Latin1_General_Cp1_CS_AS";
            SqlManager.dataAdapter = new SqlDataAdapter();
            SqlManager.dataAdapter.SelectCommand = new SqlCommand(select, SqlManager.Connection);
            SqlManager.dataAdapter.SelectCommand.Parameters.Add("@Login", SqlDbType.NVarChar).Value = txtLogin.Text;
            SqlManager.dataAdapter.SelectCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = txtPassword.Password;
            SqlManager.dataTable = new DataTable();

            SqlManager.dataAdapter.Fill(SqlManager.dataTable);

            SqlManager.Connection.Open();

            if (SqlManager.dataTable.Rows.Count > 0)
            {
                string update = "Update Users SET LoginDate = @LoginDate WHERE UserID = @ID";
                SqlCommand updateCommand = new SqlCommand(update, SqlManager.Connection);        //Create command to update LoginDate
                userId = Convert.ToInt32(SqlManager.dataTable.Rows[0]["UserID"]);                //Find current userId
                updateCommand.Parameters.Add("@ID", SqlDbType.Int).Value = userId;               //Add parameter to the command

                var panelAdmin = Convert.ToBoolean(SqlManager.dataTable.Rows[0]["Status"]);
                if (panelAdmin == true)
                {
                    updateCommand.Parameters.AddWithValue("@LoginDate", DateTime.Now); // Update Login Date
                    updateCommand.ExecuteNonQuery();                                   // Execute command
                    PageSwitcher.Navigate(new AdminPanel(userId));                     // Switch to AdminPanel
                }
                else
                {
                    updateCommand.Parameters.AddWithValue("@LoginDate", DateTime.Now); // Update Login Date
                    updateCommand.ExecuteNonQuery();                                   // Execute command
                    PageSwitcher.Navigate(new EmployeePanel(userId));                  // Switch to EmployeePanel
                }
            }
            else
            {
                if (MessageBox.Show("Please enter a valid login and password", "Inncorrect Login or Password", MessageBoxButton.OK, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                {
                    txtLogin.Focus();
                    txtLogin.Clear();
                    txtPassword.Clear();
                }
            }
            SqlManager.Connection.Close();
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();   
        }

        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
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

        public TextBox TestTxtLogin { get { return txtLogin; } }
        public PasswordBox TestTxtPassword { get { return txtPassword; } }
        public int TestUserID { get { return userId; } }
        public void GetSqlLoginForTest() { Login(); }
    }
}

