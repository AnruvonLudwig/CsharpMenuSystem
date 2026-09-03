using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CsharpMenuSystem.interfaces
{
    //Problem with IView, currently must have a context field as well...
    /// <summary>
    /// Defines the required methods for a console-based view.
    /// Implementations must provide a method to display the view and a method to
    /// display the view while retaining focus until the view is ready to close.
    /// </summary>
    internal /*static*/ interface IView{
        /// <summary>
        /// Displays the view's content to the console.
        /// </summary>
         void Display();
        /// <summary>
        /// Displays the view and holds focus until the view's interaction is complete.
        /// </summary>
        /// <remarks>
        /// The implementation must call Navigator.Pop() when the view is finished
        /// so that control can return to the previous view.
        /// The implementation must also include an input or waiting operation, such
        /// as Console.ReadKey() or Console.ReadLine(), to keep the view active.
        /// </remarks>
         void HoldDisplay();
         //Reload should not call display, or else recursion will happen
        //uncommented for now since im unsure of it 
        //  void Reload();
        

    }
}