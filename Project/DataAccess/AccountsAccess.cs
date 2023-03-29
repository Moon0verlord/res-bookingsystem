using System.Text.Json;

static class AccountsAccess
{
    static string acc_path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
    static string res_path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/reservations.json"));


    public static List<AccountModel> LoadAll()
    {
        string json = File.ReadAllText(acc_path);
        return JsonSerializer.Deserialize<List<AccountModel>>(json);
    }
    
    public static List<ReservationModel> LoadAllReservations()
    {
        string json = File.ReadAllText(res_path);
        return JsonSerializer.Deserialize<List<ReservationModel>>(json);
    }
    
    public static void WriteAll(List<AccountModel> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(acc_path, json);
    }

    public static AccountModel AddAccount(string email, string password, string name)
    {
        var allAccounts = LoadAll();
        AccountModel newAccount = new AccountModel(allAccounts[^1].Id + 1, email, password, name);
        allAccounts.Add(newAccount);
        WriteAll(allAccounts);
        return allAccounts[^1];
    }



}