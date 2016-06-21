using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedExpenseManager
{
    public class AppStarter : System.Windows.Application
    {
        [STAThread]
        public static void Main() // Use this as entry point for app, so that other processes can be started along with window
        {
            AppController.Load();
            //AppController.Save(); // For testing
            var app = new SharedExpenseManagerApp();
            app.MainWindow = new MainWindow(); // Main interface of app
            app.MainWindow.Show();
            app.Run();
        }
    }
}
