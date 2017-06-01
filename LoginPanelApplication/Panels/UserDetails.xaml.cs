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
    public partial class UserDetails : Window
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

                    totalDailyHours += row.Hours.Value;
                }
                dailyLoginInfo.Add(new Loginfo { UserID = selectedUser.UserID, LoginDate = DateTime.Now.Date, Hours = totalDailyHours });
                txtBlockRaport.Text += (String.Format("{0} | Worked hours: {1} hours {2} minutes", selectedUser.In.Value.ToShortDateString(), totalDailyHours.Hours, totalDailyHours.Minutes) + Environment.NewLine);
            }
            else
                MessageBox.Show("The employee is absent today");

            UserDataGrid.ItemsSource = dailyLoginInfo;
        //    try
        //    {
        //        // Prepare new daily List
                
        //        if (LinqManager.loggedInUser.Role) // if admin
        //            dailyLoginInfo.Add(new Loginfo
        //            {
        //                UserID = dailyRaport.First().UserID,
        //                LoginDate = dailyRaport.First().LoginDate,
        //                LogoutDate = dailyRaport.Last().LogoutDate
        //            });
        //        else // if employee
        //        {
        //            dailyLoginInfo.Add(new Loginfo
        //            {
        //                UserID = dailyRaport.First().UserID,
        //                LoginDate = dailyRaport.First().LoginDate,
        //                LogoutDate = DateTime.Now
        //            });
        //        }

        //        // Set Item Source
        //        

        //        // Calculate daily raport
        //        TimeSpan totalDayHours = TimeSpan.Zero;

        //        var firstLoginOfDay = dailyLoginInfo.First().LoginDate.Value;
        //        var lastLogoutOfDay = dailyLoginInfo.First().LogoutDate.Value;
        //        totalDayHours = lastLogoutOfDay.Subtract(firstLoginOfDay);
        //        txtBlockRaport.Text += (String.Format("{0} | Worked hours: {1} hours {2} minutes", lastLogoutOfDay.ToShortDateString(), totalDayHours.Hours, totalDayHours.Minutes) + Environment.NewLine);
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("The employee is absent today");
        //    }
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

            //var update = from x in LinqManager.usersDataContext.Loginfos
            //             select x;

            //foreach (var row in update)
            //{
            //    if (row.LoginDate == null && row.LogoutDate == null)
            //        continue;

            //    else row.Hours = row.LogoutDate - row.LoginDate;
            //    LinqManager.usersDataContext.SubmitChanges();
            //}

            // Prepare list for monthly raport
            List < Loginfo > monthlyLoginRaport = new List<Loginfo>();
            //DateTime lastLogoutOfDay;
            //DateTime firstLoginOfDay;
            TimeSpan totalDayHours;

            for (int i = 0; i < monthlyRaport.Count(); i++)
            {
                
                var day = monthlyRaport.ElementAt(i).LoginDate.Value;
                
                totalDayHours = TimeSpan.Zero;
                do
                {
                    totalDayHours+= monthlyRaport.ElementAt(i).Hours.Value;
                    if (i >= monthlyRaport.Count() - 1)
                    {
                        i++;
                        break;
                    }
                   i++;               

                } while (day.DayOfYear.Equals(monthlyRaport.ElementAt(i).LoginDate.Value.DayOfYear));
                i--;
                monthlyLoginRaport.Add(new Loginfo() { UserID = selectedUser.UserID, LoginDate = day.Date/*, LogoutDate = monthlyRaport.ElementAt(i).LoginDate.Value, */,Hours = totalDayHours });
                txtBlockRaport.Text += (String.Format("{0} | Worked hours: {1} hours {2} minutes", monthlyLoginRaport.Last().LoginDate.Value.Date.ToShortDateString(), totalDayHours.Hours, totalDayHours.Minutes) + Environment.NewLine);
                
            }

            UserDataGrid.ItemsSource = monthlyLoginRaport;
            // Fill the list with first/last login/logout of day
            //for (int i = 0; i < monthlyRaport.Count(); i++)
            //{
            //    firstLoginOfDay = monthlyRaport.ElementAt(i).LoginDate.Value;

            //    for (int j = i + 1; j < monthlyRaport.Count(); j++)
            //    {
            //        if (j == monthlyRaport.Count() - 1)
            //        {
            //            if (selectedUser.Role)
            //                lastLogoutOfDay = monthlyRaport.ElementAt(j).LogoutDate.Value;
            //            else
            //                lastLogoutOfDay = DateTime.Now;
            //            monthlyLoginRaport.Add(new Loginfo() { UserID = selectedUser.UserID, LoginDate = firstLoginOfDay, LogoutDate = lastLogoutOfDay });
            //            i = j;
            //        }

            //        else if (firstLoginOfDay.DayOfYear == monthlyRaport.ElementAt(j).LogoutDate.Value.DayOfYear || monthlyRaport.ElementAt(j).LogoutDate.Value == null)
            //            continue;
                    
            //        else
            //        {
            //            lastLogoutOfDay = monthlyRaport.ElementAt(j - 1).LogoutDate.Value;
            //            monthlyLoginRaport.Add(new Loginfo() { UserID = selectedUser.UserID, LoginDate = firstLoginOfDay, LogoutDate = lastLogoutOfDay });                        
            //            i = j-1;
            //            break;
            //        } 
            //    }
            //}

            // Set Item Source
            

            // Calculate and display monthly raport
            //TimeSpan totalDayHours = TimeSpan.Zero;
            //foreach (var item in monthlyLoginRaport)
            //{
            //    //totalDayHours = item.LogoutDate.Value - item.LoginDate.Value;
            //    txtBlockRaport.Text += (String.Format("{0} | Worked hours: {1} hours {2} minutes", item.LoginDate.Value.Date.ToShortDateString(), totalDayHours, totalDayHours.Minutes) + Environment.NewLine);
            //}
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            
            txtPassword.IsReadOnly = false;
            if (!changePassword)
            {
                btnChangePassword.Content = "Save";
                txtPassword.SelectAll();
 
                if (txtPassword.Text != selectedUser.LoginDatas.Where( x => x.UserID.Equals(selectedUser.UserID)).First().Password)
                {
                    MessageBox.Show("zmiana hasła");
                }
                changePassword = true;
            }
            else if (changePassword)
            {
                btnChangePassword.Content = "Change";
                changePassword = false;
            }

        }
    }
}
            