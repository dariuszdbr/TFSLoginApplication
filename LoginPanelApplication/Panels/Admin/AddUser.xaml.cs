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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser
    {
        public AddUser()
        {
            InitializeComponent();
            txtName.Focus();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length < 1 || txtLastName.Text.Length < 1)
            {
                MessageBox.Show("Please fill Name and the Last Name.", "Name and last name required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtName.Focus();
            }
            else
            {
                var role = false;
                var password = Password.Generate();
                var login = GenerateLogin(txtName.Text, txtLastName.Text);

                User newUser = new User()
                {
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    Status = true, //active
                    DateOfEmployment = DateTime.Now,
                    Role = role,
                    ChangePassword = true,
                };

                LoginData newUserLoginAndPassword = new LoginData()
                {
                    Login = login,
                    Password = password
                };

                Loginfo newUserLoginfo = new Loginfo()
                {
                    LoginDate = null,
                    LogoutDate = null
                };

                newUser.Loginfos.Add(newUserLoginfo);
                newUser.LoginDatas.Add(newUserLoginAndPassword);
                LinqManager.usersDataContext.Users.InsertOnSubmit(newUser);

                try
                {
                    LinqManager.usersDataContext.SubmitChanges();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if(MessageBox.Show("User has been sucesfully added.") == MessageBoxResult.OK)
                this.Close();
            }
             
        }

        private string GenerateLogin(string userName, string lastName)
        {
            bool acces = false;
            string login = String.Empty;
            int number = 0;
            do
            {
                login = (number > 0) ? userName[0].ToString().ToLower() + lastName.ToLower() + number : userName[0].ToString().ToLower() + lastName.ToLower();

                LinqManager.usersDataContext = new UserDatabaseDataContext();

                var checkUsers = (LinqManager.usersDataContext.LoginDatas.Any(user => user.Login == login));

                if (checkUsers) number++;

                else acces = true;

            } while (!acces);

            return login;
        }
    }
}
