using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ESaleEntity;

public partial class Reports_StoreDeliveryNote : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           
        }
    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        lblTitle.Text = "Store Delivery Note";
        pnlgrid.Visible = false;
        Session["ctrl"] = gvCustomerDNReport;
        string appPath = HttpContext.Current.Request.ApplicationPath;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick",
         "<script language=javascript>window.open('Reports.aspx?PageName=" + lblTitle.Text + "', '');</script>");
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void rbtnSummary_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void rbtnDetailed_CheckedChanged(object sender, EventArgs e)
    {

    }
}
