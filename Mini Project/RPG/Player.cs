﻿using System;

namespace RPG;

public class Player
{
    public string Name;
    public readonly int MaxHP;
    public static int CurrentHP;
    public static int Gold;
    public static int XP;
    public static int Level;
    public static Weapon CurrentWeapon;
    public static Location CurrentLocation;
    public static CountedItemList Inventory;
    
    public Player(string Name, int MaxHP, int currentHP, int gold,
        int xp, int level, Weapon currentWeapon, Location currentLocation)
    {
        this.Name = Name;
        this.MaxHP = MaxHP;
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
        Console.WriteLine($"Your current weapon is: {CurrentWeapon.Name}.\n");
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
}