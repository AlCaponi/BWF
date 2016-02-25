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
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
}
