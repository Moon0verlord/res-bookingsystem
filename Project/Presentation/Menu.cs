
using Newtonsoft.Json.Linq;

namespace Project.Presentation;


public static class Dishes
{
    
    private static readonly MenuLogic MyMenu = new();
    public static void WelcomeMenu()
    {
        string[] options = { "vegetarian", "Fish", "Meat", "Vegan", "Back to main menu"};
        string prompt = "\nSelect a course to view the dishes:";
        int input = MyMenu.RunMenu(options, prompt);
        switch (input)
        {
            case 0:
                JsonCursor("vegetarian");
                break;
            case 1:
                JsonCursor("fish");
                break;
            case 2:
                JsonCursor("meat");
                break;
            case 3:
                JsonCursor("vegan");
                break;
            case 4:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Please enter a valid number");
                break;
        }
    }

    private static void JsonCursor(string choice)
    {
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Menu.json"));
        string json = File.ReadAllText(path);
        JObject menu = JObject.Parse(json);
        Console.WriteLine("Dishes:");
        Console.WriteLine("-------");

        Console.WriteLine("2 Courses:");
        foreach (var course in menu[choice]["2_courses"])
        {
            string appetizer = (string)course["appetizer"]!;
            string entree = (string)course["entree"]!;
            Console.WriteLine($"Appetizer: {appetizer}");
            Console.WriteLine($"Entree: {entree}");
            Console.WriteLine();
        }

        Console.WriteLine("3 Courses:");
        foreach (var course in menu[choice]["3_courses"])
        {
            string appetizer = (string)course["appetizer"]!;
            string entree = (string)course["entree"]!;
            string dessert = (string)course["dessert"]!;
            Console.WriteLine($"Appetizer: {appetizer}");
            Console.WriteLine($"Entree: {entree}");
            Console.WriteLine($"Dessert: {dessert}");
            Console.WriteLine();
        }

        Console.WriteLine("4 Courses:");
        foreach (var course in menu[choice]["4_courses"])
        {
            string appetizer = (string)course["appetizer"]!;
            string soup = (string)course["soup"]!;
            string entree = (string)course["entree"]!;
            string dessert = (string)course["dessert"]!;
            Console.WriteLine($"Appetizer: {appetizer}");
            Console.WriteLine($"Soup: {soup}");
            Console.WriteLine($"Entree: {entree}");
            Console.WriteLine($"Dessert: {dessert}");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            MainMenu.Start();
        }
    }
    
}