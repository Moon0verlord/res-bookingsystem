using System.Drawing;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static private MenuLogic myMenu = new MenuLogic();
    public static void Start()
    {
        string userEmail = null;
        string userPassword = null;
        string prompt = "Welcome to the log in menu.\n";
        string[] options = { "Enter e-mail", "Enter password", "No account?\n  >Create one here with current credentials<", "Login with current credentials", "Quit" };
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
                    Console.Clear();
                    Console.Write("For verification you must enter your password again: ");
                    string verifyUserPassword = Console.ReadLine()!;
                    if (userPassword == verifyUserPassword)
                        options[1] += $": {userPassword}";
                    else
                    {
                        Console.WriteLine("\nEntered verification password was different than original, please try again.");
                        Thread.Sleep(2000);
                        userPassword = null;
                    }
                    break;
                case 2:
                    if (userEmail == null || userPassword == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease enter your credentials first to create an account with them.");
                        Thread.Sleep(1500);
                        Console.ResetColor();
                    }
                    else CreateAccount(userEmail, userPassword);
                    break;
                case 3:
                    if (userEmail != null && userPassword != null )
                    { 
                        AccountModel acc = accountsLogic.CheckLogin(userEmail, userPassword);
                        if (acc != null)
                        {
                            Console.Clear();
                            Console.WriteLine("Welcome back " + acc.FullName + "!");
                            Console.WriteLine("Your email is " + acc.EmailAddress);
                            acc.loggedIn = true;
                            Thread.Sleep(2000);
                            MainMenu.Start(acc);
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"E-mail: {userEmail}\nPassword: {userPassword}");
                            Console.WriteLine("No account found with that email and password.\nIf you have no account yet, create one in the log in menu.");
                            Thread.Sleep(3500);
                            Console.ResetColor();
                            Start();
                            break;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease enter your credentials first.");
                        Thread.Sleep(1500);
                        Console.ResetColor();
                    }
                    break;
                case 4:
                    MainMenu.Start();
                    break;
            }
        }
    }
    
    public static void CreateAccount(string email, string password)
    {
        while (true)
        {
            
        }
    }
}