using System.Globalization;

class MonthLogic
{
    public void Month(string[]Prompt,int month)
    {
        if (month <= Prompt.Length&&Prompt[month] == "Go Back")
        {
            Menu.Start();
        }
        else
        {
            Start(month);
        }
    }
    
    public static void Start(int month)
        {
            var current_days = DateTime.DaysInMonth(DateTime.Now.Year, month);
            var dayArray = new string[current_days-DateTime.Today.Day+1];
            for (int runs = DateTime.Today.Day; runs <= current_days; runs++)
            {
                if(runs>=DateTime.Today.Day)
                {
                    dayArray[runs-DateTime.Today.Day] = runs.ToString();
                }
            
            }
            MonthDayLogic menu = new MonthDayLogic();
            if (month == DateTime.Today.Month)
            {
                menu.RunMenu(dayArray, $"{DayConvert((int)DateTime.Today.DayOfWeek) }\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 1)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 2)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 3)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 4)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 5)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 6)}\t");
            }
            else
            {
                DateTime date = new DateTime(2023, month, DateTime.DaysInMonth(2023, month));
                menu.RunMenu(dayArray, $"{FirstDayOfMonth(date,1)}\t" +
                                       $"{FirstDayOfMonth(date, 2)}\t" +
                                       $"{FirstDayOfMonth(date, 3)}\t" +
                                       $"{FirstDayOfMonth(date, 4)}\t" +
                                       $"{FirstDayOfMonth(date, 5)}\t" +
                                       $"{FirstDayOfMonth(date, 6)}\t"+
                                       $"{FirstDayOfMonth(date, 7)}\t");

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
            if (printPrompt) Console.Write(prompt);
            for (int i = 0; i < _options.Length; i++)
            {
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