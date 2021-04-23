using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FatfsToSpiffsConverter
{
    static class Program
    {
        public static readonly string programName = Assembly.GetExecutingAssembly().FullName.Split(',')[0];

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var mutex = new System.Threading.Mutex(true, programName, out createdNew))
            {
                if (createdNew)
                    // first instance
                    Application.Run(new MainForm());
                else
                    MessageBox.Show("Программа уже запущена", programName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
