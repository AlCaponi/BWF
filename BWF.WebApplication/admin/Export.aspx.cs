using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AyrA.SQL;
using System.IO;

public partial class admin_Export : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Base.IsAdmin(Session[Base.SESSION.ADMIN_LOGIN], Session[Base.SESSION.ADMIN_ID]))
        {
            Response.Redirect("./");
        }
        if (!string.IsNullOrEmpty(Request["ID"]) && Base.GetGuid(Request["ID"]) != Guid.Empty)
        {
            string P = "/temp/" + Guid.NewGuid().ToString() + ".xls";
            SQLInterface SI=new SQLInterface(Base.DSN.ADMIN);

            Base.Anlass A = Base.GetAnlass(Base.GetGuid(Request["ID"]), SI);
            Base.Frage[] FF = Base.GetFragen(SI);
            A.Soldaten = Base.GetAnswers(Base.GetSoldaten(A.AnlassID, SI), SI);

            string insertFormat = string.Join(",", string.Empty.PadLeft(FF.Length, '?').ToCharArray());
            ExcelInterface EI = new ExcelInterface(Server.MapPath(P));
            Response.Clear();
            EI.Exec(string.Format("CREATE TABLE [Export](SVNummer varchar(255),Vorname varchar(255),Nachname varchar(255),Problematic varchar(255),{0})", Fragen2Cols(FF)));
            EI.Exec(string.Format("INSERT INTO [Export] VALUES(NULL,NULL,NULL,NULL,{0})", insertFormat),Fragen2Insert(FF));
            foreach (Base.Soldat S in A.Soldaten)
            {
                EI.Exec(string.Format("INSERT INTO [Export] VALUES(?,?,?,?,{0})", insertFormat), S.SVNummer, S.Vorname, S.Nachname,S.Problematic?"Ja":"Nein",Poll2Values(S.Antworten));
            }
            EI.Dispose();
            SI.Dispose();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment; name=\"{0}.xls\"", A.Name.Replace('"', '\'')));
            Response.WriteFile(Server.MapPath(P), true);
            Response.Flush();
            try
            {
                Base.DelExcel(Server.MapPath("/temp/"));
            }
            catch
            {
            }
        }
    }

    private object[] Poll2Values(Base.Poll[] PP)
    {
        object[] Values = new string[PP.Length];
        for (int i = 0; i < PP.Length; i++)
        {
            Values[i] = PP[i].Antwort;
        }
        return Values;
    }

    private object[] Fragen2Insert(Base.Frage[] FF)
    {
        string[] Fragen = new string[FF.Length];

        for (int i = 0; i < FF.Length; i++)
        {
            Fragen[i] = FF[i].FrageText;
        }
        return Fragen;
    }

    private string Fragen2Cols(Base.Frage[] FF)
    {
        string[] Fragen = new string[FF.Length];

        for (int i = 0; i < FF.Length; i++)
        {
            Fragen[i] = string.Format("Frage{0:00} varchar(255)", i + 1);
        }
        return string.Join(",", Fragen);
    }

}