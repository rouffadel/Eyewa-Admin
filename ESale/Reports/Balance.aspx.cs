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
using System.IO;
public partial class Reports_Balance : System.Web.UI.Page
{
    EStoreDeliveryNote Store;
    ESales Sales;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginID"] != null)
        {
            if (Page.IsPostBack == false)
            {
                FillStore();
                //txtFromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                //txtToDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                DateTime baseDate = DateTime.Today;
                var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
                string startdate = thisMonthStart.ToString("dd-MM-yyyy");
                txtFromDate.Text = startdate;
                var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
                string enddate = thisMonthEnd.ToString("dd-MM-yyyy");
                txtToDate.Text = enddate;
                FillGrid();
                lblStatus.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }
    }
    private void FillStore()
    {
        try
        {
            Store = new EStoreDeliveryNote();
            ds = new DataSet();
            Store.LoginID = Convert.ToInt32(Session["LOGINID"]);
            Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
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
    protected void FillGrid()
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
            if (txtInvoiceNo.Text != "")
                Sales.InvoiceNo = txtInvoiceNo.Text;
            else
                Sales.InvoiceNo = "";
            Sales.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            ds = Sales.GetBalanceGridReport();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("SNo");
                int j = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["SNo"] = j;
                    j++;
                }
                gvBalance.DataSource = ds;
                gvBalance.DataBind();
                gvBalance.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvBalance.Visible = true;
                btnExport.Visible = true;
                btnprint.Visible = true;
                lblStatus.Text = "";
            }
            else
            {
                dvFailure.Visible = true;
                lblStatus.Text = "No Records Found.";
                gvBalance.DataSource = null;
                gvBalance.DataBind();
                gvBalance.Visible = false;
                btnExport.Visible = false;
                btnprint.Visible = false;
            }
            if(gvBalance.HeaderRow!=null)
                gvBalance.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            FillGrid();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void imgbtnClear_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["LOGINID"]) == "1")
            ddlStore.SelectedValue = "0";
        if (Convert.ToString(Session["LOGINID"]) != "1" && Convert.ToString(Session["StoreID"]) == "0")
            ddlStore.SelectedValue = "0";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtInvoiceNo.Text = "";
        if (gvBalance.HeaderRow != null)
            gvBalance.HeaderRow.TableSection = TableRowSection.TableHeader;
       
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        //lblstatus.Text = "";
        lblTitle.Text = "Balance Report ";
        //pnlgrid.Visible = false;
        gvBalance.HeaderStyle.BackColor = System.Drawing.Color.Black;
        gvBalance.HeaderStyle.ForeColor = System.Drawing.Color.White;
       // gvBalance.Font.Bold = true;
        gvBalance.Font.Size = 10;
        gvBalance.Font.Name = "verdana";
        Session["ctrl"] = gvBalance;
        string appPath = HttpContext.Current.Request.ApplicationPath;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick",
         "<script language=javascript>window.open('Reports.aspx?PageName=" + lblTitle.Text + "', '');</script>");
        if (gvBalance.HeaderRow != null)
            gvBalance.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportFromHtmlForm(gvBalance);
    }
    public void ExportFromHtmlForm(GridView gv)
    {
        lblTitle.Text = "<h3 border='0' align='center'><b>Balance Report </b></h3>";
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=Balance.xls";
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
        gv.HeaderRow.Style.Add("background-color", "black");
        gv.HeaderRow.Style.Add("color", "white");
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
    public decimal totalinvoiceamount = 0;
    public decimal totalpaidamount = 0;
    public decimal totalbalanceamount = 0;
    protected void gvBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbltotalinvoiceamount = new Label();
            lbltotalinvoiceamount = (Label)e.Row.FindControl("lblInvoiceAmount");
            if (lbltotalinvoiceamount.Text.Trim() != string.Empty && lbltotalinvoiceamount.Text != null)
                totalinvoiceamount = totalinvoiceamount + Convert.ToDecimal(lbltotalinvoiceamount.Text);

            Label lbltotalpaidamount = new Label();
            lbltotalpaidamount = (Label)e.Row.FindControl("lblPaidAmount");
            if (lbltotalpaidamount.Text.Trim() != string.Empty && lbltotalpaidamount.Text != null)
                totalpaidamount = totalpaidamount + Convert.ToDecimal(lbltotalpaidamount.Text);
            else
                lbltotalpaidamount.Text = "0.00";
            Label lbltotalbalanceamount = new Label();
            lbltotalbalanceamount = (Label)e.Row.FindControl("lblBalanceAmount");
            if (lbltotalbalanceamount.Text.Trim() != string.Empty && lbltotalbalanceamount.Text != null)
                totalbalanceamount = totalbalanceamount + Convert.ToDecimal(lbltotalbalanceamount.Text);


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblinvoiceamount = new Label();
            lblinvoiceamount = (Label)e.Row.FindControl("lblTotalInvoiceAmount");
            lblinvoiceamount.Text = Convert.ToString(totalinvoiceamount);


            Label lblpaidamount = new Label();
            lblpaidamount = (Label)e.Row.FindControl("lblTotalPaidAmount");
            lblpaidamount.Text = Convert.ToString(totalpaidamount);

            Label lblbalanceamount = new Label();
            lblbalanceamount = (Label)e.Row.FindControl("lblTotalBalanceAmount");
            lblbalanceamount.Text = Convert.ToString(totalbalanceamount);
        }
    }
}
