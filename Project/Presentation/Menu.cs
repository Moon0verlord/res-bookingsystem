class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    public static void Start(AccountModel acc = null)
    {
        while (true)
        {

            if (acc == null || acc.loggedIn == false)
            {
                Console.WriteLine("[1] to login");
            }
            else
            {
                Console.WriteLine("[1] to logout");
            }

            Console.WriteLine("[2] to get the restaurant information");
            Console.WriteLine("[3] to get Schedule/ availability");
            Console.WriteLine("[4] to view the current menu");
            if (acc == null || acc.loggedIn == false)
            {
                Console.WriteLine("[5] to make a reservation with your email");
            }
            else
            {
                Console.WriteLine("[5] to make a reservation");
                
            }

            Console.WriteLine("[6] to view the menu");
            Console.WriteLine("[q] to leave");
        
        string input = Console.ReadLine()!;
        
            switch (input)
            {
                case "1":
                    if (acc==null||acc.loggedIn == false)
                    {
                        UserLogin.Start();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Succesfully logged out.");
                        Start(null);
                    }
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                {
                    Console.WriteLine("Invalid input");
                    Start();
                    break;
                }
            }
        }
    }
}