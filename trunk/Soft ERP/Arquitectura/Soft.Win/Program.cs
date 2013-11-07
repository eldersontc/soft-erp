using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Soft.DataAccess;

namespace Soft.Win
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ControllerApp.LoadConfiguration();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        } 
    }
}
