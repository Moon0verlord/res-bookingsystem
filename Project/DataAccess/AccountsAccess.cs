using System.Text.Json;
using Newtonsoft.Json.Linq;

public static class AccountsAccess
{
    static string acc_path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
    static string res_path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));
    private static AccountsLogic _accountsLogic = new AccountsLogic();


    public static List<AccountModel> LoadAll()
    {
        string json = File.ReadAllText(acc_path);
        return JsonSerializer.Deserialize<List<AccountModel>>(json)!;
    }

    public static List<ReservationModel> LoadAllReservations()
    {
        string json = File.ReadAllText(res_path);
        return JsonSerializer.Deserialize<List<ReservationModel>>(json)!;
    }

    public static void WriteAll(List<AccountModel> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(acc_path, json);
    }

    public static void WriteAllReservations(List<ReservationModel> reservations)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(reservations, options);
        File.WriteAllText(res_path, json);
    }

    public static AccountModel AddAccount(string email, string password, string name, bool IsEmployee, bool IsManager)
    {
        var allAccounts = LoadAll();
        AccountModel newAccount = new AccountModel(allAccounts[^1].Id + 1, email, BCrypt.Net.BCrypt.HashPassword(password, 12), name, IsEmployee, IsManager);
        allAccounts.Add(newAccount);
        WriteAll(allAccounts);
        _accountsLogic.UpdateList(allAccounts[^1]);
        return allAccounts[^1];
    }

    public static void RemoveAccount(string email)
    {
        var allAccounts = LoadAll();
        var index = allAccounts.FindIndex(s => s.EmailAddress == email);
        allAccounts.RemoveAt(index);
        WriteAll(allAccounts);
    }

    public static void ChangeReservationJson(ReservationModel resm)
    {
        var allReservations = LoadAllReservations();
        var index = allReservations.FindIndex(s => s.Id == resm.Id);
        allReservations[index] = resm;
        WriteAllReservations(allReservations);
    }

    public static void AddReservation(ReservationModel resm)
    {
        resm.isReserved = true;
        var allReservations = LoadAllReservations();
        allReservations.Add(resm);
        WriteAllReservations(allReservations);
    }

    public static void RemoveReservation(ReservationModel resm)
    {
        var allReservations = LoadAllReservations();
        var index = allReservations.FindIndex(s => s.Id == resm.Id);
        allReservations.RemoveAt(index);
        WriteAllReservations(allReservations);
    }

    public static void EventWriteAll(List<EventModel> accounts)
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }

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
                    var ClearAccounts = accounts.GetRange(1, 3);
                    WriteAll(ClearAccounts);
                }
                break;
            case 2:
                if (reservations.Count > 1)
                {
                    var ClearReservations = reservations.GetRange(0, 0);
                    WriteAllReservations(ClearReservations);
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

    public static JArray ReadAllEvents()
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        string json = File.ReadAllText(path);
        JArray eventmenu = JArray.Parse(json);
        return eventmenu;

    }

    public static void WriteAllEventsJson(List<EventModel> accounts)
    {
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/Events.json"));
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }
}