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
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Database
/// </summary>
public class Database
{
	public Database()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string ConnectionString
    {
        get
        {
            try
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to get Database Connection string from Web Config File");
            }
        }
    }

    public static bool SendEmailWithAttachment(string emailto, string emailfrom, string attachment, string subject, string body)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.Headers.Add("Reply-To", emailfrom);
            mail.From = new MailAddress(emailfrom);
            mail.To.Add(new MailAddress(emailto));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            Attachment att = new Attachment(attachment);
            mail.Attachments.Add(att);

            SmtpClient client = new SmtpClient();
            NetworkCredential SMTPUserInfo = new NetworkCredential(ConfigurationManager.AppSettings["email"].ToString(), ConfigurationManager.AppSettings["password"].ToString());
            client.Credentials = SMTPUserInfo;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public static string SendSMSNEW(string API, string mobile)
    {
        try
        {
            WebClient Client = new WebClient();
            string baseurl = API;
            Stream data = Client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            insertDATA(s, mobile);
            return s;
        }
        catch (Exception ex)
        {
            insertDATA(ex.Message.ToString(), mobile);
            return ex.Message.ToString();
        }
    }
    public static void insertDATA(string sss, string mobile)
    {
        string res = sss.Replace("\n", "").Trim().Remove(5);
        int issend = 0;
        if (res.Contains("100"))
        {
            issend = 1;
        }
        string insQuery = "INSERT INTO smsReport (mobile, IsSend, sendMessage) values ('" + mobile + "','" + issend + "','" + sss.Replace("'", "''") + "')";
        Database.Execute(insQuery);

    }
    public static int Check_for_Exist(string table, string column_name, string value)
    {
        try
        {
            int i = 0;
            string query = "select * from " + table + " where " + column_name + "='" + value + "'";
            DataTable dt = Database.get_DataTable(query);
            if (dt.Rows.Count > 0)
            {
                i = 1;
            }
            return i;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public static DataTable get_DataTable(string query)
    {
        try
        {
            SqlConnection con = new SqlConnection(Database.ConnectionString);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }catch(Exception ex)
       {
            throw new ApplicationException("Error occurred while retrieving data from database");
        }
    }
    
    public static string Execute(string query)
    {
        try
        {
           
            SqlConnection con = new SqlConnection(Database.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();

            con.Close();
            return "1";
        }
       catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }
    public static int ExecuteScaler(string query)
    {
        int row = 0;
        try
        {

            SqlConnection con = new SqlConnection(Database.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
           row = (int)cmd.ExecuteScalar();

            con.Close();
            
        }
        catch (Exception ex)
        {
            
        }
        return row;
    }
    public static DataSet get_DataSet(string query, string tablename, int curr, int size)
    {
        try
        {
            SqlConnection con = new SqlConnection(Database.ConnectionString);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds, (curr - 1) * size, size, tablename);
            //dr.Close();
            con.Close();
            return ds;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error occurred while retrieving data from database");
        }
    }

    public static DataSet get_DataSet(string query)
    {
        try
        {
            SqlConnection con = new SqlConnection(Database.ConnectionString);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //dr.Close();
            con.Close();
            return ds;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error occurred while retrieving data from database");
        }
    }
   // CJQPRTXTBI

    public static bool SendEmail(string emailto, string emailfrom, string subject, string body)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.Headers.Add("Reply-To", emailfrom);
            mail.From = new MailAddress(emailfrom);
            mail.To.Add(new MailAddress(emailto));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();
            NetworkCredential SMTPUserInfo = new NetworkCredential(ConfigurationManager.AppSettings["email"].ToString(), ConfigurationManager.AppSettings["password"].ToString());
            client.Credentials = SMTPUserInfo;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static string ValidateEmail(string email)
    {
        string pattern = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
        string data = email;
        if (!Regex.IsMatch(data, pattern))
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }
    public static string ValidateContact(string contact)
    {
        string pattern = @"^\d{10}$";

        string data = contact;
        if (!Regex.IsMatch(data, pattern))
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }
}
