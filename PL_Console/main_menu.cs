using System;
using BL;
using Persitence;
using System.Text;
using System.Text.RegularExpressions;
namespace PL_console
{
    public class mainmenu
    {
        public void Menuchoose()
        {
            Console.Clear();
            string[] choice = { "Login", "Exit" };
            int choose = Menu("Welcome to Luxury", choice);
            switch (choose)
            {
                case 1 : LoginMenu();
                break;
                case 2 : Environment.Exit(0);
                break;
            }

        }
        public void LoginMenu()
        {
            Console.Clear();
            Console.WriteLine("          Login to Luxury");
            Console.WriteLine("==================================");
            Console.Write("Username : ");
            string username = Console.ReadLine();
            Console.Write("Password : ");
            string password = Password();
            string choose;
            UserBL ubl = new UserBL();
            while (ubl.Login(username, password).user_name != username && ubl.Login(username, password).user_password != password)
            {
                Console.WriteLine("Wrong username or password!Do you want to continue login? (Y/N)");
                choose = Console.ReadLine();
                switch (choose)
                {
                    case "Y": break;
                    case "y": break;
                    case "N": Menuchoose(); break;
                    case "n": Menuchoose(); break;
                    default: continue;
                }
                Console.Clear();
                Console.WriteLine("        Login to Luxury");
                Console.WriteLine("==================================");
                Console.Write("Username : ");
                username = Console.ReadLine();
                Console.Write("Password : ");
                password = Password();
            }
            while(ubl.Login(username,password)== null)
            {
                Console.WriteLine("Empty ! Do you want to continue login? (Y/N)");
                choose = Console.ReadLine();
                switch(choose)
                {
                    case "Y" : break;
                    case "y" : break;
                    case "N" : Menuchoose(); break;
                    case "n" : Menuchoose(); break;
                    default : continue;
                }
                Console.Clear();
                Console.WriteLine("        Login to Luxury");
                Console.WriteLine("==================================");
                Console.Write("Username : ");
                username = Console.ReadLine();
                Console.Write("Password : ");
                password = Password();
            }
            if(ubl.Login(username,password).user_name == username && ubl.Login(username,password).user_password==password)
            {
                orders();
            }
        }
        public void orders()
        {
            Console.Clear();
            string[] Mainmenu = {"Create order","Logout"};
            int main = Menu("Luxury System",Mainmenu);
            MenuOrder menuorder = new MenuOrder();
            switch(main)
            {
                case 1 : menuorder.orders();
                break;
                case 2 : Menuchoose();
                break;
            }
        }

        public string Password()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.Write("\b\0\b");
                        sb.Length--;
                    }
                    continue;
                }
                Console.Write('*');
                sb.Append(cki.KeyChar);
            }
            return sb.ToString();
        }
        public short Menu(string title, string[] menuItems)
        {
            short choose = 0;
            string line1 = "========================================";
            string line2 = "----------------------------------------";
            Console.WriteLine(line1);
            Console.WriteLine(" " + title);
            Console.WriteLine(line2);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine(" " + (i + 1) + ". " + menuItems[i]);
            }
            Console.WriteLine(line2);
            do
            {
                Console.Write("Choice : ");
                try
                {
                    choose = Int16.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please re-choice");
                    continue;
                }
            }
            while (choose <= 0 || choose > menuItems.Length);
            return choose;
        }
    }

}