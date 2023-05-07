using System.Globalization;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Project.Presentation;


public static class Dishes
{
    private static int _currentIndex;

    static private MenuLogic _myMenu = new MenuLogic();
    public static void WelcomeMenu()
    {
        Console.CursorVisible = false;
        string[] options = { "Vegetarisch", "Vis", "Vlees", "Veganistisch","Terug naar hoofdmenu" };
        string prompt = "\nKies een type gerechten:";
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
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
    }

    
    // Displays the current dishes to the user
    public static void JsonCursor(string choice)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Menu.json"));
        string json = File.ReadAllText(path);
        JObject menu = JObject.Parse(json);
        Console.Clear();
        string price2 = (string)menu["Vegetarisch"]["2_Gangen"][0]["Prijs"];
        string price3 = (string)menu["Vegetarisch"]["3_Gangen"][0]["Prijs"];
        string price4 = (string)menu["Vegetarisch"]["4_Gangen"][0]["Prijs"];
        Console.WriteLine("gerechten:");
        Console.WriteLine("-------");
        
    Console.WriteLine("2 Gangen:");
    var courses2 = menu[choice]["2_Gangen"]
        .Select(item => new
        {
            Appetizer = (string)item["Voorgerecht"],
            Entree = (string)item["Maaltijd"]
        });
    foreach (var course in courses2)
    {
        Console.WriteLine($"prijs: {price2}");
        Console.WriteLine($"Voorgerecht: {course.Appetizer}");
        Console.WriteLine($"Hooftgerecht: {course.Entree}");
        Console.WriteLine();
    }

    Console.WriteLine("3 Gangen:");
    var courses3 = menu[choice]["3_Gangen"]
        .Select(item => new
        {
            Appetizer = (string)item["Voorgerecht"],
            Entree = (string)item["Maaltijd"],
            Dessert = (string)item["Nagerecht"]
        });
    foreach (var course in courses3)
    {
        Console.WriteLine($"prijs: {price3}");
        Console.WriteLine($"Voorgerecht: {course.Appetizer}");
        Console.WriteLine($"Hoofdgerecht: {course.Entree}");
        Console.WriteLine($"Nagerecht: {course.Dessert}");
        Console.WriteLine();
    }

