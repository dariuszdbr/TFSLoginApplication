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
        public EmployeePanel()
        {
            InitializeComponent();
 
            SetLabelContent();
            txtBlock.Text = "Done Tasks:\n\n1.\n\n2.\n\n3.\n\n4.\n\n5.";
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LinqManager.loggedInUser.LogoutDate = DateTime.Now;
            LinqManager.loggedInUser.WorkingTime = LinqManager.loggedInUser.LogoutDate - LinqManager.loggedInUser.LoginDate;
            LinqManager.usersDataContext.SubmitChanges();

            PageSwitcher.Navigate(new LoginPanel());
        }

        private void SetLabelContent()
        {
            string loginDate = LinqManager.loggedInUser.LoginDate.ToString();
            string name = LinqManager.loggedInUser.Name;
            string login = LinqManager.loggedInUser.Login;

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
