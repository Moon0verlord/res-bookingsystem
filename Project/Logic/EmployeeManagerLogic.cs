using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;

public class EmployeeManagerLogic : IMenuLogic
{
    private static AccountsLogic _logicMenu = new();
    private static MenuLogic _myMenu = new();
    private static string? _employeeEmail;
    private static string _employeePassword ="";
    //Employee and manager method
    public static void CheckReservations()
    {
        
        Console.Clear();
        Console.WriteLine("Overzicht van alle reserveringen\n-------------------------------");
        Console.WriteLine("{0,-8} | {1,-15} | {2, -10} | {3,-15} | {4,-35} | {5,-12} | {6,-10}", 
            "Tafel ID", "Reserverings ID", "Datum", "Tijd", "Email", "Aantal pers.", "Gekozen gang");
        Console.ForegroundColor = ConsoleColor.White;
        foreach (var item in AccountsAccess.LoadAllReservations().OrderBy(d => d.Date))
        {
            if (item.Date > DateTime.Today)
            {
                var date = item.Date.ToString("dd-MM-yy");
                string id = Convert.ToString(item.Id);
                string time = $"{item.StartTime:hh}:{item.StartTime:mm} - {item.LeaveTime:hh}:{item.LeaveTime:mm}";
                Console.WriteLine("{0,-8} | {1,-15} | {2, -10} | {3,-15} | {4,-35} | {5,-12} | {6,-10}", id, item.ResId, date, time, item.EmailAddress, item.GroupSize.ToString(), item.Course.ToString());

            }
        }
        Console.ResetColor();
        Console.WriteLine("\nDruk op een toets om terug te gaan.");
        Console.ReadKey(true);
    }

