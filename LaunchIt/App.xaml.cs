using LaunchIt.Core;
using LaunchIt.Data;
using LaunchIt.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace LaunchIt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow window;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            window = new MainWindow();

            window.Show();
        }
    }
}
