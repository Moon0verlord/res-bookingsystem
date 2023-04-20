static class InfoBoxes
{
    public static int origCol = 0;
    public static int origRow = 0;
    //this writes the tutorial box that explains to the user what all the terms behind the tables mean.
    public static void WriteBoxReservations(int groupsize)
    {
        string msg1 = " Beschikbaar: Deze tafel is nog niet gereserveerd en is gepast voor uw groepsgrootte.";
        string msg2 = $" Onbeschikbaar: Deze tafel is ongepast voor uw groepsgrootte ({groupsize}).";
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