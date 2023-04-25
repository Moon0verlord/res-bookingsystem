using System.Text.Json;
using Newtonsoft.Json.Linq;
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
                        var allAccounts = JsonSerializer.Deserialize<List<EventModel>>(File.ReadAllText(path)) ?? new List<EventModel>();
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

    public static void WriteAll(List<EventModel> accounts)
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }
}