static class InfoBoxes
{
    public static int origRow = 0;
    public static int origCol = 0;
    //this writes the tutorial box that explains to the user what all the terms behind the tables mean.
    public static void WriteBoxReservations(int groupsize, int origrow, int origcol)
    {
        origRow = origrow;
        origCol = origcol;
        string msg1 = " Beschikbaar: Deze tafel is nog niet gereserveerd en is gepast voor uw groepsgrootte.";
        string msg2 = $" Onbeschikbaar: Deze tafel is ongepast voor uw groepsgrootte ({groupsize}).";
        string msg3 = " Bezet: Deze tafel is al gereserveerd door een andere klant.";
        string boxBorder = "──────────────────────────────────────────────────────────────────────────────────────";
        int x = 0;
        int y = 23;
        Console.ForegroundColor = ConsoleColor.Cyan;
        WriteAt("┌", x, y);
        WriteAt("│", x, y + 1);
        WriteAt("│", x, y + 2);
        WriteAt("│", x, y + 3);
        WriteAt("└", x, y + 4);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 4);
        WriteAt("┐", x + boxBorder.Length, y);
        WriteAt("│", x + boxBorder.Length, y + 1);
        WriteAt("│", x + boxBorder.Length, y + 2);
        WriteAt("│", x + boxBorder.Length, y + 3);
        WriteAt("┘", x + boxBorder.Length, y + 4);
        Console.ForegroundColor = ConsoleColor.Green;
        WriteAt(msg1, x + 1, y + 1);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        WriteAt(msg2, x + 1, y + 2);
        Console.ForegroundColor = ConsoleColor.Red;
        WriteAt(msg3, x + 1, y + 3);
        Console.ResetColor();
    }
    
    public static void WriteBoxUserEmail(int origrow, int origcol)
    {
        origRow = origrow;
        origCol = origcol;
        int x = 1;
        int y = 3;
        string boxBorder = "───────────────────────────────";
        string msg2 = $"Requirements wachtwoord:\n- 8-15 karakters lang\n- Minimaal 1 hoofdletter\n- Minimaal 1 cijfer";
        Console.ForegroundColor = ConsoleColor.Cyan;
        WriteAt("┌", x, y);
        WriteAt("│", x, y + 1);
        WriteAt("│", x, y + 2);
        WriteAt("│", x, y + 3);
        WriteAt("│", x, y + 4);
        WriteAt("└", x, y + 5);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 5);
        WriteAt("┐", boxBorder.Length + 2, y);
        WriteAt("│", boxBorder.Length + 2, y + 1);
        WriteAt("│", boxBorder.Length + 2, y + 2);
        WriteAt("│", boxBorder.Length + 2, y + 3);
        WriteAt("│", boxBorder.Length + 2, y + 4);
        WriteAt("┘", boxBorder.Length + 2, y + 5);
        Console.ForegroundColor = ConsoleColor.Green;
        WriteAt(" Requirements E-mail:", x + 1, y + 1);
        WriteAt("- Een '@' teken", x + 1, y + 2);
        WriteAt("- Langer dan 3 karakters.", x + 1, y + 3);
        WriteAt("- Begint en eindigt met letter", x + 1, y + 4);
        Console.ResetColor();
    }
    
    public static void WriteBoxUserPassword(int origrow, int origcol)
    {
        origRow = origrow;
        origCol = origcol;
        int x = 1;
        int y = 5;
        string boxBorder = "───────────────────────────────";
        Console.ForegroundColor = ConsoleColor.Cyan;
        WriteAt("┌", x, y);
        WriteAt("│", x, y + 1);
        WriteAt("│", x, y + 2);
        WriteAt("│", x, y + 3);
        WriteAt("│", x, y + 4);
        WriteAt("└", x, y + 5);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 5);
        WriteAt("┐", boxBorder.Length + 2, y);
        WriteAt("│", boxBorder.Length + 2, y + 1);
        WriteAt("│", boxBorder.Length + 2, y + 2);
        WriteAt("│", boxBorder.Length + 2, y + 3);
        WriteAt("│", boxBorder.Length + 2, y + 4);
        WriteAt("┘", boxBorder.Length + 2, y + 5);
        Console.ForegroundColor = ConsoleColor.Green;
        WriteAt(" Requirements wachtwoord:", x + 1, y + 1);
        WriteAt("- 8 - 15 karakters",  x + 1, y + 2);
        WriteAt("- Minimaal 1 hoofdletter", x + 1, y + 3);
        WriteAt("- Minimaal 1 cijfer", x + 1,  y + 4);
        Console.ResetColor();
    }

    public static void WriteBoxStepCounter(int origrow, int origcol, int stepcount)
    {
        origRow = origrow;
        origCol = origcol;
        int x = 4;
        int y = 0;
        string boxBorder = "───────────────";
        Console.ForegroundColor = ConsoleColor.Green;
        WriteAt("┌", x, y);
        WriteAt("│", x, y + 1);
        WriteAt("└", x, y + 2);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 2);
        WriteAt("┐", x + boxBorder.Length + 1, y);
        WriteAt("│", x + boxBorder.Length + 1, y + 1);
        WriteAt("┘", x + boxBorder.Length + 1, y + 2);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        WriteAt($"  Stap {stepcount} / 7   ", x + 1, y + 1);
        Console.ResetColor();
    }
    
    private static void WriteAt(string s, int x, int y)
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