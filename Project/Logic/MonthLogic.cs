class MonthLogic:MonthTimeModels
{
    private static ReservationsLogic _reserv = new ReservationsLogic();
    
    public void Month(string[] prompt,int month)
    {
        if (month <= prompt.Length&&prompt[month] == "Go Back")
        {
            MainMenu.Start();
        }
        else
        {
            Start(month);
        }
    }
    
    private void Start(int month)
    {
            var dayArray = MonthTimeModels.DaysMonth(month);
            var times = MonthTimeModels.Hours();
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
                            Reservation.ResMenu(null);
                        }
                        else
                        {
                            int dayInput = menu.RunMenu(times.ToArray(),dayArray[input]);
                            
                                switch (times[dayInput])
                                {
                                    case "Go Back":
                                        Start(month);
                                        break;
                                    default:
                                        TableLogic.Start();
                                        foreach (var item in TableLogic.isTableFree)
                                        {
                                            foreach (var items in item.Value)
                                            {
                                                Console.WriteLine(item.Key+"\t "+items.Key+":"+ (items.Value?"Free":"Occupied"));
                                               
                                            }
                                        }
                                        Thread.Sleep(10000);
                                        break;
                                }
                            
                        }
                        
                    }
                }

            }
    }
    class MonthDayLogic
    {

        private int _currentIndex;
        private string[] _options = null;

        private void DisplayOptions(string prompt, bool printPrompt)
        {
            if (printPrompt) Console.WriteLine(prompt);
            for (var i = 0; i < _options.Length; i++)
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