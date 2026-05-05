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
public partial class Reports_AvailableStoreStock : System.Web.UI.Page
{
    EStoreDeliveryNote Store;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
         
        if (Session["LoginID"] != null)
        {
        if (Page.IsPostBack == false)
        {
            FillStore();
            FillCategory();
            FillBrand();
            //txtStoreFromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy ");
            //txtStoreToDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy ");
            DateTime baseDate = DateTime.Today;
            var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
            string startdate = thisMonthStart.ToString("dd-MM-yyyy");
            txtStoreFromDate.Text = startdate;
            var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
            string enddate = thisMonthEnd.ToString("dd-MM-yyyy");
            txtStoreToDate.Text = enddate;
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

    private void FillBrand()
    {
        Store = new EStoreDeliveryNote();
        ds = new DataSet();
        ds = Store.ddlBrand();
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

    private void FillCategory()
    {
        Store = new EStoreDeliveryNote();
        ds = new DataSet();
        ds = Store.ddlCategory();
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
    private void FillStore()
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

    protected void FillGrid()
    {
        lblStatus.Text = "";
        dvFailure.Visible = false;
        try
        {
            Store = new EStoreDeliveryNote();
            ds = new DataSet();
            if (ddlStore.SelectedValue != "0")
                Store.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            else
                Store.StoreID = 0;
            if (ddlCategory.SelectedValue != "0")
                Store.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            else
                Store.CategoryID = 0;
            if (ddlBrand.SelectedValue != "0")
                Store.BrandID = Convert.ToInt32(ddlBrand.SelectedValue);
            else
                Store.BrandID = 0;
            if (ddlProduct.SelectedValue != "" && ddlProduct.SelectedValue != "0")
                Store.ProductID = Convert.ToInt32(ddlProduct.SelectedValue);
            else
                Store.ProductID = 0;
            //if (txtStoreFromDate.Text != "")
            //{
            //    Store.FromDate = converttodate(txtStoreFromDate.Text);
            //}
            //else
            //{
                Store.FromDate = "";
            //}
            if (txtStoreToDate.Text != "")
            {
                Store.ToDate = converttodate(txtStoreToDate.Text);
            }
            else
            {
                Store.ToDate = "";
            }
            if(Session["StoreID"]!=null)
            Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            Store.LoginID = Convert.ToInt32(Session["LoginID"]);
            ds = Store.GetStoreAvailableStock();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("SNo");
                int j = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["SNo"] = j.ToString();
                    j++;
                }
                finaltotal = 0;
                totalbuyingprice = 0;
                if (Convert.ToInt32(Session["LoginId"]) == 1)
                {
                    gvStock.Columns[5].Visible = true;
                    gvStock.Columns[8].Visible = true;
                }
                else
                {
                    gvStock.Columns[5].Visible = false;
                    gvStock.Columns[8].Visible = false;
                }
                gvStock.DataSource = ds;
                gvStock.DataBind();
                gvStock.HeaderRow.TableSection = TableRowSection.TableHeader;
                btnExport.Visible = true;
                btnprint.Visible = true;

                gvStock.Visible = true;
                lblStatus.Text = "";
            }
            else
            {
                dvFailure.Visible = false;
                lblStatus.Text = "No Records Found.";
                gvStock.DataSource = null;
                gvStock.DataBind();
                gvStock.Visible = false;
                btnExport.Visible = false;
                btnprint.Visible = false;
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
    public static string converttodate(string data)
    {
        string[] _data = data.Split('-');
        if (_data[0].Length == 1)
        {
            _data[0] = 0 + _data[0];
        }
        data = _data[0] + "-" + _data[1] + "-" + _data[2];
        return data;
    }
    protected void imgbtnClear_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlBrand.SelectedValue = "0";
        ddlCategory.SelectedValue = "0";
        if (Convert.ToString(Session["LOGINID"]) == "1")
        ddlStore.SelectedValue = "0";
        if (Convert.ToString(Session["LOGINID"]) != "1" && Convert.ToString(Session["StoreID"]) == "0")
            ddlStore.SelectedValue = "0";
        ddlProduct.Items.Clear();
        txtStoreFromDate.Text = "";
        txtStoreToDate.Text = "";
        if (gvStock.HeaderRow != null)
            gvStock.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        //lblstatus.Text = "";
        lblTitle.Text = "Available Stock";
        //pnlgrid.Visible = false;
        gvStock.HeaderStyle.BackColor = System.Drawing.Color.Black;
        gvStock.HeaderStyle.ForeColor = System.Drawing.Color.White;
       // gvStock.Font.Bold = true;
        gvStock.Font.Size = 10;
        gvStock.Font.Name = "verdana";
        Session["ctrl"] = gvStock;
        string appPath = HttpContext.Current.Request.ApplicationPath;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick",
         "<script language=javascript>window.open('Reports.aspx?PageName=" + lblTitle.Text + "', '');</script>");
        if (gvStock.HeaderRow != null)
        {
            gvStock.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportFromHtmlForm(gvStock);
    }
    public void ExportFromHtmlForm(GridView gv)
    {
        lblTitle.Text = "<h3 border='0' align='center'><b>Available Store Stock Report </b></h3>";
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=Available Store stock Report.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";

        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        gv.HeaderRow.Style.Add("background-color", "black");
        gv.HeaderRow.Style.Add("color", "white"); 
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
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue != "0")
            FillProduct();
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBrand.SelectedValue != "0")
            FillProduct();
    }
    protected void gvStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    float sellingPrice = 0;
    float availableQuntity = 0;
    float finaltotal = 0;
    float totalbuyingprice = 0;
    float finalbuyingprice = 0;
    protected void gvStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblsellingprice = new Label();
            lblsellingprice = (Label)e.Row.FindControl("lblSellingPrice");
            if (lblsellingprice.Text != "" && lblsellingprice.Text != null)
            {
                sellingPrice = sellingPrice + Convert.ToSingle(lblsellingprice.Text);
            }

            Label lblavailableqty = new Label();
            lblavailableqty = (Label)e.Row.FindControl("lblAvailableQuantity");
            if (lblavailableqty.Text != "" && lblavailableqty.Text != null)
            {
                availableQuntity = availableQuntity + Convert.ToSingle(lblavailableqty.Text);
            }
            Label lbltotal = new Label();
            lbltotal = (Label)e.Row.FindControl("lblTotal");
            if (lbltotal.Text != "" && lbltotal.Text != null)
            {
                finaltotal = finaltotal + Convert.ToSingle(lbltotal.Text);
            }
            lbltotal = new Label();
            lbltotal=(Label)e.Row.FindControl("lblBuyingPrice");
            if (lbltotal.Text != "" && lbltotal.Text != null)
                totalbuyingprice = totalbuyingprice + Convert.ToSingle(lbltotal.Text);
            lbltotal = new Label();
            lbltotal = (Label)e.Row.FindControl("lblTotalBuyingPrice");
            if (lbltotal.Text != "" && lbltotal.Text != null)
                finalbuyingprice = finalbuyingprice + Convert.ToSingle(lbltotal.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbltotalsellingPrice = new Label();
            lbltotalsellingPrice = (Label)e.Row.FindControl("lbltotalsellingprice");
            lbltotalsellingPrice.Text = sellingPrice.ToString();

            Label lbltotalavailableqty = new Label();
            lbltotalavailableqty = (Label)e.Row.FindControl("lbltotalavailablequantity");
            lbltotalavailableqty.Text = availableQuntity.ToString();

            Label lblfinaltotal = new Label();
            lblfinaltotal = (Label)e.Row.FindControl("lblFinalTotal");
            lblfinaltotal.Text = finaltotal.ToString();

            Label lblbuyingpricetotal = new Label();
            lblbuyingpricetotal = (Label)e.Row.FindControl("lbltotalbuyingprice");
            lblbuyingpricetotal.Text = totalbuyingprice.ToString();
            Label lblfinalbuyingprice = new Label();
            lblbuyingpricetotal = (Label)e.Row.FindControl("lblFinalBuyingPrice");
            lblbuyingpricetotal.Text = finalbuyingprice.ToString();

        }
    }
}
