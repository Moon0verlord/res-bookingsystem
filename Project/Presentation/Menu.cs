using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Project.Presentation;

public static class Dishes
{
    private static int _currentIndex;

    private static MenuLogic _myMenu = new();
    
    // Takes user selection and opens the menu
    public static void UserSelection()
    {
        Console.CursorVisible = false;
        string[] options = { "Vegetarisch", "Vis", "Vlees", "Veganistisch", "Terug naar hoofdmenu" };
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
        string price2 = (string)menu["Prijzen"]!["Prijzen"]![0]!;
        string price3 = (string)menu["Prijzen"]!["Prijzen"]![1]!;
        string price4 = (string)menu["Prijzen"]!["Prijzen"]![2]!;
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Prijzen:");
        Console.WriteLine("-------");
        Console.ResetColor();
        Console.WriteLine($"2 gangen: {price2}");
        Console.WriteLine($"3 gangen: {price3}");
        Console.WriteLine($"4 gangen: {price4}");
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{choice} gerechten:");
        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Voorgerechten:");
        Console.WriteLine("--------------");
        Console.ResetColor();
        var appetizers2 = menu[choice]!["Voorgerecht"]!
            .Select(item => (string)item!);
        foreach (var appetizer in appetizers2)
        {
            Console.WriteLine($"• {appetizer}");
        }
        

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Soepen:");
        Console.WriteLine("-------");
        Console.ResetColor();
        var soups = menu[choice]!["Soep"]!
            .Select(item => (string)item!);
        foreach (var soup in soups)
        {
            Console.WriteLine($"• {soup}");
        }
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Hoofdgerechten:");
        Console.WriteLine("----------------");
        Console.ResetColor();
        var entrees = menu[choice]!["Maaltijd"]!
            .Select(item => (string)item!);
        foreach (var entree in entrees)
        {
            Console.WriteLine($"• {entree}");
        }
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Nagerechten:");
        Console.WriteLine("------------");
        Console.ResetColor();
        var desserts = menu[choice]!["Nagerecht"]!
            .Select(item => (string)item!);
        foreach (var dessert in desserts)
        {
            Console.WriteLine($"• {dessert}");
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Druk op een knop om verder te gaan......");
        Console.ResetColor();
        Console.ReadKey();
        MainMenu.Start();
    }

    // adds the ability to update dishes on the menu
    public static void ManageMenu()
    {
        Console.CursorVisible = false;
        string type = "";
        string category = "";
        
        
        // select type of food
        string[] options = { "Vegetarisch", "Vis", "Vlees", "Veganistisch", "kerst gerechten", "paas gerechten", "Terug naar hoofdmenu" };
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
                type = "kerst";
                break;
            case 5:
                type = "pasen";
                break;
            case 6:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }

        // select course
        string[] options2 = { "Voorgerecht", "Maaltijd", "Soep ","Nagerecht", "Terug naar hoofdmenu" };
        string prompt2 = "\nWat wil je toevoegen?:";
        int input2 = _myMenu.RunMenu(options2, prompt2);
        _currentIndex = 0;
        switch (input2)
        {
            case 0:
                category = "Voorgerecht";
                break;
            case 1:
                category = "Maaltijd";
                break;
            case 2:
                category = "Soep";
                break;
            case 3:
                category = "Nagerecht";
                break;
            case 4:
                MainMenu.Start();
                break;
            
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
        
        // grabs menu from json
        string path2 = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Menu.json"));
        string menu = File.ReadAllText(path2);
        JObject menuObj = JObject.Parse(menu);
        int dishIndex = 0;
        // Retrieve dish to change
        JArray menuCourse = (JArray)menuObj[type]![category]!;

        string[] options3 = menuCourse.Select((dish, index) => $"{dish} ({index + 1})").ToArray();
        options3 = options3.Append("Terug naar hoofdmenu").ToArray();
        string prompt3 = "\nWelke wil je vervangen?:";
        int input3 = _myMenu.RunMenu(options3, prompt3);
        _currentIndex = 0;
        //  dynamic index for dish to change
        if (input3 >= 1 && input3 <= menuCourse.Count)
        {
            dishIndex = input3 - 1;
        }
        else if (input3 == menuCourse.Count + 1)
        {
            MainMenu.Start();
        }
        else
        { 
            Console.WriteLine("Keuze ongeldig probeer opnieuw");
        }
        
        // displays available dishes to change to
        JObject Newdish = DisplayDishes(type, category);


        // checks if dish is the same as the one that is already on the menu 
        // if it is not the same it will update the dish
        JArray selectionArray = (JArray)menuObj[type]![category]!;
        string updatedValue = Newdish[category]?.ToString()!;
        foreach (var dish in selectionArray)
        {
            if (Newdish.GetValue(category)!.ToString() ==  dish.ToString() || Newdish.GetValue(category)!.ToString() == dish.ToString())
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Het gekozen gerecht staat al op het menu");
                Thread.Sleep(2000);
                Console.ResetColor();
                UserLogin.DiscardKeys();
                ManageMenu();
            }
        }
        selectionArray[dishIndex] = updatedValue;

        // writes the updated menu to the json file
        File.WriteAllText(path2, menuObj.ToString());

        // Displays the added dish to the user
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Het type menu: {type} in {category} is aangepast.");
        switch (category)
        {
            case "Voorgerecht":
                Console.WriteLine($"Voorgerecht: {Newdish["Voorgerecht"]}");
                break;
            case "Soep":
                Console.WriteLine($"Soep: {Newdish["Soep"]}");
                break;
            case "Maaltijd":
                Console.WriteLine($"Maaltijd: {Newdish["Maaltijd"]}");
                break;
            case "Nagerecht":
                Console.WriteLine($"Nagerecht: {Newdish["Nagerecht"]}");
                break;
        }
        Console.WriteLine("is toegevoegd aan het menu");
        Console.ResetColor();
        Console.WriteLine("Druk op een willekeurige toets om verder te gaan.....");
        Console.ReadKey();
        MainMenu.Start();
    }

