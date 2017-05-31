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
            if(!LinqManager.loggedInUser.Role) btnBlock.Visibility = Visibility.Collapsed;
            
            if(LinqManager.loggedInUser.Role) btnBlock.Visibility = Visibility.Visible;

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

            // Prepare new daily List
            var dailyLoginInfo = new List<Loginfo>();
            if(selectedUser.Role) // if admin
            dailyLoginInfo.Add(new Loginfo
            {
                UserID = dailyRaport.First().UserID,
                LoginDate = dailyRaport.First().LoginDate,
                LogoutDate = dailyRaport.Last().LogoutDate
            });
            else // if employee
            {
                dailyLoginInfo.Add(new Loginfo
                {
                    UserID = dailyRaport.First().UserID,
                    LoginDate = dailyRaport.First().LoginDate,
                    LogoutDate = DateTime.Now
                });
            }
            
            // Set Item Source
            UserDataGrid.ItemsSource = dailyLoginInfo;
            
            // Calculate daily raport
            TimeSpan totalDayHours = TimeSpan.Zero;
            try
            {
                var firstLoginOfDay = dailyLoginInfo.First().LoginDate.Value;
                var lastLogoutOfDay = dailyLoginInfo.First().LogoutDate.Value;
                totalDayHours = lastLogoutOfDay.Subtract(firstLoginOfDay);
                txtBlockRaport.Text+=(String.Format("{0} | Worked hours: {1} hours {2} minutes", lastLogoutOfDay.ToShortDateString(), totalDayHours.Hours, totalDayHours.Minutes) + Environment.NewLine);
            }
            catch (Exception)
            {
                MessageBox.Show("The employee is absent today");
            }          
        }

        private void btnMonthlyRaport_Click(object sender, RoutedEventArgs e)
        {
            txtBlockRaport.Text = "";

            var monthlyRaport = (from x in LinqManager.usersDataContext.Loginfos
                                 where x.UserID.Equals(selectedUser.UserID)
                                 orderby x.LoginDate ascending
                                 select x)
                                 .Where(y => y.LoginDate.Value.Month.Equals(DateTime.Today.Month))
                                 .ToList();

            // Prepare list for monthly raport
            List<Loginfo> monthlyLoginRaport = new List<Loginfo>();
            DateTime lastLogoutOfDay;
            DateTime firstLoginOfDay;

            // Fill the list with first/last login/logout of day
            for (int i = 0; i < monthlyRaport.Count(); i++)
            {
                firstLoginOfDay = monthlyRaport.ElementAt(i).LoginDate.Value;

                for (int j = i + 1; j < monthlyRaport.Count(); j++)
                {
                    if (j == monthlyRaport.Count() - 1)
                    {
                        if (selectedUser.Role)
                            lastLogoutOfDay = monthlyRaport.ElementAt(j).LogoutDate.Value;
                        else
                            lastLogoutOfDay = DateTime.Now;
                        monthlyLoginRaport.Add(new Loginfo() { UserID = selectedUser.UserID, LoginDate = firstLoginOfDay, LogoutDate = lastLogoutOfDay });
                        i = j;
                    }

                    else if (firstLoginOfDay.DayOfYear == monthlyRaport.ElementAt(j).LogoutDate.Value.DayOfYear || monthlyRaport.ElementAt(j).LogoutDate.Value == null)
                        continue;
                    
                    else
                    {
                        lastLogoutOfDay = monthlyRaport.ElementAt(j - 1).LogoutDate.Value;
                        monthlyLoginRaport.Add(new Loginfo() { UserID = selectedUser.UserID, LoginDate = firstLoginOfDay, LogoutDate = lastLogoutOfDay });                        
                        i = j-1;
                        break;
                    } 
                }                
            }

            // Set Item Source
            UserDataGrid.ItemsSource = monthlyLoginRaport;

            // Calculate and display monthly raport in read only RichTextBox 
            TimeSpan totalDayHours = TimeSpan.Zero;
            int counter = 0;
            foreach (var item in monthlyLoginRaport)
            {
                totalDayHours = item.LogoutDate.Value - item.LoginDate.Value;
                txtBlockRaport.Text +=(String.Format("{0} | Worked hours: {1} hours {2} minutes", item.LoginDate.Value.Date.ToShortDateString(), totalDayHours.Hours, totalDayHours.Minutes) + Environment.NewLine);
            }
           
        }
    }
}
            