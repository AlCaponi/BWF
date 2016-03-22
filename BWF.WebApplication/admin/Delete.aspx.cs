using System;
using AyrA.SQL;
using System.Collections.Generic;

public partial class _Delete : System.Web.UI.Page
{
    protected struct Anlass
    {
        public Guid ID;
        public string Name;
        public int Teilnehmer;
        public int Beantwortet;
        public int Erfolg;
        public DateTime Datum;
    }

    protected List<Anlass> Liste;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Base.IsAdmin(Session[Base.SESSION.ADMIN_LOGIN],Session[Base.SESSION.ADMIN_ID]))
        {
            Response.Redirect("./");
        }
        Liste=new List<Anlass>();
        SQLInterface SI = new SQLInterface(Base.DSN.ADMIN);

        Guid G = Base.GetGuid(Request["ID"]);

        SQLRow[] RR = SI.ExecReader("SELECT * FROM Anlass WHERE AnlassID=?", G);

        if (RR.Length == 1)
        {
            if (!string.IsNullOrEmpty(Request["confirm"]))
            {
                SI.Exec("DELETE FROM SoldatAntwort WHERE AnlassID=?", G);
                SI.Exec("DELETE FROM Soldat WHERE AnlassID=?", G);
                SI.Exec("DELETE FROM Anlass WHERE AnlassID=?", G);
                Response.Redirect("Admin.aspx");
            }
        }
        else
        {
            //invalid guid should not happen
            Response.Redirect("Admin.aspx");
       }
    }
}