    // Displays dishes and returns user selection to add to menu
    public static JObject DisplayDishes(string type, string category)
    {
        Console.CursorVisible = false;
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Dishes.json"));
        string json = File.ReadAllText(path);
        JObject dishes = JObject.Parse(json);
        JArray dishArray = (JArray)dishes[type]![category]!;

        // Display dishes in category for user to choose from
        string[] options = new string[dishArray.Count + 1];
        for (int i = 0; i < dishArray.Count; i++)
        {
            string dish = (string)dishArray[i]!;
            options[i] = dish;
        }
        options[dishArray.Count] = "Terug naar hoofdmenu";

        // Get user selection
        string prompt = $"\nWelke maaltijd wil je veranderen in de categorie '{category}'?:";
        int input = _myMenu.RunMenu(options, prompt);
    
        if (input >= 0 && input < dishArray.Count)
        {
            return new JObject(new JProperty(category, dishArray[input]));
        } 
        if (input == dishArray.Count)
        {
            MainMenu.Start();
        }
        else
        {
            Console.WriteLine("Keuze ongeldig. Probeer opnieuw.");
        }

        return null!;
    }




    // Gives manager option to change price of items on menu
    public static void PriceManager() 
    {
    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Menu.json"));
    string json = File.ReadAllText(path);
    JObject menu = JObject.Parse(json);
    string? price2 = (string)menu["Prijzen"]!["Prijzen"]![0]!;
    string price3 = (string)menu["Prijzen"]!["Prijzen"]![1]!;
    string price4 = (string)menu["Prijzen"]!["Prijzen"]![2]!;
    Console.CursorVisible = true;
    Console.OutputEncoding = System.Text.Encoding.Unicode;
    string[] options = { $"2 gang:{price2}", $"3 gang:{price3}", $"4 gang:{price4}", "Terug naar hoofdmenu" };
    string prompt = "\nWelke prijs wil je veranderen:";
    int input = _myMenu.RunMenu(options, prompt);
    switch (input)
    {
        case 0:
            Console.WriteLine("Voer een nieuwe prijs in het formaat 00,00:");
            string newprice2 = Console.ReadLine()!;
            if (IsValidPriceFormat(newprice2))
            {
                menu["Prijzen"]!["Prijzen"]![0] = $"€ {newprice2}";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nPrijs is bijgewerkt!\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ongeldige prijsindeling. Voer een prijs in het formaat 00,00.");
                Console.ResetColor();
                Thread.Sleep(2000);
                PriceManager();
            }
            break;
        case 1:
            Console.WriteLine("Voer een nieuwe prijs in:");
            string newprice3 = Console.ReadLine()!;
            if (IsValidPriceFormat(newprice3))
            {
                menu["Prijzen"]!["Prijzen"]![1] = $"€ {newprice3}";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nPrijs is bijgewerkt!\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ongeldige prijsindeling. Voer een prijs in het formaat 00,00.");
                Console.ResetColor();
                Thread.Sleep(2000);
                PriceManager();
            }
            break;
        case 2:
            Console.WriteLine("Voer een nieuwe prijs in:");
            string newprice4 = Console.ReadLine()!;
            if (IsValidPriceFormat(newprice4))
            {
                menu["Prijzen"]!["Prijzen"]![2] = $"€ {newprice4}";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nPrijs is bijgewerkt!\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ongeldige prijsindeling. Voer een prijs in het formaat 00,00.");
                Console.ResetColor();
                Thread.Sleep(2000);
                PriceManager();
            }
            break;
        case 3:
            MainMenu.Start();
            break;
        default:
            Console.WriteLine("Keuze ongeldig. Probeer opnieuw.");
            break;
    }
    
    // write the updated JSON to the file
    string updatedJson = menu.ToString();
    File.WriteAllText(path, updatedJson);
    Thread.Sleep(2000);
    UserLogin.DiscardKeys();
}


