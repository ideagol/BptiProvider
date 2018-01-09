using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Ekonomi.Payments.Terminal
{
    public class TransactionEventArgs : EventArgs
    {
        public TransactionEventArgs()
        {
            
        }

        public string TransactionNumber { get; set; }
        public bool TransactionOk { get; set; }
    }
}
