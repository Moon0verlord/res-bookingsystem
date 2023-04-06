public class EmployeeManagerLogic:IMenuLogic
{
    private static MenuLogic _myMenu = new MenuLogic();

    public static void Start()
    {
        string[] options = { "Sorteren", "Zoek op naam/emailadres" };

        int input = _myMenu.RunMenu(options, "Accounts");
        switch (input)
        {
            case 0:
                Console.WriteLine("Id of email adres ?");
                string[] choices = {"ID","Email adres"};
                int chosenInput = _myMenu.RunMenu(choices, "Gesorteerd");
                switch (chosenInput)
                {
                    case 0:
                        GetCustomer(0);
                        break;
                    case 1:
                        GetCustomer(1);
                        break;
                }

                break;
            case 1:
                Console.WriteLine("Who are you looking for?");
                var person = Console.ReadLine();
                GetCustomer<string>(person);
                break;
           
        }
    }
    public static void GetCustomer<T>(T item)
        { 
            var Useraccounts = AccountsAccess.LoadAll().FindAll(x => x.IsEmployee == false);
            if(item is string)
            {
                foreach (var Account in Useraccounts)
                {
                    if (Account.FullName == item as string || Account.EmailAddress == item as string)
                    {
                        Console.WriteLine($"Id:{Account.Id}, Name:{Account.FullName}, Email:{Account.EmailAddress}");
                    }
                }
            }

            if (item is int)
            {
                Useraccounts.Sort();
                foreach (var account in Useraccounts)
                {
                    Console.WriteLine($"Id:{account.Id}, Name:{account.FullName}, Email:{account.EmailAddress}");
                }
            }
            Thread.Sleep(100000);
        
    }
}