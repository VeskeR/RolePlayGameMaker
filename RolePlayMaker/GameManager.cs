using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RolePlayMaker
{
    public class GameManager
    {
        public List<CharacterCard> Characters { get; set; }
        public int Gold { get; set; }
        public int Ore { get; set; }

        public GameManager()
        {
            Characters = new List<CharacterCard>();
            Gold = 0;
            Ore = 0;
        }

        public void AddCharacter(CharacterInfo ci)
        {
            Characters.Add(new CharacterCard(ci));
        }

        public void RemoveCharacter(string charName)
        {
            Characters.RemoveAt(Characters.FindIndex(cc => cc.CharName == charName));
        }

        public void AddCheerfulnessToAll(int count)
        {
            foreach (var cc in Characters)
                cc.Cheerfulness += count;
        }

        public void AddFoodToAll(int count)
        {
            foreach (var cc in Characters)
                cc.Food += count;
        }

        public void SaveGame(string fileName)
        {
            List<CharacterInfo> ciList = new List<CharacterInfo>();
            foreach (var cc in Characters)
                ciList.Add(cc.GetCharacterInfo());

            RolePlayGame game = new RolePlayGame(ciList, Gold, Ore);

            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, game);
            }
        }

        public void LoadGame(string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            RolePlayGame game = null;

            using (Stream fStream = File.OpenRead(fileName))
            {
                game = (RolePlayGame)binFormat.Deserialize(fStream);
            }

            Characters = new List<CharacterCard>();

            foreach (var ci in game.Characters)
                Characters.Add(new CharacterCard(ci));
            Gold = game.Gold;
            Ore = game.Ore;
        }
    }
}
