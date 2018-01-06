using Momentum.Ekonomi.Payments.Terminal;
using Momentum.Ekonomi.Payments.Terminal.Bpti;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Momentum.Boplats.Ekonomi.Kassa
{
    public partial class KassaMain : Form
    {

        public BptiProvider Terminal = null;



        public KassaMain()
        {
            InitializeComponent();
            Terminal = new BptiProvider();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Terminal.TerminalCommonEvent += Terminal_TerminalCommonEvent;
            Terminal.TerminalDisplayEvent += Terminal_TerminalDisplayEvent;
            //Terminal.TerminalReceiptEvent += Terminal_TerminalReceiptEvent;

            ConnectToTerminal();

            if (Properties.Settings.Default.UsePrinterWindow)
            { 
                PrinterWindow printerWindow = new PrinterWindow();
                printerWindow.Terminal = Terminal;
                printerWindow.Show();
            }



        }

        private void ConnectToTerminal()
        {
            if (!Terminal.ConnectionInitiated)
            {
                if (Properties.Settings.Default.TerminalTcp.Length > 0 && Properties.Settings.Default.TerminalPort > 0)
                {
                    Terminal.AnslutTCP(Properties.Settings.Default.TerminalTcp, Properties.Settings.Default.TerminalPort);
                }
                else
                {

                }
            }
            else
            {
                Terminal.ÅterAnslut();
            }
        }

        //private void Terminal_TerminalReceiptEvent(object sender, EventArgs e)
        //{

        //    var arg = e as DisplayEventArgs;

        //    if (arg != null && arg.Items.Count > 0)
        //    {
        //        foreach (var item in arg.Items)
        //        {
        //            if (item != null )
        //            {
        //                receipt.Items.Add(item);
        //            }
        //        }
        //    }

        //}

        private void Terminal_TerminalDisplayEvent(object sender, EventArgs e)
        {
            var arg = e as DisplayEventArgs;

            if (arg != null && arg.Items.Count > 0)
            {
                foreach (var item in arg.Items)
                {
                    trmDsp.AppendText(item + Environment.NewLine);
                }
            }
        }

        private void Terminal_TerminalCommonEvent(object sender, EventArgs e)
        {
            var arg = e as DisplayEventArgs;

            if (arg != null && arg.Items.Count > 0)
            {
                foreach (var item in arg.Items)
                {
                    if (item != null && item != string.Empty)
                    {
                        eventsList.Items.Insert(0, item);
                    }
                }
            }
        }

        private void Avsluta_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void purchaseButton_Click(object sender, EventArgs e)
        {

            if (amount.Text.Length == 0)
                amount.Text = "0";
            if (VAT.Text.Length == 0)
                VAT.Text = "0";
            if (cashBack.Text.Length == 0)
                cashBack.Text = "0";
           
            var amountsToSend = GetAmountsToSend();
            Terminal.StartaKöp();
            Terminal.SkickaBelopp(amountsToSend.net, amountsToSend.vat, amountsToSend.chash);

        }

        private AmountsToSend GetAmountsToSend()
        {
            AmountsToSend amountsToSend = new AmountsToSend();

            if (amount.Text.Length > 0 && Int32.Parse(amount.Text) > 99)
            {
                amountsToSend.vat = Int32.Parse(VAT.Text);
                amountsToSend.net = Int32.Parse(amount.Text);
                amountsToSend.chash = Int32.Parse(cashBack.Text);               
            }

            return amountsToSend;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Terminal.Cancel();
        }

        private void KassaMain_Load(object sender, EventArgs e)
        {

        }

        private void avlustaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kopplaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectToTerminal();
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            EditSettings editSettings = new EditSettings();
            editSettings.StartPosition = FormStartPosition.CenterParent;
            editSettings.ShowDialog();
        }
    }


    struct AmountsToSend
    {
        public int net;
        public int vat;
        public int chash;
    }
}
