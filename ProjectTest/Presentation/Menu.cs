using System.Data;

static class Menu
{
    private static int _currentIndex = 0;
    private static string[] _options = null;
    
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
                    Console.WriteLine("Invalid input");
                    break;
                }
            }
        }
    }

    public static void DisplayOptions(string prompt, bool printPrompt)
    {
        if (printPrompt) Console.WriteLine(prompt);
        for (int i = 0; i < _options.Length; i++)
        {
            if (i == _currentIndex)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("> " + _options[i]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("  " + _options[i]);
            }
        }

        Console.ResetColor();
    }

    public static int RunMenu(string[] options, string prompt, bool printPrompt = true)
    {
        _options = options;
        ConsoleKey keyPressed;
        Console.Clear();
        do
        { 
            Console.SetCursorPosition(0, 0);
            DisplayOptions(prompt, printPrompt);
            ConsoleKeyInfo selectedKey = Console.ReadKey(true);
            keyPressed = selectedKey.Key;
            switch (keyPressed)
            {
                case ConsoleKey.UpArrow:
                    _currentIndex--;
                    if (_currentIndex <= -1) _currentIndex = _options.Length - 1;
                    break;
                case ConsoleKey.DownArrow:
                    _currentIndex++;
                    if (_currentIndex == _options.Length) _currentIndex = 0;
                    break;
            }

        } while (keyPressed != ConsoleKey.Enter);
        
        return _currentIndex;
    }
}