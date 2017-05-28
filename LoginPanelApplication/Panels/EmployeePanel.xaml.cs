using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for EmployeePanel.xaml
    /// </summary>
    public partial class EmployeePanel : Page
    {
        public EmployeePanel()
        {
            InitializeComponent();
 
            SetLabelContent();
            txtBlock.Text = "Done Tasks:\n\n1.\n\n2.\n\n3.\n\n4.\n\n5.";
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LinqManager.logInfo.LogoutDate = DateTime.Now;
            LinqManager.logInfo.WorkingHours = LinqManager.logInfo.LogoutDate - LinqManager.logInfo.LoginDate;
            LinqManager.loggedInUser.Out = LinqManager.logInfo.LogoutDate;
            LinqManager.usersDataContext.SubmitChanges();

            PageSwitcher.Navigate(new LoginPanel());
        }

        private void SetLabelContent()
        {
            string loginDate = LinqManager.logInfo.LoginDate.ToString();
            string name = LinqManager.loggedInUser.Name;
            string login = LinqManager.usersDataContext.LoginDatas.Where(x => x.UserID.Equals(LinqManager.loggedInUser.UserID)).First().Login;

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
