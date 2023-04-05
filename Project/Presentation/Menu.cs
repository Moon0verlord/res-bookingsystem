
using Newtonsoft.Json.Linq;

namespace Project.Presentation;


public static class Dishes
{
    
    static private MenuLogic _myMenu = new MenuLogic();
    public static void WelcomeMenu()
    {
        string[] options = { "Vegetarisch", "Vis", "Vlees", "Veganistisch", "Terug naar hoofdmenu" };
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
    
    public static void ManageMenu()
    {
        string type = "";
        string course = "";
        
        string[] options = { "vegetarian", "Fish", "Meat", "Vegan", "Terug naar hoofdmenu"};
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
        
        string[] options2 = { "2 Gangen", "3 Gangen", "4 Gangen", "Terug naar hoofdmenu"};
        string prompt2 = "\nWhich course would you like to add?:";
        int input2 = _myMenu.RunMenu(options2, prompt2);
        switch (input2)
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
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Dishes.json"));
        string json = File.ReadAllText(path);
        JObject dishes = JObject.Parse(json);
        JObject dish = (JObject)dishes[type];
        JArray DishArray = (JArray)dish[course];
        // grabs menu from json
        string path2 = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Menu.json"));
        string menu = File.ReadAllText(path2);
        JObject MenuOBJ = JObject.Parse(menu);
        // adds dish to menu
        JArray menuCourse = (JArray)MenuOBJ[type][course];
        JObject dishtoadd = DisplayOptions(type, course);
        menuCourse[0] = dishtoadd;
        File.WriteAllText(path2, MenuOBJ.ToString());
        // displays added dish
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{dishtoadd} has been added to the menu\n");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue.....");
        Console.ReadKey();
        MainMenu.Start();
    }

    // displays and returns selection to add to menu
    public static JObject DisplayOptions(string type, string course)
    {
        string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Dishes.json"));
        string json = File.ReadAllText(path);
        JObject dishes = JObject.Parse(json);
        JObject dish = (JObject)dishes[type];
        JArray DishArray = (JArray)dish[course];
        // displays dishes based on course
        string dish1 = "";
        string dish2 = "";
        string dish3 = "";
        string dish4 = "";
        switch (course)
        {
            case "2_Gangen":
                 dish1 = $"Voorgerecht: {DishArray[0]["Voorgerecht"]} \nMaaltijd: {DishArray[0]["Maaltijd"]}\n";
                 dish2 = $"Voorgerecht: {DishArray[1]["Voorgerecht"]} \nMaaltijd: {DishArray[1]["Maaltijd"]}\n";
                 dish3 = $"Voorgerecht: {DishArray[2]["Voorgerecht"]} \nMaaltijd: {DishArray[2]["Maaltijd"]}\n";
                 dish4 = $"Voorgerecht: {DishArray[3]["Voorgerecht"]} \nMaaltijd: {DishArray[3]["Maaltijd"]}\n";
                break;
            case "3_Gangen":
                dish1 = $"Voorgerecht: {DishArray[0]["Voorgerecht"]} \nMaaltijd: {DishArray[0]["Maaltijd"]} \nNagerecht: {DishArray[0]["Nagerecht"]}\n";
                dish2 = $"Voorgerecht: {DishArray[1]["Voorgerecht"]} \nMaaltijd: {DishArray[1]["Maaltijd"]} \nNagerecht: {DishArray[1]["Nagerecht"]}\n";
                dish3 = $"Voorgerecht: {DishArray[2]["Voorgerecht"]} \nMaaltijd: {DishArray[2]["Maaltijd"]} \nNagerecht: {DishArray[2]["Nagerecht"]}\n";
                dish4 = $"Voorgerecht: {DishArray[3]["Voorgerecht"]} \nMaaltijd: {DishArray[3]["Maaltijd"]} \nNagerecht: {DishArray[3]["Nagerecht"]}\n";
                break;
            case "4_Gangen":
                dish1 = $"Voorgerecht: {DishArray[0]["Voorgerecht"]} \nSoep: {DishArray[0]["Soep"]} \nMaaltijd: {DishArray[0]["Maaltijd"]} \nNagerecht: {DishArray[0]["Nagerecht"]}\n";
                dish2 = $"Voorgerecht: {DishArray[1]["Voorgerecht"]} \nSoep: {DishArray[1]["Soep"]} \nMaaltijd: {DishArray[1]["Maaltijd"]} \nNagerecht: {DishArray[1]["Nagerecht"]}\n";
                dish3 = $"Voorgerecht: {DishArray[2]["Voorgerecht"]} \nSoep: {DishArray[2]["Soep"]} \nMaaltijd: {DishArray[2]["Maaltijd"]} \nNagerecht: {DishArray[2]["Nagerecht"]}\n";
                dish4 = $"Voorgerecht: {DishArray[3]["Voorgerecht"]} \nSoep: {DishArray[3]["Soep"]} \nMaaltijd: {DishArray[3]["Maaltijd"]} \nNagerecht: {DishArray[3]["Nagerecht"]}\n";
                break;
        }
        // returns selection
        string[] options = { dish1, dish2, dish3, dish4, "Terug naar hoofdmenu"};
        string prompt2 = "\nWhich course would you like to add?:";
        int input2 = _myMenu.RunMenu(options, prompt2);
        switch (input2)
        {
            case 0:
                return (JObject)DishArray.ElementAt(0);
            case 1:
                return (JObject)DishArray.ElementAt(1);
            case 2:
                return (JObject)DishArray.ElementAt(2);
            case 3:
                return (JObject)DishArray.ElementAt(3);
            case 4:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Please enter a valid number");
                break;
        }

        return null;
    }
    
    
    
    
}