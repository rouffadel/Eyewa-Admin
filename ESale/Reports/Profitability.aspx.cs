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
public partial class Reports_Profitability : System.Web.UI.Page
{
    ESupplierDeliveryNote ESupplier;
    EStoreDeliveryNote Store;
    DataSet ds;
    ESales Sales;
    float totalproductvalue = 0, buyingprice = 0, totalsellingprice = 0, totalbuyingprice = 0, totalprofit = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginID"] != null)
        {
            if (Page.IsPostBack == false)
            {
                FillOrganisation();
                FillStore();
                FillCategory();
                FillBrand();
                FillSaleman();
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

    private void FillOrganisation()
    {
        Store = new EStoreDeliveryNote();
        ds = new DataSet();
        ds = Store.ddlOrganisation();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlOrganisation.DataSource = ds.Tables[0];
            ddlOrganisation.DataTextField = "OrganisationName";
            ddlOrganisation.DataValueField = "OrganisationID";
            ddlOrganisation.DataBind();
            ddlOrganisation.Items.Insert(0, new ListItem("--Any--", "0"));
            if (ds.Tables[0].Rows.Count == 1)
                ddlOrganisation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationID"]);
            else
                ddlOrganisation.SelectedValue = "0";
        }
        else
        {
            ddlOrganisation.Items.Insert(0, new ListItem("--Any--", "0"));

        }
    }

    private void FillStore()
    {
        Store = new EStoreDeliveryNote();
        Store.LoginID = Convert.ToInt32(Session["LOGINID"]);
        ds = new DataSet();
        if (Convert.ToInt32(Session["StoreID"]) == 0)
            Store.OrganisationUser = 0;
        else
            Store.OrganisationUser = 1;
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

    private void FillCategory()
    {

        ESupplier = new ESupplierDeliveryNote();
        ds = new DataSet();
        ds = ESupplier.ddlCategory();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCategory.DataSource = ds.Tables[0];
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("--Any--", "0"));

        }
        else
        {
            ddlCategory.Items.Insert(0, new ListItem("--Any--", "0"));

        }
       
    }

