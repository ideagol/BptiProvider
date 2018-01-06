using System;
using System.Runtime.InteropServices.ComTypes;
using BPTI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Momentum.Ekonomi.Payments.Terminal.Bpti;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {

        BptiProvider Terminal = null;


        BptiProvider  GetApi()
        {
            int cookie;
            BptiProvider provider = new BptiProvider();

            IConnectionPointContainer icpc;
            icpc = (IConnectionPointContainer)provider.API;
            IConnectionPoint icp;
            Guid IID_ICoBpTiEvents = typeof(ICoBpTiX2Events).GUID;

            icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
            icp.Advise(provider, out cookie);

            return provider;
        }

        [TestMethod]
        public void InitializeAPI()
        {

            Terminal = GetApi();
            
        }

        [TestMethod]
        public void AnslutTCP()
        {
            Terminal = GetApi();          
            Terminal.AnslutTCP("127.0.0.1", 2000);    
            
        }

        [TestMethod]
        public void ÖppnaTerminal()
        {
            Terminal = GetApi();
            Terminal.AnslutTCP("127.0.0.1", 2000);
            Terminal.Open();
        }

        [TestMethod]
        public void StartaKöp()
        {
            Terminal = GetApi();
            Terminal.AnslutTCP("127.0.0.1", 2000);
            Terminal.StartaKöp();
            Terminal.SkickaBelopp(30000, 60, 0);
        }

        [TestMethod]
        public void StartaÅterköp()
        {
            Terminal = GetApi();
            Terminal.AnslutTCP("127.0.0.1", 2000);
            Terminal.StartaÅterköp();
            Terminal.SkickaBelopp(30000, 60, 0);
        }

    }
}
