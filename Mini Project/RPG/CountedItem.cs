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

    public void UseQuantity()
    {
        Quantity = Quantity - 1;
    }
    public static CountedItem apples = new (Item.apple, 5);
}