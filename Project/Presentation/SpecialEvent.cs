using System.Text.Json;
using Newtonsoft.Json.Linq;
using Project.Presentation;

public class SpecialEvent
{
    private static int _currentIndex;

    private static MenuLogic _myMenu = new ();

    // function where you can see the events menu
    public static void Eventmenu()
    {
        string prompt = "Welkom in het menu voor special events. \n";
        string[] options = { "Bekijk het menu", "Aankomende evenementen bekijken", "Terug naar hoofdmenu" };
        var selectedIndex = _myMenu.RunMenu(options, prompt);
        switch (selectedIndex)
        {
            case 0:
                Console.Clear();
                EventsFood();
                break;
            case 1:
                Console.Clear();
                ViewEvents();
                break;
            case 2:
                Console.Clear();
                MainMenu.Start();
                break;
        }
    }

    // function to create a new event
    public static void ResEvent()
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        string eventName = null!;
        string eventInfo = null!;
        string eventDate = null!;
        while (true)
        {
            string[] options =
            {
                "Vul hier in wat de event naam is." + (eventName == null ? "" : $": {eventName}"),
                "Vul hier de extra informatie over het event in." + (eventInfo == null ? "" : $": {eventInfo}"),
                "Vul hier de datum in van het event." + (eventDate == null ? "" : $": {eventDate}"),
                "Het evenement definitief maken", "Ga terug"
            };
            int selectedIndex = _myMenu.RunMenu(options, "Vul de volgende gegevens in:");
            Console.Clear();
            switch (selectedIndex)
            {
                case 0:
                    Console.Write("Wat is de naam van het event: (gebruik max 30 tekens!)");
                    eventName = Console.ReadLine()!;
                    int nameLength = eventName.Length;
                    if (nameLength > 30)
                    {
                        Console.WriteLine("De naam van het event is langer dan 30 tekens.");
                        Thread.Sleep(3000);
                        ResEvent();
                    }

                    break;
                case 1:
                    Console.Write("wat wordt de extra informatie van het event: ");
                    eventInfo = Console.ReadLine()!;
                    break;
                case 2:
                    Console.Write("wat wordt de datum van het event: (gebruik deze format dd-MM-YYYY)");
                    eventDate = Console.ReadLine()!;
                    if (eventDate.Contains("-") && eventDate.Length == 10)
                    {
                        JArray allEvents = AccountsAccess.ReadAllEvents();
                        foreach (var eventItem in allEvents)
                        {
                            if (eventDate == eventItem["eventdate"]!.ToString())
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
                    if (eventName != null && eventInfo != null && eventDate != null)
                    {
                        var allEvents = JsonSerializer.Deserialize<List<EventModel>>(File.ReadAllText(path)) ??
                                        new List<EventModel>();
                        EventModel newEvent = new EventModel(eventName, eventInfo, eventDate);
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

    // function where you can see the events food
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

    static string _events =
        @"Speciale Evenementen:
Er zijn op dit moment nog geen evenementen.
Kom terug op een later moment om te zien of er al evenementen zijn.
";

    // function to check if there is an event
    public static bool CheckIfEvent()
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        string json = File.ReadAllText(path);
        JArray eventMenu = JArray.Parse(json);
        if (eventMenu.Count > 0)
        {
            return true;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Er zijn nog geen evenementen.");
        Console.ResetColor();
        Thread.Sleep(2000);
        UserLogin.DiscardKeys();
        MainMenu.Start();
        return false;
    }

    // display the events
    public static void ViewEvents()
    {
        DeleteOldEvents();
        JArray eventMenu = AccountsAccess.ReadAllEvents();
        Console.Clear();
        if (CheckIfEvent())
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Komende evenementen:");
            Console.ResetColor();
            foreach (var eventItem in eventMenu)
            {
                DateTime eventDate = DateTime.ParseExact(eventItem["eventdate"]!.ToString(), "dd-MM-yyyy", null);
                if (eventDate >= DateTime.Now)
                {
                    Console.WriteLine("-------");
                    Console.WriteLine($"{eventItem["eventname"]}:");
                    Console.WriteLine($"{eventItem["eventinfo"]}");
                    Console.WriteLine($"{eventItem["eventdate"]}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Druk op een knop om verder te gaan");
            Console.ReadKey();
            MainMenu.Start();
        }
    }
    
    // function to delete old events
    public static void DeleteOldEvents()
    {
        List<EventModel> eventMenu = AccountsAccess.ReadAllEvents().ToObject<List<EventModel>>()!;
        eventMenu.RemoveAll(eventItem =>
        {
            DateTime eventDate = DateTime.ParseExact(eventItem.EventDate, "dd-MM-yyyy", null);
            return eventDate < DateTime.Now;
        });
        AccountsAccess.WriteAllEventsJson(eventMenu);
    }

}