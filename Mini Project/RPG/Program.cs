using System;
using System.Net.Mime;
namespace RPG;

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
        Player.CurrentLocation = World.LocationByID(World.LOCATION_ID_HOME);
        Player player = new Player(name, 15, 15, 10, 0, 1, Player.CurrentWeapon, Player.CurrentLocation);
        while (boolval)
        {
            try
            {
                Console.WriteLine($"\nYou are at: {Player.CurrentLocation.Name}");
                Console.WriteLine("What would you like to do (Enter a number?).");
                Console.WriteLine("1: See game stats\n2: Move\n3: Fight\n4: Quit");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine($"\nName: {name}\nMax HP: {player.MaxHP}\n" +
                                          $"Current HP: {Player.CurrentHP}\nGold: {Player.Gold}" +
                                          $"\nXP: {Player.XP}\nLevel: {Player.Level}\nCurrent Weapon: {Player.CurrentWeapon.Name}" +
                                          $"\nCurrent Location: {Player.CurrentLocation.Name}\n");
                        Player.ViewInventory();
                        Player.ViewQuestLog();
                        break;
                    case 2:
                        Move();
                        switch (Player.CurrentLocation.ID)
                        {
                            case 3:
                                {
                                    Bridge();
                                    break;
                                }
                            case 4:
                                {
                                    Alchemist();
                                    break;
                                }
                            case 6:
                            case 7:
                                {
                                    Farmer();
                                    break;
                                }
                            case 9:
                                {
                                    Spider();
                                    break;
                                }
                        }
                        break;
                    case 3:
                        if (Player.CurrentLocation.MonsterLivingHere != null
                            && Player.CurrentLocation.MonsterLivingHere.CurrentHitPoints > 0)
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
                        Console.WriteLine("Thanks for playing!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Unknown input");
                        break;
                }
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid option.\n");
            }
        }
    }

    public static void Move()
    {
        bool loop = true;
        while (loop)
        {
            Console.WriteLine("\nWhere would you like to go?");
            Console.WriteLine($"You are at: {Player.CurrentLocation.Name}.\n{Player.CurrentLocation.Description}." +
                              $"\nFrom here you can go to:");
            Console.WriteLine(Player.CurrentLocation.Compass());
            Console.WriteLine(Player.CurrentLocation.Map() +
                              "\nEnter a compass direction or type 'L' to stay where you are: ");
            string direction = Console.ReadLine()!.ToUpper();
            switch (direction)
            {
                case "N":
                case "NORTH":
                    if (Player.CurrentLocation.LocationToNorth != null)
                    {
                        Player.CurrentLocation = Player.CurrentLocation.LocationToNorth;
                        loop = false;
                    }
                    else Console.WriteLine("You can't go north.");
                    break;

                case "S":
                case "SOUTH":
                    if (Player.CurrentLocation.LocationToSouth != null)
                    {
                        Player.CurrentLocation = Player.CurrentLocation.LocationToSouth;
                        loop = false;
                    }
                    else Console.WriteLine("You can't go south.");
                    break;

                case "E":
                case "EAST":
                    if (Player.CurrentLocation.LocationToEast != null)
                    {
                        Player.CurrentLocation = Player.CurrentLocation.LocationToEast;
                        loop = false;
                    }
                    else Console.WriteLine("You can't go east.");
                    break;

                case "W":
                case "WEST":
                    if (Player.CurrentLocation.LocationToWest != null)
                    {
                        Player.CurrentLocation = Player.CurrentLocation.LocationToWest;
                        loop = false;
                    }
                    else Console.WriteLine("You can't go west.");
                    break;

                case "L":
                case "LEAVE":
                    loop = false;
                    break;

                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }

    public static void Farmer()
    {
        if (Quest.FARMER_COMPLETION_FLAG == 0)
        {
            // Placeholder to test questing
            Console.WriteLine("You arrive at a worn-down farmhouse. An old farmer approaches you:\n" +
                "Farmer: 'I can't w'rk mine own landeth with those pesky snakes slith'ring 'round! Shall thee help me?'");
            Console.WriteLine("Do you accept his quest? y/n");
            string answer = Console.ReadLine()!.ToLower();
            if (answer == "y")
            {
                Player.QuestLog.AddQuest(new PlayerQuest(Player.CurrentLocation.QuestAvailableHere, false));
                Quest.FARMER_COMPLETION_FLAG = 1;
            }
            else Console.WriteLine("Maybe some other time then.");
        }
        else if (Player.IsInInventory(World.ITEM_ID_RAT_TAIL))
        {
            foreach (CountedItem InvItem in Player.Inventory.TheCountedItemList)
            {
                if (InvItem.TheItem.ID == World.ITEM_ID_RAT_TAIL && InvItem.Quantity == 3)
                {
                    // quest end here
                }
            }
            Console.WriteLine("You've killed some rats, but not enough.");
        }
        else if (Quest.FARMER_COMPLETION_FLAG == 1 && Player.CurrentLocation.ID == 7)
        {
            Console.WriteLine("There's alot of snakes roaming around this field..");
        }
        else if (Quest.FARMER_COMPLETION_FLAG == 1)
        {
            Console.WriteLine("\nFarmer: 'Please get rid of these snakes already!'");
        }
    }

    public static void Alchemist()
    {
        Console.WriteLine();
    }

    public static void Spider()
    {
        if (Quest.SPIDER_COMPLETION_FLAG == 0)
        {
            // Placeholder to test questing
            Console.WriteLine("You arrive at the bridge. There's a guard trying to calm down the people from the village." +
            "When they went to the forest they found a spider nest. The guard ask you if you can kill and collect the silk to protect the village");
            Console.WriteLine("Do you accept his quest? y/n");
            string answer = Console.ReadLine()!.ToLower();
            if (answer == "y")
            {
                Player.QuestLog.AddQuest(new PlayerQuest(Player.CurrentLocation.QuestAvailableHere, false));
                Quest.SPIDER_COMPLETION_FLAG = 1;
            }
            else Console.WriteLine("Maybe if you want to save the village some time.");
        }
        else if (Player.IsInInventory(World.ITEM_ID_SPIDER_SILK))
        {
            foreach (CountedItem InvItem in Player.Inventory.TheCountedItemList)
            {
                if (InvItem.TheItem.ID == World.ITEM_ID_SPIDER_SILK && InvItem.Quantity == 3)
                {
                    // quest end here
                }
            }
            Console.WriteLine("Most of the rats are dead now kill the rest.");
        }
        else if (Quest.SPIDER_COMPLETION_FLAG == 1 && Player.CurrentLocation.ID == 9)
        {
            Console.WriteLine("Wow there are a lot of spiders here LETS get to work..");
        }
        else if (Quest.SPIDER_COMPLETION_FLAG == 1)
        {
            Console.WriteLine("\nGuard: 'go kills those damn spiders!'");
        }
    }

    public static void Bridge()
    {
        Console.WriteLine("Guard: 'Turn back at once, peasant! Unless thee hast proof of thy grit.'");
        if (!Player.IsInInventory(World.ITEM_ID_ADVENTURER_PASS))
        {
            Console.WriteLine("You have no proof right now. The guard sends you back to the Town Square.");
            Player.CurrentLocation = World.LocationByID(World.LOCATION_ID_TOWN_SQUARE);
        }
        else
        {
            // guard here
            Console.WriteLine("You have proof");
        }
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
                              "\nOpen Inventory (type inventory)." +
                              "\nRun." +
                              "\nFight." +
                              "\nObserve.");
            var fightdo = Console.ReadLine()!.ToLower();
            //open inventory-ToDO
            //run???-X
            //fight-X
            //observe-ToDo
            switch (fightdo)
            {
                case "inventory":
                    Console.WriteLine();
                    foreach (var item in Player.Inventory.TheCountedItemList)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine();
                    break;
                case "fight":
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
                case "run":
                    Console.WriteLine("You decide to quickly get out of here.");
                    brave = false;
                    break;
                case "observe":
                    Console.WriteLine(Player.CurrentLocation.Description);
                    break;
            }
        }
    }
}