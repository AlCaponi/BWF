using System;
using AyrA.SQL;
using System.Collections.Generic;

public partial class _Details : System.Web.UI.Page
{
    protected Base.Anlass A;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Base.IsAdmin(Session[Base.SESSION.ADMIN_LOGIN], Session[Base.SESSION.ADMIN_ID]))
        {
            Response.Redirect("./");
        }
        if (!string.IsNullOrEmpty(Request["ID"]) && Base.GetGuid(Request["ID"]) != Guid.Empty)
        {
            SQLInterface SI = new SQLInterface(Base.DSN.ADMIN);

            A = Base.GetAnlass(Base.GetGuid(Request["ID"]), SI);

            if (A != null)
            {
                A.Soldaten = Base.GetAnswers(Base.GetSoldaten(A.AnlassID, SI), SI);
            }
            else
            {
                //Nice try
                Response.Redirect("./");
            }
            SI.Dispose();
        }
        else
        {
            //Nice try
            Response.Redirect("./");
        }
    }
}
