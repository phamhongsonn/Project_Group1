using System;
using DAL;
using Persitence;
using MySql.Data.MySqlClient;
using Xunit;
namespace BL.Test
{
    public class OrderBLUnitTest
    {
        private order order = new order();
        private itemBL ibl = new itemBL();
        private orderBL obl = new orderBL();
        [Fact]
        public void CreateOrderSuccess()
        {
            order.itemlist.Add(ibl.Getitembyid(1));
            order.itemlist[0].quantity = 1;
            Assert.False(obl.CreateOrder(order));
        }
        [Fact]
        public void CreateOrderFail()
        {
            order.itemlist.Add(ibl.Getitembyid(2));
            order.itemlist[0].quantity = 0;
            Assert.False(obl.CreateOrder(order));
        }
        [Fact]
        public void Money()
        {
           obl.AddItem(2,2,order);
           obl.CreateOrder(order);
           Assert.Equal(300000,obl.Money(order));
        }
        [Fact]
        public void getlastorderTest()
        {
            order.itemlist.Add(ibl.Getitembyid(2));
            order.itemlist[0].quantity = 1;
            obl.CreateOrder(order);
            Assert.NotNull(obl.getlastorder());
        }
        
    }
}