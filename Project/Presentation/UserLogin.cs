using System.Drawing;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new();
    static private MenuLogic myMenu = new();
    private static string userEmail;
    private static string userPassword;
    public static void Start()
    {
        userEmail = null;
        userPassword = null;
        while (true)
        {
            var prompt = "Welkom in het log in menu. \n";
            string[] options = { $"Vul hier uw e-mail in" + (userEmail == null ? "" : $": {userEmail}"),
                "Vul hier uw wachtwoord in" + $"{(userPassword == null ? "\n" : $": {userPassword}\n")}",
                "Nog geen account?\n  >Log in met huidige gegevens<", "Log in met huidige gegevens", "Afsluiten" };
            var selectedIndex = myMenu.RunMenu(options, prompt);
            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    Console.Write("\n vul hier uw e-mail in: ");
                    userEmail = Console.ReadLine()!;
                    if (!userEmail.Contains("@") || userEmail.Length < 3)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOnjuiste email.\nEmail moet minimaal een @ hebben en 3 tekens lang zijn.");
                        Console.ResetColor();
                        userEmail = null;
                        Thread.Sleep(3000);
                    }
                    break;
                case 1:
                    Console.Clear();
                    Console.Write("\n Vul hier uw wachtwoord in: ");
                    userPassword = Console.ReadLine()!;
                    Console.Clear();
                    Console.Write("\n Vul uw wachtwoord opnieuw in voor bevestiging: ");
                    var verifyUserPassword = Console.ReadLine()!;
                    if (userPassword == verifyUserPassword)
                        break;
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nHet bevestigings wachtwoord is anders dan het eerste wachtwoord, probeer opnieuw.");
                        Console.ResetColor();
                        Thread.Sleep(2000);
                        userPassword = null;
                        break;
                    }
                case 2:
                    if (userEmail == null || userPassword == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nVul uw gegevens in op een account aan te maken.");
                        Thread.Sleep(1500);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Clear();
                        var emailExists = accountsLogic.GetByEmail(userEmail);
                        if (emailExists != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nEr bestaat al een account met deze e-mail.");
                            Thread.Sleep(2000);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write("Vul hier uw volledige naam in: ");
                            var fullName = Console.ReadLine()!;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Clear();
                            Console.WriteLine(
                                $"\nVolledige naam: {fullName}\nE-mail: {userEmail}\nWachtwoord: " +
                                $"{userPassword}\nWeet je zeker dat je een account wil aanmaken met deze gegevens? (j/n)");
                            Console.ResetColor();
                            var answer = Console.ReadLine()!;
                            if (answer == "j" || answer == "J")
                            {
                                var newAccount = CreateAccount(userEmail, userPassword, fullName,false,false);
                                accountsLogic.UpdateList(newAccount);
                            }
                        }
                    }
                    break;
                case 3:
                    if (userEmail != null && userPassword != null)
                    {
                        AccountModel acc = accountsLogic.CheckLogin(userEmail, userPassword);
                        if (acc != null)
                        {
                            Console.Clear();
                            Console.WriteLine("Welkom terug " + acc.FullName + "!");
                            Console.WriteLine("Uw e-mail is " + acc.EmailAddress);
                            acc.LoggedIn = true;
                            Thread.Sleep(2000);
                            MainMenu.Start(acc);
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"E-mail: {userEmail}\nWachtwoord: {userPassword}");
                            Console.WriteLine("Geen account gevonden met deze e-mail en wachtwoord.\nAls u nog geen account heeft kunt u er een aanmaken in het login menu.");
                            Thread.Sleep(3500);
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nVul eerst uw gegevens in.");
                        Thread.Sleep(1500);
                        Console.ResetColor();
                    }
                    break;
                case 4:
                    MainMenu.Start();
                    break;
            }
        }
    }

    private static AccountModel CreateAccount(string email, string password, string name,bool IsEmployee,bool IsManager)
    {
        var newAccount = AccountsAccess.AddAccount(email, password, name,IsEmployee,IsManager);
        return newAccount;
    }
}