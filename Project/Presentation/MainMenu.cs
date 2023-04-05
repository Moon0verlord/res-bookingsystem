using System.Data;
using Project.Presentation;


class MainMenu : IMenuLogic
{
    private static MenuLogic _myMenu = new MenuLogic();
    static public AccountModel Account { get; set; }

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    public static void Start(AccountModel acc = null)
    {
        if (Account == null)
        {
            Account = acc;
        }
        if (Account == null)
        {
            while (true)
            {
                // main menu functionality for non-logged in users.
                string[] options = { "Inloggen", "Informatie", "Tijden", "Bekijk het menu", "Maak een reservatie met e-mail", "Afsluiten" };
                string prompt = "\nHoofdmenu:";
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 0:
                        UserLogin.Start();
                        break;
                    case 1:
                        restaurantInfo.Start();
                        Console.WriteLine("Druk om een knop om terug te gaan naar het hoofdmenu");
                        Console.ReadKey(true);
                        break;
                    case 2:
                        //
                        break;
                    case 3:
                        Dishes.WelcomeMenu();
                        Thread.Sleep(5000); ;
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
        if (Account != null && Account.loggedIn)
        {
            while (true)
            {
                string[] options = { "Uitloggen", "Informatie", "Tijden", "Bekijk het menu", "Maak een reservatie met e-mail", "Afsluiten (En gelijk uitloggen)" };
                string prompt = $"\nWelkom {Account.FullName}:";
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 0:
                        if (Account.loggedIn)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Weet u het zeker? (j/n): ");
                            Console.ResetColor();
                            string userAnswer = Console.ReadLine()!;
                            if (userAnswer == "j" || userAnswer == "J")
                            {
                                Account = null;
                                Start();
                            }
                        }
                        else
                        {
                            Console.WriteLine("U bent al uitgelogd");
                        }
                        break;
                    case 1:
                        restaurantInfo.Start();
                        Console.WriteLine("Druk op een knop om terug te gaan naar het hoofdmenu.");
                        Console.ReadKey(true);
                        break;
                    case 2:
                        Reservation.ResStart(Account);
                        break;
                    case 3:
                        Dishes.WelcomeMenu();
                        Thread.Sleep(5000); ;
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
        if (Account != null && Account.loggedIn && Account.IsEmployee)
        {
            if (Account.IsManager)
            {
                //Manager menu
                while (true)
                {
                    //Verander menu includes price changing
                    string[] options = { "Uitloggen", "Voeg medewerker toe", "Verander menu", "Evenementen",""};
                    string prompt = $"\nWelkom {Account.FullName}:";
                    int input = _myMenu.RunMenu(options, prompt);
                    switch (input)
                    {
                        case 0:
                            if (Account.loggedIn)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("Weet u het zeker? (j/n): ");
                                Console.ResetColor();
                                string userAnswer = Console.ReadLine()!;
                                if (userAnswer == "j" || userAnswer == "J")
                                {
                                    Account = null;
                                    Start();
                                }
                            }
                            else
                            {
                                Console.WriteLine("U bent al uitgelogd");
                            }
                            break;
                    }
                }
            }
            //Employee menu
            while (true)
            {
                string[] options = { "Uitloggen", "Overzicht Reserveringen","Contact Gegevens"};
                string prompt = $"\nWelkom {Account.FullName}:";
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 0:
                        if (Account.loggedIn)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Weet u het zeker? (j/n): ");
                            Console.ResetColor();
                            string userAnswer = Console.ReadLine()!;
                            if (userAnswer == "j" || userAnswer == "J")
                            {
                                Account = null;
                                Start();
                            }
                        }
                        else
                        {
                            Console.WriteLine("U bent al uitgelogd");
                        }

                        break;
                }
            }
        }
    }
}