﻿public class MenuLogic
{
    private int _currentIndex = 0;
    private string[] _options = null;
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

    public int RunMenu(string[] options, string prompt, bool printPrompt = true)
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