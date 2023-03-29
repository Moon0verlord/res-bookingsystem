using System.ComponentModel.Design;
using System.Globalization;
static class Reservation
{
    private static MenuLogic _myMenu = new MenuLogic();
    private static readonly ReservationsLogic Reservations = new ReservationsLogic();
    public static void ResStart(AccountModel acc = null)
    {
        Console.Clear();
        if (acc == null)
        {
            Console.Write("\nPlease enter your email to make a reservation: ");
            string email = Console.ReadLine()!;
            ResMenu(email);  
        }
        else
        {
            ResMenu(acc.EmailAddress);
        }
        //show available hours on requested date
        //User can select an hour
        //back to menu
    }

    public static void ResMenu(string email)
    {
        Console.Clear();
        Console.WriteLine($"You can only make reservations for the current month ({ReservationsLogic.CurMonth})\nPick a day of the week: \n");
        var thisWeek = Reservations.PopulateDates();
        foreach (DateTime date in thisWeek)
        {
            Console.Write($"{date.ToString("ddd", CultureInfo.InvariantCulture)}\t");
        }
        var dictChoice = _myMenu.RunMenu(thisWeek, "", false);
        ChooseTime(dictChoice);
    }

    public static void ChooseTime(Dictionary<int, DateTime> dictChoice)
    {
        
    }
}