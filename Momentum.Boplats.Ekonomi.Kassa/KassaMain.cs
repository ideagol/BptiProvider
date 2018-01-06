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
            Terminal.TerminalReceiptEvent += Terminal_TerminalReceiptEvent;

        }

        private void Terminal_TerminalReceiptEvent(object sender, EventArgs e)
        {

            var arg = e as DisplayEventArgs;

            if (arg != null && arg.Items.Count > 0)
            {
                foreach (var item in arg.Items)
                {
                    if (item != null )
                    {
                        receipt.Items.Add(item);
                    }
                }
            }

        }

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

        private void initButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Terminal.ConnectionInitiated)
                {
                    if (tcpIp.Checked)
                    {
                        Terminal.AnslutTCP(ipAddress.Text, 2000);
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
            catch (Exception)
            {

                throw;
            }

        }

        private void Avsluta_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void purchaseButton_Click(object sender, EventArgs e)
        {


            var amountsToSend = GetAmountsToSend();


            if (amount.Text.Length > 0 && Int32.Parse(amount.Text) > 99)
            {
                if (VAT.Text.Length == 0)
                    VAT.Text = "0";
                if (cashBack.Text.Length == 0)
                    cashBack.Text = "0";


            }
            Terminal.StartaKöp();
            Terminal.SkickaBelopp(amountsToSend.net, amountsToSend.vat, amountsToSend.chash);


        }

        private AmountsToSend GetAmountsToSend()
        {
            AmountsToSend amountsToSend = new AmountsToSend();
            
            amountsToSend.vat = 0;
            amountsToSend.net = 0;
            amountsToSend.chash = 0;
            
            if (amount.Text.Length > 0 && Int32.Parse(amount.Text) > 99)
            {
                if (VAT.Text.Length == 0)
                    VAT.Text = "0";
                if (cashBack.Text.Length == 0)
                    cashBack.Text = "0";


            }

            return amountsToSend;
        }
    }


    struct AmountsToSend
    {
        public int net;
        public int vat;
        public int chash;
    }
}
