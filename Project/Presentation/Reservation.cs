using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json.Linq;

static class Reservation
{
    private static AccountModel? _acc;
    private static MenuLogic _my1DMenu = new();
    private static _2DMenuLogic _my2DMenu = new();
    private static readonly ReservationsLogic Reservations = new();
    private static ReservationTableLogic _tableLogic = new();
    private static string? _userEmail;
    private static string? _userName;
    private static int _amountOfPeople;
    private static int _underageMembers;
    private static DateTime _chosenDate;
    private static (TimeSpan, TimeSpan) _chosenTimeslot;
    private static string? _chosenTable;
    private static int _chosenCourse;
    private static bool _chosenWine;
    private static int _howManyWine;
    private static int _stepCounter;

    public static void ResStart(AccountModel? acc = null)
    {
        // resetting all static fields for a fresh reservation start
        _acc = acc;
        FieldReset();
        // main menu functionality
        // step counter handles the current chosen option, so users can go back and forth
        bool loop = true;
        while (loop)
        {
            switch (_stepCounter)
            {
                case 1:
                    if (EnterCredentials())
                        _stepCounter++;
                    else
                        loop = false;
                    break;
                case 2:
                    if (ResMenu())
                        _stepCounter++;
                    else
                        _stepCounter--;
                    break;      
                case 3:
                    if (HasUnderageMembers())
                        _stepCounter++;
                    else 
                        _stepCounter --;
                    break;
                // ChooseWine returns an int, because there are 3 options. A bool would not work.
                // 0: go back, 1: yes to wine, 2: no to wine
                case 4:
                    switch (ChooseWine())
                    {
                        case 0:
                            _stepCounter--;
                            break;
                        case 1:
                            _stepCounter++;
                            break;
                        case 2:
                            _stepCounter += 2; 
                            break;
                    }
                    break;
                case 5:
                    if (ChooseWineAmount())
                        _stepCounter++;
                    else
                        _stepCounter--;
                    break;
          
                case 6:
                    if (ChooseDate())
                        _stepCounter++;
                    else
                        _stepCounter -= 2;
                    break;
                case 7:
                    if (ChooseTimeslot(_chosenDate))
                        _stepCounter++;
                    else
                        _stepCounter--;
                    break;
                case 8:
                    if (ChooseTable(_chosenDate, _chosenTimeslot))
                        _stepCounter++;
                    else
                        _stepCounter--;
                    break;
                case 9:
                    if (FinishReservation())
                        loop = false;
                    else
                        _stepCounter--;
                    break;
            }   
        }
    }

    // reset all fields so that the static class doesn't remember between calls
    private static void FieldReset()
    {
        _userEmail = null;
        _userName = null;
        _amountOfPeople = 0;
        _underageMembers = 0;
        _chosenDate = default;
        _chosenTimeslot = (default, default);
        _chosenTable = "";
        _chosenCourse = 0;
        _chosenWine = false;
        _howManyWine = 0;
        _stepCounter = 1;
        Console.Clear();
    }

    // called at the end of the main menu switch case, finishes up with all the filled n fields and asks user for confirmation.
    public static bool FinishReservation()
    {
        while (true)
        {
            Console.Clear();
            InfoBoxes.WriteBoxStepCounter(Console.CursorTop, Console.CursorLeft, _stepCounter);
            InfoBoxes.WriteBill(Console.CursorTop, Console.CursorLeft, _amountOfPeople, _underageMembers, _chosenCourse, _chosenWine, _howManyWine);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(2, 3);
            Console.WriteLine(
                $"\n   Email: {_userEmail}\n   Naam: {_userName}\n   Groepgrootte: {_amountOfPeople}\n" +
                $"   Reservatie tafel nummer: {_chosenTable}\n   Datum: {_chosenDate.Date.ToString("dd-MM-yyyy")}" +
                $"\n   Tijd: ({_chosenTimeslot.Item1} - {_chosenTimeslot.Item2})\n");
            Console.Write("\n \n        Weet u zeker dat u met deze gegevens wilt reserveren? (j/n): ");
            Console.ResetColor();
            Console.CursorVisible = true;
            string answer = Console.ReadLine()!;
            switch (AnswerLogic.CheckInput(answer))
            {
                case 1:
                    string Res_ID = Reservations.CreateID();
                    Reservations.CreateReservation(_userEmail, _chosenDate, _chosenTable, _amountOfPeople,
                        _chosenTimeslot.Item1, _chosenTimeslot.Item2, Res_ID, _chosenCourse);
                    Console.Clear();
                    Console.WriteLine("\nReservatie is gemaakt.");
                    Thread.Sleep(1500);
                    UserLogin.DiscardKeys();
                    return true;
                case 0:
                    return false;
                case -1:
                    break;
            }
        }
    }

