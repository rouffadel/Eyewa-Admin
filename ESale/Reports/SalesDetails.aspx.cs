using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ESaleEntity;
using System.IO;
public partial class Reports_SalesDetails : System.Web.UI.Page
{
    EStoreDeliveryNote Store;
    DataSet ds;
    ESales Sales;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginID"] != null)
        {
            if (Page.IsPostBack == false)
            {
                FillStore();
                FillCategory();
                FillBrand();
                //DateTime baseDate = DateTime.Today;
                //var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
                //string startdate = thisMonthStart.ToString("dd-MM-yyyy");
                //txtFromDate.Text = startdate;
                //var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
                //string enddate = thisMonthEnd.ToString("dd-MM-yyyy");
                //txtToDate.Text = enddate;
                txtFromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                FillGrid();
                lblStatus.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
            }
            if (gvSales.HeaderRow != null)
                gvSales.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            ddlProduct.Items.Clear();
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
        Sales = new ESales();
        ds = new DataSet();
        ds = Sales.ddlBrand();
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
        
        Sales = new ESales();
        ds = new DataSet();        
        ds = Sales.ddlCategory();
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
    int salescount = 0,saleidcount=0;
    decimal totaltodaypaidamount = 0, totalpreviousinvoicepayment = 0;
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
            if (txtCustomerName.Text != "")
                Sales.CustomerName = txtCustomerName.Text;
            else
                Sales.CustomerName = "";
            if (txtCustomerNo.Text != "")
                Sales.CustomerNo = txtCustomerNo.Text;
            else
                Sales.CustomerNo = "";
            if (txtInvoiceNo.Text != "")
                Sales.InvoiceNo = txtInvoiceNo.Text;
            else
                Sales.InvoiceNo = "";
            if (txtFromDate.Text != "")
                Sales.FromDate = converttodate(txtFromDate.Text);
            else
                Sales.FromDate = "";
            if (txtToDate.Text != "")
                Sales.ToDate = converttodate(txtToDate.Text);
            else
                Sales.ToDate = "";
           
            Sales.LoginID = Convert.ToInt32(Session["LOGINID"]);
            Sales.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            ds = Sales.GetSalesGridReport();
            ds.Tables[0].Columns.Add("PreviousAmount");
            ds.Tables[0].Columns.Add("mark");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add("SNo");
                    int j = 1; int k = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //if (k < ds.Tables[0].Rows.Count - 1)
                        //{
                        //    if (Convert.ToInt32(ds.Tables[0].Rows[k]["SaleId"]) == Convert.ToInt32(ds.Tables[0].Rows[k + 1]["SaleId"]))
                        //        ds.Tables[0].Rows[i]["mark"] = true;
                        //    else
                        //        ds.Tables[0].Rows[i]["mark"] = false;
                        //}
                        ds.Tables[0].Rows[i]["SNo"] = j.ToString();
                        ds.Tables[0].Rows[i]["PreviousAmount"] = 0;
                        // k++;
                        j++;
                    }
                    salescount = 0;
                    saleidcount = 0;
                    gvSales.DataSource = ds;
                    gvSales.DataBind();
                    gvSales.Columns[8].Visible = true;
                    gvSales.Visible = true;
                    gvSales.Columns[9].Visible = true;
                    gvSales.Columns[10].Visible = true;
                   gvSales.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridViewRow frow = gvSales.FooterRow;
                     totaltodaypaidamount = 0;
                    totaltodaypaidamount = Convert.ToDecimal(((Label)frow.FindControl("lblTotalGross")).Text);
                }
                else
                {
                    gvSales.DataSource = null;
                    gvSales.DataBind();
                    gvSales.Visible = false;
                }
              
                if (ds.Tables[1].Rows.Count > 0)
                {
                    previouspaidmaount = 0;
                    int j = 1;
                    ds.Tables[1].Columns.Add("SNo");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        ds.Tables[1].Rows[i]["SNo"] = j.ToString();
                        j++;
                    }
                  
                    gvPreviousInvoicePayments.DataSource = ds.Tables[1];
                    gvPreviousInvoicePayments.DataBind();
                   // gvPreviousInvoicePayments.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvPreviousInvoicePayments.Visible = true;
                  GridViewRow  frow = gvPreviousInvoicePayments.FooterRow;
                    totalpreviousinvoicepayment = 0;
                    totalpreviousinvoicepayment = Convert.ToDecimal(((Label)frow.FindControl("lblTotalGross")).Text);
                }
                else
                {
                    gvPreviousInvoicePayments.DataSource = null;
                    gvPreviousInvoicePayments.DataBind();
                    gvPreviousInvoicePayments.Visible = false;
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    gvCashCard.DataSource = ds.Tables[2];
                    gvCashCard.DataBind();
                    gvCashCard.Visible = true;
                }
                else
                {
                    gvCashCard.DataSource = null;
                    gvCashCard.DataBind();
                    gvCashCard.Visible = false;
                }
                if (totaltodaypaidamount != 0 || totalpreviousinvoicepayment != 0)
                {
                    decimal total = totaltodaypaidamount + totalpreviousinvoicepayment;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("TodayInvPaidAmount");
                    dt.Columns.Add("PreviousInvPaidAmount");
                    dt.Columns.Add("Total");
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                    dt.Rows[0]["TodayInvPaidAmount"] = totaltodaypaidamount.ToString();
                    dt.Rows[0]["PreviousInvPaidAmount"] = totalpreviousinvoicepayment.ToString();
                    dt.Rows[0]["Total"] = total.ToString();
                    gvResult.DataSource = dt;
                    gvResult.DataBind();
                   // gvResult.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvResult.Visible = true;
                }
                else {
                    gvResult.DataSource = null;
                    gvResult.DataBind();
                    gvResult.Visible = false;
                }
                if (gvSales.Visible == true || gvPreviousInvoicePayments.Visible == true || gvResult.Visible == true)
                {
                    btnprint.Visible = true;
                    btnExport.Visible = true;
                    lblStatus.Text = "";
                }
                else
                {
                    dvFailure.Visible = true;
                    lblStatus.Text = "No Records Found.";
                    btnprint.Visible = false;
                    btnExport.Visible = false;
                }
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    int count = 0;
                //   // int count1 = 0;
                //    int lastsaleid = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count-1]["SaleID"]);
                //    if (Convert.ToString(ds.Tables[0].Rows[i]["invoicepaymentid"]) != "")
                //    {                       
                //        for (int k = 0; k < ds.Tables[0].Rows.Count-1 ; k++)
                //        {
                           
                //            if (Convert.ToInt32(ds.Tables[0].Rows[k]["SaleID"]) == Convert.ToInt32(ds.Tables[0].Rows[k+1]["SaleID"]))
                //            {
                //               // count1++;
                //                if (count == 0)
                //                {
                //                    ds.Tables[0].Rows[k]["PreviousAmount"] = 0;
                //                    count++;
                //                }
                //                else
                //                {
                //                    if (Convert.ToInt32(ds.Tables[0].Rows[k]["SaleID"]) == Convert.ToInt32(ds.Tables[0].Rows[k - 1]["SaleID"]))
                //                    {
                //                        ds.Tables[0].Rows[k]["PreviousAmount"] = ds.Tables[0].Rows[k - 1]["PaymentAmount"];
                //                    }
                //                }
                //            }
                //            if (k > 0)
                //            {
                //                if (Convert.ToInt32(ds.Tables[0].Rows[k]["SaleID"]) != Convert.ToInt32(ds.Tables[0].Rows[k + 1]["SaleID"]))
                //                {
                //                    if (Convert.ToInt32(ds.Tables[0].Rows[k]["SaleID"]) == Convert.ToInt32(ds.Tables[0].Rows[k - 1]["SaleID"]))
                //                    {
                //                        ds.Tables[0].Rows[k]["PreviousAmount"] = ds.Tables[0].Rows[k - 1]["PaymentAmount"];
                //                    }
                //                }
                //            }
                //        }
                //        if (Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 2]["SaleID"]) == lastsaleid)
                //        {
                //            ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["PreviousAmount"] = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 2]["PaymentAmount"];
                //        }
                //    }
                //}
               
                
               // lblStatus.Text = "";
            }
            else
            {
                dvFailure.Visible = true;
                lblStatus.Text = "No Records Found.";
                gvSales.DataSource = null;
                gvSales.DataBind();
                gvSales.Visible = false;
                btnExport.Visible = false;
                btnprint.Visible = false;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //divprintarea.Attributes["style"] = "display:none";
    }
    [WebMethod]
    public static GridDataSet SalesPrint(string salesid,string fromdate)
    {
        DataSet dsfotStore = new DataSet();
        DataSet dsSales = new DataSet();
        ESales Sale = new ESales();
        ESales salepriscription = new ESales();
        ESaleList listSale = new ESaleList();
        DataSet ds = new DataSet();
        List<ESales> esales = new List<ESales>();
        List<ESaleList> esaleslist = new List<ESaleList>();
        List<ESales> lstsalepriscription = new List<ESales>();
        GridDataSet objGridDataset = new GridDataSet();
        Sale.SalesID = Convert.ToInt32(salesid);
        if (fromdate != "")
            Sale.FromDate = converttodate(fromdate);
        try
        {
            
            ds = Sale.GetSalesDetailsGrid();
            //ds = Sale.GetPrint();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Sale.StoreID = Convert.ToInt32(ds.Tables[0].Rows[0]["StoreID"]);
                    Sale.CustomerName = Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                    Sale.CustomerNo = Convert.ToString(ds.Tables[0].Rows[0]["CustomerNo"]);
                    Sale.GrossTotal = Convert.ToSingle(ds.Tables[0].Rows[0]["GrossTotal"]);
                    Sale.Discount = Convert.ToSingle(ds.Tables[0].Rows[0]["Discount"]);
                    Sale.NetTotal = Convert.ToSingle(ds.Tables[0].Rows[0]["NetTotal"]);

                    Sale.InvoiceDate = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceDate"]);
                    Sale.InvoiceNo = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]);

                    //txtBalance.Text = Convert.ToString(ds.Tables[0].Rows[0]["Balance"]);
                    Sale.NetTotal = Convert.ToSingle(ds.Tables[0].Rows[0]["NetTotal"]);

                    DataSet dsforprint = new DataSet();
                    dsforprint = Sale.GetEPriscriptionPrintPopup();
                    if (dsforprint.Tables[0].Rows.Count > 0)
                    {
                        salepriscription.SPH_RightEye = (dsforprint.Tables[0].Rows[0]["SPH_RightEye"]).ToString();
                        salepriscription.CYL_RightEye = (dsforprint.Tables[0].Rows[0]["CYL_RightEye"]).ToString();
                        salepriscription.AXIS_RightEye = (dsforprint.Tables[0].Rows[0]["AXIS_RightEye"]).ToString();
                        salepriscription.ADD_RightEye = (dsforprint.Tables[0].Rows[0]["ADD_RightEye"]).ToString();
                        salepriscription.SPH_LeftEye = (dsforprint.Tables[0].Rows[0]["SPH_LeftEye"]).ToString();
                        salepriscription.CYL_LeftEye = (dsforprint.Tables[0].Rows[0]["CYL_LeftEye"]).ToString();
                        salepriscription.AXIS_LeftEye = (dsforprint.Tables[0].Rows[0]["AXIS_LeftEye"]).ToString();
                        salepriscription.ADD_LeftEye = dsforprint.Tables[0].Rows[0]["ADD_LeftEye"].ToString();
                        salepriscription.SPH_IPD = dsforprint.Tables[0].Rows[0]["SPH_IPD"].ToString();
                        salepriscription.CYL_IPD = dsforprint.Tables[0].Rows[0]["CYL_IPD"].ToString();
                        salepriscription.AXIS_IPD = dsforprint.Tables[0].Rows[0]["AXIS_IPD"].ToString();
                        salepriscription.ADD_IPD = dsforprint.Tables[0].Rows[0]["ADD_IPD"].ToString();

                        lstsalepriscription.Add(salepriscription);
                    }
                }
                if (ds.Tables[2].Rows.Count > 0)
                {

                    if (ds.Tables[2].Rows[0]["PaidAmount"].ToString() == "")
                    {
                        Sale.PaidAmount = 0;
                    }
                    else
                    {
                        Sale.PaidAmount = Convert.ToSingle(ds.Tables[2].Rows[0]["PaidAmount"]);
                    }

                }

            }
            dsfotStore = Sale.GetPrintStore();
            if (dsfotStore.Tables[0].Rows.Count > 0)
            {
                Sale.StoreName = dsfotStore.Tables[0].Rows[0]["StoreName"].ToString();
                Sale.Address = dsfotStore.Tables[0].Rows[0]["Address"].ToString();
                Sale.City = dsfotStore.Tables[0].Rows[0]["City"].ToString();
                Sale.Email = dsfotStore.Tables[0].Rows[0]["EmailID"].ToString();
                Sale.ContactNum = dsfotStore.Tables[0].Rows[0]["ContactNumber"].ToString();
                Sale.VatID = dsfotStore.Tables[0].Rows[0]["VATID"].ToString();
                Sale.ZipCode = dsfotStore.Tables[0].Rows[0]["ZipCode"].ToString();
            }

            esales.Add(Sale);
            dsSales = Sale.GetPrintSales();
            if (dsSales.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                {
                    listSale = new ESaleList();
                    listSale.CategoryName = Convert.ToString(dsSales.Tables[0].Rows[i]["CategoryName"]);
                    listSale.BrandName = Convert.ToString(dsSales.Tables[0].Rows[i]["BrandName"]);
                    listSale.ProductName = Convert.ToString(dsSales.Tables[0].Rows[i]["ProductName"]);
                    listSale.Quantity = Convert.ToInt32(dsSales.Tables[0].Rows[i]["Quantity"]);
                    listSale.ProductValue = Convert.ToSingle(dsSales.Tables[0].Rows[i]["ProductValue"]);
                    listSale.SellingPrice = Convert.ToSingle(dsSales.Tables[0].Rows[i]["SellingPrice"]);
                    //listSale.TotalValue = Convert.ToSingle(dsSales.Tables[0].Rows[i]["NetValue"]);
                    listSale.TotalGrossValue = Convert.ToSingle(dsSales.Tables[0].Rows[0]["GrossTotal"]);
                    listSale.Discount = Convert.ToSingle(dsSales.Tables[0].Rows[0]["Discount"]);
                    listSale.NetValue = Convert.ToSingle(dsSales.Tables[0].Rows[0]["NetTotal"]);
                    listSale.Balance = Convert.ToSingle(dsSales.Tables[0].Rows[0]["Balance"]);
                    listSale.Remarks = dsSales.Tables[0].Rows[0]["Remarks"].ToString();
                    esaleslist.Add(listSale);
                }
            }
            
            DataSet dsorder = new DataSet();
            dsorder = Sale.GetOrderLenseGrid();
            float totalordensevalue = 0;
            for (int j = 0; j < dsorder.Tables[0].Rows.Count; j++)
            {
                listSale = new ESaleList();
                listSale.CategoryName = Convert.ToString(dsorder.Tables[0].Rows[j]["Category"]);
                listSale.BrandName = "";
                listSale.ProductName = Convert.ToString(dsorder.Tables[0].Rows[j]["Orderlense"]);
                listSale.Quantity = Convert.ToInt32(dsorder.Tables[0].Rows[j]["Quantity"]);
                listSale.ProductValue = Convert.ToSingle(dsorder.Tables[0].Rows[j]["Price"]);
                listSale.SellingPrice = Convert.ToSingle(dsorder.Tables[0].Rows[j]["Total"]);
                totalordensevalue += Convert.ToSingle(dsorder.Tables[0].Rows[j]["Total"]);
                esaleslist.Add(listSale);
            }
            if (totalordensevalue != 0)
            {
                esaleslist[0].TotalGrossValue = Convert.ToSingle(esaleslist[0].TotalGrossValue) + 0;
                esaleslist[0].NetValue = Convert.ToSingle(esaleslist[0].NetValue) + 0;
                esaleslist[0].Balance = Convert.ToSingle(esaleslist[0].Balance) + 0;
            }

        }
        catch (Exception)
        {

            throw;
        }
        objGridDataset.eSales = esales;
        objGridDataset.eSaleslist = esaleslist;
        objGridDataset.eSalePriscription = lstsalepriscription;
        return objGridDataset;
    }
    protected void FillDetailsGrid()
    {
        try
        {
            Sales = new ESales();
            ds = new DataSet();
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
            if (txtCustomerName.Text != "")
                Sales.CustomerName = txtCustomerName.Text;
            else
                Sales.CustomerName = "";
            if (txtCustomerNo.Text != "")
                Sales.CustomerNo = txtCustomerNo.Text;
            else
                Sales.CustomerNo = "";
            if (txtInvoiceNo.Text != "")
                Sales.InvoiceNo = txtInvoiceNo.Text;
            else
                Sales.InvoiceNo = "";
            if (txtFromDate.Text != "")
                Sales.FromDate = converttodate(txtFromDate.Text);
            else
                Sales.FromDate = "";
            if (txtToDate.Text != "")
                Sales.ToDate = converttodate(txtToDate.Text);
            else
                Sales.ToDate = "";
           
            Sales.LoginID = Convert.ToInt32(Session["LOGINID"]);
            Sales.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            ds = Sales.GetSalesDetailsReport();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("SNo");
                int j = 1;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["SNo"] = j.ToString();
                    j++;
                }
                l = 0;
                gvSaleDetails.DataSource = ds;
                gvSaleDetails.DataBind();
                gvSaleDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvSaleDetails.Visible = true;
                btnExport.Visible = true;
                btnprint.Visible = true;
                lblStatus.Text = "";
            }
            else
            {
                dvFailure.Visible = true;
                lblStatus.Text = "No Records Found.";
                gvSaleDetails.DataSource = null;
                gvSaleDetails.DataBind();
                gvSaleDetails.Visible = false;
                btnExport.Visible = false;
                btnprint.Visible = false;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    int l = 0;
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
        ddlCategory.SelectedValue = "0";
        ddlBrand.SelectedValue = "0";
        ddlProduct.Items.Clear();
        txtCustomerName.Text = "";
        txtCustomerNo.Text = "";
        txtToDate.Text = "";
        txtFromDate.Text = "";
        txtInvoiceNo.Text = "";
        lblStatus.Text = "";
        if (gvSaleDetails.HeaderRow != null)
            gvSaleDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        if (gvSales.Visible == true)
        {
            string str = "Sales Report";
            if (gvSales.Rows.Count > 0)
            {
                //lblstatus.Text = "";
                gvSales.HeaderStyle.BackColor = System.Drawing.Color.Black;
                gvSales.HeaderStyle.ForeColor = System.Drawing.Color.White;
               // gvSales.Font.Bold = true;
                gvSales.Font.Size = 10;
                gvSales.Font.Name = "verdana";
                //gvSales.Font.Name"verdana";
                //gvSales.Font.Name = "verdana";
               
                //pnlgrid.Visible = false;
                gvSales.Columns[9].Visible = false;
                gvSales.Columns[10].Visible = false;
                Session["ctrl"] = gvSales;
            }
                if (gvPreviousInvoicePayments.Rows.Count >= 0)
                {
                    gvPreviousInvoicePayments.HeaderStyle.BackColor = System.Drawing.Color.Black;
                    gvPreviousInvoicePayments.HeaderStyle.ForeColor = System.Drawing.Color.White;
                  //  gvPreviousInvoicePayments.Font.Bold = true;
                    gvPreviousInvoicePayments.Font.Size = 10;
                    gvPreviousInvoicePayments.Font.Name = "verdana";
                    Session["ctrl3"] = gvPreviousInvoicePayments;
                }
                //pnlgrid.Visible = false;
                if (gvResult.Rows.Count >= 0)
                {
                    gvResult.HeaderStyle.BackColor = System.Drawing.Color.Black;
                    gvResult.HeaderStyle.ForeColor = System.Drawing.Color.White;
                  //  gvResult.Font.Bold = true;
                    gvResult.Font.Size = 10;
                    gvResult.Font.Name = "verdana";
                    Session["ctrl1"] = gvResult;
                }
            
                string appPath = HttpContext.Current.Request.ApplicationPath;
                ClientScript.RegisterStartupScript(this.GetType(), "onclick",
                 "<script language=javascript>window.open('Reports.aspx?PageName=" + str + "', '');</script>");
   //             ScriptManager.RegisterStartupScript(up1, up1.GetType(),
   //"myFunction", "printsales();", true);
              
            
        }
        else
        {
            if (gvSaleDetails.Rows.Count > 0)
            {
                //lblstatus.Text = "";
                string str = "Sales Detailed Report";
                gvSaleDetails.HeaderStyle.BackColor = System.Drawing.Color.Black;
                gvSaleDetails.HeaderStyle.ForeColor = System.Drawing.Color.White;
              //  gvSaleDetails.Font.Bold = true;
                
                //pnlgrid.Visible = false;
                Session["ctrl"] = gvSaleDetails;
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
            gvSales.Columns[9].Visible = false;
            gvSales.Columns[10].Visible = false;
            ExportFromHtmlForm(gvSales,gvPreviousInvoicePayments,gvResult);
        }
        else if (rbtnDetailed.Checked)
        {
            ExportFromHtmlForm(gvSaleDetails,null,null);
        }
        //ExportFromHtmlForm(gvSaleDetails);
    }
    public void ExportFromHtmlForm(GridView gv,GridView previous,GridView result)
    {
        lblTitle.Text = "<h3 border='0' align='center'><b>Sales Details Report </b></h3>";
        HtmlForm form = new HtmlForm();
        string attachment = "attachment; filename=Sales Details Report.xls";
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
        for (int i = 0; i < previous.Rows.Count; i++)
        {
            GridViewRow row = previous.Rows[i];

            row.BackColor = System.Drawing.Color.White;
            if (i % 2 != 0)
            {
                previous.Rows[i].Style.Add("background-color", "#f2f2f2");
            }
            else
            {
                previous.Rows[i].Style.Add("background-color", "#ffffff");
            }

        }
        for (int i = 0; i < result.Rows.Count; i++)
        {
            GridViewRow row = result.Rows[i];

            row.BackColor = System.Drawing.Color.White;
            if (i % 2 != 0)
            {
                result.Rows[i].Style.Add("background-color", "#f2f2f2");
            }
            else
            {
                result.Rows[i].Style.Add("background-color", "#ffffff");
            }

        }
        gv.HeaderRow.Style.Add("background-color", "black");
        gv.HeaderRow.Style.Add("color", "white"); 
        gv.Parent.Controls.Add(form);
        if(previous!=null)
            previous.Parent.Controls.Add(form);
        if(result!=null)
            result.Parent.Controls.Add(form);
        form.Attributes["runat"] = "server";
        form.Controls.Add(gv);
        form.Controls.Add(new Literal() { ID = "br1", Text = "<br/>" });
        form.Controls.Add(previous);
        form.Controls.Add(new Literal() { ID = "br1", Text = "<br/>" });
        form.Controls.Add(result);
        
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

    public decimal totalGross = 0;
    public decimal totalSellingPrice = 0;
    public decimal totalNetTotal = 0;
    protected void gvSaleDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ds != null)
            {
                float grossvalue;
                float productvalue;
                int quantity;
                productvalue = Convert.ToSingle(ds.Tables[0].Rows[l]["ProductValue"]);
                quantity = Convert.ToInt32(ds.Tables[0].Rows[l]["Quantity"]);
                grossvalue =(float) Math.Round(quantity * productvalue, 2);
                ((Label)e.Row.FindControl("lblGross")).Text = grossvalue.ToString();
                Label lblGross = new Label();
                lblGross = (Label)e.Row.FindControl("lblGross");
                if (lblGross.Text.Trim() != string.Empty && lblGross.Text != null)
                    totalGross = totalGross + Convert.ToDecimal(lblGross.Text);

                Label lblSellingPrice = new Label();
                lblSellingPrice = (Label)e.Row.FindControl("lblSellingPrice");
                if (lblSellingPrice.Text.Trim() != string.Empty && lblSellingPrice.Text != null)
                    totalSellingPrice = totalSellingPrice + Convert.ToDecimal(lblSellingPrice.Text);

                Label lblNetTotal = new Label();
                lblNetTotal = (Label)e.Row.FindControl("lblNetTotal");
                if (lblNetTotal.Text.Trim() != string.Empty && lblNetTotal.Text != null)
                    totalNetTotal = totalNetTotal + Convert.ToDecimal(lblNetTotal.Text);

            }
            l++;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalGross = new Label();
            lblTotalGross = (Label)e.Row.FindControl("lblTotalGross");
            lblTotalGross.Text = Convert.ToString(totalGross);


            Label lblTotalSellingPrice = new Label();
            lblTotalSellingPrice = (Label)e.Row.FindControl("lblTotalSellingPrice");
            lblTotalSellingPrice.Text = Convert.ToString(totalSellingPrice);

            Label lblTotalNetTotal = new Label();
            lblTotalNetTotal = (Label)e.Row.FindControl("lblTotalNetTotal");
            lblTotalNetTotal.Text = Convert.ToString(totalNetTotal);
        }
    }
    protected void rbtnDetailed_CheckedChanged(object sender, EventArgs e)
    {
        buyingprice = 0; sellingprice = 0; qty = 0; totalbp = 0; totalsp = 0;
        FillDetailsGrid();
        gvSaleDetails.Visible = true;
        gvSales.Visible = false;
        gvResult.Visible = false;
        gvPreviousInvoicePayments.Visible = false;
    }
    float buyingprice = 0, sellingprice = 0, qty = 0, totalbp = 0, totalsp = 0;
    protected void rbtnSummary_CheckedChanged(object sender, EventArgs e)
    {
        buyingprice = 0; sellingprice = 0; qty = 0; totalbp = 0; totalsp = 0;
        FillGrid();
        gvSaleDetails.Visible = false;
        gvSales.Visible = true;
    }
  public  decimal previouspaidmaount = 0;
    protected void gvPreviousInvoicePayments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblpreviouspaidamount = new Label();
            lblpreviouspaidamount = (Label)e.Row.FindControl("lblPreGross");
            if (lblpreviouspaidamount.Text.Trim() != "" && lblpreviouspaidamount.Text.Trim() != null)
            {
                previouspaidmaount += Convert.ToDecimal(lblpreviouspaidamount.Text);
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            ((Label)e.Row.FindControl("lblTotalGross")).Text = Convert.ToString(previouspaidmaount);
        }
    }
    public decimal totinvoiceamount = 0;
    public decimal totpaidamount = 0;
    public decimal totbalance = 0;
    protected void gvSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            float paidamount = 0, invoiceamount = 0, bal = 0, previousamt = 0;
            Label lblinvoiceamount = new Label();
            lblinvoiceamount = (Label)e.Row.FindControl("lblNetTotal");
            if (lblinvoiceamount.Text.Trim() != "" && lblinvoiceamount.Text.Trim() != null)
            {
                totinvoiceamount = totinvoiceamount + Convert.ToDecimal(lblinvoiceamount.Text);
                invoiceamount = Convert.ToSingle(lblinvoiceamount.Text);
            }

            Label lblpaidamount = new Label();
            lblpaidamount = (Label)e.Row.FindControl("lblGross");
            if (lblpaidamount.Text.Trim() != "" && lblpaidamount.Text.Trim() != null)
            {
                paidamount = Convert.ToSingle(lblpaidamount.Text);
                totpaidamount = totpaidamount + Convert.ToDecimal(lblpaidamount.Text);
            }
            //bool yes=false;
            //if (Convert.ToString(ds.Tables[0].Rows[saleidcount]["mark"]) != "")
            //{
            //    if (Convert.ToBoolean(ds.Tables[0].Rows[saleidcount]["mark"]) == true)
            //        yes = true;
            //    else
            //        yes = false;
            //}
            Label lblbalance = new Label();
            lblbalance = (Label)e.Row.FindControl("lblDiscount");
            if (lblbalance.Text.Trim() != "" && lblbalance.Text.Trim() != null)
            {
                totbalance = totbalance + Convert.ToDecimal(lblbalance.Text);
                bal = Convert.ToSingle(lblbalance.Text);
                //if (yes)
                //{
                //    lblbalance.Attributes.Add("style", "text-decoration: line-through;");
                    
                //}
            }
            saleidcount++;
            if (bal > 0)
            {
                string invoiceno = string.Empty;
                if (invoiceamount == bal)
                    e.Row.BackColor = System.Drawing.Color.Red;
                else
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            else if (invoiceamount > 0 && bal == 0)
            {
                if ( bal == 0)
                    e.Row.BackColor = System.Drawing.Color.Green;
                else if(paidamount>0 && bal==0)
                    e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            else if (invoiceamount == 0 && bal == 0)
                e.Row.BackColor = System.Drawing.Color.White;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //if(ds.Tables[1].Rows.Count>0)
            //{
            //    decimal tnt = 0, tp = 0, tb = 0;
            //    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            //    {
            //        tnt += Convert.ToDecimal(ds.Tables[1].Rows[i]["NetTotal"]);
            //        //tp += Convert.ToDecimal(ds.Tables[1].Rows[i][""]);
            //        tb += Convert.ToDecimal(ds.Tables[1].Rows[i]["Balance"]);
            //    }
            ((Label)e.Row.FindControl("lblTotalNetTotal")).Text = Convert.ToString(totinvoiceamount);
            ((Label)e.Row.FindControl("lblTotalGross")).Text = totpaidamount.ToString();
            ((Label)e.Row.FindControl("lblTotalDiscount")).Text = Convert.ToString(totbalance);
            //}

        }
    }
    protected void gvSales_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
           int rowindex = Convert.ToInt32(e.CommandArgument);
           int salesid = Convert.ToInt32(gvSales.DataKeys[rowindex].Value);
           ESales Sale = new ESales();
            ds = new DataSet();
            Sale.SalesID = salesid;
            if (salesid != 0)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "SalesForPrint(" + salesid + ");", true);
            }

        }
        else if (e.CommandName == "Detail")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvSales.DataKeys[rowindex].Value);
            ds = new DataSet();
            Sales = new ESales();
            Sales.SalesID = id;
            ds = Sales.GetSalesDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dvDetails.Attributes["style"] = "display:block;";
                gvDetails.DataSource = ds.Tables[0];
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
    protected void btnDetailCancel_Click(object sender, EventArgs e)
    {

        dvDetails.Attributes["style"] = "display:none;";
        gvDetails.DataSource = null;
        gvDetails.DataBind();
        gvDetails.Visible = false;
    }
}
