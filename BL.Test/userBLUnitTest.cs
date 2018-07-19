using System;
using Xunit;
using DAL;
using Persitence;
using MySql.Data.MySqlClient;
namespace BL.Test
{
    public class userBLUnitTest
    {
        private UserBL ubl = new UserBL();

        [Fact]
        public void Success()
        {
            string username = "staff1";
            string pass = "staff1";
            users user = ubl.Login(username,pass);
            Assert.NotNull(user);
        }
        [Fact]
        public void fail()
        {
            string username = "@#%#$";
            string pass = "@#$#@T";
            users user = ubl.Login(username,pass);
            Assert.NotNull(user);
        }
        
    }
}