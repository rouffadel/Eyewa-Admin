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
using System.Web.Services;
using System.Collections.Generic;
using System.IO;
using ESaleEntity;

public partial class Reports_StoreReturn : System.Web.UI.Page
{
    EStoreReturn Store;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginID"] != null)
        {
            if (Page.IsPostBack == false)
            {
                lblStatus.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
                FillStore();
                FillCategory();
                FillBrand();
                DateTime baseDate = DateTime.Today;
                var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
                string startdate = thisMonthStart.ToString("dd-MM-yyyy");
                txtFromDate.Text = startdate;
                var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
                string enddate = thisMonthEnd.ToString("dd-MM-yyyy");
                txtToDate.Text = enddate;
                FillGrid();
            }
            if (gvStock.HeaderRow != null)
                gvStock.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            Store = new  EStoreReturn();
            ds = new DataSet();
            Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
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
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            ds = new DataSet();
            Store = new EStoreReturn();
            if (ddlStore.SelectedValue != "0")
                Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
            else
                Store.StoreId = 0;
            if (txtStoreDeliveryNoteNo.Text != "")
                Store.ReturnNumber = txtStoreDeliveryNoteNo.Text;
            else
                Store.ReturnNumber = "";
            if (txtFromDate.Text != "")
                Store.FromDate = converttodate(txtFromDate.Text);
            else
                Store.FromDate = "";
            if (txtToDate.Text != "")
                Store.ToDate = converttodate(txtToDate.Text);
            else
                Store.ToDate = "";
            Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
            if (ddlCategory.SelectedValue != "0")
                Store.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            else
                Store.CategoryId = 0;
            if (ddlBrand.SelectedValue != "0")
                Store.BrandId = Convert.ToInt32(ddlBrand.SelectedValue);
            else
                Store.BrandId = 0;
            if (ddlProduct.Items.Count > 0)
            {
                if (ddlProduct.SelectedValue != "0")
                    Store.ProductId = Convert.ToInt32(ddlProduct.SelectedValue);
                else
                    Store.ProductId = 0;
            }

            Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);

