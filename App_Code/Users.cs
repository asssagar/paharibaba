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

/// <summary>
/// Summary description for Users
/// </summary>
public class Users
{
	public Users()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string _UserID;
  
    public string UserID
    {
        get { return _UserID; }
        set { _UserID = value; }
    }
    private string _UserName;

    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }
    private string _Email;

    public string Email
    {
        get { return _Email; }
        set { _Email = value; }
    }
   
    private string _Role;

    public string Role
    {
        get { return _Role; }
        set { _Role = value; }
    }
    public static string ValidateEmail(string Email)
    {

        string UserID = "";
        string query = "select [userid] from Login where email = '" + Email + "'";
        DataTable DT = Database.get_DataTable(query);
        if ((int)DT.Rows.Count > 0)
        {
            DataRow DR = DT.Rows[0];
            UserID = DR["userid"].ToString();
        }
        else
        {
            UserID = null;
        }
        return UserID;
    }
    public static DataTable ValidateUser(string UserName, string Password)
    {
        DataTable DT = Database.get_DataTable("select * from [Login] where [username] = '" + UserName + "' and [Password] = '" + Password + "' and [status] = 'active'");
        if (DT.Rows.Count > 0)
        {
            DataRow DR = DT.Rows[0];
            if (!(UserName.Equals(DR["username"].ToString()) && Password.Equals(DR["password"].ToString())))
            {
                DT.Clear();
            }
        }
        return DT;
    }
    public static Users ActiveUser
    {
        get { return (Users)HttpContext.Current.Session["ActiveUser"]; }
        set { HttpContext.Current.Session["ActiveUser"] = value; }
    }

}
