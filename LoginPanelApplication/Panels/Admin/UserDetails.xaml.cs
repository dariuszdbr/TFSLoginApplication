using System;
using System.Collections.Generic;
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
        User selectedUser;
        bool changePassword = false;

        public UserDetails(User user)
        {
            InitializeComponent();
            SelectedUser(user);
            LoadData();
            checkRole();
        }

        private void SelectedUser(User user)
        {
            selectedUser = (from x in LinqManager.usersDataContext.Users
                            where x.UserID.Equals(user.UserID)
                            select x).Single();
        }

        private void LoadData()
        {
            var userDetails = (from a in LinqManager.usersDataContext.Users
                               join b in LinqManager.usersDataContext.LoginDatas on a.UserID equals b.UserID
                               where a.UserID.Equals(selectedUser.UserID)
                               select new { a.UserID, a.Name, a.LastName, a.Role, a.DateOfEmployment, b.Login, b.Password, a.Status, });

            var update = from x in LinqManager.usersDataContext.Loginfos
                         select x;

            foreach (var row in update)
            {
                row.Hours = row.LogoutDate - row.LoginDate;
                LinqManager.usersDataContext.SubmitChanges();
            }

            StackPanelDetails.DataContext = userDetails;
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
            if(selectedUser.Status) btnBlock.Content = "Block the User";

            if(!selectedUser.Status) btnBlock.Content = "Active";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBlock_Click(object sender, RoutedEventArgs e)
        {
            if (btnBlock.Content.Equals("Block the User"))
            {
                if (MessageBox.Show("Are you sure?", "Change user status", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    selectedUser.Status = false;
            }
            else selectedUser.Status = true;

            LinqManager.usersDataContext.SubmitChanges();
            LoadData();
            checkRole();
        }

        private void btnDailyRaport_Click(object sender, RoutedEventArgs e)
        {
            txtBlockRaport.Text = "";

            var dailyRaport = (from x in LinqManager.usersDataContext.Loginfos
                               where x.UserID.Equals(selectedUser.UserID)
                               select x)
                              .Where(y => y.LoginDate.Value.Date.Equals(DateTime.Today))
                              .ToList();

            var dailyLoginInfo = new List<Loginfo>();
            TimeSpan totalDailyHours = TimeSpan.Zero;
            if (dailyRaport.Count() > 0)
            {
                foreach (var row in dailyRaport)
                {
                    if (row.LogoutDate == null)
                        totalDailyHours += DateTime.Now - row.LoginDate.Value;
                    else
                    totalDailyHours += row.Hours.Value;
                }
                dailyLoginInfo.Add(new Loginfo { UserID = selectedUser.UserID, LoginDate = DateTime.Now.Date, Hours = totalDailyHours });
            }
            else
                MessageBox.Show("The employee is absent today");

            UserDataGrid.ItemsSource = dailyLoginInfo;
        }

        private void btnMonthlyRaport_Click(object sender, RoutedEventArgs e)
        {
            txtBlockRaport.Text = "";

            var monthlyRaport = (from x in LinqManager.usersDataContext.Loginfos
                                 where x.UserID.Equals(selectedUser.UserID)
                                 orderby x.LoginDate ascending
                                 select x)
                                 .Where(y => y.LoginDate.Value.Month.Equals(DateTime.Today.AddDays(-1).Month))
                                 .ToList();


            List < Loginfo > monthlyLoginRaport = new List<Loginfo>();
            TimeSpan totalDayHours;

            for (int i = 0; i < monthlyRaport.Count(); i++)
            {            
                var day = monthlyRaport.ElementAt(i).LoginDate.Value;
                
                totalDayHours = TimeSpan.Zero;
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
                monthlyLoginRaport.Add(new Loginfo() { UserID = selectedUser.UserID, LoginDate = day.Date, Hours = totalDayHours });
            }
            UserDataGrid.ItemsSource = monthlyLoginRaport;           
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            txtPassword.Text = Password.Generate();
            selectedUser.ChangePassword = true;
            selectedUser.LoginDatas.First().Password = txtPassword.Text;
            try
            {
                LinqManager.usersDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong, " + ex.Message, "Error");
            }
        }
    }
}
            