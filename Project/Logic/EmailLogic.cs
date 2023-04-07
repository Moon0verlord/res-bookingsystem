using System.Net.Mail;
using System.Net;


class EmailLogic
{
    public EmailLogic()
    {
        
    }
    
    public void SendEmailAccount(AccountModel acc)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {            
            Credentials = new NetworkCredential("testrestaurant12356789@gmail.com", "levkehrnvtpnqkpm"),
            EnableSsl = true        
        }; 
        client.Send("testrestaurant12356789@gmail.com", $"{acc.EmailAddress}", "test", "testbody");
    }

    public static void SendEmailNoAccount(string email, string name,int table, DateTime Date)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {            
            Credentials = new NetworkCredential("testrestaurant12356789@gmail.com", "levkehrnvtpnqkpm"),
            EnableSsl = true        
        };
        client.Send("testrestaurant12356789@gmail.com", $"{email}", "test", ($"Hello {name}!\n You have a reservation on {Date} for table {table}"));
    }
}