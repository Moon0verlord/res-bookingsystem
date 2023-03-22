public class Program
{
    static void Main()
    {
        MenuLogic myMenu = new MenuLogic();
        string[] options = { "Log in", "Information", "Schedule", "View current menu", "Make reservation with e-mail"};
        string prompt = "Menu:\n";
        int selectedOption = myMenu.RunMenu(options, prompt);
        Menu.Start(input: selectedOption);
    }
}