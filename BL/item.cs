using System;
using DAL;
using Persitence;
namespace BL
{
    public class itemBL
    {
        private static itemDAL item = new itemDAL();
        public item Getitembyid(int Item_id)
        {
            return item.Getitembyid(Item_id);
        }
    }
}