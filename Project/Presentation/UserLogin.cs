using System.Drawing;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new();
    static private MenuLogic myMenu = new();

    // starts the login process 
    // Manager login: Zxcvbnm1
    public static void Start()
    {
        string[] options = new[] { "Inloggen met account", "Account aanmaken", "Wachtwoord vergeten?", "Ga terug"};
        int chosenOption = myMenu.RunMenu(options, "Welkom bij de account portal.");
        switch (chosenOption)
        {
            case 0:
                Console.Clear();
                StartLogin();
                break;
            case 1:
                Console.Clear();
                StartAccCreation();
                break;
            case 2:
                PasswordReset();
                break;
            case 3:
                MainMenu.Start();
                break;
        }
    }

    public static void StartLogin()
    {
        string userEmail = null;
        string userPassword = null;
        while (true)
        {
            string[] options =
            {
                $"Vul hier uw e-mail in" + (userEmail == null ? "" : $": {userEmail}"),
                "Vul hier uw wachtwoord in" + $"{(userPassword == null ? "\n" : $": {HidePass(userPassword)}\n")}",
                "Doorgaan", "Ga terug"
            };
            int chosenOption = myMenu.RunMenu(options, "");
            switch (chosenOption)
            {
                case 0:
                    Console.CursorVisible = true;
                    Console.Clear();
                    InfoBoxes.WriteBoxUserEmail(Console.CursorTop, Console.CursorLeft);
                    Console.SetCursorPosition(1, 1);
                    Console.Write("\n Vul hier uw e-mail in: ");
                    userEmail = Console.ReadLine()!;
                    break;
                case 1:
                    Console.CursorVisible = true;
                    Console.Clear();
                    InfoBoxes.WriteBoxUserPassword(Console.CursorTop, Console.CursorLeft);
                    Console.SetCursorPosition(1, 1);
                    Console.Write("\n Vul hier uw wachtwoord in: ");
                    userPassword = WritePassword()!;
                    break;
                case 2:
                    if (userEmail == null || userPassword == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nVul uw gegevens in om een account aan te maken.");
                        Thread.Sleep(1500);
                        Console.ResetColor();
                    }
                    else
                    {
                        if (userEmail != null && userPassword != null)
                        {
                            accountsLogic.RefreshList();
                            AccountModel acc = accountsLogic.CheckLogin(userEmail, userPassword);
                            if (acc != null)
                            {
                                Console.Clear();
                                Console.WriteLine("Welkom terug " + acc.FullName + "!");
                                Console.WriteLine("Uw e-mail is " + acc.EmailAddress);
                                acc.LoggedIn = true;
                                Thread.Sleep(2000);
                                DiscardKeys();
                                MainMenu.Start(acc);
                            }
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"E-mail: {userEmail}\nWachtwoord: {HidePass(userPassword)}");
                            Console.WriteLine("Geen account gevonden met deze e-mail en wachtwoord.\nAls u nog geen account heeft kunt u er een aanmaken in het login menu.");
                            Thread.Sleep(3500);
                            DiscardKeys();
                            Console.ResetColor();
                        }
                    }
                    break;
                case 3:
                    Start();
                    break;
            }
        }
    }
    

    public static void StartAccCreation()
    {
        
    }

    public static void PasswordReset()
    {
        
    }
    private static AccountModel CreateAccount(string email, string password, string name,bool IsEmployee,bool IsManager)
    {
        var newAccount = AccountsAccess.AddAccount(email, password, name,IsEmployee,IsManager);
        return newAccount;
    }

    public static string HidePass(string pass)
    {
        string hiddenPass = "";
        for (int i = 0; i < pass.Length; i++)
        {
            hiddenPass += "*";
        }

        return hiddenPass;
    }

    public static string WritePassword()
    {
        string password = "";
        ConsoleKey currKey = default;
        do
        {
            var keyInfo = Console.ReadKey(true);
             currKey = keyInfo.Key;
            if (currKey == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[0..^1];
                Console.Write("\b \b");
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                password += keyInfo.KeyChar;
                Console.Write("*");
            }
        } while (currKey != ConsoleKey.Enter);
        return password;
    }

    public static bool PasswordCheck(string password)
    {
        if (password.Length < 8 || password.Length > 15) return false;
        if (password.ToLower() == password) return false;
        if (!password.Any(char.IsDigit)) return false;
        return true;
    }
    
    public static void DiscardKeys()
    {
        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }
    }
    
}