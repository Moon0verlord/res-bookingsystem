class _2DMenuLogic 
{
    private int _rowIndex = 0;
    private int _columnIndex = 0;
    private int res_GroupSize = 0;
    private string[] _options = null;
    private DateTime[,] _dateTimes = null;
    private ReservationModel[,] Tables = null;
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
    
    public void DisplayTableOptions(string prompt, bool printPrompt)
    {
        if (printPrompt) Console.WriteLine(prompt);
        for (int i = 0; i < Tables.GetLength(0); i++)
        {
            for (int j = 0; j < Tables.GetLength(1); j++)
            {
                if (Tables[i, j] != null)
                {
                    bool groupcheck = (res_GroupSize - Tables[i, j].TableSize == 0 || res_GroupSize - Tables[i, j].TableSize == -1);
                    if (Tables[i, j].isReserved || !groupcheck) Console.ForegroundColor = ConsoleColor.Red;
                    else Console.ForegroundColor = ConsoleColor.Green;
                    if (j == _columnIndex && i == _rowIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write($"Table {Tables[i, j].Id}: {(!groupcheck ? "onbeschikbaar" : Tables[i, j].isReserved ? "Gereserveed" : "beschikbaar")}");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write($"Table {Tables[i, j].Id}: {(!groupcheck ? "onbeschikbaar" : Tables[i, j].isReserved ? "Gereserveed" : "beschikbaar")}");
                    }

                    Console.ResetColor();
                    Console.Write("\t");
                }
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }
    public DateTime RunMenu(DateTime[,] options, string prompt, bool printPrompt = true)
    {
        ConsoleKey keyPressed = default;
        _dateTimes = options;
        AddForbiddenIndexes(_dateTimes);
        CheckPosition(_dateTimes, keyPressed);
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
                    CheckPosition(_dateTimes, keyPressed);
                    break;
                case ConsoleKey.DownArrow:
                    _rowIndex++;
                    CheckPosition(_dateTimes, keyPressed);
                    break;
                case ConsoleKey.RightArrow:
                    _columnIndex++;
                    CheckPosition(_dateTimes, keyPressed);
                    break;
                case ConsoleKey.LeftArrow:
                    _columnIndex--;
                    CheckPosition(_dateTimes, keyPressed);
                    break;
                case ConsoleKey.Q:
                    return default(DateTime);
            }
        } while (keyPressed != ConsoleKey.Enter);
        
        return _dateTimes[_rowIndex, _columnIndex];
    }
    
        public ReservationModel RunTableMenu(ReservationModel[,] tables, string prompt, int groupsize, bool printPrompt = true)
        {
            Tables = tables;
            res_GroupSize = groupsize;
            ConsoleKey keyPressed = default;
            AddForbiddenIndexes(Tables);
            CheckPosition(Tables, keyPressed);
        do
        {
            Console.SetCursorPosition(0, 12);
            DisplayTableOptions(prompt, printPrompt);
            ConsoleKeyInfo selectedKey = Console.ReadKey(true);
            keyPressed = selectedKey.Key;
            switch (keyPressed)
            {
                case ConsoleKey.UpArrow:
                    _rowIndex--;
                    CheckPosition(Tables, keyPressed);
                    break;
                case ConsoleKey.DownArrow:
                    _rowIndex++;
                    CheckPosition(Tables, keyPressed);
                    break;
                case ConsoleKey.RightArrow:
                    _columnIndex++;
                    CheckPosition(Tables, keyPressed);
                    break;
                case ConsoleKey.LeftArrow:
                    _columnIndex--;
                    CheckPosition(Tables, keyPressed);
                    break;
                case ConsoleKey.Q:
                    return null;
            }
        } while (keyPressed != ConsoleKey.Enter);

        return Tables[_rowIndex, _columnIndex];
        }

    public void AddForbiddenIndexes(DateTime[,] dates)
    {
        forbiddenIndex.Clear();
        for (int x = 0; x < dates.GetLength(0); x++)
        {
            for (int y = 0; y < dates.GetLength(1); y++)
            {
                if ( dates[x, y] == default)
                {
                    forbiddenIndex.Add((x, y));
                }
            }
        }
    }
    
    public void AddForbiddenIndexes(ReservationModel[,] tables)
    {
        forbiddenIndex.Clear();
        for (int i = 0; i < tables.GetLength(0); i++)
        {
            for (int j = 0; j < tables.GetLength(1); j++)
            {
                if (tables[i, j] != null)
                {
                    if (tables[i, j].isReserved)
                    {
                        forbiddenIndex.Add((i, j));
                    }
                    else if (!(res_GroupSize - tables[i, j].TableSize == 0 || res_GroupSize - tables[i, j].TableSize == -1))
                    {
                        forbiddenIndex.Add((i, j));
                    }
                }
                else
                {
                    forbiddenIndex.Add((i, j));
                }   
            }
        }
    }

    public void CheckPosition<T>(T _2DArray, ConsoleKey keyPressed)
    {
        /* this checks every index in the 2D array so you can never land on an occupied spot.
        Normally this only gets checked after a key press, but at the start of the program we should also check
        this right away.*/
        var array = _2DArray as Array;
        if (array != null)
        {
            while (forbiddenIndex.Contains((_rowIndex, _columnIndex)) && keyPressed == ConsoleKey.DownArrow || _rowIndex >= array.GetLength(0))
            {
                _rowIndex++;
                if (_rowIndex >= array.GetLength(0)) _rowIndex = 0;
            }
            while (forbiddenIndex.Contains((_rowIndex, _columnIndex)) && keyPressed == ConsoleKey.LeftArrow || _columnIndex <= -1)
            {
                _columnIndex--;
                if (_columnIndex <= -1) _columnIndex = array.GetLength(1) - 1;
            }

            while (forbiddenIndex.Contains((_rowIndex, _columnIndex)) && keyPressed == ConsoleKey.RightArrow || _columnIndex >= array.GetLength(1))
            {
                _columnIndex++;
                if (_columnIndex >= array.GetLength(1)) _columnIndex = 0;
            }
            while (forbiddenIndex.Contains((_rowIndex, _columnIndex)) && keyPressed == ConsoleKey.UpArrow || _rowIndex <= -1)
            {
                _rowIndex--;
                if (_rowIndex <= -1) _rowIndex = array.GetLength(0) - 1;
            }
        }
    }
}

