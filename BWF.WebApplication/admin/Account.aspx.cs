using System;
using AyrA.SQL;

public partial class admin_Account : System.Web.UI.Page
{
    protected string Err = string.Empty;
    protected bool Ok = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Base.IsAdmin(Session[Base.SESSION.ADMIN_LOGIN], Session[Base.SESSION.ADMIN_ID]))
        {
            Response.Redirect("./");
        }
        if (Base.Verify(Request.Params, "Slogoff"))
        {
            Response.Redirect("./Default.aspx?logoff=1");
        }
        if (Base.Verify(Request.Form, "Spwd1", "Spwd2"))
        {
            if (Base.ToString(Request.Form["pwd1"], string.Empty).Length > 5 && Base.ToString(Request.Form["pwd2"], string.Empty).Length > 5)
            {
                if (Base.ToString(Request.Form["pwd1"], string.Empty) == Base.ToString(Request.Form["pwd2"], string.Empty))
                {
                    SQLInterface SI = new SQLInterface(Base.DSN.ADMIN);
                    if (SI.Exec("UPDATE [Admin] SET [Password]=? WHERE [ID]=?", BCrypt.HashPassword(Request.Form["pwd1"], BCrypt.GenerateSalt()), Session[Base.SESSION.ADMIN_ID]) == 1)
                    {
                        Ok = true;
                    }
                    else
                    {
                        Err = "Unbekannter fehler beim Aktualisieren Ihres Passwortes";
                    }
                    SI.Dispose();
                }
                else
                {
                    Err = "Die Passwörter sind nicht identisch";
                }
            }
            else
            {
                Err="Das Passwort muss mindestend 6 Zeichen lang sein";
            }
        }
    }
}