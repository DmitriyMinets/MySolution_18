using P3.Core.Enums;
using P3.Core.Executor.Bases;
using P3.Core.Executor.Enums;
using P3.Core.Executor.Interfaces;
using P3.Core.WPF.Windows;
using Prism.Events;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Umlaut_Executor_WPF1.Executor
{
    /// <summary>
    /// This is the executor class that will implement the IPlugin interface
    /// </summary>
    public sealed class Executor : PluginCommon, IPlugin, IMenuBar //==> uncomment IMenuBar if you dont want to implement menubar
    {
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="args">arguments</param>
        public Executor()
        {
            ExecutionMode = GUIMode.Normal;
            EnableLoggingInUI = true;
            _fileMenu = new MenuItem() { Header = "File" };
            _helpMenu = new MenuItem() { Header = "Help" };

            var exit = new MenuItem() { Header = "Exit" };
            exit.Click += (s, e) =>
            {
                e.Handled = true;
                NotifyCloseApplication();
            };
            _fileMenu.Items.Add(exit);

            var about = new MenuItem() { Header = "About" };
            about.Click += (s, e) =>
            {
                e.Handled = true;
                AboutApp aboutApp = new AboutApp(
                    new P3.Core.WPF.ViewModels.Windows.AboutAppViewModel(
                        new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), @"ReleaseLogs.xml")),
                        Title,
                        new P3.Core.Models.Link(HelpLink, HelpLink.AbsoluteUri),
                        "software@umlaut.com"
                    )
                );

                aboutApp.ShowDialog();
            };
            _helpMenu.Items.Add(about);
        }

        /// <summary>
        /// Async executable function.
        /// </summary>
        /// <param name="args">arguments from command line</param>
        private async Task StartLongRunningExecution(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(100);

                if (i < 49)
                {
                    NotifyProgressChanged(true, 0, 100, i, ProgressType.Default);
                }
                else
                {
                    NotifyProgressChanged(false, 0, 100, i, ProgressType.Error);
                }

                WriteToLog(i.ToString(), LogType.Information);
            }

            WriteToLog("Hello", LogType.Information);
            NotifyCloseApplication();
        }

        #region IMenuBar

        /// <summary>
        /// Menu items to load
        /// </summary>
        public List<MenuItem> MenuItems
        {
            get
            {
                return new List<MenuItem>() {
                    _fileMenu,
                    _helpMenu
                };
            }
        }

        #endregion IMenuBar

        #region IPlugin

        /// <summary>
        /// The function for execution should be done here, the execution runs async
        /// </summary>
        /// <param name="args">Application argements</param>
        /// <returns>Task</returns>
        public Task ExecuteAsync(string[] args)
        {
            return Task.Factory.StartNew(() => StartLongRunningExecution(args));
        }

        /// <summary>
        /// This is the initialization function for the plugin.
        /// This function is first invoked to allwo the plugin to initialize resources
        /// </summary>
        /// <param name="iEventAggregator">Prism.Events.IEventAggregator : handle PubSub events exchange</param>
        public void Init(IEventAggregator iEventAggregator)
        {
            base._iEventAggregator = iEventAggregator;
        }

        /// <summary>
        /// Title of the plugin
        /// </summary>
        public string Title => "Umlaut_Executor_WPF1";

        #endregion IPlugin



        #region IDisposable

        /// <summary>
        /// Handle all disposable objects
        /// </summary>
        public void Dispose()
        {
            //Dispose any objects created by you.
            //Do not dispose injected objects, they will be automatically disposed by the ExecutorApp
        }

        #endregion IDisposable
    }
}
