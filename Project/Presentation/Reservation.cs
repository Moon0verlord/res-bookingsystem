using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
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
            string email;
            while (true)
            {
                Console.Clear();
                Console.Write("\nPlease enter your email to make a reservation: ");
                email = Console.ReadLine()!;
                if (!email.Contains("@") || email.Length < 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPlease enter a valid email.");
                    Thread.Sleep(2000);
                    Console.ResetColor();
                }
                else break;
            }
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
        DateTime res_Date = ChooseTime(dictChoice);
        int chosenTable = ChooseTable(res_Date);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Clear();
        Console.WriteLine($"Email:{email}\nReserved table number: {chosenTable}\nDate: {res_Date.Date.ToString("dd-MM-yyyy")}" +
                          $"\nTime: {res_Date.TimeOfDay.ToString("hh\\:mm")}\nAre you sure you want to reserve this date? (y/n): ");
        Console.ResetColor();
        string answer = Console.ReadLine()!;
        switch (answer)
        {
            case "y": 
            case "Y":
                Reservations.CreateReservation(email, res_Date, chosenTable);
                Console.Clear();
                Console.WriteLine("\nReservation has been made.");
                Thread.Sleep(1500);
                break;
        }
    }

    public static DateTime ChooseTime(Dictionary<int, DateTime> dictChoice)
    {
        Console.Clear();
        string prompt = "Please pick a time for your selected date " +
                          $"({dictChoice.Select(i => i.Value).FirstOrDefault().ToString("dd-MM-yyyy")})";
        var timeList = Reservations.PopulateTimes();
        int selectIndex = _myMenu.RunMenu(timeList.Select(i => i.ToString("hh\\:mm")).ToArray(), prompt, sideways: true, displayTime: true);
        DateTime res_Date = dictChoice.Select(i => i.Value).FirstOrDefault().Date + timeList[selectIndex];
        return res_Date;
    }

    public static int ChooseTable(DateTime res_Date)
    {
        //todo: remove test comments
        var tablesOnly = Reservations.PopulateTables(res_Date);
        int selectedTable = _myMenu.RunTableMenu(tablesOnly, "", false);
        return selectedTable;
    }
}