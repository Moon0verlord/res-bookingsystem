using System.Data;
using Project.Presentation;


static class MainMenu
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
                string[] options = { "Log in", "Information", "Schedule", "View current menu", "Make reservation with e-mail", "Quit"};
                string prompt = "\nMenu:";
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 0:
                        UserLogin.Start();
                        break;
                    case 1:
                        restaurantInfo.Start();
                        Console.WriteLine("Press any key to return back to main menu");
                        Console.ReadKey(true);
                        break;
                    case 2:
                        //
                         break;
                    case 3:
                        Dishes.WelcomeMenu();
                        Thread.Sleep(5000);;
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
                string[] options = { "Log out", "Information", "Schedule", "View current menu", "Make reservation", "Quit (and log out)"};
                string prompt = $"\nWelcome {Account.FullName}:";
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 0:
                        if  (Account.loggedIn)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Are you sure? (y/n): ");
                            Console.ResetColor();
                            string userAnswer = Console.ReadLine()!;
                            if (userAnswer == "y" || userAnswer == "Y")
                            {
                                Account = null;
                                Start();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Already logged out");
                        }
                        break;
                    case 1:
                        restaurantInfo.Start();
                        Console.WriteLine("Press any key to return back to main menu.");
                        Console.ReadKey(true);
                        break;
                    case 2:
                        Reservation.ResStart(Account);
                        break;
                    case 3:
                        Dishes.WelcomeMenu();
                        Thread.Sleep(5000);;
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
    }
}