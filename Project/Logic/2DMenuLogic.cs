using System.ComponentModel;

class _2DMenuLogic
{
    private bool noSpace = false;
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
        // weird spacing so it looks nice on the menu.
        if (printPrompt) Console.WriteLine(prompt + "\n" + "\n  Tafels (1-2 personen)\t      Tafels (3-4 personen)\t  Tafels (5-6 personen)");
        for (int i = 0; i < Tables.GetLength(0); i++)
        {
            for (int j = 0; j < Tables.GetLength(1); j++)
            {
                if (Tables[i, j] != null)
                {
                    bool groupcheck = (res_GroupSize - Tables[i, j].TableSize == 0 || res_GroupSize - Tables[i, j].TableSize == -1);
                    
                    // assign foreground colors based on availability
                    if (Tables[i, j].isReserved) Console.ForegroundColor = ConsoleColor.Red;
                    else if (!groupcheck) Console.ForegroundColor = ConsoleColor.DarkGray;
                    else Console.ForegroundColor = ConsoleColor.Green;
                    // assign background colors based on what's currently chosen
                    string toWrite = $"Tafel {Tables[i, j].Id}: {(Tables[i, j].isReserved ? "Bezet" : !groupcheck ? "Onbeschikbaar" : "Beschikbaar")}";
                    if (j == _columnIndex && i == _rowIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        toWrite = String.Concat("> ", toWrite);
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        toWrite = String.Concat("  ", toWrite);
                    }
                    // string formatting
                    Console.Write($"{toWrite, -18}");
                    Console.ResetColor();
                    // this is so the seperator "|" is always on the same place, no matter how long the actual string is.
                    int sep_Length = (toWrite.Length == 25 ? 1 : toWrite.Length == 23 ? 3 : 8);
                    // Write whitespaces for the above length, and then write the seperator string except for the last column.
                    Console.Write(String.Concat(Enumerable.Repeat(" ", sep_Length)) + (j != 2 ? " │" : "  "));

                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }
    public DateTime RunMenu(DateTime[,] options, string prompt, int stepCounter, bool printPrompt = true)
    {
        
        ConsoleKey keyPressed = default;
        _dateTimes = options;
        AddForbiddenIndexes(_dateTimes); 
        CheckPosition(_dateTimes, keyPressed);
        Console.CursorVisible = false;
        do
        {
            Console.SetCursorPosition(0, 0);
            InfoBoxes.WriteBoxStepCounter(Console.CursorTop, Console.CursorLeft, stepCounter);
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
            noSpace = false;
            Tables = tables;
            res_GroupSize = groupsize;
            ConsoleKey keyPressed = default;
            AddForbiddenIndexes(Tables);
            CheckPosition(Tables, keyPressed);
            Console.CursorVisible = false;
            // if noSpace is true, the entire tables array is either reserved or unavailable and the user should be kicked out.
            if (noSpace)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Er is geen ruimte meer op deze tijd voor {res_GroupSize} personen.");
                Thread.Sleep(2500);
                Console.ResetColor();
                return default;
            }
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
                    return default;
            }
        } while (keyPressed != ConsoleKey.Enter);

        return Tables[_rowIndex, _columnIndex];
        }

    public void AddForbiddenIndexes(DateTime[,] dates)
    {
        // check if there's any default dats and add them to forbidden indexes, so users can't iterate trough these in the menu.
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
        // check if there's any reserved tables, tables that have different group sizes or tables that are null
        // and add them to forbidden indexes, so users can't iterate trough these in the menu.
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
        // check position at start to make sure you dont begin on an occupied or unavailable space
        if (keyPressed == default && array != null)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (forbiddenIndex.Contains((i, j))) {}
                    // If the first row, column combination isn't forbidden, place the cursor there and go to end.
                    else
                    {
                        _rowIndex = i;
                        _columnIndex = j;
                        goto end;
                    }
                }
            }
            // if the goto didn't go off, the entire array is made out of forbidden indexes and the user should be kicked out.
            noSpace = true;
            end:
            array = null;
        }
        // check positions when moving with keyboard keys to make sure you don't move onto an occupied or unavailable space
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

