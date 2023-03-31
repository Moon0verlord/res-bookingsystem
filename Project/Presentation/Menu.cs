
using Newtonsoft.Json.Linq;

namespace Project.Presentation;


public static class Dishes
{

    private static readonly MenuLogic MyMenu = new();
    public static void WelcomeMenu()
    {
        string[] options = { "Vegetarisch", "Vis", "Vlees", "Veganistisch", "Terug naar hoofdmenu" };
        string prompt = "\nKies een maaltijd voor de gerechten:";
        int input = MyMenu.RunMenu(options, prompt);
        switch (input)
        {
            case 0:
                JsonCursor("Vegetarisch");
                break;
            case 1:
                JsonCursor("Vis");
                break;
            case 2:
                JsonCursor("Vlees");
                break;
            case 3:
                JsonCursor("Veganistisch");
                break;
            case 4:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
    }

    public static void JsonCursor(string choice)
    {
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Menu.json"));
        string json = File.ReadAllText(path);
        JObject menu = JObject.Parse(json);
        Console.Clear();
        Console.WriteLine("gerechten:");
        Console.WriteLine("-------");

        Console.WriteLine("2 Gangen:");
        foreach (var course in menu[choice]["2_Gangen"])
        {
            string appetizer = (string)course["Voorgerecht"]!;
            string entree = (string)course["Maaltijd"]!;
            Console.WriteLine($"Voorgerecht: {appetizer}");
            Console.WriteLine($"Maaltijd: {entree}");
            Console.WriteLine();
        }

        Console.WriteLine("3 Gangen:");
        foreach (var course in menu[choice]["3_Gangen"])
        {
            string appetizer = (string)course["Voorgerecht"]!;
            string entree = (string)course["Maaltijd"]!;
            string dessert = (string)course["Nagerecht"]!;
            Console.WriteLine($"Voorgerecht: {appetizer}");
            Console.WriteLine($"Maaltijd: {entree}");
            Console.WriteLine($"Nagerecht: {dessert}");
            Console.WriteLine();
        }

        Console.WriteLine("4 Gangen:");
        foreach (var course in menu[choice]["4_Gangen"])
        {
            string appetizer = (string)course["Voorgerecht"]!;
            string soup = (string)course["Soep"]!;
            string entree = (string)course["Maaltijd"]!;
            string dessert = (string)course["Nagerecht"]!;
            Console.WriteLine($"Voorgerecht: {appetizer}");
            Console.WriteLine($"Soep: {soup}");
            Console.WriteLine($"Maaltijd: {entree}");
            Console.WriteLine($"Nagerecht: {dessert}");
            Console.WriteLine();
            Console.WriteLine("Druk op een knop om verder te gaan");
            Console.ReadKey();
            MainMenu.Start();
        }
    }

}