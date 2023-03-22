using System.Data;
using Project.Presentation;

static class MainMenu
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
                        break;
                    case 3:
                        Dishes.WelcomeMenu();
                            Thread.Sleep(5000);;
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
                            MainMenu.Start();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Already logged out");
                        }
                        break;
                    case 1:
                        break;
                    case 3:
                        Dishes.WelcomeMenu();;
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