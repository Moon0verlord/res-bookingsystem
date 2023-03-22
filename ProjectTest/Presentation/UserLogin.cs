static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        string userEmail = null;
        string userPassword = null;
        string prompt = "Welcome to the log in menu.\n";
        string[] options = { "Enter e-mail", "Enter password" };
        while (true)
        {
            int selectedIndex = Menu.RunMenu(options, prompt);
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
            }

            if (userEmail != null && userPassword != null )
            { 
                AccountModel acc = accountsLogic.CheckLogin(userEmail, userPassword);
                if (acc != null)
                {
                    Console.WriteLine("Welcome back " + acc.FullName);
                    Console.WriteLine("Your email number is " + acc.EmailAddress);
                    acc.loggedIn = true;
                    Menu.Start(acc);
                    break;
                }
                else
                {
                    Console.WriteLine("No account found with that email and password");
                    Menu.Start();
                    break;
                }
            }
        }
    }
}