using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;

/// <summary>
/// Summary description for email
/// </summary>
public class email
{
	public email()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string sendmsg_toemail(string sender_name, string sender_email, string receiver_name, string receiver_email, string subject, string mymsg,string username,string password)
    {
        try

        {    // Create Mail Message Object with content that you want to send with mail.
            
           MailMessage MyMailMessage = new MailMessage();
          
           
           MailAddressCollection adcol = new MailAddressCollection();
           
            
                MyMailMessage.From = new MailAddress(sender_email, sender_name);
                //MyMailMessage.ReplyTo = new MailAddress(sender_email, sender_name);
                MyMailMessage.To.Add(new MailAddress(receiver_email, receiver_name));
                MyMailMessage.Subject =subject;
                MyMailMessage.Body = mymsg;
                MyMailMessage.IsBodyHtml = true;
            // Proper Authentication Details need to be passed when sending email from gmail
                System.Net.NetworkCredential mailAuthentication = new
                System.Net.NetworkCredential(username , password);

                // Smtp Mail server of Gmail is "smpt.gmail.com" and it uses port no. 587
                // For different server like yahoo this details changes and you can
                // Get it from respective server.
                System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);

                // Enable SSL
                mailClient.EnableSsl = true;

                mailClient.UseDefaultCredentials = false;

                mailClient.Credentials = mailAuthentication;

                mailClient.Send(MyMailMessage);
                return "Done";

            
        }
        catch (System.Net.Mail.SmtpException ex)
        {
            string value=ex.Message.ToString();
            if (ex.InnerException.ToString().StartsWith("The remote name could not be resolved"))
            {
                return "<center><b style='color:red'>Please check your network connection.</b></center>";
            }
            else if (ex.Message.ToString().StartsWith("Failure sending mail"))
            {
                return "<center><b style='color:red'>Oops! some error occured. Please try later</b></center>";
            }
            else
            {
                return "<center><b style='color:red'>Oops! some error occured. Please try later</b></center>";
            }

        }
    }
}
