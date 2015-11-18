using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayMaker
{
    [Serializable]
    public class Perks : IEnumerable<Perk>
    {
        public List<Perk> PerksList { get; set; }

        public Perks()
            :this(new List<Perk>())
        {

        }

        public Perks(IEnumerable<Perk> perks)
        {
            PerksList = new List<Perk>();
            PerksList.AddRange(perks);
        }

        public void AddPerk(string name, string desc, PerkType type)
        {
            if (!PerksList.Exists(p => p.Name == name))
                PerksList.Add(new Perk(name, desc, type));

            SortPerks();
        }

        public void RemovePerk(int index)
        {
            PerksList.RemoveAt(index);

            SortPerks();
        }

        private void SortPerks()
        {
            PerksList.Sort();
        }

        public IEnumerator<Perk> GetEnumerator()
        {
            return PerksList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
