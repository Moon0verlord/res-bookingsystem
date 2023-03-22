static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine()!;
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine()!;
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            //Password verification
            while (true)
            {
                Console.WriteLine("Verify Password");
                var VPassword = Console.ReadLine()!;
                if (VPassword == password)
                {
                    Console.WriteLine("Welcome back " + acc.FullName);
                    Console.WriteLine("Your email number is " + acc.EmailAddress);
                    acc.loggedIn = true;
                    Menu.Start(acc);
                }
                else
                {
                    Console.WriteLine("Incorrect input");
                }
            }
        }
        else
        {
            Console.WriteLine("No account found with that email and password");
        }
    }
}