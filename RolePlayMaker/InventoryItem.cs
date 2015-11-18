using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayMaker
{
    [Serializable]
    public class InventoryItem : IComparable<InventoryItem>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public int Count { get; set; }
        public int WeightOfOneItem { get; set; }
        public int Weight
        {
            get
            {
                return Count * WeightOfOneItem;
            }
        }
        public ItemType Type { get; set; }
        public bool Wearing { get; set; }

        public InventoryItem(string name, string desc, string icon, int count, int weightOfOneItem, ItemType it, bool wearing)
        {
            Name = name;
            Description = desc;
            IconPath = icon;
            Count = count;
            WeightOfOneItem = weightOfOneItem;
            Type = it;
            Wearing = wearing;
        }

        public int CompareTo(InventoryItem other)
        {
            int c = Wearing.CompareTo(other.Wearing);

            if (c != 0)
                return -c;
            else
                return Name.CompareTo(other.Name);
        }
    }
}
