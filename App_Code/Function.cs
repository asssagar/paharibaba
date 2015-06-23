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
using System.Security.Cryptography;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;


/// <summary>
/// Summary description for Function
/// </summary>
public class Function
{
	public Function()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string UrlExtension
    {
        get
        {
            Regex rx = new Regex(".([A-Z0-9a-z-]+)$");
            string strExpression = Function.GetConfigSettings("permaexpression", "setting");
            Match matchExt = rx.Match(strExpression);
            if (rx.Match(strExpression).Success)
                return matchExt.Value;
            else
                return string.Empty;
        }
    }
    public static string getMobileNo(string regno)
    {
        string query = "SELECT contact from  studentprofile where regno = '" + regno + "'";
        DataSet ds = Database.get_DataSet(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0][0].ToString();
        }
        else
            return "";
    }

    public static string getParentMobileNo(string regno)
    {
        string query = "SELECT pcontact from  studentprofile where regno = '" + regno + "'";
        DataSet ds = Database.get_DataSet(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0][0].ToString();
        }
        else
            return "";
    }
    public static string SendSMS(string mobileNo,string MSG)
    {
        string sUserID = ConfigurationManager.AppSettings["SMSID"].ToString();
        string sPwd = ConfigurationManager.AppSettings["SMSPWD"].ToString();
        string[] mobileArr = mobileNo.Split(',');
        string number = "";
        for (int i = 0; i < mobileArr.Length; i++)
        {
            if (i == mobileArr.Length-1)
            {
                if (mobileArr[i].ToString().Length == 10)
                {
                    number += "91" + mobileArr[i];
                }
                else
                {
                    number += mobileArr[i];
                }
            }
            else
            {
                if (mobileArr[i].ToString().Length == 10)
                {
                    number += "91" + mobileArr[i] + ",";
                }
                else
                {
                    number += mobileArr[i] + ",";
                }
            }
            
        }
        string sNumber = number;
        string sSID = "WebSMS";
        string sMessage = MSG;
        string sURL = "http://smslane.com/vendorsms/pushsms.aspx?user=" + sUserID + "&password=" + sPwd + "&msisdn=" + sNumber + "&sid=" + sSID + "&msg=" + sMessage + "&mt=0&fl=0";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
        request.MaximumAutomaticRedirections = 4;
        request.Credentials = CredentialCache.DefaultCredentials;
        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string sResponse = readStream.ReadToEnd();
            response.Close();
            readStream.Close();
            int count = 0;
            if (MSG.Length <= 159)
            {
                count = 1;
            }
            else
            {
                if ((MSG.Length >= 160) && (MSG.Length <= 319))
                {
                    count = 2;
                }
                else
                    if ((MSG.Length >= 320) && (MSG.Length <= 480))
                    {
                        count = 3;
                    }
                    else
                    { count = 4; }

            }
            string query = "INSERT INTO SMSLOG (mobileNo, msg, sdate, status,msgCount) VALUES ('" + mobileNo + "','" + MSG + "','" + System.DateTime.Now.ToShortDateString() + "','" + sResponse + "','" + count + "')";
            string s = Database.Execute(query);
            return sResponse;
        }
        catch
        {
            return "";
        }
    }
    public static string getStudentName()
    {
        string query = "SELECT sname From studentProfile WHERE regno = '" + Users.ActiveUser.UserName + "'";
        DataSet ds = Database.get_DataSet(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0][0].ToString();
        }
        else
            return "";
    }
    public static Char[] Chars = "ABCDEFGHIJKLMNPQRSTUVWXYZ1234567890".ToCharArray();
    public static string GetRandomStr(int stringSize)
    {
        String returns = "";
        Random Rnd = new Random();
        for (int i = 0; i < stringSize; i++)
        {
            returns += Chars[Rnd.Next(Chars.Length)];
        }
        return returns;
    }
    public static string GetNowDate(DateTime dT)
    {
        if (dT == null)
        {
            dT = DateTime.Now;
        }
        string NowDate = dT.ToString("yyyy-MM-dd HH:mm:ss");
     
        return NowDate;
    }

   
    public static string GetMd5Hash(string value)
    {
        MD5 md5Hasher = MD5.Create();
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
        StringBuilder sB = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sB.Append(data[i].ToString("x2"));
        }
        return sB.ToString();
    }
    public static string GetConfigSettings(string nodeName, string Region)
    {
        string s1;
        string path = HttpContext.Current.Request.PhysicalApplicationPath;
        path = String.Format("{0}\\{1}.config", path, Region);
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(path);
        XmlNodeList node = xDoc.GetElementsByTagName(nodeName);
        bool flag = node.Count <= 0;
        if (!flag)
            s1 = node[0].Attributes[0].InnerXml;
        else
            s1 = String.Empty;
        return s1;
    }
    public static string GetConfigSettings(string nodeName, string Region,string attribute)
    {
        string s1;
        string path = HttpContext.Current.Request.PhysicalApplicationPath;
        path = String.Format("{0}\\{1}.config", path, Region);
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(path);
        XmlNodeList node = xDoc.GetElementsByTagName(nodeName);
        bool flag = node.Count <= 0;
        if (!flag)
            s1 = node[0].Attributes[attribute].InnerXml;
        else
            s1 = String.Empty;
        return s1;
    }
    public static string GenerateRandom6Digits()
    {
        Random objRandom = new Random();
        int iRandomValue = objRandom.Next(100000, 999999);
        string cValue = iRandomValue.ToString();
        return cValue;
    }
    public static void CreateLoginCookie(string UserName, string Password)
    {
        HttpCookie UserInfo = new HttpCookie("SITUserInfo");

        //Encrypt cookie username value
        //When we retrieve the username cookie value we will have to decrypt.
        //The decyption is done in the Athentication class.
        UserInfo.Values["SITUsername"] = Function.GetMd5Hash(UserName);

        //Encrypt cookie password value
        UserInfo.Values["SITUpass"] = Function.GetMd5Hash(Password);

        UserInfo.Expires = DateTime.Now.AddDays(15);
        HttpContext.Current.Response.Cookies.Add(UserInfo);
    }
    public static string doPaging(int CurrentPage, int totelRows, int pageSize)
    {
        string links = "";

        int rowCount = totelRows;
        int pageCount = (rowCount / pageSize);
        pageCount += (rowCount % pageSize) > 0 ? 1 : 0;
        pageCount = pageSize > rowCount || pageSize == rowCount ? 1 : pageCount;
        int realCurrentPage = CurrentPage;
        CurrentPage = CurrentPage > pageCount ? 1 : CurrentPage;

        string startbutton = "<li><a id='first' href='javascript:void(0)' title='1' ><img src='images/navigation-180.png' width='16' height='16'>First</a></li>";
        string endbutton = "<li><a id='last'  href='javascript:void(0)' title='" + pageCount + "' >Last »</a></li>";
        int startItem = CurrentPage - 2;
        int endItem = CurrentPage + 2;
        if (startItem <= 0)
        {
            startItem = 1;
            endItem = pageCount >= 5 ? 5 : pageCount;
            startbutton = CurrentPage > 1 ? startbutton : "";
            endbutton = CurrentPage == pageCount ? "" : endbutton;
        }
        else
        {
            if (endItem >= pageCount)
            {
                endItem = pageCount;
                startItem = pageCount >= 5 ? pageCount - 4 : 1;
                endbutton = "";
            }
        }
        links += startbutton;
        for (int i = startItem; i <= endItem; i++)
        {
            if (i == CurrentPage)
            {
                links += "<li><a class='current'  href='javascript:void(0)' title='" + i + "'>" + i + "</a></li>";
            }
            else
            {
                links += "<li><a class='number'  href='javascript:void(0)' title='" + i + "'>" + i + "</a></li>";
            }
        }
        links += endbutton;
        return links;
    }
    public static string doPaging(int CurrentPage, int totelRows, int pageSize, string url)
    {
        string links = "";

        int rowCount = totelRows;
        int pageCount = (rowCount / pageSize);
        pageCount += (rowCount % pageSize) > 0 ? 1 : 0;
        pageCount = pageSize > rowCount || pageSize == rowCount ? 1 : pageCount;
        int realCurrentPage = CurrentPage;
        CurrentPage = CurrentPage > pageCount ? 1 : CurrentPage;

        string startbutton = "<a id='first' href='" + url + "page=1' >« First</a>";
        string endbutton = "<a id='last'  href='" + url + "page=" + pageCount + "' >Last »</a>";
        int startItem = CurrentPage - 2;
        int endItem = CurrentPage + 2;
        if (startItem <= 0)
        {
            startItem = 1;
            endItem = pageCount >= 5 ? 5 : pageCount;
            startbutton = CurrentPage > 1 ? startbutton : "";
            endbutton = CurrentPage == pageCount ? "" : endbutton;
        }
        else
        {
            if (endItem >= pageCount)
            {
                endItem = pageCount;
                startItem = pageCount >= 5 ? pageCount - 4 : 1;
                endbutton = "";
            }
        }
        links += startbutton;
        for (int i = startItem; i <= endItem; i++)
        {
            if (i == CurrentPage)
            {
                links += "<span   class='number current' >" + i + "</span>";
            }
            else
            {
                links += "<a class='number'  href='" + url + "page=" + i + "'>" + i + "</a>";
            }
        }
        links += endbutton;
        return links;
    }
    public static double MilliTimeStamp()
    {
        DateTime d1 = new DateTime(1970, 1, 1);
        DateTime d2 = DateTime.UtcNow;
        TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
        return ts.TotalMilliseconds;
    }
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
            NetworkCredential SMTPUserInfo = new NetworkCredential(ConfigurationManager.AppSettings["email"].ToString(),ConfigurationManager.AppSettings["password"].ToString());
            client.Credentials = SMTPUserInfo;
            client.Host = "smtp.gmail.com";
           // client.Port = System.Convert.ToInt32(Function.GetConfigSettings("Port", "reseller"));
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(mail);
            return true;
        }
        catch (Exception ex) { return false; }
    }

    public static bool SendEmailWithAttachment( string emailto, string emailfrom, string attachment, string subject, string body)
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
            NetworkCredential SMTPUserInfo = new NetworkCredential(ConfigurationManager.AppSettings["email"].ToString(),ConfigurationManager.AppSettings["password"].ToString());
            client.Credentials = SMTPUserInfo;
            client.Host ="" ;
            client.Port =587;
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
    //public static void SendEmail(string emailto, string emailfrom, string subject, string body)
    //{
    //    MailMessage mail = new MailMessage();
    //    mail.Headers.Add("Reply-To", emailfrom);
    //    //bool flag = !Globals.CurrentLicense.ParentLicense.bEmailAlias;
    //    //if (!flag)
    //    //    mail.From = Globals.EmailFrom;
    //    //else
    //    mail.From = emailfrom;
    //    mail.To = emailto;
    //    mail.Subject = subject;
    //    mail.Body = body;
    //    mail.BodyFormat = MailFormat.Html;
    //    SmtpMail.SmtpServer = "localhost";
    //    SmtpMail.Send(mail);
    //System.Configuration.ConfigurationManager.AppSettings["Provider"]
    //}
    public static bool isNumeric(string value)
    {
        int result;
        return int.TryParse(value, out result);
    }



    public static bool contactFormMailer(string name, string mobile, string email, string message)
    {
        String Email_Body = "";
        string path = HttpContext.Current.Server.MapPath("~\\ETemplates\\contact.htm");
        StreamReader sr = new StreamReader(path);
        Email_Body = sr.ReadToEnd();
        Email_Body = Email_Body.Replace("**##Name##**", name);
        Email_Body = Email_Body.Replace("**##Mobile No##**", mobile);
        Email_Body = Email_Body.Replace("**##Email ID##**", email);
        Email_Body = Email_Body.Replace("**##Message##**", message);

        //Add email template for user
        String User_Email_Body = "";
        string user_path = HttpContext.Current.Server.MapPath("~\\ETemplates\\user_contact.htm");
        StreamReader user_sr = new StreamReader(user_path);
        User_Email_Body = user_sr.ReadToEnd();
        User_Email_Body = User_Email_Body.Replace("**##Name##**", name);
        Function.SendEmail(Function.GetConfigSettings("EmailTo", "Reseller"), email, Function.GetConfigSettings("CompanyName", "Reseller"), Email_Body);
        Function.SendEmail(email, Function.GetConfigSettings("EmailFrom", "Reseller"), Function.GetConfigSettings("CompanyName", "Reseller"), User_Email_Body);
        return true;
    }


    public static string API(string API)
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
            return s;
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
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
    public static string UrlIsValid(string smtpHost)
    {
        string pattern = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        if (!Regex.IsMatch(smtpHost, pattern))
        {
            return "1";
        }
        else
        {
            return "0";
        }

    }

    public static int ValidateNumber(string number)
    {
        try { int i = 0; int s = int.Parse(number); return 0; }
        catch (Exception) { return 1; }
    }
    public static int ValidateDateFormate(string datetext)
    {
        try
        {
            DateTime dt = DateTime.Parse(datetext);
            return 0;
        }
        catch (Exception)
        {
            return 1;
        }
    }

    public static bool CheckEmail(string strEmail)
    {
        Regex rex = new Regex("^[\\w\\.=-]+@[\\w\\.-]+\\.[\\w]{2,3}$");
        return rex.IsMatch(strEmail);
    }

    public static int ValidateDateRenge(string selecteddate)
    {
        try
        {
            DateTime dt = DateTime.Parse(selecteddate);
            if (dt.Date <= DateTime.Today)
            {
                return 1;
            }
            return 0;
        }
        catch (Exception) { return 1; }
    }





    public static string Encrypt(string strText)
    {
        byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
        try
        {
            byte[] bykey = System.Text.Encoding.UTF8.GetBytes("$@MPURN!+".Substring(0, 8));
            byte[] InputByteArray = System.Text.Encoding.UTF8.GetBytes(strText);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write);
            cs.Write(InputByteArray, 0, InputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
    public static string Decrypt(string strText)
    {
        byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
        byte[] inputByteArray = new byte[strText.Length + 1];
        try
        {
            byte[] byKey = System.Text.Encoding.UTF8.GetBytes("$@MPURN!+".Substring(0, 8));// Strings.Left(sDecrKey, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            // s = encoding.GetString(ms.ToArray());
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    
    //public static List<Function.Package> getPackageHomePage(int Records)
    //{
    //    string query = "select TOP " + Records + " pname, intro, simage,pid,details from executive.package ORDER BY pid";
    //    List<Function.Package> _package = Function.getPackage(query);
    //    return _package;
    //}

   
   
    public static string GetClientIPAddress(System.Web.HttpRequest httpRequest)
    {
        string OriginalIP = string.Empty;
        string RemoteIP = string.Empty;
        OriginalIP = httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"]; //original IP will be updated by Proxy/Load Balancer.
        RemoteIP = httpRequest.ServerVariables["REMOTE_ADDR"]; //Proxy/Load Balancer IP or original IP if no proxy was used
        if (OriginalIP != null && OriginalIP.Trim().Length > 0)
        {
            return OriginalIP + "(" + RemoteIP + ")"; //Lets return both the IPs.
        }
        return RemoteIP;
    }

   public static bool CheckLogin(string username, string password)
    {
        bool res = false;
        string query = "select password,status, srole from login_tb where username = '" + username + "'";
        DataSet ds = Database.get_DataSet(query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (password == ds.Tables[0].Rows[0][0].ToString())
            {
                if (ds.Tables[0].Rows[0][1].ToString() == "1")
                {
                   
                    res = true;

                    Users u = new Users();
                    u.Role = ds.Tables[0].Rows[0][2].ToString();
                    u.UserName = username;
                    HttpContext.Current.Session["ActiveUser"] = u;
                }
               
               
            }
           
        }
       
        return res;
    }

}
