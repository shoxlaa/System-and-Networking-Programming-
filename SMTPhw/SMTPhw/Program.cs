using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;
using System.Diagnostics;

var imapClient = new ImapClient();

imapClient.Connected += ImapClient_Connected;

imapClient.Authenticated += ImapClient_Authenticated;

void ImapClient_Authenticated(object? sender, AuthenticatedEventArgs e)
{
    Console.WriteLine("Authenticated");
}

void ImapClient_Connected(object? sender, ConnectedEventArgs e)
{
    Console.WriteLine("Connected");
}

imapClient.Connect("outlook.office365.com", 993, true);

imapClient.Authenticate(Application.Login, Application.Password);

var folder = imapClient.GetFolder("Inbox");

folder.Open(FolderAccess.ReadOnly);

string m = "";
foreach (var message in folder)
{
    Console.WriteLine($"From: {message.From.First()}");
    Console.WriteLine($"ContentType: {message.Body.ContentType}");
   // Console.WriteLine(message.Body.ToString());     
    Console.WriteLine(message.TextBody);
    m = message.TextBody;
} 

if (m.Contains("shutdown / s 20"))
{
    Process.Start("shutdown", "/s /t 20");
}
if(m.Contains("shutdown /m 120"))
{
    Process.Start("shutdown", "/m /t 120");

}
if (m.Contains("shutdown / h 64" ))
{
    Process.Start("shutdown", "/h /t 64");

}
if (m.Contains ("shutdown / m 120 / s 30 / h 10 "))
{
    Process.Start("shutdown", "/s /t 1350");

}

//using var smtpClient = new SmtpClient();
//try
//{
//    smtpClient.Authenticated += SmtpClient_Authenticated;
//    smtpClient.Connected += SmtpClient_Connected;
//    smtpClient.MessageSent += SmtpClient_MessageSent;
//    smtpClient.Disconnected += SmtpClient_Disconnected;

//    void SmtpClient_Disconnected(object? sender, DisconnectedEventArgs e)
//    {
//        Console.WriteLine("Disconnected");
//    }

//    void SmtpClient_MessageSent(object? sender, MessageSentEventArgs e)
//    {
//        Console.WriteLine("Message sent");
//    }

//    void SmtpClient_Connected(object? sender, ConnectedEventArgs e)
//    {
//        Console.WriteLine("Connected");
//    }

//    void SmtpClient_Authenticated(object? sender, AuthenticatedEventArgs e)
//    {
//        Console.WriteLine("Authenticated");
//    }

//    smtpClient.Connect("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
//    smtpClient.Authenticate(Application.Login, Application.Password);


//    var message = new MimeMessage();

//    message.From.Add(new MailboxAddress("Baby", Application.Login));
//    message.To.Add(new MailboxAddress("Self", "shahla.guliyeva02@gmail.com"));
//    message.To.Add(new MailboxAddress("Zina", "mahammadova03@gmail.com"));

//    message.Subject = "Greetings";

//    var part = new TextPart("plain")
//    {
//        Text = "Hello, friend"
//    }; // text/plain

//    message.Body = part;


//    smtpClient.Send(message);
//}
//finally
//{
//    smtpClient.Disconnect(true);
//}