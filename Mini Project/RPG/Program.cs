using RPG;

public class Program
{
    static void Main(string[] args)
    {
        bool boolval = true;
        Console.WriteLine("The people in your town are being terrorized by giant spiders.\n" +
                          "You decide to do what you can to help.");
        Console.WriteLine("But first, who are you?");
        Console.Write("Enter your name: ");
        string name = Console.ReadLine()!;
        Player.CurrentWeapon = World.WeaponByID(World.WEAPON_ID_RUSTY_SWORD);
        Player.CurrentLocation = World.LocationByID(World.LOCATION_ID_HOME);
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
                        $"\nXp: {Player.XP}\nLevel: {Player.Level}.\nCurrent Weapon: {Player.CurrentWeapon.name}."+
                                          $"\nCurrent Location: {Player.CurrentLocation.Name}.");
                        break;
                    case 2:
                        Console.WriteLine(2);
                        break;
                    case 3:
                        Console.WriteLine(3);
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
}