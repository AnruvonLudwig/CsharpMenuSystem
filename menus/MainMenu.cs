using CsharpMenuSystem.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CsharpMenuSystem.menus{


    enum MainMenuOptions{
        FirstMenu=1,//BaseMenu starts at 1
        SecondMenu,
        ThirdMenu,
        Exit,
    }
    internal class MainMenu : BaseMenu
    {

        public MainMenu()
        {
            menuName = "Menu System"; //Main menu
            menuOptions = OptionsFromEnum(typeof(MainMenuOptions));
        }

        protected override void ResolveOption(int option)
        {
            string[] optionNames = Enum.GetNames(typeof (MainMenuOptions));
            switch (option)
            {
                case (int)MainMenuOptions.FirstMenu:
                    Navigator.Push(new OtherMenu(optionNames[((int)MainMenuOptions.FirstMenu)- 1]));
                break;

                case (int)MainMenuOptions.SecondMenu:
                    Navigator.Push(new OtherMenu(optionNames[((int)MainMenuOptions.SecondMenu)-1]));
                    break;

                case (int)MainMenuOptions.ThirdMenu:
                    Navigator.Push(new OtherMenu(optionNames[((int)MainMenuOptions.ThirdMenu)-1]));
                break;

                case (int)MainMenuOptions.Exit:
                    Navigator.Exit();
                break;

                default:
                    message = $"{option} is not a valid choice!";
                return;
            }

        }
    }
 }