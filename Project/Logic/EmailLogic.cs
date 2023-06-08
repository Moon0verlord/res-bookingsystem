
using System.Net;
using System.Net.Mail;
using System.ComponentModel;
public class EmailLogic
{
    public static bool mailSent = false;
    // check if the top-level domain of the email is valid
    private static bool CheckDomain(string? email)
    {
        email = email!.ToLower();
        string page = "https://data.iana.org/TLD/tlds-alpha-by-domain.txt";
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = httpClient.GetAsync(page).Result;
            //A connection to the api has been established 
            if (response.IsSuccessStatusCode)
            {
                //get the part of the email from the '.' until the end
                int index = email.LastIndexOf(".");
                string domain = email.Substring(index + 1);

                string responseBody = response.Content.ReadAsStringAsync().Result.ToLower();
                //domain.length is here due to a bug allowing empty top level domains to pass
                //Still gotta dive into the api to check 
                if (domain.Length > 0 && responseBody.Contains(domain))
                {
                    return true;

                }
                return false;

            }
            return false;
        }
    }

    // check if the email is valid
    public static bool IsValidEmail(string? email)
    {
        try
        {
            var addr = new MailAddress(email);
            if (addr.ToString().Contains('.') && CheckDomain(email))
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
    public static void SendEmail(string? email, DateTime Date, string code, TimeSpan StartTime, TimeSpan LeaveTime)
    {
        try
        {
            // only works if you use visual studio
            // try to send a mail with a qrcode personalized for the user
            try
            {
                EmailModel mail = new EmailModel(email, "Reservering", HTMLInfo.GetHTML(Date.ToString("dd-MM-yyyy"), code,
                    $"{StartTime:hh}:{StartTime:mm} - {LeaveTime:hh}:{LeaveTime:mm}"));

                string qrcodeimagePath = Path.Combine(Environment.CurrentDirectory, @"DataSources/QR_Codes",
                $"{email}-{code}.pdf");
                LinkedResource imageResourceqrcode = new LinkedResource(qrcodeimagePath);
                imageResourceqrcode.ContentId = "QRCode.pdf";
                mail.htmlView.LinkedResources.Add(imageResourceqrcode);
                mail.mailMessage.AlternateViews.Add(mail.htmlView);
                mail.Client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                string userState = "Reservering";
                mail.Client.SendAsync(mail.mailMessage, userState);
                Console.WriteLine("Mail versturen...");
                Thread.Sleep(3000);
                mail.mailMessage.Dispose();
            }

            // if the personalized qrcode can't be found, send a mail with a default qrcode
            catch
            {
                EmailModel mail = new EmailModel(email, "Reservering", HTMLInfo.GetHTML(Date.ToString("dd-MM-yyyy"), code,
                    $"{StartTime:hh}:{StartTime:mm} - {LeaveTime:hh}:{LeaveTime:mm}"));


                string imagePath = Path.Combine(Environment.CurrentDirectory, "DataSources",
                    "VoorbeeldQRCode.pdf");
                LinkedResource imageResource = new LinkedResource(imagePath);
                imageResource.ContentId = "VoorbeeldQRCode.pdf";
                mail.htmlView.LinkedResources.Add(imageResource);
                mail.mailMessage.AlternateViews.Add(mail.htmlView);
                mail.Client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                string userState = "Reservering";
                mail.Client.SendAsync(mail.mailMessage, userState);
                Console.WriteLine("Mail versturen...");
                Thread.Sleep(3000);
                mail.mailMessage.Dispose();
            }
        }
        catch (SmtpException ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    // sends a mail to the user with a verification code if they want to change their password
    public static void SendVerificationMail(string? email, string name, string vrfyCode)
    {
        try
        {
            EmailModel verMail = new EmailModel(email, "Reset van wachtwoord", $"<html><body><div><h1>Hallo {name}!</h1></div><div><p><h3>U heeft aangegeven dat u uw huidige wachtwoord bent vergeten en daarom hebben wij een verificatie code voor u aangemaakt. " +
                                                                              $"<br>Deze code is: <b>{vrfyCode}</b></br><br>Gebruik deze code in ons programma om uw wachtwoord te resetten.</br></h3></div></body></html>", name);
            verMail.mailMessage.AlternateViews.Add(verMail.htmlView);
            verMail.Client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            string userState = "Reservering";
            verMail.Client.SendAsync(verMail.mailMessage, userState);
            Console.WriteLine("Mail versturen...");
            Thread.Sleep(3000);
            verMail.mailMessage.Dispose();
        }
        catch (SmtpException ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    // sends a mail to the user if their reservation couldn't be altered
    public static void SendCancellationMail(string? email, string name)
    {
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
                $"<html><body><div><h1>Hallo {name}!</h1></div><div><p><h3>U heeft aangegeven dat u uw reservering verkeerd heeft ingevoerd. Helaas is het ons niet gelukt om uw gewenste tijd in te voeren. " +
                $"<br>U kunt zelf een nieuwe reservering invullen. Hopelijk zien wij u snel bij de Witte haven!</br></h3></div></body></html>", null, "text/html");
            myMail.AlternateViews.Add(htmlView);

            //Reply toList is what it says on the tin, the reply to option in mail can contain multiple emails
            myMail.ReplyToList.Add(replyTo);

            //What is the subject, the encoding, the message in the body and its encoding etc
            myMail.Subject = "Annulatie van reservering";
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
    //Adjusts mailsent depending on if sending the email was a succes or not
    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        String token = (string)e.UserState!;

        if (e.Cancelled)
        {
            Console.WriteLine("[{0}] Send canceled.", token);
            mailSent = false;
        }
        if (e.Error != null)
        {
            Console.WriteLine("[{0}] {1}", token, e.Error);
            mailSent = false;
        }
        else
        {
            Console.WriteLine($"Bericht verstuurd.");
            mailSent = true;

        }

    }
}