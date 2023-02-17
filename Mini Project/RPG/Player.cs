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
    public static QuestList QuestLog;
    
    public Player(string Name, int MaxHP, int currentHP, int gold,
        int xp, int level, Weapon currentWeapon, Location currentLocation)
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
        QuestLog = new QuestList();
        Inventory.AddCountedItem(CountedItem.rocks);
        Inventory.AddCountedItem(CountedItem.rocks);
    }
    
    public static void ViewInventory()
    {
        Console.WriteLine($"Your current weapon is: {CurrentWeapon.Name}.");
        Console.WriteLine("You have the following items in your inventory:");
        
        foreach (var item in Inventory.TheCountedItemList)
        {
            if (item.Quantity == 1)
            {
                Console.WriteLine($"{item.Quantity} {item.TheItem.Name}\n");
            }
            else
            {
                Console.WriteLine($"{item.Quantity} {item.TheItem.NamePlural}\n");
            }
        }
    }
    
    public static void ViewQuestLog()
    {
        Console.WriteLine("You have the following quests in your quest log:");
        foreach (var quest in QuestLog.QuestLog)
        {
            Console.WriteLine($"{quest.TheQuest.Name}");
        }
    }

    public static void UseItem()
    {
            Console.WriteLine("Which item would you like to use?");
            string? itemToUse = Console.ReadLine();
            foreach (var item in Inventory.TheCountedItemList)
            {
                if (item.TheItem.Name == itemToUse)
                {
                    item.UseQuantity();
                    if (item.Quantity == 0)
                    {
                        Inventory.RemoveItem(item);
                    }
                    Console.WriteLine($"You used {itemToUse}.");
                    break;
                }
            }
    }
}

// TODO add an (is in inventory) method to check for quest items in inventory and returns true or false