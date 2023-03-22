class ReservationsLogic
{
    static private int cur_month = DateTime.UtcNow.Date.Month;
    static private ReservationsLogic reserv = new ReservationsLogic();
    static private MenuLogic _myMenu = new MenuLogic();
    public void ReservationsMenu()
    {
        var options = new string[12-cur_month+1];
        var counter = 0;
        for (int i = cur_month; i<=12; i++)
        {
            options[counter] = i.ToString();
            counter++;
        }
        string prompt = $"make a reservation for one these months: \n{DateTime.UtcNow.Date.Year}";
        
        int input = _myMenu.RunMenu(options, prompt);
    }
}