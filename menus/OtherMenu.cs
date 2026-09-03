
using CsharpMenuSystem.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CsharpMenuSystem.menus{


    enum OtherMenuOptions{
        FirstView=1,//BaseMenu starts at 1
        Back,
        Exit,
    }
    internal class OtherMenu : BaseMenu
    {

        public OtherMenu(string menuName)
        {
            this.menuName = menuName;
            menuOptions = OptionsFromEnum(typeof(OtherMenuOptions));
        }

        protected override void ResolveOption(int option)
        {
            switch (option)
            {
                case (int)OtherMenuOptions.FirstView:
                    Navigator.Push(new FirstView());
                break;

                case (int)OtherMenuOptions.Back:
                    Navigator.Pop();
                break;

                case (int)OtherMenuOptions.Exit:
                    Navigator.Exit();
                break;

                default:
                    message = $"{option} is not a valid choice!";
                return;
            }

        }
    }
 }