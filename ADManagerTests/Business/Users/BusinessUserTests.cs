using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace ADManager.Tests
{
    [TestClass()]
    public class BusinessUserTests
    {
        private readonly string _userName;
        private readonly string _userPassword;
      
        public BusinessUserTests()
        {
            this._userName = "admin user";
            this._userPassword = "admin pass";
            Giris.UserName = _userName;
            Giris.UserPassword = _userPassword;
        }

        [TestMethod()]
        public void SearchUserTest()
        {
            int count = new BusinessUser().SearchUser("far").Count;
            Assert.AreEqual(2, count);
        }

        [TestMethod()]
        public void DisableUserTest()
        {
            string actualResponse = new BusinessUser().DisableUser("user.test2");
            string expectedresponse = "Zaten Pasif";
            Assert.AreEqual(expectedresponse, actualResponse);
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            new BusinessUser().DeleteUser("test.user2");
            bool actual = BusinessUser.testState;
            Assert.AreEqual(true, actual);

        }

        [TestMethod()]
        public void GetAllAdUsersTest()
        {
            var actuals = new BusinessUser().GetAllAdUsers();
            Assert.IsTrue(actuals.Rows.Count > 200);
        }

        [TestMethod()]
        public void CreateUserAccountTest()
        {
            UserFormInputs _userFormProperties = new UserFormInputs("test", "user6", "test.user6", "Deneme42..", true);
            new BusinessUser().CreateUserAccount(_userFormProperties);
            bool actual = BusinessUser.testState;
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddUserToAdminGroupTest()
        {
            var actual = new BusinessUser().AddUserToAdminGroup("test.user3", "Domain Admins");
            var expected = "İşlem Başarılı";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetUserGroupsTest()
        {
            IEnumerable<string> groupList = new BusinessUser().GetUserGroups("test.user3");
            bool actual = (groupList.Contains("Domain Admins"));
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void ResetUserPasswordTest()
        {
            string actual = new BusinessUser().ResetUserPassword("test.user3", "Adana18..");
            string expected = "Kullanıcı Parolası Değiştirildi";
            Assert.AreEqual(expected, actual);
        }
    }
}