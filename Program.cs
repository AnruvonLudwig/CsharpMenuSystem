using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpMenuSystem.menus;

namespace CsharpMenuSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu baseMenu = new MainMenu();
            Navigator.Push(baseMenu);
            while (true)
            {
                Navigator.HoldDisplay();
            }
        }
    }
}
