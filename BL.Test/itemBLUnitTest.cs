using System;
using Persitence;
using MySql.Data.MySqlClient;
using Xunit;
namespace BL.Test
{
    public class ItemBLUnitTest
    {
        itemBL ibl = new itemBL();
        [Fact]
        
        public void success()
        {
            int id = 2;
            Assert.NotNull(ibl.Getitembyid(id));
        }
        [Fact]
        public void fail()
        {
            int id = -1;
            Assert.Null(ibl.Getitembyid(id));
        }
    }
}