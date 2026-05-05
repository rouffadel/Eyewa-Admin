using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Admin_Bottom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        MySql objMySql = new MySql();
        Hashtable ht = new Hashtable();
        DataSet ds = new DataSet();
        try
        {
            if (Session["LOGINID"] == null)
            {

                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
               
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            CommonFunctions cf = new CommonFunctions();
            cf.WriteError(ex.Message, ex.StackTrace.ToString(), Session["LOGINNAME"].ToString(), "Search");
            Response.Write("<!--" + ex.ToString() + "-->");
        }
        finally
        {
           
        }
    }

}
