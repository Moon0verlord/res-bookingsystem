using System.Collections.Generic;

namespace RPG;

public class CountedItemList
{
    public List<CountedItem> TheCountedItemList;

    public CountedItemList()
    {
        TheCountedItemList = new List<CountedItem>();
    }
    public void AddCountedItem(CountedItem item)
    {
        bool itemExists = false;
        foreach (CountedItem countedItem in TheCountedItemList)
        {
            if (countedItem.TheItem.Name == item.TheItem.Name)
            {
                countedItem.Quantity++;
                itemExists = true;
                break;
            }
        }
        if (!itemExists)
        {
            TheCountedItemList.Add(item);
        }
    }


    public void AddItem(Item item)
    {
        TheCountedItemList.Add(new CountedItem(item,1));
    }

    
    public void RemoveItem(CountedItem item)
    {
        {
            CountedItem ItemToRemove = null;
            foreach (CountedItem CountItem in TheCountedItemList)
            {
                if (CountItem.TheItem.ID == item.TheItem.ID)
                {
                    ItemToRemove = CountItem;
                    break;
                }
            }
            if (ItemToRemove != null) TheCountedItemList.Remove(ItemToRemove);
        }
    }
}
