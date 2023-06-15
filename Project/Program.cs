using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    private static void Main()
    {

        Console.Clear();
        Console.SetCursorPosition(25,15);
        var info = @"                                   Onze applicatie werkt het best in full screen.
                                                             Als u het niet in fullscreen heeft,
                                                kunnen we niet garanderen dat de applicatie werkt als verwacht. 
                                                       Druk een toets in als u dit bericht gelezen heeft.";
        Console.WriteLine(info);
        Console.ReadKey();
        Console.Clear();
        MainMenu.Start();
    }
}