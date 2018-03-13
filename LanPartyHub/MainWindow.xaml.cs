using LanPartyHub.Managers;
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
            InitializeComponent();
        }

        private void Doom2MouseDown(object sender, MouseButtonEventArgs e)
        {
            var doomWindow = new Doom2Window(this, 1);
            Application.Current.MainWindow = doomWindow;
            doomWindow.Show();
            Hide();
        }

        private void GameImageClick(object sender, MouseButtonEventArgs e)
        {
            var doomWindow = new Doom2Window(this, (e.Source as GameImage).GameId);
            Application.Current.MainWindow = doomWindow;
            doomWindow.Show();
            Hide();
        }

        private void War2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var war2Window = new War2Window(this, 2);
            Application.Current.MainWindow = war2Window;
            war2Window.Show();
            Hide();
        }

        private void GameInage_Loaded(object sender, RoutedEventArgs e)
        {
            GameInage.Init(ApplicationManager.Settings.Games[0]);
        }

        private void MainConfig_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var Window = new DOSBoxConfigWindow(this);
            Application.Current.MainWindow = Window;
            Window.Show();
            Hide();
        }
    }
}
