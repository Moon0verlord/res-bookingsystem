namespace Project.Logic;

class _2DMenuLogic 
{
    private int _rowIndex = 0;
    private int _columnIndex = 0;
    private string[] _options = null;
    private DateTime[,] _dateTimes = null;
    private List<string> sizes = new List<string>();
    private List<int> forbiddenIndex = new List<int>();
    
    
    public void DisplayDateOptions(string prompt, bool printPrompt)
    {
        if (printPrompt) Console.WriteLine(prompt);
        Console.WriteLine("\n" + "\n" + "\n");
        for (int i = 0; i < _dateTimes.GetLength(0); i++)
        {
            for (int j = 0; j < _dateTimes.GetLength(1); j++)
            {
                if (j == _columnIndex && i == _rowIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(_dateTimes[i, j]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(_dateTimes[i, j]);
                }
                Console.Write("\t");
            }

            Console.WriteLine();
        }

        Console.ResetColor();
    }
    public (int, int) RunMenu(DateTime[,] options, string prompt, bool printPrompt = true)
    {
        _dateTimes = options;
        ConsoleKey keyPressed;
        Console.Clear();
        do
        {
            Console.SetCursorPosition(0, 0);
            DisplayDateOptions(prompt, printPrompt);
            ConsoleKeyInfo selectedKey = Console.ReadKey(true);
            keyPressed = selectedKey.Key;
            switch (keyPressed)
            {
                case ConsoleKey.UpArrow:
                    _rowIndex--;
                    if (_rowIndex <= -1) _rowIndex = _dateTimes.GetLength(0) - 1;
                    break;
                case ConsoleKey.DownArrow:
                    _rowIndex++;
                    if (_rowIndex == _dateTimes.GetLength(0)) _rowIndex = 0;
                    break;
                case ConsoleKey.RightArrow:
                    _columnIndex++;
                    if (_columnIndex >= _dateTimes.GetLength(1)) _columnIndex = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    _columnIndex--;
                    if (_columnIndex <= -1) _columnIndex = _dateTimes.GetLength(1) - 1;
                    break;
            }
        } while (keyPressed != ConsoleKey.Enter);

        return (_rowIndex, _columnIndex);
    }
}