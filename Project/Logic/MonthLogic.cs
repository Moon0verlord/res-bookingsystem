using System.Globalization;

class MonthLogic
{
    
    static private ReservationsLogic reserv = new ReservationsLogic();
    static private MenuLogic _myMenu = new MenuLogic();
    

    public void Month(string[]Prompt,int month)
    {
        if (month <= Prompt.Length&&Prompt[month] == "Go Back")
        {
            MainMenu.Start();
        }
        else
        {
            Start(month);
        }
    }
    
    public void Start(int month)
    {
            var dayArray = MonthTimeModels.DaysMonth(month);
            var Times = MonthTimeModels.Hours();
            MonthDayLogic menu = new MonthDayLogic();
            if (month == DateTime.Today.Month)
            {
                while (true)
                {
                    {
                        int input = menu.RunMenu(dayArray, $"{DayConvert((int)DateTime.Today.DayOfWeek)}\t" +
                                                           $"{DayConvert((int)DateTime.Today.DayOfWeek + 1)}\t" +
                                                           $"{DayConvert((int)DateTime.Today.DayOfWeek + 2)}\t" +
                                                           $"{DayConvert((int)DateTime.Today.DayOfWeek + 3)}\t" +
                                                           $"{DayConvert((int)DateTime.Today.DayOfWeek + 4)}\t" +
                                                           $"{DayConvert((int)DateTime.Today.DayOfWeek + 5)}\t" +
                                                           $"{DayConvert((int)DateTime.Today.DayOfWeek + 6)}\t");

                        if (dayArray[input] == "Go Back")
                        {
                            reserv.ReservationsMenu();
                        }
                        else
                        {
                            int dayInput = menu.RunMenu(Times.ToArray(),dayArray[input]);
                            
                                switch (Times[dayInput])
                                {
                                    case "Go Back":
                                        Start(month);
                                        break;
                                    default:
                                        Console.WriteLine(Times[dayInput]);
                                        Thread.Sleep(1000);
                                        
                                        Start(month);
                                        break;
                                }
                            
                        }
                        
                    }
                }

            }
    }
    public static string DayConvert(int value)
        {
            var day = Enum.GetName(typeof(DayOfWeek), value % 7);
            day = day.Substring(0, 3);
            return day;
        }
    public static string FirstDayOfMonth(DateTime dt,int day)
    {
        return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(
            (new DateTime(dt.Year, dt.Month, day).DayOfWeek));

    }

    class MonthDayLogic
    {

        private int _currentIndex = 0;
        private string[] _options = null;

        public void DisplayOptions(string prompt, bool printPrompt)
        {
            if (printPrompt) Console.WriteLine(prompt);
            for (int i = 0; i < _options.Length; i++)
            {
                if (i % 7 == 0)
                {
                    Console.WriteLine();
                }
                if (i == _currentIndex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.BackgroundColor = ConsoleColor.Gray;

                    Console.Write(_options[i] + "\t");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(_options[i] + "\t");
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
                    case ConsoleKey.LeftArrow:
                        _currentIndex--;
                        if (_currentIndex <= -1) _currentIndex = _options.Length - 1;
                        break;
                    case ConsoleKey.RightArrow:
                        _currentIndex++;
                        if (_currentIndex == _options.Length) _currentIndex = 0;
                        break;
                }

            } while (keyPressed != ConsoleKey.Enter);

            return _currentIndex;
        }

    }
}