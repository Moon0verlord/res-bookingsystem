using Newtonsoft.Json.Linq;
using System.Text.Json;

static class restaurantInfo
{
    private static int _currentIndex;

    static private MenuLogic _myMenu = new MenuLogic();
    static string Information =
@"
/Restaurant name/

Hier in Rotterdam vind je het prachtige /restaurant name/.
Wij serveren uit een keuze van 2, 3 of 4 gangen menu's. Ook is
het mogelijk om er een wijnarrangement bij te boeken. Naast 
het reserveren van een tafel is er ook beschikking tot een bar.";
    static string Contact =
@"
Contact Informatie:
Wijnhaven 107
3011 WN / Rotterdam
0612345678
/email/ 
";
    static string Events =
@"Speciale Evenementen:
Er zijn op dit moment nog geen evenementen.
Kom terug op een later moment om te zien of er al evenementen zijn.
";

    public static bool CheckIfEvent()
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        string json = File.ReadAllText(path);
        JArray eventmenu = JArray.Parse(json);
        foreach (var course in eventmenu)
        {
            if (json != null) return true;
        }
        return false;
    }

    public static string ResEvent()
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        string eventname = null;
        string eventinfo = null;
        string eventdate = null;
        while (true)
        {
            string[] options = new[]
                { "Vul hier in wat de event naam is." + (eventname == null ? "" : $": {eventname}"),
                "Vul hier de extra informatie over het event in." + (eventinfo == null ? "" : $": {eventinfo}"),
                "Vul hier de datum in van het event." + (eventdate == null ? "" : $": {eventname}"),
                "Het evenement definitief maken", "Ga terug" };
            int selectedIndex = _myMenu.RunMenu(options, "Kies hier uw groepsgrootte:");
            Console.Clear();
            switch (selectedIndex)
            {
                case 0:
                    Console.Write("Wat is de naam van het event: ");
                    eventname = Console.ReadLine()!;
                    break;
                case 1:
                    Console.Write("wat wordt de extra informatie van het event: ");
                    eventinfo = Console.ReadLine()!;
                    break;
                case 2:
                    Console.Write("wat wordt de datum van het event: ");
                    eventdate = Console.ReadLine()!;
                    break;
                case 3:
                    if (eventname != null && eventinfo != null && eventdate != null)
                    {
                        var allAccounts = LoadAll();
                        EventModel newAccount = new EventModel(eventname, eventinfo, eventdate);
                        allAccounts.Add(newAccount);
                        WriteAll(allAccounts);
                        Console.Clear();
                        Console.WriteLine("Het evenement is aangemaakt.");
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("U heeft nog niet alle informatie vereiste ingevuld.");
                        Thread.Sleep(3000);
                        ResEvent();
                    }
                    break;
                case 4:
                    Eventmenu();
                    break;
            }
        }
    }

    public static void Eventmenu()
    {
        string prompt = "Welkom in het menu voor special events. \n";
        string[] options = { "Organiseer event namens restaurant", "Organiseer event namens klant", "Terug naar hoofdmenu" };
        var selectedIndex = _myMenu.RunMenu(options, prompt);
        switch (selectedIndex)
        {
            case 0:
                Console.Clear();
                ResEvent();
                break;
            case 1:
                Console.Clear();
                ResEvent();
                break;
            case 2:
                Console.Clear();
                MainMenu.Start();
                break;
        }
    }

    public static List<EventModel> LoadAll()
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        string json = File.ReadAllText(path);
        JArray eventmenu = JArray.Parse(json);
        List<EventModel> events = new List<EventModel>();
        foreach (var course in eventmenu)
        {
            events.Add(new()
            {
                EventName = course["eventname"].ToString(),
                EventInfo = course["eventinfo"].ToString(),
                EventDate = course["datum"].ToString()
            });
        }
        return events;
    }

    public static void WriteAll(List<EventModel> accounts)
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }

    public static void Start()
    {
        List<EventModel> AllEvents = LoadAll();
        JArray eventmenu = JArray.Parse(AllEvents.ToString());
        Console.Clear();
        Console.WriteLine(Information);
        Console.WriteLine(Contact);
        if (CheckIfEvent())
        {
            Console.WriteLine("Alle evenementen:");
            foreach (var course in eventmenu)
            {
                Console.WriteLine(course["eventname"]);
                Console.WriteLine(course["eventinfo"]);
                Console.WriteLine(course["datum"]);
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine(Events);
        }
    }
}