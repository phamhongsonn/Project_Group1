using System;
using Xunit;
using DAL;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL.Test
{
    public class itemDALUnitTest
    {
        private itemDAL idal = new itemDAL();
        [Fact]
        public void item01()
        {
            int id = 1;
            item item = idal.Getitembyid(id);
            Assert.NotNull(item);
        }
        [Fact]
        public void item02()
        {
            char id = 'a';
            item item= idal.Getitembyid(id);
            Assert.Null(item); 
        }
        [Fact]
        public void item03()
        {
            int id = 0;
            item item= idal.Getitembyid(id);
            Assert.Null(item); 
        }
        [Fact]
        public void item04()
        {
            int id = -1;
            item item= idal.Getitembyid(id);
            Assert.Null(item); 
        }
    }
}