            ds = Store.GetStoreReturnGrid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("SNo");
                int j = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["SNo"] = j.ToString();
                    j++;
                }
                gvStock.DataSource = ds;
                gvStock.DataBind();
                gvStock.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvStock.Visible = true;
                btnExport.Visible = true;
                btnprint.Visible = true;
                lblStatus.Text = "";
            }
            else
            {
                dvFailure.Visible = true;
                lblStatus.Text = "No Records Found.";
                gvStock.DataSource = null;
                gvStock.DataBind();
                gvStock.Columns[5].Visible = true;
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
    protected void rbtnSummary_CheckedChanged(object sender, EventArgs e)
    {
        buyingprice = 0; sellingprice = 0; qty = 0; totalbp = 0; totalsp = 0;
        FillGrid();
        gvDetails.Visible = false;
        gvStock.Visible = true;
    }
    protected void rbtnDetailed_CheckedChanged(object sender, EventArgs e)
    {
        buyingprice = 0; sellingprice = 0; qty = 0; totalbp = 0; totalsp = 0;
        FillDetailsGrid();
        gvDetails.Visible = true;
        gvStock.Visible = false;
    }
    protected void FillDetailsGrid()
    {
        try
        {
            Store = new EStoreReturn();
            ds = new DataSet();
            if (ddlStore.SelectedValue != "0")
                Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
            else
                Store.StoreId = 0;
            if (txtStoreDeliveryNoteNo.Text != "")
                Store.ReturnNumber = txtStoreDeliveryNoteNo.Text;
            else
                Store.ReturnNumber = "";
            if (txtFromDate.Text != "")
                Store.FromDate = converttodate(txtFromDate.Text);
            else
                Store.FromDate = "";
            if (txtToDate.Text != "")
                Store.ToDate = converttodate(txtToDate.Text);
            else
                Store.ToDate = "";
            Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
            if (ddlCategory.SelectedValue != "0")
                Store.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            else
                Store.CategoryId = 0;
            if (ddlBrand.SelectedValue != "0")
                Store.BrandId = Convert.ToInt32(ddlBrand.SelectedValue);
            else
                Store.BrandId = 0;
            if (ddlProduct.Items.Count > 0)
            {
                if (ddlProduct.SelectedValue != "0")
                    Store.ProductId = Convert.ToInt32(ddlProduct.SelectedValue);
                else
                    Store.ProductId = 0;
            }

            Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);

            ds = Store.GetStoreReturnDetailGrid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("SNo");
                int j = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["SNo"] = j.ToString();
                    j++;
                }
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvDetails.Visible = true;
                btnExport.Visible = true;
                btnprint.Visible = true;
                lblStatus.Text = "";
            }
            else
            {
                lblStatus.Text = "No Records Found.";
                gvDetails.DataSource = null;
                gvDetails.DataBind();
                gvDetails.Visible = false;
                btnExport.Visible = false;
                btnprint.Visible = false;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    float buyingprice = 0, sellingprice = 0, qty = 0, totalbp = 0, totalsp = 0;
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtnSummary.Checked)
            {
                FillGrid();
            }
            else if (rbtnDetailed.Checked)
            {
                FillDetailsGrid();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void imgbtnClear_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        lblStatus.Text = "";
        if (Convert.ToString(Session["LOGINID"]) == "1")
            ddlStore.SelectedValue = "0";
        if (Convert.ToInt32(Session["LoginID"]) != 1 && Convert.ToInt32(Session["StoreID"]) == 0)
            ddlStore.SelectedValue = "0";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        ddlBrand.SelectedValue = "0";
        ddlCategory.SelectedValue = "0";
        txtStoreDeliveryNoteNo.Text = "";
        //gvStock.Visible = false;
        btnprint.Visible = false;
        btnExport.Visible = false;
        if (gvDetails.HeaderRow != null)
            gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        //FillCategory();
        //FillBrand();
    }
    private void FillBrand()
    {
        Store = new EStoreReturn();
        ds = new DataSet();
        Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
        Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);   
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
    private void FillProduct()
    {
        try
        {
            Store = new EStoreReturn();
            ds = new DataSet();
            int categoryid = Convert.ToInt32(ddlCategory.SelectedValue);
            int brandid = Convert.ToInt32(ddlBrand.SelectedValue);
            Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
            Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);   
            Store.CategoryId = categoryid;
            Store.BrandId = brandid;
            ds = Store.ddlProducts();
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
    private void FillCategory()
    {
        Store = new EStoreReturn();
        ds = new DataSet();
        Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
        Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);   
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
    protected void gvStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);
            int storeReturnId = Convert.ToInt32(gvStock.DataKeys[rowindex].Value);
            Store = new EStoreReturn();
            ds = new DataSet();
            Store.StoreReturnId = storeReturnId;
            try
            {
                Store.LoginId = Convert.ToInt32(Session["LOGINID"].ToString());
                Store.StoreReturnId = storeReturnId;

                if (storeReturnId != 0)
                {

                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "StoreForPrint(" + storeReturnId + ");", true);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
    protected void gvStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = new Label();

            lbl = (Label)e.Row.FindControl("lblSellingprice");
            if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
                sellingprice = sellingprice + Convert.ToSingle(lbl.Text);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblQuantity");
            if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
                qty = qty + Convert.ToSingle(lbl.Text);


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalSellingprice");
            lbl.Text = Convert.ToString(sellingprice);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalQuantity");
            lbl.Text = Convert.ToString(qty);
        }
    }
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = new Label();
            //lbl = (Label)e.Row.FindControl("lblBuyingPrice");
            //if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
            //    buyingprice = buyingprice + Convert.ToSingle(lbl.Text);

            lbl = (Label)e.Row.FindControl("lblSellingPrice");
            if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
                sellingprice = sellingprice + Convert.ToSingle(lbl.Text);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblQuantity");
            if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
                qty = qty + Convert.ToSingle(lbl.Text);
            //lbl = new Label();
            //lbl = (Label)e.Row.FindControl("lblNetBuyingPrice");
            //if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
            //    totalbp = totalbp + Convert.ToSingle(lbl.Text);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblNetSellingPrice");
            if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
                totalsp = totalsp + Convert.ToSingle(lbl.Text);


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = new Label();
            //lbl = (Label)e.Row.FindControl("lblTotalBuyingPrice");
            //lbl.Text = Convert.ToString(buyingprice);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalSellingPrice");
            lbl.Text = Convert.ToString(sellingprice);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalQuantity");
            lbl.Text = Convert.ToString(qty);
            //lbl = new Label();
            //lbl = (Label)e.Row.FindControl("lblTotalNetBuyingPrice");
            //lbl.Text = Convert.ToString(totalbp);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalNetSellingPrice");
            lbl.Text = Convert.ToString(totalsp);
        }
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        if (gvStock.Visible == true)
        {
            if (gvStock.Rows.Count > 0)
            {
                //lblstatus.Text = "";
                gvStock.HeaderStyle.BackColor = System.Drawing.Color.Black;
                gvStock.HeaderStyle.ForeColor = System.Drawing.Color.White;
                string str = "Store Return Report";
              //  gvStock.Font.Bold = true;
                gvStock.Font.Size = 10;
                gvStock.Font.Name = "verdana";
                //pnlgrid.Visible = false;
                gvStock.Columns[5].Visible = false;
                Session["ctrl"] = gvStock;
                string appPath = HttpContext.Current.Request.ApplicationPath;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick",
                 "<script language=javascript>window.open('Reports.aspx?PageName=" + str + "', '');</script>");
            }
        }
        else
        {
            if (gvDetails.Rows.Count > 0)
            {
                gvDetails.HeaderStyle.BackColor = System.Drawing.Color.Black;
                gvDetails.HeaderStyle.ForeColor = System.Drawing.Color.White;
                //lblstatus.Text = "";
               // gvDetails.Font.Bold = true;
                string str = "Store Return Detailed Report";
                //pnlgrid.Visible = false;
                Session["ctrl"] = gvDetails;
                string appPath = HttpContext.Current.Request.ApplicationPath;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick",
                 "<script language=javascript>window.open('Reports.aspx?PageName=" + str + "', '');</script>");
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (rbtnSummary.Checked)
        {
            ExportFromHtmlForm(gvStock);
        }
        else if (rbtnDetailed.Checked)
        {
            ExportFromHtmlForm(gvDetails);
        }
    }
    public void ExportFromHtmlForm(GridView gv)
    {
        lblTitle.Text = "<h3 border='0' align='center'><b>Store Return Report  </b></h3>";
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=Store Return Stock.xls";
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
        gv.Columns[5].Visible = false;
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
        else
            ddlProduct.Items.Clear();
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBrand.SelectedValue != "0")
            FillProduct();
        else
            ddlProduct.Items.Clear();
    }
    [WebMethod]
    public static GridDataSet StoreForPrint(int StoreReturnId)
    {

        DataSet dsforstore = new DataSet();
        EStoreReturn Store = new EStoreReturn();
        EStoreDeliveryNote estore = new EStoreDeliveryNote();
        EStoreDeliveryList liststore = new EStoreDeliveryList();
        List<EStoreDeliveryNote> estorelist = new List<EStoreDeliveryNote>();
        List<EStoreDeliveryList> estoretable = new List<EStoreDeliveryList>();
        GridDataSet ObjGridData = new GridDataSet();
        Store.StoreReturnId = StoreReturnId;
        DataSet dsforSDN = new DataSet();
        DataSet ds = new DataSet();
        DataSet dstotal = new DataSet();


        try
        {
            dsforSDN = Store.DataToPrint();
            if (dsforSDN.Tables.Count > 0)
            {
                if (dsforSDN.Tables[1].Rows.Count > 0)
                {

                    estore.StoreName = dsforSDN.Tables[1].Rows[0]["StoreName"].ToString();
                    estore.Vatid = dsforSDN.Tables[1].Rows[0]["VATID"].ToString();
                    estore.DeliveryNoteNo = dsforSDN.Tables[1].Rows[0]["ReturnNumber"].ToString();
                    estore.DeliveryNoteDate = dsforSDN.Tables[1].Rows[0]["ReturnDate"].ToString();
                    estore.Remarks = dsforSDN.Tables[1].Rows[0]["Remarks"].ToString();

                    estore.TotalGrossValue = Convert.ToDecimal(dsforSDN.Tables[1].Rows[0]["TotalSellingPrice"]);
                    estore.TotalValue = Convert.ToDecimal(dsforSDN.Tables[1].Rows[0]["TotalQty"]);

                }
                if (dsforSDN.Tables[0].Rows.Count > 0)
                {
                    estore.OrganisationName = dsforSDN.Tables[0].Rows[0]["OrganisationName"].ToString();
                    estore.Address = dsforSDN.Tables[0].Rows[0]["Address"].ToString();
                    estore.City = dsforSDN.Tables[0].Rows[0]["City"].ToString();
                    estore.ContactNum = dsforSDN.Tables[0].Rows[0]["ContactNumber"].ToString();
                    estore.Email = dsforSDN.Tables[0].Rows[0]["Email"].ToString();

                }

            }
           

            for (int i = 0; i < dsforSDN.Tables[2].Rows.Count; i++)
            {
                liststore = new EStoreDeliveryList();
                liststore.CategoryName = dsforSDN.Tables[2].Rows[i]["CategoryName"].ToString();
                liststore.BrandName = dsforSDN.Tables[2].Rows[i]["BrandName"].ToString();
                liststore.ProductName = dsforSDN.Tables[2].Rows[i]["ProductName"].ToString();
                liststore.ProductValue = Convert.ToSingle(dsforSDN.Tables[2].Rows[i]["SellingPrice"]);
                liststore.Quantity = Convert.ToSingle(dsforSDN.Tables[2].Rows[i]["Quantity"]);
                liststore.GrossValue = Convert.ToSingle(dsforSDN.Tables[2].Rows[i]["TotalSellingPrice"]);
                estoretable.Add(liststore);
            }          
            estorelist.Add(estore);
        }
        catch (Exception)
        {

            throw;
        }
        ObjGridData.eStore = estorelist;
        ObjGridData.eStoreList = estoretable;
        return ObjGridData;
    }
}