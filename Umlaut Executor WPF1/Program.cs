using P3.Core.ExecutorWPF;
using System;

namespace Umlaut_Executor_WPF1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            App app = new App();
            app.Run();
        }
    }
}
