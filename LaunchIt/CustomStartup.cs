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
            //using (var hook = new KeyboardHook())
            {
                var app = new App();// new App(hook);
                app.InitializeComponent();
                app.Run();
            }
        }
    }
}
