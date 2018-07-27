using System;
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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("                      Create Order");
                Console.WriteLine("=================================================================");
                Console.Write("Input Item ID : ");
                int itemid = valid(Console.ReadLine());
                item item = ibl.Getitembyid(itemid);
                list.Add(item);
                if (item != null)
                {
                    Console.WriteLine("=====================================================");
                    Console.WriteLine("|Item ID|Item name                   |Unit price    |");
                    Console.WriteLine("=====================================================");
                    foreach (var i in list)
                    {
                        Console.WriteLine("|{0,-7}|{1,-28}|{2,-14}|", i.item_id, i.item_name,moneyformat(i.unit_price));
                        Console.WriteLine("=====================================================");
                    }
                    Console.Write("Input quantity : ");
                    int quantity = valid(Console.ReadLine());
                    if (quantity > item.quantity)
                    {
                        list.Remove(item);
                        Console.Clear();
                        Console.WriteLine("Only have" + item.quantity);
                        Console.Write("Press enter to input item id !");
                        Console.ReadLine();
                    }
                    else
                    {
                        obl.AddItem(itemid, quantity, order);
                        Console.Write("Add more item ? (Y/N) : ");
                        char add = valid2(Console.ReadLine());
                        if (add == 'n' || add == 'N')
                        {
                            Console.WriteLine("**************************");
                            Console.WriteLine("a.Pay and print bill\n\nb.Cancel order");
                            Console.WriteLine("**************************");
                            Console.Write("Choice : ");
                            char pay = valid4(Console.ReadLine());
                            if (pay == 'b'||pay =='B')
                            {
                                Console.Clear();
                                Console.WriteLine("Order was canceled !\nPress enter to back");
                                Console.ReadKey();
                                main.orders();
                            }
                            else if (pay == 'a'||pay =='A')
                            {
                                Console.Clear();
                                bool re = obl.CreateOrder(order);
                                if (re == true)
                                {
                                    Console.WriteLine("                       Item List");
                                    Console.WriteLine("======================================================================");
                                    Console.WriteLine("|Item                  |Unit price       |Quantity|Price             |");
                                    Console.WriteLine("======================================================================");
                                    foreach (var i in obl.GetOrderDetail())
                                    {
                                        decimal price = i.amount * i.price;
                                        Console.WriteLine("|{0,-22}|{1,-17}|{2,-8}|{3,-18}|",i.item_name, moneyformat(i.price), i.amount, moneyformat(price));
                                        Console.WriteLine("======================================================================");
                                    }
                                    Console.WriteLine("                                      Total Money :" + moneyformat(obl.Money(order)));
                                    Console.WriteLine("======================================================================");
                                    Console.Write("Input money get from customer : ");
                                    decimal paid = valid(Console.ReadLine());
                                    while (paid < obl.Money(order))
                                    {
                                        Console.Write("Not enough, please re-enter : ");
                                        paid = valid(Console.ReadLine());
                                    }
                                    decimal payback = paid - obl.Money(order);
                                    Console.WriteLine("Pay success!\nPress enter to print bill !");
                                    Console.ReadLine();
                                    Console.Clear();
                                    Console.WriteLine("                           BILL");
                                    Console.WriteLine("==============================================================");
                                    Console.WriteLine("Store : Luxury shop");
                                    Console.WriteLine("Address : 300 Kim Nguu, Ha Noi");
                                    Console.WriteLine("Phone number : 01627344748");
                                    order or = obl.GetLastOrder();
                                    Console.WriteLine("Order ID : " + order.order_id);
                                    Console.WriteLine("Order time : " + or.order_date);
                                    Console.WriteLine("Staff : " + or.user_name);
                                    Console.WriteLine("                           Item list");
                                    Console.WriteLine("==============================================================");
                                    Console.WriteLine("|Item                  |Unit price   |Quantity|Price         |");
                                    Console.WriteLine("==============================================================");
                                    foreach (var i in obl.GetOrderDetail())
                                    {
                                        decimal price = i.amount * i.price;
                                        Console.WriteLine("|{0,-22}|{1,-13}|{2,-8}|{3,-14}|", i.item_name, moneyformat(i.price), i.amount, moneyformat(price));
                                        Console.WriteLine("==============================================================");
                                    }
                                    Console.WriteLine("                                   Total money : " + moneyformat(obl.Money(order)));
                                    Console.WriteLine("                             Get from customer : " + moneyformat(paid));
                                    Console.WriteLine("                                      Pay back : " + moneyformat(payback));
                                    Console.WriteLine(".....................................................................");
                                    Console.WriteLine("                             Thanks You");
                                    Console.WriteLine("                           SEE YOU AIGAIN");
                                    Console.WriteLine("Press enter to back to menu!");
                                    Console.ReadKey();
                                    main.orders();
                                }
                                else
                                {
                                    Console.WriteLine("Tạo đơn hàng thất bại!");
                                    Console.Write("Nhấn phím [Enter] để quay lại menu chính...");
                                    Console.ReadLine();
                                    main.orders();
                                }
                            }
                        }
                    }
                }
                if (item == null)
                {

                    Console.WriteLine("ID sách không tồn tại...");
                    Console.Write("Nhấn phím [Enter] để nhập lại ID sách...");
                    Console.ReadLine();

                }
            }
        }
        public int valid(string a)
        {
            Regex regex = new Regex("[0-9]");
            MatchCollection matchcollectionstr = regex.Matches(a);
            while ((matchcollectionstr.Count < a.Length) || (a == ""))
            {
                Console.Write("Not valid ! please re-enter : ");
                a = Console.ReadLine();
                matchcollectionstr = regex.Matches(a);
            }
            return Convert.ToInt32(a);
        }
        public char valid2(string a)
        {
            Regex regex = new Regex("[a-zA-Z]");
            MatchCollection matchCollectionstr = regex.Matches(a);
            while ((matchCollectionstr.Count < a.Length) || (a != "y" && a != "Y" && a != "n" && a != "N") || (a.Length > 1) || a == "")
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
            while ((matchcollectionstr.Count < a.Length) || (a == "") || a != "1" || a != "2")
            {
                Console.Write("Not valid ! please re-enter : ");
                a = Console.ReadLine();
                matchcollectionstr = regex.Matches(a);
            }
            return Convert.ToInt32(a);
        }
        public char valid4(string a)
        {
            Regex regex = new Regex("[a-zA-Z]");
            MatchCollection matchCollectionstr = regex.Matches(a);
            while ((matchCollectionstr.Count < a.Length) || (a != "a" && a != "A" && a != "b" && a != "B") || (a.Length > 1) || a == "")
            {
                Console.Write("Not valid ! please re-enter : ");
                a = Console.ReadLine();
                matchCollectionstr = regex.Matches(a);
            }
            return Convert.ToChar(a);
        }
        public string moneyformat(decimal a)
        {
            string money = a.ToString();
            string price = "";
            int x = (money.Length - 1) % 3;
            for (int i = 0; i < money.Length; i++)
            {
                if (i == money.Length - 1)
                {
                    price = price + money[i];
                }
                else if ((i - x) % 3 == 0)
                {
                    price = price + money[i] + ",";
                }
                else
                {
                    price = price + money[i].ToString();
                }

            }
            price = price + "VND";
            return price;
        }

    }
}