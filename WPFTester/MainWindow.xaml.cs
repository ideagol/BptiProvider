using Momentum.Ekonomi.Payments.Terminal.Bpti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BptiProvider Terminal;
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Terminal.Anslut("127.0.0.1", 2000, null);
            Terminal.Betala(100, 25, 0);
        }
    }
}
