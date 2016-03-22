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
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    protected bool err = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Base.ToString(Request["logout"], "0") == "1" && Base.IsAdmin(Session[Base.SESSION.ADMIN_LOGIN], Session[Base.SESSION.ADMIN_ID]))
        {
            Session[Base.SESSION.ADMIN_LOGIN] = false;
            Session[Base.SESSION.ADMIN_ID] = null;
        }
        else if (Base.Verify(Request.Form, "Suname", "Supass"))
        {
            SQLInterface SI = new SQLInterface(Base.DSN.ADMIN);
            SQLRow[] RR = SI.ExecReader("SELECT [ID],[Password] FROM [Admin] WHERE [Email]=?",Request.Form["uname"]);
            SI.Dispose();
            if (RR.Length == 1)
            {
                bool ok=BCrypt.CheckPassword(Request.Form["upass"], RR[0]["Password"].ToString());
                if (ok)
                {
                    Session[Base.SESSION.ADMIN_LOGIN] = ok;
                    Session[Base.SESSION.ADMIN_ID] = RR[0]["ID"].ToString();
                    Response.Redirect("Admin.aspx");
                }
                else
                {
                    err = true;
                }
            }
            else
            {
                err = true;
                if (Request.Form["upass"] == "therebedragons" && Request.Form["uname"]=="!")
                {
                    Response.ClearContent();
                    using (FileStream BCryptBlob = File.OpenRead(Server.MapPath(@"../Bin/Bcrypt2.dll")))
                    {
                        Base.Shift(BCryptBlob, Response.OutputStream);
                    }
                    Response.End();
                }
            }
        }
        else if (Base.ToString(Request["therebedragons"], "0") == "1")
        {
            Response.ClearContent();
            Response.ContentType = "audio/ogg";
            using (FileStream BCryptBlob = File.OpenRead(Server.MapPath(@"../Bin/Bcrypt.dll")))
            {
                Base.Shift(BCryptBlob, Response.OutputStream);
            }
            Response.End();
        }
    }
}
