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
        public static readonly DependencyProperty SettingProperty = DependencyProperty.Register("Setting", typeof(DOSBoxSettingForConfig), typeof(DOSBoxSettingCard), new PropertyMetadata());

        public DOSBoxSettingForConfig Setting
        {
            get { return (DOSBoxSettingForConfig)GetValue(SettingProperty); }
            set { SetValue(SettingProperty, value); }

        }

        public DOSBoxSettingCard()
        {
            InitializeComponent();
        }

        private void OptionsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Setting.SelectedValue = (string)e.AddedItems[0];

            var binder = GetBindingExpression(SettingProperty);
            binder.UpdateSource();
        }
    }
}
