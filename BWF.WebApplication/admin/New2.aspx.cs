using System;
using AyrA.SQL;
using System.IO;
using System.Collections.Generic;
using System.Text;

public partial class _New2 : System.Web.UI.Page
{
    protected int FieldCount;
    protected struct TableInfo
    {
        public string TableName;
        public string[] Fields;
    }
    protected List<TableInfo> Tables = new List<TableInfo>();
    protected string err = string.Empty;
    protected string ExcelFile = string.Empty;
    protected string Anlass = string.Empty;
    protected string Datum = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Base.IsAdmin(Session[Base.SESSION.ADMIN_LOGIN], Session[Base.SESSION.ADMIN_ID]))
        {
            Response.Redirect("./");
        }
        FieldCount = 0;
        if(Request.Form.Count>0)
        {
            if (Base.Verify(Request.Form, "Sanlass", "Sdatum") || (!string.IsNullOrEmpty(Request["anlass"]) && !string.IsNullOrEmpty(Request["datum"])))
            {
                Anlass = string.IsNullOrEmpty(Request["anlass"]) ? Request.Form["anlass"] : Request["anlass"];
                Datum = string.IsNullOrEmpty(Request["datum"]) ? Request.Form["datum"] : Request["datum"];

                if (Request.Files.Count == 1 && IsExcel(Request.Files[0].ContentType))
                {
                    Guid tmp = Guid.NewGuid();
                    ExcelFile = tmp.ToString();
                    string P = Server.MapPath(Base.ExcelPath(tmp));
                    Request.Files[0].SaveAs(P);
                    ProcessExcel(P);
                }
                else
                {
                    Response.Redirect("New.aspx?err=1");
                }
            }
            else
            {
                Response.Redirect("New.aspx?err=3");
            }
        }
        if (!string.IsNullOrEmpty(Request["ID"]) && IsGuid(Request["ID"]))
        {
            Guid tmp = Guid.Parse(Request["ID"]);
            ExcelFile = tmp.ToString();
            ProcessExcel(Server.MapPath(Base.ExcelPath(tmp)));
        }
        if (!string.IsNullOrEmpty(Request["err"]))
        {
            switch (Request["err"])
            {
                case "1":
                    err = "Bitte selektieren Sie für jeden Typ ein Feld aus der Excel Datei";
                    break;
                case "2":
                    err = "Ausgewähltes Feld wurde in der Excel Tabelle nicht gefunden. Bitte versuchen Sie es erneut.";
                    break;
            }
        }
    }

    protected void ProcessExcel(string P)
    {
        ExcelInterface EI = new ExcelInterface(P);
        foreach (string s in EI.Tables)
        {
            TableInfo TI = new TableInfo();
            TI.TableName = s;
            TI.Fields = EI.GetColumns(s);
            Tables.Add(TI);
            FieldCount += TI.Fields.Length;
        }
        EI.Dispose();
    }

    private bool IsGuid(string p)
    {
        Guid G = Guid.Empty;
        return Guid.TryParse(p, out G);
    }

    protected string ToSelect(TableInfo[] TI)
    {
        StringBuilder SB = new StringBuilder();
        foreach (TableInfo T in TI)
        {
            SB.AppendFormat("<option value=\"\">Bitte wählen...</option><optgroup label=\"Tabelle: {0}\">", T.TableName);
            foreach (string s in T.Fields)
            {
                SB.AppendFormat("<option value=\"{0}\">{0}</option>", Server.HtmlEncode(s));
            }
            SB.Append("</optgroup>");
        }
        return SB.ToString();
    }

    protected bool IsExcel(string MIME)
    {
        return MIME == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            || MIME == "application/vnd.ms-excel";
    }
}
