using System.Globalization;
class ReservationsLogic
{
    private static readonly int CurMonth = DateTime.UtcNow.Date.Month;
    
    private static readonly MenuLogic _myMenu = new MenuLogic();

    public static void SwitchMonthMethod(string[]Prompt,string Month)
    {
        if (Month == "Go Back")
        {
            MainMenu.Start();
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
        var options = new string[2];
        options[0] = DateTimeFormatInfo.CurrentInfo.GetMonthName(CurMonth);
            options[^1] = "Go Back";
            string prompt = $"make a reservation for this month: \n{DateTime.UtcNow.Date.Year}";
            while (true)
            {
                int input = _myMenu.RunMenu(options, prompt);
                switch (input)
                {
                    case 0:
                        SwitchMonthMethod(options, options[input]);
                        break;
                    case 1:
                        SwitchMonthMethod(options, options[input]);
                        break;
                }
            }
    }
}