public class EmployeeManagerLogic : IMenuLogic
{
    private static MenuLogic _myMenu = new ();
    private static AccountsLogic _logicMenu = new ();
    static private MenuLogic myMenu = new();
    private static string employeeEmail;
    private static string employeePassword;
    //Employee and manager method
    public static void CheckReservations()
    {
        Console.Clear();
        //The weird spacing is there to make it show up nicer when called
        Console.WriteLine("Tafel id |  Datum    | Tijd        | Email");
        
        foreach (var item in AccountsAccess.LoadAllReservations().OrderBy(d => d.Date))
        {
            var Date = item.Date.ToString("dd-MM-yy");
            string id = Convert.ToString(item.Id);
            string time = $"{item.StartTime.Hours}:00-{item.LeaveTime.Hours}:00";
            Console.WriteLine(String.Format("{0,-8} |  {1,-6} | {2,5} | {3,5}", id, Date, time, item.EmailAddress));
        }
        Console.WriteLine("druk toets om terug te gaan");
        Console.ReadKey(true);
    }

    //Manager only methods
    public static void AddEmployee()
    {
        employeeEmail = null!;
        employeePassword = null!;
        while (true)
        {
            string[] options =
            {
                $"Vul hier de e-mail in" + (employeeEmail == null ? "" : $": {employeeEmail}"),
                "Vul hier het wachtwoord in" + $"{(employeePassword == null ? "\n" : $": {HidePass(employeePassword)}\n")}",
                "Maak account", "Afsluiten"
            };
            var selectedIndex = myMenu.RunMenu(options, "Medewerker toevoegen");
            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    Console.CursorVisible = true;
                    Console.Write("\n vul hier de e-mail in: ");
                    employeeEmail = Console.ReadLine()!;
                    if (!employeeEmail.Contains("@") || employeeEmail.Length < 3)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOnjuiste email.\nEmail moet minimaal een @ hebben en 3 tekens lang zijn.");
                        Console.ResetColor();
                        employeeEmail = null!;
                        Thread.Sleep(3000);
                    }

                    break;
                case 1:
                    Console.Clear();
                    Console.CursorVisible = true;
                    Console.Write("\n Vul hier het wachtwoord in: ");
                    employeePassword = WritePassword()!;
                    Console.Clear();
                    Console.Write("\n Vul het wachtwoord opnieuw in voor bevestiging: ");
                    var verifyUserPassword = WritePassword()!;
                    if (employeePassword == verifyUserPassword)
                        break;
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        "\nHet bevestigings wachtwoord is anders dan het eerste wachtwoord, probeer opnieuw.");
                    Console.ResetColor();
                    Thread.Sleep(2000);
                    employeePassword = null!;
                    break;
                }
                case 2:
                    if (employeeEmail == null || employeePassword == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nVul de gegevens in om een account aan te maken.");
                        Thread.Sleep(1500);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Clear();
                        var emailExists = _logicMenu.GetByEmail(employeeEmail);
                        if (emailExists != null!)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nEr bestaat al een account met deze e-mail.");
                            Thread.Sleep(2000);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.CursorVisible = true;
                            Console.Write("Vul hier de medewerkers volledige naam in: ");
                            var fullName = Console.ReadLine()!;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Clear();
                            Console.WriteLine(
                                $"\nVolledige naam: {fullName}\nE-mail: {employeeEmail}\nWachtwoord: " +
                                $"{HidePass(employeePassword)}\nWeet je zeker dat je een account wil aanmaken met deze gegevens? (j/n)");
                            Console.ResetColor();
                            var answer = Console.ReadLine()!;
                            switch (answer)
                            {

                                case "j":
                                case "J":
                                case "ja":
                                case "Ja":
                                {
                                    Console.Clear();
                                    var employeeacc = AccountsAccess.AddAccount(employeeEmail, employeePassword, fullName, true, false);
                                    Console.WriteLine("Medewerker toegevoegd");
                                    Thread.Sleep(3000);
                                }
                                    break;
                            }
                        }
                    }

                    break;
                case 3:
                    MainMenu.Start();
                    break;
                
            }
        }
    }

    public static void AddSpecialEvent()
    {

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
}

