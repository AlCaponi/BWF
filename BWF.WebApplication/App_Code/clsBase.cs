using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using AyrA.SQL;
using System.IO;

public class Base
{
    public struct DSN
    {
        //public const string ADMIN = @"DRIVER={SQL Server};SERVER=sql3.mcis.agrinet.local;DATABASE=db930888;UID=db930888_admin;PWD=B2RkphT2BAfVp8";
        public const string ADMIN = @"PROVIDER=SQLOLEDB;SERVER=sql3.mcis.agrinet.local;DATABASE=db930888;UID=db930888_admin;PWD=B2RkphT2BAfVp8";
    }

    /// <summary>
    /// Contains values for the specified questions we do not want to see
    /// </summary>
    public struct CRITICAL
    {
        //Alle unerwünschten werte angeben im format: frageID|wert,wert,wert,...
        //wobei 'wert' entweder eine Antwort oder eine AntwortID sein kann.
        //<null> zeigt ann, dass keine Antwort als falsch gewertet wird
        public const string DIENSTTAGE = "76142E93-7A55-488C-A24B-2E0A16E0F7FF|<null>,0,1";
        public const string EINTEILUNG = "CF7DBAFA-CFF1-40AE-89CF-CD8766626A43|<null>,D08FED5A-3496-44F2-9C90-B0714D222725";
        public const string GESUND = "440F7883-247F-40AB-82B1-427BA143855E|<null>,8016A124-1D91-42AA-852D-B5306C29976A";

        public Guid FrageID;
        public string[] Antworten;
    }

    public struct SESSION
    {
        public const string ADMIN_LOGIN = "admin_login";
        public const string ADMIN_ID = "admin_id";
    }

    public class Frage
    {
        public Guid FragenID { get; set; }
        public string FrageText { get; set; }
    }

