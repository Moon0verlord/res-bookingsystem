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

    /*public void ForgotPassword(string email)
    {
        AccountModel? acc = GetByEmail(email);
        Random r = new Random();
        int randNum = r.Next(1000000);
        string sixDigitNumber = randNum.ToString("D6");
        if (acc != null)
        {
            EmailLogic.SendVerificationMail(email, acc.FullName, sixDigitNumber);
        }
        else
        {
            Console.WriteLine("Email is niet gevonden");
        }
    }

    public void ResetPassword(string email, string verificationCode){
        Console.WriteLine("Voer de verificatie code in");
        string code = Console.ReadLine();
        if (code == verificationCode)
        {
            Console.WriteLine("Voer uw nieuwe wachtwoord in");
            string password = Console.ReadLine();
            Console.WriteLine("herhaal uw nieuwe wachtwoord");
            string confirmPassword = Console.ReadLine();
            if (password == confirmPassword)
            {
                AccountModel acc = GetByEmail(email);
                acc.Password = BCrypt.Net.BCrypt.HashPassword(password);
                UpdateList(acc);
            }
            else
            {
                Console.WriteLine("Wachtwoorden komen niet overeen");
            }
        }
        else
        {
            Console.WriteLine("Verificatie code is niet correct");
        }
    }*/
}




