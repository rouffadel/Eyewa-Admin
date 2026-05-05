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
public partial class Reports_Counter : System.Web.UI.Page
{
    DataSet ds;
    Counter ECounter;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            LoadStore();
            LoadUser();
            txtStoreToDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            txtStoreFromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            FillGrid();
        }
    }

    private void LoadUser()
    {
        try
        {
            ds = new DataSet();
            ECounter = new Counter();
            ds = ECounter.FillUsers();
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlUser.DataSource = ds.Tables[0];
                ddlUser.DataValueField = "UserID";
                ddlUser.DataTextField = "UserName";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                ddlUser.Items.Insert(0, new ListItem("--Any--", "0"));
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void LoadStore()
    {
        try
        {
            ds = new DataSet();
            ECounter = new Counter();
            ECounter.LoginID = Convert.ToInt32(Session["LOGINID"]);
            ds = ECounter.FillStore();
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlStore.DataSource = ds.Tables[0];
                ddlStore.DataValueField = "StoreID";
                ddlStore.DataTextField = "StoreName";
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
            ECounter = new Counter();
            ds = new DataSet();
            if (ddlStore.SelectedValue != "0")
                ECounter.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            else
                ECounter.StoreID = 0;
            if (ddlUser.SelectedValue != "0")
                ECounter.UserID = Convert.ToInt32(ddlUser.SelectedValue);
            else
                ECounter.UserID = 0;
            if (txtStoreFromDate.Text != "")
                ECounter.FromDate = (txtStoreFromDate.Text);
            else
                ECounter.FromDate = "";
            if (txtStoreToDate.Text != "")
                ECounter.ToDate = (txtStoreToDate.Text);
            else
                ECounter.ToDate = "";
            ds = ECounter.GetCounterReportGrid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("SNo");
                int i = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dr["SNo"] = i.ToString();
                    i++;
                }
                gvCounter.DataSource = ds.Tables[0];
                gvCounter.DataBind();
                btnprint.Visible = true;
                btnExport.Visible = true;
                gvCounter.Visible = true;
                lblStatus.Text = "";
            }
            else
            {
                gvCounter.DataSource = null;
                gvCounter.DataBind();
                btnprint.Visible = false;
                btnExport.Visible = false;
                gvCounter.Visible = false;
                lblStatus.Text = "No Record Found.";
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
    protected void btnprint_Click(object sender, EventArgs e)
    {
        //lblstatus.Text = "";
        lblTitle.Text = "Counter Report";
        //pnlgrid.Visible = false;
        Session["ctrl"] = gvCounter;
        string appPath = HttpContext.Current.Request.ApplicationPath;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick",
         "<script language=javascript>window.open('Reports.aspx?PageName=" + lblTitle.Text + "', '');</script>");
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportFromHtmlForm(gvCounter);
    }
    public void ExportFromHtmlForm(GridView gv)
    {
        lblTitle.Text = "<h3 border='0' align='center'><b> Counter Report </b></h3>";
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename= Counter Report.xls";
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
    protected void btnClear_Click(object sender, ImageClickEventArgs e)
    {
        ddlStore.SelectedValue = "0";
        ddlUser.SelectedValue = "0";
        txtStoreFromDate.Text = "";
        txtStoreToDate.Text = "";
        btnprint.Visible = false;
        btnExport.Visible = false;
        gvCounter.Visible = false;
    }
    public static string converttodate(string data)
    {
        string[] _data = data.Split('-');
        if (_data[0].Length == 1)
        {
            _data[0] = 0 + _data[0];
        }
        data = _data[1] + "-" + _data[0] + "-" + _data[2];
        return data;
    }



    public decimal TotalOpenValue = 0;
    public decimal TotalCloseValue = 0;
    public decimal TotalSales = 0;
    public decimal TotalExpenses = 0;
    public float withdrawl = 0;
    public float finalvalue = 0;
    protected void gvCounter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblOpenValue = new Label();
            lblOpenValue = (Label)e.Row.FindControl("lblOpenValue");
            if (lblOpenValue.Text.Trim() != string.Empty && lblOpenValue.Text != null)
                TotalOpenValue = TotalOpenValue + Convert.ToDecimal(lblOpenValue.Text);

            Label lblCloseValue = new Label();
            lblCloseValue = (Label)e.Row.FindControl("lblCloseValue");
            if (lblCloseValue.Text.Trim() != string.Empty && lblCloseValue.Text != null)
                TotalCloseValue = TotalCloseValue + Convert.ToDecimal(lblCloseValue.Text);

            Label lblSales = new Label();
            lblSales = (Label)e.Row.FindControl("lblSales");
            if (lblSales.Text.Trim() != string.Empty && lblSales.Text != null)
                TotalSales = TotalSales + Convert.ToDecimal(lblSales.Text);

            Label lblExpenses = new Label();
            lblExpenses = (Label)e.Row.FindControl("lblExpenses");
            if (lblExpenses.Text.Trim() != string.Empty && lblExpenses.Text != null)
                TotalExpenses = TotalExpenses + Convert.ToDecimal(lblExpenses.Text);

            Label lblwithdrawl = new Label();
            lblwithdrawl=(Label)e.Row.FindControl("lblwithdrawl");
            if (lblwithdrawl.Text.Trim() != string.Empty && lblwithdrawl.Text != null)
                withdrawl += Convert.ToSingle(lblwithdrawl.Text);

            Label lblfinalvalue = new Label();
            lblfinalvalue = (Label)e.Row.FindControl("lblfinalvalue");
            if (lblfinalvalue.Text.Trim() != string.Empty && lblfinalvalue.Text != null)
                finalvalue += Convert.ToSingle(lblfinalvalue.Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalOpenValue = new Label();
            lblTotalOpenValue = (Label)e.Row.FindControl("lblTotalOpenValue");
            lblTotalOpenValue.Text = Convert.ToString(TotalOpenValue);


            Label lblTotalCloseValue = new Label();
            lblTotalCloseValue = (Label)e.Row.FindControl("lblTotalCloseValue");
            lblTotalCloseValue.Text = Convert.ToString(TotalCloseValue);

            Label lblTotalSales = new Label();
            lblTotalSales = (Label)e.Row.FindControl("lblTotalSales");
            lblTotalSales.Text = Convert.ToString(TotalSales);

            Label lblTotalExpenses = new Label();
            lblTotalExpenses = (Label)e.Row.FindControl("lblTotalExpenses");
            lblTotalExpenses.Text = Convert.ToString(TotalExpenses);

             lblTotalExpenses = new Label();
             lblTotalExpenses = (Label)e.Row.FindControl("lbltotalwithdrawl");
             lblTotalExpenses.Text = Convert.ToString(withdrawl);

             lblTotalExpenses = new Label();
             lblTotalExpenses = (Label)e.Row.FindControl("lbltotalfinalvalue");
             lblTotalExpenses.Text = Convert.ToString(finalvalue);
        }
    }
}
