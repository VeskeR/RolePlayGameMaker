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
using System.Windows.Forms;

namespace RolePlayMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameManager _gm;

        public MainWindow()
        {
            InitializeComponent();

            _gm = new GameManager();
        }

        private void AddCheerfulnessForAll(object sender, RoutedEventArgs e)
        {
            int count;
            if (int.TryParse(TxtBoxCheerfulnessForAll.Text, out count))
                _gm.AddCheerfulnessToAll(count);
            TxtBoxCheerfulnessForAll.Text = string.Empty;
        }

        private void AddFoodForAll(object sender, RoutedEventArgs e)
        {
            int count;
            if (int.TryParse(TxtBoxFoodForAll.Text, out count))
                _gm.AddFoodToAll(count);
            TxtBoxFoodForAll.Text = string.Empty;
        }

        private void AddGold(object sender, RoutedEventArgs e)
        {
            int count;
            if (int.TryParse(TxtBoxAddGold.Text, out count))
                _gm.Gold += count;
            TxtBoxAddGold.Text = string.Empty;
            UpdateTextBlocks();
        }

        private void AddOre(object sender, RoutedEventArgs e)
        {
            int count;
            if (int.TryParse(TxtBoxAddOre.Text, out count))
                _gm.Ore += count;
            TxtBoxAddOre.Text = string.Empty;
            UpdateTextBlocks();
        }

        private void UpdateCharacterCards()
        {
            StackPnlCharacters.Children.Clear();

            foreach (var cc in _gm.Characters)
                StackPnlCharacters.Children.Add(cc);
        }

        private void UpdateTextBlocks()
        {
            TxtBlockGold.Text = _gm.Gold.ToString();
            TxtBlockOre.Text = _gm.Ore.ToString();
        }

        private void AddCharacter(object sender, RoutedEventArgs e)
        {
            AddCharacterWindow w = new AddCharacterWindow(AddCharacterCard);
            w.ShowDialog();
        }

        private void AddCharacterCard(CharacterInfo ci)
        {
            _gm.Characters.Add(new CharacterCard(ci));
            UpdateCharacterCards();
        }

        private void OpenGameCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RPM Files |*.rpm";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _gm.LoadGame(dlg.FileName);
                UpdateCharacterCards();
            }
        }

        private void OpenGameCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveGameCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "RPM Files |*.rpm";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _gm.SaveGame(dlg.FileName);
            }
        }

        private void SaveGameCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
