using System.Globalization;
class ReservationsLogic
{
    static private int cur_month = DateTime.UtcNow.Date.Month;
    static private ReservationsLogic reserv = new ReservationsLogic();
    static private MenuLogic _myMenu = new MenuLogic();

    static void SwitchMonthMethod(string[]Prompt,string Month)
    {
        if (Month == "Go Back")
        {
            Menu.Start();
        }
        else
        {
            MonthLogic month = new MonthLogic();
            int monthIndex;
            string desiredMonth = Month;
            string[] MonthNames=CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            monthIndex = Array.IndexOf(MonthNames, desiredMonth)+1;
        
            month.Month(Prompt,monthIndex);
        }
        
        
    }
    public void ReservationsMenu()
    {
        var options = new string[13 - cur_month + 1];
            var counter = 0;
            for (int i = cur_month; i <= 12; i++)
            {
                if (i == 12)
                {
                    options[counter] = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                    options[^1] = "Go Back";
                    counter++;
                }
                else
                {
                    options[counter] = DateTimeFormatInfo.CurrentInfo.GetMonthName(i);
                    counter++;
                }


            }

            string prompt = $"make a reservation for one these months: \n{DateTime.UtcNow.Date.Year}";
            while (true)
            {
            int input = _myMenu.RunMenu(options, prompt);
            //Ask to po how far reservations should be
            // ask times for reservations
            switch (input)
            {
                case 0:
                    SwitchMonthMethod(options,options[input]);
                    break;
                case 1:
                    SwitchMonthMethod(options,options[input]);
                    break;
                
                case 2:
                    SwitchMonthMethod(options,options[input]);
                    break;
                    
                case 3:
                    SwitchMonthMethod(options,options[input]);
                    break;
                    
                case 4:
                    SwitchMonthMethod(options,options[input]);
                    break;
                case 5:
                    SwitchMonthMethod(options,options[input]);
                    break;
                case 6:
                    SwitchMonthMethod(options,options[input]);
                    break;
                case 7:
                    SwitchMonthMethod(options,options[input]);
                    break;
                case 8:
                    SwitchMonthMethod(options,options[input]);
                    break;
                case 9:
                    SwitchMonthMethod(options,options[input]);
                    break;
                case 10:
                    SwitchMonthMethod(options,options[input]);
                    break;
                case 11:
                    SwitchMonthMethod(options,options[input]);
                    break;
                case 12:
                    SwitchMonthMethod(options,options[input]);
                    break;
                
                default:
                    break;

            }
        }
    }
}