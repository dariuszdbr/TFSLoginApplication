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
            selectedUser = user;
            LoadData();
        }

        private void LoadData()
        {
            LinqManager.usersDataContext = new UserDatabaseDataContext();

            var LoginInfo = (from x in LinqManager.usersDataContext.Loginfos
                             where x.UserID.Equals(selectedUser.UserID)
                             select x);

            UserDataGrid.ItemsSource = LoginInfo;

            var userDetails = (from a in LinqManager.usersDataContext.Users
                               join b in LinqManager.usersDataContext.LoginDatas on a.UserID equals b.UserID
                               where a.UserID.Equals(selectedUser.UserID)
                               select new { a.UserID, a.Name, a.LastName, a.Role, a.DateOfEmployment, b.Login, b.Password, a.Status, });

            StackPanelDetails.DataContext = userDetails;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
