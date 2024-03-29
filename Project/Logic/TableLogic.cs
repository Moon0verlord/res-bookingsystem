class TableLogic : IMenuLogic
{
    public static IDictionary<int, Dictionary<int, bool>> isTableFree = new Dictionary<int, Dictionary<int, bool>>();

    public void occupyTable(int tableNumber)
    {
        isTableFree[1][tableNumber] = false;
    }

    public void freeTable(int tableNumber)
    {
        isTableFree[1][tableNumber] = true;
    }

    public TableLogic()
    {
        for (int i = 1; i < 9; i++)
        {
            if (!isTableFree.ContainsKey(i))
            {
                isTableFree.Add(i, new Dictionary<int, bool>());
                isTableFree[i].Add(2, true);
            }
        }
        for (int j = 9; j < 14; j++)
        {
            if (!isTableFree.ContainsKey(j))
            {
                isTableFree.Add(j, new Dictionary<int, bool>());
                isTableFree[j].Add(4, true);
            }
        }
        for (int k = 14; k < 16; k++)
        {
            if (!isTableFree.ContainsKey(k))
            {
                isTableFree.Add(k, new Dictionary<int, bool>());
                isTableFree[k].Add(6, true);
            }
        }
    }

    public static void ShowTable()
    {
        bool firstTable2 = true;
        bool firstTable4 = true;
        bool firstTable6 = true;
        Console.Clear();
        Console.WriteLine("Tafel\tStatus");
        for (int i = 1; i <= isTableFree.Count; i++)
            foreach (var table in isTableFree[i])
            {
                if (table.Key == 2)
                {
                    if (firstTable2)
                    {
                        Console.WriteLine("Tafel voor 2");
                        firstTable2 = false;
                    }
                    if (table.Value)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(i + "\t" + (table.Value ? "Beschikbaar" : "Bezet"));
                    Console.ResetColor();
                }
                else if (table.Key == 4)
                {
                    if (firstTable4)
                    {
                        Console.WriteLine("Tafel voor 4");
                        firstTable4 = false;
                    }
                    if (table.Value)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(i + "\t" + (table.Value ? "Beschikbaar" : "Bezet"));
                    Console.ResetColor();
                }
                else
                {
                    if (firstTable6)
                    {
                        Console.WriteLine("tafel voor 6");
                        firstTable6 = false;
                    }
                    if (table.Value)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(i + "\t" + (table.Value ? "Beschikbaar" : "Bezet"));
                    Console.ResetColor();
                }
            }
    }

    public static TableLogic Start()
    {
        TableLogic table = new TableLogic();
        return table;
    }
}