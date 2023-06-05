
using Project.Presentation;


class MainMenu : IMenuLogic
{
    private static MenuLogic _myMenu = new MenuLogic();
    public static AccountModel? Account { get; set; }

    private static string _ascii = @"  
██╗  ██╗ ██████╗  ██████╗ ███████╗██████╗ ███╗   ███╗███████╗███╗   ██╗██╗   ██╗
██║  ██║██╔═══██╗██╔═══██╗██╔════╝██╔══██╗████╗ ████║██╔════╝████╗  ██║██║   ██║
███████║██║   ██║██║   ██║█████╗  ██║  ██║██╔████╔██║█████╗  ██╔██╗ ██║██║   ██║
██╔══██║██║   ██║██║   ██║██╔══╝  ██║  ██║██║╚██╔╝██║██╔══╝  ██║╚██╗██║██║   ██║
██║  ██║╚██████╔╝╚██████╔╝██║     ██████╔╝██║ ╚═╝ ██║███████╗██║ ╚████║╚██████╔╝
╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝     ╚═════╝ ╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝ ╚═════╝ ";


    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    public static void Start(AccountModel? acc = null)
    {
        if (Account == null!)
        {
            Account = acc!;
        }
        if (Account == null!)
        {
            while (true)
            {
                // main menu functionality for non-logged in users.
                string[] options = { "Log-in portal", "Informatie", "Bekijk het menu","Special Events", "Reserveringen bekijken", "Maak een reservering met e-mail", "Afsluiten" };
                string prompt = $"{_ascii}";
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 0:
                        UserLogin.Start();
                        break;
                    case 1:
                        restaurantInfo.Start();
                        break;
                    case 2:
                        Dishes.UserOptions();
                        Thread.Sleep(5000);
                        break;
                    case 3:
                        SpecialEvent.Eventmenu();
                        break;
                    case 4:
                        Reservation.ViewRes2();
                        break;
                    case 5:
                        Reservation.ResStart(Account!);
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;

                }
            }
        }
        if (Account != null! && Account.LoggedIn && Account.IsEmployee)
        {
            if (Account.IsManager)
            {
                //Manager menu
                while (true)
                {
                    // displays menu with various management options if the user is a manager
                    string[] options = { "Uitloggen", "Voeg medewerker toe", "Verwijder een medewerker", "Verander menu", "Event organiseren", "Reserverings overzicht", "Reservering aanpassen" };
                    string prompt = $"\nWelkom {Account.FullName}:";
                    int input = _myMenu.RunMenu(options, prompt);
                    switch (input)
                    {
                        case 0:
                            if (Account.LoggedIn)
                            {
                                AccountsLogic.LogOut();
                            }
                            else
                            {
                                Console.WriteLine("U bent al uitgelogd");
                            }
                            break;
                        case 1:
                            EmployeeManagerLogic.AddEmployee();
                            break;
                        case 2:
                            EmployeeManagerLogic.RemoveEmployee();
                            break;
                        case 3:
                            Dishes.ManagerOptions();
                            break;
                        case 4:
                            SpecialEvent.ResEvent();
                            break;
                        case 5:
                            EmployeeManagerLogic.CheckReservations();
                            break;
                        case 6:
                            EmployeeManagerLogic.ChangeReservation();
                            break;
                    }
                }
            }
            //Employee menu
            while (true)
            {
                // menu for employees who are not managers
                string[] options = {  "Bekijk het menu",
                    "Reserveringen","Uitloggen"};
                string prompt = $"\nWelkom {Account.FullName}:";
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 2:
                        if (Account.LoggedIn)
                        {
                            AccountsLogic.LogOut();
                        }
                        else
                        {
                            Console.WriteLine("U bent al uitgelogd");
                        }
                        break;
                    case 1:
                        EmployeeManagerLogic.CheckReservations();
                        break;
                    case 0:
                        Dishes.UserSelection();
                        break;

                }
            }
        }
        //User Login
        if (Account != null && Account.LoggedIn)
        {
            while (true)
            {
                string[] options = { "Informatie", "Bekijk het menu", "Reserveren", "Reserveringen bekijken", "Uitloggen", "Afsluiten (En gelijk uitloggen)" };
                string prompt = $"\nWelkom {Account.FullName}:";
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 0:
                        restaurantInfo.Start();
                        break;
                    case 1:
                        Dishes.UserSelection();
                        Thread.Sleep(5000);
                        break;
                    case 2:
                        Reservation.ResStart(Account);
                        break;
                    case 3:
                        Reservation.ViewResAccount(Account);
                        break;
                    case 4:
                        if (Account.LoggedIn)
                        {
                            AccountsLogic.LogOut();
                        }
                        else
                        {
                            Console.WriteLine("U bent al uitgelogd");
                        }
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}