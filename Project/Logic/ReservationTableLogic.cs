class ReservationTableLogic
{
    protected static int origRow;
    protected static int origCol;
    private ReservationModel[,] _tables = null;
    private int _groupSize = 0;

    public void TableStart(ReservationModel[,] tables , int amountOfPeople)
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
                CheckIfAvailable(Convert.ToString(i + 1) + "S");
                WriteTableTwo(twoColumn, 0, Convert.ToString(i + 1) + "S");
                twoColumn += 10;
                Console.ResetColor();
            }
            // Table for four here, tables 1 - 5M (or tables 9 - 13 in numerical order)
            for (int i = 0; i < 5; i++)
            {
                // i gets +8 here because every loop starts back at 0 for the table ID's.
                // However the list still expects the 4 table sizes to be at position 8 - 13 not 0 - 4.
                CheckIfAvailable(Convert.ToString(i + 1) + "M");
                WriteTableFour(fourColumn, (i % 2 == 0 ? 4 : 3), Convert.ToString(i + 1) + "M");
                fourColumn += 15;
                Console.ResetColor();
            }
            // tables for six here, tables 1 - 2L (or tables 14 - 15 in numerical order)
            for (int i = 0; i < 2; i++)
            {
                // i gets +13 here because every loop starts back at 0 for the table ID's.
                // However the list still expects the 6 table sizes to be at position 14 - 15 not 0 - 1.
                CheckIfAvailable(Convert.ToString(i + 1) + "L");
                WriteTableSix(sixColumn + 15, 7, Convert.ToString(i + 1) + "L");
                sixColumn += 23;
                Console.ResetColor();
            }

            WriteTutorialBox();
            Console.Write("\n");
        }

        // this method checks if the given ID is not reserved or unavailable due to group size, and changes foreground color accordingly.
        public void CheckIfAvailable(string id)
        {
            foreach (ReservationModel table in _tables)
            {
                if (table != null)
                {
                    if (table.Id == id)
                    {
                        bool groupcheck = (_groupSize - table.TableSize == 0 || _groupSize - table.TableSize == -1);
                        if (table.isReserved) Console.ForegroundColor = ConsoleColor.Red;
                        else if (!groupcheck) Console.ForegroundColor = ConsoleColor.DarkGray;
                        else Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
            }
        }

        //this writes the tutorial box that explains to the user what all the terms behind the tables mean.
        public void WriteTutorialBox()
        {
            string msg1 = " Beschikbaar: Deze tafel is nog niet gereserveerd en is gepast voor uw groepsgrootte.";
            string msg2 = $" Onbeschikbaar: Deze tafel is ongepast voor uw groepsgrootte ({_groupSize}).";
            string msg3 = " Bezet: Deze tafel is al gereserveerd door een andere klant.";
            Console.ForegroundColor = ConsoleColor.Cyan;
            WriteAt("┌", 85, 0);
            WriteAt("│", 85, 1);
            WriteAt("│", 85, 2);
            WriteAt("│", 85, 3);
            WriteAt("└", 85, 4);
            WriteAt("──────────────────────────────────────────────────────────────────────────────────────", 86, 0);
            WriteAt("──────────────────────────────────────────────────────────────────────────────────────", 86, 4);
            WriteAt("┐", 171, 0);
            WriteAt("│", 171, 1);
            WriteAt("│", 171, 2);
            WriteAt("│", 171, 3);
            WriteAt("┘", 171, 4);
            Console.ForegroundColor = ConsoleColor.Green;
            WriteAt(msg1, 86, 1);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            WriteAt(msg2, 86, 2);
            Console.ForegroundColor = ConsoleColor.Red;
            WriteAt(msg3, 86, 3);
            Console.ResetColor();
        }

        // write the table for four string
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

        // write the table for six string
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

        // write the table for two string
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

        // this method writes at the given coordinates so you can have more control over writing locations in the console.
        // which would otherwise not be possible using /n etc.
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