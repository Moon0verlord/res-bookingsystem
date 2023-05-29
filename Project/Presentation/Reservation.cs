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
    private static int _amountOfPeople;
    private static DateTime _chosenDate;
    private static (TimeSpan, TimeSpan) _chosenTimeslot;
    private static string _chosenTable;
    private static int _chosenCourse;
    private static int _stepCounter;

    public static void ResStart(AccountModel acc = null)
    {
        // resetting all static fields for a fresh reservation start
        _acc = acc;
        _userEmail = null;
        _amountOfPeople = 0;
        _chosenDate = default;
        _chosenTimeslot = (default, default);
        _chosenTable = "";
        _chosenCourse = 0;
        _stepCounter = 1;
        Console.Clear();
        if (acc == null!)
        {
            string email = null!;
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                string prompt = $"\n\n\nVul hier uw e-mail in om een reservatie te maken.";
                string[] options =
                {
                    $"Vul hier uw e-mail in" + (email == null ? "\n" : $": {email}\n"), "Doorgaan",
                    "Reservering bekijken", "Ga terug"
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
                            Thread.Sleep(3000);
                            UserLogin.DiscardKeys();
                        }

                        break;
                    case 1:
                        if (email != null)
                        {
                            loop = false;
                            _userEmail = email;
                            _stepCounter++;
                            ResMenu();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\nVul eerst uw e-mail in.");
                            Thread.Sleep(1800);
                            Console.ResetColor();
                        }

                        break;
                    case 2:
                        ViewRes(email!);
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
            _userEmail = acc.EmailAddress;
            ResMenu();
        }
    }

    public static void ResMenu()
    {
        Console.Clear();
        _amountOfPeople = ChooseGroupSize();
        if (_amountOfPeople <= 0)
        {
            _stepCounter--;
            if (_acc == null!)
                ResStart();
            else MainMenu.Start();
        }
        else
        {
            _stepCounter++;
            ChooseCourse();
        }

        //chosenTimeslot item 1 is time when entering, item 2 is time when leaving.
        while (true)
        {
            _stepCounter++;
            Console.Clear();
            InfoBoxes.WriteBoxStepCounter(Console.CursorTop, Console.CursorLeft, _stepCounter);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(
                $"\n\n\nEmail:{_userEmail}\nReservatie tafel nummer: {_chosenTable}\nDatum: {_chosenDate.Date.ToString("dd-MM-yyyy")}" +
                $"\nTijd: ({_chosenTimeslot.Item1} - {_chosenTimeslot.Item2})\nWeet u zeker dat u deze tijd wil reserveren? (j/n): ");
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
                    MainMenu.Start(_acc);
                    break;
                case 0:
                    MainMenu.Start(_acc);
                    break;
                case -1:
                    break;
            }
        }
    }

    public static void ChooseCourse()
    {
        string[] options = new[] { "2 Gangen", "3 Gangen", "4 Gangen", "Ga terug" };
        int chosenIndex = _my1DMenu.RunResMenu(options, "\n\n\nKies een gang voor uw reservering:", _stepCounter);
        switch (chosenIndex)
        {
            case 0:
                _chosenCourse = 2;
                _stepCounter++;
                ChooseDate();
                break;
            case 1:
                _chosenCourse = 3;
                _stepCounter++;
                ChooseDate();
                break;
            case 2:
                _chosenCourse = 4;
                _stepCounter++;
                ChooseDate();
                break;
            case 3:
                _stepCounter--;
                ResMenu();
                break;
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
