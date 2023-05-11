using System.Drawing;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new();
    static private MenuLogic myMenu = new();
    private static string userEmail;
    private static string userPassword;
    
    // starts the login process 
    
    // Manager login: Zxcvbnm1
    public static void Start()
    {
        userEmail = null;
        userPassword = null;
        while (true)
        {
            var prompt = "Welkom in het log in menu. \n";
            string[] options = { $"Vul hier uw e-mail in" + (userEmail == null ? "" : $": {userEmail}"),
                "Vul hier uw wachtwoord in" + $"{(userPassword == null ? "\n" : $": {HidePass(userPassword)}\n")}",
                "Nog geen account?\n  >Maak een nieuw account aan<", "Log in met huidige gegevens", "reset uw wachtwoord", "Ga terug" };
            var selectedIndex = myMenu.RunMenu(options, prompt);
            switch (selectedIndex)
            {
                case 0:
                    Console.CursorVisible = true;
                    Console.Clear();
                    InfoBoxes.WriteBoxUserEmail(Console.CursorTop, Console.CursorLeft);
                    Console.SetCursorPosition(1, 1);
                    Console.Write("\n Vul hier uw e-mail in: ");
                    userEmail = Console.ReadLine()!;
                    if (!EmailLogic.IsValidEmail(userEmail))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOnjuiste email.\nEmail moet minimaal een @ hebben en 3 tekens lang zijn.");
                        Console.ResetColor();
                        userEmail = null;
                        Thread.Sleep(3000);
                        DiscardKeys();
                    }
                    break;
                case 1:
                    Console.CursorVisible = true;
                    Console.Clear();
                    InfoBoxes.WriteBoxUserPassword(Console.CursorTop, Console.CursorLeft);
                    Console.SetCursorPosition(1, 1);
                    Console.Write("\n Vul hier uw wachtwoord in: ");
                    userPassword = WritePassword()!;
                    if(AccountsAccess.LoadAll().Find(user=> userEmail == user.EmailAddress)==null)
                    {
                        Console.Write("\n Vul uw wachtwoord opnieuw in voor bevestiging: ");
                        var verifyUserPassword = WritePassword()!;
                        if (userPassword == verifyUserPassword)
                            break;
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(
                                "\nHet bevestigings wachtwoord is anders dan het eerste wachtwoord, probeer opnieuw.");
                            Console.ResetColor();
                            Thread.Sleep(2000);
                            DiscardKeys();
                            userPassword = null;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
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
                        Console.Clear();
                        var emailExists = accountsLogic.GetByEmail(userEmail);
                        if (emailExists != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nEr bestaat al een account met deze e-mail.");
                            Thread.Sleep(2000);
                            DiscardKeys();
                            Console.ResetColor();
                        }
                        else
                        {
                            if (!PasswordCheck(userPassword))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nUw wachtwoord moet minimaal bestaan uit 8-15 karakters, en moet een hoofdletter en een cijfer bevatten.");
                                Console.ResetColor();
                                Thread.Sleep(2000);
                                DiscardKeys();
                                userPassword = null;
                                break;
                            }
                            Console.CursorVisible = true;
                            Console.Write("Vul hier uw volledige naam in: ");
                            var fullName = Console.ReadLine()!;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Clear();
                            Console.WriteLine(
                                $"\nVolledige naam: {fullName}\nE-mail: {userEmail}\nWachtwoord: " +
                                $"{HidePass(userPassword)}\nWeet u zeker dat u een account wil aanmaken met deze gegevens? (j/n)");
                            Console.ResetColor();
                            var answer = Console.ReadLine()!;
                            if (answer == "j" || answer == "J")
                            {
                                AccountsAccess.AddAccount(userEmail, userPassword, fullName, false, false);
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Account succesvol aangemaakt.");
                                Thread.Sleep(2500);
                                DiscardKeys();
                                Console.ResetColor();
                            }
                        }
                    }
                    break;
                case 3:
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
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"E-mail: {userEmail}\nWachtwoord: {HidePass(userPassword)}");
                            Console.WriteLine("Geen account gevonden met deze e-mail en wachtwoord.\nAls u nog geen account heeft kunt u er een aanmaken in het login menu.");
                            Thread.Sleep(3500);
                            DiscardKeys();
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nVul eerst uw gegevens in.");
                        Thread.Sleep(1500);
                        DiscardKeys();
                        Console.ResetColor();
                    }
                    break;
                /*case 4:
                    if (userEmail != null){
                        Console.Clear();
                        AccountModel acc = accountsLogic.GetByEmail(userEmail);
                        if (acc != null){
                            accountsLogic.ForgotPassword(userEmail);
                        }
                        Console.WriteLine("Er is een e-mail verstuurd naar " + userEmail + " met uw Verificatiecode.");
                        Console.Write("Vul hier uw Verificatiecode in: ");
                        string verificationCode = Console.ReadLine()!;
                        accountsLogic.ResetPassword(verificationCode, userEmail);
                    }
                    else{
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nVul eerst uw e-mailadres in.");
                        Thread.Sleep(1500);
                        DiscardKeys();
                        Console.ResetColor();
                    }
                    break;*/
                case 5:
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