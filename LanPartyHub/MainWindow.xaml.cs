using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            IPAddress newaddress = IPAddress.Parse("10.0.0.85");

            InitializeComponent();
        }

        private void Doom2MouseDown(object sender, MouseButtonEventArgs e)
        {
            var doomWindow = new Doom2Window(this);
            App.Current.MainWindow = doomWindow;
            doomWindow.Show();
            Hide();
        }

        private void War2_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Doom2MouseEnter(object sender, MouseEventArgs e)
        {
        }
    }
}
