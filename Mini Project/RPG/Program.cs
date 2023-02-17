using System;
using System.Net.Mime;
using RPG;

public class Program
{
    static void Main(string[] args)
    {
        var boolval = true;
        Console.WriteLine("The people in your town are being terrorized by giant spiders.\n" +
                          "You decide to do what you can to help.");
        Console.WriteLine("But first, who are you?");
        Console.Write("Enter your name: ");
        string name = Console.ReadLine()!;
        Player.CurrentWeapon = World.WeaponByID(World.WEAPON_ID_RUSTY_SWORD);
        Player.CurrentLocation = World.LocationByID(World.LOCATION_ID_FARM_FIELD);
        Player player = new Player(name,15,15,10,0,1, Player.CurrentWeapon,Player.CurrentLocation);
        Player.Inventory.TheCountedItemList.Add(new CountedItem(new Item(5,"Apple","Apples"),5));
        Player.Inventory.TheCountedItemList.Add(new CountedItem(new Item(World.WEAPON_ID_RUSTY_SWORD,
            World.WeaponByID(World.WEAPON_ID_RUSTY_SWORD).Name,World.WeaponByID(World.WEAPON_ID_RUSTY_SWORD).NamePlural),1));
        while (boolval)
        {
            try
            {
                Console.WriteLine("What would you like to do (Enter a number?).");
                Console.WriteLine("1: See game stats\n2: Move\n3: Fight\n4: Quit");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine($"Name: {name}.\nMax hp: {player.MaxHP}.\n" +
                                          $"Current hp: {Player.CurrentHP}.\nGold: {Player.Gold}."+
                        $"\nXp: {Player.XP}\nLevel: {Player.Level}.\nCurrent Weapon: {Player.CurrentWeapon.Name}."+
                                          $"\nCurrent Location: {Player.CurrentLocation.Name}.\nInventory Items:");
                        Player.ViewInventory();
                        break;
                    case 2:
                        Console.WriteLine("Where would you like to go?");
                        Console.WriteLine($"You are at: {Player.CurrentLocation.Name}.\n{Player.CurrentLocation.Description}." +
                                          $"\nFrom here you can go to:");
                        Console.WriteLine(Player.CurrentLocation.Compass());
                        Console.WriteLine(Player.CurrentLocation.Map());
                        break;
                    case 3:
                        if (Player.CurrentLocation.MonsterLivingHere != null 
                            && Player.CurrentLocation.MonsterLivingHere.CurrentHitPoints>0)
                        {
                            Console.WriteLine($"The {Player.CurrentLocation.MonsterLivingHere.Name} attacks!");
                            fight();
                        }
                        else
                        {
                            Console.WriteLine("There seems to be nothing here to fight..");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Unknown input");
                        break;
                }
            }
            catch (System.FormatException e)
            {
                Console.WriteLine("Invalid input. Please enter a valid option.\n");
            }
            
        }
    }

