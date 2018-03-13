using LanPartyHub.Managers;
using System.Windows;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for War2Window.xaml
    /// </summary>
    public partial class DOSBoxConfigWindow : Window
    {
        MainWindow _main;

        public DOSBoxConfigWindow(MainWindow main)
        {
            _main = main;

            InitializeComponent();
            DOSBoxCPath.Text = ApplicationManager.Settings.VirtualDOSBoxCDrivePath;
        }

        private void Unload(object sender, RoutedEventArgs e)
        {
            _main.Show();
        }

        private void DOSBoxConfigSave(object sender, RoutedEventArgs e)
        {
            ApplicationManager.Settings.VirtualDOSBoxCDrivePath = DOSBoxCPath.Text.ToString();
            ApplicationManager.SaveSettings();
        }
}
}
