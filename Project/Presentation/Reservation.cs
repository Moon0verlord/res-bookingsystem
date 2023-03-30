using System.ComponentModel.Design;

static class Reservation
{
    private static readonly ReservationsLogic Reservations = new ReservationsLogic();
    public static void ResStart(AccountModel acc = null)
    {
        Reservations.ReservationsMenu();
        //show available hours on requested date
        //User can select an hour
        //back to menu
    }
}