
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
        string type = "";
        string course = "";
        
        string[] options = { "vegetarian", "Fish", "Meat", "Vegan", "Back to main menu"};
        string prompt = "\nWhich type of dish would you like to add?:";
        int input = _myMenu.RunMenu(options, prompt);
        switch (input)
        {
            case 0:
                type = "Vegetarisch";
                break;
            case 1:
                type = "Vis";
                break;
            case 2:
                type = "Vlees";
                break;
            case 3:
                type = "Veganistisch";
                break;
            case 4:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Please enter a valid number");
                break;
        }
        
        string[] options2 = { "2 Gangen", "3 Gangen", "4 Gangen", "Back to main menu"};
        string prompt2 = "\nWhich course would you like to add?:";
        int input2 = _myMenu.RunMenu(options2, prompt2);
        switch (input)
        {
            case 0:
                course = "2_Gangen";
                break;
            case 1:
                course = "3_Gangen";
                break;
            case 2:
                course = "4_Gangen";
                break;
            case 3:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Please enter a valid number");
                break;
        }
        
        // grabs dishes from json
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
        string json = File.ReadAllText("Dishes.json");
        JObject dishes = JObject.Parse(json);
        JObject dish = (JObject)dishes[type];
        JArray DishArray = (JArray)dish[course];
        // grabs menu from json
        string menu = File.ReadAllText("Menu.json");
        JObject MenuOBJ = JObject.Parse(menu);
        // adds dish to menu
        JArray menuCourse = (JArray)MenuOBJ[type][course];
        JObject dishtoadd = (JObject)DishArray.ElementAt(0);
        menuCourse[0] = dishtoadd;
        File.WriteAllText("Menu.json", MenuOBJ.ToString());
        Console.WriteLine("Dish added to menu");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        MainMenu.Start();
        // Reminder move json back to DataSources folder
        // TODO: Make a function That displays dishes using the menu framework
    }

}