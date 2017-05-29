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
            selectedUser = (from x in LinqManager.usersDataContext.Users
                            where x.UserID.Equals(user.UserID)
                            select x).Single();
            LoadData();
            checkRole();
            CalculateHours();
        }

        private void LoadData()
        {
            //LinqManager.usersDataContext = new UserDatabaseDataContext();

            //var LoginInfo = (from x in LinqManager.usersDataContext.Loginfos
            //                 where x.UserID.Equals(selectedUser.UserID)
            //                 select x).Skip(1);

            //UserDataGrid.ItemsSource = LoginInfo;

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
        }

        public void CalculateHours()
        {
            
        }

        private void btnDailyRaport_Click(object sender, RoutedEventArgs e)
        {
            //var listLoginDate = (from x in LinqManager.usersDataContext.Loginfos
            //                     where x.UserID.Equals(selectedUser.UserID)
            //                     select x.LoginDate)
            //                     .Where(y => y.Value.Date == DateTime.Today)
            //                     .ToList();

            //var listLogoutDate = (from x in LinqManager.usersDataContext.Loginfos
            //                      where x.UserID.Equals(selectedUser.UserID)
            //                      select x.LogoutDate)
            //                      .Where(y => y.Value.Date.Equals(DateTime.Today))
            //                      .ToList();

            var dailyRaport = (from x in LinqManager.usersDataContext.Loginfos
                               where x.UserID.Equals(selectedUser.UserID)
                               select x)
                              .Where(y => y.LoginDate.Value.Date.Equals(DateTime.Today)).ToList();

            UserDataGrid.ItemsSource = dailyRaport;

            var firstLoginOfDay = dailyRaport.First().LoginDate.Value.Date;
            var lastLogoutOfDay = dailyRaport.Last().LogoutDate.Value.Date;

            TimeSpan totalDayHours = TimeSpan.Zero;
            try
            {
                string dateToSplit = lastLogoutOfDay.ToString();
                string[] split = dateToSplit.Split(' ');
                totalDayHours = lastLogoutOfDay.Subtract(firstLoginOfDay);
                Console.WriteLine("Day {0} | Worked hours = {1} hours {2} minutes", split[0], totalDayHours.Hours, totalDayHours.Minutes);
                lblSum.Content = String.Format("Day {0} | Worked hours = {1} hours {2} minutes", split[0], totalDayHours.Hours, totalDayHours.Minutes);
            }
            catch (Exception)
            {
                MessageBox.Show("The employee is absent today");
            }

            
        }

        private void btnMonthlyRaport_Click(object sender, RoutedEventArgs e)
        {
            var monthlyRaport = (from x in LinqManager.usersDataContext.Loginfos
                                 where x.UserID.Equals(selectedUser.UserID)
                                 select x)
                                 .Where(y => y.LoginDate.Value.Month.Equals(DateTime.Today.Month)).ToList();

            foreach (var item in monthlyRaport)
            {
                var firstLoginOfday = item.LoginDate.Value.Date.Day;
                while()
                
            }

            UserDataGrid.ItemsSource = monthlyRaport;
        }
    }
}
