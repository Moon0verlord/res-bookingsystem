using System.Runtime.InteropServices;
public class Program
{
    private static void Main()
    {
        
        Console.Clear();
        Console.SetCursorPosition(50,15);
        var info = @"                                   Onze applicatie werkt het best in full screen.
                                                              Als u niet het in fullscreen heeft kunnen we niet garanderen dat de applicatie werkt als verwacht. 
                                                                                  Druk een toets in als u dit bericht gelezen heeft.";
        Console.WriteLine(info);
        Console.ReadKey();
        Console.Clear();
        MainMenu.Start();
    }
}