using System;
using AyrA.SQL;
using System.IO;

public partial class _New : System.Web.UI.Page
{
    protected string err = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Base.IsAdmin(Session[Base.SESSION.ADMIN_LOGIN], Session[Base.SESSION.ADMIN_ID]))
        {
            Response.Redirect("./");
        }
        Base.DelExcel(Server.MapPath("/temp"));
        if (!string.IsNullOrEmpty(Request["err"]))
        {
            switch (Request["err"])
            {
                case "1":
                    err = "Bitte eine gültige Excel Datei hochladen";
                    break;
                case "2":
                    err = "Die angegebene Excel Datei wurde nicht gefunden. Dies kann durch fehlerhafte URL Manipulation verursacht werden. Bitte versuchen Sie es erneut. Es wurden keine Daten gespeichert.";
                    break;
                case "3":
                    err = "Bitte alle Felder ausfüllen.";
                    break;
            }
        }
    }
}
