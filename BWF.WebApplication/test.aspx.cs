using System;
using System.Data.OleDb;
using System.IO;

public partial class test : System.Web.UI.Page
{
    //private const string DSN = "DRIVER={0};SERVER={1};DATABASE={2};UID={3};PWD={4}";
    //private const string DRV = "{SQL Server}";
    private const string DSN = "PROVIDER={0};SERVER={1};DATABASE={2};UID={3};PWD={4}";
    private const string DRV = "SQLOLEDB";
    private const string HOST = "sql3.mcis.agrinet.local";
    private const string DB = "db930888";
    private const string USER = "db930888_manager";
    private const string PASS = "Esuf8s9JeCuFC";

    protected void Page_Load(object sender, EventArgs e)
    {
        string FullDSN = string.Format(DSN, DRV, HOST, DB, USER, PASS);

        Response.ContentType = "text/plain";
        try
        {
            //just open and close a connection.
            using (OleDbConnection OC = new OleDbConnection(FullDSN))
            {
                OC.Open();
                Response.Write("OK");
                OC.Close();
            }
            Response.Write(File.ReadAllText(Server.MapPath("err.html")));
        }
        catch (Exception ex)
        {
            //we replace some strings, because anyone could see it in the error message.
            Response.Write(ex.ToString()
                .Replace(HOST, "**HOST**")
                .Replace(PASS, "**PASSWORD**")
                .Replace(USER, "**USERNAME**")
                .Replace(DB, "**DB**")
            );
        }
    }
}