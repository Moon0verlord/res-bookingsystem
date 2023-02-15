namespace RPG;

public class CountedItemList
{
    public List<CountedItem> TheCountedItemList;

    public void AddItem(Item item)
    {
        TheCountedItemList.Add(new CountedItem(item,1));
    }
}
