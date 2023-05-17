using System.Text.Json;
using Newtonsoft.Json.Linq;
using Project.Presentation;

public class SpecialEvent
{
    private static int _currentIndex;

    static private MenuLogic _myMenu = new MenuLogic();
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
                "Vul hier de datum in van het event." + (eventdate == null ? "" : $": {eventdate}"),
                "Het evenement definitief maken", "Ga terug" };
            int selectedIndex = _myMenu.RunMenu(options, "Vul de volgende gegevens in:");
            Console.Clear();
            switch (selectedIndex)
            {
                case 0:
                    Console.Write("Wat is de naam van het event: (gebruik max 30 tekens!)");
                    eventname = Console.ReadLine()!;
                    int name_Length = eventname.Length;
                    if (name_Length > 30)
                    {
                        Console.WriteLine("De naam van het event is langer dan 30 tekens.");
                        Thread.Sleep(3000);
                        ResEvent();
                        break;
                    }
                    break;
                case 1:
                    Console.Write("wat wordt de extra informatie van het event: ");
                    eventinfo = Console.ReadLine()!;
                    break;
                case 2:
                    Console.Write("wat wordt de datum van het event: (gebruik deze format dd-MM-YYYY)");
                    eventdate = Console.ReadLine();
                    if (eventdate.Contains("-") && eventdate.Length == 10)
                    {
                        JArray allEvents = AccountsAccess.ReadAllEvents();
                        foreach (var event_item in allEvents)
                        {
                            if (eventdate == event_item["eventdate"].ToString())
                            {
                                Console.WriteLine("Er is al een evenement op deze datum.");
                                Thread.Sleep(3000);
                                ResEvent();
                                break;
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("U heeft niet de juiste format gebruikt.");
                        Thread.Sleep(3000);
                        ResEvent();
                        break;
                    }

                case 3:
                    if (eventname != null && eventinfo != null && eventdate != null)
                    {
                        var allEvents = JsonSerializer.Deserialize<List<EventModel>>(File.ReadAllText(path)) ?? new List<EventModel>();
                        EventModel newEvent = new EventModel(eventname, eventinfo, eventdate);
                        allEvents.Add(newEvent);
                        AccountsAccess.WriteAllEventsJson(allEvents);
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


    public static void EventsFood()
    {
         string prompt = "Welkom in het eten menu voor special events. \n";
        string[] options = { "Paas gerechten", "Kerst gerechten", "Terug naar hoofdmenu" };
        var selectedIndex = _myMenu.RunMenu(options, prompt);
        switch (selectedIndex)
        {
            case 0:
                Console.Clear();
                Dishes.JsonCursor("kerst");
                break;
            case 1:
                Console.Clear();
                Dishes.JsonCursor("pasen");
                break;
            case 2:
                Console.Clear();
                MainMenu.Start();
                break;
        }
    }

    public static void Eventmenu()
    {
        string prompt = "Welkom in het menu voor special events. \n";
        string[] options = { "Organiseer een evenement", "Eten","View Events","Terug naar hoofdmenu" };
        var selectedIndex = _myMenu.RunMenu(options, prompt);
        switch (selectedIndex)
        {
            case 0:
                Console.Clear();
                ResEvent();
                break;
            case 1:
                Console.Clear();
                EventsFood();
                break;
            case 2:
                Console.Clear();
                Start();
                break;
            case 3:
                Console.Clear();
                MainMenu.Start();
                break;
        }
    }
    
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

    public static void Start()
    {
        JArray eventmenu = AccountsAccess.ReadAllEvents();
        Console.Clear();

        if (CheckIfEvent())
        {
            Console.WriteLine("Alle evenementen:");
            foreach (var event_item in eventmenu)
            {
                Console.WriteLine(event_item["eventname"]);
                Console.WriteLine(event_item["eventinfo"]);
                Console.WriteLine(event_item["eventdate"]);
                Console.WriteLine();
            }
            Console.WriteLine("Druk op een knop om verder te gaan");
            Console.ReadKey();
            MainMenu.Start();
        }
        else
        {
            Console.WriteLine(Events);
        }
    }
    
    
}