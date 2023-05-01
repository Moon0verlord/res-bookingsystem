using System.Text.Json;

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

    public static AccountModel AddAccount(string email, string password, string name,bool IsEmployee,bool IsManager)
    {
        var allAccounts = LoadAll();
        AccountModel newAccount = new AccountModel(allAccounts[^1].Id + 1, email, BCrypt.Net.BCrypt.HashPassword(password, 12), name,IsEmployee,IsManager);
        allAccounts.Add(newAccount);
        WriteAll(allAccounts);
        _accountsLogic.UpdateList(allAccounts[^1]);
        return allAccounts[^1];
    }

    public static void AddReservation(ReservationModel resm)
    {
        resm.isReserved = true;
        var allReservations = LoadAllReservations();
        allReservations.Add(resm);
        WriteAllReservations(allReservations);
    }


}