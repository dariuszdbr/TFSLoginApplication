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
            DataGridManager.ItemsSource = LinqManager.usersDataContext.Users;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text.Length < 1 || txtLastName.Text.Length < 1 || (permissionAdmin.IsChecked == false && permissionEmployee.IsChecked == false) || (txtPassword.Text.Length > 0 && txtPassword.Text.Length < 8 ))
            {
                MessageBox.Show("Please fill Name, Last Name, and select permission\nRemember, the password can be automatically  generated only if you leave the Password field blank. " +
                                "Otherwise the password should contain at least 8 characters with one uppercase letter and one digit",
                                "Name, Surname and Permission Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtName.Focus();
                btnClear_Click(sender,e);
            }
            else
            {
                var status = (permissionAdmin.IsChecked == true) ? true : false;
                var password = (IsPasswordCorrect(txtPassword.Text)) ? txtPassword.Text : Password.Generate();
                var login = GenerateLogin(txtName.Text, txtLastName.Text);

                User newUser = new User()
                {
                    Name = txtName.Text,
                    LastName = txtLastName.Text,
                    Login = login,
                    Password = password,
                    Status = status,
                    LoginDate = null,
                    LogoutDate = null,
                    WorkingTime = null,
                    DateOfEmployment = DateTime.Now
                };

                LinqManager.usersDataContext.Users.InsertOnSubmit(newUser);
                try
                {
                    LinqManager.usersDataContext.SubmitChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                DisplayUsers();
            }

            btnClear_Click(sender, e);
        }

        public bool IsPasswordCorrect(string password)
        {
            byte IsOneUppercaseLetter = 0;
            byte IsOneDigit = 0;
            byte isOneLowercaseLetter = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] >= 65 && password[i] <= 90)
                    IsOneUppercaseLetter++;

                if (password[i] >= 40 && password[i] <= 57)
                    IsOneDigit++;

                if (password[i] >= 99 && password[i] <= 122)
                    isOneLowercaseLetter++;

                if (IsOneDigit > 0 && IsOneUppercaseLetter > 0 && isOneLowercaseLetter > 0) return true;
            }
            return (IsOneUppercaseLetter > 0 && IsOneDigit > 0 && isOneLowercaseLetter > 0);
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

                var checkUsers = (LinqManager.usersDataContext.Users.Where( user => user.Login == login ));

                if (checkUsers.Count() > 0)
                {
                    number++;
                }

                else
                {
                    acces = true;
                }

            } while (!acces);

            return login;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Navigate(new AdminPanel());
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            User user = DataGridManager.SelectedItem as User;

            if (user != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?\nThere is no undo once data is deleted!", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        LinqManager.usersDataContext.Users.DeleteOnSubmit(user);
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

        private void DataGridManager_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            User user = DataGridManager.SelectedItem as User;

            txtName.IsReadOnly = true;
            txtLastName.IsReadOnly = true;
            txtName.Text = user.Name;
            txtLastName.Text = user.LastName;
            txtPassword.Text = user.Password;

            txtPassword.Focus();
            txtPassword.SelectAll();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            User user = DataGridManager.SelectedItem as User;

            if (user != null)
            {
                if (txtPassword.Text.Length < 8 && !IsPasswordCorrect(txtPassword.Text))
                {
                    MessageBox.Show("The the password should contain at least 8 characters with one uppercase letter and one digit");
                    txtPassword.Focus();
                    txtPassword.SelectAll();
                }
                else
                {
                    user.Password = IsPasswordCorrect(txtPassword.Text) ? txtPassword.Text : Password.Generate();
                    LinqManager.usersDataContext.SubmitChanges();

                    if (MessageBox.Show("Record(s) has been sucessfully updated", "Update report", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                    {
                        DisplayUsers();
                        btnClear_Click(sender, e);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please press the mouse button twice on the selected user in the table and then make changes.", "Update report", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtLastName.IsReadOnly = false;
            txtName.IsReadOnly = false;
            txtLastName.Clear();
            txtName.Clear();
            txtPassword.Clear();
            txtName.Focus();
        }
    }

    public class Password
    {

        static Random lowerCase = new Random();
        static Random upperCase = new Random();
        static Random digit = new Random();
        static Random index = new Random();
        static string password;

        public static string Generate()
        {
            password = String.Empty;
            do
            {
                password += Convert.ToChar(upperCase.Next(65, 91));
                password += digit.Next(1, 10);
                password += Convert.ToChar(lowerCase.Next(97, 123));
                 
            } while (password.Length < 8);

            return password;
        }
    }
}
