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
        Player.CurrentLocation = World.LocationByID(World.LOCATION_ID_SPIDER_FIELD);
        Player player = new Player(name,10,10,10,
        0,1, Player.CurrentWeapon,Player.CurrentLocation);
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
                        Console.WriteLine($"Name: {name}.\nMax hp: {Player.MaxHP}.\n" +
                                          $"Current hp: {Player.CurrentHP}.\nGold: {Player.Gold}."+
                        $"\nXp: {Player.XP}\nLevel: {Player.Level}.\nCurrent Weapon: {Player.CurrentWeapon.Name}."+
                                          $"\nCurrent Location: {Player.CurrentLocation.Description}.");
                        break;
                    case 2:
                        Console.WriteLine("Where would you like to go?");
                        Console.WriteLine($"You are at: {Player.CurrentLocation.Name}.\n{Player.CurrentLocation.Description}." +
                                          $"\nFrom here you can go to:");
                        Console.WriteLine(Player.CurrentLocation.Compass());
                        Console.WriteLine(Player.CurrentLocation.Map());
                        break;
                    case 3:
                        if (Player.CurrentLocation.MonsterLivingHere != null)
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
                        Console.WriteLine("Unknown");
                        break;
                }
            }
            catch (System.FormatException e)
            {
                Console.WriteLine("Invalid input. Please enter a valid option.\n");
            }
            
        }
    }

    public static void fight()
    {
        Random rnd = new Random();
        var hitChanceRand = rnd.Next(1, 6);
        var monster = Player.CurrentLocation.MonsterLivingHere;
        Console.WriteLine($"You have: {Player.CurrentHP} Hp");
        Console.WriteLine($"The {monster.Name} has: {monster.CurrentHitPoints} Hp\n");
        while (Player.CurrentHP > 0)
        {
            var damage = rnd.Next(Player.CurrentWeapon.MinimumDamage, Player.CurrentWeapon.MaximumDamage);
            var monsterDamage = rnd.Next(1, Monster.MaximumDamage);
            if (hitChanceRand / 2 >= 1)
            {
                Console.WriteLine($"You hit the {monster.Name}!");
                
                monster.CurrentHitPoints = monster.CurrentHitPoints- damage;

                if (monster.CurrentHitPoints <= 0)
                {
                    Console.WriteLine("You won");
                    //add item to inventory//
                    break;
                }
                Console.WriteLine($"The {monster.Name} has: 0 Hp");
            }
            else
            {
                Console.WriteLine("You missed haha");
                Console.WriteLine($"The {monster.Name} has: {monster.CurrentHitPoints} Hp\n");
            }
            Console.WriteLine($"The {monster.Name} hits you!");
            Player.CurrentHP = Player.CurrentHP - monsterDamage;
            Console.WriteLine($"You have: {Player.CurrentHP} Hp\n");
        }
        if (Player.CurrentHP <= 0)
        {
            Console.WriteLine("You sadly passed away:(");
        }
    }
}