public class MonthTimeModels
{
    //This class is used to initialized the Times array and dayArray in monthlogic.cs
    // Its here to keep the other code more clean
    public static string[] Hours()
    {
        var Times = new string[13];
        var counter = 0;
        for (int hours = 16; hours < 22; hours++)
        {
            for(int minutes = 0;minutes<=30;minutes+=30)
            {
                Times[counter] = new TimeSpan(hours, minutes, 0).ToString();
                if(hours == 21 && minutes == 30)
                {
                    Times[counter] = new TimeSpan(hours, minutes, 0).ToString();
                    Times[^1] = "Go Back";
                }
                counter++;
            }
                
        }

        return Times;
    }

    public static string[] DaysMonth(int month)
    {
        var current_days = DateTime.DaysInMonth(DateTime.Now.Year, month);
        var dayArray = new string[current_days+1-DateTime.Today.Day+1];
        for (int runs = DateTime.Today.Day; runs <= current_days; runs++)
        {
            if(runs>=DateTime.Today.Day)
            {
                dayArray[runs-DateTime.Today.Day] = runs.ToString();
            }
            if(runs==current_days)
            {
                dayArray[runs-DateTime.Today.Day] = runs.ToString();
                dayArray[^1] = "Go Back";
            }
            
        }

        return dayArray;
    }
}