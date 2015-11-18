using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayMaker
{
    [Serializable]
    public class Inventory : IEnumerable<InventoryItem>
    {
        public List<InventoryItem> Items { get; set; }

        public Inventory()
            : this(new List<InventoryItem>())
        {

        }

        public Inventory(IEnumerable<InventoryItem> items)
        {
            Items = new List<InventoryItem>();
            Items.AddRange(items);
        }

        public void AddNewItemToInventory(string itemName, string desc, string iconPath, int count, int weight, ItemType it)
        {
            int i = Items.FindIndex(t => t.Name == itemName);

            if (i >= 0)
                AddItem(i, count);
            else
            {
                Items.Add(new InventoryItem(itemName, desc, iconPath, count, weight, it, false));
                SortItems();
            }
        }

        public void AddItem(int index, int count)
        {
            Items[index].Count += count;
        }

        public void DeleteItemFromInventory(int index)
        {
            Items.RemoveAt(index);
            SortItems();
        }

        public void RemoveItem(int index, int count)
        {
            AddItem(index, -count);
            if (Items[index].Count <= 0)
                DeleteItemFromInventory(index);
        }

        public void WearUpItem(int index)
        {
            if (Items[index].Wearing)
                return;

            ItemType itToWear = Items[index].Type;

            switch (itToWear)
            {
                case ItemType.Helmet:
                case ItemType.Chest:
                case ItemType.Legs:
                case ItemType.Feet:
                case ItemType.Amulet:
                    int i = Items.FindIndex(t => t.Type == itToWear && t.Wearing);

                    if (i >= 0 && i < Items.Count)
                        ChangeItemWear(i, false);

                    ChangeItemWear(index, true);
                    break;
                case ItemType.OneHandedWeapon:
                case ItemType.TwoHandedWeapon:
                    int countWeapons = 0;
                    Items.ForEach(t =>
                    {
                        if (t.Type == ItemType.OneHandedWeapon && t.Wearing)
                            countWeapons++;
                        else if (t.Type == ItemType.TwoHandedWeapon && t.Wearing)
                            countWeapons += 2;
                    });

                    if ((itToWear == ItemType.OneHandedWeapon && countWeapons > 1) || (itToWear == ItemType.TwoHandedWeapon && countWeapons > 0))
                        for (int j = 0; j < Items.Count; j++)
                            if ((Items[j].Type == ItemType.OneHandedWeapon || Items[j].Type == ItemType.TwoHandedWeapon) && Items[j].Wearing)
                                ChangeItemWear(j, false);

                    ChangeItemWear(index, true);
                    break;
                case ItemType.Ring:
                    int countRings = Items.Count(t => t.Type == ItemType.Ring && t.Wearing);

                    if (countRings > 1)
                        for (int j = 0; j < Items.Count; j++)
                            if (Items[j].Type == ItemType.Ring && Items[j].Wearing)
                                ChangeItemWear(j, false);

                    ChangeItemWear(index, true);
                    break;
                case ItemType.Other:
                    return;
            }

            SortItems();
        }

        public void UnwearItem(int index)
        {
            ChangeItemWear(index, false);
            SortItems();
        }

        private void ChangeItemWear(int index, bool wear)
        {
            Items[index].Wearing = wear;
        }

        private void SortItems()
        {
            Items.Sort();
        }

        public IEnumerator<InventoryItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
