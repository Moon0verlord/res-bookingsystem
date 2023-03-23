using System.ComponentModel.Design;

static class Reservation
{
    static private ReservationsLogic reserv = new ReservationsLogic();
    public static void ResStart(AccountModel acc = null)
    {
        reserv.ReservationsMenu();
        //show available hours on requested date
        //User can select an hour
        //back to menu
    }
}