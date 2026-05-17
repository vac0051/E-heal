using System;
using Terminal.Gui;

namespace EHealthDesktop
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize Db and seed data
            DbHelper.InitDb();

            // Init Terminal.Gui
            Application.Init();

            var top = Application.Top;
            var win = new MainWindow();
            top.Add(win);

            Application.Run();
            Application.Shutdown();
        }
    }
}