    private void FillBrand()
    {


        ESupplier = new ESupplierDeliveryNote();
        ds = new DataSet();
        ds = ESupplier.ddlBrand();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBrand.DataSource = ds.Tables[0];
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandID";
            ddlBrand.DataBind();
            ddlBrand.Items.Insert(0, new ListItem("--Any--", "0"));

        }
        else
        {
            ddlBrand.Items.Insert(0, new ListItem("--Any--", "0"));

        }
    }

    private void FillSaleman()
    {
        try
        {
            Sales = new ESales();
            ds = new DataSet();
            Sales.LoginID = Convert.ToInt32(Session["LOGINID"]);
            ds = Sales.ddlSalesMan();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSalesMan.DataSource = ds.Tables[0];
                ddlSalesMan.DataTextField = "EmployeeName";
                ddlSalesMan.DataValueField = "EmployeeID";
                ddlSalesMan.DataBind();
                ddlSalesMan.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                ddlSalesMan.Items.Insert(0, new ListItem("--Any--", "0"));
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    float totalquantity;
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBrand.SelectedValue != "0")
            FillProduct();
    }
    private void FillProduct()
    {
        try
        {
            Store = new EStoreDeliveryNote();
            ds = new DataSet();
            int categoryid = Convert.ToInt32(ddlCategory.SelectedValue);
            int brandid = Convert.ToInt32(ddlBrand.SelectedValue);
            Store.CategoryID = categoryid;
            Store.BrandID = brandid;
            ds = Store.ddlProduct();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlProduct.DataSource = ds.Tables[0];
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductID";
                ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, new ListItem("--Any--", "0"));

            }
            else
            {
                ddlProduct.Items.Insert(0, new ListItem("--Any--", "0"));

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
    protected void FillGrid()
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            Sales = new ESales();
            ds = new DataSet();
            if (ddlOrganisation.SelectedValue != "0")
                Sales.OrganisationiD = Convert.ToInt32(ddlOrganisation.SelectedValue);
            else
                Sales.OrganisationiD = 0;
            if (ddlStore.SelectedValue != "0")
                Sales.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            else
                Sales.StoreID = 0;
            if (ddlCategory.SelectedValue != "0")
                Sales.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            else
                Sales.CategoryID = 0;
            if (ddlBrand.SelectedValue != "0")
                Sales.BrandID = Convert.ToInt32(ddlBrand.SelectedValue);
            else
                Sales.BrandID = 0;
            if (ddlProduct.SelectedValue != "" && ddlProduct.SelectedValue != "0")
                Sales.ProductID = Convert.ToInt32(ddlProduct.SelectedValue);
            else
                Sales.ProductID = 0;
            if (ddlSalesMan.SelectedValue != "0")
                Sales.SalesManID = Convert.ToInt32(ddlSalesMan.SelectedValue);
            else
                Sales.SalesManID = 0;
            if (txtFromDate.Text != "")
                Sales.FromDate =converttodate(txtFromDate.Text);
            else
                Sales.FromDate = "";
            if (txtToDate.Text != "")
                Sales.ToDate =converttodate(txtToDate.Text);
            else
                Sales.ToDate = "";
            Sales.LoginID = Convert.ToInt32(Session["LOGINID"]);
            Sales.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            ds = Sales.GetProfitReport();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("SNo");
                int j = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["SNo"] = j.ToString();
                    j++;
                }
                DataRow dr1 = ds.Tables[0].NewRow();

                ds.Tables[0].Rows.Add(dr1);
                totalproductvalue = 0; buyingprice = 0; totalsellingprice = 0; totalbuyingprice = 0; totalprofit = 0;
                totalquantity = 0;
                gvProfitability.DataSource = ds.Tables[0];
                gvProfitability.DataBind();
                gvProfitability.Columns[9].Visible = true;
                gvProfitability.HeaderRow.TableSection = TableRowSection.TableHeader;
                lblStatus.Text = "";
                btnExport.Visible = true;
                btnprint.Visible = true;
                gvProfitability.Visible = true;

                
               
                
            }
            else
            {
                gvProfitability.DataSource = null;
                gvProfitability.DataBind();
                lblStatus.Text = "No Records found.";
                btnprint.Visible = false;
                btnExport.Visible = false;
                tblprofit.Visible = false;
                gvProfitability.Visible = false;
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                ds.Tables[1].Columns.Add("SNo");
              int  i = 1;
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    dr["SNo"] = i.ToString();
                    i++;
                }
                gvExpenses.DataSource = ds.Tables[1];
                expenseamount = 0; paidamount = 0; balance = 0;
                gvExpenses.DataBind();
                lblStatus.Text = "";
                gvExpenses.Visible = true;
                //gvExpenses.HeaderRow.TableSection = TableRowSection.TableHeader;
               
            }
            else
            {
                gvExpenses.DataSource = null;
                gvExpenses.DataBind();
                gvExpenses.Visible = false;
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                ds.Tables[2].Columns.Add("SNo");
                int i = 1;
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    dr["SNo"] = i.ToString();
                    i++;
                }
                orlenseqty = 0;
                orlenstotal = 0;
                gvOrderLense.DataSource = ds.Tables[2];
                gvOrderLense.DataBind();
                gvOrderLense.Visible = true;
                lblStatus.Text = "";
                //gvOrderLense.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                gvOrderLense.DataSource = null;
                gvOrderLense.DataBind();
                gvOrderLense.Visible = false;
            }
            
            DataTable dt = new DataTable();
            dt.Columns.Add("SalesProfit");
            dt.Columns.Add("OrderLense");
            dt.Columns.Add("Expenses");
            dt.Columns.Add("FinalProfit");
           DataRow dr2 = dt.NewRow();
            dt.Rows.Add(dr2);
            gvProfit.DataSource = dt;
            gvProfit.DataBind();
            if (gvProfitability.Visible == true || gvOrderLense.Visible == true || gvExpenses.Visible == true)
                gvProfit.Visible = true;
            else
                gvProfit.Visible = false;
            //gvProfit.HeaderRow.TableSection = TableRowSection.TableHeader;
            tblprofit.Visible = false;
            if (gvProfitability.HeaderRow != null)
                gvProfitability.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvExpenses.HeaderRow != null)
                gvExpenses.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (gvProfit.HeaderRow != null)
                gvProfit.HeaderRow.TableSection = TableRowSection.TableHeader;
           
        }
        catch (Exception)
        {

            throw;
        }
    }
    float orlenseqty = 0, orlenstotal = 0;
    protected void CalculateProfit()
    {
        try
        {
            Label lbl = gvProfitability.FooterRow.FindControl("lblTotalProfit")as Label;
            float totalprofit = 0, totalpaidamount = 0,finalprofit=0,orderlense=0;
            if (lbl.Text != "")
                totalprofit = Convert.ToSingle(lbl.Text);
            lbl = gvExpenses.FooterRow.FindControl("lblTotalPaidAmount")as Label;
            if (lbl.Text != "") 
            totalpaidamount = Convert.ToSingle(lbl.Text);
            finalprofit = totalprofit - totalpaidamount;
            lblSalesProfit.Text = totalprofit.ToString();
            lblExpensesProfit.Text = totalpaidamount.ToString();
            lblFinalProfit.Text = finalprofit.ToString();
            if (finalprofit < 0)
                lblFinalProfit.ForeColor = System.Drawing.Color.Red;
            

            
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
    protected void gvProditablility_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblsno = (Label)e.Row.FindControl("lblSNo");
            LinkButton lbldetail=(LinkButton)e.Row.FindControl("lblDetails");
            float productvalue = 0, bp = 0, sellingprice = 0, tbp = 0, profit = 0;
            float totQuantity=0;

            if (((Label)e.Row.FindControl("lblProductValue")).Text != "")
                productvalue = Convert.ToSingle(((Label)e.Row.FindControl("lblProductValue")).Text); 
            //if(((Label)e.Row.FindControl("lblBuyingPrice")).Text!="")
            //    bp = Convert.ToSingle(((Label)e.Row.FindControl("lblBuyingPrice")).Text);
            if (((Label)e.Row.FindControl("lblSellingPrice")).Text!="")
                sellingprice = Convert.ToSingle(((Label)e.Row.FindControl("lblSellingPrice")).Text);
            if (((Label)e.Row.FindControl("lblGrossBuyingPrice")).Text!="")
                tbp = Convert.ToSingle(((Label)e.Row.FindControl("lblGrossBuyingPrice")).Text);
            if(((Label)e.Row.FindControl("lblProfit")).Text!="")
                 profit = Convert.ToSingle(((Label)e.Row.FindControl("lblProfit")).Text);
            if (((Label)e.Row.FindControl("lblQuantity")).Text != "")
                totalquantity += Convert.ToSingle(((Label)e.Row.FindControl("lblQuantity")).Text);
            if (((Label)e.Row.FindControl("lblQuantity")).Text != "")
            {
                totQuantity += Convert.ToSingle(((Label)e.Row.FindControl("lblQuantity")).Text);
            }

            totalproductvalue += productvalue;
           // buyingprice += bp;
            totalsellingprice += sellingprice;
            totalbuyingprice += tbp;
            totalprofit += profit;
            if (lblsno.Text == "")
                lbldetail.Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            ((Label)e.Row.FindControl("lblTotalProductValue")).Text = totalproductvalue.ToString();
           // ((Label)e.Row.FindControl("lblTotalBuyingPrice")).Text = buyingprice.ToString();
            ((Label)e.Row.FindControl("lblTotalSellingPrice")).Text = totalsellingprice.ToString();
            ((Label)e.Row.FindControl("lblTotalGrossBuyingPrice")).Text = totalbuyingprice.ToString();
            ((Label)e.Row.FindControl("lblTotalProfit")).Text = totalprofit.ToString();
            ((Label)e.Row.FindControl("lblTotalQuantity")).Text = totalquantity.ToString();
           
        }
    }
    protected void gvProfitability_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvProfitability.DataKeys[rowindex].Value);
            ds = new DataSet();
            Sales = new ESales();
            Sales.ProductID = id;
            ds = Sales.GetProfitDetails();
            if (ds.Tables[0].Rows.Count > 0)
            { 
                dvDetails.Attributes["style"]="display:block;height:520px;";
                gvDetails.DataSource=ds.Tables[0];
                gvDetails.DataBind();
                gvDetails.Visible = true;
            }
            else
            {
                dvDetails.Attributes["style"] = "display:none;";
                gvDetails.DataSource = null;
                gvDetails.DataBind();
                gvDetails.Visible = false;
            }
        }
    }
    protected void imgbtnClear_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlOrganisation.SelectedValue = "0";
        if (Convert.ToString(Session["LOGINID"]) == "1")        
            ddlStore.SelectedValue = "0";
        if (Convert.ToString(Session["LOGINID"]) != "1" && Convert.ToString(Session["StoreID"]) == "0")
            ddlStore.SelectedValue = "0";
        ddlCategory.SelectedValue = "0";
        ddlBrand.SelectedValue = "0";
        ddlProduct.Items.Clear();
        ddlSalesMan.SelectedValue = "0";
        txtFromDate.Text = "";
        txtToDate.Text = "";
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        lblTitle.Text = "Profit Report ";
        if (gvProfitability.Rows.Count > 0)
        {
            gvProfitability.HeaderStyle.BackColor = System.Drawing.Color.Black;
            gvProfitability.HeaderStyle.ForeColor = System.Drawing.Color.White;
          //  gvProfitability.Font.Bold = true;
            gvProfitability.Font.Size = 10;
            gvProfitability.Font.Name = "verdana";
            //lblstatus.Text = "";
            gvProfitability.Columns[10].Visible = false;
            Session["ctrl"] = gvProfitability;
           
        }
        else
        {
            Session["ctrl"] = null;
        }
        if (gvOrderLense.Rows.Count >= 0)
        {
            gvOrderLense.HeaderStyle.BackColor = System.Drawing.Color.Black;
            gvOrderLense.HeaderStyle.ForeColor = System.Drawing.Color.White;
         //   gvOrderLense.Font.Bold = true;
            gvOrderLense.Font.Size = 10;
            gvOrderLense.Font.Name = "verdana";
            Session["ctrl3"] = gvOrderLense;
        }
            //pnlgrid.Visible = false;
            if (gvExpenses.Rows.Count >= 0)
            {
                gvExpenses.HeaderStyle.BackColor = System.Drawing.Color.Black;
                gvExpenses.HeaderStyle.ForeColor = System.Drawing.Color.White;
             //   gvExpenses.Font.Bold = true;
                gvExpenses.Font.Size = 10;
                gvExpenses.Font.Name = "verdana";
                Session["ctrl1"] = gvExpenses;
            }
            if (gvProfit.Rows.Count > 0)
            {
                gvProfit.HeaderStyle.BackColor = System.Drawing.Color.Black;
                gvProfit.HeaderStyle.ForeColor = System.Drawing.Color.White;
               // gvProfit.Font.Bold = true;
                gvProfit.Font.Size = 10;
                gvProfit.Font.Name = "verdana";
                Session["ctrl2"] = gvProfit;
            }
       
            string appPath = HttpContext.Current.Request.ApplicationPath;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick",
             "<script language=javascript>window.open('Reports.aspx?PageName=" + lblTitle.Text + "', '');</script>");
        if(gvProfitability.HeaderRow!=null)
            gvProfitability.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvOrderLense.HeaderRow != null)
            gvOrderLense.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvExpenses.HeaderRow != null)
            gvExpenses.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvProfit.HeaderRow != null)
            gvProfit.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportFromHtmlForm(gvProfitability,gvExpenses,gvProfit,gvOrderLense);
        
    }
 
   
    public void ExportFromHtmlForm(GridView gv,GridView expenses,GridView profit,GridView ORL)
    {
        if (gv.Rows.Count > 0)
        {
            lblTitle.Text = "<h3 border='0' align='center'><b>Profit Report </b></h3>";
            HtmlForm form = new HtmlForm();
            string attachment = "attachment; filename=Profit.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";

            StringWriter stw = new StringWriter();
            HtmlTextWriter htextw = new HtmlTextWriter(stw);
            gv.Columns[10].Visible = false;
            // gv.HeaderRow.Style.Add("background-color", "#ccc");
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
            for (int i = 0; i < ORL.Rows.Count; i++)
            {
                GridViewRow row = ORL.Rows[i];

                row.BackColor = System.Drawing.Color.White;
                if (i % 2 != 0)
                {
                    ORL.Rows[i].Style.Add("background-color", "#f2f2f2");
                }
                else
                {
                    ORL.Rows[i].Style.Add("background-color", "#ffffff");
                }

            }
            for (int i = 0; i < expenses.Rows.Count; i++)
            {
                GridViewRow row = expenses.Rows[i];

                row.BackColor = System.Drawing.Color.White;
                if (i % 2 != 0)
                {
                    expenses.Rows[i].Style.Add("background-color", "#f2f2f2");
                }
                else
                {
                    expenses.Rows[i].Style.Add("background-color", "#ffffff");
                }

            }
            for (int i = 0; i < profit.Rows.Count; i++)
            {
                GridViewRow row = profit.Rows[i];

                row.BackColor = System.Drawing.Color.White;
                if (i % 2 != 0)
                {
                    profit.Rows[i].Style.Add("background-color", "#f2f2f2");
                }
                else
                {
                    profit.Rows[i].Style.Add("background-color", "#ffffff");
                }

            }
            gv.Parent.Controls.Add(form);
            ORL.Parent.Controls.Add(form);
            expenses.Parent.Controls.Add(form);
            profit.Parent.Controls.Add(form);
            form.Attributes["runat"] = "server";
            form.Controls.Add(gv);
            form.Controls.Add(new Literal() { ID = "br1", Text = "<br/>" });
            form.Controls.Add(ORL);
            form.Controls.Add(new Literal() {ID="br1", Text="<br/>"});
            form.Controls.Add(expenses);
            form.Controls.Add(new Literal() { ID = "br2", Text = "<br/>" });
            form.Controls.Add(profit);
            this.Controls.Add(form);

            lblTitle.Visible = true;
            lblTitle.RenderControl(htextw);
            form.RenderControl(htextw);
            Response.Write(stw.ToString());
            Response.End();
        }
    }
    protected void btnDetailCancel_Click(object sender, EventArgs e)
    {
        dvDetails.Attributes["style"] = "display:none;";
        gvDetails.DataSource = null;
        gvDetails.DataBind();
        gvDetails.Visible = false;
    }
    float expenseamount, paidamount, balance;
    protected void gvExpenses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)e.Row.FindControl("lblExpenseAmount")).Text != "")
                expenseamount += Convert.ToSingle(((Label)e.Row.FindControl("lblExpenseAmount")).Text);
            if (((Label)e.Row.FindControl("lblPaidAmount")).Text != "")
                paidamount += Convert.ToSingle(((Label)e.Row.FindControl("lblPaidAmount")).Text);
            if (((Label)e.Row.FindControl("lblExpenseAmount")).Text != "")
                balance += Convert.ToSingle(((Label)e.Row.FindControl("lblExpenseAmount")).Text); 
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            ((Label)e.Row.FindControl("lblTotalExpenseAmount")).Text = expenseamount.ToString();
            ((Label)e.Row.FindControl("lblTotalPaidAmount")).Text = paidamount.ToString();
            ((Label)e.Row.FindControl("lblTotalBalance")).Text = balance.ToString();
        }
    }
    protected void gvProfit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            float totalprofit = 0, totalpaidamount = 0, finalprofit = 0, orderlense = 0;
            Label lbl = new Label();
            if (gvProfitability.Rows.Count != 0)
            {
                lbl = gvProfitability.FooterRow.FindControl("lblTotalProfit") as Label;

                if (lbl.Text != "")
                    totalprofit = Convert.ToSingle(lbl.Text);
            }
            else
            {
                totalprofit = 0;
            }
            if (gvExpenses.Rows.Count != 0)
            {
                if (gvExpenses.Rows.Count > 0)
                {
                    lbl = gvExpenses.FooterRow.FindControl("lblTotalPaidAmount") as Label;
                    if (lbl.Text != "")
                        totalpaidamount = Convert.ToSingle(lbl.Text);
                }
            }

            else
            {

                totalpaidamount = 0;
            }
            if (gvOrderLense.Rows.Count != 0)
            {
                if (gvOrderLense.Rows.Count > 0)
                {
                    lbl = gvOrderLense.FooterRow.FindControl("lblfinaltotal") as Label;
                    if (lbl.Text != "")
                        orderlense = Convert.ToSingle(lbl.Text);
                }
            }
            else
            {
                orderlense = 0;
            }
            finalprofit = totalprofit +orderlense - totalpaidamount;
            ((Label)e.Row.FindControl("lblSalesProfit")).Text = totalprofit.ToString();
            ((Label)e.Row.FindControl("lblExpenses")).Text = totalpaidamount.ToString();
            ((Label)e.Row.FindControl("lblFinalProfit")).Text = finalprofit.ToString();
            ((Label)e.Row.FindControl("lblOrderlense")).Text = orderlense.ToString();
            if (finalprofit < 0)
                ((Label)e.Row.FindControl("lblFinalProfit")).ForeColor = System.Drawing.Color.Red;
            else
                ((Label)e.Row.FindControl("lblFinalProfit")).ForeColor = System.Drawing.Color.Black;
        }
    }
    protected void gvProfit_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvOrderLense_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvOrderLense_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label l = new Label();
            l = (Label)e.Row.FindControl("lblQuantity");
            orlenseqty += Convert.ToInt32(l.Text);
            l = (Label)e.Row.FindControl("lblTotal");
            orlenstotal += Convert.ToSingle(l.Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label l = new Label();
            l = (Label)e.Row.FindControl("lbltotalquantity");
            l.Text = orlenseqty.ToString();
            l = (Label)e.Row.FindControl("lblfinaltotal");
            l.Text = orlenstotal.ToString();
        }        
    }
}
