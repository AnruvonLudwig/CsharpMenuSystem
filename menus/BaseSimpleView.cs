
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpMenuSystem.interfaces;

namespace CsharpMenuSystem.menus{
    /// <summary>
    /// Provides the basic implementation of an IView for simple console views.
    /// </summary>
    internal abstract class BaseSimpleView : IView
    {
        /// <summary>
        /// Displays the view, waits for user input, and then removes the view from the Navigator.
        /// </summary>
        /// <remarks>
        /// ReadKey call keeps the view displayed.
        /// Navigator.Pop is required by IView to return from the current view.
        /// </remarks>
        public virtual void HoldDisplay(){
            // BuildMenu(menuOptions);
            Display();
            Console.ReadKey();
            Navigator.Pop();
        }
        /// <summary>
        /// The raw Display logic for the view.
        /// </summary>
        public abstract void Display();

    }
}