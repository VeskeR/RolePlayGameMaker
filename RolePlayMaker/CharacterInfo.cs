using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayMaker
{
    [Serializable]
    public class CharacterInfo
    {
        public string Name { get; set; }

        public int HP { get; set; }
        public int MP { get; set; }
        public int Cheerfulness { get; set; }
        public int Food { get; set; }

        public int MaxHP { get; set; }
        public int MaxMP { get; set; }
        public int MaxCheerfulness { get; set; }
        public int MaxFood { get; set; }

        public Perks Perks { get; set; }
        public Inventory Inventory { get; set; }

        public CharacterInfo(string name, int hp, int mp, int cheerfulness, int food, int maxHp, int maxMp, int maxCheerfulness, int maxFood, Perks perks, Inventory inventory)
        {
            Name = name;

            HP = hp;
            MP = mp;
            Cheerfulness = cheerfulness;
            Food = food;

            MaxHP = maxHp;
            MaxMP = maxMp;
            MaxCheerfulness = maxCheerfulness;
            MaxFood = maxFood;

            Perks = perks;
            Inventory = inventory;
        }
    }
}
