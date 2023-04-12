public class EmployeeManagerLogic : IMenuLogic
{
    private static MenuLogic _myMenu = new MenuLogic();
    private static AccountsLogic _logicMenu = new AccountsLogic();
    static private MenuLogic myMenu = new();
    private static string employeeEmail;
    private static string employeePassword;
    //Employee and manager method
    public static void CheckReservations()
    {
        //ToDo 2D menu currently in development by rokimajo
        // when a date is picked in the menu return all reservations on that date
        //this is a place holder
        int Date = Convert.ToInt32(Console.ReadLine());
        foreach (var item in AccountsAccess.LoadAllReservations())
        {
            if(item.Date.Day == Date)
            {
                Console.WriteLine(item.Id+" "+item.Date+" "+item.EmailAddress);
            }
        }
        Console.WriteLine("druk toets om terug te gaan");
        Console.ReadKey(true);
    }

    //Manager only methods
    public static void AddEmployee()
    {
        employeeEmail = null;
        employeePassword = null;
        while (true)
        {
            string[] options =
            {
                $"Vul hier de e-mail in" + (employeeEmail == null ? "" : $": {employeeEmail}"),
                "Vul hier het wachtwoord in" + $"{(employeePassword == null ? "\n" : $": {employeePassword}\n")}",
                "Maak account", "Afsluiten"
            };
            var selectedIndex = myMenu.RunMenu(options, "Medwerker toevoegen");
            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    Console.Write("\n vul hier de e-mail in: ");
                    employeeEmail = Console.ReadLine()!;
                    if (!employeeEmail.Contains("@") || employeeEmail.Length < 3)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOnjuiste email.\nEmail moet minimaal een @ hebben en 3 tekens lang zijn.");
                        Console.ResetColor();
                        employeeEmail = null;
                        Thread.Sleep(3000);
                    }

                    break;
                case 1:
                    Console.Clear();
                    Console.Write("\n Vul hier het wachtwoord in: ");
                    employeePassword = Console.ReadLine()!;
                    Console.Clear();
                    Console.Write("\n Vul het wachtwoord opnieuw in voor bevestiging: ");
                    var verifyUserPassword = Console.ReadLine()!;
                    if (employeePassword == verifyUserPassword)
                        break;
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        "\nHet bevestigings wachtwoord is anders dan het eerste wachtwoord, probeer opnieuw.");
                    Console.ResetColor();
                    Thread.Sleep(2000);
                    employeePassword = null;
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
                        if (emailExists != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nEr bestaat al een account met deze e-mail.");
                            Thread.Sleep(2000);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write("Vul hier de medewerkers volledige naam in: ");
                            var fullName = Console.ReadLine()!;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Clear();
                            Console.WriteLine(
                                $"\nVolledige naam: {fullName}\nE-mail: {employeeEmail}\nWachtwoord: " +
                                $"{employeePassword}\nWeet je zeker dat je een account wil aanmaken met deze gegevens? (j/n)");
                            Console.ResetColor();
                            var answer = Console.ReadLine()!;
                            if (answer == "j" || answer == "J")
                            {
                                AccountsAccess.AddAccount(employeeEmail, employeePassword, fullName, true, false);
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

    public static void RestaurantLayout()
    {
        
    }

}

