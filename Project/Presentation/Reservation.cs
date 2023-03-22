using System.ComponentModel.Design;

public static class Reservation
{
    static private ReservationsLogic reserv = new ReservationsLogic();
    public static void ResStart()
    {
        reserv.ReservationsMenu();
        //show available hours on requested date
        //User can select an hour
        //back to menu
    }
}