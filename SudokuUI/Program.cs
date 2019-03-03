using System;
using System.Windows.Forms;

namespace SudokuUI
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MessageBox.Show("This program is still in development. Unfinished stuff and bugs ahead.");
            Application.Run(args.Length == 0 ? new MainWindowForm(string.Empty) : new MainWindowForm(args[0]));
        }
    }
}
