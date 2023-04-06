public class EmployeeManagerLogic : IMenuLogic
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
                string[] choices = { "ID", "Email adres" };
                int chosenInput = _myMenu.RunMenu(choices, "Gesorteerd");
                switch (chosenInput)
                {
                    case 0:
                        GetCustomer<Int32>(0);
                        break;
                    case 1:
                        GetCustomer<Int32>(1);
                        break;
                }

                break;
            case 1:
                Console.WriteLine("Who are you looking for?");
                string person = Console.ReadLine();
                GetCustomer<string>(person);
                break;

        }
    }

    public static void GetCustomer<T>(T item)
    {
        var Useraccounts = AccountsAccess.LoadAll().FindAll(x => x.IsEmployee == false);
        if (item is string)
        {
            Console.Clear();
            foreach (var Account in Useraccounts)
            {
                if (Account.FullName == item as string || Account.EmailAddress == item as string)
                {
                    Console.WriteLine($"Id:{Account.Id}, Name:{Account.FullName}, Email:{Account.EmailAddress}");
                }
            }
            Console.WriteLine("Druk op een knop om terug te gaan naar het Employee menu.");
            Console.ReadKey(true);
        }

        if (item is int)
        {
            if (Convert.ToInt32(item) == 0)
            {
                string[] idChoices = { "1-10", "10-1" };
                int ChosenId = _myMenu.RunMenu(idChoices, "Van laagst-hoogst of hoogst-laagst");
                switch (ChosenId)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Accounts gesorteerd 1-10");
                        foreach (var account in Useraccounts)
                        {
                            Console.WriteLine(
                                $"Id:{account.Id}, Name:{account.FullName}, Email:{account.EmailAddress}");
                        }

                        break;
                    case 1:
                        Console.Clear();
                        Useraccounts.Reverse();
                        Console.WriteLine("Accounts gesorteerd 10-1");
                        foreach (var account in Useraccounts)
                        {
                            Console.WriteLine(
                                $"Id:{account.Id}, Name:{account.FullName}, Email:{account.EmailAddress}");
                        }

                        break;
                    
                }
                Console.WriteLine("Druk op een knop om terug te gaan naar het Employee menu.");
                Console.ReadKey(true);
            }
            else if (Convert.ToInt32(item) == 1)
            {
                
                Useraccounts.Sort();
                string[] choices = { "Z-A", "A-Z" };
                int chosenInput = _myMenu.RunMenu(choices, "Van A-Z of Z-A");
                switch (chosenInput)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Accounts gesorteerd Z-A");
                        Useraccounts.Reverse();
                        foreach (var account in Useraccounts)
                        {
                            Console.WriteLine(
                                $"Id:{account.Id}, Name:{account.FullName}, Email:{account.EmailAddress}");
                        }

                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Accounts gesorteerd A-Z");
                        foreach (var account in Useraccounts)
                        {
                            Console.WriteLine(
                                $"Id:{account.Id}, Name:{account.FullName}, Email:{account.EmailAddress}");
                        }
                        break;
                }
                Console.WriteLine("Druk op een knop om terug te gaan naar het Employee menu.");
                Console.ReadKey(true);
            }

        }
    }
}