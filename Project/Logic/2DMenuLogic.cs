class _2DMenuLogic 
{
    private int _rowIndex = 0;
    private int _columnIndex = 0;
    private string[] _options = null;
    private DateTime[,] _dateTimes = null;
    private List<string> sizes = new List<string>();
    private List<(int, int)> forbiddenIndex = new List<(int, int)>();
    
    
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
                    if (_dateTimes[i, j] != DateTime.MinValue) Console.Write(_dateTimes[i, j].Day);
                    else Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    if (_dateTimes[i, j] != DateTime.MinValue) Console.Write(_dateTimes[i, j].Day);
                    else Console.ResetColor();
                }
                Console.Write("\t");
            }

            Console.WriteLine();
        }

        Console.ResetColor();
    }
    public DateTime RunMenu(DateTime[,] options, string prompt, bool printPrompt = true)
    {
        _dateTimes = options;
        AddForbiddenIndexes(_dateTimes);
        CheckPosition();
        ConsoleKey keyPressed;
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
                    CheckPosition();
                    break;
                case ConsoleKey.DownArrow:
                    _rowIndex++;
                    CheckPosition();
                    break;
                case ConsoleKey.RightArrow:
                    _columnIndex++;
                    CheckPosition();
                    break;
                case ConsoleKey.LeftArrow:
                    _columnIndex--;
                    CheckPosition();
                    break;
                case ConsoleKey.Q:
                    return default(DateTime);
            }
        } while (keyPressed != ConsoleKey.Enter);
        
        return _dateTimes[_rowIndex, _columnIndex];
    }

    public void AddForbiddenIndexes(DateTime[,] dates)
    {
        for (int x = 0; x < dates.GetLength(0); x++)
        {
            for (int y = 0; y < dates.GetLength(1); y++)
            {
                if (dates[x, y] == default(DateTime))
                {
                    forbiddenIndex.Add((x, y));
                }
            }
        }
    }

    public void CheckPosition()
    {
        /* this checks every index in the 2D array so you can never land on an occupied spot.
        Normally this only gets checked after a key press, but at the start of the program we should also check
        this right away.*/
        while (forbiddenIndex.Contains((_rowIndex, _columnIndex)) || _columnIndex <= -1)
        {
            _columnIndex--;
            if (_columnIndex <= -1) _columnIndex = _dateTimes.GetLength(1) - 1;
        }
        while (forbiddenIndex.Contains((_rowIndex, _columnIndex)) || _rowIndex >= _dateTimes.GetLength(0))
        {
            _rowIndex++;
            if (_rowIndex >= _dateTimes.GetLength(0)) _rowIndex = 0;
        }
        while (forbiddenIndex.Contains((_rowIndex, _columnIndex)) || _columnIndex >= _dateTimes.GetLength(1))
        {
            _columnIndex++;
            if (_columnIndex >= _dateTimes.GetLength(1)) _columnIndex = 0;
        }
        while (forbiddenIndex.Contains((_rowIndex, _columnIndex)) || _rowIndex <= -1)
        {
            _rowIndex--;
            if (_rowIndex <= -1) _rowIndex = _dateTimes.GetLength(0) - 1;
        }
    }
}

