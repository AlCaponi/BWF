using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AyrA.SQL;

public partial class _Register : System.Web.UI.Page
{
    protected string err = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Base.Verify(Request.Form, "Suname", "Supass", "Smaster"))
        {
            if (BCrypt.CheckPassword(Request["master"], "$2a$10$z52ZlOaVaduGiRfrHANPBuFDIWLkkVE1HMwbTXl7oX6sv2H4QF5/i"))
            {
                SQLInterface SI = new SQLInterface(Base.DSN.ADMIN);
                SQLRow[] RR = SI.ExecReader("SELECT [ID],[Password] FROM [Admin] WHERE [Email]=?", Request.Form["uname"]);
                if (RR.Length == 1)
                {
                    err = "Benutzer existiert bereits";
                }
                else
                {
                    SI.Exec("INSERT INTO [Admin] (ID,Email,Password) VALUES(NEWID(),?,?)", Request.Form["uname"], BCrypt.HashPassword(Request.Form["upass"], BCrypt.GenerateSalt()));
                    Response.Redirect("./");
                }
                SI.Dispose();
            }
            else
            {
                err = "Ungültiges Master Passwort";
            }
        }
        /*
        string tmp = BCrypt.GenerateSalt();
        string pwd = BCrypt.HashPassword("DINGENS", tmp);
        Response.Write(string.Format("SALT: {0}; PWD: {1}", tmp, pwd));
        */
    }
}
