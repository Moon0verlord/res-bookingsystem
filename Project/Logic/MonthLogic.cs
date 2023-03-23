class MonthLogic
{
    public void Month(int month)
    {
        
        Start(month);
    }
    
    public static void Start(int month)
        {
            var current_days = DateTime.DaysInMonth(DateTime.Now.Year, month);
            var dayArray = new string[current_days-DateTime.Today.Day+1];
            for (int runs = DateTime.Today.Day; runs <= current_days; runs++)
            {
                if(runs>=DateTime.Today.Day)
                {
                    dayArray[runs-(int)DateTime.Today.Day] = runs.ToString();
                }
            
            }
            MonthDayLogic menu = new MonthDayLogic();
            if (month == DateTime.Today.Month)
            {
                menu.RunMenu(dayArray, $"{DayConvert((int)DateTime.Today.DayOfWeek)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 1)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 2)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 3)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 4)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 5)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 6)}\t");
            }
            else
            {
                menu.RunMenu(dayArray, $"{monthConvert(DateTime.Today.Year,month, DateTime.DaysInMonth(DateTime.Today.Year, month))}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 1)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 2)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 3)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 4)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 5)}\t" +
                                       $"{DayConvert((int)DateTime.Today.DayOfWeek + 6)}\t");
            }
        }

        public static string DayConvert(int value)
        {
            var day = Enum.GetName(typeof(DayOfWeek), value % 7);
            day = day.Substring(0, 3);
            return day;
        }
        public static string monthConvert(int year,int month,int days=31)
        {
            DateTime now = new DateTime(year,month,days);
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            return startDate.DayOfWeek.ToString();
        }
    
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
                if (i%7 == 0)
                {
                    Console.WriteLine();
                }
                
                if (i == _currentIndex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.BackgroundColor = ConsoleColor.Gray;
                        
                    Console.Write(_options[i]+"\t");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(_options[i]+"\t");
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