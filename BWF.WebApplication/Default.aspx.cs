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

public partial class _Default : System.Web.UI.Page
{

    public IBWFService Service
    {
        get
        {
            return new BWF.Service.BWFService();
        }
    }

    public Soldat Soldat
    {
        get 
        {
            return (Soldat)Session["SOLDAT"];
        }
        set 
        {
            Session["SOLDAT"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {   
#if !DEBUG
		if(string.IsNullOrEmpty(Request.Headers["SSL-HTTPS"]))
		{
			Response.Redirect("https://www.zso-emme.ch/");
		}
#endif
	
        if (this.Soldat != null)
        {
            Response.Redirect("/Poll.aspx");
        }
    }

    protected void btnLogin_OnClick(Object sender, EventArgs e)
    {
        string svNumber = svnr.Text;

        Soldat soldat = Service.GetSoldat(svNumber);
        if (soldat != null)
        {
            this.Soldat = soldat;
            Response.Redirect("/Poll.aspx");
        }
        else 
        {
            lblErrorMessage.Text = "Die eingegebene SVNummer konnte leider nicht gefunden werden.";
            lblErrorMessage.Visible = true;
        }
        
    }
}
