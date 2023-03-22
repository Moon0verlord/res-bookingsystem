static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static private MenuLogic myMenu = new MenuLogic();
    public static void Start()
    {
        string userEmail = null;
        string userPassword = null;
        string prompt = "Welcome to the log in menu.\n";
        string[] options = { "Enter e-mail", "Enter password", "Quit" };
        while (true)
        {
            int selectedIndex = myMenu.RunMenu(options, prompt);
            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    Console.Write("Enter your e-mail: ");
                    userEmail = Console.ReadLine()!;
                    options[0] += $": {userEmail}";
                    break;
                case 1:
                    Console.Clear();
                    Console.Write("Enter your password: ");
                    userPassword = Console.ReadLine()!;
                    options[1] += $": {userPassword}";
                    break;
                case 2:
                    Menu.Start();
                    break;
            }

            if (userEmail != null && userPassword != null )
            { 
                AccountModel acc = accountsLogic.CheckLogin(userEmail, userPassword);
                if (acc != null)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome back " + acc.FullName);
                    Console.WriteLine("Your email number is " + acc.EmailAddress);
                    acc.loggedIn = true;
                    Thread.Sleep(2000);
                    Menu.Start(acc);
                    break;
                }
                else
                {
                    Console.WriteLine("No account found with that email and password");
                    Thread.Sleep(2000);
                    Menu.Start();
                    break;
                }
            }
        }
    }
}