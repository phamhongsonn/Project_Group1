using System;
using System.Text;
using System.Security;
using BL;
using System.Text.RegularExpressions;
using Persitence;
using PL_console;
namespace PL_console
{
    public class Program
    {
        static void Main(string[] args)
        {
            mainmenu m = new mainmenu();
            m.Menuchoose();
        }
    }
}