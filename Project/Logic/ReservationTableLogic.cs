class ReservationTableLogic
{
    protected static int origRow;
    protected static int origCol;
    private List<ReservationModel> _tables = new List<ReservationModel>();
    private int _groupSize = 0;

    public void TableStart(List<ReservationModel> tables , int amountOfPeople)
        {
            Console.Clear();
            _tables = tables;
            _groupSize = amountOfPeople;
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            int twoColumn = 0;
            int fourColumn = 0;
            int sixColumn = 0;
            // Table for two here, tables 1 - 8S
            for (int i = 0; i < 8; i++)
            {
                CheckIfAvailable(i);
                WriteTableTwo(twoColumn, 0, Convert.ToString(i + 1) + "S");
                twoColumn += 10;
                Console.ResetColor();
            }
            // Table for four here, tables 1 - 5M (or tables 9 - 13 in numerical order)
            for (int i = 0; i < 5; i++)
            {
                // i gets +8 here because every loop starts back at 0 for the table ID's.
                // However the list still expects the 4 table sizes to be at position 8 - 13 not 0 - 4.
                CheckIfAvailable(i + 8);
                WriteTableFour(fourColumn, (i % 2 == 0 ? 4 : 3), Convert.ToString(i + 1) + "M");
                fourColumn += 15;
                Console.ResetColor();
            }
            // tables for six here, tables 1 - 2L (or tables 14 - 15 in numerical order)
            for (int i = 0; i < 2; i++)
            {
                // i gets +13 here because every loop starts back at 0 for the table ID's.
                // However the list still expects the 6 table sizes to be at position 14 - 15 not 0 - 1.
                CheckIfAvailable(i + 13);
                WriteTableSix(sixColumn + 15, 7, Convert.ToString(i + 1) + "L");
                sixColumn += 23;
                Console.ResetColor();
            }
            Console.Write("\n");
        }

        public void CheckIfAvailable(int i)
        {
            if (_tables[i] != null)
            {
                bool groupcheck = (_groupSize - _tables[i].TableSize == 0 || _groupSize - _tables[i].TableSize == -1);
                if (_tables[i].isReserved || !groupcheck) Console.ForegroundColor = ConsoleColor.Red;
                else Console.ForegroundColor = ConsoleColor.Green;
            }
        }

        private void WriteTableFour(int colPlus, int rowPlus, string tableindex)
        {
            origCol += colPlus;
            origRow += rowPlus;
            WriteAt("o", 0, 1);
            WriteAt("o", 0, 2);
            WriteAt("┌", 1, 0);
            WriteAt("│", 1, 1);
            WriteAt("│", 1, 2);
            WriteAt("└", 1, 3);
            WriteAt("─────", 2, 0);
            WriteAt(tableindex, 3, 1);
            WriteAt("─────", 2, 3);
            WriteAt("┐", 6, 0);
            WriteAt("│", 6, 1);
            WriteAt("│", 6, 2);
            WriteAt("┘", 6, 3);
            WriteAt("o", 7, 1);
            WriteAt("o", 7, 2);
            origCol -= colPlus;
            origRow -= rowPlus;
        }

        private void WriteTableSix(int colPlus, int rowPlus, string tableindex)
        {
            origCol += colPlus;
            origRow += rowPlus;
            WriteAt("── ── ── ──", 2, 1);
            WriteAt("o", 4, 1);
            WriteAt("o", 7, 1);
            WriteAt("o", 10, 1);
            WriteAt("┌", 1, 1);
            WriteAt(tableindex, 6, 2);
            WriteAt("┐", 13, 1);
            WriteAt("│", 1, 2);
            WriteAt("│", 13, 2);
            WriteAt("└", 1, 3);
            WriteAt("┘", 13, 3);
            WriteAt("── ── ── ──", 2, 3);
            WriteAt("o", 4, 3);
            WriteAt("o", 7, 3);
            WriteAt("o", 10, 3);
            origCol -= colPlus;
            origRow -= rowPlus;
        }

        private void WriteTableTwo(int colPlus, int rowPlus, string tableindex)
        {
            origCol += colPlus;
            origRow += rowPlus;
            WriteAt("o", 0, 1);
            WriteAt("┌", 1, 0);
            WriteAt("│", 1, 1);
            WriteAt("└", 1, 2);
            WriteAt("─────", 2, 0);
            WriteAt(tableindex, 3, 1);
            WriteAt("─────", 2, 2);
            WriteAt("┐", 6, 0);
            WriteAt("│", 6, 1);
            WriteAt("┘", 6, 2);
            WriteAt("o", 7, 1);
            origCol -= colPlus;
            origRow -= rowPlus;
        }

        protected void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
}