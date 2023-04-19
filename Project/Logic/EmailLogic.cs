using System.Net;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
class EmailLogic
{
    public static bool IsValidEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
    public static void SendEmail(string email, string name, string table, DateTime Date)
    {
        try
        {
            //Which of the servers hostnames is gonna be used to send emails
            var Smtp = new SmtpClient("smtp.gmail.com", 587);

            //Authentification info
            Smtp.UseDefaultCredentials = false;
            NetworkCredential basicAuthenticationInfo = new
                NetworkCredential("testrestaurant12356789@gmail.com", "levkehrnvtpnqkpm");
            Smtp.Credentials = basicAuthenticationInfo;

            //Who the email is from, who its going to, the mail message and what the reply email is 
            MailAddress from = new MailAddress("testrestaurant12356789@gmail.com", "Restaurant");
            MailAddress to = new MailAddress(email, $"{email}");
            
            MailMessage myMail = new MailMessage(from, to);
            MailAddress replyTo = new MailAddress("testrestaurant12356789@gmail.com");
            //ReplytoList is what it says on the tin, the reply to option in mail can contain multiple emails
            myMail.ReplyToList.Add(replyTo);
            //What is the subject, the encoding, the message in the body and its encoding etc
            myMail.Subject = "Reservatie";
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;
            myMail.Body =
                $"<html><body><h1>Hallo {name}!</h1><p><h2>U heeft een reservatie op <b>{Date:d/MMMM/yyyy}</b> om " +
                $"<b><b>{Date:hh:mm:ss}</b> </b>voor tafel <b>{table}</b>. " +
                $"<br>tot dan!</br></h2> </body></html>";
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            myMail.IsBodyHtml = true;
            //Encrypts the emails being sent for extra security
            Smtp.EnableSsl = true;
            
            Smtp.Send(myMail);
        }

        catch (SmtpException ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
}