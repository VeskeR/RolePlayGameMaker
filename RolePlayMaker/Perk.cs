using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayMaker
{
    [Serializable]
    public class Perk : IComparable<Perk>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PerkType Type { get; set; }

        public Perk(string name, string desc, PerkType type)
        {
            Name = name;
            Description = desc;
            Type = type;
        }

        public int CompareTo(Perk other)
        {
            int c1 = Type.CompareTo(other.Type);

            if (c1 != 0)
                return -c1;
            else
                return Name.CompareTo(other.Name);
        }
    }
}
