using System.Net;
using System.Text.RegularExpressions;

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
        for (int i = 1; i <= 3; i++)
            WriteAt("│", x, y + i);
        WriteAt("└", x, y + 4);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 4);
        WriteAt("┐", x + boxBorder.Length, y);
        for (int i = 1; i <= 3; i++)
            WriteAt("│", x + boxBorder.Length, y + i);
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
        for (int i = 1; i <= 4; i++)
            WriteAt("│", x, y + i);
        WriteAt("└", x, y + 5);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 5);
        WriteAt("┐", boxBorder.Length + 2, y);
        for (int i = 1; i <= 4; i++)
            WriteAt("│", boxBorder.Length + 2, y + i);
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
        int x = 0;
        int y = 5;
        string boxBorder = "──────────────────────────";
        Console.ForegroundColor = ConsoleColor.Cyan;
        WriteAt("┌", x, y);
        for (int i = 1; i <= 4; i++)
            WriteAt("│", x, y + i);
        WriteAt("└", x, y + 5);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 5);
        WriteAt("┐", boxBorder.Length + 1, y);
        for (int i = 1; i <= 4; i++)
            WriteAt("│", boxBorder.Length + 1, y + i);
        WriteAt("┘", boxBorder.Length + 1, y + 5);
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
        WriteAt($"  Stap {(stepcount <= 9?stepcount:9)} / 9   ", x + 1, y + 1);
        Console.ResetColor();
    }
    
    public static void WriteBill(int origrow, int origcol,int _amountOfPeople, int _underageMembers, int chosenCourse, bool _chosenWine, int _howManyWine)
    {
        // variables here
        origRow = origrow;
        origCol = origcol;
        int x = 20;
        int y = 0;
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        int amountAdult = _amountOfPeople - _underageMembers;
        var pricingstring = AccountsAccess.LoadAllMenu()["Prijzen"]!["Prijzen"]![chosenCourse - 2]!.ToObject<string>()!;
        Match match = Regex.Match(pricingstring, @"^€\s+(\d+)");
        int pricing = int.Parse(match.Groups[1].Value);
        // word writing here
        string boxBorder = "─────────────────────────────────────────────────────────────────────────────────";
        string info1 =
            $"{$"{amountAdult} volwassen personen x {match.Groups[0].Value} ({chosenCourse} gangen)",-8}: {"€ " + amountAdult * pricing,-30}";
        
        string info2 = (_underageMembers == 0 ? "Geen minderjarige personen in groep" :
            String.Format("{0,-8}: {1,-30}", $"{_underageMembers} minderjarige personen in groep x € {pricing * 0.90} (10% korting)", "€ " + _underageMembers * (pricing * 0.90)));
        
        string info3 = String.Format("{0,-8}: {1,-30}", (_chosenWine ? $"Wijn arrangement: Ja ({_howManyWine} arrangementen x €10)" : "Wijn arrangement"),
            (_chosenWine ? "€ " + (10 * _howManyWine) : "Nee"));
        
        string info4 = String.Format("{0,-3}: {1,-30}", "Totaal",
            "€ " + ((amountAdult * pricing) + (_underageMembers * (pricing * 0.90)) + (_chosenWine ? 10 * _howManyWine : 0)));
        Console.ResetColor();
        WriteAt("Rekening", x + 38, y + 1);
        WriteAt("Wij verzorgen geen betalingen via dit programma, dit is alleen een hulpmiddel", x + 2, y + 3);
        WriteAt("om te laten zien wat het gaat kosten in ons restaurant.", x + 2, y + 4);
        WriteAt(info1, x + 2, y + 6);
        WriteAt(info2, x + 2, y + 7);
        WriteAt(info3, x + 2, y + 8);
        Console.ForegroundColor = ConsoleColor.White;
        WriteAt(info4, x + 2, y + 10);
        // box writing here
        Console.ForegroundColor = ConsoleColor.Green;
        WriteAt("┌", x, y);
        for (var i = 1; i <= 10; i++)
            WriteAt("│", x, y + i);
        WriteAt("└", x, y + 11);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 2);
        WriteAt(boxBorder, x + 1, y + 5);
        WriteAt(boxBorder, x + 1, y + 9);
        WriteAt(boxBorder, x + 1, y + 11);
        WriteAt("┐", x + boxBorder.Length + 1, y);
        for (var i = 1; i <= 10; i++) 
            WriteAt("│", x + boxBorder.Length + 1, y + i);
        WriteAt("┘", x + boxBorder.Length + 1, y + 11);
        Console.ResetColor();
    }

    public static void WriteInformation(int origrow, int origcol)
    {
        origRow = origrow;
        origCol = origcol;
        WriteHours(origrow, origcol);
        WriteDescription(origrow, origcol);
        WriteContactInfo(origrow, origcol);
        WriteQuit(origrow, origcol);
        WriteEvents(origrow, origcol);
    }

    private static void WriteDescription(int origrow, int origcol)
    {
        origRow = origrow;
        origCol = origcol;
        string boxBorder = "────────────────────────────────────────────────────────────────────────────────────────────";
        string information =
            @"
            Hier in Rotterdam vind u het prachtige restaurant 'De Witte Haven'.
            Wij serveren uit een keuze van 2, 3 of 4 gangen menu's. 
            Ook is het mogelijk om er een wijnarrangement bij te boeken. 
            Naast het reserveren van een tafel is er ook nog beschikking tot een bar.
            Verder wanneer u klaar bent kunt u ook een filmpje pakken bij het dichtbijgelegen bioscoop
            die zich op hetzelfde adres bevind als ons restaurant.
            ";
        int x = 10;
        int y = 0;
        WriteAt("Beschrijving", x + 38, y + 1);
        Console.ResetColor();
        WriteAt(information, x + 1, y + 2);
        Console.ForegroundColor = ConsoleColor.Cyan;
        WriteAt("┌", x, y);
        for (var i = 1; i <= 8; i++)
            WriteAt("│", x, y + i);
        WriteAt("└", x, y + 9);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 2);
        WriteAt(boxBorder, x + 1, y + 9);
        WriteAt("┐", x + boxBorder.Length + 1, y);
        for (var i = 1; i <= 8; i++) 
            WriteAt("│", x + boxBorder.Length + 1, y + i);
        WriteAt("┘", x + boxBorder.Length + 1, y + 9);
        Console.ResetColor();
    }
    
    private static void WriteContactInfo(int origrow, int origcol)
    {
        origRow = origrow;
        origCol = origcol;
        string boxBorder = "─────────────────────────────────────────";
        string information =
            @"
  Wijnhaven 107
  3011 WN / Rotterdam
  +31-0612345678
  restaurant1234567891011@gmail.com
            ";
        int x = 0;
        int y = 10;
        WriteAt("Contact Informatie", x + 12, y + 1);
        WriteAt(information, x + 1, y + 2);
        Console.ForegroundColor = ConsoleColor.Cyan;
        WriteAt("┌", x, y);
        for (int i = 1; i <= 7; i++)
            WriteAt("│", x, y + i);
        WriteAt("└", x, y + 7);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 2);
        WriteAt(boxBorder, x + 1, y + 7);
        WriteAt("┐", x + boxBorder.Length + 1, y);
        for (int i = 1; i <= 7; i++)
            WriteAt("│", x + boxBorder.Length + 1, y + i);
        WriteAt("┘", x + boxBorder.Length + 1, y + 7);
        Console.ResetColor();
    }
    public static void WriteTimeInfo(int origrow, int origcol)
    {
        origRow = origrow;
        origCol = origcol;
        string boxBorder = "───────────────────────────────────────────────────────────────────────────────";
        // i know this is very ugly, but @ string literals dont seem to be affected by writing at certain X coordinates
        // so i have to use manual whitespaces to format it correctly
        // i hate it too
        string information = @"
                                                         Ons restaurant is geopend van 16:00 tot 22:00.
                                                Tijdens deze uren hebben wij 3 tijdssloten waarin u kunt reserveren.
                                                                      De tijdssloten zijn:
                                                                      16:00 tot 18:00
                                                                      18:00 tot 20:00
                                                                      20:00 tot 22:00

                                        wanneer er een evenement plaatst vindt in ons restaurant zijn de openingstijden anders.
                                                                  De openingstijden zijn dan:
                                                                      16:00 tot 19:00
                                                                      19:00 tot 22:00

                                                        wij hopen u snel te zien in ons restaurant!
            ";
        int x = 46;
        int y = 10;
        WriteAt("Tijden", x + 37, y + 1);
        WriteAt(information, x + 1, y + 2);
        Console.ForegroundColor = ConsoleColor.Cyan;
        WriteAt("┌", x, y);
        for (var i = 1; i <= 17; i++) 
            WriteAt("│", x, y + i);
        WriteAt("└", x, y + 18);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 2);
        WriteAt(boxBorder, x + 1, y + 15);
        WriteAt("┐", x + boxBorder.Length + 1, y);
        for (var i = 1; i <= 17; i++)
            WriteAt("│", x + boxBorder.Length + 1, y + i);
        WriteAt("┘", x + boxBorder.Length + 1, y + 18);
        Console.ResetColor();
    }
    private static void WriteHours(int origrow, int origcol)
    {
        origRow = origrow;
        origCol = origcol;
        string boxBorder = "───────────────────────────────────────────────────────────────────────────────";
        // i know this is very ugly, but @ string literals dont seem to be affected by writing at certain X coordinates
        // so i have to use manual whitespaces to format it correctly
        // i hate it too
        string information = @"
                                                Ons restaurant is geopend van 16:00 tot 00:00.
                                                Onze reserveringstijden hangen af van uw gekozen gang.
                                                Wanneer er een evenement plaatsvindt in ons restaurant zijn de tijden anders.

                                                               2 Gangen        3 Gangen       4 Gangen      
                                                            16:00 - 17:30   16:00 - 18:00   16:00 - 18:30
                                                            17:30 - 19:00   18:00 - 20:00   18:30 - 21:00
                                                            19:00 - 20:30   20:00 - 22:00   21:00 - 23:30

                                                                            Evenementtijden
                                                                             16:00 - 19:00
                                                                             19:00 - 22:00
";
        int x = 46;
        int y = 10;
        WriteAt("Tijden", x + 37, y + 1);
        WriteAt(information, x + 1, y + 2);
        Console.ForegroundColor = ConsoleColor.Cyan;
        WriteAt("┌", x, y);
        for (var i = 1; i <= 14; i++) 
            WriteAt("│", x, y + i);
        WriteAt("└", x, y + 15);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 2);
        WriteAt(boxBorder, x + 1, y + 15);
        WriteAt("┐", x + boxBorder.Length + 1, y);
        for (var i = 1; i <= 14; i++)
            WriteAt("│", x + boxBorder.Length + 1, y + i);
        WriteAt("┘", x + boxBorder.Length + 1, y + 15);
        Console.ResetColor();
    }
    
    private static void WriteQuit(int origrow, int origcol)
    {
        origRow = origrow;
        origCol = origcol;
        string boxBorder = "─────────────────────────────────────────";
        string information =
            @"
     Druk op een willekeurige toets
   om terug te gaan naar het hoofdmenu.
            ";
        int x = 0;
        int y = 20;
        WriteAt("Terug naar het hoofdmenu", x + 10, y + 1);
        WriteAt(information, x + 1, y + 2);
        Console.ForegroundColor = ConsoleColor.Red;
        WriteAt("┌", x, y);
        for (var i = 1; i <= 4; i++) 
            WriteAt("│", x, y + i);
        WriteAt("└", x, y + 5);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 2);
        WriteAt(boxBorder, x + 1, y + 5);
        WriteAt("┐", x + boxBorder.Length + 1, y);
        for (var i = 1; i <= 5; i++) 
            WriteAt("│", x + boxBorder.Length + 1, y + i);
        WriteAt("┘", x + boxBorder.Length + 1, y + 5);
        Console.ResetColor();
    }
    private static void WriteEvents(int origrow, int origcol)
    {
        origRow = origrow;
        origCol = origcol;
        int x = 0;
        int y = 0;
    }

    public static void WritePasswordToggle(int origrow, int origcol, bool register)
    {
        origRow = origrow;
        origCol = origcol;
        string boxBorder = "───────────────────────────────────────────────────────";
        string information =
            @"
    Druk op de 'F1' toets linksboven op uw toetsenbord
          om te toggelen tussen '*' en letters.
            ";
        int x = 0;
        int y = 3;
        if (register)
            y += 8;
        WriteAt("Wachtwoord Toggle", x + 19, y + 1);
        WriteAt(information, x + 1, y + 2);
        Console.ForegroundColor = ConsoleColor.Green;
        WriteAt("┌", x, y);
        for (int i = 1; i <= 4; i++)
            WriteAt("│", x, y + i);
        WriteAt("└", x, y + 5);
        WriteAt(boxBorder, x + 1, y);
        WriteAt(boxBorder, x + 1, y + 2);
        WriteAt(boxBorder, x + 1, y + 5);
        WriteAt("┐", x + boxBorder.Length + 1, y);
        for (int i = 1; i <= 4; i++)
            WriteAt("│", x + boxBorder.Length + 1, y + i);
        WriteAt("┘", x + boxBorder.Length + 1, y + 5);
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