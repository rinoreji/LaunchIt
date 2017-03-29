using LaunchIt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LaunchIt
{
    public class CustomStartup
    {
        [System.STAThreadAttribute]
        static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
