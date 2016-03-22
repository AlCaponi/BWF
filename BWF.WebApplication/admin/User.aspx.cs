using System;
using AyrA.SQL;
using System.Collections.Generic;

public partial class _User : System.Web.UI.Page
{
    protected Base.Soldat S;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Base.IsAdmin(Session[Base.SESSION.ADMIN_LOGIN], Session[Base.SESSION.ADMIN_ID]))
        {
            Response.Redirect("./");
        }
        Guid G = Base.GetGuid(Request["ID"]);
        SQLInterface SI = new SQLInterface(Base.DSN.ADMIN);

        S = Base.GetSoldat(G,SI);
        S.Antworten = Base.FillAnswers(Base.GetFragen(SI), Base.GetAnswers(S.ID, SI));
        S.Problematic = Base.ProblematicSoldat(S);
        SI.Dispose();
    }
}