    // ask users to fill in their email en full name.
    // If the user has an account, this will be skipped inside this method.
    public static bool EnterCredentials()
    {
        if (_acc == null!)
        {
            string name = null!;
            string? email = null!;
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                string prompt = $"\n\n\nVul hier uw e-mail in om een reservatie te maken.";
                string[] options =
                {
                    $"Vul hier uw e-mail in" + (email == null ? "" : $": {email}"),
                    "Vul hier uw volledige naam in" + (name == null ? "\n" : $": {name}\n"), "Doorgaan", "Ga terug"
                };
                int selectedIndex = _my1DMenu.RunResMenu(options, prompt, _stepCounter);
                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.Write("\n Vul hier uw e-mail in: ");
                        email = Console.ReadLine()!;
                        if (!EmailLogic.IsValidEmail(email))
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(
                                "\nOnjuiste email.\nEmail moet minimaal een @ hebben en 3 tekens lang zijn.");
                            Console.ResetColor();
                            email = null;
                            Thread.Sleep(2000);
                            UserLogin.DiscardKeys();
                        }

                        break;
                    case 1:
                        Console.Clear();
                        Console.CursorVisible = true;
                        Console.Write("\n Vul hier uw volledige naam in: ");
                        name = Console.ReadLine()!;
                        if (!name.Contains(" "))
                        {
                            name = null;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(
                                "\nOnjuiste naam. Een volledige naam moet minimaal een spatie bevatten.");
                            Console.ResetColor();
                            Thread.Sleep(2000);
                            UserLogin.DiscardKeys();
                        }
                        break;
                    case 2:
                        if (email != null && name != null)
                        {
                            _userEmail = email;
                            _userName = name;
                            return true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\nVul eerst al uw gegevens in.");
                            Thread.Sleep(1800);
                            Console.ResetColor();
                        }
                        break;
                    case 3:
                        return false;
                }
            }
        }
        _userEmail = _acc!.EmailAddress;
        _userName = _acc.FullName;
        return true;
    }

    // asks user to enter group size and course.
    public static bool ResMenu()
    {
        int gr_size = 0;
        int course = 0;
        bool loop = true;
        Console.Clear();
        while (loop)
        {
            Console.Clear();
            Console.ResetColor();
            string prompt = $"\n\n\nVul hier een paar gegevens in die nodig zijn voor uw reservering";
            string[] options =
            {
                $"Vul hier groepsgrootte in" + (gr_size == 0 ? "" : $": {gr_size}"),
                "Kies hier uw gewenste gang" + (course == 0 ? "\n" : $": {course} gangen\n"),
                "Doorgaan", "Ga terug"
            };
            int selectedIndex = _my1DMenu.RunResMenu(options, prompt, _stepCounter);
            switch (selectedIndex)
            {
                case 0:
                    gr_size = ChooseGroupSize();
                    break;
                case 1:
                    course = ChooseCourse();
                    break;
                case 2:
                    if (gr_size == 0 || course == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "\nVul eerst al de informatie in.");
                        Console.ResetColor();
                        Thread.Sleep(2000);
                        UserLogin.DiscardKeys();
                    }
                    else
                    {
                        _amountOfPeople = gr_size;
                        _chosenCourse = course;
                        return true;
                    }
                    break;
                case 3:
                    return false;
            }
        }

        return false;
    }

    // return a 0 if going back
    // return a 1 if yes to wine
    // return a 2 if no to wine
    public static int ChooseWine()
    {
        while (true)
        {
            string prompt = "\n\n\nKies hier of u een wijnarrangement wilt.\n" +
                            "Een wijnarrangement geeft een samengestelde wijnselectie bij iedere course.\n" +
                            "Een wijnarrangement kost €10 extra voor ieder persoon in uw groep die het wilt.\n" +
                            "Als u 'Ja' invult, vragen wij dalijk hoeveel mensen een wijnarrangement willen.";
            string[] options = new[] { "Ja, ik wil een wijnarrangement", "Nee, ik wil geen wijnarrangement\n", "Ga terug"};
            int chosenOption = _my1DMenu.RunResMenu(options, prompt, _stepCounter);
            switch (chosenOption)
            {
                case 0:
                    if (_amountOfPeople == _underageMembers)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                            "\nDeze groep bestaat uit alleen maar minderjarige groepsleden\n" +
                            "Daarom bent u verboden om een wijnarrangement te bestellen.");
                        Console.ResetColor();
                        Thread.Sleep(2000);
                        UserLogin.DiscardKeys();
                        continue;
                    }
                    _chosenWine = true;
                    return 1;
                case 1:
                    _chosenWine = false;
                    return 2;
                default:
                    return 0;
            }   
        }
    }
    
    // if user said yes to wine, user chooses amount of wine arrangements wanted here
    // calculates group size - amount of minors in the group, so you can't order 6 wine arrangements when you have 3 kids with you.
    public static bool ChooseWineAmount()
    {
        int wineamount = 0;
        int num;
        while (true)
        {
            Console.ResetColor();
            string prompt = "\n\n\nU heeft aangegeven dat u een wijnarrangement wilt.\n" +
                            "Hoeveel personen uit u groep willen een wijnarrangement?\n" +
                            "Let op: per persoon kost een wijnarrangement €10.";
            string[] options = new[] { "Vul hier in hoeveel personen een wijnarrangement willen" + (wineamount == 0 ? "\n" : $": {wineamount}\n"), 
                "Doorgaan", "Ga terug"};
            int chosenOption = _my1DMenu.RunResMenu(options, prompt, _stepCounter);
            switch (chosenOption)
            {
                case 0:
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.Write("Typ hier hoeveel mensen een wijnarrangement willen: ");
                    string answer = Console.ReadLine()!;
                    bool success = int.TryParse(answer, out num);
                    if (!success)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Voer enkel geldige nummers in, alstublieft.");
                        Thread.Sleep(2500);
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        if (num <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(
                                "Nummers kleiner of gelijk aan nul zijn niet toegestaan. Voer opnieuw in.");
                            Thread.Sleep(2500);
                            UserLogin.DiscardKeys();
                        }

                        else if (num > _amountOfPeople)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"U kunt niet meer mensen aanwijzen dan de grootte van uw groep ({_amountOfPeople}). Voer opnieuw in.");
                            Thread.Sleep(2500);
                            UserLogin.DiscardKeys();
                        }
                        else if (num > (_amountOfPeople - _underageMembers))
                        {
                            int amountAllowed = _amountOfPeople - _underageMembers;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"U heeft een groepsgrootte aangewezen van {_amountOfPeople} personen\n met {_underageMembers} minderjarige personen.\n" +
                                              $"Dit betekent dat u maximaal {amountAllowed} wijnarrangementen mag bestellen.\n" +
                                              $"Druk op een knop om verder te gaan als u dit bericht gelezen heeft.");
                            Console.ReadKey(true);
                        }
                        else
                            wineamount = num;
                    }
                    break;
                case 1:
                    if (wineamount == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\nVul eerst in hoeveel mensen een wijnarrangement willen.");
                        Thread.Sleep(2500);
                        UserLogin.DiscardKeys();
                    }
                    else
                    {
                        _howManyWine = wineamount;
                        return true;
                    }
                    break;
                case 2:
                    return false;
            }   
        }
    }
    
    // asks user if there are any minors in the group, so they can get a discount and aren't eligible for the wine course.
    public static bool HasUnderageMembers()
    {
        int num;
        bool loop = true;
        while (loop)
        {
            string prompt = "\n\n\nHeeft u minderjarige personen in uw groep?\n" +
                            "Als dit zo is, kies dan 'Ja', en vul in hoeveel personen minderjarig zijn\n" +
                            "Dit geeft u wat extra korting per minderjarig persoon. (10%)";
            string[] options = new[] { "Ja, ik heb minderjarige in mijn groep", "Nee, ik heb geen minderjarige in mijn groep\n", "Ga terug"};
            int chosenOption = _my1DMenu.RunResMenu(options, prompt, _stepCounter);
            switch (chosenOption)
            {
                case 0:
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.Write("Typ hier hoeveel mensen minderjarig zijn: ");
                    string answer = Console.ReadLine()!;
                    bool success = int.TryParse(answer, out num);
                    if (!success)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Voer enkel geldige nummers in, alstublieft.");
                        Thread.Sleep(2500);
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        if (num <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(
                                "Nummers kleiner of gelijk aan nul zijn niet toegestaan. Voer opnieuw in.");
                            Thread.Sleep(2500);
                            UserLogin.DiscardKeys();
                        }

                        else if (num > _amountOfPeople)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(
                                $"U kunt niet meer mensen aanwijzen dan de grootte van uw groep ({_amountOfPeople}). Voer opnieuw in.");
                            Thread.Sleep(2500);
                            UserLogin.DiscardKeys();
                        }
                        else
                        {
                            _underageMembers = num;
                            return true;
                        }
                    }
                    break;
                case 1:
                    _underageMembers = 0;
                    return true;
                default:
                    return false;
            }
        }

        return false;
    }

    // choose 2, 3, or 4 meal course
    public static int ChooseCourse()
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        JObject menu = AccountsAccess.LoadAllMenu();
        var pricing = menu["Prijzen"]!["Prijzen"];
        string[] options = new[] { $"2 Gangen ({pricing![0]})", $"3 Gangen ({pricing[1]})", $"4 Gangen ({pricing[2]})", "Ga terug" };
        int chosenIndex = _my1DMenu.RunResMenu(options, "\n\n\nKies een gang voor uw reservering:", _stepCounter);
        switch (chosenIndex)
        {
            case 0:
                return 2;
            case 1:
                return 3;
            case 2:
                return 4;
            default:
                return 0;
        }
    }
    
    // ask user how many people the group will consist of.
    public static int ChooseGroupSize()
    {
        Console.Clear();
        Console.CursorVisible = true;
        Console.Write("Typ hier met hoeveel mensen u van plan bent te komen: ");
        string amountofPeople = Console.ReadLine()!;
        bool success = int.TryParse(amountofPeople, out int number);
        if (!success)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Voer enkel geldige nummers in, alstublieft.");
            Thread.Sleep(2500);
        }
        else
        {
            if (number <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "Nummers kleiner of gelijk aan nul zijn niet toegestaan. Voer opnieuw in.");
                Thread.Sleep(2500);
                return 0;
            }
            else if (number > 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "U moet bellen voor groepsgroottes boven de zes personen, bekijk onze contactinformatie in het hoofdmenu.");
                Thread.Sleep(2500);
                return 0;
            }
            return number;
        }

        return 0;
    }

    // let the user choose a date for reservation based on current month
    public static bool ChooseDate()
    {
        Console.Clear();
        var thisMonth = Reservations.PopulateDates();
        Console.WriteLine(
            $"\n\n\nU kunt alleen een reservatie maken voor de huidige maand ({ReservationsLogic.CurMonth})\nKies een datum (of druk op 'q' om terug te gaan):");
        for (int i = 0; i < thisMonth.GetLength(1); i++)
        {
            Console.Write($"{thisMonth[0, i].Date.ToString("ddd", CultureInfo.GetCultureInfo("nl"))}\t");
        }
        DateTime userChoice = _my2DMenu.RunMenu(thisMonth, "", _stepCounter, false);
        if (userChoice == default)
            return false;
        _chosenDate = userChoice;
        return true;
    }

    // let user make decision on their timeslot based on chosen course (extra time for bigger courses)
    public static bool ChooseTimeslot(DateTime chosenDate)
    {
        bool todayeventcheck = false;
        JArray allEvents = AccountsAccess.ReadAllEvents();
        TimeSpan ts1 = default;
        TimeSpan ts2 = default;
        foreach (var eventItem in allEvents)
        {
            string date = Convert.ToString(eventItem["eventdate"])!;
            if (date == chosenDate.ToString("dd-MM-yyyy"))
            {
                todayeventcheck = true;
            }
        }

        if (todayeventcheck)
        {
            string[] optionsEvent = new[] { "16:00 - 19:00", "19:00 - 22:00", "Ga terug" };
            int selectedIndexEvent = _my1DMenu.RunMenu(optionsEvent, "\n\n\nVandaag is er een event kies uw tijdslot:");
            switch (selectedIndexEvent)
            {
                case 0:
                    ts1 = new TimeSpan(0, 16, 0, 0);
                    ts2 = new TimeSpan(0, 19, 0, 0);
                    _chosenTimeslot = (ts1, ts2);
                    return true;
                case 1:
                    ts1 = new TimeSpan(0, 19, 0, 0);
                    ts2 = new TimeSpan(0, 22, 0, 0);
                    _chosenTimeslot = (ts1, ts2);
                    return true;
                case 2:
                    return false;
            }
        }
        else
        {
            string timeslot1 = (_chosenCourse == 2 ? "16:00 - 17:30" :
                _chosenCourse == 3 ? "16:00 - 18:00" : "16:00 - 18:30");
            string timeslot2 = (_chosenCourse == 2 ? "17:30 - 19:00" :
                _chosenCourse == 3 ? "18:00 - 20:00" : "18:30 - 21:00");
            string timeslot3 = (_chosenCourse == 2 ? "19:00 - 20:30" :
                _chosenCourse == 3 ? "20:00 - 22:00" : "21:00 - 23:30");
            string[] entertime;
            string[] leavetime;
            string[] optionsmenu = new[] { timeslot1, timeslot2, timeslot3, "Ga terug" };
            int selectedIndex = _my1DMenu.RunResMenu(optionsmenu, "\n\n\nKies uw gewenste tijdslot:", _stepCounter);
            switch (selectedIndex)
            {
                case 0:
                    entertime = timeslot1.Split("-")[0].Split(":");
                    leavetime = timeslot1.Split("-")[1].Split(":");
                    ts1 = new TimeSpan(0, Convert.ToInt32(entertime[0].Trim()), Convert.ToInt32(entertime[1].Trim()),
                        0);
                    ts2 = new TimeSpan(0, Convert.ToInt32(leavetime[0].Trim()), Convert.ToInt32(leavetime[1].Trim()),
                        0);
                    _chosenTimeslot = (ts1, ts2);
                    return true;
                case 1:
                    entertime = timeslot2.Split("-")[0].Split(":");
                    leavetime = timeslot2.Split("-")[1].Split(":");
                    ts1 = new TimeSpan(0, Convert.ToInt32(entertime[0].Trim()), Convert.ToInt32(entertime[1].Trim()),
                        0);
                    ts2 = new TimeSpan(0, Convert.ToInt32(leavetime[0].Trim()), Convert.ToInt32(leavetime[1].Trim()),
                        0);
                    _chosenTimeslot = (ts1, ts2);
                    return true;
                case 2:
                    entertime = timeslot3.Split("-")[0].Split(":");
                    leavetime = timeslot3.Split("-")[1].Split(":");
                    ts1 = new TimeSpan(0, Convert.ToInt32(entertime[0].Trim()), Convert.ToInt32(entertime[1].Trim()),
                        0);
                    ts2 = new TimeSpan(0, Convert.ToInt32(leavetime[0].Trim()), Convert.ToInt32(leavetime[1].Trim()),
                        0);
                    _chosenTimeslot = (ts1, ts2);
                    return true;
                case 3:
                    return false;
            }
        }

        return false;
    }

    // let user choose the table they want
    public static bool ChooseTable(DateTime res_Date, (TimeSpan, TimeSpan) chosenTime)
    {
        var tablesOnly = Reservations.PopulateTables2D(res_Date, chosenTime);
        // another setwindowsize here to make sure the user didn't make the window too small when choosing other options
        // Picking a setwindowsize under 171 will make the console crash with a bounding error.
        try
        {
            Console.SetWindowSize(180, 35);
        }
        catch
        {
        }

        _tableLogic.TableStart(tablesOnly, _amountOfPeople, _stepCounter);
        ReservationModel selectedTable = _my2DMenu.RunTableMenu(tablesOnly,
            "  Kies uw tafel (of druk op 'q' om terug te gaan):", _amountOfPeople);
        if (selectedTable != default!)
        {
            _chosenTable = selectedTable.Id;
            return true;
        }
        return false;
    }

 
    public static void ViewRes(string email)
    {
        Console.Clear();
        bool checkIfRes = false;
        List<ReservationModel> allRes = AccountsAccess.LoadAllReservations();
        List<string> reservationsPerson = new();
        List<int> reservationPersonPositions = new();
        Console.WriteLine("Voer uw reservatie ID in: ");
        string? resid = Console.ReadLine();
        foreach (ReservationModel res in allRes)
        {
            if (email == res.EmailAddress && res.Res_ID == resid && res.Date >= DateTime.Now.Date)
            {
                reservationsPerson.Add(
                    $"U heeft een reservering onder de Email: {res.EmailAddress}. Voor tafel {res.Id} en De datum van de resevering is: {res.Date.ToString("dd-MM-yyyy")}. Tijdslot: {res.StartTime} - {res.LeaveTime}");
                reservationPersonPositions.Add(allRes.FindIndex(a => a == res));
                checkIfRes = true;
            }
        }

        if (checkIfRes == false)
        {
            Console.WriteLine("Er staan nog geen reserveringen open met dit email adres.");
            Thread.Sleep(2000);
        }
        else
        {
            reservationsPerson.Add("Ga terug");
            while (true)
            {
                var reservInput = _my1DMenu.RunMenu(reservationsPerson.ToArray(), "Reserveringen");
                switch (reservationsPerson[reservInput])
                {
                    case "Ga terug":
                        MainMenu.Start();
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wilt u uw reservering annuleren? (j/n)");
                        Console.ResetColor();
                        Console.CursorVisible = true;
                        var choice = Console.ReadLine();
                        switch (AnswerLogic.CheckInput(choice))
                        {
                            case 1:
                                Console.WriteLine("De reservatie is verwijderd");
                                allRes.RemoveAt(reservationPersonPositions[reservInput]);
                                reservationsPerson.RemoveAt(reservInput);
                                AccountsAccess.WriteAllReservations(allRes);
                                Thread.Sleep(5000);
                                UserLogin.DiscardKeys();
                                break;
                            case 0:
                                Console.WriteLine("De reservatie is niet verwijderd");
                                Thread.Sleep(2000);
                                UserLogin.DiscardKeys();
                                break;
                            case -1:
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Incorrecte input");
                                Thread.Sleep(2000);
                                Console.ResetColor();
                                UserLogin.DiscardKeys();
                                break;
                        }

                        break;
                }
            }
        }
    }

    public static void ViewResAccount()
    {
        bool inMenu = true;
        while (inMenu)
        {
            var allRes = AccountsAccess.LoadAllReservations().Where(x => x.Date.AddHours(x.StartTime.Hours) >= DateTime.Now).OrderBy(x => x.Date).ToArray();
            var menuViewable = allRes.Select(x => 
                    x.Date.ToString("dd-MM-yyyy") + $" ({x.StartTime:hh}:{x.StartTime:mm} - {x.LeaveTime:hh}:{x.LeaveTime:mm})").Append("Ga terug").ToArray();
            int chosenOption = _my1DMenu.RunMenu(menuViewable, "Overzicht van al uw reservaties.\nKlik op een datum om meer informatie te zien.\n");
            switch (menuViewable[chosenOption])
            {
                case "Ga terug":
                    inMenu = false;
                    break;
                default:
                    ViewRes2(allRes[chosenOption].Res_ID);
                    break;
            }   
        }
    }

    // todo : name is this for now to not be an overload for martijn's viewres. will change.
    public static void ViewRes2(string resid = null)
    {
        Console.Clear();
        List<ReservationModel> allRes = AccountsAccess.LoadAllReservations().Where(x => x.Date.AddHours(x.StartTime.Hours) >= DateTime.Now).ToList();
        Console.CursorVisible = true;
        if (resid == null)
        {
            Console.Write("Voer uw reservatie ID in: ");
            resid = Console.ReadLine()!.ToUpper();
            resid = resid!.Contains("RES-") ? resid : "RES-" + resid;   
        }
        ReservationModel? chosenRes = allRes.Find(x => x.Res_ID == resid);
        if (chosenRes == default)
        {
            Console.WriteLine($"Geen reservatie gevonden met het gegeven reservatie ID.");
            Thread.Sleep(1500);
            UserLogin.DiscardKeys();
        }
        else
        {
            bool inMenu = true;
            while (inMenu)
            {
                string resInfo =
                    $"Reservatie {resid}:\nEmail: {chosenRes.EmailAddress}\nDatum: {chosenRes.Date.Date:dd-MM-yyyy}\n" +
                    $"Tijd: {chosenRes.StartTime:hh}:{chosenRes.StartTime:mm} - {chosenRes.LeaveTime:hh}:{chosenRes.LeaveTime:mm}" +
                    $"\nTafel: {chosenRes.Id}\nGroepsgrootte: {chosenRes.GroupSize}\nGekozen gang: {chosenRes.Course}\n";
                int? chosenOption = _my1DMenu.RunMenu(new[] { "Reservatie annuleren", "Ga terug" }, resInfo);
                switch (chosenOption)
                {
                   case 0 :
                        if ((chosenRes.Date.Date - DateTime.Now.Date).Days <= 1)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Reservatie is morgen, en dan kunt u" +
                                              " niet annuleren via dit programma.\nVerwijs naar het informatie tab " +
                                              "binnen het hoofdmenu om te bellen voor annulering.\n\n\nDruk op een knop om terug te gaan.");
                            Console.ReadKey(true);
                            Console.ResetColor();
                        }
                        else
                        {

                            Console.Clear();
                            Console.CursorVisible = true;
                            Console.Write("Weet u zeker dat u deze reservatie wilt annuleren? (j/n): ");
                            string choice = Console.ReadLine()!.ToLower();
                            switch (AnswerLogic.CheckInput(choice))
                            {
                                case 1: 
                                    allRes.Remove(chosenRes);
                                    AccountsAccess.WriteAllReservations(allRes);
                                    Console.WriteLine("\nReservatie is verwijderd.");
                                    Thread.Sleep(1500);
                                    UserLogin.DiscardKeys();
                                    inMenu = false;
                                    break;
                                case 0 :
                                    Console.WriteLine("\nReservatie is niet verwijderd.");
                                    Thread.Sleep(1500);
                                    UserLogin.DiscardKeys();
                                    break;
                            }
                        }

                        break;
                    case 1:
                        inMenu = false;
                        break;
                }
            }
        }
    }
}
