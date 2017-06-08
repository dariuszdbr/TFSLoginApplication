using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

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
            //UpdateDatabase();
        }

        private void DisplayUsers()
        {
            LinqManager.usersDataContext = new UserDatabaseDataContext();

            DataGridManager.ItemsSource = (from a in LinqManager.usersDataContext.Users
                join b in LinqManager.usersDataContext.LoginDatas
                on a.UserID equals b.UserID
                orderby a.UserID
                select new
                {
                    a.ImageId,
                    a.UserID,
                    a.Name,
                    a.LastName,
                    a.Role,
                    a.In,
                    a.Out,
                    a.DateOfEmployment,
                    b.Login,
                    b.Password,
                    a.Status
                }).Skip(1);
        }

        //private static void UpdateDatabase()
        //{
        //    var update = from x in LinqManager.usersDataContext.Loginfos
        //                 select x;

        //    foreach (var row in update)
        //    {
        //        row.Hours = row.LogoutDate - row.LoginDate;
        //        LinqManager.usersDataContext.SubmitChanges();
        //    }
        //}

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Navigate(new AdminPanel());
        }

        private async void btnDeleteUser_ClickAsync(object sender, RoutedEventArgs e)
        {
            var selectedItem = DataGridManager.SelectedItem;
            try
            {
                var ID = selectedItem.GetType().GetProperty("UserID")?.GetValue(selectedItem);
                User user = LinqManager.usersDataContext.Users.First(x => x.UserID.Equals(ID));

                if (user != null)
                {
                    var metroWindow = (MetroWindow) Application.Current.MainWindow;
                    var result = await metroWindow.ShowMessageAsync("Confirmation",
                        "Are You sure?\nThere is no undo once data is deleted!",
                        MessageDialogStyle.AffirmativeAndNegative);
                    // MessageBoxResult result = MessageBox.Show("Are you sure?\nThere is no undo once data is deleted!", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageDialogResult.Affirmative)
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
                            await metroWindow.ShowMessageAsync("Error", "Something went wrong: " + ex.Message);
                        }
                        finally
                        {
                            DisplayUsers();
                        }
                    }
                    else
                    {
                        await metroWindow.ShowMessageAsync("Deletion status", "Deletion canceled");
                    }
                }
            }

            catch (Exception)
            {
                var metroWindow = (MetroWindow) Application.Current.MainWindow;
                await metroWindow.ShowMessageAsync("Deletion result",
                    "Please select first the user You want to Delete");
            }
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
            selectUserAsync();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            selectUserAsync();
        }

        public async void selectUserAsync()
        {
            try
            {
                var selectedItem = DataGridManager.SelectedItem;

                var ID = selectedItem.GetType().GetProperty("UserID")?.GetValue(selectedItem);
                User user = LinqManager.usersDataContext.Users.First(x => x.UserID.Equals(ID));

                UserDetails userdetails = new UserDetails(user);
                userdetails.ShowDialog();

                if (!userdetails.IsActive)
                    DisplayUsers();
            }
            catch (Exception)
            {
                var metroWindow = (MetroWindow)Application.Current.MainWindow;
                await metroWindow.ShowMessageAsync("Selection status", "Please select the Employee.");
            }
        }
    }

   
}
