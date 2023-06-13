
using System.Net;
using System.Net.Mail;
using System.ComponentModel;
//Created this class to make it easier to create emails
public class EmailModel
{
    public SmtpClient Client;
    private MailAddress from;
    private MailAddress to;
    public MailMessage mailMessage;
    public MailAddress replyTo;
    public AlternateView htmlView;
    private string htmlBody;
    private string Name;
    public NetworkCredential AuthenticationInfo;

    public EmailModel(string? email,string subject,string emailBody,string name ="")
    {
        Client = new SmtpClient("smtp.gmail.com", 587);;
        from = new MailAddress("testrestaurant12356789@gmail.com", "Restaurant");
        to = new MailAddress(email, $"{email}");
        mailMessage = new MailMessage(from, to);
        Name = name;
        replyTo = new MailAddress("testrestaurant12356789@gmail.com");
        htmlBody = emailBody;
        htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");
        AuthenticationInfo = new NetworkCredential("restaurant1234567891011@gmail.com", "vqxjoomtkvrjmnxu");
        Client.UseDefaultCredentials = false;
        Client.Credentials = AuthenticationInfo; 
        mailMessage.Subject = subject;
        mailMessage.ReplyToList.Add(replyTo);
        mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
        mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
        mailMessage.IsBodyHtml = true;
        Client.EnableSsl = true;
    }
}