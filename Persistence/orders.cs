using System;
using System.Collections.Generic;
namespace Persitence
{
    public class order
    {
        public int order_id {get;set;}
        public string user_name{get;set;}
        public DateTime order_date{get;set;}
        public List<item> itemlist {get;set;}
        public item this[int index]
        {
            get
            {
                if (itemlist == null || itemlist.Count == 0 || index < 0||itemlist.Count<index)
                return null;
                return itemlist[index];
            }
            set
            {
                if(itemlist == null) 
                itemlist = new List<item>();
                itemlist.Add(value);
            }
        }
        public order()
        {
            itemlist = new List<item>();
        }
        public override bool Equals(object obj)
        {
            if(obj is order)
            {
                return((order)obj).order_id.Equals(order_id);
            }
            return false;
        }
        public override int GetHashCode() 
        {
            return order_id.GetHashCode();
        }
    }
}