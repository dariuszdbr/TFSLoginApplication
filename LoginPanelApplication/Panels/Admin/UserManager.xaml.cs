using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for UserManager.xaml
    /// </summary>
    public partial class UserManager : Page
    {

        public UserManager()
        {
            InitializeComponent();
            DisplayUsers();
        }

        private void DisplayUsers()
        {
            LinqManager.usersDataContext = new UserDatabaseDataContext();
            var Join = (from a in LinqManager.usersDataContext.Users
                        join b in LinqManager.usersDataContext.LoginDatas
                        on a.UserID equals b.UserID
                        select  new { a.UserID, a.Name, a.LastName, a.Role, a.In, a.Out, a.DateOfEmployment, b.Login, b.Password, a.Status}).Skip(1);

            DataGridManager.ItemsSource = Join;           
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Navigate(new AdminPanel());
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DataGridManager.SelectedItem;
            var ID = selectedItem.GetType().GetProperty("UserID").GetValue(selectedItem);
            User user = LinqManager.usersDataContext.Users.Where(x => x.UserID.Equals(ID)).First();

            if (user != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?\nThere is no undo once data is deleted!", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var deleteLoginfos = from x in LinqManager.usersDataContext.Loginfos
                                         where x.UserID.Equals(user.UserID)
                                         select x;

                    foreach (var row in deleteLoginfos)
                    {
                        LinqManager.usersDataContext.Loginfos.DeleteOnSubmit(row);
                    }

                    LinqManager.usersDataContext.LoginDatas.DeleteOnSubmit(user.LoginDatas.First());
                    LinqManager.usersDataContext.Users.DeleteOnSubmit(user);

                    try
                    {
                        LinqManager.usersDataContext.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Something went wrong: " + ex.Message);
                    }
                    finally
                    {
                        DisplayUsers();
                    }
                }
                else
                {
                    MessageBox.Show("Deletion canceled");
                }
            }
            else
                MessageBox.Show("Please select first the user You want to Delete", "Delete result", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser add = new AddUser();
            add.ShowDialog();
            if(!add.IsActive)
            DisplayUsers();
        }

        private void DataGridManager_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            selectUser();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            selectUser();
        }

        public void selectUser()
        {
            try
            {
                var selectedItem = DataGridManager.SelectedItem;

                var ID = selectedItem.GetType().GetProperty("UserID").GetValue(selectedItem);
                User user = LinqManager.usersDataContext.Users.Where(x => x.UserID.Equals(ID)).First();
                LoginData loginData = LinqManager.usersDataContext.LoginDatas.Where(x => x.UserID.Equals(ID)).First();

                UserDetails userdetails = new UserDetails(user);
                userdetails.ShowDialog();

                if (!userdetails.IsActive)
                    DisplayUsers();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select the Employee.");
            }           
        }    
    }

   
}
