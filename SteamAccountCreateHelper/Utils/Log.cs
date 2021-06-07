using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountCreateHelper.Utils
{
    class Log
    {
        Main MyForm1 = new Main();

        private static readonly object locker = new object();

        public static void info(string msg)
        {

            lock (locker)
            {
                msg = DateTime.Now + " - " + msg;

                Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.SelectionColor = Color.DarkGreen));
                Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.AppendText(msg + Environment.NewLine)));
                Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.SelectionColor = Main._Form1.txtConsole.ForeColor));
                Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.SelectionStart = Main._Form1.txtConsole.Text.Length));
                Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.ScrollToCaret()));

                StreamWriter sw;
                sw = File.AppendText(Path.Combine(Main.Database_Path, "log.txt"));
                sw.WriteLine(msg);
                sw.Close();
                sw.Dispose();
            }

        }

        public static void error(string msg)
        {
            msg = DateTime.Now + " - " + msg;

            Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.SelectionColor = Color.DarkRed));
            Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.AppendText(msg + Environment.NewLine)));
            Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.SelectionColor = Main._Form1.txtConsole.ForeColor));
            Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.SelectionStart = Main._Form1.txtConsole.Text.Length));
            Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.ScrollToCaret()));

            File.AppendAllText(Path.Combine(Main.Database_Path, "log.txt"), msg + "\n");
        }

        public static void error(string format, params object[] args)
        {
            string msg = string.Format(format, args);
            error(msg);
        }

        public static void error(string msg, Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            msg = DateTime.Now + " - " + msg + ". " + e.Message;

            Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.SelectionColor = Color.DarkRed));
            Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.AppendText(msg + Environment.NewLine)));
            Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.SelectionColor = Main._Form1.txtConsole.ForeColor));
            Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.SelectionStart = Main._Form1.txtConsole.Text.Length));
            Main._Form1.txtConsole.Invoke(new Action(() => Main._Form1.txtConsole.ScrollToCaret()));

            File.AppendAllText(Path.Combine(Main.Database_Path, "log.txt"), msg + "\n");
            File.AppendAllText(Path.Combine(Main.Database_Path, "error.txt"), msg + "\n" + e.StackTrace + "\n");

        }
    }
}
