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
            string email = null;
            bool loop = true;
            while (loop)
            {
                Console.Clear();
                string prompt = "Vul hier uw e-mail in om een reservatie te maken.";
                string[] options = { $"Vul hier uw e-mail in" + (email == null ? "\n" : $": {email}\n"), "Doorgaan", "Afsluiten" };
                int selectedIndex = _myMenu.RunMenu(options, prompt);
                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        Console.Write("\n Vul hier uw e-mail in: ");
                        email = Console.ReadLine()!;
                        if (!email.Contains("@") || email.Length < 3)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nOnjuiste email.\nEmail moet minimaal een @ hebben en 3 tekens lang zijn.");
                            Console.ResetColor();
                            email = null;
                            Thread.Sleep(3000);
                        }
                        break;
                    case 1:
                        if (email != null)
                        {
                            loop = false;
                            ResMenu(email);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\n vul hier uw e-mail in: ");
                            Thread.Sleep(1800);
                            Console.ResetColor();
                        }
                        break;
                    case 2:
                        MainMenu.Start();
                        break;
                }
            }
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
        Console.WriteLine($"U kunt alleen een reservatie maken voor de hudige maand ({ReservationsLogic.CurMonth})\nKies een dag van de week: \n");
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
        Console.WriteLine($"Email:{email}\nReservatie tafel nummer: {chosenTable}\nDatum: {res_Date.Date.ToString("dd-MM-yyyy")}" +
                          $"\nTijd: {res_Date.TimeOfDay.ToString("hh\\:mm")}\nWeet u zeker dat u deze tijd wil reserveren? (j/n): ");
        Console.ResetColor();
        string answer = Console.ReadLine()!;
        switch (answer.ToLower())
        {
            case "j":
                Reservations.CreateReservation(email, res_Date, chosenTable);
                Console.Clear();
                Console.WriteLine("\nReservatie is gemaakt.");
                Thread.Sleep(1500);
                break;
        }
    }

    public static DateTime ChooseTime(Dictionary<int, DateTime> dictChoice)
    {
        Console.Clear();
        string prompt = "Kies hier een tijd voor de geselcteerde datum " +
                          $"({dictChoice.Select(i => i.Value).FirstOrDefault().ToString("dd-MM-yyyy")})";
        var timeList = Reservations.PopulateTimes();
        int selectIndex = _myMenu.RunMenu(timeList.Select(i => i.ToString("hh\\:mm")).ToArray(), prompt, sideways: true, displayTime: true);
        DateTime res_Date = dictChoice.Select(i => i.Value).FirstOrDefault().Date + timeList[selectIndex];
        return res_Date;
    }

    public static int ChooseTable(DateTime res_Date)
    {
        var tablesOnly = Reservations.PopulateTables(res_Date);
        int selectedTable = _myMenu.RunTableMenu(tablesOnly, "", false);
        return selectedTable;
    }
}