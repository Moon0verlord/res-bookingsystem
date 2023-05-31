using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using System.Text.Json;

static class restaurantInfo
{
    private static int _currentIndex;

    private static MenuLogic _myMenu = new MenuLogic();
    static string Information =
@"
De Witte Haven

Hier in Rotterdam vind je het prachtige De Witte Haven.
Wij serveren uit een keuze van 2, 3 of 4 gangen menu's. Ook is
het mogelijk om er een wijnarrangement bij te boeken. Naast 
het reserveren van een tafel is er ook beschikking tot een bar.
Verder wanneer u klaar bent kunt u ook een filmpje pakken bij het dichtbijgelegen bioscoop:
/Bioscoop/ Wijnhaven 107, 3011 WN in Rotterdam";
    static string Contact =
@"
Contact Informatie:
Wijnhaven 107
3011 WN / Rotterdam
0612345678
restaurant1234567891011@gmail.com
";
    static string Events =
@"Speciale Evenementen:
Er zijn op dit moment nog geen evenementen.
Kom terug op een later moment om te zien of er al evenementen zijn.
";

    public static void Start()
    {
        Console.Clear();
        InfoBoxes.WriteInformation(Console.CursorTop, Console.CursorLeft);
        Console.ReadKey(true);
    }
}