    Console.WriteLine("4 Gangen:");
    var courses4 = menu[choice]["4_Gangen"]
        .Select(item => new
        {
            Appetizer = (string)item["Voorgerecht"],
            Soup = (string)item["Soep"],
            Entree = (string)item["Maaltijd"],
            Dessert = (string)item["Nagerecht"]
        });
    foreach (var course in courses4)
    {
        Console.WriteLine($"prijs: {price4}");
        Console.WriteLine($"Voorgerecht: {course.Appetizer}");
        Console.WriteLine($"Soep: {course.Soup}");
        Console.WriteLine($"Hoofdgerecht: {course.Entree}");
        Console.WriteLine($"Nagerecht: {course.Dessert}");
        Console.WriteLine();
        Console.WriteLine("Druk op een knop om verder te gaan");
        Console.ReadKey();
        MainMenu.Start();
    }
}

    
    // adds the ability to update dishes on the menu
    public static void ManageMenu()
    {
        Console.CursorVisible = false;
        string type = "";
        string course = "";
        // select type of food
        string[] options = { "Vegetarisch", "Vis", "Vlees", "Veganistisch", "Bekijk het menu", "Terug naar hoofdmenu"};
        string prompt = "\nWelk type gerecht zou je willen veranderen?:";
        int input = _myMenu.RunMenu(options, prompt);
        _currentIndex = 0;
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
                WelcomeMenu();
                break;
            case 5:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
        
        // select course
        string[] options2 = { "2 Gangen", "3 Gangen", "4 Gangen", "Terug naar hoofdmenu"};
        string prompt2 = "\nWelke gang wil je toevoegen?:";
        int input2 = _myMenu.RunMenu(options2, prompt2);
        _currentIndex = 0;
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
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
        
        // grabs menu from json
        string path2 = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Menu.json"));
        string menu = File.ReadAllText(path2);
        JObject MenuOBJ = JObject.Parse(menu);
        // Gets dish from menu
        JArray menuCourse = (JArray)MenuOBJ[type]![course]!;
        JObject dishtoadd = DisplayOptions(type, course);

        // update values in dishtoadd object
        dishtoadd["Voorgerecht"] = (string)dishtoadd["Voorgerecht"] == "none" ? (string)menuCourse![0]["Voorgerecht"] : (string)dishtoadd["Voorgerecht"];
        dishtoadd["Soep"] = (string)dishtoadd["Soep"] == "none" ? (string)menuCourse![0]["Soep"] : (string)dishtoadd["Soep"];
        dishtoadd["Maaltijd"] = (string)dishtoadd["Maaltijd"] == "none" ? (string)menuCourse![0]["Maaltijd"] : (string)dishtoadd["Maaltijd"];
        dishtoadd["Nagerecht"] = (string)dishtoadd["Nagerecht"] == "none" ? (string)menuCourse![0]["Nagerecht"] : (string)dishtoadd["Nagerecht"];

        // keep the existing price
        dishtoadd["Prijs"] = (string)menuCourse![0]["Prijs"];
        // add dish to menu
        menuCourse![0] = dishtoadd;
        File.WriteAllText(path2, MenuOBJ.ToString());
        // displays added dish
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Het type menu: {type} in {course} is aangepast.");
        if (course == "2_Gangen")
        {
            Console.WriteLine($"Voorgerecht: {dishtoadd?["Voorgerecht"]}");
            Console.WriteLine($"Maaltijd: {dishtoadd?["Maaltijd"]}");
        } 
        else if (course == "3_Gangen")
        {
            Console.WriteLine($"Voorgerecht: {dishtoadd?["Voorgerecht"]}");
            Console.WriteLine($"Maaltijd: {dishtoadd?["Maaltijd"]}");
            Console.WriteLine($"Nagercht: {dishtoadd?["Nagerecht"]}");
        } 
        else if (course == "4_Gangen")
        {
            Console.WriteLine($"Voorgerecht: {dishtoadd?["Voorgerecht"]}");
            Console.WriteLine($"Soep: {dishtoadd?["Soep"]}");
            Console.WriteLine($"Maaltijd: {dishtoadd?["Maaltijd"]}");
            Console.WriteLine($"Nagercht: {dishtoadd?["Nagerecht"]}");
        } 
        Console.WriteLine("is toegevoegd aan het menu");
        Console.ResetColor();
        Console.WriteLine("Druk op een willekeurige toets om verder te gaan.....");
        Console.ReadKey();
        MainMenu.Start();
    }

    // Displays dishes and returns user selection to add to menu
    public static JObject DisplayOptions(string type, string course)
    {
        Console.CursorVisible = false;
        // Retrieves dishes from json 
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Dishes.json"));
        string json = File.ReadAllText(path);
        JObject dishes = JObject.Parse(json);
        JObject dish = (JObject)dishes[type]!;
        JArray dishArray = (JArray)dish[course]!;
        // Displays dishes based on course
        string dish1 = "";
        string dish2 = "";
        string dish3 = "";
        string dish4 = "";
        switch (course)
        {
            case "2_Gangen":
                 dish1 = $"Voorgerecht: {dishArray![0]["Voorgerecht"]} \nHoofdgerecht: {dishArray[0]["Maaltijd"]}\n";
                 dish2 = $"Voorgerecht: {dishArray[1]["Voorgerecht"]} \nHoofdgerecht: {dishArray[1]["Maaltijd"]}\n";
                 dish3 = $"Voorgerecht: {dishArray[2]["Voorgerecht"]} \nHoofdgerecht: {dishArray[2]["Maaltijd"]}\n";
                 dish4 = $"Voorgerecht: {dishArray[3]["Voorgerecht"]} \nHoofdgerecht: {dishArray[3]["Maaltijd"]}\n";
                break;
            case "3_Gangen":
                dish1 = $"Voorgerecht: {dishArray![0]["Voorgerecht"]} \nHoofdgerecht: {dishArray[0]["Maaltijd"]} \nNagerecht: {dishArray[0]["Nagerecht"]}\n";
                dish2 = $"Voorgerecht: {dishArray[1]["Voorgerecht"]} \nHoofdgerecht: {dishArray[1]["Maaltijd"]} \nNagerecht: {dishArray[1]["Nagerecht"]}\n";
                dish3 = $"Voorgerecht: {dishArray[2]["Voorgerecht"]} \nHoofdgerecht: {dishArray[2]["Maaltijd"]} \nNagerecht: {dishArray[2]["Nagerecht"]}\n";
                dish4 = $"Voorgerecht: {dishArray[3]["Voorgerecht"]} \nHoofdgerecht: {dishArray[3]["Maaltijd"]} \nNagerecht: {dishArray[3]["Nagerecht"]}\n";
                break;
            case "4_Gangen":
                dish1 = $"Voorgerecht: {dishArray![0]["Voorgerecht"]} \nSoep: {dishArray[0]["Soep"]} \nHoofdgerecht: {dishArray[0]["Maaltijd"]} \nNagerecht: {dishArray[0]["Nagerecht"]}\n";
                dish2 = $"Voorgerecht: {dishArray[1]["Voorgerecht"]} \nSoep: {dishArray[1]["Soep"]} \nHoofdgerecht: {dishArray[1]["Maaltijd"]} \nNagerecht: {dishArray[1]["Nagerecht"]}\n";
                dish3 = $"Voorgerecht: {dishArray[2]["Voorgerecht"]} \nSoep: {dishArray[2]["Soep"]} \nHoofdgerecht: {dishArray[2]["Maaltijd"]} \nNagerecht: {dishArray[2]["Nagerecht"]}\n";
                dish4 = $"Voorgerecht: {dishArray[3]["Voorgerecht"]} \nSoep: {dishArray[3]["Soep"]} \nHoofdgerecht: {dishArray[3]["Maaltijd"]} \nNagerecht: {dishArray[3]["Nagerecht"]}\n";
                break;
        }
        // returns selection
        string[] options = { dish1, dish2, dish3, dish4, "Terug naar hoofdmenu"};
        string prompt2 = "\nWelke maaltijd wil je veranderen?:";
        int input2 = _myMenu.RunMenu(options, prompt2);
        switch (input2)
        {
            case 0:
                return (JObject)dishArray!.ElementAt(0);
            case 1:
                return (JObject)dishArray!.ElementAt(1);
            case 2:
                return (JObject)dishArray!.ElementAt(2);
            case 3:
                return (JObject)dishArray!.ElementAt(3);
            case 4:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }

        return null!;
    }

    // Gives manager option to change price of items on menu
    public static void PriceManager()
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Menu.json"));
        string json = File.ReadAllText(path);
        JObject menu = JObject.Parse(json);
        string price2 = (string)menu["Vegetarisch"]["2_Gangen"][0]["Prijs"];
        string price3 = (string)menu["Vegetarisch"]["3_Gangen"][0]["Prijs"];
        string price4 = (string)menu["Vegetarisch"]["4_Gangen"][0]["Prijs"];
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        
        string[] options = { $"2 gang:{price2}", $"3 gang:{price3}", $"4 gang:{price4}", "Terug naar hoofdmenu" };
        string prompt = "\nWelke prijs zou je willen veranderen:";
        int input = _myMenu.RunMenu(options, prompt);
        switch (input)
        {
            case 0:
                Console.WriteLine("Voer een nieuwe prijs in:");
                string newprice2 = Console.ReadLine();
                menu["Vegetarisch"]["2_Gangen"][0]["Prijs"] = $"€ {newprice2}";
                break;
            case 1:
                Console.WriteLine("Voer een nieuwe prijs in:");
                string newprice3 = Console.ReadLine();
                menu["Vegetarisch"]["3_Gangen"][0]["Prijs"] = $"€ {newprice3}";
                break;
            case 2:
                Console.WriteLine("Voer een nieuwe prijs in:");
                string newprice4 = Console.ReadLine();
                menu["Vegetarisch"]["4_Gangen"][0]["Prijs"] = $"€ {newprice4}";
                break;
            case 3:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
        // save the updated JSON to the file
        string updatedJson = menu.ToString();
        File.WriteAllText(path, updatedJson);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nPrijs is bijgewerkt!\n");
        Console.ResetColor();
        Thread.Sleep(2000);
        UserLogin.DiscardKeys();
    }

    // Displays wines based on user selection
    public static void WineDisplay()
    {
        NumberFormatInfo euroFormat = new NumberFormatInfo();
        euroFormat.CurrencySymbol = "\u20AC";
        string choice = "";
        Console.CursorVisible = false;
        string[] options = { "Rode Wijn", "Witte Wijn", "Bubbles" };
        string prompt = "\nKies een type gerechten:";
        int input = _myMenu.RunMenu(options, prompt);
        switch (input)
        {
            case 0:
                choice = "red";
                break;
            case 1:
                choice = "white";
                break;
            case 2:
                choice = "sparkling";
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/WineMenu.json"));
        string json = File.ReadAllText(path);
        JObject Wines = JObject.Parse(json);
        Console.Clear();
        Console.WriteLine("Wijn menu:");
        Console.WriteLine("-------");
        var selection = Wines["Winemenu"][choice]
            .Select(item => new
            {
                name = (string)item["name"],
                price = decimal.Parse(((string)item["price"]).Replace("€", "")),
                region = (string)item["region"],
                description = (string)item["description"]
            });
        foreach (var wine in selection)
        {
            Console.WriteLine($"{wine.name}");
            Console.WriteLine($"{wine.region}");
            Console.WriteLine($"{wine.description}");
            Console.WriteLine($"Prijs per fles {wine.price.ToString("C", euroFormat)}");
            Console.WriteLine($"Prijs per glas {(wine.price / 4).ToString("C", euroFormat)}");
            Console.WriteLine("-------");
        }
        Console.WriteLine("Druk op een knop om verder te gaan");
        Console.ReadKey();
        MainMenu.Start();
    }
    
    // User options to choose between food menu and wines
    public static void UserOptions()
    {
        Console.CursorVisible = false;
        string[] options = { "Menu zien", "Wijn Selectie", "Terug naar hoofdmenu" };
        string prompt = "\nKies een type gerechten:";
        int input = _myMenu.RunMenu(options, prompt);
        switch (input)
        {
            case 0:
                WelcomeMenu();
                break;
            case 1:
                WineDisplay();
                break;
            case 2:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
    }
    
    
    // Manager options to choose between changing menu or changing prices
    public static void ManagerOptions()
    {
        Console.CursorVisible = false;
        string[] options = { "Menu veranderen", "Prijs veranderen", "Terug naar hoofdmenu" };
        string prompt = "\nKies een type gerechten:";
        int input = _myMenu.RunMenu(options, prompt);
        switch (input)
        {
            case 0:
                ManageMenu();
                break;
            case 1:
                PriceManager();
                break;
            case 2:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
    }
    
}