class restaurantInfo:IMenuLogic
{
    static string Information =
@"
/Restaurant name/

Hier in Rotterdam vind je het prachtige /restaurant name/.
Wij serven uit een keuze van 2, 3 of 4 gangen menu's. Ook is
het mogelijk om er een wijnarrangement bij te boeken. Naast 
het reserveren van een tafel is er ook beschikking tot een bar.";
    static string Contact =
@"
Contact Information:
Wijnhaven 107
3011 WN / Rotterdam
0612345678
/email/ 
";
    static string Events =
@"Special Events:
Er zijn op dit moment nog geen events.
Kom terug op een later moment om te zien of er al events zijn.
";

    public static void Start()
    {
        Console.WriteLine(Information);
        Console.WriteLine(Contact);
        Console.WriteLine(Events); // later nog de echte events toevoegen
    }
}