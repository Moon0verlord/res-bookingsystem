using System;
using System.Net.Mime;
namespace RPG;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("██████╗ ██████╗  ██████╗      ██████╗  █████╗ ███╗   ███╗███████╗" + "\n" +
                          "██╔══██╗██╔══██╗██╔════╝     ██╔════╝ ██╔══██╗████╗ ████║██╔════╝" + "\n" +
                          "██████╔╝██████╔╝██║  ███╗    ██║  ███╗███████║██╔████╔██║█████╗" + "\n" +
                          "██╔══██╗██╔═══╝ ██║   ██║    ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝" + "\n" +
                          "██║  ██║██║     ╚██████╔╝    ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗" + "\n" +
                          "╚═╝  ╚═╝╚═╝      ╚═════╝      ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝");
        LoadingScreen();
        var boolval = true;
        Console.WriteLine("\nThe people in your town are being terrorized by giant spiders.\n" +
                          "You decide to do what you can to help.");
        Console.WriteLine("But first, who are you?");
        Console.Write("Enter your name: ");
        string name = Console.ReadLine()!;
        Player.CurrentWeapon = World.WeaponByID(World.WEAPON_ID_RUSTY_SWORD);
        Player.CurrentLocation = World.LocationByID(World.LOCATION_ID_HOME);
        Player player = new Player(name, 25, 25, 10, 0, 1, Player.CurrentWeapon, Player.CurrentLocation);
        while (boolval)
        {
            try
            {
                Console.WriteLine($"\nYou are at: {Player.CurrentLocation.Name}");
                Console.WriteLine("What would you like to do (Enter a number?).");
                Console.WriteLine("1: See game stats\n2: Move\n3: Fight\n4: Quit" + (Quest.GameDone() ? " and end the game." : null));
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine($"\nName: {name}\nHP: {Player.CurrentHP}/{player.MaxHP}\nGold: {Player.Gold}" +
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
                                    guardPost();
                                    break;
                                }
                            case 4:
                            case 5:
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
                            case 8:
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
                            Console.WriteLine($"The {Player.CurrentLocation.MonsterLivingHere.NamePlural} attack!");
                            fight();
                        }
                        else
                        {
                            Console.WriteLine("There seems to be nothing here to fight..");
                        }

                        break;
                    case 4:
                        if (Quest.GameDone())
                        {
                            Console.WriteLine("After defeating all the monsters in town, you're hailed as a hero!\n" +
                                              "Congratulations, you win!\n" + "Thanks for playing!");
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("Thanks for playing!");
                            Environment.Exit(0);
                        }
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

    public static void LoadingScreen()
    {
        string loading = "Loading...";
        for (int i = 0; i < 6; i++)
        {
            loading = (loading == "Loading...") ? "Loading" : (loading + ".");
            Console.Write("                       " + loading + "\r");
            Thread.Sleep(500);
        }

        Console.Clear();
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
        else if (Player.IsInInventory(World.ITEM_ID_SNAKE_FANG) && Quest.FARMER_COMPLETION_FLAG == 1)
        {
            foreach (CountedItem InvItem in Player.Inventory.TheCountedItemList.ToList())
            {
                if (InvItem.TheItem.ID == World.ITEM_ID_SNAKE_FANG && InvItem.Quantity == 3)
                {
                    Console.WriteLine("Thank ye for gettin' rid of those darned snakes!\n" +
                                      "For yer effort, I shall grant ye this Adventurer's pass to get across the bridge!");
                    Player.Inventory.AddCountedItem(new CountedItem(World.ItemByID(World.ITEM_ID_ADVENTURER_PASS), 1));
                    Player.Inventory.RemoveItem(new CountedItem(World.ItemByID(World.ITEM_ID_SNAKE_FANG), 3));
                    Player.QuestLog.QuestComplete(World.QUEST_ID_CLEAR_FARMERS_FIELD);
                    Quest.FARMER_COMPLETION_FLAG = 2;
                }
                else if (InvItem.TheItem.ID == World.ITEM_ID_SNAKE_FANG && InvItem.Quantity != 3)
                {
                    Console.WriteLine("You've killed some snakes, but not enough.");
                }
            }
        }
        else if (Quest.FARMER_COMPLETION_FLAG == 1 && Player.CurrentLocation.ID == 7)
        {
            Console.WriteLine("\nThere's alot of snakes roaming around this field..");
        }
        else if (Quest.FARMER_COMPLETION_FLAG == 1)
        {
            Console.WriteLine("\nFarmer: 'Please get rid of these snakes already!'");
        }
    }

    public static void Alchemist()
    {
        if (Quest.ALCHEMIST_COMPLETION_FLAG == 0)
        {
            Console.WriteLine(
                "Alchemist: 'Those rats art nibbling on mine own h'rbs! I couldst very much useth an adventur'r to taketh careth of those folk'");
            Console.WriteLine("Do you accept his quest? y/n");
            string answer = Console.ReadLine()!.ToLower();
            if (answer == "y")
            {
                Player.QuestLog.AddQuest(new PlayerQuest(Player.CurrentLocation.QuestAvailableHere, false));
                Quest.ALCHEMIST_COMPLETION_FLAG = 1;
            }
            else Console.WriteLine("Maybe another time.");
        }

        else if (Player.IsInInventory(World.ITEM_ID_RAT_TAIL) && Quest.ALCHEMIST_COMPLETION_FLAG == 1)
        {
            foreach (CountedItem InvItem in Player.Inventory.TheCountedItemList.ToList())
            {
                if (InvItem.TheItem.ID == World.ITEM_ID_RAT_TAIL && InvItem.Quantity == 3)
                {
                    Console.WriteLine("Alchemist: 'Thank you for killing those damned rats!'\n" +
                                      "The alchemist gives you a club as reward.");
                    Player.Inventory.AddCountedItem(new CountedItem(World.ItemByID(World.WEAPON_ID_CLUB), 1));
                    Player.Inventory.RemoveItem(new CountedItem(World.ItemByID(World.ITEM_ID_RAT_TAIL), 3));
                    Player.QuestLog.QuestComplete(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN);
                    Quest.ALCHEMIST_COMPLETION_FLAG = 2;
                }
                else if (InvItem.TheItem.ID == World.ITEM_ID_RAT_TAIL && InvItem.Quantity != 3)
                {
                    Console.WriteLine("Alchemist: 'You've killed some rats, but I can still see some around.'");
                }
            }
        }

        else if (Quest.ALCHEMIST_COMPLETION_FLAG == 1 && Player.CurrentLocation.ID == 5)
        {
            Console.WriteLine("\nThere are alot of rats scurrying around in this garden..");
        }

        else if (Quest.ALCHEMIST_COMPLETION_FLAG == 1)
        {
            Console.WriteLine("\nAlchemist: 'Why hast thou not killed those rats yet?'");
        }
    }

    public static void Spider()
    {
        if (Quest.SPIDER_COMPLETION_FLAG == 0)
        {
            // Placeholder to test questing
            Console.WriteLine("You arrive at the bridge. There's a guard trying to calm down the people from the village." +
            "\nWhen they went to the forest they found a spider nest. The guard ask you if you can kill and collect the silk to protect the village");
            Console.WriteLine("Do you accept his quest? y/n");
            string answer = Console.ReadLine()!.ToLower();
            if (answer == "y")
            {
                Player.QuestLog.AddQuest(new PlayerQuest(Player.CurrentLocation.QuestAvailableHere, false));
                Quest.SPIDER_COMPLETION_FLAG = 1;
            }
            else Console.WriteLine("Guard: 'Maybe if you want to save the village some other time.'");
        }
        else if (Player.IsInInventory(World.ITEM_ID_SPIDER_SILK) && Quest.SPIDER_COMPLETION_FLAG == 1)
        {
            foreach (CountedItem InvItem in Player.Inventory.TheCountedItemList.ToList())
            {
                if (InvItem.TheItem.ID == World.ITEM_ID_SPIDER_SILK && InvItem.Quantity == 3)
                {
                    Console.WriteLine("Guard: 'Thank you for killing those damned spiders!'\n" +
                                        "The whole village gives you the Winner's Medal for your accomplishment.");
                    Player.Inventory.AddCountedItem(new CountedItem(World.ItemByID(World.ITEM_ID_WINNERS_MEDAL), 1));
                    Player.Inventory.RemoveItem(new CountedItem(World.ItemByID(World.ITEM_ID_SPIDER_SILK), 3));
                    Player.QuestLog.QuestComplete(World.QUEST_ID_COLLECT_SPIDER_SILK);
                    Quest.SPIDER_COMPLETION_FLAG = 2;
                }
                else if (InvItem.TheItem.ID == World.ITEM_ID_SPIDER_SILK && InvItem.Quantity != 3)
                {
                    Console.WriteLine("\nGuard: 'There are still spiders roaming around!'");
                }
            }
        }
        else if (Quest.SPIDER_COMPLETION_FLAG == 1 && Player.CurrentLocation.ID == 9)
        {
            Console.WriteLine("\nYou see alot of large spiders crawling around..");
        }
        else if (Quest.SPIDER_COMPLETION_FLAG == 1)
        {
            Console.WriteLine("\nGuard: 'Someone go kill those damn spiders!'");
        }
    }

    public static void guardPost()
    {
        if (!Player.PassedBridge)
        {
            Console.WriteLine("Guard: 'Turn back at once, peasant! Unless thee hast proof of thy grit.'");
            if (!Player.IsInInventory(World.ITEM_ID_ADVENTURER_PASS))
            {
                Console.WriteLine("You have no proof right now. The guard sends you back to the Town Square.");
                Player.CurrentLocation = World.LocationByID(World.LOCATION_ID_TOWN_SQUARE);
            }
            else
            {
                Console.WriteLine("Guard: 'Hmm... Your pass checks out, you may cross the bridge.'");
                Player.PassedBridge = true;
            }
        }
    }

    public static void fight()
    {
        Random rnd = new Random();
        var monster = Player.CurrentLocation.MonsterLivingHere;
        Console.WriteLine($"You have: {Player.CurrentHP} Hp");
        Console.WriteLine($"The {monster.NamePlural} have: {monster.CurrentHitPoints} Hp\n");
        bool brave = true;
        while (brave && Player.CurrentHP > 0)
        {
            Console.WriteLine("What would you like to do?:" +
                              "\n1: Open Inventory." +
                              "\n2: Run." +
                              "\n3: Fight." +
                              "\n4: Observe.");
            var fightdo = Convert.ToInt32(Console.ReadLine());
            //open inventory-ToDO
            //run???-X
            //fight-X
            //observe-ToDo
            switch (fightdo)
            {
                case 1:
                    Player.ViewInventory();
                    Console.WriteLine(
                        "Type the name of an item you'd like to use. Or type 'exit' to leave the inventory.");
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
                                item.UseQuantity();
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

                        if (invChoice == "Exit" || invChoice == "exit")
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
                        Console.WriteLine($"You hit the {monster.NamePlural}!");

                        monster.CurrentHitPoints = monster.CurrentHitPoints - damage;

                        if (monster.CurrentHitPoints <= 0)
                        {
                            Console.WriteLine($"The {monster.Name} have: 0 Hp");
                            Console.WriteLine("You won!");
                            //Loot after fight
                            Console.WriteLine("You gained:\n");
                            foreach (var monst in World.Monsters)
                            {
                                if (monst.ID == monster.ID)
                                    foreach (var piece in monst.Loot.TheCountedItemList)
                                    {
                                        Console.WriteLine($"+3 {piece.TheItem.Name}");
                                        Player.Inventory.TheCountedItemList.Add(
                                            new CountedItem(new Item(piece.TheItem.ID,
                                                (piece.TheItem.Name),
                                                piece.TheItem.NamePlural), 3));
                                    }
                            }

                            Console.WriteLine($"+{monster.RewardExperience}Xp.\n" +
                                              $"+{monster.RewardGold} Gold.\n");
                            Player.XP += monster.RewardExperience;
                            Player.Gold += monster.RewardGold;
                            brave = false;
                            break;
                        }

                        Console.WriteLine($"The {monster.NamePlural} have: {monster.CurrentHitPoints} Hp\n");
                    }
                    else
                    {
                        Console.WriteLine($"You missed the {monster.NamePlural}!");
                        Console.WriteLine($"The {monster.NamePlural} has: {monster.CurrentHitPoints} Hp\n");
                    }

                    Console.WriteLine($"The {monster.NamePlural} hit you!");
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
