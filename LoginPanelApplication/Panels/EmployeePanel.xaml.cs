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
    /// Interaction logic for EmployeePanel.xaml
    /// </summary>
    public partial class EmployeePanel : Page
    {
        int EmployeeID;

        public EmployeePanel(int userid)
        {
            InitializeComponent();
            EmployeeID = userid;
            LabelContent();

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            string select = "SELECT LoginDate, WorkingTime From Users WHERE UserID = @Id";
            string update = "UPDATE Users SET LogoutDate = @LogoutDate, WorkingTime = @WorkingTime WHERE UserID = @Id";
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlCommand selectCommand = new SqlCommand(select, connection);
            selectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = EmployeeID;

            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            connection.Open();

            SqlCommand updateCommand = new SqlCommand(update, connection);
            SqlParameter paramUserId = new SqlParameter("@Id", EmployeeID);
            updateCommand.Parameters.Add(paramUserId);

            DateTime logout;

            updateCommand.Parameters.AddWithValue("@LogoutDate", logout = DateTime.Now);
            
            DateTime login = Convert.ToDateTime(dataTable.Rows[0]["LoginDate"]);
            TimeSpan workingTime = logout - login;

         
            MessageBox.Show(workingTime.TotalHours.ToString());

            updateCommand.Parameters.Add("@WorkingTime", SqlDbType.Time).Value = workingTime;
            updateCommand.ExecuteNonQuery();
            connection.Close();

            

            PageSwitcher.Navigate(new LoginPanel());

        }


        private void btnAddNotes_Click(object sender, RoutedEventArgs e)
        {
            txtBlock.Text += txtAddNotes.Text + "\n";
            txtAddNotes.Clear();
        }

        private void btnEditNotes_Click(object sender, RoutedEventArgs e)
        {
            txtAddNotes.Text = txtBlock.Text;
            txtBlock.Text = "";
            txtAddNotes.Focus();
        }

        private void LabelContent()
        {
            string loginDate="";
            string name = "";

            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlCommand selectCommand = new SqlCommand("Select Name,LoginDate From Users Where UserID = @Id", connection);
            selectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = EmployeeID;
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            while(reader.Read())
            {
                name = reader.GetString(0);
                loginDate = reader.GetDateTime(1).ToString();
            }
            connection.Close();
            lblCurrentUser.Content = "Welcome " + name + " Login date: " + loginDate + ". Have a nice day :). \n";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtAddNotes.Clear();
            txtAddNotes.Focus();
        }
    }
}
