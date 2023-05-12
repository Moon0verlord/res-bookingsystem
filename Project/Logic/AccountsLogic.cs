using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class AccountsLogic:IMenuLogic
{
    private List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

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

    public AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id)!;
    }

    public AccountModel GetByEmail(string email)
        => _accounts.Find(i => i.EmailAddress == email)!;
    
    
    public AccountModel CheckLogin(string email, string password)
    {
        AccountModel? acc = GetByEmail(email);
        if (acc != null)
        {
            if (BCrypt.Net.BCrypt.Verify(password, acc.Password))
            {
                CurrentAccount = acc;
                return acc;
            }
        }
        return null!;
    }

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

        // check if the code is correct
        do
        {
            Console.WriteLine("Voer de verificatie code in: ");
            code = Console.ReadLine();
            if (code == sixDigitNumber)
            {
                Console.WriteLine("Verificatie gelukt!");
            }
            else
            {
                Console.WriteLine("Incorrecte code");
            }
        } 
        while (code != sixDigitNumber);
        string password = "";
        string confirmPassword = "";

        // check if the passwords are equal
        do{
            // check if the password is valid
            do{
                Console.WriteLine("Voer uw nieuwe wachtwoord in: ");
                password = Console.ReadLine();
                Console.WriteLine("herhaal uw nieuwe wachtwoord: ");
                confirmPassword = Console.ReadLine();
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
            }
            else
            {
                Console.WriteLine("Wachtwoorden komen niet overeen");
            }
        } 
        while (password != confirmPassword);
    }
}




