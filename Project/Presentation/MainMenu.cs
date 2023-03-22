using System.Data;

static class Menu
{
    static private MenuLogic _myMenu = new MenuLogic();

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    public static void Start(AccountModel acc = null)
    {
        if (acc == null)
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
                        Console.WriteLine("Press any key to return back to main menu.");
                        Console.ReadKey(true);
                        break;
                    case 5:
                        System.Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
        else if (acc != null && acc.loggedIn)
        {
            while (true)
            {
                string[] options = { "Log out", "Information", "Schedule", "View current menu", "Make reservation", "Quit (and log out)"};
                string prompt = $"\nWelcome {acc.FullName}:";
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 0:
                        if  (acc.loggedIn == true)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("Are you sure? (y/n): ");
                            Console.ResetColor();
                            string userAnswer = Console.ReadLine()!;
                            if (userAnswer == "y" || userAnswer == "Y") Menu.Start();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Already logged out");
                        }
                        break;
                    case 1:
                        break;
                    case 5:
                        System.Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}