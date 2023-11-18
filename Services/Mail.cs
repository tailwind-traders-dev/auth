namespace Tailwind.Auth.Services;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

public class Outbox 
{


  public static async Task<bool> SendNow(string to, string from, string subject, string html)
  {
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
     
    var host = Environment.GetEnvironmentVariable("SMTP_HOST");
    var user = Environment.GetEnvironmentVariable("SMTP_USER");
    var pw = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
    var port = 465;
    if (env == "Integration"){
      host="smtp.ethereal.email";
      user = Environment.GetEnvironmentVariable("ETHEREAL_USER");
      pw = Environment.GetEnvironmentVariable("ETHEREAL_PASSWORD");
      port = 587;
    }
    
    var sendMessage = new MailMessage{
      IsBodyHtml = true,
      Subject = subject,
      Body = html,
      From=new MailAddress(from),
      To = {new MailAddress(to)}
    };
    
    if (env == "Integration" || env == "Production"){
      Console.WriteLine("Sending email");
      var _client = new SmtpClient(host, port);
      _client.Credentials = new NetworkCredential(user, pw);
      _client.UseDefaultCredentials = false;
      _client.EnableSsl = true;
      await _client.SendMailAsync(sendMessage);
      //TODO: Log this!

    }
    return true;
  }

}