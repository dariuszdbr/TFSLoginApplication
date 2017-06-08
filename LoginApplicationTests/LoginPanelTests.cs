using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoginPanelApplication.Panels;
using LoginPanelApplication;
using System.Linq;

namespace LoginApplicationTests
{
    [TestClass]
    public class LoginPanelTests
    {
        [TestMethod]
        public async void Should_Login_With_Admin_If_Login_And_Password_Are_CorrectAsync()
        {
            //-- Arange
            //MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            loginPanel.TestTxtLogin.Text = "admin";
            loginPanel.TestTxtPassword.Password = "admin";
            await loginPanel.LoginAsync();

            var expected = LinqManager.usersDataContext.LoginDatas
                .First(x => x.Login.Equals(loginPanel.TestTxtLogin.Text) && x.Password.Equals(loginPanel.TestTxtPassword.Password))
                .User;
            //-- Act 
            var actual = LinqManager.loggedInUser;

            //-- Assert
            Assert.AreEqual(expected.UserID, actual.UserID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.FailedLoginCount, actual.FailedLoginCount);
            Assert.AreEqual(expected.Role, actual.Role);
            Assert.AreEqual(expected.Status, actual.Status);
            Assert.AreEqual(expected.In, actual.In);
            Assert.AreEqual(expected.Out, actual.Out);

        }

        [TestMethod]
        public void Shouldnt_Login_With_Admin_If_Login_And_Password_Are_Incorrect()
        {
            //-- Arange
            //MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            loginPanel.TestTxtLogin.Text = "Admin";
            loginPanel.TestTxtPassword.Password = "Admin";
            loginPanel.LoginAsync();

            var expected = false;

            //-- Act 
            var actual = LinqManager.usersDataContext.LoginDatas
                .Any(x => x.Login.Equals(loginPanel.TestTxtLogin.Text) && x.Password.Equals(loginPanel.TestTxtPassword.Password));

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_Login_With_Employee_If_Login_And_Password_Are_Correct()
        {
            //-- Arange
            MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            loginPanel.TestTxtLogin.Text = "ddąbrowski";
            loginPanel.TestTxtPassword.Password = "Darek123";
            loginPanel.LoginAsync();

            var expected = LinqManager.usersDataContext.LoginDatas
                .Where(x => x.Login.Equals(loginPanel.TestTxtLogin.Text) && x.Password.Equals(loginPanel.TestTxtPassword.Password))
                .First().User;
            //-- Act 
            var actual = LinqManager.loggedInUser;

            //-- Assert
            Assert.AreEqual(expected.UserID, actual.UserID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.FailedLoginCount, actual.FailedLoginCount);
            Assert.AreEqual(expected.Role, actual.Role);
            Assert.AreEqual(expected.Status, actual.Status);
            Assert.AreEqual(expected.In, actual.In);
            Assert.AreEqual(expected.Out, actual.Out);

        }

        [TestMethod]
        public void Shouldnt_Login_With_Employee_If_Login_And_Password_Are_Incorrect()
        {
            //-- Arange
            MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            loginPanel.TestTxtLogin.Text = "ddąbrowski";
            loginPanel.TestTxtPassword.Password = "darek123";
            loginPanel.LoginAsync();

            var expected = false;

            //-- Act 
            var actual = LinqManager.usersDataContext.LoginDatas
                .Any(x => x.Login.Equals(loginPanel.TestTxtLogin.Text) && x.Password.Equals(loginPanel.TestTxtPassword.Password));

            //-- Assert
            Assert.AreEqual(expected, actual);

        }

    }
}
