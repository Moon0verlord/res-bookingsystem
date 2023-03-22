using System.Drawing;

static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    static private MenuLogic myMenu = new MenuLogic();
    private static string  userEmail;
    private static string userPassword;
    public static void Start()
    {
        userEmail = null;
        userPassword = null;
        while (true)
        {
            string prompt = "Welcome to the log in menu.\n";
            string[] options = { $"Enter e-mail" + (userEmail == null ? "" : $": {userEmail}"), 
                "Enter password" + $"{(userPassword == null ? "\n" : $": {userPassword}\n")}", 
                "No account?\n  >Create one here with current credentials<", "Login with current credentials", "Quit" };
            int selectedIndex = myMenu.RunMenu(options, prompt);
            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    Console.Write("Enter your e-mail: ");
                    userEmail = Console.ReadLine()!;
                    if (userEmail.Contains("@") == false)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid email.\nA valid email must contain a '@' character.");
                        Console.ResetColor();
                        userEmail = null;
                        Thread.Sleep(3000);
                    }
                    break;
                case 1:
                    Console.Clear();
                    Console.Write("Enter your password: ");
                    userPassword = Console.ReadLine()!;
                    Console.Clear();
                    Console.Write("For verification you must enter your password again: ");
                    string verifyUserPassword = Console.ReadLine()!;
                    if (userPassword == verifyUserPassword)
                        break;
                    else
                    {
                        Console.WriteLine("\nEntered verification password was different than original, please try again.");
                        Thread.Sleep(2000);
                        userPassword = null;
                        break;
                    }
                case 2:
                    if (userEmail == null || userPassword == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease enter your credentials first to create an account with them.");
                        Thread.Sleep(1500);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Clear();
                        var emailExists = accountsLogic.GetByEmail(userEmail);
                        if (emailExists != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nAccount already exists with this e-mail.");
                            Thread.Sleep(2000);
                            Console.ResetColor();
                            Start();
                        }
                        Console.Write("Please enter your full name: ");
                        string fullName = Console.ReadLine()!;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Clear();
                        Console.WriteLine($"\nFull name: {fullName}\nE-mail: {userEmail}\nPassword: {userPassword}\nAre you sure you want to make an account with these credentials? (y/n)");
                        Console.ResetColor();
                        string answer = Console.ReadLine()!;
                        if (answer == "y" || answer == "Y")
                        {
                            var newAccount = CreateAccount(userEmail, userPassword, fullName);
                            accountsLogic.UpdateList(newAccount);
                        }
                        else Start();
                        
                    }
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
                            Menu.Start(acc);
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
                    Menu.Start();
                    break;
            }
        }
    }
    
    public static AccountModel CreateAccount(string email, string password, string name)
    {
        var newAccount = AccountsAccess.AddAccount(email, password, name);
        return newAccount;
    }
}