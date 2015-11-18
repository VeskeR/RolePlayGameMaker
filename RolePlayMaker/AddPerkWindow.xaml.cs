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

namespace RolePlayMaker
{
    /// <summary>
    /// Interaction logic for AddPerkWindow.xaml
    /// </summary>
    public partial class AddPerkWindow : Window
    {
        private Action<string, string, PerkType> _cb;

        public AddPerkWindow(Action<string, string, PerkType> callback)
        {
            InitializeComponent();

            _cb = callback;
        }

        private void AddPerk(object sender, RoutedEventArgs e)
        {
            string name = TxtBoxPerkName.Text;
            string desc = TxtBoxPerkDescription.Text;

            if (name.Length == 0 || desc.Length == 0)
            {
                MessageBox.Show("Перк должен иметь название и описание");
                return;
            }

            PerkType pt = PerkType.Perk;

            if ((bool)RdBtnPerk.IsChecked)
                pt = PerkType.Perk;
            if ((bool)RdBtnPositiveEffect.IsChecked)
                pt = PerkType.PositiveEffect;
            if ((bool)RdBtnNegativeEffect.IsChecked)
                pt = PerkType.NegativeEffect;

            _cb(name, desc, pt);
            this.Close();
        }
    }
}
