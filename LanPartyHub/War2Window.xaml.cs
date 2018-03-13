using LanPartyHub.Managers;
using System;
using System.Diagnostics;
using System.Windows;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for War2Window.xaml
    /// </summary>
    public partial class War2Window : Window
    {
        MainWindow _main;
        War2Manager war2Manager;
        Process war2;

        public War2Window(MainWindow main, int gameId)
        {
            _main = main;
            war2Manager = new War2Manager(gameId);

            InitializeComponent();

            Closed += War2Window_Closed;
        }

        private void War2Window_Closed(object sender, EventArgs e)
        {
            if (war2 != null && !war2.HasExited)
            {
                war2.Kill();
            }
        }

        private void Unload(object sender, RoutedEventArgs e)
        {
            _main.Show();
        }

        private void StartWar2(object sender, RoutedEventArgs e)
        {
            war2 = DOSBoxManager.StartDOSBox(war2Manager.GetDOSBoxOptions(this));
        }
    }
}
