using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for LoginPanel.xaml
    /// </summary>
    public partial class LoginPanel : Page
    {

        string connectionString;
        int userId;

        public LoginPanel()
        {
            InitializeComponent();
        }

        private void Login()
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\users\darek\documents\visual studio 2017\Projects\LoginPanelApplication\LoginPanelApplication\SqlUserDatabase.mdf;Integrated Security=True;MultipleActiveResultSets=True";

            string select = "Select * From Users WHERE Login = @Login and Password = @Password";
            string update = "Update Users SET LoginDate = @LoginDate WHERE UserID = @ID";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand selectCommand = new SqlCommand(select, connection);
            SqlParameter login = new SqlParameter("@Login", txtLogin.Text);
            SqlParameter password = new SqlParameter("@Password", txtPassword.Password);
            selectCommand.Parameters.Add(login);
            selectCommand.Parameters.Add(password);

            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            connection.Open();

            if (dataTable.Rows.Count > 0)
            {
                SqlCommand updateCommand = new SqlCommand(update, connection);  //Create command to update LoginDate
                userId = Convert.ToInt32(dataTable.Rows[0]["UserID"]);          //Find current userId
                SqlParameter userID = new SqlParameter("@ID", userId);              //Create parameter with actual Id
                updateCommand.Parameters.Add(userID);                           //Add parameter to the command

                var panelAdmin = Convert.ToBoolean(dataTable.Rows[0]["Status"]);
                if (panelAdmin == true)
                {
                    updateCommand.Parameters.AddWithValue("@LoginDate", DateTime.Now); // Update Login Date
                    updateCommand.ExecuteNonQuery();                                   // Execute command
                    PageSwitcher.Navigate(new AdminPanel(userId));                           // Switch to AdminPanel
                }
                else
                {
                    updateCommand.Parameters.AddWithValue("@LoginDate", DateTime.Now); // Update Login Date
                    updateCommand.ExecuteNonQuery();                                   // Execute command
                    PageSwitcher.Navigate(new EmployeePanel(userId));                        // Switch to EmployeePanel
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

            connection.Close();

        }


        private void Label_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();   
        }

        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Window.Close();
        }
    }
}

