public class Program
{
    static void Main()
    {
        string[] options = { "Log in", "Information", "Schedule", "View current menu", "Make reservation with e-mail"};
        string prompt = "Menu:\n";
        int selectedOption = Menu.RunMenu(options, prompt);
        Menu.Start(input: selectedOption);
    }
}