using System;
using DAL;
using Persitence;

namespace BL
{
    public class UserBL
    {
        private static UserDal use = new UserDal();
        public users Login(string use_name, string user_pass)
        {
            return use.Login(use_name,user_pass);
        } 
    }
}
