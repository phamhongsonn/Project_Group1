using System;
using System.Collections.Generic;
using Persitence;
using DAL;
namespace BL
{
    public class orderBL
    {
        private orderDAL odal = new orderDAL();
        private itemDAL idal = new itemDAL();
        public bool CreateOrder(order order)
        {
            bool result = odal.CreateOrder(order);
            return result;
        }
        public order GetLastOrder()
        {
            return odal.GetLastOrder(null);
        }
        public List<orderdetail> GetOrderDetail()
        {
            return odal.GetLastOrderDetail(null);
        }
        public bool AddItem(int itemid,int quantity,order order)
        {
            try
            {
                foreach(item x in order.itemlist)
                {
                    if(itemid == x.item_id)
                    {
                        x.quantity += quantity;
                        return true;
                    }
                }
                order.itemlist.Add(idal.Getitembyid(itemid));
                order.itemlist[order.itemlist.Count - 1].quantity = quantity;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        public decimal Money(order order)
        {
            decimal Money = 0;
            foreach(item i in order.itemlist)
            {
                Money = Money + i.quantity * i.unit_price;
            }
            return Money;
        }
    }
}