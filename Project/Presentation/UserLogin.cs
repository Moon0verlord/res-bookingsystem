public static class UserLogin
{
    private static AccountsLogic _accountsLogic = new();
    private static MenuLogic _myMenu = new();

    // starts the login process 
    // Manager login: Zxcvbnm1
    public static void Start()
    {
        string[] options = { "Inloggen met account", "Account aanmaken\n", "Wachtwoord vergeten?", "Ga terug"};
        int chosenOption = _myMenu.RunMenu(options, "Welkom bij de account portal.");
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

    // starts the login process
    public static void StartLogin()
    {
        string? userEmail = null!;
        string userPassword = null!;
        while (true)
        {
            string[] options =
            {
                $"Vul hier uw e-mail in" + (userEmail == null ? "" : $": {userEmail}"),
                "Vul hier uw wachtwoord in" + $"{(userPassword == null ? "\n" : $": {HidePass(userPassword)}\n")}",
                "Inloggen met ingevulde gegevens", "Ga terug"
            };
            int chosenOption = _myMenu.RunMenu(options, "");
            switch (chosenOption)
            {
                case 0:
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.SetCursorPosition(1, 1);
                    Console.Write("\n Vul hier uw e-mail in: ");
                    userEmail = Console.ReadLine()!;
                    break;
                case 1:
                    Console.CursorVisible = true;
                    Console.Clear();
                    InfoBoxes.WritePasswordToggle(Console.CursorTop, Console.CursorLeft, false);
                    Console.SetCursorPosition(1, 1);
                    Console.Write("\n Vul hier uw wachtwoord in: ");
                    userPassword = WritePassword();
                    break;
                case 2:
                    if (userEmail == null || userPassword == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nVul eerst uw gegevens in om in te loggen.");
                        Thread.Sleep(1500);
                        Console.ResetColor();
                    }
                    else
                    {
                        if (userEmail != null! && userPassword != null!)
                        {
                            _accountsLogic.RefreshList();
                            AccountModel acc = _accountsLogic.CheckLogin(userEmail, userPassword);
                            if (acc != null!)
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
    

    // starts the account creation process
    public static void StartAccCreation()
    {
        string? userEmail = null!;
        string userPassword = null!;
        string fullName = null!;
        while (true)
        {
            string[] options =
            {
                $"Vul hier uw e-mail in" + (userEmail == null ? "" : $": {userEmail}"),
                "Vul hier uw wachtwoord in" + $"{(userPassword == null ? "" : $": {HidePass(userPassword)}")}",
                "Vul hier uw volledige naam in" + (fullName == null ? "\n" : $": {fullName}\n"), "Account aanmaken met ingevulde gegevens", "Ga terug" 
            };
             int chosenOption = _myMenu.RunMenu(options, "");
            switch (chosenOption)
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
                        userEmail = null!;
                        Thread.Sleep(3000);
                        DiscardKeys();
                    }
                    break;
                case 1:
                    Console.CursorVisible = true;
                    Console.Clear();
                    InfoBoxes.WritePasswordToggle(Console.CursorTop, Console.CursorLeft, true);
                    // cursor reset to write next box correctly
                    Console.SetCursorPosition(0, 0);
                    InfoBoxes.WriteBoxUserPassword(Console.CursorTop, Console.CursorLeft);
                    Console.SetCursorPosition(1, 1);
                    Console.Write("\n Vul hier uw wachtwoord in: ");
                    userPassword = WritePassword();
                    if (!PasswordCheck(userPassword))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nUw wachtwoord moet minimaal bestaan uit 8-15 karakters, en moet een hoofdletter en een cijfer bevatten.");
                        Console.ResetColor();
                        Thread.Sleep(2000);
                        DiscardKeys();
                        userPassword = null!;
                        break;
                    }
                    Console.Write("\n Vul uw wachtwoord opnieuw in voor bevestiging: ");
                    var verifyUserPassword = WritePassword();
                    if (userPassword != verifyUserPassword) 
                    {
                      Console.ForegroundColor = ConsoleColor.Red;
                      Console.WriteLine(
                          "\nHet bevestigings wachtwoord is anders dan het eerste wachtwoord, probeer opnieuw.");
                      Console.ResetColor();
                      Thread.Sleep(2000);
                      DiscardKeys();
                      userPassword = null!;
                    }
                    break;
                case 2:
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.SetCursorPosition(1, 1);
                    Console.Write("\n Vul hier uw volledige naam in: ");
                    fullName = Console.ReadLine()!;
                    break;
                case 3:
                    if (userEmail == null || userPassword == null || fullName == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nVul eerst uw gegevens in om een account aan te maken.");
                        Thread.Sleep(1500);
                        Console.ResetColor();
                    }
                    else
                    {
                        var emailExists = _accountsLogic.GetByEmail(userEmail);
                        if (emailExists != null!)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(
                                "\nEr bestaat al een account met deze e-mail, als u wilt inloggen kan dit in de login portal.");
                            Thread.Sleep(2000);
                            DiscardKeys();
                            Console.ResetColor();
                        }
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(
                            $"\nVolledige naam: {fullName}\nE-mail: {userEmail}\nWachtwoord: " +
                            $"{HidePass(userPassword)}\nWeet u zeker dat u een account wil aanmaken met deze gegevens? (j/n)");
                        Console.ResetColor();
                        var answer = Console.ReadLine()!;
                        if (AnswerLogic.CheckInput(answer)==1)
                        {
                            AccountsAccess.AddAccount(userEmail, userPassword, fullName, false, false);
                            Console.Clear();
                            Console.WriteLine("Account succesvol aangemaakt.");
                            Thread.Sleep(2500);
                            DiscardKeys();
                            Start();
                            Console.ResetColor();
                        }
                        
                    }
                    break;
                case 4:
                    Start();
                    break;
            }
        }
    }

    
    // Gives the user the option to reset their password
    public static void PasswordReset()
    {
        string? userEmail = null!;
        while (true)
        {
            string[] options = { $"Vul hier uw e-mail in" + (userEmail == null ? "\n" : $": {userEmail}\n"), "Reset wachtwoord voor ingevulde email", "Ga terug" };
            int chosenOption = _myMenu.RunMenu(options, "Vul hier uw wachtwoord in om uw wachtwoord te resetten:");
            switch (chosenOption)
            {
                case 0:
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.SetCursorPosition(1, 1);
                    Console.Write("\n Vul hier uw e-mail in: ");
                    userEmail = Console.ReadLine()!;
                    break;
                case 1:
                    if (userEmail != null)
                    {
                        Console.Clear();
                        AccountModel acc = _accountsLogic.GetByEmail(userEmail);
                        if (acc != null!) 
                            _accountsLogic.ForgotPassword(userEmail);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nVul eerst uw e-mailadres in.");
                        Thread.Sleep(1500);
                        DiscardKeys();
                        Console.ResetColor();
                    }
                    
                    break;
                case 2:
                    Start();
                    break;
            }
        }
    }
    private static AccountModel CreateAccount(string? email, string password, string name,bool isEmployee,bool isManager)
    {
        var newAccount = AccountsAccess.AddAccount(email, password, name,isEmployee,isManager);
        return newAccount;
    }

    // hides the password with stars
    public static string HidePass(string pass)
    {
        string hiddenPass = "";
        for (int i = 0; i < pass.Length; i++)
        {
            hiddenPass += "*";
        }

        return hiddenPass;
    }

    // hides the password with stars when writing
    public static string WritePassword()
    {
        string currentMode = "stars";
        string password = "";
        ConsoleKey currKey;
        do
        {
            var keyInfo = Console.ReadKey(true);
             currKey = keyInfo.Key;
            if (currKey == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[0..^1];
                Console.Write("\b \b");
            }
            else if (currKey == ConsoleKey.F1)
            {
                Console.Write(String.Concat(Enumerable.Repeat("\b \b", password.Length)));
                if (currentMode == "stars")
                {
                    Console.Write(password);
                    currentMode = "words";
                }
                else if (currentMode == "words")
                {
                    Console.Write(String.Concat(Enumerable.Repeat("*", password.Length)));
                    currentMode = "stars";
                }
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                password += keyInfo.KeyChar;
                if (currentMode == "stars")
                    Console.Write("*");
                else if (currentMode == "words")
                    Console.Write(keyInfo.KeyChar);
            }
        } while (currKey != ConsoleKey.Enter);
        // make sure the password is set to stars when the user presses enter
        // otherwise password will be visible when confirming.
        Console.Write(String.Concat(Enumerable.Repeat("\b \b", password.Length)));
        Console.Write(String.Concat(Enumerable.Repeat("*", password.Length)));
        return password;
    }

    // checks if the password meets the requirements
    public static bool PasswordCheck(string password)
    {
        if (password.Length < 8 || password.Length > 15) return false;
        if (password.ToLower() == password) return false;
        if (!password.Any(char.IsDigit)) return false;
        return true;
    }
    
    // Discard keys in stream to prevent spamming
    public static void DiscardKeys()
    {
        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }
    }
    
}