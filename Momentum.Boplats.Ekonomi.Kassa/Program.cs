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

            int cookie;
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new KassaMain();
            form.Terminal = new BptiProvider();

            IConnectionPointContainer icpc;
            icpc = (IConnectionPointContainer)form.Terminal.API;
            IConnectionPoint icp;
            Guid IID_ICoBpTiEvents = typeof(ICoBpTiX2Events).GUID;

            icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
            icp.Advise(form.Terminal, out cookie);

         
            Application.EnableVisualStyles();
            Application.Run(form);
            icp.Unadvise(cookie);            
        }
    }
}