    // Use regex to validate the price format (00,00)
    private static bool IsValidPriceFormat(string price) => Regex.IsMatch(price, @"^\d{1,3}(,\d{2})?$");
    // Displays wines based on user selection
    public static void WineDisplay()
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        string choice = "";
        Console.CursorVisible = false;
        string[] options = { "Rode Wijn", "Witte Wijn", "Champagne", "Terug naar hoofdmenu"};
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
            case 3:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/WineMenu.json"));
        string json = File.ReadAllText(path);
        JObject wines = JObject.Parse(json);
        Console.Clear();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{choice} Wijn Arrangement:");
        Console.WriteLine("-------");
        Console.ResetColor();
        var selection = wines["Winemenu"]![choice]!
            .Select(item => new
            {
                name = (string)item["name"]!,
                price = (string)item["price"]!,
                region = (string)item["region"]!,
                description = (string)item["description"]!
            });
        foreach (var wine in selection)
        {
            Console.WriteLine($"{wine.name}");
            Console.WriteLine($"{wine.region}");
            Console.WriteLine($"{wine.description}");
            Console.WriteLine($"Prijs per fles: {wine.price}");
            Match match = Regex.Match(wine.price, @"^€(\d+),\d+");
            Console.WriteLine($"Price per glass is: €{Convert.ToInt32(match.Groups[1].Value) / 4},00");
            Console.WriteLine("-------");
            Console.WriteLine();
        }
        Console.WriteLine("Druk op een knop om verder te gaan......");
        Console.ReadKey();
        MainMenu.Start();
    }
    
    // Gives manager option to add dish to dishes.json
    public static void AddToDishes()
    {
        Console.CursorVisible = true;
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Dishes.json"));
        string json = File.ReadAllText(path);
        JObject dishesObj = JObject.Parse(json);

        string type = "";
        string category = "";
        string name = "";

        string[] options = { "Vegetarisch", "Vis", "Vlees", "Veganistisch", "kerst gerechten", "paas gerechten", "Terug naar hoofdmenu" };
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
                type = "kerst";
                break;
            case 5:
                type = "pasen";
                break;
            case 7:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }

        // select course
        string[] options2 = { "Voorgerecht", "Maaltijd", "Soep", "Nagerecht", "Terug naar hoofdmenu" };
        string prompt2 = "\nWat wil je toevoegen?:";
        int input2 = _myMenu.RunMenu(options2, prompt2);
        _currentIndex = 0;
        switch (input2)
        {
            case 0:
                category = "Voorgerecht";
                break;
            case 1:
                category = "Maaltijd";
                break;
            case 2:
                category = "Soep";
                break;
            case 3:
                category = "Nagerecht";
                break;
            case 4:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
        Console.CursorVisible = true;
        // takes dish name from user and writes this to the json
        Console.WriteLine("Wat is de naam van het gerecht?:");
        name = Console.ReadLine()!;

        if (!IsValidDishName(name) || name == "")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ongeldige gerechtnaam. Voer een geldige naam in zonder speciale tekens.");
            Console.ResetColor();
            Thread.Sleep(2000);
            MainMenu.Start();
            return;
        }

        JArray selectionArray = (JArray)dishesObj[type]![category]!;
        selectionArray.Add(name);
        string output = Newtonsoft.Json.JsonConvert.SerializeObject(dishesObj, Newtonsoft.Json.Formatting.Indented);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{name} is Toegevoegd aan de Dishes\n");
        Console.ResetColor();
        File.WriteAllText(path, output);
        Console.WriteLine("Druk op een knop om verder te gaan......");
        Console.ReadKey();
        MainMenu.Start();
    }

    // Use regex to validate the dish name (only letters and spaces allowed)
    private static bool IsValidDishName(string name)
    {
        var pattern = @"^[a-zA-Z\s]+$";
        return Regex.IsMatch(name, pattern);
    }

    
    // User options to choose between food menu or wine arrangement
    public static void UserOptions()
    {
        Console.CursorVisible = false;
        string[] options = { "Bekijk het menu", "Wijn Arrangement", "Terug naar hoofdmenu" };
        var prompt = "\nKies een optie:";
        int input = _myMenu.RunMenu(options, prompt);
        switch (input)
        {
            case 0:
                UserSelection();
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
        string[] options = { "Menu veranderen", "Prijs veranderen","Gerecht toevoegen", "Terug naar hoofdmenu" };
        string prompt = "\nKies een optie:";
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
                AddToDishes();
                break;
            case 3:
                MainMenu.Start();
                break;
            default:
                Console.WriteLine("Keuze ongeldig probeer opnieuw");
                break;
        }
    }
    
}