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
public partial class Reports_IncomingStock : System.Web.UI.Page
{
    ESupplierDeliveryNote Supplier;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginID"] != null)
        {
            if (Page.IsPostBack == false)
            {
                FillSupplier();
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
                lblStatus.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
            }
            if (gvStock.HeaderRow != null)
                gvStock.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }
    }
    private void FillBrand()
    {
        Supplier = new ESupplierDeliveryNote();
        ds = new DataSet();
        ds = Supplier.ddlBrand();
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
            Supplier = new ESupplierDeliveryNote();
            ds = new DataSet();
            int categoryid = Convert.ToInt32(ddlCategory.SelectedValue);
            int brandid = Convert.ToInt32(ddlBrand.SelectedValue);
            Supplier.CategoryID = categoryid;
            Supplier.BrandID = brandid;
            ds = Supplier.ddlProducts();
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
    private void FillCategory()
    {
        Supplier = new ESupplierDeliveryNote();
        ds = new DataSet();
        ds = Supplier.ddlCategory();
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
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBrand.SelectedValue != "0")
            FillProduct();
        else
            ddlProduct.Items.Clear();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue != "0")
            FillProduct();
        else
            ddlProduct.Items.Clear();
    }
    private void FillSupplier()
    {
        try
        {
            Supplier = new ESupplierDeliveryNote();
            ds = new DataSet();
            ds = Supplier.ddlSupplier();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSupplier.DataSource = ds.Tables[0];
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataValueField = "SupplierID";
                ddlSupplier.DataBind();
                ddlSupplier.Items.Insert(0, new ListItem("--Any--", "0"));               
            }
            else
            {
                ddlSupplier.Items.Insert(0, new ListItem("--Any--", "0"));
                
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
        try
        {
            ds = new DataSet();
            Supplier = new ESupplierDeliveryNote();
            if (ddlSupplier.SelectedValue != "0")
                Supplier.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
            else
                Supplier.SupplierID = 0;
            if (txtSupplierDeliveryNoteNo.Text != "")
                Supplier.SupplierDeliveryNoteNo = txtSupplierDeliveryNoteNo.Text;
            else
                Supplier.SupplierDeliveryNoteNo = "";
            if (txtSupplierDeliveryNoteDate.Text != "")
                Supplier.SupplierDeliveryNoteDate = converttodate(txtSupplierDeliveryNoteDate.Text);
            else
                Supplier.SupplierDeliveryNoteDate = "";
            if (txtPaymentDueDate.Text != "")
                Supplier.PaymentDueDate = converttodate(txtPaymentDueDate.Text);
            else
                Supplier.PaymentDueDate = "";
            if (txtFromDate.Text != "")
                Supplier.FromDate = converttodate(txtFromDate.Text);
            else
                Supplier.FromDate = "";
            if (txtToDate.Text != "")
                Supplier.ToDate = converttodate(txtToDate.Text);
            else
                Supplier.ToDate = "";

            if (ddlCategory.SelectedValue != "0")
                Supplier.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            else
                Supplier.CategoryID = 0;
            if (ddlBrand.SelectedValue != "0")
                Supplier.BrandID = Convert.ToInt32(ddlBrand.SelectedValue);
            else
                Supplier.BrandID = 0;
            if (ddlProduct.SelectedValue != "" && ddlProduct.SelectedValue != "0")
                Supplier.ProductID = Convert.ToInt32(ddlProduct.SelectedValue);
            else
                Supplier.ProductID = 0;


            ds = Supplier.GetInflowStock();
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
                gvStock.Columns[7].Visible = true;
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
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlSupplier.SelectedValue = "0";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtSupplierDeliveryNoteDate.Text = "";
        txtPaymentDueDate.Text = "";
        txtSupplierDeliveryNoteNo.Text = "";
        ddlCategory.SelectedValue = "0";
        ddlBrand.SelectedValue = "0";
        ddlProduct.Items.Clear();
        if (gvDetails.HeaderRow != null)
            gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        if (gvStock.Visible == true )
        {
            if (gvStock.Rows.Count > 0)
            {
                //lblstatus.Text = "";
                string str = "Good Received Report";
                gvStock.HeaderStyle.BackColor = System.Drawing.Color.Black;
                gvStock.HeaderStyle.ForeColor = System.Drawing.Color.White;
                //gvStock.Font.Bold = true;
                gvStock.Font.Size = 10;
                gvStock.Font.Name = "verdana";
                //pnlgrid.Visible = false;
                gvStock.Columns[7].Visible = false;
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
                //lblstatus.Text = "";
                gvDetails.HeaderStyle.BackColor = System.Drawing.Color.Black;
                gvDetails.HeaderStyle.ForeColor = System.Drawing.Color.White;
               // gvDetails.Font.Bold = true;
                gvDetails.Font.Size = 10;
                gvDetails.Font.Name = "verdana";
                string str = "Good Received Detailed Report";
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
        lblTitle.Text = "<h3 border='0' align='center'><b>Goods Received Report </b></h3>";
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=Goods Received Report.xls";
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
        gv.Columns[7].Visible = false;
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

    public decimal total = 0;
    protected void gvStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblBuyingprice");
            if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
                buyingprice = buyingprice + Convert.ToSingle(lbl.Text);
            lbl = new Label();
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
            Label lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalBuyingprice");
            lbl.Text = Convert.ToString(buyingprice);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalSellingprice");
            lbl.Text = Convert.ToString(sellingprice);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalQuantity");
            lbl.Text = Convert.ToString(qty);
        }


    }
    protected void rbtnDetailed_CheckedChanged(object sender, EventArgs e)
    {
        buyingprice = 0; sellingprice = 0; qty = 0; totalbp = 0; totalsp = 0;
        FillDetailsGrid();
        gvDetails.Visible = true;
        gvStock.Visible = false;
    }
    protected void rbtnSummary_CheckedChanged(object sender, EventArgs e)
    {
        buyingprice = 0; sellingprice = 0; qty = 0; totalbp = 0; totalsp = 0;
        FillGrid();
        gvDetails.Visible = false;
        gvStock.Visible = true;
    }
    float buyingprice = 0, sellingprice = 0, qty = 0, totalbp = 0, totalsp = 0;
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblBuyingPrice");
            if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
                buyingprice = buyingprice + Convert.ToSingle(lbl.Text);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblSellingPrice");
            if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
                sellingprice = sellingprice + Convert.ToSingle(lbl.Text);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblQuantity");
            if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
                qty = qty + Convert.ToSingle(lbl.Text);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblNetBuyingPrice");
            if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
                totalbp = totalbp + Convert.ToSingle(lbl.Text);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblNetSellingPrice");
            if (lbl.Text.Trim() != string.Empty && lbl.Text != null)
                totalsp = totalsp + Convert.ToSingle(lbl.Text);


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalBuyingPrice");
            lbl.Text = Convert.ToString(buyingprice);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalSellingPrice");
            lbl.Text = Convert.ToString(sellingprice);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalQuantity");
            lbl.Text = Convert.ToString(qty);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalNetBuyingPrice");
            lbl.Text = Convert.ToString(totalbp);
            lbl = new Label();
            lbl = (Label)e.Row.FindControl("lblTotalNetSellingPrice");
            lbl.Text = Convert.ToString(totalsp);
        }
    }

    protected void FillDetailsGrid()
    {
        try
        {
            Supplier = new ESupplierDeliveryNote();
            ds = new DataSet();
            if (txtSupplierDeliveryNoteDate.Text != "")
                Supplier.SupplierDeliveryNoteDate = converttodate(txtSupplierDeliveryNoteDate.Text);
            else
                Supplier.SupplierDeliveryNoteDate = "";
            if (txtPaymentDueDate.Text != "")
                Supplier.PaymentDueDate = converttodate(txtPaymentDueDate.Text);
            else
                Supplier.PaymentDueDate = "";
            if (txtSupplierDeliveryNoteNo.Text != "")
                Supplier.SupplierDeliveryNoteNo = txtSupplierDeliveryNoteNo.Text;
            else
                Supplier.SupplierDeliveryNoteNo = "";
            if (ddlSupplier.SelectedValue != "0")
                Supplier.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
            else
                Supplier.SupplierID = 0;
            if (ddlCategory.SelectedValue != "0")
                Supplier.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            else
                Supplier.CategoryID = 0;
            if (ddlBrand.SelectedValue != "0")
                Supplier.BrandID = Convert.ToInt32(ddlBrand.SelectedValue);
            else
                Supplier.BrandID = 0;
            if (ddlProduct.SelectedValue != "" && ddlProduct.SelectedValue != "0")
                Supplier.ProductID = Convert.ToInt32(ddlProduct.SelectedValue);
            else
                Supplier.ProductID = 0;
            if (txtFromDate.Text != "")
                Supplier.FromDate =converttodate( txtFromDate.Text);
            else
                Supplier.FromDate = "";
            if (txtToDate.Text != "")
                Supplier.ToDate =converttodate( txtToDate.Text);
            else
                Supplier.ToDate = "";
            ds = Supplier.GetSupplierStockGrid();
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
    [WebMethod]
    public static GridDataSet SupplierForPrint(int supplierdeliverynoteid)
    {
        DataSet dsforPrint = new DataSet();
        ESupplierDeliveryNote SupplierPrint = new ESupplierDeliveryNote();
        ESupplierDeliveryNoteList listSupplier = new ESupplierDeliveryNoteList();

        DataSet ds = new DataSet();
        List<ESupplierDeliveryNote> esupplier = new List<ESupplierDeliveryNote>();
        List<ESupplierDeliveryNoteList> esupplierlist = new List<ESupplierDeliveryNoteList>();

        GridDataSet objGridDataset = new GridDataSet();
        ESupplierDeliveryNote SDN = new ESupplierDeliveryNote();
        try
        {
            //SupplierPrint.SupplierDeliveryNoteID = supplierdeliverynoteid;
            SDN.SupplierDeliveryNoteID = supplierdeliverynoteid;
            dsforPrint = SDN.GridForViewEdit();
            if (dsforPrint.Tables[0].Rows.Count > 0)
            {

                SDN.OrganisationID = Convert.ToInt32(dsforPrint.Tables[0].Rows[0]["OrganisationID"]);
                // SDN.PWhereCondition = Convert.ToString(ds.Tables[0].Rows[0]["SupplierID"]);
                SDN.PWhereCondition += " Sdn.SupplierDeliveryNoteID= " + supplierdeliverynoteid;

            }
            DataSet dsforSupID = SDN.GetPrintScreen();
            if (dsforSupID.Tables[0].Rows.Count > 0)
            {
                SupplierPrint.SupplierName = dsforSupID.Tables[0].Rows[0]["SupplierName"].ToString();
                SupplierPrint.SupplierDeliveryNoteNo = dsforSupID.Tables[0].Rows[0]["SupplierDeliveryNoteNo"].ToString();
                SupplierPrint.Vatid = dsforSupID.Tables[0].Rows[0]["VATID"].ToString();
                //SupplierPrint.SupplierDeliveryNoteDate = dsforSupID.Tables[0].Rows[0]["SupplierDeliveryNoteDate"].ToString();
                string supdelnotedate = dsforSupID.Tables[0].Rows[0]["SupplierDeliveryNoteDate"].ToString();
                DateTime MyDateTime = new DateTime();
                MyDateTime = Convert.ToDateTime(supdelnotedate);
                string date = MyDateTime.ToString("dd-MM-yyyy");
                SupplierPrint.SupplierDeliveryNoteDate = date.ToString();
            }
            DataSet dsforOrgntnID = SDN.GetPrintOrganisation();
            if (dsforOrgntnID.Tables[0].Rows.Count > 0)
            {
                SupplierPrint.OrganisationName = dsforOrgntnID.Tables[0].Rows[0]["OrganisationName"].ToString();
                SupplierPrint.Address = dsforOrgntnID.Tables[0].Rows[0]["Address"].ToString();
                SupplierPrint.Email = dsforOrgntnID.Tables[0].Rows[0]["Email"].ToString();
                SupplierPrint.City = dsforOrgntnID.Tables[0].Rows[0]["City"].ToString();
                SupplierPrint.ContactNumber = dsforOrgntnID.Tables[0].Rows[0]["ContactNumber"].ToString();
            }
            if (dsforSupID.Tables[0].Rows.Count > 0)
            {
                SupplierPrint.HandlingCharges = Convert.ToDecimal(dsforSupID.Tables[0].Rows[0]["HandlingCharges"].ToString());
                SupplierPrint.NetProductValue = Convert.ToDecimal(dsforSupID.Tables[0].Rows[0]["NetProductValue"].ToString()); ;
                SupplierPrint.TotalValue = Convert.ToDecimal(dsforSupID.Tables[0].Rows[0]["TotalValue"].ToString());
                SupplierPrint.Remarks = dsforSupID.Tables[0].Rows[0]["Remarks"].ToString();
            }
            esupplier.Add(SupplierPrint);

            SDN.PWhereCondition = supplierdeliverynoteid.ToString();

            ds = SDN.GetPrintGridTable();
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    listSupplier = new ESupplierDeliveryNoteList();
                    listSupplier.CategoryName = (ds.Tables[0].Rows[i]["CategoryName"]).ToString();
                    listSupplier.ProductName = (ds.Tables[0].Rows[i]["ProductName"]).ToString();
                    listSupplier.BrandName = (ds.Tables[0].Rows[i]["BrandName"]).ToString();
                    listSupplier.Quantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"].ToString());
                    listSupplier.ProductValue = Convert.ToDecimal(ds.Tables[0].Rows[i]["ProductValue"]);
                    listSupplier.BuyingPriceperPice = Convert.ToDecimal(ds.Tables[0].Rows[i]["BuyingPrice"]);
                    listSupplier.GrossValue = (Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalGrossValue"]));
                    listSupplier.NetBuyingPrice = Convert.ToDecimal((listSupplier.Quantity) * (listSupplier.BuyingPriceperPice));
                    esupplierlist.Add(listSupplier);
                }

            }

            objGridDataset.eSupplier = esupplier;
            objGridDataset.eSupplierlist = esupplierlist;
        }
        catch (Exception)
        {

            throw;
        }
        return objGridDataset;
    }
    protected void gvStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);
            int supplierdeliverynoteid = Convert.ToInt32(gvStock.DataKeys[rowindex].Value);
            Supplier = new ESupplierDeliveryNote();
            ds = new DataSet();
            Supplier.SupplierDeliveryNoteID = supplierdeliverynoteid;
            try
            {
                Supplier.LoginID = Convert.ToInt32(Session["LOGINID"].ToString());
                Supplier.SupplierDeliveryNoteID = supplierdeliverynoteid;

                if (supplierdeliverynoteid != 0)
                {

                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "SalesForPrint(" + supplierdeliverynoteid + ");", true);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}
