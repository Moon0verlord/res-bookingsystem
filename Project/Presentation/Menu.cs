
using Newtonsoft.Json.Linq;

namespace Project.Presentation;


public static class Dishes
{
    
    static private MenuLogic _myMenu = new MenuLogic();
    public static void WelcomeMenu()
    {
        string[] options = { "vegetarian", "Fish", "Meat", "Vegan", "Back to main menu"};
        string prompt = "\nSelect a course to view the dishes:";
        int input = _myMenu.RunMenu(options, prompt);
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
                Console.WriteLine("Please enter a valid number");
                break;
        }
    }

    public static void JsonCursor(string choice)
    {
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Menu.json"));
        string json = File.ReadAllText(path);
        JObject dishes = JObject.Parse(json);
        Console.WriteLine("Gerechten:");
        Console.WriteLine("-------");

        Console.WriteLine("2 Courses:");
        foreach (var course in dishes[choice]["2_courses"])
        {
            string appetizer = (string)course["Voorgerecht"];
            string entree = (string)course["Maaltijd"];
            Console.WriteLine($"Appetizer: {appetizer}");
            Console.WriteLine($"Entree: {entree}");
            Console.WriteLine();
        }

        Console.WriteLine("3 Courses:");
        foreach (var course in dishes[choice]["3_courses"])
        {
            string appetizer = (string)course["Voorgerecht"];
            string entree = (string)course["Maaltijd"];
            string dessert = (string)course["Nagerecht"];
            Console.WriteLine($"Appetizer: {appetizer}");
            Console.WriteLine($"Entree: {entree}");
            Console.WriteLine($"Dessert: {dessert}");
            Console.WriteLine();
        }

        Console.WriteLine("4 Courses:");
        foreach (var course in dishes[choice]["4_courses"])
        {
            string appetizer = (string)course["Voorgerecht"];
            string soup = (string)course["Soep"];
            string entree = (string)course["Maaltijd"];
            string dessert = (string)course["Nagerecht"];
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
    
    // W.I.P - working skeleton for managing dishes in the menu
    public static void ManageMenu()
    {
       
        Console.WriteLine("Which type of dish would you like to add?");
        Console.WriteLine("1. Vegetarian");
        Console.WriteLine("2. Fish");
        Console.WriteLine("3. Meat");
        Console.WriteLine("4. Vegan");
        Console.WriteLine("5. Back to main menu");
        //console.readline - string choice1 = "Vegetarisch";
        Console.WriteLine("Which course would you like to add?");
        Console.WriteLine("1. 2 Courses");
        Console.WriteLine("2. 3 Courses");
        Console.WriteLine("3. 4 Courses");
        Console.WriteLine("4. Back to main menu");
        //console.readline - string choice2 = "4_Gangen";
        
        // TODO better naming for json variables
        //TODO Turn skeleton into a fully working method
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
        string json = File.ReadAllText("Dishes.json");
        JObject dishes = JObject.Parse(json);

        string menu = File.ReadAllText("Menu.json");
        JObject menuobj = JObject.Parse(menu);

        string choice1 = "Vegetarisch";
        string choice2 = "4_Gangen";

        JObject dish = (JObject)dishes[choice1];
        JArray vm = (JArray)dish[choice2];
        JObject itemA = (JObject)vm.ElementAt(0);

        JArray vm2 = (JArray)menuobj[choice1][choice2];
        vm2.Add(itemA);

        File.WriteAllText("Menu.json", menuobj.ToString());

        // move json back to DataSources
        // ask what type of dish
        // ask what course
        // add course to json
        
        //File.WriteAllText("Menu.json", vm[0].ToString());
        
    }

}