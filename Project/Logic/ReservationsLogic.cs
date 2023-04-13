using System.ComponentModel.Design.Serialization;
using System.Globalization;
class ReservationsLogic
{
    public static readonly string CurMonth = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
    
    // empty constructor to call CurMonth
    public ReservationsLogic() {}

    public DateTime[,] PopulateDates()
    {
        int rowCount = 0;
        int columnCount = 0;
        // Kijk hoeveel rijen de array nodig heeft door de maand op te delen in weken
        int amountOfRows = (int)DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) / 7;
        DateTime[,] dates = new DateTime[amountOfRows,7];
        DateTime currentDate = DateTime.Today;
        do
        {
            if (currentDate.Date != DateTime.MinValue) dates[rowCount, columnCount] = currentDate.Date;
            columnCount++;
            if (columnCount % 7 == 0)
            {
                rowCount++;
                columnCount = 0;
            }
            currentDate = currentDate.AddDays(1);
        } while (currentDate.Month == DateTime.Today.Month);

        return dates;
    }
    
    public List<ReservationModel> PopulateTables(DateTime res_Date, (TimeSpan, TimeSpan) chosenTime)
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
                    if (table.Date == res_Date.Date)
                    {
                        if (table.StartTime >= chosenTime.Item1 && table.LeaveTime <= chosenTime.Item2)
                            table.isReserved = true;
                            tablesToAdd.Add(table);
                    }
                    else
                    {
                        ReservationModel resm = new ReservationModel(i, null, new DateTime(0), 0, default, default);
                        resm.isReserved = false;
                        tablesToAdd.Add(resm);
                    }
                }
            }
            else
            {
                ReservationModel resm = new ReservationModel(i, null, new DateTime(0), 0, default, default);
                resm.isReserved = false;
                tablesToAdd.Add(resm);
            }
        }
        return tablesToAdd;
    }


    public void CreateReservation(string email, DateTime res_Date, int chosenTable, int groupsize, TimeSpan entertime, TimeSpan leavetime)
    {
        ReservationModel newReservation = new ReservationModel(chosenTable, email, res_Date, groupsize, entertime, leavetime);
        AccountsAccess.AddReservation(newReservation);
    }
}