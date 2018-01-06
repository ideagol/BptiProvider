using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Ekonomi.Payments.Terminal
{
    public class DisplayEventArgs: EventArgs
    {
        public DisplayEventArgs()
        {
            Items = new List<string>();
        }

        public List<string> Items { get; set; }        
    }
}
