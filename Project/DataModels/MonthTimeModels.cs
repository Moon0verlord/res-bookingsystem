using System.Globalization;
public abstract class MonthTimeModels
{
    //This class is used to initialized the Times array and dayArray in monthlogic.cs
    // Its here to keep the other code more clean
    //Its als o used to house the following methods: DayConvert and firstdaymonth
    protected static string[] Hours()
    {
        var Times = new string[13];
        var counter = 0;
        for (int hours = 16; hours < 22; hours++)
        {
            for (int minutes = 0; minutes <= 30; minutes += 30)
            {
                Times[counter] = new TimeSpan(hours, minutes, 0).ToString();
                if (hours == 21 && minutes == 30)
                {
                    Times[counter] = new TimeSpan(hours, minutes, 0).ToString();
                    Times[^1] = "Ga terug";
                }
                counter++;
            }
        }
        return Times;
    }

    protected static string[] DaysMonth(int month)
    {
        var current_days = DateTime.DaysInMonth(DateTime.Now.Year, month);
        var dayArray = new string[current_days + 1 - DateTime.Today.Day + 1];
        for (int runs = DateTime.Today.Day; runs <= current_days; runs++)
        {
            if (runs >= DateTime.Today.Day)
            {
                dayArray[runs - DateTime.Today.Day] = runs.ToString();
            }
            if (runs == current_days)
            {
                dayArray[runs - DateTime.Today.Day] = runs.ToString();
                dayArray[^1] = "Ga terug";
            }

        }

        return dayArray;
    }
    //Returns the name of the day of the week
    protected static string DayConvert(int value)
    {
        var day = Enum.GetName(typeof(DayOfWeek), value % 7);
        day = day.Substring(0, 3);
        return day;
    }
    // gets the first day of a month and returns an abbreviated string: Mon Tue etc
    protected static string FirstDayOfMonth(DateTime dt, int day)
    {
        return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(
            (new DateTime(dt.Year, dt.Month, day).DayOfWeek));
    }
}