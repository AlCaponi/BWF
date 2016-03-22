using System;
using AyrA.SQL;
using System.IO;
using System.Data.OleDb;
using System.Data;

public partial class Excel : System.Web.UI.Page
{
    protected string Status = "NO FILE";
    protected string P = "/temp/temp.xls";
    protected void Page_Load(object sender, EventArgs e)
    {
        P = Server.MapPath(P);
        if (Request.Files.Count == 1)
        {
            if (File.Exists(P))
            {
                File.Delete(P);
            }
            Request.Files[0].SaveAs(P);

            if (File.Exists(P))
            {
                using (ExcelInterface EI = new ExcelInterface(P))
                {
                    Status = string.Empty;
                    foreach (string s in EI.Tables)
                    {
                        Status += string.Format("<h1>Table: {0}</h1>", Server.HtmlEncode(s));
                        SQLRow[] RR = EI.ExecReader("SELECT * FROM [" + s + "]");
                        if (RR.Length > 0)
                        {
                            Status += "<table><tr>";
                            for (int j = 0; j < RR[0].Names.Count; j++)
                            {
                                Status += string.Format("<th>{0}</th>", Server.HtmlEncode(RR[0].Names[j]));
                            }
                            Status += "</tr>";
                            foreach (SQLRow R in RR)
                            {
                                Status += "<tr>";
                                for (int i = 0; i < R.Values.Count; i++)
                                {
                                    Status += string.Format("<td>{0}</td>", R.Values[R.Names[i]] == null ? "" : Server.HtmlEncode(R.Values[R.Names[i]].ToString()));
                                }
                                Status += "</tr>";
                            }
                            Status += "</table>";
                        }
                        else
                        {
                            Status += "<i>Empty</i>";
                        }
                    }
                }
                File.Delete(P);
            }
            else
            {
                Status = "FILE ERROR";
            }
        }
    }
}
