using MahApps.Metro.Controls.Dialogs;
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
using MahApps.Metro.Controls;

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

        private async void ShowMessageBoxAsync(string title, string content)
        {

            await this.ShowMessageAsync(title, content);
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Length < 1 || txtLastName.Text.Length < 1)
            {
                ShowMessageBoxAsync("Name and last name required","Please fill Name and the Last Name.");
                txtName.Focus();
            }
            else
            {
                var role = false;
                var password = Password.Generate();
                var login = GenerateLogin(txtName.Text, txtLastName.Text);

                var rand = new Random();

                User newUser = new User()
                {
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    Status = true, //active
                    DateOfEmployment = DateTime.Now,
                    Role = role,
                    ChangePassword = true,
                    ImageId = rand.Next(0,2)
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
                    ShowMessageBoxAsync("Error", "Something went wrong: " + ex.Message);
                }

                this.Close();
                
                ShowMessageBoxAsync("Employee Added", "User has been sucesfully added");   
            }
             
        }

        private string GenerateLogin(string userName, string lastName)
        {
            var acces = false;
            string login;
            var number = 0;
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
