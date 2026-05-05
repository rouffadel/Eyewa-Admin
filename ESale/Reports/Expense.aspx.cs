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

public partial class Reports_Expense : System.Web.UI.Page
{
    EexpenseType Expense;
    DataSet ds;
    EStoreDeliveryNote Store;
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
    private void FillGrid()
    {
        lblStatus.Text = "";
        dvFailure.Visible=false;
        try
        {
             ds = new DataSet();
           Expense=new EexpenseType();
            if (ddlStore.SelectedValue != "0")
                Expense.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            else
                Expense.StoreID = 0;            
            if (txtFromDate.Text != "")
                Expense.FromDate = txtFromDate.Text;
            else
                Expense.FromDate = "";
            if (txtToDate.Text != "")
                Expense.ToDate = txtToDate.Text;
            else
                Expense.ToDate = "";           
            Expense.LoginID = Convert.ToInt32(Session["LOGINID"]);
          
            Expense.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            ds = Expense.GetExpenseGridReport();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("SNo");
                int j = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["SNo"] = j.ToString();
                    j++;
                }
                gvExpense.DataSource = ds;
                gvExpense.DataBind();
                gvExpense.Visible = true;
                btnExport.Visible = true;
                btnprint.Visible = true;
                lblStatus.Text = "";
                gvExpense.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                dvFailure.Visible = true;
                lblStatus.Text = "No Records Found.";
                gvExpense.DataSource = null;
                gvExpense.DataBind();
                gvExpense.Visible = false;
                btnExport.Visible = false;
                btnprint.Visible = false;
            }
        }
        catch (Exception)
        {
            
            throw;
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
       lblStatus.Text = string.Empty;
       lblSuccess.Text = string.Empty;
       dvFailure.Visible = false;
       dvSuccess.Visible = false;
        if (Convert.ToString(Session["LOGINID"]) == "1")
            ddlStore.SelectedValue = "0";
        if (Convert.ToString(Session["LOGINID"]) != "1" && Convert.ToString(Session["StoreID"]) == "0")
            ddlStore.SelectedValue = "0";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        if (gvExpense.HeaderRow != null)
            gvExpense.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        //lblstatus.Text = "";
        lblTitle.Text = "Expense Report ";
        //pnlgrid.Visible = false;
        gvExpense.HeaderStyle.BackColor = System.Drawing.Color.Black;
        gvExpense.HeaderStyle.ForeColor = System.Drawing.Color.White;
        //gvExpense.Font.Bold = true;
        //gvExpense.Font.Bold = true;
        gvExpense.Font.Size = 10;
        Session["ctrl"] = gvExpense;
        string appPath = HttpContext.Current.Request.ApplicationPath;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick",
         "<script language=javascript>window.open('Reports.aspx?PageName=" + lblTitle.Text + "', '');</script>");
        if(gvExpense.HeaderRow!=null)
        gvExpense.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportFromHtmlForm(gvExpense);
    }
    public void ExportFromHtmlForm(GridView gv)
    {
        lblTitle.Text = "<h3 border='0' align='center'><b>Expense Report </b></h3>";
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=Expense.xls";
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
    public decimal totalexpenseamount = 0;
    public decimal totalpaidamount = 0;
    public decimal totalbalanceamount = 0;
    protected void gvExpense_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbltotalinvoiceamount = new Label();
            lbltotalinvoiceamount = (Label)e.Row.FindControl("lblExpenseAmount");
            if (lbltotalinvoiceamount.Text.Trim() != string.Empty && lbltotalinvoiceamount.Text != null)
                totalexpenseamount = totalexpenseamount + Convert.ToDecimal(lbltotalinvoiceamount.Text);

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
            lblinvoiceamount = (Label)e.Row.FindControl("lblTotalExpenseAmount");
            lblinvoiceamount.Text = Convert.ToString(totalexpenseamount);


            Label lblpaidamount = new Label();
            lblpaidamount = (Label)e.Row.FindControl("lblTotalPaidAmount");
            lblpaidamount.Text = Convert.ToString(totalpaidamount);

            Label lblbalanceamount = new Label();
            lblbalanceamount = (Label)e.Row.FindControl("lblTotalBalanceAmount");
            lblbalanceamount.Text = Convert.ToString(totalbalanceamount);
        }
    }
}

