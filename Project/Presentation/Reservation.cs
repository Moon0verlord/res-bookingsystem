using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using Newtonsoft.Json.Linq;

static class Reservation
{
    private static AccountModel _acc = null;
    private static MenuLogic _my1DMenu = new MenuLogic();
    private static _2DMenuLogic _my2DMenu = new _2DMenuLogic();
    private static readonly ReservationsLogic Reservations = new ReservationsLogic();
    private static AnswerLogic _answerLogic = new AnswerLogic();
    private static ReservationTableLogic _tableLogic = new ReservationTableLogic();
    private static string _userEmail;
    private static string _userName;
    private static int _amountOfPeople;
    private static int _underageMembers;
    private static DateTime _chosenDate;
    private static (TimeSpan, TimeSpan) _chosenTimeslot;
    private static string _chosenTable;
    private static int _chosenCourse;
    private static bool _chosenWine;
    private static int _stepCounter;

    public static void ResStart(AccountModel acc = null)
    {
        // resetting all static fields for a fresh reservation start
        _acc = acc;
        FieldReset();
        
    }

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
        _stepCounter = 1;
        Console.Clear();
    }

    public static void EnterCredentials()
    {
        if (_acc == null!)
        {
            string name = null!;
            string email = null!;
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
                            loop = false;
                            _userEmail = email;
                            _userName = name;
                            _stepCounter++;
                            ResMenu();
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
                        MainMenu.Start();
                        break;
                }
            }
        }
        else
        {
            _stepCounter++;
            _userEmail = _acc.EmailAddress;
            ResMenu();
        }
    }

    public static void ResMenu()
    {
        int gr_size = 0;
        int course = 0;
        bool wine = false;
        bool loop = true;
        Console.Clear();
        while (loop)
        {
            Console.Clear();
            string prompt = $"\n\n\nVul hier een paar gegevens in die nodig zijn voor uw reservering";
            string[] options =
            {
                $"Vul hier groepsgrootte in" + (gr_size == 0 ? "" : $": {gr_size}"),
                "Kies hier uw gewenste gang" + (course == 0 ? "" : $": {course} gangen"),
                "Kies hier of u een wijnarrangement wilt" + (!wine ? $": Nee (standaard)\n" : ": Ja\n"), 
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
                    wine = ChooseWine();
                    break;
                case 3:
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
                        _chosenWine = wine;
                        loop = false;
                        if (_chosenWine)
                            ChooseWineAmount();
                        else 
                            ChooseDate();
                    }
                    break;
                case 4:
                    ResStart();
                    break;
            }
        }
    }

    public static bool ChooseWine()
    {
        string prompt = "Kies hier of u een wijnarrangement wilt.\n" +
                        "Een wijnarrangement geeft een samengestelde wijnselectie bij iedere course.\n" +
                        "Een wijnarrangement kost €10 extra voor ieder persoon in uw groep die het wilt.\n" +
                        "Als u 'Ja' invult, vragen wij dalijk hoeveel mensen een wijnarrangement willen.\n";
        string[] options = new[] { "Ja", "Nee"};
        int chosenOption = _my1DMenu.RunMenu(options, prompt);
        switch (chosenOption)
        {
            case 0:
                return true;
            default:
                return false;
        }
    }

    public static void HasUnderageMembers()
    {
        int num;
        bool loop = true;
        while (loop)
        {
            string prompt = "Heeft u minderjarige personen in uw groep?\n" +
                            "Als dit zo is, vul dan 'Ja' in, en vul in hoeveel personen minderjarig zijn\n" +
                            "Dit geeft u wat extra korting per minderjarig persoon.\n";
            string[] options = new[] { "Ja", "Nee"};
            int chosenOption = _my1DMenu.RunMenu(options, prompt);
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
                            Console.WriteLine(
                                $"U kunt niet meer mensen aanwijzen dan de grootte van uw groep ({_amountOfPeople}). Voer opnieuw in.");
                            Thread.Sleep(2500);
                            UserLogin.DiscardKeys();
                        }
                        else
                        {
                            
                            loop = false;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public static void ChooseWineAmount()
    {
        int wineamount = 0;
        int num;
        while (true)
        {
            Console.ResetColor();
            string prompt = "U heeft aangegeven dat u een wijnarrangement wilt.\n" +
                            "Hoeveel personen uit u groep willen een wijnarrangement?\n" +
                            "Let op: per persoon kost een wijnarrangement €10.\n";
            string[] options = new[] { "Vul hier in hoeveel personen een wijnarrangement willen" + (wineamount == 0 ? "\n" : $": {wineamount}\n"), 
                "Doorgaan", "Ga terug"};
            int chosenOption = _my1DMenu.RunMenu(options, prompt);
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
                        ChooseDate();
                    }
                    break;
                case 2:
                    ResMenu();
                    break;
            }   
        }
    }

    public static int ChooseCourse()
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        JObject menu = AccountsAccess.LoadAllMenu();
        var pricing = menu["Prijzen"]!["Prijzen"];
        string[] options = new[] { $"2 Gangen ({pricing[0]})", $"3 Gangen ({pricing[1]})", $"4 Gangen ({pricing[2]})", "Ga terug" };
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

    public static int ChooseGroupSize()
    {
        string amountofPeople;
        while (true)
        {
            Console.ResetColor();
            string[] options = new[]
                { "Vul hier in met hoeveel mensen u komt.", "Ga terug" };
            int selectedIndex = _my1DMenu.RunResMenu(options, "\n\n\nKies hier uw groepsgrootte:", _stepCounter);
            Console.Clear();
            switch (selectedIndex)
            {
                case 0:
                    Console.CursorVisible = true;
                    Console.Write("Typ hier met hoeveel mensen u van plan bent te komen: ");
                    amountofPeople = Console.ReadLine()!;
                    bool success = int.TryParse(amountofPeople, out int number);
                    if (!success)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Voer enkel geldige nummers in, alstublieft.");
                        Thread.Sleep(2500);
                        amountofPeople = default;
                        break;
                    }
                    else
                    {
                        if (number <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(
                                "Nummers kleiner of gelijk aan nul zijn niet toegestaan. Voer opnieuw in.");
                            Thread.Sleep(2500);
                            amountofPeople = default;
                            break;
                        }
                        else if (number > 6)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(
                                "U moet bellen voor groepsgroottes boven de zes personen, bekijk onze contactinformatie in het hoofdmenu.");
                            Thread.Sleep(2500);
                            amountofPeople = default;
                            break;
                        }
                        else return number;
                    }

                    break;
                case 1:
                    return 0;
                    break;
            }
        }
    }

    public static void ChooseDate()
    {
        Console.Clear();
        var thisMonth = Reservations.PopulateDates();
        Console.WriteLine(
            $"\n\n\nU kunt alleen een reservatie maken voor de huidige maand ({ReservationsLogic.CurMonth})\nKies een datum (of druk op 'q' om terug te gaan):\n");
        for (int i = 0; i < thisMonth.GetLength(1); i++)
        {
            Console.Write($"{thisMonth[0, i].Date.ToString("ddd", CultureInfo.GetCultureInfo("nl"))}\t");
        }

        DateTime userChoice = _my2DMenu.RunMenu(thisMonth, "", _stepCounter, false);
        if (userChoice == default)
        {
            _stepCounter--;
            ChooseCourse();
        }
        else
        {
            _stepCounter++;
            _chosenDate = userChoice;
            ChooseTimeslot(_chosenDate);
        }
    }

    public static void ChooseTimeslot(DateTime chosenDate)
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
                    _stepCounter++;
                    ChooseTable(_chosenDate, _chosenTimeslot);
                    break;
                case 1:
                    ts1 = new TimeSpan(0, 19, 0, 0);
                    ts2 = new TimeSpan(0, 22, 0, 0);
                    _chosenTimeslot = (ts1, ts2);
                    _stepCounter++;
                    ChooseTable(_chosenDate, _chosenTimeslot);
                    break;
                case 2:
                    _stepCounter--;
                    ChooseDate();
                    break;
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
                    _stepCounter++;
                    ChooseTable(_chosenDate, _chosenTimeslot);
                    break;
                case 1:
                    entertime = timeslot2.Split("-")[0].Split(":");
                    leavetime = timeslot2.Split("-")[1].Split(":");
                    ts1 = new TimeSpan(0, Convert.ToInt32(entertime[0].Trim()), Convert.ToInt32(entertime[1].Trim()),
                        0);
                    ts2 = new TimeSpan(0, Convert.ToInt32(leavetime[0].Trim()), Convert.ToInt32(leavetime[1].Trim()),
                        0);
                    _chosenTimeslot = (ts1, ts2);
                    _stepCounter++;
                    ChooseTable(_chosenDate, _chosenTimeslot);
                    break;
                case 2:
                    entertime = timeslot3.Split("-")[0].Split(":");
                    leavetime = timeslot3.Split("-")[1].Split(":");
                    ts1 = new TimeSpan(0, Convert.ToInt32(entertime[0].Trim()), Convert.ToInt32(entertime[1].Trim()),
                        0);
                    ts2 = new TimeSpan(0, Convert.ToInt32(leavetime[0].Trim()), Convert.ToInt32(leavetime[1].Trim()),
                        0);
                    _chosenTimeslot = (ts1, ts2);
                    _stepCounter++;
                    ChooseTable(_chosenDate, _chosenTimeslot);
                    break;
                case 3:
                    _stepCounter--;
                    ChooseDate();
                    break;
            }
        }
    }

    public static void ChooseTable(DateTime res_Date, (TimeSpan, TimeSpan) chosenTime)
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
        if (selectedTable != default!) _chosenTable = selectedTable.Id;
        else
        {
            _stepCounter--;
            ChooseTimeslot(res_Date);
        }
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
