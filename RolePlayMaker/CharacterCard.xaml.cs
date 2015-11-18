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

namespace RolePlayMaker
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CharacterCard : UserControl
    {
        private string _name;

        private int _hp;
        private int _mp;
        private int _cheerfulness;
        private int _food;

        private int _maxHp;
        private int _maxMp;
        private int _maxCheerfulness;
        private int _maxFood;

        public string CharName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                TxtBlockCharName.Text = "Имя: " + value;
            }
        }

        public int HP
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
                if (_hp > MaxHP)
                    _hp = MaxHP;
                if (_hp < 0)
                    _hp = 0;

                UpdateStatField("CurrentHP", _hp);
            }
        }
        public int MP
        {
            get
            {
                return _mp;
            }
            set
            {
                _mp = value;
                if (_mp > MaxMP)
                    _mp = MaxMP;
                if (_mp < 0)
                    _mp = 0;

                UpdateStatField("CurrentMP", _mp);
            }
        }
        public int Cheerfulness
        {
            get
            {
                return _cheerfulness;
            }
            set
            {
                _cheerfulness = value;
                if (_cheerfulness > MaxCheerfulness)
                    _cheerfulness = MaxCheerfulness;
                if (_cheerfulness < 0)
                    _cheerfulness = 0;

                UpdateStatField("CurrentCheerfulness", _cheerfulness);
            }
        }
        public int Food
        {
            get
            {
                return _food;
            }
            set
            {
                _food = value;
                if (_food > MaxFood)
                    _food = MaxFood;
                if (_food < 0)
                    _food = 0;

                UpdateStatField("CurrentFood", _food);
            }
        }

        public int MaxHP
        {
            get
            {
                return _maxHp;
            }
            set
            {
                _maxHp = value;
                if (_maxHp < 0)
                    _maxHp = 0;
                HP = HP;
                UpdateStatField("MaxHP", _maxHp);
            }
        }
        public int MaxMP
        {
            get
            {
                return _maxMp;
            }
            set
            {
                _maxMp = value;
                if (_maxMp < 0)
                    _maxMp = 0;
                MP = MP;
                UpdateStatField("MaxMP", _maxMp);
            }
        }
        public int MaxCheerfulness
        {
            get
            {
                return _maxCheerfulness;
            }
            set
            {
                _maxCheerfulness = value;
                if (_maxCheerfulness < 0)
                    _maxCheerfulness = 0;
                Cheerfulness = Cheerfulness;
                UpdateStatField("MaxCheerfulness", _maxCheerfulness);
            }
        }
        public int MaxFood
        {
            get
            {
                return _maxFood;
            }
            set
            {
                _maxFood = value;
                if (_maxFood < 0)
                    _maxFood = 0;
                Food = Food;
                UpdateStatField("MaxFood", _maxFood);
            }
        }

        public Perks Perks { get; set; }
        public Inventory Inventory { get; set; }

        public CharacterCard(string charName)
        {
            InitializeComponent();

            CharName = charName;
            SetDefaultStats();

            UpdatePerks();
            UpdateInventory();
        }

        public CharacterCard(CharacterInfo ci)
        {
            InitializeComponent();

            CharName = ci.Name;

            MaxHP = ci.MaxHP;
            MaxMP = ci.MaxMP;
            MaxCheerfulness = ci.MaxCheerfulness;
            MaxFood = ci.MaxFood;

            HP = ci.HP;
            MP = ci.MP;
            Cheerfulness = ci.Cheerfulness;
            Food = ci.Food;

            Perks = ci.Perks;
            Inventory = ci.Inventory;

            UpdatePerks();
            UpdateInventory();
        }

        public CharacterInfo GetCharacterInfo()
        {
            return new CharacterInfo(CharName, HP, MP, Cheerfulness, Food, MaxHP, MaxMP, MaxCheerfulness, MaxFood, Perks, Inventory);
        }

        private void SetDefaultStats()
        {
            MaxHP = 100;
            MaxMP = 0;
            MaxCheerfulness = 100;
            MaxFood = 100;

            HP = 100;
            MP = 0;
            Cheerfulness = 100;
            Food = 100;

            Perks = new RolePlayMaker.Perks();
            Inventory = new Inventory();
        }

        private void AddHP(object sender, RoutedEventArgs e)
        {
            HP += GetAddStat("HP");
        }

        private void AddMP(object sender, RoutedEventArgs e)
        {
            MP += GetAddStat("MP");
        }

        private void AddCheerfulness(object sender, RoutedEventArgs e)
        {
            Cheerfulness += GetAddStat("Cheerfulness");
        }

        private void AddFood(object sender, RoutedEventArgs e)
        {
            Food += GetAddStat("Food");
        }

        private int GetAddStat(string stat)
        {
            TextBox statTxtBox = this.FindName("TxtBoxAdd" + stat) as TextBox;

            int addStat;
            string text = statTxtBox.Text;
            statTxtBox.Text = string.Empty;
            if (int.TryParse(text, out addStat))
                return addStat;
            else
                return 0;
        }

        private void AddConcreteHP(object sender, RoutedEventArgs e)
        {
            HP += GetConcreteAddStat(sender);
        }

        private void AddConcreteMP(object sender, RoutedEventArgs e)
        {
            MP += GetConcreteAddStat(sender);
        }

        private void AddConcreteCheerfulness(object sender, RoutedEventArgs e)
        {
            Cheerfulness += GetConcreteAddStat(sender);
        }

        private void AddConcreteFood(object sender, RoutedEventArgs e)
        {
            Food += GetConcreteAddStat(sender);
        }

        private int GetConcreteAddStat(object sender)
        {
            string addStr = (sender as Button).Content.ToString();

            int addStat = int.Parse(addStr.Substring(1));
            addStat *= addStr[0] == '-' ? -1 : 1;

            return addStat;
        }

        private void UpdateStatField(string stat, int value)
        {
            TextBlock currStatTxtBlock = this.FindName("TxtBlock" + stat) as TextBlock;
            currStatTxtBlock.Text = value.ToString();
        }

        private void AddNewItem(object sender, RoutedEventArgs e)
        {
            AddItemWindow w = new AddItemWindow(AddNewItemToInventory);
            w.ShowDialog();
        }

        public void AddNewItemToInventory(string itemName, string itemDescription, string itemIconPath, int count, int weight, ItemType it)
        {
            Inventory.AddNewItemToInventory(itemName, itemDescription, itemIconPath, count, weight, it);
            UpdateInventory();
        }

        private void IncreaseItemQuantity(object sender, RoutedEventArgs e)
        {
            int i = GetSelectedItemIndex();
            if (i >= 0 && i < Inventory.Count())
            {
                Inventory.AddItem(i, 1);
                UpdateInventory();
            }
        }

        private void DecreaseItemQuantity(object sender, RoutedEventArgs e)
        {
            int i = GetSelectedItemIndex();
            if (i >= 0 && i < Inventory.Count())
            {
                Inventory.RemoveItem(i, 1);
                UpdateInventory();
            }
        }

        private void WearUpItem(object sender, RoutedEventArgs e)
        {
            int i = GetSelectedItemIndex();
            if (i >= 0 && i < Inventory.Count())
            {
                Inventory.WearUpItem(i);
                UpdateInventory();
            }
        }

        private void UnwearItem(object sender, RoutedEventArgs e)
        {
            int i = GetSelectedItemIndex();
            if (i >= 0 && i < Inventory.Count())
            {
                Inventory.UnwearItem(i);
                UpdateInventory();
            }
        }

        private int GetSelectedItemIndex()
        {
            return ListBoxInventory.SelectedIndex;
        }

        private void UpdateInventory()
        {
            ListBoxInventory.Items.Clear();

            foreach (var t in Inventory)
            {
                string item = t.Name + " x" + t.Count;

                ListBoxItem lbi = new ListBoxItem();
                lbi.Content = item;
                lbi.Background = t.Wearing ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFE0AB")) : Brushes.White;

                ListBoxInventory.Items.Add(lbi);
            }
        }

        private void AddPerk(object sender, RoutedEventArgs e)
        {
            AddPerkWindow w = new AddPerkWindow(AddPerkToPerksList);
            w.ShowDialog();
        }

        public void AddPerkToPerksList(string perkName, string description, PerkType type)
        {
            Perks.AddPerk(perkName, description, type);
            UpdatePerks();
        }

        private void RemovePerk(object sender, RoutedEventArgs e)
        {
            int i = GetSelectedPerkIndex();
            if (i >= 0 && i < Perks.Count())
            {
                Perks.RemovePerk(GetSelectedPerkIndex());
                UpdatePerks();
            }
        }

        private int GetSelectedPerkIndex()
        {
            return ListBoxPerks.SelectedIndex;
        }

        private void UpdatePerks()
        {
            ListBoxPerks.Items.Clear();

            foreach (var p in Perks)
            {
                string perk = p.Name;

                ListBoxItem lbi = new ListBoxItem();
                lbi.Content = perk;

                switch (p.Type)
                {
                    case PerkType.Perk:
                        lbi.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFE0AB"));
                        break;
                    case PerkType.PositiveEffect:
                        lbi.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3FFAB"));
                        break;
                    case PerkType.NegativeEffect:
                        lbi.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFABAB"));
                        break;
                }

                lbi.Margin = new Thickness(2);

                ListBoxPerks.Items.Add(lbi);
            }
        }

        private void AddOneToMaxHP(object sender, RoutedEventArgs e)
        {
            MaxHP++;
        }

        private void RemoveOneFromMaxHP(object sender, RoutedEventArgs e)
        {
            MaxHP--;
        }

        private void AddOneToMaxMP(object sender, RoutedEventArgs e)
        {
            MaxMP++;
        }

        private void RemoveOneFromMaxMP(object sender, RoutedEventArgs e)
        {
            MaxMP--;
        }
        }
    }
}
