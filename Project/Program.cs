using System;
using System.Runtime.InteropServices;
public class Program
{
    private static void Main()
    {
        // set windowsize here to make sure there's no bounding errors later in the program.
        try
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
        }
        catch { }
        Console.Clear();
        MainMenu.Start();
    }
}