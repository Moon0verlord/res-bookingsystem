using System.Runtime.InteropServices;
public class Program
{
    private static void Main()
    {
        // set window size here to make sure there's no bounding errors later in the program.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
        }
        Console.Clear();
        MainMenu.Start();
    }
}