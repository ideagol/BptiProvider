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
    public partial class EditSettings : Form
    {
        public EditSettings()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            tcp.Text = Properties.Settings.Default.TerminalTcp;
            port.Text = Properties.Settings.Default.TerminalPort.ToString();
            api.Text = Properties.Settings.Default.BoplatsApiUrl;
            printerwindow.Checked = Properties.Settings.Default.UsePrinterWindow;

        }

        private void save_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.TerminalTcp = tcp.Text;
            Properties.Settings.Default.TerminalPort = int.Parse(port.Text);
            Properties.Settings.Default.BoplatsApiUrl = api.Text;
            Properties.Settings.Default.UsePrinterWindow = printerwindow.Checked;
            Properties.Settings.Default.Save();

            this.Close();            
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
