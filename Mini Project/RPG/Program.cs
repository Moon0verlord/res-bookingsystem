namespace RPG;

public class Program
{
    static void Main(string[] args)
    {
        bool boolval = true;
        Console.WriteLine("The people in your town are being terrorized by giant spiders.\n" +
                          "You decide to do what you can to help.");
        while (boolval)
        {
            Console.WriteLine("What would you like to do (Enter a number?).");
            Console.WriteLine("1: See game stats\n2: Move\n3: Fight\n4: Quit\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine(1);
                    Console.WriteLine($"\nName: {Player.Name}" +
                                      $"\nCurrent HP:{Player.CurrentHP}" +
                                      $"\nMax HP: {Player.MaxHP}" +
                                      $"\nCurrent Weapon: {Player.CurrentWeapon}" +
                                      $"\nCurrent Location: {Player.CurrentLocation}");
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
                    Console.WriteLine("Unkown");
                    break;
            }
        }
    }
}