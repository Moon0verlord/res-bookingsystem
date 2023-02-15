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
    public static CountedItem rocks = new CountedItem(Item.rock, 1);
}