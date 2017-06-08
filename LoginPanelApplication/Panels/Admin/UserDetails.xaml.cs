using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace LoginPanelApplication.Panels
{
    /// <summary>
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails 
    {
        User _selectedUser;
       

        public UserDetails(User user)
        {
            InitializeComponent();
            SelectedUser(user);
            LoadData();
            checkRole();
        }

        private void SelectedUser(User user)
        {
            _selectedUser = (from x in LinqManager.usersDataContext.Users
                            where x.UserID.Equals(user.UserID)
                            select x).Single();
        }

        private void LoadData()
        {
            var userDetails = (from a in LinqManager.usersDataContext.Users
                               join b in LinqManager.usersDataContext.LoginDatas on a.UserID equals b.UserID
                               where a.UserID.Equals(_selectedUser.UserID)
                               select new { a.ImageId, a.UserID, a.Name, a.LastName, a.Role, a.DateOfEmployment, b.Login, b.Password, a.Status, });

 

            StackPanelDetails.DataContext = userDetails;
            ImageSource.DataContext = _selectedUser.ImageId;
        }

        private void checkRole()
        {
            if (!LinqManager.loggedInUser.Role) // if not admin
            {
                btnBlock.Visibility = Visibility.Collapsed;
                btnChangePassword.Visibility = Visibility.Collapsed;
            }
            if (LinqManager.loggedInUser.Role) // if admin
            {
                btnBlock.Visibility = Visibility.Visible;
            }
            if(_selectedUser.Status) btnBlock.Content = "Block the User";

            if(!_selectedUser.Status) btnBlock.Content = "Active";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnBlock_ClickAsync(object sender, RoutedEventArgs e)
        {
            
            if (btnBlock.Content.Equals("Block the User"))
            {
                
                var result = await this.ShowMessageAsync("Change user status", "Are you sure?",
                    MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                    _selectedUser.Status = false;
            }
            else _selectedUser.Status = true;

            LinqManager.usersDataContext.SubmitChanges();
            LoadData();
            checkRole();
        }

        private async void btnDailyRaport_ClickAsync(object sender, RoutedEventArgs e)
        {
            txtBlockRaport.Text = "";

            var dailyRaport = (from x in LinqManager.usersDataContext.Loginfos
                               where x.UserID.Equals(_selectedUser.UserID)
                               select x)
                              .Where(y => y.LoginDate.Value.Date.Equals(DateTime.Today))
                              .ToList();

            var dailyLoginInfo = new List<Loginfo>();
            var totalDailyHours = TimeSpan.Zero;
            if (dailyRaport.Any())
            {
                foreach (var row in dailyRaport)
                {
                    if (row.LogoutDate == null)
                        totalDailyHours += DateTime.Now - row.LoginDate.Value;
                    else
                    {
                        totalDailyHours += row.Hours.Value;
                    }
                }
                dailyLoginInfo.Add(new Loginfo { UserID = _selectedUser.UserID, LoginDate = DateTime.Now.Date, Hours = totalDailyHours });
            }
            else
                await this.ShowMessageAsync("Employee status", "The employee is absent today");

            UserDataGrid.ItemsSource = dailyRaport;
        }

        private void btnMonthlyRaport_Click(object sender, RoutedEventArgs e)
        {
            txtBlockRaport.Text = "";

            var monthlyRaport = (from x in LinqManager.usersDataContext.Loginfos
                                 where x.UserID.Equals(_selectedUser.UserID)
                                 orderby x.LoginDate
                                 select x)
                                 .Where(y => y.LoginDate.Value.Month.Equals(DateTime.Today.AddDays(-1).Month))
                                 .ToList();


            List < Loginfo > monthlyLoginRaport = new List<Loginfo>();

            for (int i = 0; i < monthlyRaport.Count(); i++)
            {            
                var day = monthlyRaport.ElementAt(i).LoginDate.Value;
                
                var totalDayHours = TimeSpan.Zero;
                do
                {
                    if(monthlyRaport.ElementAt(i).Hours == null)
                    {
                        totalDayHours += DateTime.Now - monthlyRaport.ElementAt(i).LoginDate.Value;
                    }

                    else
                    totalDayHours+= monthlyRaport.ElementAt(i).Hours.Value;

                    if (i >= monthlyRaport.Count() - 1)
                    {
                        i++;
                        break;
                    }
                   i++;               

                } while (day.DayOfYear.Equals(monthlyRaport.ElementAt(i).LoginDate.Value.DayOfYear));
                i--;
                monthlyLoginRaport.Add(new Loginfo() { UserID = _selectedUser.UserID, LoginDate = day.Date, Hours = totalDayHours });
            }
            UserDataGrid.ItemsSource = monthlyLoginRaport;           
        }

        private async void btnChangePassword_ClickAsync(object sender, RoutedEventArgs e)
        {
            txtPassword.Text = Password.Generate();
            _selectedUser.ChangePassword = true;
            _selectedUser.LoginDatas.First().Password = txtPassword.Text;
            try
            {
                LinqManager.usersDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Error","Something went wrong, " + ex.Message);
            }
        }
    }
}
            