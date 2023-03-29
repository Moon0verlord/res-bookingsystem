using System.Globalization;
class ReservationsLogic
{
    public static readonly string CurMonth = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
    
    // empty constructor to call CurMonth
    public ReservationsLogic() {}

    public List<DateTime> PopulateDates()
    {
        var thisWeek = new List<DateTime>();
        for (int i = 0; i <= 6; i++)
        {
            thisWeek.Add(DateTime.Today.AddDays(i));
        }

        return thisWeek;
    }
}