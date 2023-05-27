using System.Net;
using System.Net.Mail;
using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
public class EmailLogic
{
    // check if the domain of the email is valid
    public static bool CheckDomain(string email)
    {
        email = email.ToLower();
        string page = "https://data.iana.org/TLD/tlds-alpha-by-domain.txt";
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = httpClient.GetAsync(page).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine();
                int index = email.LastIndexOf(".");
                string domain = email.Substring(index +1);
                
                string responseBody = response.Content.ReadAsStringAsync().Result.ToLower();
                
                if (domain.Length>0 && responseBody.Contains(domain))
                {
                    return true;

                }
                return false;
                
            }
            return false;
        }
    }

    // check if the email is valid
    public static bool IsValidEmail( string email)
    {
        try
        {
            var addr = new MailAddress(email);
            if(addr.ToString().Contains(".")&&CheckDomain(email))
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
        return false;
    }

    // Send an email to the user after they have made a reservation
    public static void SendEmail(string email, string name, string table, DateTime Date)
    {
        try
        {
            // create the linked resource for the image (assuming the image file is in a subdirectory of the program's working directory)
            string imagePath = Path.Combine(Environment.CurrentDirectory, "DataSources", "360_F_324739203_keeq8udvv0P2h1MLYJ0GLSlTBagoXS48.jpg");
            //Which of the servers hostnames is gonna be used to send emails
            var Smtp = new SmtpClient("smtp.gmail.com", 587);
            //Authentification info
            Smtp.UseDefaultCredentials = false;     
            NetworkCredential basicAuthenticationInfo = new
                NetworkCredential("restaurant1234567891011@gmail.com", "vqxjoomtkvrjmnxu");
            Smtp.Credentials = basicAuthenticationInfo;
            
            

            //Who the email is from, who its going to, the mail message and what the reply email is 
            MailAddress from = new MailAddress("testrestaurant12356789@gmail.com", "Restaurant");
            MailAddress to = new MailAddress(email, $"{email}");
            
            MailMessage myMail = new MailMessage(from, to);
            MailAddress replyTo = new MailAddress("testrestaurant12356789@gmail.com");
            LinkedResource imageResource = new LinkedResource(imagePath, "image/jpeg");
            imageResource.ContentId = "image1";
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                $"<html><body><div><h1>Hallo{name}!</h1></div><img src=\"cid:image1\"><div><p><h3>U heeft een reservatie op <b>{Date:d/MMMM/yyyy}</b> om " +
                $"<b><b>{Date:hh:mm:ss}</b> </b>voor tafel <b>{table}</b>. " +
                $"<br>tot dan!</br></h3></div></body></html>", null, "text/html");
            htmlView.LinkedResources.Add(imageResource);
            myMail.AlternateViews.Add(htmlView);
            //ReplytoList is what it says on the tin, the reply to option in mail can contain multiple emails
            myMail.ReplyToList.Add(replyTo);
            //What is the subject, the encoding, the message in the body and its encoding etc
            myMail.Subject = "Reservatie";
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;
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

    // sends a mail to the user with a verification code if they want to change their password
    public static void SendVerificationMail(string email, string name, string vrfyCode){
       try
        {
            //Which of the servers hostnames is gonna be used to send emails
            var Smtp = new SmtpClient("smtp.gmail.com", 587);
            //Authentification info
            Smtp.UseDefaultCredentials = false;
            NetworkCredential basicAuthenticationInfo = new
                NetworkCredential("restaurant1234567891011@gmail.com", "vqxjoomtkvrjmnxu");
            Smtp.Credentials = basicAuthenticationInfo;

            //Who the email is from, who its going to, the mail message and what the reply email is 
            MailAddress from = new MailAddress("testrestaurant12356789@gmail.com", "Restaurant");
            MailAddress to = new MailAddress(email, $"{email}");
            
            MailMessage myMail = new MailMessage(from, to);
            MailAddress replyTo = new MailAddress("testrestaurant12356789@gmail.com");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                $"<html><body><div><h1>Hallo {name}!</h1></div><div><p><h3>U heeft aangegeven dat u uw huidige wachtwoord bent vergeten en daarom hebben wij een verificatie code voor u aangemaakt. " +
                $"<br>Deze code is: <b>{vrfyCode}</b></br><br>Gebruik deze code in ons programma om uw wachtwoord te resetten.</br></h3></div></body></html>", null, "text/html");
            myMail.AlternateViews.Add(htmlView);
            //ReplytoList is what it says on the tin, the reply to option in mail can contain multiple emails
            myMail.ReplyToList.Add(replyTo);
            //What is the subject, the encoding, the message in the body and its encoding etc
            myMail.Subject = "Reset van wachtwoord";
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;
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

    // sends a mail to the user if their reservation couldn't be altered
    public static void SendCancellationMail(string email, string name){
       try
        {
            //Which of the servers hostnames is gonna be used to send emails
            var Smtp = new SmtpClient("smtp.gmail.com", 587);
            //Authentification info
            Smtp.UseDefaultCredentials = false;
            NetworkCredential basicAuthenticationInfo = new
                NetworkCredential("restaurant1234567891011@gmail.com", "vqxjoomtkvrjmnxu");
            Smtp.Credentials = basicAuthenticationInfo;

            //Who the email is from, who its going to, the mail message and what the reply email is 
            MailAddress from = new MailAddress("testrestaurant12356789@gmail.com", "Restaurant");
            MailAddress to = new MailAddress(email, $"{email}");
            
            MailMessage myMail = new MailMessage(from, to);
            MailAddress replyTo = new MailAddress("testrestaurant12356789@gmail.com");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                $"<html><body><div><h1>Hallo {name}!</h1></div><div><p><h3>U heeft aangegeven dat u uw reservatie verkeerd heeft ingevoerd. Helaas is het ons niet gelukt om uw gewenste tijd in te voeren. " +
                $"<br>U kunt zelf een nieuwe reservering invullen. Hopelijk zien wij u snel bij de Witte haven!</br></h3></div></body></html>", null, "text/html");
            myMail.AlternateViews.Add(htmlView);
            //ReplytoList is what it says on the tin, the reply to option in mail can contain multiple emails
            myMail.ReplyToList.Add(replyTo);
            //What is the subject, the encoding, the message in the body and its encoding etc
            myMail.Subject = "Annulatie van reservatie";
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;
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