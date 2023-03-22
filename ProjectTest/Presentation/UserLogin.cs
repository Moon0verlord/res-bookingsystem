static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        
        while (true)
        {
            string prompt = "Welcome to the log in menu.\n";
            string[] options = { "Enter e-mail", "Enter password" };
            int selectedIndex = Menu.RunMenu(options, prompt);
            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    Console.Write("Enter your e-mail: ");
                    string userEmail = Console.ReadLine()!;
                    options[0] = options[0] + $": {userEmail}";
                    Console.WriteLine();
                    break;
                case 1:
                    Console.Clear();
                    Console.Write("Enter your password: ");
                    string userPassword = Console.ReadLine()!;
                    options[1] += $": {userPassword}";
                    break;
            }
        }

//         AccountModel acc = accountsLogic.CheckLogin(email, password);
//         if (acc != null)
//         {
//             Console.WriteLine("Welcome back " + acc.FullName);
//             Console.WriteLine("Your email number is " + acc.EmailAddress);
//             acc.loggedIn = true;
//             Menu.Start(acc);
//         }
//         else
//         {
//             Console.WriteLine("No account found with that email and password");
//         }
     }
}