﻿namespace RPG;

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
    }
    public static void ViewInventory()
    {
        Console.WriteLine("You have the following items in your inventory:");
        Console.WriteLine($"Your current weapon is: {CurrentWeapon.Name}.");

        foreach (var item in Inventory.TheCountedItemList)
        {
            if (item.Quantity == 1)
            {
                Console.WriteLine($"{item.Quantity} {item.TheItem.Name}");
            }
            else
            {
                Console.WriteLine($"{item.Quantity} {item.TheItem.NamePlural}");
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
        // TODO: Implement this method
    }
}