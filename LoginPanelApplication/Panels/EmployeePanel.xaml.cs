using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for EmployeePanel.xaml
    /// </summary>
    public partial class EmployeePanel : Page
    {

        int EmployeeID;
        DateTime logout;
        DateTime login;

        public EmployeePanel(int userid)
        {
            InitializeComponent();
            EmployeeID = userid;
            SetLabelContent();
            txtBlock.Text = "Done Tasks:\n\n1.\n\n2.\n\n3.\n\n4.\n\n5.";
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            string select = "SELECT LoginDate, WorkingTime From Users WHERE UserID = @Id";
            SqlManager.Connection.Open();
            SqlManager.dataAdapter = new SqlDataAdapter();
            SqlManager.dataAdapter.SelectCommand = new SqlCommand(select, SqlManager.Connection);
            SqlManager.dataAdapter.SelectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = EmployeeID;
            SqlManager.dataTable = new DataTable();
            SqlManager.dataAdapter.Fill(SqlManager.dataTable);

            string update = "UPDATE Users SET LogoutDate = @LogoutDate, WorkingTime = @WorkingTime WHERE UserID = @Id";
            SqlManager.updateCommand = new SqlCommand(update, SqlManager.Connection);
            SqlManager.updateCommand.Parameters.Add("@Id", SqlDbType.Int).Value = EmployeeID;
            SqlManager.updateCommand.Parameters.AddWithValue("@LogoutDate", logout = DateTime.Now);
            login = Convert.ToDateTime(SqlManager.dataTable.Rows[0]["LoginDate"]);
            TimeSpan workingTime = logout - login;
            SqlManager.updateCommand.Parameters.Add("@WorkingTime", SqlDbType.Time).Value = workingTime;
            SqlManager.updateCommand.ExecuteNonQuery();
            SqlManager.Connection.Close();

            PageSwitcher.Navigate(new LoginPanel());
        }

        private void SetLabelContent()
        {
            string loginDate = String.Empty;
            string name = String.Empty;
            string login = String.Empty;

            SqlManager.selectCommand = new SqlCommand("Select Name,LoginDate,Login From Users Where UserID = @Id", SqlManager.Connection);
            SqlManager.selectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = EmployeeID;
            //SqlManager.Connection.Open();
            SqlManager.reader = SqlManager.selectCommand.ExecuteReader();

            while(SqlManager.reader.Read())
            {
                name = SqlManager.reader.GetString(0);
                loginDate = SqlManager.reader.GetDateTime(1).ToString();
                login = SqlManager.reader.GetString(2);
            }
            SqlManager.Connection.Close();
            lblCurrentUser.Content = "Welcome " + name + " (" + login + ")   " + " Login date: " + loginDate + ". Have a nice day :). \n";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtAddNotes.Clear();
            txtAddNotes.Focus();
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
    }
}
