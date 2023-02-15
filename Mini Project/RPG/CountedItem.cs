namespace RPG;

public class CountedItem
{
    public Item TheItem;
    public int Quantity;

    public CountedItem(Item theItem, int quantity)
    {
        TheItem = theItem;
        Quantity = quantity;
    }
    //public static Item rockItem = new Item(1, "rock", "rocks");
    //public CountedItem rockCountedItem = new CountedItem(rockItem, 1);
}