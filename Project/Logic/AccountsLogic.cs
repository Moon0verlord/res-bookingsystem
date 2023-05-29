//This class is not static so later on we can use inheritance and interfaces

using System.Text.RegularExpressions;

class AccountsLogic:IMenuLogic
{
    private List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }
    
    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AccountsAccess.WriteAll(_accounts);

    }
    
    public void RefreshList()
    => _accounts = AccountsAccess.LoadAll();

    // get an account by id
    public AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id)!;
    }

    // get an account by email
    public AccountModel GetByEmail(string email)
        => _accounts.Find(i => i.EmailAddress == email)!;
    
    // check if a login is valid
    public AccountModel CheckLogin(string email, string password)
    {
        AccountModel? acc = GetByEmail(email);
        if (acc != null!)
        {
            if (BCrypt.Net.BCrypt.Verify(password, acc.Password))
            {
                CurrentAccount = acc;
                return acc;
            }
        }
        return null!;
    }

    // method used if you forgot your password
    public void ForgotPassword(string email)
    {
        // get the account by email
        AccountModel? acc = GetByEmail(email);

        // create a 6 digit random number
        Random r = new Random();
        int randNum = r.Next(1000000);
        string sixDigitNumber = randNum.ToString("D6");

        // send the email
        EmailLogic.SendVerificationMail(email, acc.FullName, sixDigitNumber);
        Console.WriteLine("Er is een e-mail verstuurd naar " + email + " met uw Verificatiecode.");
        string code;
        Console.CursorVisible = true;
        // check if the code is correct
        do
        {
            Console.WriteLine("Voer de verificatie code in: ");
            code = Console.ReadLine()!;
            Console.WriteLine(code == sixDigitNumber ? "Verificatie gelukt!" : "Incorrecte code");
        } 
        while (code != sixDigitNumber);
        string password;
        string confirmPassword;

        // check if the passwords are equal
        do{
            // check if the password is valid
            do{
                Console.WriteLine("Voer uw nieuwe wachtwoord in: ");
                password = Console.ReadLine()!;
                Console.WriteLine("herhaal uw nieuwe wachtwoord: ");
                confirmPassword = Console.ReadLine()!;
                if (UserLogin.PasswordCheck(password) == false)
                {
                    Console.WriteLine("Wachtwoord moet minimaal 8 karakters bevatten, een hoofdletter, een kleine letter, een cijfer en een speciaal teken");
                }
            }
            while (UserLogin.PasswordCheck(password) == false);

            if (password == confirmPassword)
            {
                // hash the password and update the account
                acc.Password = BCrypt.Net.BCrypt.HashPassword(password);
                UpdateList(acc);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Wachtwoord is veranderd");
                Console.ResetColor();
                Thread.Sleep(2000);
                UserLogin.DiscardKeys();
            }
            else
            {
                Console.WriteLine("Wachtwoorden komen niet overeen");
            }
        } 
        while (password != confirmPassword);
    }

    public static void LogOut()
    {
        Console.CursorVisible = true;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Weet u het zeker? (j/n): ");
        Console.ResetColor();
        var userAnswer = Console.ReadLine()!.ToLower();
        switch (AnswerLogic.CheckInput(userAnswer)) 
        {
            case 1:
            MainMenu.Account = null!;
            MainMenu.Start();
            break;
            case 0:
                break;
            case -1:
                LogOut();
                break;
        }
        
    }
}




