
using Newtonsoft.Json.Linq;

namespace Project.Presentation;


public static class Dishes
{
    
    // TODO add Add Dish method
    static private MenuLogic _myMenu = new MenuLogic();
    public static void WelcomeMenu()
    {
        string[] options = { "vegetarian", "Fish", "Meat", "Vegan", "Back to main menu"};
        string prompt = "\nSelect a course to view the dishes:";
        int input = _myMenu.RunMenu(options, prompt);
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

    public static void JsonCursor(string choice)
    {
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, "Dishes.json"));
        string json = File.ReadAllText(path);
        JObject menu = JObject.Parse(json);
        Console.WriteLine("Dishes:");
        Console.WriteLine("-------");

        Console.WriteLine("2 Courses:");
        foreach (var course in menu[choice]["2_courses"])
        {
            string appetizer = (string)course["appetizer"];
            string entree = (string)course["entree"];
            Console.WriteLine($"Appetizer: {appetizer}");
            Console.WriteLine($"Entree: {entree}");
            Console.WriteLine();
        }

        Console.WriteLine("3 Courses:");
        foreach (var course in menu[choice]["3_courses"])
        {
            string appetizer = (string)course["appetizer"];
            string entree = (string)course["entree"];
            string dessert = (string)course["dessert"];
            Console.WriteLine($"Appetizer: {appetizer}");
            Console.WriteLine($"Entree: {entree}");
            Console.WriteLine($"Dessert: {dessert}");
            Console.WriteLine();
        }

        Console.WriteLine("4 Courses:");
        foreach (var course in menu[choice]["4_courses"])
        {
            string appetizer = (string)course["appetizer"];
            string soup = (string)course["soup"];
            string entree = (string)course["entree"];
            string dessert = (string)course["dessert"];
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

    public static void AddDish()
    {
        //string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
        string json = File.ReadAllText("Dishes.json");
        JObject menu = JObject.Parse(json);
        Console.WriteLine("Which type of dish would you like to add?");
        JObject Vegetarian = (JObject)menu["vegetarian"];
        JArray vm = (JArray)Vegetarian["2_courses"];
        vm.Add(new JObject(new JProperty("appetizer", "test"), new JProperty("entree", "test")));
        Console.WriteLine(menu.ToString());
        // move json back to DataSources
        // ask what type of dish
        // ask what course
        // add course to json
        File.WriteAllText("Dishes.json", menu.ToString());
        
    }
}