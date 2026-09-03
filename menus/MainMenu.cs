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
            switch (option)
            {
                case (int)MainMenuOptions.FirstMenu:
                break;

                case (int)MainMenuOptions.SecondMenu:
                    break;

                case (int)MainMenuOptions.ThirdMenu:
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