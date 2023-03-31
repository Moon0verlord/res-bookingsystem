using System.ComponentModel.Design.Serialization;
using System.Globalization;
class ReservationsLogic
{
    public static readonly string CurMonth = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
    
    // empty constructor to call CurMonth
    public ReservationsLogic() {}

    public List<DateTime> PopulateDates()
    {
        var thisWeek = new List<DateTime>();
        var DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        for (int i = DateTime.Now.Day; i <= DaysInMonth; i++)
        {
            if (i == DaysInMonth)
            {
                thisWeek.Add(new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                    DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)));
            }
            else
            {
                thisWeek.Add(DateTime.Today.AddDays(i));
            }
        }
        return thisWeek;
    }

    public List<TimeSpan> PopulateTimes()
    {
        var timeList = new List<TimeSpan>();
        for (int hours = 16; hours < 22; hours++)
        {
            for(int minutes = 0;minutes<=30;minutes+=30)
            {
                timeList.Add(new TimeSpan(hours, minutes, 0));
                if(hours == 21 && minutes == 30)
                {
                    break;
                }
            }
        }
        
        return timeList;
    }

    public List<ReservationModel> PopulateTables(DateTime res_Date)
    {
        List<ReservationModel> reservedTables = AccountsAccess.LoadAllReservations();
        List<ReservationModel> tablesToAdd = new List<ReservationModel>();

        for (int i = 1; i <= 15; i++)
        {
            IEnumerable<ReservationModel> tablesWithThisID = reservedTables.Where(res => res.Id.Equals(i));
            if (i == 1 || i == 9 || i == 14) tablesToAdd.Add(null);
            if (tablesWithThisID.Count() >= 1)
            {
                foreach (ReservationModel table in tablesWithThisID)
                {
                    if (table.Date == res_Date)
                    {
                        tablesToAdd.Add(table);
                    }
                    else
                    {
                        ReservationModel resm = new ReservationModel(i, null, new DateTime(0));
                        resm.isReserved = false;
                        tablesToAdd.Add(resm);
                    }
                }
            }
            else
            {
                ReservationModel resm = new ReservationModel(i, null, new DateTime(0));
                resm.isReserved = false;
                tablesToAdd.Add(resm);
            }
        }
        return tablesToAdd;
    }


    public void CreateReservation(string email, DateTime res_Date, int chosenTable)
    {
        ReservationModel newReservation = new ReservationModel(chosenTable, email, res_Date);
        AccountsAccess.AddReservation(newReservation);
    }
}