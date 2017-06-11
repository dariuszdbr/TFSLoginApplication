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
        public void Should_Login_With_Admin_If_Login_And_Password_Are_Correct()
        {
            //--Arange
            MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            string login = "admin";
            string password = "admin";
            loginPanel.Login(login, password);

            var expected = new User()
            {
                UserID = 1000,
                Name = "Administrator",
                Role = true,
                Status = true,
            };

            //-- Act 
            var actual = LinqManager.loggedInUser;

            //-- Assert
            Assert.AreEqual(expected.UserID, actual.UserID);
            Assert.AreEqual(expected.Name, actual.Name);
            //Assert.AreEqual(expected.LastName, actual.LastName);
            //Assert.AreEqual(expected.FailedLoginCount, actual.FailedLoginCount);
            Assert.AreEqual(expected.Role, actual.Role);
            Assert.AreEqual(expected.Status, actual.Status);
            // Assert.AreEqual(expected.In, actual.In);
            //Assert.AreEqual(expected.Out, actual.Out);
        }

        [TestMethod]
        public void Shouldnt_Login_With_Admin_If_Login_And_Password_Are_Incorrect()
        {
            //-- Arange
            MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            string login = "Admin";
            string password = "Admin";
            loginPanel.Login(login, password);

            var expected = false;

            //-- Act 
            var actual = LinqManager.usersDataContext.LoginDatas
                .Any(x => x.Login.Equals(login) && x.Password.Equals(password));

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_Login_With_Employee_If_Login_And_Password_Are_Correct()
        {
            //-- Arange
            MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            string login = "ddąbrowski";
            string password = "Darek123";
            loginPanel.Login(login, password);

            var expected = new User()
            {
                UserID = 1001,
                Name = "Dariusz",
                LastName = "Dąbrowski",
                FailedLoginCount = 0,
                Role = false,
                Status = true,
            };
            //-- Act 
            var actual = LinqManager.loggedInUser;

            //-- Assert
            Assert.AreEqual(expected.UserID, actual.UserID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.FailedLoginCount, actual.FailedLoginCount);
            Assert.AreEqual(expected.Role, actual.Role);
            Assert.AreEqual(expected.Status, actual.Status);

        }

        [TestMethod]
        public void Shouldnt_Login_With_Employee_If_Login_And_Password_Are_Incorrect()
        {
            //-- Arange
            MainWindow Window = new MainWindow();
            LoginPanel loginPanel = new LoginPanel();
            string login = "ddąbrowski";
            string password = "darek123";


            var expected = false;

            //-- Act 
            var actual = LinqManager.usersDataContext.LoginDatas
                .Any(x => x.Login.Equals(login) && x.Password.Equals(password));

            //-- Assert
            Assert.AreEqual(expected, actual);

        }

    }
}
