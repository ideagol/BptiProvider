using BPTI;
using Momentum.Ekonomi.Payments.Terminal.Bpti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Boplats.Ekonomi.Kassa
{
    public class BptiBootstrap
    {
        IConnectionPointContainer icpc;
        IConnectionPoint icp;
        public void InitializeBpti(BptiProvider Terminal)
        {
            IConnectionPointContainer icpc;
            icpc = (IConnectionPointContainer)Terminal.API;
            IConnectionPoint icp;
            Guid IID_ICoBpTiEvents = typeof(ICoBpTiX2Events).GUID;
            icpc.FindConnectionPoint(ref IID_ICoBpTiEvents, out icp);
            int localCookie;
            icp.Advise(Terminal, out localCookie);
            Terminal.ConnectionCookie = localCookie;
        }


        public void UnInitializeBpti(BptiProvider Terminal)
        {
            icp.Unadvise(Terminal.ConnectionCookie);
        }
    }
}
