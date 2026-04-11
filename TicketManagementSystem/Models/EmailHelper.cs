//using System;
//using System.Net;
//using System.Net.Mail;

//public static class EmailHelper
//{
//    private static string SmtpServer = "smtp.gmail.com";
//    private static int SmtpPort = 587;
//    private static string SenderEmail = "nepalmerodesh185@gmail.com";
//    private static string SenderPassword = "tgel seii xsti liic"; 
//    private static bool EnableSsl = true;

//    public static void SendEmail()
//    {
//        var subject = "TestEmail";
//        var body = "This is a test email sent from C# application.";
//        var toEmail = "rujasubba123@gmail.com"; 
//        try
//        {
//            using (var client = new SmtpClient(SmtpServer, SmtpPort))
//            {
//                client.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
//                client.EnableSsl = EnableSsl;

//                var mail = new MailMessage
//                {
//                    From = new MailAddress(SenderEmail),
//                    Subject = subject,
//                    Body = body,
//                    IsBodyHtml = true
//                };

//                mail.To.Add(toEmail);

//                client.Send(mail);
//            }

//            Console.WriteLine("Email sent successfully.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Error sending email: " + ex.Message);
//        }
//    }
//}




using System.Net;
using System.Net.Mail;

public static class EmailHelper
{
    private static string SmtpServer = "smtp.gmail.com";
    private static int SmtpPort = 587;
    private static string SenderEmail = "nepalmerodesh185@gmail.com";
    private static string SenderPassword = "tgel seii xsti liic";
    private static bool EnableSsl = true;

    public static void SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            using (var client = new SmtpClient(SmtpServer, SmtpPort))
            {
                client.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
                client.EnableSsl = EnableSsl;

                var mail = new MailMessage
                {
                    From = new MailAddress(SenderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mail.To.Add(toEmail);

                client.Send(mail);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Email sending failed: " + ex.Message);
        }
    }
}