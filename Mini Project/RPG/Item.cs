namespace RPG;

public class Item
{
    public int ID;
    public string Name;
    public string NamePlural;

    public Item(int id,string name,string namePlural)
    {
        ID = id;
        Name = name;
        NamePlural = namePlural;
    }
    public static Item rock = new Item(1, "rock", "rocks");
}