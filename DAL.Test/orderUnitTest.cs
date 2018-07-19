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
        public void Success()
        {
            order.itemlist.Add(idal.Getitembyid(1));
            order.itemlist[0].quantity = 1;
            Assert.False(odal.CreateOrder(order));
        }
        [Fact]
        public void Fail01()
        {
            order.itemlist.Add(idal.Getitembyid(1));
            order.itemlist[0].quantity = 0;
            Assert.False(odal.CreateOrder(order));
        }
        [Fact]
        public void getlastorderTest()
        {

            order.itemlist.Add(idal.Getitembyid(1));
            order.itemlist[0].quantity = 1;
            odal.CreateOrder(order);
            Assert.NotNull(odal.CreateOrder(null));
        }

        
    }
}