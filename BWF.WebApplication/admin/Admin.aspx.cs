using System;
using AyrA.SQL;
using System.Collections.Generic;

public partial class _Admin : System.Web.UI.Page
{
    protected List<Base.Anlass> Liste;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Base.IsAdmin(Session[Base.SESSION.ADMIN_LOGIN],Session[Base.SESSION.ADMIN_ID]))
        {
            Response.Redirect("./");
        }
        Liste=new List<Base.Anlass>();
        SQLInterface SI = new SQLInterface(Base.DSN.ADMIN);

        SQLRow[] RR = SI.ExecReader("SELECT AnlassID FROM Anlass ORDER BY Datum DESC, Zeit DESC");

        foreach (SQLRow R in RR)
        {
            Base.Anlass A = Base.GetAnlass((Guid)R["AnlassID"], SI);
            A.Soldaten = Base.GetAnswers(Base.GetSoldaten(A.AnlassID, SI),SI);
            Liste.Add(A);
        }
        SI.Dispose();
        Base.DelExcel(Server.MapPath("/temp/"));
    }

    protected string UmfrageUrl()
    {
        return  string.Format("https://{0}/",Request.ServerVariables["HTTP_HOST"]);
    }
}
