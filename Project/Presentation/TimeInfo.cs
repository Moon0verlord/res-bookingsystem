static class TimeInfo
{
    static string OpeningsHours = 
    @"Ons restaurant is geopend van 16:00 tot 22:00.
Tijdens deze uren hebben wij 3 tijdssloten waarin u kunt reserveren.
De tijdssloten zijn:
16:00 tot 18:00
18:00 tot 20:00
20:00 tot 22:00
";

    public static void Start()
    {
        Console.Clear();
        Console.WriteLine(OpeningsHours);
    }
}