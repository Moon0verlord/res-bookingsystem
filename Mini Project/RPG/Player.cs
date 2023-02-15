namespace RPG;

public class Player
{
    public string Name;
    public static int MaxHP;
    public static int CurrentHP;
    public static int Gold;
    public static int XP;
    public static int Level;
    public static Weapon CurrentWeapon;
    public static Location CurrentLocation;
    public static CountedItemList Inventory;
    
    public Player(string Name, int MaxHP, int currentHP, int gold,
        int xp, int level, Weapon currentWeapon, Location currentLocation, CountedItemList inventory)
    {
        this.Name = Name;
        MaxHP = 100;
        CurrentHP = currentHP;
        Gold = gold;
        XP = xp;
        Level = level;
        CurrentWeapon = currentWeapon;
        CurrentLocation = currentLocation;
        Inventory = new CountedItemList();
    }
    public static void ViewInventory()
    {
        Console.WriteLine("You have the following items in your inventory:");
        Console.WriteLine(CountedItem.rocks);
        Console.WriteLine(CountedItem.rocks.Quantity);
        Console.WriteLine(Item.rock);
        Console.WriteLine(Item.rock.Name);
        Console.WriteLine(Item.rock.NamePlural);
        Console.WriteLine(Inventory);
        //Inventory.AddCountedItem(CountedItem.rocks);
        Console.WriteLine(Inventory);
        var countries = new List<string>();
        Console.WriteLine(countries);

    }
}