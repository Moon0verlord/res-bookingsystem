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
        // these are needed to set each table's maximum allowed size of 2, 4 and 6.
        // Starts at -1 so that the first if statement makes the index start at 0.
        List<int> CurrentTableSizes = new List<int>() { 2, 4, 6 };
        int tableIndex = -1;
        for (int i = 1; i <= 15; i++)
        {
            string ID = (i < 9 ? $"{i}S" : i < 14 ? $"{i - 8}M" : $"{i - 13}L");
            IEnumerable<ReservationModel> tablesWithThisID = reservedTables.Where(res => res.Id.Equals(i)&&res.Date==res_Date);
            if (i == 1 || i == 9 || i == 14)
            {
                tableIndex++;
            }
            if (tablesWithThisID.Count() >= 1)
            {
                foreach (ReservationModel table in tablesWithThisID)
                {
                    if (table.Date == res_Date.Date)
                    {
                        if (table.StartTime >= chosenTime.Item1 && table.LeaveTime <= chosenTime.Item2)
                        {
                            table.isReserved = true;
                            table.TableSize = CurrentTableSizes[tableIndex];
                            tablesToAdd.Add(table);
                        }
                        else
                        {
                            bool noDuplicates = true;
                            foreach (ReservationModel check in tablesToAdd)
                            {
                                if (check != null)
                                {
                                    if (check.Id == ID)
                                        noDuplicates = false;
                                }
                            }
                            if (noDuplicates)
                            {
                                int size = CurrentTableSizes[tableIndex];
                                var AddTable = AddDefaultTable(ID, size);
                                tablesToAdd.Add(AddTable);
                                
                            }
                        }
                    }
                    else 
                    {
                        int size = CurrentTableSizes[tableIndex];
                        tablesToAdd.Add(AddDefaultTable(ID, size));
                    }
                }
            }
            else
            {
                int size = CurrentTableSizes[tableIndex];
                tablesToAdd.Add(AddDefaultTable(ID, size));
            }
        }
        return tablesToAdd;
    }
    
    public ReservationModel[,] PopulateTables2D(DateTime res_Date, (TimeSpan, TimeSpan) chosenTime)
    {
        int rowCount = -1;
        int columnCount = -1;
        int tableIndex = -1;
        List<ReservationModel> reservedTables = AccountsAccess.LoadAllReservations();
        ReservationModel[,] tables2D = new ReservationModel[8, 3];
        // these are needed to set each table's maximum allowed size of 2, 4 and 6.
        // Starts at -1 so that the first if statement makes the index start at 0.
        List<int> CurrentTableSizes = new List<int>() { 2, 4, 6 };
        for (int i = 1; i <= 15; i++)
        {
            // create the ID (1-9S / 1-5M / 1-2L) even within a loop that goes to 15.
            string ID = (i < 9 ? $"{i}S" : i < 14 ? $"{i - 8}M" : $"{i - 13}L");
            IEnumerable<ReservationModel> tablesWithThisID = reservedTables.Where(res => res.Id.Equals(ID)&&res.Date==res_Date);
            if (i == 1 || i == 9 || i == 14)
            {
                tableIndex++;
                rowCount = 0;
                columnCount++;
            }
            if (tablesWithThisID.Count() >= 1)
            {
                foreach (ReservationModel table in tablesWithThisID)
                {
                    if (table.Date == res_Date.Date)
                    {
                        if (table.StartTime >= chosenTime.Item1 && table.LeaveTime <= chosenTime.Item2)
                        {
                            table.isReserved = true;
                            table.TableSize = CurrentTableSizes[tableIndex];
                            tables2D[rowCount, columnCount] = table;
                        }
                        else
                        {
                            bool noDuplicates = true;
                            foreach (ReservationModel check in tables2D)
                            {
                                if (check != null)
                                {
                                    if (check.Id == ID)
                                        noDuplicates = false;
                                }
                            }
                            if (noDuplicates)
                            {
                                int size = CurrentTableSizes[tableIndex];
                                tables2D[rowCount, columnCount] = AddDefaultTable(ID, size);
                                
                            }
                        }
                    }
                    else 
                    {
                        int size = CurrentTableSizes[tableIndex];
                        tables2D[rowCount, columnCount] = AddDefaultTable(ID, size);
                    }
                }
            }
            else
            {
                int size = CurrentTableSizes[tableIndex];
                tables2D[rowCount, columnCount] = AddDefaultTable(ID, size);
            }

            rowCount++;
        }

        return tables2D;
    }

    public ReservationModel AddDefaultTable(string id, int size)
    {
        ReservationModel resm = new ReservationModel(id,null, new DateTime(0), 0, default, default, null, default);
        resm.isReserved = false;
        resm.TableSize = size;
        return resm;
    }


    public void CreateReservation(string email, DateTime res_Date, string chosenTable, int groupsize, TimeSpan entertime, TimeSpan leavetime, string res_id, int course)
    {
        AccountModel User = AccountsAccess.LoadAll().Find(account => email == account.EmailAddress)!;
        if(User!=null)
        {
            ReservationModel newReservation = new ReservationModel(chosenTable, email, res_Date, groupsize, entertime, leavetime, res_id, course);
            EmailLogic.SendEmail(email, res_Date, res_id, entertime, leavetime);
            AccountsAccess.AddReservation(newReservation);
        }
        else
        {
            ReservationModel newReservation = new ReservationModel(chosenTable, email, res_Date, groupsize, entertime, leavetime, res_id, course);
            EmailLogic.SendEmail(email, res_Date, res_id, entertime, leavetime);
            AccountsAccess.AddReservation(newReservation);
        }
        
    }

    public string CreateID()
    {
        Random rand = new();
        int int_ID = rand.Next(100000, 999999);
        if (IDExists(int_ID))
        {
            return CreateID();
        }
        return $"RES-{int_ID}";
    }

    private bool IDExists(int id_num)
    {
        var all_res =  AccountsAccess.LoadAllReservations();
        foreach (ReservationModel res in all_res)
        {
            if (res.Res_ID != null)
            {
                var number = Regex.Match(res.Res_ID, @"[0-9]+");
                if (id_num == Convert.ToInt32(number.Value))
                {
                    return true;
                }   
            }
        }

        return false;
    }

    public static ReservationModel GetReservationById(string res_id)
    {
        var all_res = AccountsAccess.LoadAllReservations();
        foreach (ReservationModel res in all_res)
        {
            if (res.Res_ID != null)
            {
                if (res_id == res.Res_ID)
                {
                    return res;
                }
            }
        }

        return null;
    }
}