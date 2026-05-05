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
using System.IO;
using ESaleEntity;
public partial class Reports_Ledger : System.Web.UI.Page
{
    EStoreDeliveryNote Store;
    ESales Sales;
    DataSet ds;
    int l = 0;
    float totalcredit = 0, totaldebit = 0, totalamount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            FillStore();
            txtFromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            txtToDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
        }
    }
    private void FillStore()
    {
        try
        {
            Store = new EStoreDeliveryNote();
            ds = new DataSet();
            Store.LoginID = Convert.ToInt32(Session["LOGINID"]);
            ds = Store.ddlStore();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStore.DataSource = ds.Tables[0];
                ddlStore.DataTextField = "StoreName";
                ddlStore.DataValueField = "StoreID";
                ddlStore.DataBind();
                ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));

            }
            ddlStore.Enabled = true;
            if (Convert.ToInt32(Session["LOGINID"]) != 1 && ds.Tables[0].Rows.Count == 1)
            {
                ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);
                ddlStore.Enabled = false;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public static string converttodate(string date)
    {
        string[] _date = date.Split('-');
        if (_date[0].Length == 1)
        {
            _date[0] = 0 + _date[0];
        }
        date = _date[1] + "-" + _date[0] + "-" + _date[2];
        return date;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            Sales = new ESales();
            if (ddlStore.SelectedValue != "0")
                Sales.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            else
                Sales.StoreID = 0;            
           
            if (txtFromDate.Text != "")
                Sales.FromDate = converttodate(txtFromDate.Text);
            else
                Sales.FromDate = "";
            if (txtToDate.Text != "")
                Sales.ToDate = converttodate(txtToDate.Text);
            else
                Sales.ToDate = "";            
            Sales.LoginID = Convert.ToInt32(Session["LOGINID"]);
            ds = Sales.GetLedgerGrid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("SNo");
                int j = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["SNo"] = j.ToString();
                    j++;
                }
                totalamount = 0;
                totalcredit = 0;
                totaldebit = 0;
                gvLedger.DataSource = ds;
                l = 0;
                gvLedger.DataBind();
                gvLedger.Visible = true;
                btnExport.Visible = true;
                btnprint.Visible = true;
                lblStatus.Text = "";
            }
            else
            {
                lblStatus.Text = "No Records Found.";
                gvLedger.DataSource = null;
                gvLedger.DataBind();
                gvLedger.Visible = false;
                btnExport.Visible = false;
                btnprint.Visible = false;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlStore.SelectedValue = "0";
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        //lblstatus.Text = "";
        lblTitle.Text = "Ledger ";
        //pnlgrid.Visible = false;
        Session["ctrl"] = gvLedger;
        string appPath = HttpContext.Current.Request.ApplicationPath;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick",
         "<script language=javascript>window.open('Reports.aspx?PageName=" + lblTitle.Text + "', '');</script>");
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportFromHtmlForm(gvLedger);
    }
    public void ExportFromHtmlForm(GridView gv)
    {
        lblTitle.Text = "<h3 border='0' align='center'><b>Ledger Report </b></h3>";
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=Ledger.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";

        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        gv.HeaderRow.Style.Add("background-color", "#ccc");
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            GridViewRow row = gv.Rows[i];

            row.BackColor = System.Drawing.Color.White;
            if (i % 2 != 0)
            {
                gv.Rows[i].Style.Add("background-color", "#f2f2f2");
            }
            else
            {
                gv.Rows[i].Style.Add("background-color", "#ffffff");
            }

        }
        gv.Parent.Controls.Add(form);
        form.Attributes["runat"] = "server";
        form.Controls.Add(gv);
        this.Controls.Add(form);
        lblTitle.Visible = true;
        lblTitle.RenderControl(htextw);
        form.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }
    protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        float credit = 0, Debit = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcredit = (Label)e.Row.FindControl("lblCredit");            
             credit = Convert.ToSingle(ds.Tables[0].Rows[l]["Credit"]);
            lblcredit.Text =Convert.ToString(credit);
            Label lblDebit = (Label)e.Row.FindControl("lblDebit");     
             Debit = Convert.ToSingle(ds.Tables[0].Rows[l]["Debit"]);
            lblDebit.Text = Convert.ToString(Debit);
            Label lblamount = (Label)e.Row.FindControl("lblAmount"); 
            lblamount.Text=Convert.ToString(credit-Debit);
            totalcredit += credit;
            totaldebit += Debit;
            totalamount += credit - Debit;
            l++;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label tcredit = (Label)e.Row.FindControl("lblTotalCredit");
            Label tdebit = (Label)e.Row.FindControl("lblTotalDebit");
            Label tamount = (Label)e.Row.FindControl("lblTotalAmount");
            tcredit.Text = totalcredit.ToString();
            tdebit.Text = totaldebit.ToString();
            tamount.Text = totalamount.ToString();
        }
       
    }
}