    //Manager only methods
    public static void AddEmployee()
    {
        _employeeEmail = null!;
        _employeePassword = null!;
        while (true)
        {
            string[] options =
            {
                $"Vul hier de e-mail in" + (_employeeEmail == null ? "" : $": {_employeeEmail}"),
                "Vul hier het wachtwoord in" + $"{(_employeePassword == null ? "\n" : $": {HidePass(_employeePassword)}\n")}",
                "Maak account", "Afsluiten"
            };
            var selectedIndex = _myMenu.RunMenu(options, "Medewerker toevoegen");
            switch (selectedIndex)
            {
                // add email of employee
                case 0:
                    Console.Clear();
                    Console.CursorVisible = true;
                    Console.Write("\n vul hier de e-mail in: ");
                    _employeeEmail = Console.ReadLine()!;
                    if (!_employeeEmail.Contains("@") || _employeeEmail.Length < 3)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOnjuiste email.\nEmail moet minimaal een @ hebben en 3 tekens lang zijn.");
                        Console.ResetColor();
                        _employeeEmail = null!;
                        Thread.Sleep(3000);
                    }

                    break;
                // add password of employee
                case 1:
                    Console.Clear();
                    Console.CursorVisible = true;
                    Console.Write("\n Vul hier het wachtwoord in: ");
                    _employeePassword = WritePassword()!;
                    Console.Clear();
                    Console.Write("\n Vul het wachtwoord opnieuw in voor bevestiging: ");
                    var verifyUserPassword = WritePassword()!;
                    if (_employeePassword == verifyUserPassword)
                        break;
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "\nHet bevestigings wachtwoord is anders dan het eerste wachtwoord, probeer opnieuw.");
                        Console.ResetColor();
                        Thread.Sleep(2000);
                        _employeePassword = null!;
                        break;
                    }
                // create account
                case 2:
                    if (_employeeEmail == null || _employeePassword == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nVul de gegevens in om een account aan te maken.");
                        Thread.Sleep(1500);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Clear();
                        var emailExists = _logicMenu.GetByEmail(_employeeEmail);
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
                                $"\nVolledige naam: {fullName}\nE-mail: {_employeeEmail}\nWachtwoord: " +
                                $"{HidePass(_employeePassword)}\nWeet je zeker dat je een account wil aanmaken met deze gegevens? (j/n)");
                            Console.ResetColor();
                            var answer = Console.ReadLine()!;
                            switch (AnswerLogic.CheckInput(answer))
                            {
                                case 1:
                                    {
                                        Console.Clear();
                                        AccountsAccess.AddAccount(_employeeEmail, _employeePassword, fullName, true, false);
                                        Console.WriteLine("Medewerker toegevoegd");
                                        Thread.Sleep(3000);
                                        MainMenu.Start();
                                    }
                                    break;
                                case 0:
                                    Console.WriteLine("Medewerker niet toegevoegd");
                                    Thread.Sleep(3000);
                                    break;
                                case -1:
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
    public static void RemoveEmployee()
    {
       
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Accounts van actieve medewerks:");
            Console.ResetColor();
            // Show all employees
            foreach (var item in AccountsAccess.LoadAll().Where(d => d.IsEmployee && !d.IsManager))
            {
                Console.WriteLine(item.EmailAddress);
            }
            Console.WriteLine("Vul hier de email in van de medewerker die je wilt verwijderen (druk op q om terug te gaan)");
            string email = Console.ReadLine()!;
            if (email == "q")
            {
                MainMenu.Start();
                break;
            } 
            if (email!=null!&&AccountsAccess.LoadAll().Any(d => d.EmailAddress == email))
            {
                Console.WriteLine("Weet je zeker dat je deze medewerker wilt verwijderen? (j/n)");
                string answer = Console.ReadLine()!.ToLower();
                switch (AnswerLogic.CheckInput(answer))
                {
                    case 1:
                        AccountsAccess.RemoveAccount(email);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Medewerker verwijderd");
                        Thread.Sleep(2000);
                        Console.ResetColor();
                        UserLogin.DiscardKeys();
                        MainMenu.Start();
                        break;
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Medewerker niet verwijderd");
                        Thread.Sleep(2000);
                        Console.ResetColor();
                        UserLogin.DiscardKeys();
                        break;
                    case -1:
                        break;
                }
            }
            else
            {
                Console.WriteLine("Deze medewerker bestaat niet");
                Thread.Sleep(2000);
                UserLogin.DiscardKeys();
              
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
            Console.WriteLine("Reserverings id |  Datum    | Tijd        | Email");

            // show all reservations
            foreach (var item in AccountsAccess.LoadAllReservations().OrderBy(d => d.Date))
            {
                var date = item.Date.ToString("dd-MM-yy");
                string time = $"{item.StartTime.Hours}:00-{item.LeaveTime.Hours}:00";
                Console.WriteLine(String.Format("{0,-15} |  {1,-6} | {2,5} | {3,5}", item.ResId, date, time, item.EmailAddress));
            }
            Console.WriteLine("Vul hier het id in van de reservering die je wilt aanpassen (druk op enter om terug te gaan)");
            id = Console.ReadLine()!.ToUpper();
            string[] resIds = (from res in AccountsAccess.LoadAllReservations() select res.ResId).ToArray();
            if (id == "")
            {
                MainMenu.Start();
            }
            if (!resIds.Contains(id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Dit ID bestaat niet, vul het opnieuw in.");
                Thread.Sleep(500);
                Console.ResetColor();
                UserLogin.DiscardKeys();
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
                $"Vul hier de nieuwe datum in" + (reservation.Date == default ? "" : $": {reservation.Date.ToString("dd-MM-yy")}"),
                "Vul hier het nieuwe tijdsslot in" + (reservation.StartTime == default ? "" : $": {reservation.StartTime} - {reservation.LeaveTime}"),
                "Vul hier de nieuwe email in" + (reservation.EmailAddress == null ? "" : $": {reservation.EmailAddress}"),
                "Terug en opslaan",
                "Terug zonder opslaan"
            };
            var selectedIndex = _myMenu.RunMenu(options, "Vul hier de gegevens in");
            switch (selectedIndex)
            {
                // set new date
                case 0:
                    Console.Clear();
                    Console.WriteLine("Vul hier de nieuwe datum in (dd-mm-jjjj)");
                    string dateInput = Console.ReadLine()!;
                    if (IsValidDateFormat(dateInput))
                    {
                        DateTime date = Convert.ToDateTime(dateInput);
                        reservation.Date = date;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Dit is geen geldige datum");
                        Thread.Sleep(2000);
                        Console.ResetColor();
                    }
                    break;
                // set new time
                case 1:
                    Console.Clear();
                    Console.WriteLine("Vul hier het nieuwe tijdsslot in");
                    if (reservation.Course == 2){
                        while (true)
                        {
                            string[] time =
                            {
                                "16:00-17:30",
                                "17:30-19:00",
                                "19:00-20:30",
                                "Terug"
                            };
                            var sltdIndex = _myMenu.RunMenu(time, "selecteer een tijdsslot");
                            switch (sltdIndex)
                            {
                                case 0:
                                    reservation.StartTime = new TimeSpan(16, 0, 0);
                                    reservation.LeaveTime = new TimeSpan(17, 30, 0);
                                    break;
                                case 1:
                                    reservation.StartTime = new TimeSpan(17, 30, 0);
                                    reservation.LeaveTime = new TimeSpan(19, 0, 0);
                                    break;
                                case 2:
                                    reservation.StartTime = new TimeSpan(19, 0, 0);
                                    reservation.LeaveTime = new TimeSpan(20, 30, 0);
                                    break;
                                case 3:
                                    break;
                            }
                            break;
                        }
                    }
                    else if(reservation.Course == 3){
                        while (true)
                        {
                            string[] time =
                            {
                                "16:00-18:00",
                                "18:00-20:00",
                                "20:00-22:00",
                                "Terug"
                            };
                            var sltdIndex = _myMenu.RunMenu(time, "selecteer een tijdsslot");
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
                                    reservation.StartTime = new TimeSpan(20, 0, 0);
                                    reservation.LeaveTime = new TimeSpan(22, 0, 0);
                                    break;
                                case 3:
                                    break;
                            }
                            break;
                        }
                    }
                    else{
                        while (true)
                        {
                            string[] time =
                            {
                                "16:00-18:30",
                                "18:30-21:00",
                                "21:00-23:30",
                                "Terug"
                            };
                            var sltdIndex = _myMenu.RunMenu(time, "selecteer een tijdsslot");
                            switch (sltdIndex)
                            {
                                case 0:
                                    reservation.StartTime = new TimeSpan(16, 0, 0);
                                    reservation.LeaveTime = new TimeSpan(18, 30, 0);
                                    break;
                                case 1:
                                    reservation.StartTime = new TimeSpan(18, 30, 0);
                                    reservation.LeaveTime = new TimeSpan(21, 0, 0);
                                    break;
                                case 2:
                                    reservation.StartTime = new TimeSpan(21, 0, 0);
                                    reservation.LeaveTime = new TimeSpan(23, 30, 0);
                                    break;
                                case 3:
                                    break;
                            }
                            break;
                        }
                    }
                    break;
                // set new email
                case 2:
                    Console.Clear();
                    Console.WriteLine("Vul hier de nieuwe email in");
                    string? email = Console.ReadLine()!;
                    switch (EmailLogic.IsValidEmail(email))
                    {
                        case true:
                            reservation.EmailAddress = email;
                            break;
                        case false:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Dit is geen geldig email adres");
                            Thread.Sleep(2000);
                            Console.ResetColor();
                            UserLogin.DiscardKeys();
                            break;
                    }
                    break;
                // save changes and send email if possible else send cancellation email
                case 3:
                    var user = AccountsAccess.LoadAll().Find(account => reservation.EmailAddress == account.EmailAddress)!;
                    foreach (var item in AccountsAccess.LoadAllReservations())
                    {
                        if (item.Date == reservation.Date && item.StartTime == reservation.StartTime && item.LeaveTime == reservation.LeaveTime && item.Id == reservation.Id && item.EmailAddress == reservation.EmailAddress)
                        {
                            Console.WriteLine("Er is al een reservering op deze datum en tijd");
                            Console.WriteLine("Wilt u de reservering annuleren? (j/n)");
                            string answer = Console.ReadLine()!;
                            switch (AnswerLogic.CheckInput(answer))
                            {
                                case 1:
                                    if (user != null)
                                        EmailLogic.SendCancellationMail(item.EmailAddress, user.FullName);
                                    else
                                        EmailLogic.SendCancellationMail(item.EmailAddress, "Gebruiker");
                                    AccountsAccess.RemoveReservation(item);
                                    Thread.Sleep(2000);
                                    MainMenu.Start();
                                    break;
                                case 0:
                                    MainMenu.Start();
                                    break;
                            }
                        }
                    }
                    AccountsAccess.ChangeReservationJson(reservation);
                    EmailLogic.SendEmail(reservation.EmailAddress!, reservation.Date, reservation.ResId, reservation.StartTime, reservation.LeaveTime);
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

    // hides password from user for security
    public static string HidePass(string pass)
    {
        string hiddenPass = "";
        for (int i = 0; i < pass.Length; i++)
        {
            hiddenPass += "*";
        }

        return hiddenPass;
    }

    // writes password in console as *
    public static string WritePassword()
    {
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
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                password += keyInfo.KeyChar;
                Console.Write("*");
            }
        } while (currKey != ConsoleKey.Enter);
        return password;
    }
    
    
    // checks if date matches the dd-mm-yyyy format
    public static bool IsValidDateFormat(string input)
    {
        var pattern = @"^(0[1-9]|1[0-9]|2[0-9]|3[01])-(0[1-9]|1[0-2])-\d{4}$";
        return Regex.IsMatch(input,pattern);
    }
}

