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
    /// Interaction logic for AddCharacterWindow.xaml
    /// </summary>
    public partial class AddCharacterWindow : Window
    {
        private Action<CharacterInfo> _cb;

        public AddCharacterWindow(Action<CharacterInfo> callback)
        {
            InitializeComponent();

            _cb = callback;
        }

        private void AddCharacter(object sender, RoutedEventArgs e)
        {
            string name = TxtBoxName.Text;

            if (name.Length == 0)
            {
                MessageBox.Show("Имя персонажа должно содержать минимум 1 символ");
                return;
            }

            int currHP, currMP, currCheerfulness, currFood, maxHP, maxMP, maxCheerfulness, maxFood;
            try
            {
                currHP = int.Parse(TxtBoxCurrentHP.Text);
                currMP = int.Parse(TxtBoxCurrentMP.Text);
                currCheerfulness = int.Parse(TxtBoxCurrentCheerfulness.Text);
                currFood = int.Parse(TxtBoxCurrentFood.Text);
                maxHP = int.Parse(TxtBoxMaxHP.Text);
                maxMP = int.Parse(TxtBoxMaxMP.Text);
                maxCheerfulness = int.Parse(TxtBoxMaxCheerfulness.Text);
                maxFood = int.Parse(TxtBoxMaxFood.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("Поля статистики персонажа должны содержать только целые числа");
                return;
            }

            CharacterInfo ci = new CharacterInfo(name, currHP, currMP, currCheerfulness, currFood, maxHP, maxMP, maxCheerfulness, maxFood, new Perks(), new Inventory());

            _cb(ci);
            this.Close();
        }
    }
}
