using BPTI;
using Momentum.Ekonomi.Payments.Terminal.Bpti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Momentum.Boplats.Ekonomi.Kassa
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.SetCompatibleTextRenderingDefault(false);
            var form = new KassaMain();           
            Application.EnableVisualStyles();
            Application.Run(form);
                     
        }
    }
}
