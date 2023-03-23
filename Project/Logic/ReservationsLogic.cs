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
        var options = new string[2];
            var counter = 0;
            options[counter] = DateTimeFormatInfo.CurrentInfo.GetMonthName(cur_month);
            options[^1] = "Go Back";
            string prompt = $"make a reservation for one these months: \n{DateTime.UtcNow.Date.Year}";
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