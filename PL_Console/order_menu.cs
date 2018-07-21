using  System;
using System.Collections.Generic;
using Persitence;
using BL;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
namespace PL_console
{
    public class MenuOrder
    {
        itemBL ibl = new itemBL();
        orderBL obl = new orderBL();
        order order = new order();
        List<item> list = new List<item>();
        mainmenu main = new mainmenu();
        public void orders()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("                      Create Order");
                Console.WriteLine("=================================================================");
                Console.Write("Input Item ID : ");
                int itemid = valid(Console.ReadLine());
                item item = ibl.Getitembyid(itemid);
                list.Add(item);
                if(item != null)
                {
                    Console.WriteLine("=================================================================");
                    Console.WriteLine("|Item ID|Item name                   |Unit price    |Quantity   |");
                    Console.WriteLine("=================================================================");
                    foreach(var i in list)
                    {
                        Console.WriteLine("|{0,-7}|{1,-28}|{2,-14}|{3,-11}|",i.item_id,i.item_name,i.unit_price,i.quantity);
                        Console.WriteLine("=================================================================");
                    }
                    Console.Write("Input quantity : ");
                    int quantity = valid(Console.ReadLine());
                    obl.AddItem(itemid,quantity,order);
                    Console.Write("Add more item ? (Y/N) : ");
                    char add = valid2(Console.ReadLine());
                    if(add == 'n'||add == 'N')
                    {
                        Console.WriteLine("a. Pay and print bill \t\t b. Remove order");
                        Console.Write("Choice : ");
                        char pay = valid4(Console.ReadLine());
                        if(pay == 'b')
                        {
                            Console.Clear();
                            Console.WriteLine("Remove Order !\nPress enter to back");
                            Console.ReadKey();
                            main.orders();
                        }
                        else if(pay == 'a')
                        {
                            Console.Clear();
                            Console.WriteLine(obl.CreateOrder(order)? "Compelete" : "Fail");
                            Console.WriteLine("Press enter to print bill");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("                        LUXURY SHOP");
                            Console.WriteLine("=====================================================================");
                            Console.WriteLine("Store : Luxury");
                            Console.WriteLine("Address : 300 Kim Nguu, Ha Noi");
                            Console.WriteLine("Phone number : 01627344748");
                            order or = obl.getlastorder();
                            Console.WriteLine("Order ID : "+order.order_id);
                            Console.WriteLine("Date : "+or.order_date);
                            Console.WriteLine("Staff : "+or.user_name);
                            Console.WriteLine("------------------------------ Item List ----------------------------");
                            Console.WriteLine("=====================================================================");
                            Console.WriteLine("|Item                       |Unit price     |Quantity |Price        |");
                            Console.WriteLine("=====================================================================");
                            foreach(var x in obl.GetAllItemFromLastOrderDetail(order.order_id))
                            {
                                decimal money = x.price * x.amount;
                                Console.WriteLine("|{0,-27}|{1,-15}|{2,-9}|{3,-13}|",x.item_name,x.price,x.amount,money);
                                Console.WriteLine("=====================================================================");
                            }
                            Console.WriteLine("                                             TOTAL : "+obl.Money(order));
                            Console.WriteLine(".....................................................................");
                            Console.WriteLine("                             Thanks You");
                            Console.WriteLine("                           SEE YOU AIGAIN");
                            Console.WriteLine("Press enter to back to menu!");
                            Console.ReadKey();
                            main.orders();
                        }
                    }
                }   
            }
        }
        public int valid(string a)
        {
            Regex regex = new Regex("[0-9]");
            MatchCollection matchcollectionstr = regex.Matches(a);
            while ((matchcollectionstr.Count < a.Length) || (a==""))
            {
                Console.Write("Not valid ! please re-enter : ");
                a = Console.ReadLine();
                matchcollectionstr= regex.Matches(a);
            }
            return Convert.ToInt32(a);
        }
        public char valid2(string a)
        {
            Regex regex = new Regex("[a-zA-Z]");
            MatchCollection matchCollectionstr = regex.Matches(a);
            while ((matchCollectionstr.Count < a.Length) || (a != "y" && a != "Y" && a != "n" && a != "N") || (a.Length > 1)||a =="")
            {
                Console.Write("Not valid ! please re-enter : ");
                a = Console.ReadLine();
                matchCollectionstr = regex.Matches(a);
            }
            return Convert.ToChar(a);
        }
        public int valid3(string a)
        {
            Regex regex = new Regex("[0-9]");
            MatchCollection matchcollectionstr = regex.Matches(a);
            while ((matchcollectionstr.Count < a.Length) || (a=="")|| a != "1" ||a !="2")
            {
                Console.Write("Not valid ! please re-enter : ");
                a = Console.ReadLine();
                matchcollectionstr= regex.Matches(a);
            }
            return Convert.ToInt32(a);
        }
        public char valid4(string a)
        {
            Regex regex = new Regex("[a-zA-Z]");
            MatchCollection matchCollectionstr = regex.Matches(a);
            while ((matchCollectionstr.Count < a.Length) || (a != "a" && a != "A" && a != "b" && a != "B") || (a.Length > 1)||a =="")
            {
                Console.Write("Not valid ! please re-enter : ");
                a = Console.ReadLine();
                matchCollectionstr = regex.Matches(a);
            }
            return Convert.ToChar(a);
        }

    }
}