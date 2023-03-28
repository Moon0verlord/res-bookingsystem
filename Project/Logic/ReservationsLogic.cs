using System.Globalization;
class ReservationsLogic
{
    private static readonly string CurMonth = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
    
    private static readonly MenuLogic _myMenu = new MenuLogic();
    public void ReservationsMenu(string email)
    {
        Console.Clear();
        Console.WriteLine($"You can only make reservations for the current month ({CurMonth})\nPick a day of the week: \n");
        var thisWeek = PopulateDates();
        foreach (DateTime date in thisWeek)
        {
            Console.Write($"{date.Day.ToString("ddd", CultureInfo.InvariantCulture)}\t");
        }
        _myMenu.RunMenu(thisWeek, "", false);
    }

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