using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BWF.Library;
using BWF.Service.Interface;
using Newtonsoft.Json.Linq;

public partial class Poll : System.Web.UI.Page
{

    public IBWFService Service
    {
        get
        {
            return BWF.BWFApplication.Current.Services<IBWFService>();
        }
    }

    public FragenGruppeCol FragenGruppeCol
    { 
        get 
        {
            if (Session["FragenGruppeCol"] == null)
            {
                Session["FragenGruppeCol"] = Service.GetFragenGruppeCol();
            }
            return (FragenGruppeCol)Session["FragenGruppeCol"];
        }
    }

    public Soldat Soldat
    {
        get
        {
            return Session["SOLDAT"] as Soldat;
        }
        set
        {
            Session["SOLDAT"] = value;        
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.DataBind();
            if (this.Soldat == null)
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    protected double GetPreviousPercentage(int currentPosition)
    {
        return ((currentPosition - 1.0) / FragenGruppeCol.Count) * 100.0;
    }

    protected double GetNextPercentage(int currentPosition)
    {
        return ((currentPosition + 1.0) / FragenGruppeCol.Count) * 100.0;
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        this.SaveAnswers();
        this.Soldat = null;
        Response.Redirect("/end.aspx");

    }

    private void SaveAnswers()
    {
        if (this.hidAnswerValues.Value != string.Empty)
        {
            string jsonResponse = this.hidAnswerValues.Value;
            JArray json = JArray.Parse(jsonResponse);
            SoldatAntwortCol soldatAntwortCol = new SoldatAntwortCol(); 
            foreach (var item in json)
            {
                JToken frage = item["FrageID"];
                JToken frageTyp = item["FrageTyp"];
                JToken answer = item["answer"];

                Guid frageId;
                Guid frageTypId;

                Guid.TryParse(frage.ToString(), out frageId);
                Guid.TryParse(frageTyp.ToString(), out frageTypId);
                if (item["answer"] != null)
                {
                    if (item["answer"] is JArray)
                    {
                        foreach (var antwort in item["answer"])
                        {
                            SoldatAntwort soldatAntwort = new SoldatAntwort();
                            soldatAntwort.Fragen.ID = frageId;
                            Guid antwortId;
                            if (Guid.TryParse(antwort.ToString(), out antwortId))
                            {
                                soldatAntwort.Antwort.AntwortID = antwortId;
                            }
                            else
                            {
                                soldatAntwort.TextAntwort = antwort.ToString();
                            }

                            soldatAntwortCol.Add(soldatAntwort);
                        }
                    }
                    else
                    {
                        SoldatAntwort soldatAntwort = new SoldatAntwort();
                        soldatAntwort.Fragen.ID = frageId;
                        Guid antwortId;
                        if (Guid.TryParse(answer.ToString(), out antwortId))
                        {
                            soldatAntwort.Antwort.AntwortID = antwortId;
                        }
                        else
                        {
                            soldatAntwort.TextAntwort = answer.ToString();
                        }
                        soldatAntwortCol.Add(soldatAntwort);
                    }
                }   
            }
            Service.SaveSoldatAntwortCol(soldatAntwortCol, Soldat.ID, Soldat.Anlass.ID );
        }
    }
    
}
