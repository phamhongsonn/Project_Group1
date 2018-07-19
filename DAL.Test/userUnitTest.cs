using System;
using Xunit;
using DAL;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL.Test
{
    public class userDALUnitTest
    {
        private UserDal udal = new UserDal();
        [Fact]
        public void Success()
        {
            string username = "staff1";
            string pass = "staff1";
            
            users user = udal.Login(username,pass);
            Assert.NotNull(user);
        }
        [Fact]
        public void Fail01()
        {
            string username = "staff1";
            string pass = "123123";
            users user = udal.Login(username,pass);
            Assert.Null(user); 
        }
        [Fact]
        public void Fail02()
        {
            string username = null;
            string pass = null;
            users user = udal.Login(username,pass);
            Assert.Null(user); 
        }
        [Fact]
        public void Fail03()
        {
            string username = "%$@%&";
            string pass = "@$^&%#";
            users user = udal.Login(username,pass);
            Assert.Null(user); 
        }
    }
}