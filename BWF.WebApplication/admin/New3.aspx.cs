using System;
using AyrA.SQL;
using System.IO;
using System.Collections.Generic;
using System.Text;

public partial class _New3 : System.Web.UI.Page
{
    protected string err = string.Empty;
    protected string ExcelFile = string.Empty;
    protected string Log = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Base.IsAdmin(Session[Base.SESSION.ADMIN_LOGIN], Session[Base.SESSION.ADMIN_ID]))
        {
            Response.Redirect("./");
        }

        if (Base.Verify(Request.Form, "Svorname", "Snachname", "Ssvnr", "Sdatum","Sanlassname","Sanlassdatum")
            && !string.IsNullOrEmpty(Request["ID"])
            && IsGuid(Request["ID"])
            && Request.Form["anlassdatum"].Contains(" "))
        {
            Guid tmp = Guid.Parse(Request["ID"]);
            ExcelFile = tmp.ToString();
            string P = Server.MapPath(Base.ExcelPath(tmp));
            if (File.Exists(P))
            {
                ExcelInterface EI = new ExcelInterface(P);
                string Table = EI.Tables[0];
                List<string> Cols = new List<string>(EI.GetColumns(Table));

                if (Cols.Contains(Request.Form["vorname"]) &&
                    Cols.Contains(Request.Form["nachname"]) &&
                    Cols.Contains(Request.Form["svnr"]) &&
                    Cols.Contains(Request.Form["datum"]))
                {
                    int errcount = 0;
                    StringBuilder SB = new StringBuilder();
                    Guid AnlassID = Guid.NewGuid();
                    string Name;
                    DateTime Datum;
                    string Zeit;
                    Name = Request.Form["anlassname"];
                    Datum = DateTime.Parse(Request.Form["anlassdatum"].Split(' ')[0]);
                    Zeit = Request.Form["anlassdatum"].Split(' ')[1];

                    SQLInterface SI = new SQLInterface(Base.DSN.ADMIN);
                    SI.Exec(@"INSERT INTO [Anlass]
                        ([AnlassID],[Name],[Datum],[Zeit])
                        VALUES(?,?,?,?)",
                        AnlassID, Name, Datum, Zeit);
                    SQLRow[] RR = EI.ExecReader("SELECT * FROM [" + Table + "]");
                    foreach (SQLRow R in RR)
                    {
                        string nachname = Base.ToString(R[Request.Form["nachname"]],string.Empty).Trim();
                        string vorname = Base.ToString(R[Request.Form["vorname"]], string.Empty).Trim();
                        string svnr = Base.ToString(R[Request.Form["svnr"]], string.Empty).Trim();
						string datum = Base.ToString(R[Request.Form["datum"]], string.Empty).Trim();
                        DateTime gebdatum = DateTime.MinValue;
						
						if(R[Request.Form["datum"]] is DateTime)
						{
							gebdatum=(DateTime)R[Request.Form["datum"]];
						}
						else
						{
							int tempdate=0;
							//excel date is sometimes in days
							if(int.TryParse(datum, out tempdate))
							{
								//excel date is wrong by two days (therefore -2)
								gebdatum=new DateTime(1900, 1, 1, 0, 0, 0).AddDays(tempdate - 2);
								if(gebdatum.Ticks >= DateTime.Now.Ticks || tempdate==0)
								{
									++errcount;
									SB.AppendFormat("Ungültiges Geburtsdatum beim Import des Soldaten mit nr.: {0}. Datum: {1}\r\n", svnr, datum);
									continue;
								}
							}
							else if (!DateTime.TryParse(datum, out gebdatum) || //ungültiges format
								gebdatum.Ticks >= DateTime.Now.Ticks || //datum in der Zukunft
								gebdatum.Ticks == DateTime.MinValue.Ticks) //Datum nicht gesetzt
							{
								++errcount;
								SB.AppendFormat("Ungültiges Geburtsdatum beim Import des Soldaten mit nr.: {0}\r\n", svnr);
								continue;
							}
						}
                        if (!IsValid(vorname, nachname, svnr))
                        {
                            ++errcount;
                            SB.AppendFormat("Ungültige Angaben beim Import des Soldaten mit nr.: {0}\r\n", svnr);
                            continue;
                        }


                        if (Base.SoldatExists((string)R[Request.Form["svnr"]], SI))
                        {
                            //Soldat auf neuen Anlass eintragen
                            Guid SoldatID=(Guid)SI.ExecReader("SELECT SoldatID FROM Soldat WHERE SVNummer=?", svnr)[0][0];
                            if (SI.Exec("UPDATE Soldat SET AnlassID=? WHERE SoldatID=?", AnlassID, SoldatID) < 0 ||
                                SI.Exec("UPDATE SoldatAntwort SET AnlassID=? WHERE SoldatID=?",AnlassID, SoldatID) < 0)
                            {
                                ++errcount;
                                SB.AppendFormat("Fehlerhafter Datensatz beim Import des Soldaten mit nr.: {0}\r\n",svnr);
                            }
                            else
                            {
                                SB.AppendFormat("Existierenden Soldat auf neuen Anlass eingetragen. Soldat: {0} {1}\r\n", vorname, nachname);
                            }
                        }
                        else
                        {
                            //Soldat erfassen
                            if (
                            SI.Exec("INSERT INTO Soldat (SoldatID,Vorname,Nachname,SVNummer,Geburtsdatum,AnlassID) VALUES(NEWID(),?,?,?,?,?)",
                                vorname,
                                nachname,
                                svnr,
                                gebdatum,
                                AnlassID) < 1)
                            {
                                ++errcount;
                                SB.AppendFormat("Fehlerhafter Datensatz beim Import des Soldaten mit nr.: {0}\r\n", svnr);
                            }
                            else
                            {
                                SB.AppendFormat("Soldat erfasst: {0} {1}\r\n", vorname, nachname);
                            }
                        }
                    }
                    SB.AppendFormat("Anzahl Fehler: {0}", errcount);
                    Log = Server.HtmlEncode(SB.ToString());
                    SI.Dispose();
                    EI.Dispose();
                    try
                    {
                        Base.DelExcel(Server.MapPath("/temp/"));
                    }
                    catch
                    {
                    }
                }
                else
                {
                    Response.Redirect(string.Format("New2.aspx?err=2&ID={0}&anlass={1}&datum={2}",
                        Server.UrlEncode(Request.Form["ID"]),
                        Server.UrlEncode(Request.Form["anlassname"]),
                        Server.UrlEncode(Request.Form["anlassdatum"]))
                        );
                }
            }
            else
            {
                Response.Redirect("New.aspx?err=2");
            }
        }
        else
        {
            Response.Redirect("New2.aspx?err=1&ID=" + Server.UrlEncode(Request.Form["ID"]));
        }
    }

    private bool IsValid(string vorname, string nachname, string svnr)
    {
        //zu kurze namen
        if (string.IsNullOrEmpty(vorname) || string.IsNullOrEmpty(nachname) || vorname.Length < 2 || nachname.Length < 2)
        {
            return false;
        }
        //ungültiges svnr-format
        string[] parts = svnr.Split('.');
        int temp = 0;
        if (parts.Length == 4)
        {
            foreach (string part in parts)
            {
                temp = -1;
                if (!int.TryParse(part, out temp) || temp < 0 || temp > 9999)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool IsGuid(string p)
    {
        Guid G = Guid.Empty;
        return Guid.TryParse(p, out G);
    }
}
