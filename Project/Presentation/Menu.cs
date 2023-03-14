static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        Console.WriteLine("[1] to login");
        Console.WriteLine("[2] to get the restaurant information");
        Console.WriteLine("[3] to get Schedule/ availability");
        Console.WriteLine("[4] to view the current menu");
        Console.WriteLine("[5] to make a reservation with your email");

        string input = Console.ReadLine();
        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            Console.WriteLine("This feature is not yet implemented");
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }

    }
}