    public static void Farmer()
    {
        Console.WriteLine();
    }
    public static void Alchemist()
    {
        Console.WriteLine();
    }
    public static void Spider()
    {
        Console.WriteLine();
    }
    public static void Gate()
    {
        Console.WriteLine();
    }
    public static void fight()
    {
        Random rnd = new Random();
        var monster = Player.CurrentLocation.MonsterLivingHere;
        Console.WriteLine($"You have: {Player.CurrentHP} Hp");
        Console.WriteLine($"The {monster.Name} has: {monster.CurrentHitPoints} Hp\n");
        bool brave = true;
        while (brave && Player.CurrentHP > 0)
        {
            Console.WriteLine("What would you like to do?:" +
                          "\n1 Open Inventory (type inventory)." +
                          "\n2 Run." +
                          "\n3 Fight." +
                          "\n4 Observe.");
            var fightdo = Convert.ToInt32(Console.ReadLine());
            //open inventory-ToDO
            //run???-X
            //fight-X
            //observe-ToDo
            switch (fightdo)
            {
                    case 1:
                        Player.ViewInventory();
                        Console.WriteLine("Type the name of an item you'd like to use. Or type 'exit' to leave the inventory.");
                        var invChoice = Console.ReadLine();
                        foreach (var item in Player.Inventory.TheCountedItemList)
                        {
                            if (item.TheItem.Name == invChoice)
                            {
                                if (item.TheItem.Name == "Apple")
                                {
                                    Console.WriteLine("You take eat the apple");
                                    Console.WriteLine("You get three HP");
                                    Player.CurrentHP += 3;
                                    //Remove 1 apple from inventory toDo
                                    break;
                                }

                                else if (item.TheItem.Name == Player.CurrentWeapon.Name)
                                {
                                    Console.WriteLine("You already have this item equipped");
                                }
                                else
                                {
                                    foreach (Weapon weapon in World.Weapons)
                                    {
                                        if (item.TheItem.Name == weapon.Name)
                                        {
                                            Console.WriteLine($"You equip the {item.TheItem.Name}\n");
                                            Player.CurrentWeapon = weapon;
                                            break;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }

                            if (invChoice == "Exit"||invChoice=="exit")
                            {
                                break;
                            }
                        }
                        break;
                    case 3:
                    var hitChanceRand = rnd.Next(1, 6);
                    var damage = rnd.Next(Player.CurrentWeapon.MinimumDamage, Player.CurrentWeapon.MaximumDamage);
                    var monsterDamage = rnd.Next(1, Monster.MaximumDamage);
                    if (hitChanceRand / 2 > 0.5)
                    {
                        Console.WriteLine($"You hit the {monster.Name}!");

                        monster.CurrentHitPoints = monster.CurrentHitPoints - damage;

                        if (monster.CurrentHitPoints <= 0)
                        {
                            Console.WriteLine($"The {monster.Name} has: 0 Hp");
                            Console.WriteLine("You won!");
                            //Loot after fight
                            Console.WriteLine("You gained:\n");
                            foreach (var monst in World.Monsters)
                            {
                                if (monst.ID == monster.ID)
                                    foreach (var piece in monst.Loot.TheCountedItemList)
                                    {
                                        Console.WriteLine($"+{piece.TheItem.Name}");
                                        Player.Inventory.AddItem(piece.TheItem);
                                    }
                            }

                            Console.WriteLine($"+{monster.RewardExperience}Xp.\n" +
                                              $"+{monster.RewardGold} Gold.\n");
                            Player.XP += monster.RewardExperience;
                            Player.Gold += monster.RewardGold;
                            brave = false;
                            break;
                        }

                        Console.WriteLine($"The {monster.Name} has: {monster.CurrentHitPoints} Hp\n");
                    }
                    else
                    {
                        Console.WriteLine($"You missed the {monster.Name}!");
                        Console.WriteLine($"The {monster.Name} has: {monster.CurrentHitPoints} Hp\n");
                    }

                    Console.WriteLine($"The {monster.Name} hits you!");
                    Player.CurrentHP = Player.CurrentHP - monsterDamage;
                    if (Player.CurrentHP <= 0)
                    {
                        Console.WriteLine("You have: 0 Hp\n");
                        Console.WriteLine("You sadly passed away:(");
                        Console.WriteLine("Would you like to try again?");

                        Console.Write("Yes or no?: ");
                        bool retry = false;
                        while (!retry)
                        {
                            var Choice = Console.ReadLine();
                            if (Choice == "Yes" || Choice == "yes")
                            {
                                Program game = new Program();
                                Main(null);
                            }

                            if (Choice == "No" || Choice == "no")
                            {
                                Console.WriteLine("Bye!");
                                Environment.Exit(0);
                            }

                            Console.Write("The value must be Yes or No, try again: ");
                        }
                    }

                    Console.WriteLine($"You have: {Player.CurrentHP} Hp\n");
                    break;
                    case 2:
                        Console.WriteLine("You decide to quickly get out of here.");
                        brave = false;
                        break;
                    case 4:
                        Console.WriteLine(Player.CurrentLocation.Description);
                        break;
            }
            }
        }
}