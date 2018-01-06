using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Momentum.Ekonomi.Payments.Terminal;
using Momentum.Ekonomi.Payments.Terminal.Bpti;

namespace Momentum.Boplats.Ekonomi.Kassa
{
    public partial class PrinterWindow : Form
    {
        public PrinterWindow()
        {
            InitializeComponent();
        }

        public BptiProvider Terminal { get; internal set; }

        private void PrinterWindow_Load(object sender, EventArgs e)
        {
            Terminal.TerminalReceiptEvent += Terminal_TerminalReceiptEvent;
        }

        private void Terminal_TerminalReceiptEvent(object sender, EventArgs e)
        {
            var arg = e as DisplayEventArgs;

            if (arg != null && arg.Items.Count > 0)
            {
                foreach (var item in arg.Items)
                {
                    if (item != null)
                    {
                        receipt.Items.Add(item);
                    }
                }
            }
        }
    }
}
