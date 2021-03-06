using System;
using System.Collections.Generic;
using DAL;
using Persitence;
using MySql.Data.MySqlClient;
using Xunit;
namespace DAL.Test
{
    public class orderDALUnitTest
    {
        private order order = new order();
        private itemDAL idal = new itemDAL();
        private orderDAL odal = new orderDAL();
        [Fact]
        public void order01()
        {
            order.itemlist.Add(idal.Getitembyid(1));
            order.itemlist[0].quantity = 1;
            Assert.True(odal.CreateOrder(order));
        }
        [Fact]
        public void order02()
        {
            order.itemlist.Add(idal.Getitembyid(1));
            order.itemlist[0].quantity = 0;
            Assert.False(odal.CreateOrder(order));
        }
    }
}