public class Program
{
    private static void Main()
    {
        // set windowsize here to make sure there's no bounding errors later in the program.
        try
        {
            Console.SetWindowSize(180, 35);
        }
        catch {}
        Console.Clear();
        MainMenu.Start();
    }
}