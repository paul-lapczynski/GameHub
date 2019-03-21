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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LanPartyHub
{
    /// <summary>
    /// Interaction logic for DOSBoxSettingCard.xaml
    /// </summary>
    public partial class DOSBoxSettingCard : UserControl
    {
        private DOSBoxSettingOverride _setting;

        public DOSBoxSettingCard()
        {
            InitializeComponent();
        }


        public void Init(DOSBoxSettingOverride setting)
        {
            _setting = setting;
            Render(setting);
        }

        private void Ready(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Render(DOSBoxSettingOverride setting)
        {
            OptionLabel.Content = setting.Name;
            OptionsCombo.DisplayMemberPath = "Value";
            OptionsCombo.SelectedValuePath = "Value";

            OptionsCombo.ItemsSource = setting.Values;
            OptionsCombo.SelectedValue = setting.SelectedValue;
        }
    }
}
