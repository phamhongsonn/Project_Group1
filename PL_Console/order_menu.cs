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
                    Console.Write("Input quantity : ");
                    int quantity = valid(Console.ReadLine());
                    decimal price = quantity * item.unit_price;
                    obl.AddItem(itemid,quantity,order);
                    Console.WriteLine("=================================================================");
                    Console.WriteLine("|Item                         |Unit price  |Quantity|Price      |");
                    Console.WriteLine("=================================================================");
                    foreach(var i in list)
                    {
                        Console.WriteLine("|{0,-29}|{1,-12}|{2,-8}|{3,-11}|",i.item_name,i.unit_price,quantity,price);
                        Console.WriteLine("=================================================================");
                    }
                    Console.Write("Add more item ? (Y/N) : ");
                    char add = Convert.ToChar(Console.ReadLine());
                    if(add == 'n'||add == 'N')
                    {
                        Console.WriteLine("1. Pay and print bill \t\t 2. Remove order");
                        Console.Write("Choice : ");
                        int pay = Convert.ToInt32(Console.ReadLine());
                        if(pay == 2)
                        {
                            Console.Clear();
                            Console.WriteLine("Remove Order !\nPress enter to back");
                            Console.ReadKey();
                            main.orders();
                        }
                        else if(pay == 1)
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
                            Console.WriteLine("Date : "+or.order_date);
                            Console.WriteLine("Staff : "+or.user_name);
                            Console.WriteLine("=====================================================================");
                            Console.WriteLine("|Item                       |Unit price     |Quantity |Price        |");
                            Console.WriteLine("=====================================================================");
                            foreach(var x in obl.GetAllItemFromLastOrderDetail(order.order_id))
                            {
                                decimal money = item.quantity * item.unit_price;
                                Console.WriteLine("|{0,-27}|{1,-15}|{2,-9}|{3,-13}|",x.item_name,x.price,x.amount,money);
                                Console.WriteLine("=====================================================================");
                            }
                            Console.WriteLine("                                  TOTAL : "+obl.Money(order));
                            Console.WriteLine(".................................................................");
                            Console.WriteLine("                       Thanks You");
                            Console.WriteLine("                     SEE YOU AIGAIN");
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
            Regex regex = new Regex("[1-9]");
            MatchCollection matchcollectionstr = regex.Matches(a);
            while ((matchcollectionstr.Count < a.Length) || (a==""))
            {
                Console.Write("Not valid ! please re-enter : ");
                a = Console.ReadLine();
                matchcollectionstr= regex.Matches(a);
            }
            return Convert.ToInt32(a);
        }

    }
}