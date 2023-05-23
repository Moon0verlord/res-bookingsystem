static class TimeInfo
{
    static string OpeningsHours =
    @"Ons restaurant is geopend van 16:00 tot 22:00.
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

    public static void Start()
    {
        Console.Clear();
        InfoBoxes.WriteTimeInfo(Console.CursorTop, Console.CursorLeft);
    }
}