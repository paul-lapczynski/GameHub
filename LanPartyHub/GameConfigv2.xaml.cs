using LanPartyHub.Models;
using LanPartyHub.Models.DOSBox;
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
using System.Windows.Shapes;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for GameConfigv2.xaml
    /// </summary>
    public partial class GameConfigv2 : Window
    {
        public GameConfigv2()
        {
            InitializeComponent();
            Test();
        }

        public void Test()
        {
            var combo = new ComboBox();
            combo.DisplayMemberPath = "Value";
            combo.SelectedValuePath = "Key";

            combo.ItemsSource = new List<KeyValue> { new KeyValue { Key = "GG", Value = "GG" } };

            icSettingsList.ItemsSource = new List<DOSBoxSettingOverride> { new DOSBoxSettingOverride { }, new DOSBoxSettingOverride { }
            , new DOSBoxSettingOverride { }, new DOSBoxSettingOverride { }, new DOSBoxSettingOverride { }, new DOSBoxSettingOverride { }, new DOSBoxSettingOverride { }};
        }

        public void CardLoaded(object sender, RoutedEventArgs e)
        {
            var card = (DOSBoxSettingCard)e.Source;
            var setting = (DOSBoxSettingOverride)card.DataContext;
            card.Init(setting);
        }
    }
}
