using BPTI;
using Momentum.Ekonomi.Payments.Terminal.Bpti;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows;


namespace WPFTester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

         void App_Startup(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Terminal = new BptiProvider();

            icpc = (IConnectionPointContainer)window.Terminal.API;

            Guid IID_ICoBpTiEvents = typeof(ICoBpTiX2Events).GUID;

            icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
            icp.Advise(window.Terminal, out cookie);

            window.Show();
        }

        int cookie;
        IConnectionPointContainer icpc;
        IConnectionPoint icp;
   

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            var main = Windows;

            var m = main[0] as MainWindow;
            icp.Advise(m.Terminal, out cookie);
        }
    }
}
