using backend.Model;
using MailKit.Net.Smtp;
using MimeKit;

namespace backend.Service;

public class EmailService
{
    
    public void SendEmail(User user)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("PcForum","EmailPcForumOrganisation"));
        
        message.To.Add(new MailboxAddress(user.Username,user.Email));

        message.Subject = "Velkommen til forummet";

        var body = new TextPart("plain")
        {
            Text = "Velkommen til forummet " + user.Username +
                   ". Vi glæder os til, at du tager del i vores community. Hilsen Moderator teamet."
        };

        message.Body = body;

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 465, true);
            
            client.Authenticate(Environment.GetEnvironmentVariable("fromemail"),Environment.GetEnvironmentVariable("frompass"));
            client.Send(message);
            client.Disconnect(true);
        }

    }
}