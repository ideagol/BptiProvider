using System;
using System.Runtime.InteropServices.ComTypes;
using BPTI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Momentum.Ekonomi.Payments.Terminal.Bpti;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        BptiProvider Terminal = new BptiProvider();
        
        [TestMethod]
        public void Connect()
        {
            Terminal.Anslut("127.0.0.1", 2000, null);
        }       
    }
}
