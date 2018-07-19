using System;
using Persitence;
namespace Persitence
{
    public class orderdetail
    {
        public int order_id{get;set;}
        public int item_id{get;set;}
        public string item_name{get;set;}
        public int amount{get;set;}
        public decimal price{get;set;}
    }
}