    public class Soldat
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string SVNummer { get; set; }
        public Guid ID { get; set; }
        public Guid Anlass { get; set; }
        public Poll[] Antworten { get; set; }
        public bool Problematic { get; set; }
        public bool Beantwortet
        {
            get
            {
                if (Antworten == null)
                {
                    return false;
                }
                foreach (Poll P in Antworten)
                {
                    if (!string.IsNullOrEmpty(P.Antwort) || P.AntwortID != Guid.Empty)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }

    public class Poll
    {
        public Guid FragenID { get; set; }
        public string Frage { get; set; }
        public string Antwort { get; set; }
        public Guid AntwortID { get; set; }
    }

    public class Anlass
    {
        public Soldat[] Soldaten { get; set; }
        public DateTime Datum { get; set; }
        public string Name { get; set; }
        public Guid AnlassID { get; set; }

        public int Erfolg
        {
            get
            {
                int i = 0;
                foreach (Soldat S in Soldaten)
                {
                    if (!S.Problematic && S.Beantwortet)
                    {
                        ++i;
                    }
                }
                return i;
            }
        }
        public int Beantwortet
        {
            get
            {
                int i = 0;
                foreach (Soldat S in Soldaten)
                {
                    if (S.Beantwortet)
                    {
                        ++i;
                    }
                }
                return i;
            }
        }
    }

    static Base()
	{
	}

    public static void Shift(Stream In, Stream Out)
    {
        byte[] b = new byte[1024];
        int i = 1;
        while (i > 0)
        {
            i = In.Read(b, 0, b.Length);
            if (i > 0)
            {
                for (int j = 0; j < i; j++)
                {
                    b[j] += 194;
                }
                Out.Write(b, 0, i);
            }
        }
    }

    public static string ExcelPath(Guid G)
    {
        return string.Format("/temp/{0}.xls", G);
    }

    public static bool DelExcel(string Dir)
    {
        bool success = true;
        string[] Files = Directory.GetFiles(Dir);
        foreach (string Filename in Files)
        {
            try
            {
                File.Delete(Filename);
            }
            catch
            {
                success = false;
            }
        }
        return success;
    }

    public static bool Verify(NameValueCollection nvc,params string[] args)
    {
        if (nvc == null || args == null || nvc.Count == 0)
        {
            return false;
        }

        List<string> Keys = new List<string>(nvc.AllKeys);

        foreach (string s in args)
        {
            if (s.Length > 1)
            {
                if (Keys.Contains(s.Substring(1)))
                {
                    switch (s[0])
                    {
                        case 'S':
                            //string (required)
                            if (nvc[s.Substring(1)].Length == 0)
                            {
                                return false;
                            }
                            goto case 's';
                        case 's':
                            //string
                            break;
                        case 'N':
                            //number (required)
                            if (nvc[s.Substring(1)].Length == 0)
                            {
                                return false;
                            }
                            goto case 'n';
                        case 'n':
                            //number
                            int temp = 0;
                            return nvc[s.Substring(1)].Length == 0 || int.TryParse(nvc[s.Substring(1)], out temp);
                        case 'U':
                            //unknown (required)
                            if (nvc[s.Substring(1)].Length == 0)
                            {
                                return false;
                            }
                            goto case 'u';
                        case 'u':
                            //unknown
                            break;
                        default:
                            throw new Exception("Unknown field type: " + s[0].ToString());
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static string ToString(object param, string std)
    {
        if (param == null)
        {
            return std;
        }
        try
        {
            return (string)param;
        }
        catch
        {
            return std;
        }
    }

    public static bool ToBool(object param, bool std)
    {
        try
        {
            return (bool)param;
        }
        catch
        {
            return std;
        }
    }

    public static bool IsAdmin(object probablyBool, object uID)
    {
        //TODO: IMPLEMENT LOGIN SYSTEM
        return (ToBool(probablyBool, false) && !string.IsNullOrEmpty(ToString(uID, null)));
    }

    public static Guid GetGuid(string param)
    {
        Guid G = Guid.Empty;
        return Guid.TryParse(param, out G) ? G : Guid.Empty;
    }

    public static bool SoldatExists(string SVN, SQLInterface SI)
    {
        return (int)SI.ExecReader("SELECT COUNT(SoldatID) FROM Soldat WHERE SVNummer=?", SVN)[0][0] > 0;
    }

    public static bool SoldatExists(Guid SoldatID, SQLInterface SI)
    {
        return (int)SI.ExecReader("SELECT COUNT(SoldatID) FROM Soldat WHERE SoldatID=?", SoldatID)[0][0] > 0;
    }

    public static Frage[] GetFragen(SQLInterface SI)
    {
        SQLRow[] RR = SI.ExecReader(@"
SELECT FragenID,Frage
FROM Fragen
ORDER BY FragenGruppeID ASC, Sort ASC");

        Frage[] FF = new Frage[RR.Length];

        for (int i = 0; i < RR.Length; i++)
        {
            FF[i] = new Frage();
            FF[i].FragenID = (Guid)RR[i]["FragenID"];
            FF[i].FrageText = (string)RR[i]["Frage"];
        }

        return FF;
    }

    public static Poll[] FillAnswers(Frage[] Fragen, Poll[] Vorlage)
    {
        Poll[] PP = new Poll[Fragen.Length];

        //add empty questions
        for (int k = 0; k < Fragen.Length; k++)
        {
            PP[k] = new Poll();
            PP[k].Frage = Fragen[k].FrageText;
            PP[k].FragenID = Fragen[k].FragenID;
        }

        for (int i = 0; i < Vorlage.Length; i++)
        {
            for (int j = 0; j < PP.Length; j++)
            {
                if (Vorlage[i].FragenID == PP[j].FragenID)
                {
                    PP[j] = Vorlage[i];
                }
            }
        }
        return PP;
    }

    public static bool ProblematicSoldat(Soldat S)
    {
        CRITICAL[] CC = new CRITICAL[3];
        for (int i = 0; i < 3; i++)
        {
            CC[i] = new CRITICAL();
        }
        CC[0].FrageID = Guid.Parse(CRITICAL.DIENSTTAGE.Split('|')[0]);
        CC[1].FrageID = Guid.Parse(CRITICAL.EINTEILUNG.Split('|')[0]);
        CC[2].FrageID = Guid.Parse(CRITICAL.GESUND.Split('|')[0]);

        CC[0].Antworten = CRITICAL.DIENSTTAGE.Split('|')[1].Split(';');
        CC[1].Antworten = CRITICAL.EINTEILUNG.Split('|')[1].Split(',');
        CC[2].Antworten = CRITICAL.GESUND.Split('|')[1].Split(',');

        foreach (Poll P in S.Antworten)
        {
            foreach (CRITICAL C in CC)
            {
                if (P.FragenID == C.FrageID)
                {
                    foreach (string A in C.Antworten)
                    {
                        if ((A.ToLower() == (string.IsNullOrEmpty(P.Antwort) ? "" : P.Antwort).ToLower() || A.ToLower() == P.AntwortID.ToString().ToLower()) || (A=="<null>" && string.IsNullOrEmpty(P.Antwort)))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public static Soldat GetSoldat(Guid SoldatID, SQLInterface SI)
    {
        SQLRow R = SI.ExecReader(@"
SELECT
    Vorname,SVNummer,Nachname,SoldatID,AnlassID
FROM
    Soldat
WHERE
    Soldat.SoldatID=?", SoldatID)[0];
        Soldat S = new Soldat();
        S.Anlass = (Guid)R["AnlassID"];
        S.Vorname = (string)R["Vorname"];
        S.Nachname = (string)R["Nachname"];
        S.ID = (Guid)R["SoldatID"];
        S.SVNummer = (string)R["SVNummer"];
        return S;
    }

    public static Soldat[] GetSoldaten(Guid AnlassID, SQLInterface SI)
    {
        SQLRow[] R = SI.ExecReader(@"
SELECT
    Vorname,SVNummer,Nachname,SoldatID,AnlassID
FROM
    Soldat
WHERE
    Soldat.AnlassID=?", AnlassID);
        Soldat[] SS = new Soldat[R.Length];
        for (int i = 0; i < R.Length; i++)
        {
            SS[i] = new Soldat();
            SS[i].Anlass = (Guid)R[i]["AnlassID"];
            SS[i].Vorname = (string)R[i]["Vorname"];
            SS[i].Nachname = (string)R[i]["Nachname"];
            SS[i].ID = (Guid)R[i]["SoldatID"];
            SS[i].SVNummer = (string)R[i]["SVNummer"];
        }
        return SS;
    }

    public static Poll[] GetAnswers(Guid SoldatID, SQLInterface SI)
    {
        List<Poll> PP = new List<Poll>();
        string Frage, Antwort;
        Guid id;
        Guid AntwortID;
        SQLRow[] Answers = SI.ExecReader(@"
SELECT
	Fragen.FragenID,Fragen.Frage,SoldatAntwort.AntwortID,
	SoldatAntwort.TextAntwort
FROM
    Fragen
LEFT JOIN
    SoldatAntwort ON SoldatAntwort.FragenID=Fragen.FragenID
WHERE
    SoldatAntwort.SoldatID=?
ORDER BY Fragen.Sort ASC", SoldatID);
        for (int i = 0; i < Answers.Length; i++)
        {
            AntwortID = Guid.Empty;
            SQLRow Answer = Answers[i];
            Frage = (string)Answer["Frage"];
            id = (Guid)Answer["FragenID"];
            int curr = IndexOfPoll(PP,id);

            if (Answer["TextAntwort"] == null)
            {
                AntwortID = (Guid)Answer["AntwortID"];
                Antwort = (string)(SI.ExecReader("SELECT MöglicheAntwort AS Antwort FROM Antworten WHERE AntwortID=?", (Guid)Answer["AntwortID"])[0]["Antwort"]);
            }
            else
            {
                Antwort = (string)Answer["TextAntwort"];
            }
            //ignore empty answers
            if (!string.IsNullOrEmpty(Antwort))
            {
                if (curr >= 0)
                {
                    PP[curr].Antwort += "\r\n" + Antwort;
                }
                else
                {
                    Poll P = new Poll();
                    P.Frage = Frage;
                    P.Antwort = Antwort;
                    P.FragenID = id;
                    P.AntwortID = AntwortID;
                    PP.Add(P);
                }
            }
        }

        return PP.ToArray();
    }

    public static Soldat[] GetAnswers(Soldat[] SS, SQLInterface SI)
    {
        Frage[] Fragen = GetFragen(SI);
        for (int i = 0; i < SS.Length; i++)
        {
            SS[i].Antworten = FillAnswers(Fragen, GetAnswers(SS[i].ID, SI));
            SS[i].Problematic = ProblematicSoldat(SS[i]);
        }
        return SS;
    }

    public static Anlass GetAnlass(Guid AnlassID, SQLInterface SI)
    {
        SQLRow[] RR = SI.ExecReader("SELECT * FROM Anlass WHERE AnlassID=?", AnlassID);
        if (RR.Length == 1)
        {
            Anlass A = new Anlass();
            A.AnlassID = AnlassID;
            A.Name = (string)RR[0]["Name"];
            A.Datum = DateTime.Parse(string.Format("{0} {1}", RR[0]["Datum"], RR[0]["Zeit"]));
            return A;
        }
        return null;
    }

    private static int IndexOfPoll(List<Poll> P,Guid Frage)
    {
        for (int i = 0; i < P.Count; i++)
        {
            if (P[i].Frage != null)
            {
                if (P[i].FragenID == Frage)
                {
                    return i;
                }
            }
        }
        return -1;
    }
}
