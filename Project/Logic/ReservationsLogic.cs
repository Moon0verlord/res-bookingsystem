using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Text.RegularExpressions;

public class ReservationsLogic
{
    public static readonly string CurMonth = DateTime.Now.ToString("MMMM", CultureInfo.GetCultureInfo("nl"));
    
    // empty constructor to call CurMonth
    public ReservationsLogic() {}

    public DateTime[,] PopulateDates()
    {
        int rowCount = 0;
        int columnCount = 0;
        // Kijk hoeveel rijen de array nodig heeft door de maand op te delen in weken
        int amountOfRows = (int)Math.Ceiling(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) / 7.0);
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
    
    
    public ReservationModel[,] PopulateTables2D(DateTime resDate, (TimeSpan, TimeSpan) chosenTime)
    {
        int rowCount = -1;
        int columnCount = -1;
        int tableIndex = -1;
        List<ReservationModel> reservedTables = AccountsAccess.LoadAllReservations();
        ReservationModel[,] tables2D = new ReservationModel[8, 3];
        // these are needed to set each table's maximum allowed size of 2, 4 and 6.
        // Starts at -1 so that the first if statement makes the index start at 0.
        List<int> currentTableSizes = new List<int>() { 2, 4, 6 };
        for (int i = 1; i <= 15; i++)
        {
            // create the ID (1-9S / 1-5M / 1-2L) even within a loop that goes to 15.
            string ID = (i < 9 ? $"{i}S" : i < 14 ? $"{i - 8}M" : $"{i - 13}L");
            IEnumerable<ReservationModel> tablesWithThisId = reservedTables.Where(res => res.Id.Equals(ID)&&res.Date==resDate);
            if (i == 1 || i == 9 || i == 14)
            {
                tableIndex++;
                rowCount = 0;
                columnCount++;
            }
            if (tablesWithThisId.Count() >= 1)
            {
                foreach (ReservationModel table in tablesWithThisId)
                {
                    if (table.Date == resDate.Date)
                    {
                        if (table.StartTime >= chosenTime.Item1 && table.LeaveTime <= chosenTime.Item2)
                        {
                            table.IsReserved = true;
                            table.TableSize = currentTableSizes[tableIndex];
                            tables2D[rowCount, columnCount] = table;
                        }
                        else
                        {
                            bool noDuplicates = true;
                            foreach (ReservationModel check in tables2D)
                            {
                                if (check != null!)
                                {
                                    if (check.Id == ID)
                                        noDuplicates = false;
                                }
                            }
                            if (noDuplicates)
                            {
                                int size = currentTableSizes[tableIndex];
                                tables2D[rowCount, columnCount] = AddDefaultTable(ID, size);
                                
                            }
                        }
                    }
                    else 
                    {
                        int size = currentTableSizes[tableIndex];
                        tables2D[rowCount, columnCount] = AddDefaultTable(ID, size);
                    }
                }
            }
            else
            {
                int size = currentTableSizes[tableIndex];
                tables2D[rowCount, columnCount] = AddDefaultTable(ID, size);
            }

            rowCount++;
        }

        return tables2D;
    }
   
    public ReservationModel AddDefaultTable(string id, int size)
    {
        
        ReservationModel resm = new ReservationModel(id,null, new DateTime(0), 0, default, default, null, default);
        resm.IsReserved = false;
        resm.TableSize = size;
        return resm;
    }


    public void CreateReservation(string? email, DateTime resDate, string chosenTable, int groupsize, TimeSpan entertime, TimeSpan leavetime, string resId, int course)
    {
        AccountModel user = AccountsAccess.LoadAll().Find(account => email == account.EmailAddress)!;
        if(user!=null!)
        {
            ReservationModel newReservation = new ReservationModel(chosenTable, email, resDate, groupsize, entertime, leavetime, resId, course);
            EmailLogic.SendEmail(email, resDate, resId, entertime, leavetime);
            AccountsAccess.AddReservation(newReservation);
        }
        else
        {
            ReservationModel newReservation = new ReservationModel(chosenTable, email, resDate, groupsize, entertime, leavetime, resId, course);
            EmailLogic.SendEmail(email, resDate, resId, entertime, leavetime);
            AccountsAccess.AddReservation(newReservation);
        }
        
    }

    public string CreateId()
    {
        Random rand = new();
        int intId;
        do
        {
            intId = rand.Next(100000, 999999);
        } while (IDExists(intId));
        return $"RES-{intId}";
    }

    private bool IDExists(int idNum)
    {
        var allRes = (from res in AccountsAccess.LoadAllReservations()
            where idNum == Convert.ToInt32(Regex.Match(res.ResId, @"[0-9]+").Value)
            select res).ToList();
        return allRes.Count > 0;
    }

    public static ReservationModel GetReservationById(string resId)
    {
        var GetRes = (from res in AccountsAccess.LoadAllReservations() where res.ResId == resId select res).ToList();
        if (GetRes.Count > 0)
        {
            return GetRes[0];
        }
        return null!;
    }
}