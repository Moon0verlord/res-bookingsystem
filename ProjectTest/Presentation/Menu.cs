using System.Data;

static class Menu
{
    static private MenuLogic myMenu = new MenuLogic();

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    public static void Start(AccountModel acc = null, int input = default)
    {
        while (true)
        {
            switch (input)
            {
                case 0:
                    if (acc==null||acc.loggedIn == false)
                    {
                        UserLogin.Start();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Already Logged in");
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                // case "4":
                //     break;
                // case "5":
                //     break;
                // case "q":
                //     Environment.Exit(0);
                //     break;
                default:
                {
                    string[] options = { "Log in", "Information", "Schedule", "View current menu", "Make reservation with e-mail"};
                    string prompt = "Menu:\n";
                    int selectedOption = myMenu.RunMenu(options, prompt);
                    break;
                }
            }
        }
    }
}