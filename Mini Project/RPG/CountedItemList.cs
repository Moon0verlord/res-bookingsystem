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
        TheCountedItemList.Add(item);
        CountedItem existingCountedItem = (item);
        if (existingCountedItem != null)
        {
            existingCountedItem.Quantity++;
        }
        else
        {
            TheCountedItemList.Add(new CountedItem(item.TheItem, 1));
        }
    }

    public void AddItem(Item item)
    {
        TheCountedItemList.Add(new CountedItem(item,1));
    }

    
    public void RemoveItem(CountedItem item)
    {
        {
            TheCountedItemList.Remove(item);
        }
    }
}
