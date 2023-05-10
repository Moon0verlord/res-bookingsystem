public class MenuLogic
{
    private int _currentIndex = 0;
    private string[] _options = null;
    private List<string> sizes = new List<string>();
    private List<int> forbiddenIndex = new List<int>();
    private List<ReservationModel> tables = new List<ReservationModel>();
    private int res_GroupSize = default;

    public void DisplayOptions(string prompt, bool printPrompt)
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

    public void DisplayTimeOptions(string prompt, bool printPrompt)
    {
        if (printPrompt) Console.WriteLine(prompt);
        Console.WriteLine();
        for (int i = 0; i < _options.Length; i++)
        {
            if (i == _currentIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Write(_options[i]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Write(_options[i]);
            }

            Console.Write("\t");
        }

        Console.ResetColor();
    }

    public int RunMenu(string[] options, string prompt, bool printPrompt = true, bool sideways = false,
        bool displayTime = false)
    {
        _currentIndex = 0;
        _options = options;
        ConsoleKey keyPressed;
        Console.Clear();
        Console.CursorVisible = false;
        do
        {
            Console.SetCursorPosition(0, 0);
            if (!displayTime) DisplayOptions(prompt, printPrompt);
            else if (displayTime) DisplayTimeOptions(prompt, printPrompt);
            ConsoleKeyInfo selectedKey = Console.ReadKey(true);
            keyPressed = selectedKey.Key;
            if (sideways)
            {
                switch (keyPressed)
                {
                    case ConsoleKey.LeftArrow:
                        _currentIndex--;
                        if (_currentIndex <= -1) _currentIndex = _options.Length - 1;
                        break;
                    case ConsoleKey.RightArrow:
                        _currentIndex++;
                        if (_currentIndex == _options.Length) _currentIndex = 0;
                        break;
                }
            }
            else
            {
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
            }


        } while (keyPressed != ConsoleKey.Enter);

        return _currentIndex;
    }

    public int RunResMenu(string[] options, string prompt, int stepCounter, bool printPrompt = true,
        bool sideways = false)
    {
        _currentIndex = 0;
        _options = options;
        ConsoleKey keyPressed;
        Console.Clear();
        Console.CursorVisible = false;
        do
        {
            Console.SetCursorPosition(0, 0);
            InfoBoxes.WriteBoxStepCounter(Console.CursorTop, Console.CursorLeft, stepCounter);
            DisplayOptions(prompt, printPrompt);
            ConsoleKeyInfo selectedKey = Console.ReadKey(true);
            keyPressed = selectedKey.Key;
            if (sideways)
            {
                switch (keyPressed)
                {
                    case ConsoleKey.LeftArrow:
                        _currentIndex--;
                        if (_currentIndex <= -1) _currentIndex = _options.Length - 1;
                        break;
                    case ConsoleKey.RightArrow:
                        _currentIndex++;
                        if (_currentIndex == _options.Length) _currentIndex = 0;
                        break;
                }
            }
            else
            {
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
            }


        } while (keyPressed != ConsoleKey.Enter);

        return _currentIndex;
    }
}
