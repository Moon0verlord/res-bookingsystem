using System.Data;
using Project.Presentation;
using Newtonsoft.Json;

class MainMenu : IMenuLogic
{
    private static MenuLogic _myMenu = new MenuLogic();
    static public AccountModel Account { get; set; }

    private static string ascii = @"  
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
                string[] options = { "Inloggen", "Informatie", "Tijden", "Bekijk het menu", "Maak een reservatie met e-mail", "Afsluiten" };
                string prompt = $"{ascii}";
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 0:
                        UserLogin.Start();
                        break;
                    case 1:
                        restaurantInfo.Start();
                        Console.WriteLine("Druk op een knop om terug te gaan naar het hoofdmenu");
                        Console.ReadKey(true);
                        break;
                    case 2:
                        TimeInfo.Start();
                        Console.WriteLine("Druk op een knop om terug te gaan naar het hoofdmenu");
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Dishes.UserOptions();
                        Thread.Sleep(5000);
                        break;
                    case 4:
                        Reservation.ResStart(Account);
                        break;
                    case 5:
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
                    string[] options = { "Uitloggen", "Voeg medewerker toe", "Verander menu", "Evenementen", "Reservatie overzicht" };
                    string prompt = $"\nWelkom {Account.FullName}:";
                    int input = _myMenu.RunMenu(options, prompt);
                    switch (input)
                    {
                        case 0:
                            if (Account.LoggedIn)
                            {
                                Console.CursorVisible = true;
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("Weet u het zeker? (j/n): ");
                                Console.ResetColor();
                                string userAnswer = Console.ReadLine()!;
                                if (userAnswer == "j" || userAnswer == "J")
                                {
                                    Account = null!;
                                    Start();
                                }
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
                            Dishes.ManagerOptions();
                            break;
                        case 3:
                            SpecialEvent.Eventmenu();
                            break;
                        case 4:
                            EmployeeManagerLogic.CheckReservations();
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
                            Console.CursorVisible = true;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Weet u het zeker? (j/n): ");
                            Console.ResetColor();
                            string userAnswer = Console.ReadLine()!;
                            if (userAnswer == "j" || userAnswer == "J")
                            {
                                Account = null!;
                                Start();
                            }
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
                string[] options = { "Informatie", "Tijden", "Bekijk het menu", "Reserveren", "Reserveringen bekijken", "Uitloggen", "Afsluiten (En gelijk uitloggen)" };
                string prompt = $"\nWelkom {Account.FullName}:";
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 0:
                        restaurantInfo.Start();
                        Console.WriteLine("Druk op een knop om terug te gaan naar het hoofdmenu.");
                        Console.ReadKey(true);
                        break;
                    case 1:
                        TimeInfo.Start();
                        Console.WriteLine("Druk op een knop om terug te gaan naar het hoofdmenu.");
                        break;
                    case 2:
                        Dishes.UserSelection();
                        Thread.Sleep(5000); ;
                        break;
                    case 3:
                        Reservation.ResStart(Account);
                        break;
                    case 4:
                        Reservation.ViewRes(Account.EmailAddress);
                        Console.WriteLine("Druk op een knop om terug te gaan naar het hoofdmenu.");
                        Console.ReadKey(true);
                        break;
                    case 5:
                        if (Account.LoggedIn)
                        {
                            Console.CursorVisible = true;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Weet u het zeker? (j/n): ");
                            Console.ResetColor();
                            string userAnswer = Console.ReadLine()!;
                            if (userAnswer == "j" || userAnswer == "J")
                            {
                                Account = null!;
                                Start();
                            }
                        }
                        else
                        {
                            Console.WriteLine("U bent al uitgelogd");
                        }
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}