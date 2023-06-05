using System.Text.Json;
using Newtonsoft.Json.Linq;

public static class AccountsAccess
{
    private static readonly string AccPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
    private static readonly string  ResPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));
    private static readonly string MenuPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Menu.json"));
    private static readonly AccountsLogic AccountsLogic = new ();

    // load all accounts from the json file
    public static List<AccountModel> LoadAll()
    {
        try
        {
            string json = File.ReadAllText(AccPath);
            return JsonSerializer.Deserialize<List<AccountModel>>(json)!;
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"Missing JSON file. {e.Message}");
            return null!;
        }
    }

    // load all reservations from the json file
    public static List<ReservationModel> LoadAllReservations()
    {
        try
        {
            string json = File.ReadAllText(ResPath);
            return JsonSerializer.Deserialize<List<ReservationModel>>(json)!;
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"Missing JSON file. {e.Message}");
            return null!;
        }
    }
    
    public static JObject LoadAllMenu()
    {
        try
        {
            string json = File.ReadAllText(MenuPath);
            return JObject.Parse(json);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"Missing JSON file. {e.Message}");
            return null!;
        }
    }

    // write all accounts to the json file
    public static void WriteAll(List<AccountModel> accounts)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(accounts, options);
            File.WriteAllText(AccPath, json);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"Missing JSON file. {e.Message}");
        }
    }

    // write all reservations to the json file
    public static void WriteAllReservations(List<ReservationModel> reservations)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(reservations, options);
            File.WriteAllText(ResPath, json);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"Missing JSON file. {e.Message}");
        }
    }

    // add an account to the json file
    public static AccountModel AddAccount(string? email, string password, string name, bool IsEmployee, bool IsManager)
    {
        var allAccounts = LoadAll();
        AccountModel newAccount = new AccountModel(allAccounts[^1].Id + 1, email, BCrypt.Net.BCrypt.HashPassword(password, 12), name, IsEmployee, IsManager);
        allAccounts.Add(newAccount);
        WriteAll(allAccounts);
        AccountsLogic.UpdateList(allAccounts[^1]);
        return allAccounts[^1];
    }
    

    // remove an account from the json file
    public static void RemoveAccount(string email)
    {
        var allAccounts = LoadAll();
        var index = allAccounts.FindIndex(s => s.EmailAddress == email);
        allAccounts.RemoveAt(index);
        WriteAll(allAccounts);
    }

    // change an reservation in the json file
    public static void ChangeReservationJson(ReservationModel resm)
    {
        var allReservations = LoadAllReservations();
        var index = allReservations.FindIndex(s => s.Id == resm.Id);
        allReservations[index] = resm;
        WriteAllReservations(allReservations);
    }

    // add a reservation to the json file
    public static void AddReservation(ReservationModel resm)
    {
        resm.IsReserved = true;
        var allReservations = LoadAllReservations();
        allReservations.Add(resm);
        WriteAllReservations(allReservations);
    }

    // remove a reservation from the json file
    public static void RemoveReservation(ReservationModel resm)
    {
        var allReservations = LoadAllReservations();
        var index = allReservations.FindIndex(s => s.Id == resm.Id);
        allReservations.RemoveAt(index);
        WriteAllReservations(allReservations);
    }

    // Write all events to the json file
    public static void EventWriteAll(List<EventModel> accounts)
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }

    // Clear all events from the json file
    public static void ClearJsonFiles(int choice)
    {
        //1 Clears Accounts
        //2 Clear Reservations
        var accounts = LoadAll();
        var reservations = LoadAllReservations();
        switch (choice)
        {
            case 1:
                if (accounts.Count > 3)
                {
                    var clearAccounts = accounts.GetRange(1, 3);
                    WriteAll(clearAccounts);
                }
                break;
            case 2:
                if (reservations.Count > 1)
                {
                    var clearReservations = reservations.GetRange(0, 0);
                    WriteAllReservations(clearReservations);
                }

                break;
            case 3:
                Console.WriteLine("--Accounts--");
                foreach (var item in accounts)
                {
                    Console.WriteLine(item.FullName);
                }
                Console.WriteLine("--Reservations--");
                foreach (var reservation in reservations)
                {
                    Console.WriteLine(reservation.Id);
                }
                break;
        }
    }

    // Read all events from the json file
    public static JArray ReadAllEvents()
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        string json = File.ReadAllText(path);
        JArray eventmenu = JArray.Parse(json);
        return eventmenu;

    }

    // Write all events to the json file
    public static void WriteAllEventsJson(List<EventModel> accounts)
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }
}