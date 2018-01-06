using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Ekonomi.Payments.Terminal
{
    public class StatusEventArgs : EventArgs
    {
        public int Status { get; set; }        
    }
}
