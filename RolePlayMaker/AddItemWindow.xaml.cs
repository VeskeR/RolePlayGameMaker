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
using System.Windows.Forms;
using System.IO;

namespace RolePlayMaker
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        private Action<string, string, string, int, int, ItemType> _cb;
        private string _iconPath = string.Empty;

        public AddItemWindow(Action<string, string, string, int, int, ItemType> callback)
        {
            InitializeComponent();

            _cb = callback;
        }

        private void SelectIcon(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!Directory.Exists("Images"))
                    Directory.CreateDirectory("Images");

                string safeFileName = dlg.SafeFileName;

                File.Copy(dlg.FileName, "Images/" + safeFileName);

                ImgIcon.Source = new BitmapImage(new Uri("Images/" + safeFileName));
                _iconPath = "Images/" + safeFileName;
            }
        }

        private void DeselectIcon(object sender, RoutedEventArgs e)
        {
            _iconPath = string.Empty;
            ImgIcon.Source = new BitmapImage();
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            string itemName = TxtBoxItemName.Text;
            string itemDescription = TxtBoxItemDescription.Text;

            if (itemName.Length == 0 || itemDescription.Length == 0)
            {
                System.Windows.MessageBox.Show("Предмет должен иметь название и описание");
                return;
            }

            string iconPath = _iconPath;

            int count = 0;
            int weight = 0;

            try
            {
                count = int.Parse(TxtBoxCount.Text);
                weight = int.Parse(TxtBoxWeight.Text);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Количество предметов и их вес должны быть целыми числами");
                return;
            }
            
            ItemType it = ItemType.Other;
            if ((bool)RdBtnHelmet.IsChecked)
                it = ItemType.Helmet;
            else if ((bool)RdBtnChest.IsChecked)
                it = ItemType.Chest;
            else if ((bool)RdBtnLegs.IsChecked)
                it = ItemType.Legs;
            else if ((bool)RdBtnFeet.IsChecked)
                it = ItemType.Feet;
            else if ((bool)RdBtnOneHandedWeapon.IsChecked)
                it = ItemType.OneHandedWeapon;
            else if ((bool)RdBtnTwoHandedWeapon.IsChecked)
                it = ItemType.TwoHandedWeapon;
            else if ((bool)RdBtnRing.IsChecked)
                it = ItemType.Ring;
            else if ((bool)RdBtnAmulet.IsChecked)
                it = ItemType.Amulet;
            else if ((bool)RdBtnOther.IsChecked)
                it = ItemType.Other;

            _cb(itemName, itemDescription, iconPath, count, weight, it);
            this.Close();
        }
    }
}
