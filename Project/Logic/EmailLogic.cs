using System.Net;
using System.Net.Mail;
using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
public class EmailLogic
{
    // check if a domain is valid
    public static async Task<bool> IsValidDomain(string domain)
    {
        string url = "https://data.iana.org/TLD/tlds-alpha-by-domain.txt";
        bool containsSubstring = false;

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                containsSubstring = content.Contains(domain);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        return containsSubstring;
    }

    // check if an email is valid
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);
                if (Convert.ToBoolean(IsValidDomain(domainName))) ;
                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }
        catch (InvalidCastException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    // sends a mail to the user after they have reserved a table
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