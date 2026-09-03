using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsharpMenuSystem.interfaces;

namespace CsharpMenuSystem.menus{
    /// <summary>
    /// Manages the navigation stack and display flow for the console application's IView system.
    /// </summary>
    /// <remarks>
    /// Navigator stores active views in a stack, allowing views to be pushed and popped as
    /// the user moves between menus and submenus. The main application normally calls
    /// HoldDisplay while hasContext returns true.
    /// Loading can temporarily take control of the display while a background operation runs,
    /// then restore normal view display when the operation completes.
    /// </remarks>
    internal static class Navigator{
        private static Stack<IView> context = new Stack<IView>();
        /// <summary>
        /// Adds a view to the navigation stack and clears the console for the new view.
        /// </summary>
        /// <param name="newView">The view to add to the navigation stack.</param>
        public static void Push(IView newView){
            context.Push(newView);
            Console.Clear();
        }
        /// <summary>
        /// Removes the current view from the navigation stack and clears the console.
        /// </summary>
        /// <remarks>
        /// Does nothing if the navigation stack is empty.
        /// </remarks>
        public static void Pop(){
            if (context.Count > 0) context.Pop();
            //needs better error handling
            //TODO: custom exception, no out of context
            Console.Clear();
        }
        /// <summary>
        /// Removes views from the navigation stack until only the root view remains.
        /// </summary>
        /// <remarks>
        /// The root view is the first view added to the navigation stack and represents
        /// the main point of navigation.
        /// </remarks>
        public static void PopToMain(){
            // if (context.Count > 0) context.Pop();
            //context is a ref so for loop will get live count, so must store the initial.
            int count= context.Count;
            for (int i = 0; i < count -1; i++)
            {
                context.Pop();
            }
            //TODO: custom exception, no out of context
            Console.Clear();
        }
        /// <summary>
        /// Clears the navigation stack and terminates the application with code 0.
        /// </summary>
        public static void Exit(){
            context.Clear();
            Console.WriteLine("GoodBye");
            Environment.Exit(0);
            Console.ReadKey();
        }
        /// <summary>
        /// Determines whether there are any views currently in the navigation stack.
        /// </summary>
        /// <returns><c>true</c> if the navigation stack contains a view; otherwise, <c>false</c>.</returns>
        public static bool hasContext(){
            return context.Count > 0;
        }
        private delegate void DisplayDelegate();
        private static DisplayDelegate display = Display;
        /// <summary>
        /// Displays the current navigation view using its HoldDisplay implementation.
        /// </summary>
        /// <remarks>
        /// The display operation can be temporarily replaced by a loading display while
        /// a background operation is running.
        /// </remarks>
        public static void HoldDisplay(){
            display();

        }
        /// <summary>
        /// Displays and holds the current view from the top of the navigation stack.
        /// </summary>
        /// <remarks>
        /// This method delegates the display behaviour to the current view's HoldDisplay method.
        /// </remarks>
        private static void Display(){
            context.Peek().HoldDisplay();

        }
        // private static Thread background;
        /// <summary>
        /// Starts a background operation and temporarily displays loading progress.
        /// </summary>
        /// <param name="_background">The background operation to execute while loading is displayed.</param>
        /// <param name="_loadTime">The approximate overall loading time, in seconds, used to determine the progress intervals.</param>
        /// <remarks>
        /// The loading display divides the specified loading time into ten equally timed
        /// progress intervals. The loading display remains active until the background
        /// operation finishes, after which normal view display is restored.
        /// This is intended for longer operations that lead to another or refreshed view.
        /// </remarks>
        public static void Loading(Action _background,int _loadTime = 1){
            // background.Start();
            Thread background = new Thread(() => _background());
            // Thread background = 
            background.Start();
            if (_loadTime < 0) _loadTime =0;
            // loadTime = _loadTime;
            display = () => LoadingText(background, _loadTime*10);


        }
        /// <summary>
        /// Starts a background operation and displays loading progress inline with the current console flow.
        /// </summary>
        /// <param name="_background">The background operation to execute while loading is displayed.</param>
        /// <param name="_loadTime">The time between loading progress updates, in 100-millisecond units.</param>
        /// <remarks>
        /// The loading display remains inline while the background operation runs and is removed
        /// when the operation completes, allowing the current view to continue displaying normally.
        /// This is intended for loading that occurs during an existing user interaction.
        /// </remarks>
        public static void InLineLoading(Action _background,int _loadTime = 1){
            // background.Start();
            Thread background = new Thread(() => _background());
            // Thread background = 
            background.Start();
            if (_loadTime < 0) _loadTime =0;
            // loadTime = _loadTime;
            LoadingText(background, _loadTime, true);


        }
        // private static int loadTime =1;
        /// <summary>
        /// Displays loading progress until the background operation completes.
        /// </summary>
        /// <param name="background">The background thread being monitored for completion.</param>
        /// <param name="loadTime">The delay used between progress updates.</param>
        /// <param name="inLine">Determines whether the loading display should remain inline with the current console output.</param>
        /// <remarks>
        /// Progress is displayed in ten-percent increments while the background operation is active.
        /// When not displaying inline, the console is cleared and normal view display is restored
        /// after the operation completes.
        /// </remarks>
        private static void LoadingText(Thread background,int loadTime, bool inLine = false){
            while (background.IsAlive)
            {
                for(int sec = 0; sec <= 100;sec += 10)
                {
                    Console.Write($"...{sec}%");
                    Thread.Sleep(loadTime*10);
                }
                // Overwrite the line with empty spaces to wipe out old text
                Console.Write($"\r" + new string(' ',Console.WindowWidth) + $"\r");
                    
            }
            // Console.SetCursorPosition(0,Console.CursorTop -1);
            if (!inLine)
            {            
                Console.Clear();
                display = Display;
            }
        }
    }
}