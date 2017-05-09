using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoginPanelApplication.Panels;
using LoginPanelApplication;

namespace LoginApplicationTests
{
    [TestClass]
    public class LoginApplicationTests
    {
        [TestMethod]
        public void Should_Login_With_Admin_If_Login_And_Password_Are_Correct()
        {
            MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            loginPanel.TestTxtLogin.Text = "admin";
            loginPanel.TestTxtPassword.Password = "admin";
            loginPanel.GetSqlLogin();

            var expected = 1; // admin Id

            Assert.AreEqual(expected, loginPanel.TestUserID);
        }

        [TestMethod]
        public void Shouldnt_Login_With_Admin_If_Login_And_Password_Are_Incorrect()
        {
            MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            loginPanel.TestTxtLogin.Text = "Admin";
            loginPanel.TestTxtPassword.Password = "Admin";
            loginPanel.GetSqlLogin();

            int expected = 0; // ID

            Assert.AreEqual(expected, loginPanel.TestUserID);
        }

        [TestMethod]
        public void Should_Login_With_Employee_If_Login_And_Password_Are_Correct()
        {
            MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            loginPanel.TestTxtLogin.Text = "ddąbrowski";
            loginPanel.TestTxtPassword.Password = "darek123";
            loginPanel.GetSqlLogin();

            var expected = 2; // employee Id

            Assert.AreEqual(expected, loginPanel.TestUserID);
        }

        [TestMethod]
        public void Shouldnt_Login_With_Employee_If_Login_And_Password_Are_Incorrect()
        {
            MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            loginPanel.TestTxtLogin.Text = "ddąbrowski";
            loginPanel.TestTxtPassword.Password = "Darek123";
            loginPanel.GetSqlLogin();

            int expected = 0; // ID

            Assert.AreEqual(expected, loginPanel.TestUserID);
        }

    }
}
