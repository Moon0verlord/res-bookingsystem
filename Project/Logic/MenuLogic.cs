﻿public class MenuLogic
{
    private int _currentIndex = 0;
    private string[] _options = null;
    private List<string> sizes = new List<string>();
    private List<int> forbiddenIndex = new List<int>();
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
    
    public void DisplayDateOptions(string prompt, bool printPrompt)
    {
        if (printPrompt) Console.WriteLine(prompt);
        Console.WriteLine("\n" + "\n" + "\n");
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
    
    public void DisplayTableOptions(List<ReservationModel> tables, string prompt, bool printPrompt)
    {
        sizes.Add("table for 2");
        sizes.Add("table for 4");
        sizes.Add("table for 6");
        if (printPrompt) Console.WriteLine(prompt);
        Console.WriteLine();
        for (int i = 0; i < tables.Count; i++)
        {
            if (tables[i] != null)
            {
                if (tables[i].isReserved) Console.ForegroundColor = ConsoleColor.Red;
                else Console.ForegroundColor = ConsoleColor.Green;
                if (i == _currentIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"Table {tables[i].Id}: {(tables[i].isReserved ? "Occupied" : "Available")}");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"Table {tables[i].Id}: {(tables[i].isReserved ? "Occupied" : "Available")}");
                }
            }
            else
            {
                if (sizes.Count >= 1)
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(sizes[0]);
                    sizes.RemoveAt(0);
                }
            }
        }

        Console.ResetColor();
    }

    public int RunMenu(string[] options, string prompt, bool printPrompt = true, bool sideways = false, bool displayTime = false)
    {
        _options = options;
        ConsoleKey keyPressed;
        Console.Clear();
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
    
    public Dictionary<int, DateTime> RunMenu(List<DateTime> options, string prompt, bool printPrompt = true)
    {
        _currentIndex = 0;
        _options = options.Select(i => i.Day.ToString()).ToArray();
        ConsoleKey keyPressed;
        do
        {
            Console.SetCursorPosition(0, 0);
            DisplayDateOptions(prompt, printPrompt);
            ConsoleKeyInfo selectedKey = Console.ReadKey(true);
            keyPressed = selectedKey.Key;
            switch (keyPressed)
            {
                case ConsoleKey.LeftArrow:
                    _currentIndex--;
                    if (_currentIndex <= -1) _currentIndex = options.Count - 1;
                    break;
                case ConsoleKey.RightArrow:
                    _currentIndex++;
                    if (_currentIndex == options.Count) _currentIndex = 0;
                    break;
            }
    
        } while (keyPressed != ConsoleKey.Enter);

        return new Dictionary<int, DateTime>() { { _currentIndex, options[_currentIndex] } };
    }
    
    public int RunTableMenu(List<ReservationModel> tables, string prompt, bool printPrompt = true, bool sideways = false, bool displayTime = false)
    {
        ConsoleKey keyPressed;
        Console.Clear();
        AddForbiddenIndexes(tables);
        do
        {
            Console.SetCursorPosition(0, 0);
            DisplayTableOptions(tables, prompt, true);
            ConsoleKeyInfo selectedKey = Console.ReadKey(true);
            keyPressed = selectedKey.Key;
            if (sideways)
            {
                switch (keyPressed)
                {
                    case ConsoleKey.LeftArrow:
                        _currentIndex--;
                        if (forbiddenIndex.Contains(_currentIndex)) _currentIndex--;
                        if (_currentIndex <= -1) _currentIndex = tables.Count - 1;
                        break;
                    case ConsoleKey.RightArrow:
                        _currentIndex++;
                        if (_currentIndex == tables.Count) _currentIndex = 0;
                        if (forbiddenIndex.Contains(_currentIndex)) _currentIndex++;
                        break;
                }
            }
            else
            {
                switch (keyPressed)
                {
                    case ConsoleKey.UpArrow:
                        _currentIndex--;
                        if (forbiddenIndex.Contains(_currentIndex)) _currentIndex--;
                        if (_currentIndex <= -1) _currentIndex = tables.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        _currentIndex++;
                        if (_currentIndex == tables.Count) _currentIndex = 0;
                        if (forbiddenIndex.Contains(_currentIndex)) _currentIndex++;
                        break;
                }
            }
        } while (keyPressed != ConsoleKey.Enter);
        
        return _currentIndex;
    }

    public void AddForbiddenIndexes(List<ReservationModel> tables)
    {
        try
        {
            forbiddenIndex.Add(0);
            for (int i = 0; i < tables.Count(); i++)
            {
                if (tables[i] != null)
                {
                    if (tables[i].isReserved)
                    {
                        forbiddenIndex.Add(i);
                    }
                }
                else
                {
                    forbiddenIndex.Add(i);
                }
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            
        }
    }
}