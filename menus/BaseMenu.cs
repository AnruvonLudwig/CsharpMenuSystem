using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsharpMenuSystem.interfaces;

namespace CsharpMenuSystem.menus{
    /// <summary>
    /// Represents the base class for menu-based console views.
    /// Provides common functionality for displaying menus and handling user input.
    /// </summary>
    /// <remarks>
    /// This class implements IView and requires derived classes to menu options and define how menu
    /// selections are resolved.
    /// </remarks>
    internal abstract class BaseMenu : IView
    {
        /// <summary>
        /// Holds the current menu options by the menu.
        /// </summary>
        protected string[] menuOptions = Array.Empty<string>();
        /// <summary>
        /// Holds the current menu name used by the menu.
        /// </summary>
        protected string menuName = string.Empty;
        //this is only to neatly display a message between same menu options
        /// <summary>
        /// Message to display to the user in a controlled manner.
        /// </summary>
        /// <remarks>
        /// Leave Empty to hid message section.
        /// </remarks>
        protected string message = string.Empty;

        //make non virtual
        /// <summary>
        /// Displays the menu, reads the user's selection, and resolves the selected option.
        /// </summary>
        /// <remarks>
        /// Resolves user selection via ResolveOption().
        /// </remarks>
        public virtual void HoldDisplay(){
            // BuildMenu(menuOptions);
            Display();
            int choice = ReadOption();
            if (choice == -1) return;
            ResolveOption(choice);//IView required Navigator.Pop() lives here
        }
        /// <summary>
        /// The raw Display logic.
        /// </summary>
        public virtual void Display(){
            Console.WriteLine();
            BuildMenu(menuOptions);

            if (!string.IsNullOrEmpty(message)) {
                Console.WriteLine(message);
                Console.WriteLine("-----------");
            }
            Console.Write("Select: ");

        }

        /// <summary>
        /// Resolves the selected menu option.
        /// </summary>
        /// <param name="option">The number of the selected menu option.</param>
        /// <remarks>
        /// Requires a flow with Navigator.Pop() as required by IView.
        /// </remarks>
        protected abstract void ResolveOption(int option);
        /// <summary>
        /// Builds and displays the menu using the supplied options.
        /// </summary>
        /// <param name="options">The options to display in the menu.</param>
        ///
        protected void BuildMenu(string[] options){
            Console.WriteLine("-----------");
            Console.WriteLine(menuName);
            Console.WriteLine("-----------");
            for (int i = 0; i < options.Length; i++){
                Console.WriteLine($"{i+1}. {options[i]} ");
            }
            Console.WriteLine("-----------");
        }
        /// <summary>
        /// Reads a single character from the console and attempts to convert it to a menu option.
        /// </summary>
        /// <returns>The selected option number, or -1 if the input is not a number.</returns>
        ///
        private int ReadOption(){
            char choose = Console.ReadKey().KeyChar;
            message = string.Empty;//reset message after every choice
            Console.WriteLine();
            if (int.TryParse(choose.ToString(), out int choice) )
            {
                return choice;
            }
                message = ($"{choose} is not a number!");
                // HoldDisplay();
                // Reload($"{choose} is not a number!");
                return -1;//in Display has if(choice == -1) return;
                //when exception handling is covered implement custom err?
                //instead of -1
                
        }
        /// <summary>
        /// Creates display-friendly menu options from the names of an enum.
        /// </summary>
        /// <param name="enumType">The enum type whose names are used as menu options.</param>
        /// <returns>An array containing the formatted enum names.</returns>
        protected static string[] OptionsFromEnum(Type enumType)
        {
            string[] options = Enum.GetNames(enumType);
            for (int i =0; i< options.Length;i++)
            {
                options[i] = Regex.Replace(options[i],"([A-Z])"," $0");   
            }
            return options;
        }
        // public void Reload(string warning = ""){
        //     Console.Clear();
        //     if (!string.IsNullOrEmpty(warning)) Console.WriteLine(warning);
        //     HoldDisplay();
        // }

        //uncommented for now since im unsure of it 
        // public void Reload() {
        //     Console.Clear();
        //     HoldDisplay();
        // }
    }
}