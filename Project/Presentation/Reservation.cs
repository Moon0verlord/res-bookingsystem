using System.ComponentModel.Design;

static class Reservation
{
    private static readonly ReservationsLogic Reservations = new ReservationsLogic();
    public static void ResStart(AccountModel acc = null)
    {
        Console.Clear();
        if (acc == null)
        {
            Console.Write("\nPlease enter your email to make a reservation: ");
            string email = Console.ReadLine()!;
            Reservations.ReservationsMenu(email);  
        }
        else
        {
            Reservations.ReservationsMenu(acc.EmailAddress);
        }
        //show available hours on requested date
        //User can select an hour
        //back to menu
    }
}