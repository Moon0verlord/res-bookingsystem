using Microsoft.VisualBasic.CompilerServices;

public class EmployeeManagerLogic : IMenuLogic
{
    private static MenuLogic _myMenu = new();
    private static AccountsLogic _logicMenu = new();
    static private MenuLogic myMenu = new();
    private static string employeeEmail;
    private static string employeePassword;
    //Employee and manager method
    public static void CheckReservations()
    {
        Console.Clear();
        Console.WriteLine("Overzicht van alle reservaties\n-------------------------------");
        Console.WriteLine(String.Format("{0,-8} | {1,-15} | {2, -10} | {3,-15} | {4,-13} | {5,-13} | {6,-10}", 
            "Tafel ID", "Reservatie ID", "Datum", "Tijd", "Email", "Aantal pers.", "Gekozen gang"));
        Console.ForegroundColor = ConsoleColor.White;
        foreach (var item in AccountsAccess.LoadAllReservations().OrderBy(d => d.Date))
        {
            if (item.Date > DateTime.Today)
            {
                var Date = item.Date.ToString("dd-MM-yy");
                string id = Convert.ToString(item.Id);
                string time = $"{item.StartTime:hh}:{item.StartTime:mm} - {item.LeaveTime:hh}:{item.LeaveTime:mm}";
                Console.WriteLine(String.Format("{0,-8} | {1,-15} | {2, -10} | {3,-15} | {4,-13} | {5,-13} | {6,-10}", 
                    id, item.Res_ID, Date, time, item.EmailAddress, item.GroupSize.ToString(), item.Course.ToString()));

            }
        }
        Console.ResetColor();
        Console.WriteLine("\nDruk op een toets om terug te gaan.");
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
                // add email of employee
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
                // add password of employee
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
                // create account
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
                                        var employeeAcc = AccountsAccess.AddAccount(employeeEmail, employeePassword, fullName, true, false);
                                        Console.WriteLine("Medewerker toegevoegd");
                                        Thread.Sleep(3000);
                                    }
                                    break;
                            }
                        }
                    }

                    break;
                // exit
                case 3:
                    MainMenu.Start();
                    break;

            }
        }
    }

    // Remove an employee
    public static void RemoveEmployee(){
        Console.Clear();
        Console.WriteLine("Email");
        // Show all employees
        foreach (var item in AccountsAccess.LoadAll().Where(d => d.IsEmployee == true && d.IsManager == false))
        {
            Console.WriteLine(item.EmailAddress);
        }
        Console.WriteLine("Vul hier de email in van de medewerker die je wilt verwijderen (druk op enter om terug te gaan)");
        while (true)
        {
            string email = Console.ReadLine();
            if (email!=null&&AccountsAccess.LoadAll().Any(d => d.EmailAddress == email))
            {
                Console.WriteLine("Weet je zeker dat je deze medewerker wilt verwijderen? (j/n)");
                string answer = Console.ReadLine()!.ToLower();
                switch (answer)
                {
                    case "j":case"J":case"Ja":case"ja:":
                        AccountsAccess.RemoveAccount(email);
                        Console.WriteLine("Medewerker verwijderd");
                        Thread.Sleep(3000);
                        break;
                    case "n":case"N":case"nee":case"Nee":
                        Console.WriteLine("Medewerker niet verwijderd");
                        break;
                    default:
                        Console.WriteLine("Ongeldige invoer");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Deze medewerker bestaat niet");
                Thread.Sleep(2000);
                break;
            }
        }
    }

    // change a reservation of a customer
    public static void ChangeReservation()
    {
        string id;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Reservatie id |  Datum    | Tijd        | Email");

            // show all reservations
            foreach (var item in AccountsAccess.LoadAllReservations().OrderBy(d => d.Date))
            {
                var Date = item.Date.ToString("dd-MM-yy");
                string time = $"{item.StartTime.Hours}:00-{item.LeaveTime.Hours}:00";
                Console.WriteLine(String.Format("{0,-8} |  {1,-6} | {2,5} | {3,5}", item.Res_ID, Date, time, item.EmailAddress));
            }
            Console.WriteLine("Vul hier het id in van de reservering die je wilt aanpassen (druk op enter om terug te gaan)");
            id = Console.ReadLine().ToUpper();
            string[] res_ids = (from res in AccountsAccess.LoadAllReservations() select res.Res_ID).ToArray();
            if (id == "")
            {
                break;
            }
            if (!res_ids.Contains(id))
            {
                Console.WriteLine("Dit ID bestaat niet, vul het opnieuw in.");
                Thread.Sleep(500);
                Console.Clear();
                continue;
            }
            break;
        }
        ReservationModel reservation = ReservationsLogic.GetReservationById(id);
        while (true)
        {
            string[] options =
            {
                $"Vul hier de nieuwe datum in" + (reservation.Date == null ? "" : $": {reservation.Date.ToString("dd-MM-yy")}"),
                "Vul hier het nieuwe tijdsslot in" + (reservation.StartTime == null ? "" : $": {reservation.StartTime} - {reservation.LeaveTime}"),
                "Vul hier de nieuwe email in" + (reservation.EmailAddress == null ? "" : $": {reservation.EmailAddress}"),
                "Terug en opslaan",
                "Terug zonder opslaan"
            };
            var selectedIndex = myMenu.RunMenu(options, "Vul hier de gegevens in");
            switch (selectedIndex)
            {
                // set new date
                case 0:
                    Console.Clear();
                    Console.WriteLine("Vul hier de nieuwe datum in (dd-mm-jjjj)");
                    DateTime date = Convert.ToDateTime(Console.ReadLine());
                    reservation.Date = date;
                    break;
                // set new time
                case 1:
                    Console.Clear();
                    Console.WriteLine("Vul hier het nieuwe tijdsslot in");
                    while (true)
                    {
                        string[] time =
                        {
                            "16:00-18:00",
                            "18:00-20:00",
                            "22:00-00:00",
                            "Terug"
                        };
                        var sltdIndex = myMenu.RunMenu(time, "selecteer een tijdsslot");
                        switch (sltdIndex)
                        {
                            case 0:
                                reservation.StartTime = new TimeSpan(16, 0, 0);
                                reservation.LeaveTime = new TimeSpan(18, 0, 0);
                                break;
                            case 1:
                                reservation.StartTime = new TimeSpan(18, 0, 0);
                                reservation.LeaveTime = new TimeSpan(20, 0, 0);
                                break;
                            case 2:
                                reservation.StartTime = new TimeSpan(22, 0, 0);
                                reservation.LeaveTime = new TimeSpan(00, 0, 0);
                                break;
                            case 3:
                                break;
                        }
                        break;
                    }
                    break;
                // set new email
                case 2:
                    Console.Clear();
                    Console.WriteLine("Vul hier de nieuwe email in");
                    string email = Console.ReadLine();
                    if (EmailLogic.IsValidEmail(email))
                    {
                        reservation.EmailAddress = email;
                    }
                    else
                    {
                        Console.WriteLine("Dit is geen geldig email adres");
                        Thread.Sleep(2000);
                    }
                    break;
                // save changes and send email if possible else send cancellation email
                case 3:
                    var User = AccountsAccess.LoadAll().Find(account => reservation.EmailAddress == account.EmailAddress)!;
                    foreach (var item in AccountsAccess.LoadAllReservations())
                    {
                        if (item.Date == reservation.Date && item.StartTime == reservation.StartTime && item.LeaveTime == reservation.LeaveTime && item.Id == reservation.Id)
                        {
                            Console.WriteLine("Er is al een reservering op deze datum en tijd");
                            if (User != null)
                                EmailLogic.SendCancellationMail(item.EmailAddress, User.FullName);
                            else
                                EmailLogic.SendCancellationMail(item.EmailAddress, "Gebruiker");
                            AccountsAccess.RemoveReservation(item);
                            Thread.Sleep(2000);
                            MainMenu.Start();
                        }
                    }
                    AccountsAccess.ChangeReservationJson(reservation);
                    EmailLogic.SendEmail(reservation.EmailAddress, User.FullName, reservation.Id, reservation.Date);
                    Console.WriteLine("Reservering aangepast");
                    Thread.Sleep(2000);
                    MainMenu.Start();
                    break;
                // go back without saving
                case 4:
                    MainMenu.Start();
                    break;
            }
        }
    }

    // hide password for security
    public static string HidePass(string pass)
    {
        string hiddenPass = "";
        for (int i = 0; i < pass.Length; i++)
        {
            hiddenPass += "*";
        }

        return hiddenPass;
    }

    // write password in console
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

