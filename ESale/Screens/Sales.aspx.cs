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
using System.Web.Services;
using System.Collections.Generic;

public partial class Screens_Sales : System.Web.UI.Page
{
    int l = 0;
    ESales Sale;
    EProduct product;   
    DataSet ds;
    DataTable dtCategory, dtBrand, dt, dt1, dt3;
    ECheckPermission ECPobj;
    DataTable dtsph, dtcyl, dtaxis, dtadd;
    static bool addPermission = false;
    static bool viewPermission = true;
    static bool EditPermission = true;
    static bool deletepermission = true;
    string ScreenUrl = string.Empty;
    float totalsellingprice = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScreenUrl = Request.FilePath;
        ScreenUrl = ScreenUrl.Substring(ScreenUrl.LastIndexOf('/') + 1);
        if (!IsPostBack)
        {
            if (Session["LOGINID"] != null)
            {
                Session["LOGINID"] = Session["LOGINID"].ToString();
            }
            else
            {
                Response.Redirect("~\\Login.aspx");
            }
            if (Session["LOGINID"].ToString() != "1")
            {
                PCheckPermission();
            }
            else
            {
                addPermission = true;
                viewPermission = true;
                EditPermission = true;
                deletepermission = true;
            }
          
            //FillOrganisation();
            //txtFromDate.Text = System.DateTime.Today.ToString("dd-MM-yyyy");
            //txtToDate.Text = System.DateTime.Today.ToString("dd-MM-yyyy");

            //DateTime baseDate = DateTime.Today;
            //var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
            //string startdate = thisMonthStart.ToString("dd-MM-yyyy");
            //txtFromDate.Text = startdate;
            //var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
            //string enddate = thisMonthEnd.ToString("dd-MM-yyyy");
            //txtToDate.Text = enddate;
            txtFromDate.Text = System.DateTime.UtcNow.AddHours(3).ToString("dd-MM-yyyy");
            txtToDate.Text = System.DateTime.UtcNow.AddHours(3).ToString("dd-MM-yyyy");
            string str = Convert.ToString(System.DateTime.UtcNow.AddHours(3)).Split(' ')[1];
            FillStore();
            lblStatus.Text = string.Empty;
            lblSuccess.Text = string.Empty;
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            txtInvoiceDate.Text = DateTime.UtcNow.AddHours(3).ToString("dd-MM-yyyy");
            txtInvoiceDate.Text +=" "+ str;
            //DateTime dt = DateTime.UtcNow.AddHours(3).ToString(";
            FillGrid();
            ShowTabs(2);
        }
        if(gvSales.HeaderRow!=null)
        gvSales.HeaderRow.TableSection = TableRowSection.TableHeader;
        txttotalgrossvalue.Attributes.Add("readonly", "readonly");
        txtNetValue.Attributes.Add("readonly", "readonly");
        txtBalance.Attributes.Add("readonly", "readonly");
    }
    public void PCheckPermission()
    {
        try
        {
            ECPobj = new ECheckPermission();
            ECPobj.RoleId = Convert.ToInt32(Session["RoleId"]);
            ECPobj.ScreenUrl = ScreenUrl;
            DataSet ds = new DataSet();
            object obj = new object();
            ds = ECPobj.Echeckpermissions();
            addPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["ADD"].ToString());
            viewPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["VIEW"].ToString());
            deletepermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["DELETE"].ToString());
            EditPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["EDIT"].ToString());



            if (EditPermission == true)
            {
                imgSave.Visible = EditPermission;
                //imgClear.Visible = EditPermission;

            }
            else
            {
                imgSave.Visible = EditPermission;
                // imgClear.Visible = EditPermission;

            }
            if (addPermission == true)
            {
                imgSave.Visible = addPermission;
                lnkAdd.Visible = addPermission;
            }
            else
            {
                imgSave.Visible = addPermission;
                lnkAdd.Visible = addPermission;
            }


        }
        catch (Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            GlobalDeclarations Gcd = new GlobalDeclarations();
            Gcd.WriteError(ex.Message, ex.StackTrace.ToString(), Session["USERNAME"].ToString(), string.Empty);
            Response.Write("<!--" + ex.ToString() + "-->");
        }
        finally
        { }
    }
    public void FillStore()
    {
        ds = new DataSet();
        Sale = new ESales();
        try
        {
            Sale.LoginID = Convert.ToInt32(Session["LOGINID"]);
            Sale.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            ds = Sale.ddlStore();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    ddlSearchStore.DataSource = ds;
                    ddlSearchStore.DataValueField = "StoreID";
                    ddlSearchStore.DataTextField = "StoreName";
                    ddlSearchStore.DataBind();
                    ddlStore.DataSource = ds;
                    ddlStore.DataValueField = "StoreID";
                    ddlStore.DataTextField = "StoreName";
                    ddlStore.DataBind();
                    ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));
                    ddlSearchStore.Items.Insert(0, new ListItem("--Any--", "0"));
                }
                else if (ds.Tables[0].Rows.Count > 1)
                {
                    ddlSearchStore.DataSource = ds;
                    ddlSearchStore.DataValueField = "StoreID";
                    ddlSearchStore.DataTextField = "StoreName";
                    ddlSearchStore.DataBind();
                    ddlSearchStore.Items.Insert(0, new ListItem("--Any--", "0"));

                    ddlStore.DataSource = ds;
                    ddlStore.DataValueField = "StoreID";
                    ddlStore.DataTextField = "StoreName";
                    ddlStore.DataBind();
                    ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));
                }

            }
            else
            {
                ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlSearchStore.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            if (Convert.ToInt32(Session["LOGINID"]) != 1 && ds.Tables[0].Rows.Count == 1)
            {
                ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);
                ddlSearchStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);
                ddlSearchStore.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    void ShowTabs(int TabNum)
    {
        if (TabNum == 1)
        {
           // dvSalesAdd.Visible = false;
            pnlAdd.Visible = false;
            pnlSearch.Visible = true;
            gvSales.Visible = false;
            //grdRole.Visible = false;
            lnkAdd.Visible = true;
            lnkAdd.Text = "Add";
        }
        else if (TabNum == 2)
        {
            pnlAdd.Visible = true;
            pnlSearch.Visible = false;
            gvSales.Visible = false;
            lnkAdd.Visible = false;
            lnkAdd.Text = "Add";
            btnOrderLense.Visible = false;
        }
    }
    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        ShowTabs(1);
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlSearchStore.SelectedIndex = 0;
        ddlSearchStore.Enabled = true;
        if (Convert.ToString(Session["LOGINID"]) != "1" && ddlStore.Items.Count == 2)
        {
            ddlStore.SelectedIndex = 1;
            ddlSearchStore.SelectedIndex = 1;
            ddlSearchStore.Enabled = false;            
        }
       
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        ShowTabs(2);
        //Image1.Visible = true;
        txtInvoiceDate.Enabled = true;

        gvSaleDetails.Visible = false;
        imgSave.Visible = false;
        //imgClear.Visible = false;
        SDNCalculation.Visible = false;
        EnableControls(true);
        lblStatus.Text = "";
        ddlStore.SelectedValue = "0";
        txtCustomerName.Text = "";
        txtCustomerNo.Text = "";
        btnPayment.Visible = false;
        txtPaidAmount.Visible = true;
        txtBalance.Visible = true;
        //lblBalance.Visible = true;
        //lblPaidAmount.Visible = true;
        btnPrescription.Visible = false;
        txtInvoiceNo.Text = "";
        hdpaidamount.Value = "";
        ddlStore.Enabled = true;
        gvPrescription1.Visible = false;
        gvOrderLense1.Visible = false;
        btnOrderLense.Visible = false;
        if (Convert.ToString(Session["LOGINID"]) != "1" && ddlStore.Items.Count == 2)
        {
            ddlStore.SelectedIndex = 1;
            ddlSearchStore.SelectedIndex = 1;
            ddlStore.Enabled = false;
        }
    }
    void EnableControls(bool status)
    {
        btnSave.Visible = status;
        btn1.Visible = status;
        dvcancel.Visible = status;
        btncancel1.Visible = status;
        ddlStore.Enabled = status;
        txtCustomerName.Enabled = status;
        txtCustomerNo.Enabled = status;
    }
    protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddlcategory = (DropDownList)grow.FindControl("ddlCategory");
            DropDownList ddlbrand = (DropDownList)grow.FindControl("ddlBrand");
            DropDownList ddlProd = (DropDownList)grow.FindControl("ddlProduct");
            ddlProd.Items.Clear();
            int categoryid = Convert.ToInt32(ddlcategory.SelectedValue);
            int brandid = Convert.ToInt32(ddlbrand.SelectedValue);

            if (categoryid != 0 && brandid != 0)
            {
                Sale = new ESales();
                ds = new DataSet();
                Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
                Sale.CategoryID = categoryid;
                Sale.BrandID = brandid;
                ds = Sale.ddlProduct();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlProd.DataSource = ds.Tables[0];
                    ddlProd.DataTextField = "Productname";
                    ddlProd.DataValueField = "ProductID";
                    ddlProd.DataBind();
                    ddlProd.Items.Insert(0, new ListItem("--Any--", "0"));
                }
                else
                {
                    ddlProd.Items.Insert(0, new ListItem("--Any--", "0"));
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void AddNewRow()
    {
        try
        {
            dt = new DataTable();
            dt.Columns.Add("SalesDetailsID");
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("BrandID");
            dt.Columns.Add("ProductID");
            dt.Columns.Add("BrandName");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ProductValue");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("SellingPrice");
            dt.Columns.Add("MaxDiscount");
             dt.Columns.Add("HDSP");
             dt.Columns.Add("Discount");
             dt.Columns.Add("DiscountValue");
            DataRow dr;
            for (int i = 0; i <= gvSaleDetails.Rows.Count - 1; i++)
            {

                dr = dt.NewRow();
                dr["SalesDetailsID"] = ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDSalesDetailID")).Value;
                dr["CategoryID"] = ((DropDownList)gvSaleDetails.Rows[i].FindControl("ddlCategory")).SelectedValue;
                if ((((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductID")).Value) != "")
                    dr["ProductID"] = ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductID")).Value;
                else
                    dr["ProductID"] = "0";
                dr["BrandID"] = ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDBrandID")).Value;
                dr["BrandName"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtBrand")).Text;
                dr["ProductName"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtProduct")).Text;
                dr["ProductValue"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtProductValue")).Text;
                dr["Quantity"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtQuantity")).Text;
                dr["SellingPrice"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtSellingPrice")).Text;
                dr["HDSP"] = ((HiddenField)gvSaleDetails.Rows[i].FindControl("hiddensellingprice")).Value;
                dr["MaxDiscount"] = ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductDiscount")).Value;
                dr["Discount"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtDiscount")).Text;
                dr["DiscountValue"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtDiscountvalue")).Text;
                dt.Rows.Add(dr);
            }
            dr = dt.NewRow();
            dr["SalesDetailsID"] = "0";
            dr["CategoryID"] = "0";
            dr["BrandID"] = "0";
            dr["ProductID"] = "0";
            dr["BrandName"] = "";
            dr["ProductName"] = "";
            dr["Quantity"] = "0";
            dr["ProductValue"] = "0.00";
            dr["SellingPrice"] = "0.00";
            dr["MaxDiscount"] = 0;
            dr["HDSP"] = "0";
            dr["Discount"] = "0";
           dr["DiscountValue"]="0";
            dt.Rows.Add(dr);
            Sale = new ESales();
            Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            ds = Sale.ddlCategory();
            dtCategory = ds.Tables[0];
            ds = Sale.ddlBrand();
            dtBrand = ds.Tables[0];
            l = 0;
            totalsellingprice = 0;
            gvSaleDetails.DataSource = dt;
            gvSaleDetails.DataBind();
            txtTotalSellingPrice.Text = totalsellingprice.ToString();
            ((TextBox)(gvSaleDetails.Rows[gvSaleDetails.Rows.Count - 2].FindControl("txtQuantity"))).Focus();
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddlprod = (DropDownList)gr.FindControl("ddlProduct");
            TextBox prodvalue = (TextBox)gr.FindControl("txtProductValue");
            HiddenField hddiscount = (HiddenField)gr.FindControl("HDProductDiscount");
            //TextBox defaultdiscount = (TextBox)gr.FindControl("txtDefaultBuyingDiscount");
            Sale = new ESales();
            Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            if (ddlprod.SelectedValue != "0")
            {
                Sale.ProductID = Convert.ToInt32(ddlprod.SelectedValue);
                ds = new DataSet();
                ds = Sale.GetProductValue();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    prodvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                    hddiscount.Value = Convert.ToString(ds.Tables[0].Rows[0]["MaxDiscount"]);
                   
                }
                if (ds.Tables[1].Rows.Count > 0)
                    Session["buyingprice"] = ds.Tables[1];
                else
                    Session["buyingprice"] = null;
            }
            int rowcount = gvSaleDetails.Rows.Count;
            ddlprod = sender as DropDownList;
            string ID = ddlprod.ClientID;
            ID = ID.Replace("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl", "");
            ID = ID.Replace("_ddlProduct", "");
            int maxrow = Convert.ToInt32(ID) - 1;
            if (rowcount == maxrow)
            {
                AddNewRow();
                if (deletepermission == false)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LinkButton im = (LinkButton)gvSaleDetails.Rows[i].FindControl("imgDeleteRow");
                        im.Visible = false;
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LinkButton im = (LinkButton)gvSaleDetails.Rows[i].FindControl("imgDeleteRow");
                        im.Visible = true;
                    }
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void imgSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        string chkqnty;
        int avlqnty;
        try
        {
            ds = new DataSet();
            Sale = new ESales();
            string GridData = string.Empty;
            int salesdetailsid = 0;
           
            for (int i = 0; i < gvSaleDetails.Rows.Count; i++)
            {
                salesdetailsid = Convert.ToInt32(((HiddenField)gvSaleDetails.Rows[i].FindControl("HDSalesDetailID")).Value);
                if (salesdetailsid == 0)
                {
                    if (((DropDownList)gvSaleDetails.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" && ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDBrandID")).Value != "" && ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductID")).Value != "")
                    {

                        GridData += ((DropDownList)gvSaleDetails.Rows[i].FindControl("ddlCategory")).SelectedValue + "~"
                      + ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDBrandID")).Value + "~"
                     + ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductID")).Value + "~"
                     + ((TextBox)gvSaleDetails.Rows[i].FindControl("txtProductValue")).Text + "~"
                     + ((TextBox)gvSaleDetails.Rows[i].FindControl("txtQuantity")).Text + "~"
                      + ((TextBox)gvSaleDetails.Rows[i].FindControl("txtDiscount")).Text + "~"
                     + ((TextBox)gvSaleDetails.Rows[i].FindControl("txtSellingPrice")).Text + "$";
                       
                    }
                
                }
            }
            if (GridData != "")
            {
                GridData = GridData.Substring(0, GridData.Length - 1);
            }
                Sale.GridData = GridData;
                if (txttotalgrossvalue.Text != "")
                    Sale.GrossTotal = Convert.ToSingle(txttotalgrossvalue.Text);
                else
                    Sale.GrossTotal = 0;
                if (txtDiscount.Text != "")
                    Sale.Discount = Convert.ToSingle(txtDiscount.Text);
                else
                    Sale.Discount = 0;
                if (txtNetValue.Text != "")
                {
                    Sale.NetTotal = Convert.ToSingle(txtNetValue.Text);
                }
                else
                    Sale.NetTotal = 0;
                if (txtBalance.Text != "")
                    Sale.Balance = Convert.ToSingle(txtBalance.Text);
                else
                    Sale.Balance = Convert.ToSingle(txtNetValue.Text);
                //if (txtadvancepaidamount.Text != "")
                //    if (Convert.ToSingle(txtadvancepaidamount.Text) == 0)
                //        Sale.PaidAmount = 0;
                //    else
                //        Sale.PaidAmount = Convert.ToSingle(txtadvancepaidamount.Text);
                //else
                //    Sale.PaidAmount = 0;

                if (txtPaidAmount.Text == "" || txtPaidAmount.Text == null)
                {
                    txtPaidAmount.Text = "0.00";
                }
                if (txtadvancepaidamount.Text != "")
                    if (Convert.ToSingle(txtadvancepaidamount.Text) == 0)
                        Sale.PaidAmount = 0;
                    else
                        Sale.PaidAmount = Convert.ToSingle(txtadvancepaidamount.Text);
                else
                    Sale.PaidAmount = 0;

                if (rbtnCard.Checked)
                    Sale.PaymentMode = "CARD";
                else
                    Sale.PaymentMode = "CASH";

                if (ddlSalesMan.SelectedValue != "0")
                {
                    Sale.SalesManID = Convert.ToInt32(ddlSalesMan.SelectedValue);
                }
                else
                {
                    Sale.SalesManID = 0;
                }
                if (txtCustomerName.Text != "")
                    Sale.CustomerName = txtCustomerName.Text;
                if (txtCustomerNo.Text != "")
                    Sale.CustomerNo = txtCustomerNo.Text;
                else
                    Sale.CustomerNo = "";
                Sale.SalesID = Convert.ToInt32(HDSaleID.Value);
                Sale.LoginID = Convert.ToInt32(Session["LOGINID"]);

                Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
                //char[] separatingChars = { '$', '~' };
                // string[] words = GridData.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

                //ds = new DataSet();
                //ds = Sale.ECheckDuplicatesVIEWEDIT(words[0], words[1], words[2]);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    dvFailure.Visible = true;
                //    lblStatus.Text = "Record Already Exists";
                //}
                //else
                //{
                string result = Sale.InsertSalesDetails();
                if (result == "Success")
                {
                    //divprintarea.Attributes["style"] = "display:block; overflow:auto;height:475px;";
                    // ShowTabs(1);
                    if (Sale.PaidAmount != 0 || GridData != "")
                    {
                        EditFunction(Convert.ToInt32(HDSaleID.Value));
                        Sale = new ESales();
                        ds = new DataSet();
                        int salesid = Convert.ToInt32(HDSaleID.Value);
                        Sale.SalesID = Convert.ToInt32(HDSaleID.Value);
                        ds = Sale.GetPrint();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblStoreNamePopup.Text = Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]);
                            lblAddressPopup.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                            lblCustomerNamePopup.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                            lblCustomerNoPopup.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerNo"]);
                            lblDiscount.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                            lblTotalGrossValue.Text = Convert.ToString(ds.Tables[0].Rows[0]["GrossTotal"]);
                            lblNetValue.Text = Convert.ToString(ds.Tables[0].Rows[0]["NetTotal"]);
                            //lblLoginUser.Text = Convert.ToString(ds.Tables[0].Rows[0]["LoginName"]);
                            //divprintarea.DataSource = ds.Tables[1];
                            //divprintarea.DataBind();
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "SalesForPrint(" + salesid + ");", true);
                        }
                        else
                        {

                        }
                        dvSuccess.Visible = true;
                        lblSuccess.Text = result;
                    }
                    if (Sale.PaidAmount == 0 && GridData == "")
                    {
                        //lblStatus.Text = "Neither Payment nor any Sale are Made.";
                    }


                }
                else if (result == "Payment is successfull.")
                {
                    EditFunction(Convert.ToInt32(HDSaleID.Value));
                    Sale = new ESales();
                    ds = new DataSet();
                    int salesid = Convert.ToInt32(HDSaleID.Value);
                    Sale.SalesID = Convert.ToInt32(HDSaleID.Value);
                    ds = Sale.GetPrint();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblStoreNamePopup.Text = Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]);
                        lblAddressPopup.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                        lblCustomerNamePopup.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                        lblCustomerNoPopup.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerNo"]);
                        lblDiscount.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                        lblTotalGrossValue.Text = Convert.ToString(ds.Tables[0].Rows[0]["GrossTotal"]);
                        lblNetValue.Text = Convert.ToString(ds.Tables[0].Rows[0]["NetTotal"]);
                        //lblLoginUser.Text = Convert.ToString(ds.Tables[0].Rows[0]["LoginName"]);
                        //divprintarea.DataSource = ds.Tables[1];
                        //divprintarea.DataBind();
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "SalesPrint(" + salesid + ");", true);
                    }
                    dvSuccess.Visible = true;
                    lblSuccess.Text = result;
                }

                else
                {
                    dvFailure.Visible = true;
                    lblStatus.Text = result;
                }
                btnOrderLense.Visible = true;
                // }
          
        
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "AlertMessage", "<script language='javascript'  type='text/javascript'>;alert('Please Fill Required Fields');</script>", false);
            //    return;
            //}
            
            //ShowTabs(2);
        }
        catch (Exception)
        {

            throw;
        }
    }
    [WebMethod]
    public static GridDataSet SalesForPrint(int salesid)
    {

        ESales Sale = new ESales();
        ESaleList listSale = new ESaleList();
        ESales salepriscription = new ESales();
        DataSet ds = new DataSet();
        List<ESales> esales = new List<ESales>();
        List<ESales> lstsalepriscription = new List<ESales>();
        List<ESaleList> esaleslist = new List<ESaleList>();
        GridDataSet objGridDataset = new GridDataSet();
        Sale.SalesID = salesid;
        float nttotal;
        float pamount;
        ds = Sale.GetPrint();
        if (ds.Tables.Count>0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                //Sale = new ESales();
                Sale.StoreName = Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]);
                Sale.Address = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                Sale.CustomerName = Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                Sale.CustomerNo = Convert.ToString(ds.Tables[0].Rows[0]["CustomerNo"]);
                Sale.GrossTotal = Convert.ToSingle(ds.Tables[0].Rows[0]["GrossTotal"]);
                Sale.NetTotal = Convert.ToSingle(ds.Tables[0].Rows[0]["NetTotal"]);
                Sale.Discount = Convert.ToSingle(ds.Tables[0].Rows[0]["Discount"]);
                // Sale.LoginName = Convert.ToString(ds.Tables[0].Rows[0]["LoginName"]);
                Sale.LoginName = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                Sale.InvoiceDate = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceDate"]);
                Sale.InvoiceNo = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]);

               
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                float p1 = 0;
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    p1 +=Convert.ToSingle(ds.Tables[2].Rows[i]["PaidAmount"]);
                }
                Sale.PaidAmount = p1;
                Sale.PaymentMode = Convert.ToString(ds.Tables[2].Rows[0]["paymentMode"]);
            }
            esales.Add(Sale);
            if (ds.Tables[3].Rows.Count > 0)
            {
                salepriscription.SPH_RightEye = (ds.Tables[3].Rows[0]["SPH_RightEye"]).ToString();
                salepriscription.CYL_RightEye = (ds.Tables[3].Rows[0]["CYL_RightEye"]).ToString();
                salepriscription.AXIS_RightEye = (ds.Tables[3].Rows[0]["AXIS_RightEye"]).ToString();
                salepriscription.ADD_RightEye = (ds.Tables[3].Rows[0]["ADD_RightEye"]).ToString();
                salepriscription.SPH_LeftEye = (ds.Tables[3].Rows[0]["SPH_LeftEye"]).ToString();
                salepriscription.CYL_LeftEye = (ds.Tables[3].Rows[0]["CYL_LeftEye"]).ToString();
                salepriscription.AXIS_LeftEye = (ds.Tables[3].Rows[0]["AXIS_LeftEye"]).ToString();
                salepriscription.ADD_LeftEye = ds.Tables[3].Rows[0]["ADD_LeftEye"].ToString();
                salepriscription.SPH_IPD = ds.Tables[3].Rows[0]["SPH_IPD"].ToString();
                salepriscription.CYL_IPD = ds.Tables[3].Rows[0]["CYL_IPD"].ToString();
                salepriscription.AXIS_IPD = ds.Tables[3].Rows[0]["AXIS_IPD"].ToString();
                salepriscription.ADD_IPD = ds.Tables[3].Rows[0]["ADD_IPD"].ToString();
                lstsalepriscription.Add(salepriscription);
            }

           
            nttotal = Sale.NetTotal;
            pamount = Convert.ToSingle(Sale.PaidAmount);
            Sale.Balance=(float)((nttotal) - (pamount));

         
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    listSale = new ESaleList();
                    listSale.CategoryName = Convert.ToString(ds.Tables[1].Rows[i]["CategoryName"]);
                    listSale.BrandName = Convert.ToString(ds.Tables[1].Rows[i]["BrandName"]);
                    listSale.ProductName = Convert.ToString(ds.Tables[1].Rows[i]["ProductName"]);
                    listSale.Quantity = Convert.ToInt32(ds.Tables[1].Rows[i]["Quantity"]);
                    listSale.ProductValue = Convert.ToSingle(ds.Tables[1].Rows[i]["ProductValue"]);
                    listSale.SellingPrice = Convert.ToSingle(ds.Tables[1].Rows[i]["SellingPrice"]);
                    listSale.TotalValue = Convert.ToSingle(ds.Tables[1].Rows[i]["TotalValue"]);
                    listSale.Discount = Convert.ToSingle(ds.Tables[1].Rows[i]["Discount"]);
                    esaleslist.Add(listSale); 

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
                    //esaleslist[0].TotalGrossValue = Convert.ToSingle(esaleslist[0].TotalGrossValue) + totalordensevalue;
                    //esaleslist[0].NetValue = Convert.ToSingle(esaleslist[0].NetValue) + totalordensevalue;
                    Sale.NetTotal = Convert.ToSingle(Sale.NetTotal) + 0;
                    Sale.GrossTotal = Convert.ToSingle(Sale.GrossTotal) + 0;
                    Sale.Balance = Convert.ToSingle(Sale.Balance) + 0;
                }
            } 
        }
        objGridDataset.eSales = esales;
        objGridDataset.eSaleslist = esaleslist;
        objGridDataset.eSalePriscription=lstsalepriscription;
        return objGridDataset;
    }
    [WebMethod]
    public static GridDataSet SalesPrint(int salesid)
    {
        DataSet dsfotStore = new DataSet();
        DataSet dsSales = new DataSet();
        ESales Sale = new ESales();
        ESaleList listSale = new ESaleList();
        ESales salepriscription = new ESales();
        DataSet ds = new DataSet();
        List<ESales> esales = new List<ESales>();
        List<ESaleList> esaleslist = new List<ESaleList>();
        List<ESales> lstsalepriscription = new List<ESales>();
        GridDataSet objGridDataset = new GridDataSet();
        Sale.SalesID = salesid;
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
                if (ds.Tables[2].Rows.Count>0)
                {
                    
                    if (ds.Tables[2].Rows[0]["PaidAmount"].ToString() == "")
                    {
                        Sale.PaidAmount = 0;
                    }
                    else
                    {
                        Sale.PaidAmount = Convert.ToSingle(ds.Tables[2].Rows[0]["PaidAmount"]);
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                        Sale.PaymentMode = Convert.ToString(ds.Tables[3].Rows[0]["PaymentMode"]);
                    else
                        Sale.PaymentMode = "";
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
                    esaleslist[0].Balance=Convert.ToSingle(esaleslist[0].Balance)+0;
                }
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
    protected void imgClear_Click(object sender, ImageClickEventArgs e)
    {

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
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblStatus.Text = string.Empty;
            lblSuccess.Text = string.Empty;
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            FillGrid();
            //Clear();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void Clear()
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlSearchStore.SelectedValue = "0";
        txtSearchCustomerName.Text = "";
        txtSearchCustomerNo.Text = "";
        txtsearchinvoiceNo.Text = "";
        txtSerialNo.Text = "";
    }
    protected void FillGrid()
    {
        try
        {
            Sale = new ESales();
            ds = new DataSet();
            if (ddlSearchStore.SelectedValue != "0")
                Sale.StoreID = Convert.ToInt32(ddlSearchStore.SelectedValue);
            else
                Sale.StoreID = 0;
            if (txtSearchCustomerName.Text != "")
                Sale.CustomerName = txtSearchCustomerName.Text;
            else
                Sale.CustomerName = "";
            if (txtSearchCustomerNo.Text != "")
                Sale.CustomerNo = txtSearchCustomerNo.Text;
            else
                Sale.CustomerNo = "";
            if (txtFromDate.Text != "")
                Sale.FromDate =converttodate(txtFromDate.Text);
            else
                Sale.FromDate = "";
            if (txtToDate.Text != "")
                Sale.ToDate =converttodate(txtToDate.Text);
            else
                Sale.ToDate = "";
            if (txtsearchinvoiceNo.Text != "")
                Sale.InvoiceNo = txtsearchinvoiceNo.Text;
            else
                Sale.InvoiceNo = "";

            Sale.LoginID = Convert.ToInt32(Session["LOGINID"]);
            Sale.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            if (txtSerialNo.Text != "")
                Sale.SerialNo = txtSerialNo.Text;
            else
                Sale.SerialNo = "";
            ds = Sale.GetSalesGrid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count == 1 )
                {
                    if (txtsearchinvoiceNo.Text != ""||txtSerialNo.Text!="")
                    {
                        EditFunction(Convert.ToInt32(ds.Tables[0].Rows[0]["SaleID"]));
                    }
                    else if (txtSerialNo.Text == "" && txtsearchinvoiceNo.Text == "")
                    {
                        gvSales.DataSource = ds.Tables[0];
                        gvSales.DataBind();
                        gvSales.HeaderRow.TableSection = TableRowSection.TableHeader;
                        lblStatus.Text = "";
                        gvSales.Visible = true;

                    }
                    txtsearchinvoiceNo.Text = "";
                }
                else
                {
                    gvSales.DataSource = ds.Tables[0];
                    gvSales.DataBind();
                    gvSales.HeaderRow.TableSection = TableRowSection.TableHeader;
                    lblStatus.Text = "";
                    gvSales.Visible = true;
                }

            }
            else
            {
                gvSales.DataSource = null;
                gvSales.DataBind();
                dvFailure.Visible = true;
                lblStatus.Text = "No Record Found.";
                gvSales.Visible = false;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = false;
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {

            Sale = new ESales();
            ds = new DataSet();
            if (ddlStore.SelectedValue != "0")
                Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            else
                Sale.StoreID = 0;
            if (txtCustomerName.Text != "")
                Sale.CustomerName = txtCustomerName.Text;
            else
                Sale.CustomerName = "";
            if (txtCustomerNo.Text != "")
                Sale.CustomerNo = txtCustomerNo.Text;
            else
                Sale.CustomerNo = "";
            int LoginID = Convert.ToInt32(Session["LOGINID"]);
            Sale.LoginID = LoginID;
            if (txtInvoiceNo.Text != "")
                Sale.InvoiceNo = txtInvoiceNo.Text;
            else
                Sale.InvoiceNo = "";
            if (txtInvoiceDate.Text != "")
                Sale.InvoiceDate = converttodate(txtInvoiceDate.Text);
            else
                Sale.InvoiceDate = "";
            ds = Sale.InsertSales();
            if (ds.Tables[0].Rows.Count > 0)
            {
                int saleid = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                HDSaleID.Value = saleid.ToString();
                txtInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]);
                txtCustomerNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerNo"]);
                gvSaleDetails.Visible = true;
                SDNCalculation.Visible = true;
                btnSave.Visible = false;
                EnableControls(false);
                txtCustomerNo.Enabled = true;
                txttotalgrossvalue.Text = "";
                txtDiscount.Text = "0.00";
                txtNetValue.Text = "";
                txtadvancepaidamount.Text = "0.00";
                txtPaidAmount.Text = "0.00";
                txtBalance.Text = "0.0";
                hdorderlensevalue.Value = "0";
                AddEmptyRows();
                
                // FillUser();
                FillSalesMan();
                ddlSalesMan.SelectedValue = "0";
                ddlUser.Enabled = false;
                btnPrescription.Visible = true;
                FillUser();
                ddlUser.SelectedValue = LoginID.ToString();
                btnOrderLense.Visible = true;
            }
            else
            {
                dvFailure.Visible = true;
                lblStatus.Text = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                gvSaleDetails.Visible = false;
                SDNCalculation.Visible = false;
                btnPrescription.Visible = true;
                return;
            }
            if (deletepermission == false)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    LinkButton im = (LinkButton)gvSaleDetails.Rows[i].FindControl("imgDeleteRow");
                    im.Visible = false;
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    LinkButton im = (LinkButton)gvSaleDetails.Rows[i].FindControl("imgDeleteRow");
                    im.Visible = true;
                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void FillUser()
    {
        try
        {
            Sale = new ESales();
            ds = new DataSet();
            Sale.LoginID = Convert.ToInt32(Session["LOGINID"]);
            Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            ds = Sale.ddlUser();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlUser.DataSource = ds.Tables[0];
                ddlUser.DataTextField = "UserName";
                ddlUser.DataValueField = "UserID";
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
    protected void FillSalesMan()
    {
        try
        {
            Sale = new ESales();
            ds = new DataSet();
            Sale.LoginID = Convert.ToInt32(Session["LOGINID"]);
            Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            ds = Sale.ddlSalesMan();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSalesMan.Items.Clear();
                ddlSalesMan.DataSource = ds.Tables[0];
                ddlSalesMan.DataTextField = "EmployeeName";
                ddlSalesMan.DataValueField = "EmployeeID";
                ddlSalesMan.DataBind();
                ddlSalesMan.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                ddlSalesMan.Items.Clear();
                ddlSalesMan.Items.Insert(0, new ListItem("--Any--", "0"));
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void AddEmptyRows()
    {
        try
        {
            dt = new DataTable();
            if (dt.Rows.Count == 0 || dt.Rows.Count < 5)
            {
                dt = new DataTable();
                dt.Columns.Add("SalesDetailsID");
                dt.Columns.Add("CategoryID");
                dt.Columns.Add("BrandID");
                dt.Columns.Add("ProductID");
                dt.Columns.Add("BrandName");
                dt.Columns.Add("ProductName");
                dt.Columns.Add("ProductValue");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("SellingPrice");
                dt.Columns.Add("MaxDiscount");
                 dt.Columns.Add("HDSP");
                 dt.Columns.Add("Discount");
                 dt.Columns.Add("DiscountValue");
            }
            DataRow dr;
            for (int i = dt.Rows.Count; i < 5; i++)
            {
                dr = dt.NewRow();
                dr["SalesDetailsID"] = "0";
                dr["CategoryID"] = "0";
                dr["BrandID"] = "0";
                dr["ProductID"] = "0";
                dr["BrandName"] = "";
                dr["ProductName"] = "";
                dr["Quantity"] = "0";
                dr["ProductValue"] = "0.00";
                dr["SellingPrice"] = "0.00";
                dr["MaxDiscount"] = 0;
                dr["HDSP"] = "0";
                dr["Discount"] = "0";
                dr["DiscountValue"] = "0";
                dt.Rows.Add(dr);
            }
            ShowTabs(2);
            SDNCalculation.Visible = true;
            imgSave.Visible = true;
           // btnPayment.Visible=true
            txttotalgrossvalue.Text = "0.00";
            Sale = new ESales();
            Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            ds = Sale.ddlCategory();
            dtCategory = ds.Tables[0];
            ds = Sale.ddlBrand();
            dtBrand = ds.Tables[0];
            l = 0;
            totalsellingprice = 0;
            gvSaleDetails.DataSource = dt;
            gvSaleDetails.DataBind();
            txtTotalSellingPrice.Text = totalsellingprice.ToString();
            gvSaleDetails.Visible = true;
            if (deletepermission == false)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        TextBox tt = (TextBox)gvSaleDetails.Rows[i].FindControl("txtProduct");
                        tt.Focus();
                    }
                    LinkButton im = (LinkButton)gvSaleDetails.Rows[i].FindControl("imgDeleteRow");
                    im.Visible = false;
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        TextBox tt = (TextBox)gvSaleDetails.Rows[i].FindControl("txtProduct");
                        tt.Focus();
                    }
                    LinkButton im = (LinkButton)gvSaleDetails.Rows[i].FindControl("imgDeleteRow");
                    im.Visible = true;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void gvSaleDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            TextBox tt, txtprod=null, txtbrand=null;
            string hdnstr;
            HiddenField hd,hd1; int qty = 0;
            DropDownList ddlproducts, ddlcard, ddlbrand;
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddlcard = (DropDownList)e.Row.FindControl("ddlCategory");
                ddlbrand = (DropDownList)e.Row.FindControl("ddlBrand");
                ddlproducts = (DropDownList)e.Row.FindControl("ddlProduct");
                ddlcard.DataSource = dtCategory;
                ddlcard.DataValueField = "CategoryID";
                ddlcard.DataTextField = "CategoryName";
                ddlcard.DataBind();
                ddlcard.Items.Insert(0, new ListItem("--Any--", "0"));

                ddlbrand.DataSource = dtBrand;
                ddlbrand.DataValueField = "BrandID";
                ddlbrand.DataTextField = "BrandName";
                ddlbrand.DataBind();
                ddlbrand.Items.Insert(0, new ListItem("--Any--", "0"));
                HiddenField hdsp = (HiddenField)e.Row.FindControl("hiddensellingprice");
                hdsp.Value=Convert.ToString(dt.Rows[l]["HDSP"]);
                if (Convert.ToInt32(dt.Rows[l]["ProductID"]) != 0 && Convert.ToString(dt.Rows[l]["ProductID"]) != "")
                {
                    ddlcard.SelectedValue = Convert.ToString(dt.Rows[l]["CategoryID"]);
                    int categoryid = Convert.ToInt32(ddlcard.SelectedValue);
                    ddlbrand.SelectedValue = Convert.ToString(dt.Rows[l]["BrandID"]);
                    int brandid = Convert.ToInt32(ddlbrand.SelectedValue);
                    fillproduct(categoryid, brandid, ddlproducts);
                    ddlproducts.SelectedValue = Convert.ToString(dt.Rows[l]["ProductID"]);
                    hdnstr = Convert.ToString(dt.Rows[l]["MaxDiscount"]).ToString();
                    if (hdnstr != "")
                    {
                       
                       string str = Convert.ToString(dt.Rows[l]["MaxDiscount"]);
                       ((HiddenField)e.Row.FindControl("HDProductDiscount")).Value = str;
                    }

                    hd = (HiddenField)e.Row.FindControl("HDBrandID");
                    hd.Value = Convert.ToString(dt.Rows[l]["BrandID"]);
                    hd1 = (HiddenField)e.Row.FindControl("HDProductID");
                    hd1.Value = Convert.ToString(dt.Rows[l]["ProductID"]);
                    txtprod = (TextBox)e.Row.FindControl("txtProduct");
                    txtprod.Text = Convert.ToString(dt.Rows[l]["ProductName"]);
                    txtbrand = (TextBox)e.Row.FindControl("txtBrand");
                    txtbrand.Text = Convert.ToString(dt.Rows[l]["BrandName"]);
                }
                if (Convert.ToString(dt.Rows[l]["ProductName"]) != "")
                {
                    ddlcard.SelectedValue = Convert.ToString(dt.Rows[l]["CategoryID"]);
                    txtprod = (TextBox)e.Row.FindControl("txtProduct");
                    txtprod.Text = Convert.ToString(dt.Rows[l]["ProductName"]);
                    txtbrand = (TextBox)e.Row.FindControl("txtBrand");
                    txtbrand.Text = Convert.ToString(dt.Rows[l]["BrandName"]);
                    if(Convert.ToString(dt.Rows[l]["DiscountValue"])!="")
                    {
                        TextBox t = (TextBox)e.Row.FindControl("txtDiscountvalue");
                        t.Text = Convert.ToString(dt.Rows[l]["DiscountValue"]);
                    }
                    if (Convert.ToString(dt.Rows[l]["MaxDiscount"]) != "")
                    {
                        hdnstr = Convert.ToString(dt.Rows[l]["MaxDiscount"]).ToString();
                        if (hdnstr != "")
                        {

                            string str = Convert.ToString(dt.Rows[l]["MaxDiscount"]);
                            ((HiddenField)e.Row.FindControl("HDProductDiscount")).Value = str;
                        }
                    }
                }
                if (Convert.ToInt32(dt.Rows[l]["SalesDetailsID"]) != 0)
                {
                    ddlcard.Enabled = false;
                    ddlbrand.Enabled = false;
                    ddlproducts.Enabled = false;
                    txtbrand.Enabled = false;
                    txtprod.Enabled = false;
                    float discount=0;
                    if(Convert.ToString(dt.Rows[l]["Discount"])!="")
                     discount = Convert.ToSingle(dt.Rows[l]["Discount"]);
                    int quantity=Convert.ToInt32(dt.Rows[l]["Quantity"]);
                    float productvalue=Convert.ToSingle(dt.Rows[l]["ProductValue"]);
                    TextBox td=(TextBox)e.Row.FindControl("txtDiscountvalue");
                    td.Text=Convert.ToString((quantity*productvalue*discount)/100);
                    //(((TextBox)e.Row.FindControl("txtDiscountvalue")).Text=;
                    ((TextBox)e.Row.FindControl("txtProductValue")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtQuantity")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtSellingPrice")).Enabled = false;
                    //((TextBox)e.Row.FindControl("txtSellingPrice")).Enabled = false;
                    qty = Convert.ToInt32(dt.Rows[l]["Quantity"]);
                    totalsellingprice += (Convert.ToSingle(dt.Rows[l]["SellingPrice"]));
                }
                else
                {
                    ((TextBox)e.Row.FindControl("txtProductValue")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtQuantity")).Enabled = true;
                    //((TextBox)e.Row.FindControl("txtSellingPrice")).Enabled = true;
                    if (Convert.ToString(dt.Rows[l]["MaxDiscount"]) != "")
                    {
                        hdnstr = Convert.ToString(dt.Rows[l]["MaxDiscount"]).ToString();
                        if (hdnstr != "")
                        {

                            string str = Convert.ToString(dt.Rows[l]["MaxDiscount"]);
                            ((HiddenField)e.Row.FindControl("HDProductDiscount")).Value = str;
                        }
                    }
                }
                l++;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void fillproduct(int categoryid, int brandid, DropDownList ddlprod)
    {
        try
        {
           
            ds = new DataSet();
            Sale = new ESales();
            Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            Sale.CategoryID = categoryid;
            Sale.BrandID = brandid;
            ds = Sale.ddlProduct();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlprod.DataSource = ds.Tables[0];
                ddlprod.DataValueField = "ProductID";
                ddlprod.DataTextField = "ProductName";
                ddlprod.DataBind();
                ddlprod.Items.Insert(0, new ListItem("--Any--", "0"));
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void gvSaleDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        if (e.CommandName == "DeleteRow")
        {
            string result = string.Empty;
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int rowcount = gvSaleDetails.Rows.Count;
            if (rowcount != 1)
            {
                int salesdetailid = Convert.ToInt32(((HiddenField)gvSaleDetails.Rows[rowindex].FindControl("HDSalesDetailID")).Value);
                Sale = new ESales();
                ds = new DataSet();
                if (salesdetailid != 0)
                {
                    Sale.SalesDetailID = salesdetailid;
                    Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
                    Sale.LoginID = Convert.ToInt32(Session["LoginID"]);
                    ds = Sale.DeleteSalesDetails();
                    result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                    int supplierdeliverynoteid = Convert.ToInt32(HDSaleID.Value);
                    EditFunction(supplierdeliverynoteid);
                    dvSuccess.Visible = true;
                    lblSuccess.Text = "Record Deleted Successfully";
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add("SalesDetailsID");
                    dt.Columns.Add("CategoryID");
                    dt.Columns.Add("BrandID");
                    dt.Columns.Add("ProductID");
                    dt.Columns.Add("BrandName");
                    dt.Columns.Add("ProductName");
                    dt.Columns.Add("ProductValue");
                    dt.Columns.Add("Quantity");
                    dt.Columns.Add("SellingPrice");
                    dt.Columns.Add("Discount");
                    dt.Columns.Add("DiscountValue");
                    dt.Columns.Add("MaxDiscount");
                    dt.Columns.Add("HDSP");
                    DataRow dr;
                    for (int i = 0; i <= gvSaleDetails.Rows.Count - 2; i++)
                    {

                        dr = dt.NewRow();
                        dr["SalesDetailsID"] = ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDSalesDetailID")).Value;
                        dr["CategoryID"] = ((DropDownList)gvSaleDetails.Rows[i].FindControl("ddlCategory")).SelectedValue;
                        if ((((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductID")).Value) != "")
                            dr["ProductID"] = ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductID")).Value;
                        else
                            dr["ProductID"] = "0";
                        dr["BrandID"] = ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDBrandID")).Value;
                        dr["BrandName"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtBrand")).Text;
                        dr["ProductName"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtProduct")).Text;
                        dr["ProductValue"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtProductValue")).Text;
                        dr["Quantity"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtQuantity")).Text;
                        dr["Discount"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtDiscount")).Text;
                        dr["DiscountValue"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtDiscountvalue")).Text;
                        dr["SellingPrice"] = ((TextBox)gvSaleDetails.Rows[i].FindControl("txtSellingPrice")).Text;
                        dr["HDSP"] = ((HiddenField)gvSaleDetails.Rows[i].FindControl("hiddensellingprice")).Value;
                        dr["MaxDiscount"] = ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductDiscount")).Value;

                        dt.Rows.Add(dr);
                    }
                    Sale = new ESales();
                    ds = Sale.ddlCategory();
                    dtCategory = ds.Tables[0];
                    ds = Sale.ddlBrand();
                    dtBrand = ds.Tables[0];
                    l = 0;

                    totalsellingprice = 0;
                    gvSaleDetails.DataSource = dt;
                    gvSaleDetails.DataBind();
                    txtTotalSellingPrice.Text = totalsellingprice.ToString();
                    dvSuccess.Visible = true;
                    lblSuccess.Text = "Record Deleted Successfully";
                }
            }
            else
            {
                int salesdetailid = Convert.ToInt32(((HiddenField)gvSaleDetails.Rows[rowindex].FindControl("HDSalesDetailID")).Value);
                Sale = new ESales();
                Sale.SalesDetailID = salesdetailid;
                if (salesdetailid != 0)
                    ds = Sale.DeleteSalesDetails();
                AddEmptyRows();
            }

           
            //ShowTabs(1);
            btnOrderLense.Visible = true;   
        }
    }
    protected void gvSaleDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvSales_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int salesid = Convert.ToInt32(gvSales.DataKeys[rowindex].Value);
            Sale = new ESales();
            ds = new DataSet();
            txtadvancepaidamount.Text = "0.00";
            //Image1.Visible = false;
            txtInvoiceDate.Enabled = false;
            hdorderlensevalue.Value = "";
            txttotalgrossvalue.Text = "";
            if (e.CommandName == "View")
            {
                
                Sale.SalesID = salesid;
                string userid = "", salesman = "";
                ds = Sale.GetSalesDetailsGrid();
                btnOrderLense.Visible = false;
                HDSaleID.Value = salesid.ToString();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);
                        txtCustomerName.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                        txtCustomerNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerNo"]);
                        txttotalgrossvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["GrossTotal"]);

                        //if (Convert.ToInt32(ds.Tables[0].Rows[0]["GrossTotal"])==0)
                        //{
                        //    lbladvancepaidamount.Text = "Advance";
                        //}
                        //else
                        //{
                        //    lbladvancepaidamount.Text = "Pay Now";
                        //}

                        txtDiscount.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                        txtNetValue.Text = Convert.ToString(ds.Tables[0].Rows[0]["NetTotal"]);
                       
                        txtInvoiceDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceDate"]);
                        txtInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]);
                        userid = Convert.ToString(ds.Tables[0].Rows[0]["CreatedBy"]);
                        salesman = Convert.ToString(ds.Tables[0].Rows[0]["UserID"]);
                        btnSave.Visible = false;
                        SDNCalculation.Visible = true;
                        EnableControls(false);
                        imgSave.Visible = false;                       
                        Div4.Visible=false;
                        btncancelgrid.Visible = true;
                        txtBalance.Text = Convert.ToString(ds.Tables[0].Rows[0]["Balance"]);
                        txtadvancepaidamount.Text = "0.00";
                        hdBalance.Value = txtBalance.Text;
                        float paidamount = 0;
                        if (ds.Tables[2].Rows.Count > 0)
                        { 
                            for(int i=0;i<ds.Tables[2].Rows.Count;i++)
                            {
                                if(Convert.ToString(ds.Tables[2].Rows[i]["PaidAmount"])!="")
                                paidamount += Convert.ToSingle(ds.Tables[2].Rows[i]["PaidAmount"]);
                            }
                        }
                        txtPaidAmount.Text = paidamount.ToString();
                        hdpaidamount.Value = txtPaidAmount.Text;
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        dt = ds.Tables[1];
                        dt.Columns.Add("DiscountValue");
                        ds = Sale.ddlCategory();
                        l = 0;
                        totalsellingprice = 0;
                        dtCategory = ds.Tables[0];
                        ds = Sale.ddlBrand();
                        dtBrand = ds.Tables[0];
                        dt.Columns.Add("HDSP");
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["HDSP"] = "0";
                            dr["DiscountValue"] = "0";
                        }
                        
                          
                        gvSaleDetails.DataSource = dt;
                        gvSaleDetails.DataBind();
                        txtTotalSellingPrice.Text = totalsellingprice.ToString();
                        gvSaleDetails.Visible = true;
                        //  gvSaleDetails.Columns[6].Visible = false;
                        ShowTabs(2);
                        btnPayment.Visible = false;
                        
                        txtPaidAmount.Visible = true;
                        txtBalance.Visible = true;
                        //lblBalance.Visible = true;
                       // lblPaidAmount.Visible = true;
                        
                    }
                    else
                    {
                        AddEmptyRows();
                        imgSave.Visible = false;
                    }

                    FillUser();
                    FillSalesMan();
                    if (salesman != "")
                        ddlSalesMan.SelectedValue = salesman;
                    else
                        ddlSalesMan.SelectedValue = "0";
                    if (userid != "")
                        ddlUser.SelectedValue = userid;
                    else
                        ddlUser.SelectedValue = "0";
                    btnPrescription.Visible = false;
                }
                lnkAdd.Text = "View";

                txtCustomerName.Enabled = false;
                txtCustomerNo.Enabled = false;
                btnPayment.Visible = false;
                OrderLense();
                if (deletepermission == false)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LinkButton im = (LinkButton)gvSaleDetails.Rows[i].FindControl("imgDeleteRow");
                        im.Visible = false;
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LinkButton im = (LinkButton)gvSaleDetails.Rows[i].FindControl("imgDeleteRow");
                        im.Visible = true;
                    }
                }
                //float temptotalgrossvalue = 0;
                //if(((Label)gvOrderLense.FooterRow.FindControl("lblOrderLenseTotal")).Text!="")
                //temptotalgrossvalue = Convert.ToSingle(((Label)gvOrderLense.FooterRow.FindControl("lblOrderLenseTotal")).Text);
            }
            if (e.CommandName == "Editing")
            {
                gvOrderLense1.Visible = false;
                gvPrescription1.Visible = false;
                btnOrderLense.Visible = true;
                HDSaleID.Value = salesid.ToString();
                EditFunction(salesid);
                //lnkAdd.Text = "Edit";

                txtCustomerName.Enabled = true;
                txtCustomerNo.Enabled = true;
                OrderLense();
                btnOrderLense.Visible = true;
                btnPayment.Visible = true;
            }
            if (e.CommandName == "Delete")
            {
                Sale.SalesID = salesid;
                ds = Sale.DeleteSales();
                //lblStatus.Text = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                ShowTabs(1);
            }
            if (e.CommandName == "Print")
            {
                rowindex = Convert.ToInt32(e.CommandArgument);
                salesid = Convert.ToInt32(gvSales.DataKeys[rowindex].Value);
                Sale = new ESales();
                ds = new DataSet();
                Sale.SalesID = salesid;
                if (salesid != 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "SalesPrint(" + salesid + ");", true);
                }

            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void EditFunction(int salesid)
    {
        try
        {
            Sale = new ESales();
            Sale.SalesID = salesid;
            ds = Sale.GetSalesDetailsGrid();
            HDSaleID.Value = salesid.ToString();
            string userid = "", salesman = "";
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);
                    txtCustomerName.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"]);
                    txtCustomerNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["CustomerNo"]);
                    txttotalgrossvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["GrossTotal"]);
                    txtDiscount.Text = Convert.ToString(ds.Tables[0].Rows[0]["Discount"]);
                    txtNetValue.Text = Convert.ToString(ds.Tables[0].Rows[0]["NetTotal"]);                   
                    txtInvoiceDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceDate"]);
                    txtInvoiceNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["InvoiceNo"]);
                    userid = Convert.ToString(ds.Tables[0].Rows[0]["CreatedBy"]);
                    salesman = Convert.ToString(ds.Tables[0].Rows[0]["UserID"]);
                    btnSave.Visible = false;
                    SDNCalculation.Visible = true;
                    EnableControls(false);
                    txtCustomerNo.Enabled = true;
                    txtCustomerName.Enabled = true;
                    imgSave.Visible = true;

                    //if (Convert.ToInt32(ds.Tables[0].Rows[0]["GrossTotal"])==0)
                    //{
                    //    lbladvancepaidamount.Text = "Advance";
                    //}
                    //else
                    //{
                    //    lbladvancepaidamount.Text = "Advance";
                    //}

                    if (Convert.ToString(ds.Tables[0].Rows[0]["Balance"]) == "0.00")
                    {
                        txtBalance.Text = "0.00";//Convert.ToString(ds.Tables[0].Rows[0]["Balance"]);
                        hdBalance.Value = txtBalance.Text;
                        txtadvancepaidamount.Text = "0.00";
                        if (Convert.ToString(ds.Tables[2].Rows[0]["PaidAmount"]) != "")
                            txtPaidAmount.Text = Convert.ToString(ds.Tables[2].Rows[0]["PaidAmount"]);
                        else
                            txtPaidAmount.Text = "0.00";
                        hdpaidamount.Value = Convert.ToString(ds.Tables[2].Rows[0]["PaidAmount"]);
                    }
                    else
                    {
                        txtBalance.Text = Convert.ToString(ds.Tables[0].Rows[0]["Balance"]);//"0.00";
                        hdBalance.Value = Convert.ToString(ds.Tables[0].Rows[0]["Balance"]);
                        txtadvancepaidamount.Text = "0.00";
                        //txtadvancepaidamount.Text = Convert.ToString(ds.Tables[0].Rows[0]["Balance"]);
                        txtPaidAmount.Text = Convert.ToString(ds.Tables[2].Rows[0]["PaidAmount"]); //Convert.ToString(ds.Tables[0].Rows[0]["Balance"]);
                        hdpaidamount.Value = Convert.ToString(ds.Tables[2].Rows[0]["PaidAmount"]);
                    }
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    dt = ds.Tables[1];
                    dt.Columns.Add("HDSP");
                    dt.Columns.Add("DiscountValue");
                    DataRow dr = dt.NewRow();
                    dr["SalesDetailsID"] = "0";
                    dr["CategoryID"] = "0";
                    dr["BrandID"] = "0";
                    dr["ProductID"] = "0";
                    dr["BrandName"] = "";
                    dr["ProductName"] = "";
                    dr["Quantity"] = "0";
                    dr["ProductValue"] = "0.00";
                    dr["SellingPrice"] = "0.00";
                    dr["MaxDiscount"] = 0;
                    dr["HDSP"] = 0;
                    //dr["Discount"] = "0";
                    dr["DiscountValue"] = "0";
                    dt.Rows.Add(dr);
                    Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
                    ds = Sale.ddlCategory();
                    totalsellingprice = 0;
                    l = 0;
                    dtCategory = ds.Tables[0];
                    ds = Sale.ddlBrand();
                    dtBrand = ds.Tables[0];
                    gvSaleDetails.DataSource = dt;
                    gvSaleDetails.DataBind();
                    txtTotalSellingPrice.Text = totalsellingprice.ToString();
                    //gvSaleDetails.Columns[6].Visible = true;
                    gvSaleDetails.Visible = true;
                    ShowTabs(2);
                    btnPayment.Visible = true;
                    txtPaidAmount.Visible = true;
                    txtBalance.Visible = true;
                   // lblBalance.Visible = true;
                    //lblPaidAmount.Visible = true;
                    SDNCalculation.Visible = true;
                    ((TextBox)gvSaleDetails.Rows[gvSaleDetails.Rows.Count - 1].FindControl("txtProduct")).Focus();
                 
                }
                else
                {
                    //gvSaleDetails.Columns[6].Visible = true;
                    ShowTabs(2);
                    AddEmptyRows();
                    ((TextBox)gvSaleDetails.Rows[0].FindControl("txtProduct")).Focus();
                }

                btnPrescription.Visible = true;
                FillUser();
                FillSalesMan();
                if(salesman!="")
                ddlSalesMan.SelectedValue = salesman;
                if (userid != "")
                {
                    ddlUser.Enabled = false;
                    ddlUser.SelectedValue = userid;
                }
                else
                {
                    ddlUser.Enabled = true;
                    ddlUser.SelectedValue = "0";
                }
                if (deletepermission == false)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LinkButton im = (LinkButton)gvSaleDetails.Rows[i].FindControl("imgDeleteRow");
                        im.Visible = false;
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        LinkButton im = (LinkButton)gvSaleDetails.Rows[i].FindControl("imgDeleteRow");
                        im.Visible = true;
                    }
                }
                //FillUser();
                //string str = "Lenses";
                //str = str.ToUpper();
                //string str1 = "Lense";
                //str1 = str1.ToUpper();
                //foreach (GridViewRow gr in gvSaleDetails.Rows)
                //{
                //    DropDownList ddlcategory = (DropDownList)gr.FindControl("ddlCategory");
                //    if (ddlcategory.SelectedItem.Text.ToUpper() == str || ddlcategory.SelectedItem.Text.ToUpper() == str1)
                //    {
                //        btnPrescription.Visible = true;
                //        break;
                //    }
                //    else
                //    {
                //        btnPrescription.Visible = false;
                //    }
                //}
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void gvSales_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvSales_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void gvSales_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void txtProduct_ontextchanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddl = (DropDownList)gr.FindControl("ddlCategory");
            TextBox txtproduct = (TextBox)gr.FindControl("txtProduct");
            TextBox txtbrand = (TextBox)gr.FindControl("txtBrand");
            TextBox prodvalue = (TextBox)gr.FindControl("txtProductValue");
            TextBox txt = (TextBox)gr.FindControl("txtQuantity");
            HiddenField hdprodid = (HiddenField)gr.FindControl("HDProductID");
            HiddenField hdbrandid = (HiddenField)gr.FindControl("HDBrandID");
            HiddenField hdproddiscount = (HiddenField)gr.FindControl("HDProductDiscount");
            HiddenField hdsp = (HiddenField)gr.FindControl("hiddensellingprice");
            Sale = new ESales();
            bool ok = false;
            if (txtproduct.Text != "")
            {
                Sale.ProductName = txtproduct.Text.Trim();
                int categoryid = 0, brandid = 0;
                if (ddl.SelectedValue != "0")
                    categoryid = Convert.ToInt32(ddl.SelectedValue);
                else
                    categoryid = 0;
                Sale.CategoryID = categoryid;
                if (hdbrandid.Value != "" && hdbrandid.Value != "0")
                    brandid = Convert.ToInt32(hdbrandid.Value);
                else
                    brandid = 0;
                if (txtbrand.Text == "")
                    brandid = 0;
                Sale.BrandID = brandid;
                Sale.BrandName = txtbrand.Text.Trim();
                Sale.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
                ds = new DataSet();
                if (categoryid == 0 || brandid == 0)
                {
                    ds = Sale.GetProductCategorybrandID();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CategoryID"]);
                        hdbrandid.Value = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                        hdprodid.Value = Convert.ToString(ds.Tables[0].Rows[0]["ProductID"]);
                        prodvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                        txtbrand.Text = Convert.ToString(ds.Tables[0].Rows[0]["BrandName"]);
                        hdproddiscount.Value = Convert.ToString(ds.Tables[0].Rows[0]["MaxDiscount"]);
                        txt.Text = "1";
                        hdsp.Value = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                        //for (int i = 0; i < gvSaleDetails.Rows.Count; i++)
                        //{
                        //    if (((DropDownList)gvSaleDetails.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" && 
                        //        ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDBrandID")).Value != "" 
                        //        && ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductID")).Value != "")
                        //        {
                        //        if(i!=gr.RowIndex)
                        //        {
                        //            if (((DropDownList)gvSaleDetails.Rows[i].FindControl("ddlCategory")).SelectedValue == ddl.SelectedValue &&
                        //               ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDBrandID")).Value == hdbrandid.Value &&
                        //               ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductID")).Value == hdprodid.Value)
                        //            {
                        //                ddl.SelectedValue = "0";
                        //                txtbrand.Text = "";
                        //                prodvalue.Text = "0.00";
                        //                hdbrandid.Value = "";
                        //                hdprodid.Value = "";
                        //                txtproduct.Text = "";
                        //                txt.Text = "0";
                        //                hdsp.Value = "0";
                        //                ok = false;
                        //                txtproduct.Focus();
                        //                dvFailure.Visible = true;
                        //                lblStatus.Text = "Line Items must me unique";
                        //                return;
                        //            }
                        //            else
                        //            {
                        //                dvFailure.Visible = false;
                        //                lblStatus.Text = "";
                        //            }
                        //        }
                                   
                        //        }

                        //    }
                        
                    }
                    else
                    {
                        ddl.SelectedValue = "0";
                        txtbrand.Text = "";
                        prodvalue.Text = "0.00";
                        hdbrandid.Value = "";
                        hdprodid.Value = "";
                        txt.Text = "0";
                        hdsp.Value = "0";
                        ok = false;
                        txtproduct.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('No Stock Avaiable for this Product.');", true);
                        return;
                    }
                }
                else
                {
                    ds = Sale.GetProductIDandValue();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        hdbrandid.Value = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                        hdprodid.Value = Convert.ToString(ds.Tables[0].Rows[0]["ProductID"]);
                        prodvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                        txtbrand.Text = Convert.ToString(ds.Tables[0].Rows[0]["BrandName"]);
                        hdproddiscount.Value = Convert.ToString(ds.Tables[0].Rows[0]["MaxDiscount"]);
                        hdsp.Value = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                        txt.Text = "1";
                        //for (int i = 0; i < gvSaleDetails.Rows.Count; i++)
                        //{
                        //    if (((DropDownList)gvSaleDetails.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" &&
                        //        ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDBrandID")).Value != ""
                        //        && ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductID")).Value != "")
                        //    {
                        //        if (i != gr.RowIndex)
                        //        {
                        //            if (((DropDownList)gvSaleDetails.Rows[i].FindControl("ddlCategory")).SelectedValue == ddl.SelectedValue &&
                        //               ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDBrandID")).Value == hdbrandid.Value &&
                        //               ((HiddenField)gvSaleDetails.Rows[i].FindControl("HDProductID")).Value == hdprodid.Value)
                        //            {
                        //                ddl.SelectedValue = "0";
                        //                txtbrand.Text = "";
                        //                prodvalue.Text = "0.00";
                        //                hdbrandid.Value = "";
                        //                hdprodid.Value = "";
                        //                txtproduct.Text = "";
                        //                txt.Text = "0";
                        //                hdsp.Value = "0";
                        //                ok = false;
                        //                txtproduct.Focus();
                        //                dvFailure.Visible = true;
                        //                lblStatus.Text = "Line Items must me unique";
                        //                return;
                        //            }
                        //            else
                        //            {
                        //                dvFailure.Visible = false;
                        //                lblStatus.Text = "";
                        //            }
                        //        }

                        //    }

                        //}
                    }
                    else
                    {
                        ddl.SelectedValue = "0";
                        txtbrand.Text = "";
                        prodvalue.Text = "0.00";
                        hdbrandid.Value = "";
                        hdprodid.Value = "";
                        txt.Text = "0";
                        hdsp.Value = "0";
                        ok = false;
                        txtproduct.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('No Stock Avaiable for this Product.');", true);
                        return;
                    }
                }
                int qty = 0;               
                if (txt.Text != "0" && txt.Text != "")
                    qty = Convert.ToInt32(txt.Text);
                if (qty != 0)
                {
                    ((TextBox)gr.FindControl("txtSellingPrice")).Text = Convert.ToString(qty * Convert.ToSingle(prodvalue.Text));
                }
                int rowcount = gvSaleDetails.Rows.Count;
                txtproduct = sender as TextBox;
                string ID = txtproduct.ClientID;
                ID = ID.Replace("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl", "");
                ID = ID.Replace("_txtProduct", "");

              //  int maxrow = Convert.ToInt32(ID) - 1;

                int gvcount = gvSaleDetails.Rows.Count - 1;
                int gvrowindex = gr.RowIndex;
                if (gvcount == gvrowindex)
                {
                    AddNewRow();
                }
                else
                {
                    txt.Focus();
                }
                float totalgrossvalue = 0, totalsellingprice = 0, totalnetvalue = 0,totaldis=0;
                if (txtPaidAmount.Text == "")
                {
                    txtPaidAmount.Text = "0";
                }
                float paidamount=Convert.ToSingle(txtPaidAmount.Text);
                //if (Convert.ToSingle(txttotalgrossvalue.Text) != 0)
                //    totalgrossvalue = Convert.ToSingle(txttotalgrossvalue.Text);
                //if (Convert.ToSingle(txtNetValue.Text) != 0)
                //    totalsellingprice = Convert.ToSingle(txtNetValue.Text);
                foreach (GridViewRow g in gvSaleDetails.Rows)
                {
                    TextBox txtsel = (TextBox)g.FindControl("txtSellingPrice");
                    float sp = Convert.ToSingle(txtsel.Text);
                    TextBox txtqty = (TextBox)g.FindControl("txtQuantity");
                    int qty1 = Convert.ToInt32(txtqty.Text);
                    TextBox txtpro = (TextBox)g.FindControl("txtProductValue");
                    float pv = Convert.ToSingle(txtpro.Text);
                    if (txtsel.Text != "" && txtsel.Text != "0.00")
                    {
                        totalgrossvalue += Convert.ToSingle(qty1*pv);
                        totalsellingprice += Convert.ToSingle(sp);
                        totalnetvalue = totalsellingprice;
                    }
                }
                float mt = 0;
                float olt = 0,gv=0,sp1=0,nv=0;
                if (hdorderlensevalue.Value != "")
                {
                    olt = Convert.ToSingle(hdorderlensevalue.Value);
                }
                foreach (GridViewRow g in gvSaleDetails.Rows)
                {
                    TextBox txtsel = (TextBox)g.FindControl("txtSellingPrice");
                    float sp = Convert.ToSingle(txtsel.Text);
                    TextBox txtqty = (TextBox)g.FindControl("txtQuantity");
                    int qty1 = Convert.ToInt32(txtqty.Text);
                    TextBox txtpro = (TextBox)g.FindControl("txtProductValue");
                    float pv = Convert.ToSingle(txtpro.Text);
                    if (txtsel.Text != "" && txtsel.Text != "0.00")
                    {
                        gv += Convert.ToSingle(qty1 * pv);
                        sp1 += Convert.ToSingle(sp);
                        nv = totalsellingprice;
                    }
                }
                txttotalgrossvalue.Text = (olt + gv).ToString();
                txtTotalSellingPrice.Text = totalsellingprice.ToString();
                txtNetValue.Text = (sp1 + olt).ToString();
                float paidamount1;
                if (hdBalance.Value == "0.00" || hdBalance.Value=="")
                {
                   
                    if (hdpaidamount.Value != "")
                        paidamount1 = Convert.ToSingle(hdpaidamount.Value);
                    else
                        paidamount1 = 0;
                    if (paidamount1!=0)
                    {

                        txtadvancepaidamount.Text = "0.00"; //(totalnetvalue - paidamount1).ToString();
                        txtPaidAmount.Text = paidamount1.ToString(); //txtadvancepaidamount.Text;
                        txtBalance.Text = (olt + gv - paidamount1).ToString();
                    }
                    else
                    {
                        //txtadvancepaidamount.Text = "0";//totalnetvalue.ToString();
                        //txtPaidAmount.Text = Convert.ToString((totalnetvalue)-Convert.ToSingle(txtadvancepaidamount.Text));// totalnetvalue.ToString();
                        if (txtadvancepaidamount.Text == "")
                        {
                            txtadvancepaidamount.Text = "0.00";
                            txtBalance.Text = (Convert.ToSingle(totalnetvalue.ToString()) - Convert.ToSingle(paidamount1)).ToString(); //"0.00";// Convert.ToString((totalnetvalue) - Convert.ToSingle(txtPaidAmount.Text));
                        }
                        else
                        {
                            txtBalance.Text=(Convert.ToSingle(olt+nv)- Convert.ToSingle(txtadvancepaidamount.Text)).ToString();
                        }
                         
                    }
                }
                else
                {
                    if (hdpaidamount.Value != "")
                        paidamount1 = Convert.ToSingle(hdpaidamount.Value);
                    else
                        paidamount1 = 0;
                   
                    //txtadvancepaidamount.Text = ((totalnetvalue) - (paidamount1)).ToString();
                    txtPaidAmount.Text = Convert.ToString(hdpaidamount.Value);//txtadvancepaidamount.Text;
                    if ((txtadvancepaidamount.Text != "") || (txtadvancepaidamount.Text != null))
                    {
                        txtBalance.Text = ((olt + nv) - (paidamount1) - Convert.ToSingle(txtadvancepaidamount.Text)).ToString();
                    }
                    else
                    {
                        txtBalance.Text = ((totalnetvalue) - (paidamount1)).ToString();  //"0.00"; //((totalnetvalue) - (paidamount1)).ToString();
                    }
                    
                }
                //if(txtadvancepaidamount.Text=="0")
                //    txtadvancepaidamount.Text = totalnetvalue.ToString();
                //if (txtPaidAmount.Text == "0.00")
                //{
                //    txtPaidAmount.Text = totalnetvalue.ToString();
                //    txtBalance.Text = "0.00";
                //}
                //if(hdBalance.Value=="0.00")
                //    hdBalance.Value = totalnetvalue.ToString();
                if (totalgrossvalue != 0)
                {
                    totaldis = ((totalgrossvalue - totalsellingprice) / totalgrossvalue) * 100;
                    totaldis =Convert.ToSingle( Math.Round(totaldis, 2));
                }
                else
                   totaldis = 0;
                txtDiscount.Text = Convert.ToSingle(totaldis).ToString();
                 string nextid = string.Empty;
                 int id = Convert.ToInt32(ID);
                 int rowindex = id - 1;
                if (Convert.ToInt32(ID) < 9)
                    nextid = "0" + Convert.ToString((Convert.ToInt32(ID) + 1));
                else
                    nextid = Convert.ToString(Convert.ToInt32(ID)+1);

              //  TextBox txtprod = (TextBox)(gvSaleDetails.Rows[rowindex].FindControl("txtProduct"));
                    //(TextBox) gr.FindControl("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl"+nextid+"_txtProduct");
                //txtprod.Focus();
            }
            else
            {
                ddl.SelectedValue = "0";
                txtbrand.Text = "";
                prodvalue.Text = "0.00";
                txt.Text = "0";
                hdprodid.Value = "";
                hdbrandid.Value = "";
            }

            
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void txtBrand_ontextchanged(object sender, EventArgs e)
    {
        try
        {
             GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
             TextBox txtbrand = sender as TextBox;
             if (txtbrand.Text != "")
             {
                 DropDownList ddl = (DropDownList)gr.FindControl("ddlCategory");
                 HiddenField hd = (HiddenField)gr.FindControl("HDBrandID");
                 Sale = new  ESales();
                 Sale.BrandName = txtbrand.Text.Trim();
                 ds = new DataSet();
                 ds = Sale.GetBrandID();
                 int brandid=0;
                 if(ds.Tables[0].Rows.Count>0)
                 {
                     brandid = Convert.ToInt32(ds.Tables[0].Rows[0]["BrandID"]);
                     hd.Value = brandid.ToString();
                     AjaxControlToolkit.AutoCompleteExtender ae = (AjaxControlToolkit.AutoCompleteExtender)gr.FindControl("AutoCompleteExtender2");
                     ae.ContextKey = ddl.SelectedValue + "~" + brandid.ToString();
                 }
                 
             }
            // txtbrand.Focus();
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dvPaymentDetails.Attributes["style"] = "display:none";
        gvInvoicePayment.Visible = false;
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true); 
    }
    protected void btnSavePayments_Click(object sender, EventArgs e)
    {
        try
        {
            Sale = new ESales();
            string Griddata = string.Empty;
            int invoiceid = 0;
            for (int i = 0; i < gvInvoicePayment.Rows.Count; i++)
            {
                invoiceid = Convert.ToInt32(((HiddenField)gvInvoicePayment.Rows[i].FindControl("HDInvoicePaymentID")).Value);
                if (invoiceid == 0)
                {
                    if (((DropDownList)gvInvoicePayment.Rows[i].FindControl("ddlPaymentMode")).SelectedValue != "0" && ((TextBox)gvInvoicePayment.Rows[i].FindControl("txtPaymentAmount")).Text != "" && ((TextBox)gvInvoicePayment.Rows[i].FindControl("txtBalance")).Text != "")
                        Griddata += converttodate(((TextBox)gvInvoicePayment.Rows[i].FindControl("txtPaymentDate")).Text) + "~"
                             + ((DropDownList)gvInvoicePayment.Rows[i].FindControl("ddlPaymentMode")).SelectedValue + "~"
                            + ((TextBox)gvInvoicePayment.Rows[i].FindControl("txtPaymentAmount")).Text + "~"
                            + ((TextBox)gvInvoicePayment.Rows[i].FindControl("txtRecieptNo")).Text + "~"
                            + ((TextBox)gvInvoicePayment.Rows[i].FindControl("txtBalance")).Text + "$";
                }
            }
            if (Griddata != "")
                Griddata = Griddata.Substring(0, Griddata.Length - 1);
            Sale.GridData = Griddata;
            Sale.SalesID = Convert.ToInt32(HDSaleID.Value);
            int SaleID = Sale.SalesID;
            if (txtRemark.Text != "")
                Sale.Remarks = txtRemark.Text;
            else
                Sale.Remarks = "";
            ds = Sale.SavePayment();
            if (ds.Tables[0].Rows.Count > 0)
            {
               // GetInvoiceDetails();
                dvPaymentDetails.Attributes["style"] = "display:none;";
                EditFunction(Convert.ToInt32(HDSaleID.Value));
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "PrintPayment(" + SaleID + ");", true);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    [WebMethod]
    public static GridDataSet PrintPayment(int Saleid)
    {
        ESales Sale = new ESales();
        ESaleList eSalelist = new ESaleList();
        DataSet ds = new DataSet();
        DataSet dsforpayment = new DataSet();
        List<ESales> esales = new List<ESales>();
        List<ESaleList> esalelist = new List<ESaleList>();
        GridDataSet objGridDataset = new GridDataSet();
        Sale.SalesID = Saleid;
        try
        {
            dsforpayment = Sale.GetPaymentPrint();
            if (dsforpayment.Tables[0].Rows.Count > 0)
            {
                Sale.CustomerName = dsforpayment.Tables[0].Rows[0]["CustomerName"].ToString();
                Sale.CustomerNo = (dsforpayment.Tables[0].Rows[0]["CustomerNo"]).ToString();
                Sale.StoreName = dsforpayment.Tables[0].Rows[0]["StoreName"].ToString();
                Sale.VatID = dsforpayment.Tables[0].Rows[0]["VATID"].ToString();
                Sale.City = dsforpayment.Tables[0].Rows[0]["City"].ToString();
                Sale.Address = dsforpayment.Tables[0].Rows[0]["Address"].ToString();
                Sale.Email = dsforpayment.Tables[0].Rows[0]["EmailID"].ToString();
                Sale.Discount = Convert.ToSingle(dsforpayment.Tables[0].Rows[0]["Discount"]);
                Sale.InvoiceDate = dsforpayment.Tables[0].Rows[0]["InvoiceDate"].ToString();
                Sale.InvoiceNo = dsforpayment.Tables[0].Rows[0]["InvoiceNo"].ToString();
                Sale.NetTotal = Convert.ToSingle(dsforpayment.Tables[0].Rows[0]["NetTotal"]);
                Sale.Balance = Convert.ToSingle(dsforpayment.Tables[0].Rows[0]["Balance"]);
                Sale.ContactNum = dsforpayment.Tables[0].Rows[0]["ContactNumber"].ToString();
                esales.Add(Sale);
            }
            ds = Sale.GetPaymentSavePrint();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    eSalelist = new ESaleList();
                    eSalelist.IPBalance = Convert.ToSingle(ds.Tables[0].Rows[i]["IPBalance"]);
                    eSalelist.PaymentDate = ds.Tables[0].Rows[i]["PaymentDate"].ToString();
                    eSalelist.PaymentMode = ds.Tables[0].Rows[i]["PaymentMode"].ToString();
                    eSalelist.PaymentAmount = Convert.ToSingle(ds.Tables[0].Rows[i]["PaymentAmount"]);
                    eSalelist.PaymentReceiptNum = (ds.Tables[0].Rows[i]["ReceiptNo"]).ToString();
                    esalelist.Add(eSalelist);
                }

            }

        }
        catch (Exception)
        {

            throw;
        }
        objGridDataset.eSales = esales;
        objGridDataset.eSaleslist = esalelist;
        return objGridDataset;
    }

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        dvPaymentDetails.Attributes["style"] = "display:block;overflow:auto;";
        gvInvoicePayment.Visible = true;
        txtPopInvoiceNo.Text = txtInvoiceNo.Text;
        txtPopInvoiceDate.Text = txtInvoiceDate.Text;
        GetInvoiceDetails();
    }

    protected void GetInvoiceDetails()
    {
        try
        {
            Sale = new ESales();
            ds = new DataSet();
            Sale.SalesID = Convert.ToInt32(HDSaleID.Value);
            ds = Sale.GetInvoiceDetails();
            if (ds.Tables.Count > 0)
            {
                txtPopupAmount.Text = Convert.ToString(ds.Tables[0].Rows[0]["NetTotal"]);
                txtPopupBalance.Text = Convert.ToString(ds.Tables[0].Rows[0]["Balance"]);
                txtBalance.Text = Convert.ToString(ds.Tables[0].Rows[0]["Balance"]);
                if (ds.Tables[1].Rows.Count > 0)
                {
                    dt1 = ds.Tables[1];
                    //DataRow dr = dt1.NewRow();
                    //dr["InvoicePaymentID"] = "0";
                    //dr["PaymentAmount"] = "0.00";
                    //dr["PaymentDate"] = System.DateTime.Now.ToString("dd-MM-yyyy");
                    //dr["PaymentMode"] = "0";
                    //dr["ReceiptNo"] = "";
                    //dr["Balance"] = "0.00";
                    //dt1.Rows.Add(dr);
                    k = 0;
                    gvInvoicePayment.DataSource = dt1;
                    gvInvoicePayment.DataBind();
                }
                //else
                //{
                //    CreateEmptyRows();
                //}
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void CreateEmptyRows()
    {
        try
        {
            dt1 = new DataTable();
            if (dt1.Rows.Count == 0 || dt1.Rows.Count < 5)
            {
                dt1 = new DataTable();                
                dt1.Columns.Add("InvoicePaymentID");
                dt1.Columns.Add("PaymentAmount");
                dt1.Columns.Add("PaymentDate");
                dt1.Columns.Add("PaymentMode");
                dt1.Columns.Add("ReceiptNo");
                dt1.Columns.Add("Balance");
            }
            DataRow dr;
            for (int i = dt1.Rows.Count; i < 5; i++)
            {
                dr = dt1.NewRow();
                dr["InvoicePaymentID"] = "0";
                dr["PaymentAmount"] = "0.00";
                dr["PaymentDate"] = System.DateTime.Now.ToString("dd-MM-yyyy");
                dr["PaymentMode"] = "0";
                dr["ReceiptNo"] = "";
                dr["Balance"] = "0.00";
                dt1.Rows.Add(dr);
            }
            k = 0;
            gvInvoicePayment.DataSource = dt1;
            gvInvoicePayment.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnCancelPayments_Click(object sender, EventArgs e)
    {
        dvPaymentDetails.Attributes["style"] = "display:none;";
        gvInvoicePayment.Visible = false;
    }
    protected void ddlPaymentMode_selectedindexchanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddlpaymentmode = (DropDownList)gr.FindControl("ddlProduct");
            int rowcount = gvInvoicePayment.Rows.Count;
            ddlpaymentmode = sender as DropDownList;
            string ID = ddlpaymentmode.ClientID;
            ID = ID.Replace("ctl00_ContentPlaceHolder1_gvInvoicePayment_ctl", "");
            ID = ID.Replace("_ddlPaymentMode", "");
            int maxrow = Convert.ToInt32(ID) - 1;
            if (rowcount == maxrow)
            {
                AddNewEmptyRow();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void AddNewEmptyRow()
    {
        try
        {
            dt1 = new DataTable();
            dt1.Columns.Add("InvoicePaymentID");
            dt1.Columns.Add("PaymentAmount");
            dt1.Columns.Add("PaymentDate");
            dt1.Columns.Add("PaymentMode");
            dt1.Columns.Add("ReceiptNo");
            dt1.Columns.Add("Balance");
            DataRow dr;
            for (int i = 0; i <= gvInvoicePayment.Rows.Count - 1; i++)
            {

                dr = dt1.NewRow();
                dr["InvoicePaymentID"] = ((HiddenField)gvInvoicePayment.Rows[i].FindControl("HDInvoicePaymentID")).Value;
                dr["PaymentAmount"] = ((TextBox)gvInvoicePayment.Rows[i].FindControl("txtPaymentAmount")).Text;
                dr["PaymentDate"] = ((TextBox)gvInvoicePayment.Rows[i].FindControl("txtPaymentDate")).Text;
                dr["PaymentMode"] = ((DropDownList)gvInvoicePayment.Rows[i].FindControl("ddlPaymentMode")).SelectedValue;
                dr["ReceiptNo"] = ((TextBox)gvInvoicePayment.Rows[i].FindControl("txtRecieptNo")).Text;
                dr["Balance"] = ((TextBox)gvInvoicePayment.Rows[i].FindControl("txtBalance")).Text;
                dt1.Rows.Add(dr);
            }
            dr = dt1.NewRow();
            dr["InvoicePaymentID"] = "0";
            dr["PaymentAmount"] = "0.00";
            dr["PaymentDate"] = System.DateTime.Now.ToString("dd-MM-yyyy");
            dr["PaymentMode"] = "0";
            dr["ReceiptNo"] = "";
            dr["Balance"] = "0.00";
            dt1.Rows.Add(dr);
            k = 0;
            gvInvoicePayment.DataSource = dt1;
            gvInvoicePayment.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }
    int k = 0;
    protected void gvInvoicePayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlpaymentmode = (DropDownList)e.Row.FindControl("ddlPaymentMode");
            if (Convert.ToInt32(dt1.Rows[k]["InvoicePaymentID"]) != 0)
            {
                e.Row.Enabled = false;
                ddlpaymentmode.SelectedValue = Convert.ToString(dt1.Rows[k]["PaymentMode"]);
            }
            else
            {
                e.Row.Enabled = true;
                ddlpaymentmode.SelectedValue = Convert.ToString(dt1.Rows[k]["PaymentMode"]);
            }
            k++;
        }

        
               
    }
    //protected void btnPrescription_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        dvPrescription.Attributes["style"] = "display:block;height:300px;width:800px;margin-top:55px;";
    //        gvPrescription.Visible = true;
    //        HtmlGenericControl mydiv = new HtmlGenericControl("DIV");
    //        mydiv.ID = "Divid";
    //        mydiv.Attributes.Add("class", "Overlay");
    //        this.Controls.Add(mydiv);

    //        Sale = new ESales();
    //        ds = new DataSet();
    //        dt3 = new DataTable();
    //        Sale.SalesID = Convert.ToInt32(HDSaleID.Value);
    //        ds = Sale.getPrescription();
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            // dt3.Columns.Add("").SetOrdinal(0);
    //            dt3 = new DataTable();
                
    //            dt3.Columns.Add("SPH");
    //            dt3.Columns.Add("CYL");
    //            dt3.Columns.Add("AXIS");
    //            dt3.Columns.Add("AD");
                
    //            dt3.ImportRow(ds.Tables[0].Rows[0]);
    //            dt3.ImportRow(ds.Tables[1].Rows[0]);
    //            dt3.ImportRow(ds.Tables[2].Rows[0]);
    //            //dt3.Columns.Add("");
    //            dt3.Columns.Add("").SetOrdinal(0);
    //            dt3.Rows[0][0] = "Right Eye";
    //            dt3.Rows[1][0] = "Left Eye";
    //            dt3.Rows[2][0] = "IPD";
    //            //for (int k = 0; k < dt3.Rows.Count; k++)
    //            //{
    //            //    for (int l = 0; k < dt3.Columns.Count; l++)
    //            //    { 
    //            //        dt[3]
    //            //    }
    //            //}
    //        }
    //        else
    //        {
    //            dt3 = new DataTable();
    //            dt3.Columns.Add("");
    //            dt3.Columns.Add("SPH");
    //            dt3.Columns.Add("CYL");
    //            dt3.Columns.Add("AXIS");
    //            dt3.Columns.Add("AD");
    //            DataRow dr;
    //            for (int i = 0; i < 3; i++)
    //            {
    //                dr = dt3.NewRow();
    //                dt3.Rows.Add(dr);
    //            }
    //            dt3.Rows[0][0] = "Right Eye";
    //            dt3.Rows[1][0] = "Left Eye";
    //            dt3.Rows[2][0] = "IPD";
    //        }
    //        m = 0;
    //        gvPrescription.DataSource = dt3;
    //        gvPrescription.DataBind();
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }

    //}
    protected void btnPrescriptionSave_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = gvPrescription.Rows.Count;
            string griddata = string.Empty;
            Sale = new ESales();
            ds = new DataSet();
            TextBox tt;
            for (int i = 0; i < rowcount; i++)
            {
                tt = (TextBox)gvPrescription.Rows[i].FindControl("txtSPH");
                if (tt.Text != "")
                    griddata += tt.Text + "~";
                else
                    griddata += "" + "~";
                tt = (TextBox)gvPrescription.Rows[i].FindControl("txtCYL");
                if (tt.Text != "")
                    griddata += tt.Text + "~";
                else
                    griddata += "" + "~";
                tt = (TextBox)gvPrescription.Rows[i].FindControl("txtAXIS");
                if (tt.Text != "")
                    griddata += tt.Text + "~";
                else
                    griddata += "" + "~";
                tt = (TextBox)gvPrescription.Rows[i].FindControl("txtADD");
                if (tt.Text != "")
                    griddata += tt.Text + "~";
                else
                    griddata += "" + "~";
            }
            if (griddata != "")
                griddata = griddata.Substring(0, griddata.Length - 1);
            Sale.SalesID = Convert.ToInt32(HDSaleID.Value);
            Sale.GridData = griddata;

            ds = Sale.SavePrescription();
            if (Convert.ToString(ds.Tables[0].Rows[0]["Status"]) == "Success")
            {
                dvSuccess.Visible = true;
                lblSuccess.Text = "Record inserted Successfully.";
                gvPrescription.Visible = false;
                dvPrescription.Attributes["style"] = "display:none;";
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true); 
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnPrescriptionCancel_Click(object sender, EventArgs e)
    {
        dvPrescription.Attributes["style"] = "display:none;";
        gvPrescription.Visible = false;
    }
    int m = 0;
    protected void gvPrescription_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (m < 2)
            {
                if (dtsph != null)
                {
                    DropDownList dts = (DropDownList)e.Row.FindControl("ddlsph");
                    dts.DataSource = dtsph;
                    dts.DataValueField = "val";
                    dts.DataTextField = "txt";
                    dts.DataBind();
                    //dts.SelectedValue = "0.00";
                    string val1;
                    if (Convert.ToString(dt3.Rows[m]["SPH"]) != "")
                    {
                        val1 = Convert.ToString(dt3.Rows[m]["SPH"]);
                        dts.SelectedValue = val1;
                    }
                    else
                        dts.SelectedValue = "0.00";

                }
                if (dtcyl != null)
                {
                    DropDownList dtc = (DropDownList)e.Row.FindControl("ddlcyl");
                    dtc.DataSource = dtcyl;
                    dtc.DataValueField = "val";
                    dtc.DataTextField = "txt";
                    dtc.DataBind();
                    //dtc.SelectedValue = "0.00";
                    string val;
                    if (Convert.ToString(dt3.Rows[m]["CYL"]) != "")
                    {
                        val = Convert.ToString(dt3.Rows[m]["CYL"]);
                        dtc.SelectedValue =val.ToString();
                    }
                    else
                        dtc.SelectedValue = "0.00";
                }
                if (dtaxis != null)
                {
                    DropDownList dta = (DropDownList)e.Row.FindControl("ddlaxis");
                    dta.DataSource = dtaxis;
                    dta.DataValueField = "val";
                    dta.DataTextField = "txt";
                    dta.DataBind();
                    double val;
                    if (Convert.ToString(dt3.Rows[m]["AXIS"]) != "")
                    {
                        val = Convert.ToDouble(dt3.Rows[m]["AXIS"]);
                        dta.SelectedValue = val.ToString();
                    }
                    else
                        dta.SelectedValue = "0";
                }
                if (dtadd != null)
                {
                    DropDownList dta = (DropDownList)e.Row.FindControl("ddladd");
                    dta.DataSource = dtadd;
                    dta.DataValueField = "val";
                    dta.DataTextField = "txt";
                    dta.DataBind();
                    //dta.SelectedValue = "0.00";
                    string val;
                    if (Convert.ToString(dt3.Rows[m]["AD"]) != "")
                    {
                        val = Convert.ToString(dt3.Rows[m]["AD"]);
                        dta.SelectedValue = val;
                    }
                    else
                        dta.SelectedValue = "0.00";
                }

                Label lbl = (Label)e.Row.FindControl("lblSection");
                lbl.Text = Convert.ToString(dt3.Rows[m][0]);
            }
            m++;
        }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (dt1 != null)
                {
                    TextBox txt = (TextBox)e.Row.FindControl("txtsph");
                    if (Convert.ToString(dt1.Rows[0]["SPH"]) != "")
                        txt.Text = Convert.ToString(dt1.Rows[0]["SPH"]);
                    txt = (TextBox)e.Row.FindControl("txtcyl");
                    if (Convert.ToString(dt1.Rows[0]["CYL"]) != "")
                        txt.Text = Convert.ToString(dt1.Rows[0]["CYL"]);
                    txt = (TextBox)e.Row.FindControl("txtaxis");
                    if (Convert.ToString(dt1.Rows[0]["AXIS"]) != "")
                        txt.Text = Convert.ToString(dt1.Rows[0]["AXIS"]);
                    txt = (TextBox)e.Row.FindControl("txtadd");
                    if (Convert.ToString(dt1.Rows[0]["AD"]) != "")
                        txt.Text = Convert.ToString(dt1.Rows[0]["AD"]);
                }
                else
                {
                    ((TextBox)e.Row.FindControl("txtsph")).Text = "";
                    ((TextBox)e.Row.FindControl("txtcyl")).Text = "";
                    ((TextBox)e.Row.FindControl("txtaxis")).Text = "";
                    ((TextBox)e.Row.FindControl("txtadd")).Text = "";
                }
                m++;
            }
            
       
    }
    protected void btnClearProduct_onclick(object sender, EventArgs e)
    {
        dvProductPopUp.Attributes["style"] = "display:none";
    }
    protected void imgSaveProduct_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string result = string.Empty;
            product = new EProduct();
            if (txtProductName.Text != "")
                product.ProductName = txtProductName.Text;
            else
                product.ProductName = "";
            if (DDLCategory.SelectedValue != "0")
                product.CategoryID = Convert.ToInt32(DDLCategory.SelectedValue);
            else
                product.CategoryID = 0;
            if (txtProductValue.Text != "")
                product.ProductValue = Convert.ToSingle(txtProductValue.Text);
            else
                product.ProductValue = 0;
            if (txtDefaultSellingDiscount.Text != "")
                product.SellingDiscount = Convert.ToSingle(txtDefaultSellingDiscount.Text);
            else
                product.SellingDiscount = 0;
            if (ddlBrand.SelectedValue != "0")
                product.BrandID = Convert.ToInt32(ddlBrand.SelectedValue);
            else
                product.BrandID = 0;

            int loginid = Convert.ToInt32(Session["LOGINID"]);
            product.MaxDiscountPer = Convert.ToDecimal(txtMaxDiscPer.Text);
            if (chkActive.Checked)
                product.Active = true;
            else
                product.Active = false;
            product.LoginID = loginid;
            ds = new DataSet();
            ds = product.Insert();
            if (result == "Record Inserted Succesfully.")
            {
                //ShowTabs(1);
                dvSuccess.Visible = true;
                lblSuccess.Text = result;
               // lblStatus.CssClass = "SuccessMsg";               
                dvProductPopUp.Attributes["style"] = "display:none";
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true);                

            }
            else
            {
                
                //lblStatusProduct.CssClass = "ErrorMsg";                
                clearproductpopupfields();
                dvFailure.Visible = true;
                lblStatusProduct.Text = "Details already Exist";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void FillCategory()
    {
        try
        {
            ds = new DataSet();
            product = new EProduct();
            ds = product.DDLCategory();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLCategory.DataSource = ds.Tables[0];
                DDLCategory.DataTextField = "CategoryName";
                DDLCategory.DataValueField = "CategoryID";
                DDLCategory.DataBind();
                DDLCategory.Items.Insert(0, new ListItem("--Any--", "0"));

                //DDLCategorySearch.DataSource = ds.Tables[0];
                //DDLCategorySearch.DataTextField = "CategoryName";
                //DDLCategorySearch.DataValueField = "CategoryID";
                //DDLCategorySearch.DataBind();
                //DDLCategorySearch.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                DDLCategory.Items.Insert(0, new ListItem("--Any--", "0"));
                //DDLCategorySearch.Items.Insert(0, new ListItem("--Any--", "0"));
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void FillBrand()
    {
        try
        {
            ds = new DataSet();
            product = new EProduct();
            ds = product.ddlBrand();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ddlBrandSearch.DataSource = ds.Tables[0];
                //ddlBrandSearch.DataTextField = "BrandName";
                //ddlBrandSearch.DataValueField = "BrandID";
                //ddlBrandSearch.DataBind();
                //ddlBrandSearch.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlBrand.Items.Clear();
                ddlBrand.DataSource = ds.Tables[0];
                ddlBrand.DataTextField = "BrandName";
                ddlBrand.DataValueField = "BrandID";
                ddlBrand.DataBind();
                ddlBrand.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                ddlBrand.Items.Insert(0, new ListItem("--Any--", "0"));
                //ddlBrandSearch.Items.Insert(0, new ListItem("--Any--", "0"));
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btncancelgrid_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        ShowTabs(1);
        FillGrid();
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        txtSerialNo.Text = "";
        txtsearchinvoiceNo.Text = "";
        txtSearchCustomerNo.Text = "";
        txtSearchCustomerName.Text = "";
        ddlSearchStore.SelectedIndex =0;
        txtFromDate.Text = "";
        txtToDate.Text = "";
        FillGrid();
    }
    protected void imgAddBrand_Click(object sender, EventArgs e)
    {
        EBrand objBrand = new EBrand();
        DataSet dsBrand = new DataSet();
        string result = string.Empty;
        try
        {
            objBrand.BrandName = txtAddBrand.Text;
            if (chkActive.Checked)
                objBrand.IsActive = true;
            else
                objBrand.IsActive = false;
            objBrand.LoginID = Convert.ToInt32(Session["LOGINID"].ToString());
            result = objBrand.EAddBrand();
            if (result == "Success")
            {
                dvSuccess.Visible = true;
                lblSuccess.Text = "Brand Details Inserted Successfully";
              //  lblStatus.CssClass = "SuccessMsg";
                txtAddBrand.Text = string.Empty;
                dvBrandPopUp.Attributes["style"] = "display:none";
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true); 
                
            }
            else
            {
                dvFailure.Visible = true;
                lblStatusBrand.Text = "Details already Exist";
                //lblStatusBrand.CssClass = "ErrorMsg";
                txtAddBrand.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {

            lblStatus.Text = ex.Message;
            lblStatus.CssClass = "ErrorMsg";
        }
    }
    protected void btnCancelBrand_onclick(object sender, EventArgs e)
    {
        txtAddBrand.Text = "";
        dvBrandPopUp.Attributes["style"] = "display:none";
    }
    protected void imgBrand_onclick(object sender, ImageClickEventArgs e)
    {
        dvBrandPopUp.Attributes["style"] = "display:block;";
        lblStatusBrand.Text = "";

    }
    protected void imgProduct_onclick(object sender, ImageClickEventArgs e)
     {
         clearproductpopupfields();
    }
    protected void clearproductpopupfields()
    {
        txtProductName.Text = "";
        txtProductValue.Text = "";
        txtMaxDiscPer.Text = "0.00";
        chkActiveProduct.Checked = true;
        dvProductPopUp.Attributes["style"] = "display:block;";
        FillBrand();
        FillCategory();
        DDLCategory.SelectedValue = "0";
        ddlBrand.SelectedValue = "0";
        lblStatusProduct.Text = "";
    }
    protected void gvSaleDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvSaleDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    public decimal invoiceamount = 0,nettotal=0;
    public decimal paidamount = 0,amountpaid=0;
    public decimal balance = 0,bal=0;
    protected void gvSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          
            Label lblinvoiceamount = new Label();
            lblinvoiceamount = (Label)e.Row.FindControl("lblinvoiceamount");
            if (lblinvoiceamount != null)
            {
                if (lblinvoiceamount.Text.Trim() != "" && lblinvoiceamount.Text.Trim() != null)
                {
                    invoiceamount = invoiceamount + Convert.ToDecimal(lblinvoiceamount.Text);
                    nettotal = Convert.ToDecimal(lblinvoiceamount.Text);
                }
            }

            Label lblpaidamount = new Label();
            lblpaidamount = (Label)e.Row.FindControl("lbltotalpaidamount");
            if (lblpaidamount != null)
            {
                if (lblpaidamount.Text.Trim() != "" && lblpaidamount.Text.Trim() != null)
                {
                    paidamount = paidamount + Convert.ToDecimal(lblpaidamount.Text);
                    amountpaid = Convert.ToDecimal(lblpaidamount.Text);
                }
            }
            Label lblbalance = new Label();
            lblbalance = (Label)e.Row.FindControl("lbltotalbalance");
            if (lblbalance != null)
            {
                if (lblbalance.Text.Trim() != "" && lblbalance.Text.Trim() != null)
                {
                    balance = balance + Convert.ToDecimal(lblbalance.Text);
                    bal = Convert.ToDecimal(lblbalance.Text);
                }
            }
            //if (bal == nettotal)
            //{
            //    e.Row.ForeColor = System.Drawing.Color.Yellow;
            //    //string hex = "#FF9980";
            //   // e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(hex);
            //}
             if (bal == 0)
            {
                if (nettotal == 0 && bal == 0)
                    e.Row.BackColor = System.Drawing.Color.White;
                else
                e.Row.BackColor = System.Drawing.Color.Green;
                //e.Row.ForeColor = System.Drawing.Color.White;
            }
           
            else if (bal > 0)
            {
                if (amountpaid == 0)
                    e.Row.BackColor = System.Drawing.Color.Red;
                else
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                //e.Row.ForeColor = System.Drawing.Color.White;
            }
            bal = 0;
            nettotal = 0;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbltotalinvoiceamount = new Label();
            lbltotalinvoiceamount = (Label)e.Row.FindControl("ftlbltotalinvoiceamount");
            lbltotalinvoiceamount.Text = Convert.ToString(invoiceamount);

            Label lbltotalpaidamount = new Label();
            lbltotalpaidamount = (Label)e.Row.FindControl("ftlbltotalpaidamount");
            lbltotalpaidamount.Text = Convert.ToString(paidamount);

            Label lbltotalbalance = new Label();
            lbltotalbalance = (Label)e.Row.FindControl("ftlbltotalbalance");
            lbltotalbalance.Text = Convert.ToString(balance);
        }
    }
    protected void gvSales_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (viewPermission == true && EditPermission == true && deletepermission == false)
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = true;
                    ForTdEdit.Visible = true;
                    ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == true && EditPermission == true && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = true;
                    ForTdEdit.Visible = true;
                    ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == true && EditPermission == false && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = true;
                    ForTdEdit.Visible = false;
                    ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == true && EditPermission == false && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = true;
                    ForTdEdit.Visible = false;
                    ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == false && EditPermission == true && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = false;
                    ForTdEdit.Visible = true;
                    ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == false && EditPermission == true && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = false;
                    ForTdEdit.Visible = true;
                    ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == false && EditPermission == false && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = false;
                    ForTdEdit.Visible = false;
                    ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == false && EditPermission == false && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = false;
                    ForTdEdit.Visible = false;
                    ForTdDelete.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }
    protected void btnCancelOrderLense_click(object sender, EventArgs e)
    {
        try
        {
            dvOrderLense.Attributes["style"] = "display:none";
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true); 
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void txtPrice_change(object sender, EventArgs e)
        {
        try
        {
              GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
             TextBox txtprice = sender as TextBox;
             int rowcount = gvOrderLense.Rows.Count;
             string ID = txtprice.ClientID;
             ID = ID.Replace("ctl00_ContentPlaceHolder1_gvOrderLense_ctl", "");
             ID = ID.Replace("_txtOrderLense", "");
             int maxrow = Convert.ToInt32(ID) - 1;
             DataTable dt = new DataTable();
             if (rowcount == maxrow)
             {
                 
                 dt.Columns.Add("OrderLenseID");
                 dt.Columns.Add("Category");
                 dt.Columns.Add("OrderLense");
                 dt.Columns.Add("Price");
                 dt.Columns.Add("Quantity");
                 dt.Columns.Add("Total");
                 DataRow dr;
                 for (int i = 0; i <= gvOrderLense.Rows.Count - 1; i++)
                 {

                     dr = dt.NewRow();
                     dr["OrderLenseID"] = ((HiddenField)gvOrderLense.Rows[i].FindControl("hdOrderLenseID")).Value;
                     dr["Category"] = ((TextBox)gvOrderLense.Rows[i].FindControl("txtCategory")).Text;
                     dr["OrderLense"] = ((TextBox)gvOrderLense.Rows[i].FindControl("txtOrderLense")).Text;
                     dr["Price"] = ((TextBox)gvOrderLense.Rows[i].FindControl("txtPrice")).Text;
                     dr["Quantity"] = ((TextBox)gvOrderLense.Rows[i].FindControl("txtQuantity")).Text;
                     dr["Total"] = ((TextBox)gvOrderLense.Rows[i].FindControl("txtTotal")).Text;
                     dt.Rows.Add(dr);
                 }
                 dr = dt.NewRow();
                 dr["OrderLenseID"] = "0";
                 dr["Category"] = "";
                 dr["OrderLense"] = "";
                 dr["Price"] = "";
                 dr["Quantity"] = "";
                 dr["Total"] = "";                
                 dt.Rows.Add(dr);
                 orderlensetotal = 0;
                 gvOrderLense.DataSource = dt;
                 gvOrderLense.DataBind();
             }
            
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    float orderlensetotal = 0;
    int o = 0;
 
    protected void gvOrderLense_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if(Convert.ToInt32(dt.Rows[t]["OrderLenseID"])!=0)
                {
                    DropDownList ddl = (DropDownList)e.Row.FindControl("ddlCategoryOrd");
                    ddl.SelectedValue = Convert.ToString(dt.Rows[t]["Category"]);
                }
              TextBox  txttotal = (TextBox)e.Row.FindControl("txtTotal");
              if (txttotal.Text != "")
              {
                  orderlensetotal += Convert.ToSingle(txttotal.Text);
                  e.Row.Enabled = false;
              }
              t++;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                TextBox txt = (TextBox)e.Row.FindControl("lblOrderLenseTotal");
                txt.Text = orderlensetotal.ToString();
                if (txt.Text != "")
                {
                    float tgv = 0;
                    //if (Convert.ToSingle(txttotalgrossvalue.Text) != 0)
                    //{
                    //    tgv = Convert.ToSingle(txttotalgrossvalue.Text);
                    //    tgv += Convert.ToSingle(txt.Text);
                    //    txttotalgrossvalue.Text = tgv.ToString();                        
                    //}
                    //else
                    //{
                    //    tgv += Convert.ToSingle(txt.Text);
                    //    txttotalgrossvalue.Text = tgv.ToString();
                         
                    //}
                    //if(Convert.ToSingle(txtNetValue.Text)!=0)
                    //{
                    // txtNetValue.Text = Convert.ToString(Convert.ToSingle(txtNetValue.Text) + Convert.ToSingle(txt.Text));
                    //}
                    //else
                    //{
                    //    txtNetValue.Text = Convert.ToString(Convert.ToSingle(txt.Text));
                    //}
                }
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void SavePrescription(string result1)
    {
        try
        {
            int rowcount = gvPrescription.Rows.Count;
            string griddata = string.Empty;
            Sale = new ESales();
            ds = new DataSet();
            DropDownList tt; TextBox tx;
            for (int i = 0; i < rowcount; i++)
            {
                tt = (DropDownList)gvPrescription.Rows[i].FindControl("ddlsph");
                if (tt.Text != "")
                    griddata += tt.SelectedValue + "~";
                else
                    griddata += "" + "~";               
                tt = (DropDownList)gvPrescription.Rows[i].FindControl("ddlcyl");
                if (tt.Text != "")
                    griddata += tt.SelectedValue + "~";
                else
                    griddata += "" + "~";
                tt = (DropDownList)gvPrescription.Rows[i].FindControl("ddlaxis");
                if (tt.Text != "")
                    griddata += tt.SelectedValue + "~";
                else
                    griddata += "" + "~";
                tt = (DropDownList)gvPrescription.Rows[i].FindControl("ddladd");
                if (tt.Text != "")
                    griddata += tt.SelectedValue + "~";
                else
                    griddata += "" + "~";
            }
            
            tx = (TextBox)gvPrescription.FooterRow.FindControl("txtsph");
            if (tx.Text != "")
                griddata += tx.Text + "~";
            else
                griddata += "" + "~";

            tx = (TextBox)gvPrescription.FooterRow.FindControl("txtcyl");
            if (tx.Text != "")
                griddata += tx.Text + "~";
            else
                griddata += "" + "~";
            tx = (TextBox)gvPrescription.FooterRow.FindControl("txtaxis");
            if (tx.Text != "")
                griddata += tx.Text + "~";
            else
                griddata += "" + "~";
            tx = (TextBox)gvPrescription.FooterRow.FindControl("txtadd");
            if (tx.Text != "")
                griddata += tx.Text + "~";
            else
                griddata += "" + "~";

            if (griddata != "")
                griddata = griddata.Substring(0, griddata.Length - 1);
            Sale.SalesID = Convert.ToInt32(HDSaleID.Value);
            Sale.GridData = griddata;

            ds = Sale.SavePrescription();
            if (Convert.ToString(ds.Tables[0].Rows[0]["Status"]) == "Success")
            {
                lblStatus.Text = "";
                gvPrescription.Visible = false;
                dvOrderLense.Attributes["style"] = "display:none;";
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true);

            }
            OrderLense();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void Prescription()
    {
        Sale = new ESales();
        ds = new DataSet();
        dt3 = new DataTable();
        Sale.SalesID = Convert.ToInt32(HDSaleID.Value);
        ds = Sale.getPrescription();
        dtsph = new DataTable();
        dtsph.Columns.Add("txt");
        dtsph.Columns.Add("val");
        DataRow dr1; float val=16;
        for (int i = 0; i < 129; i++)
        {
            dr1 = dtsph.NewRow();
            if (val > 0)
            {
                dr1["txt"] = "+" + String.Format("{0:f2}", val);
                dr1["val"] = "+" + String.Format("{0:f2}", val);
            }
            else
            {
                dr1["txt"] = String.Format("{0:f2}", val);
                dr1["val"] = String.Format("{0:f2}", val);
            }
            val =(float) (val - 0.25);
            dtsph.Rows.Add(dr1);
        }

        dtcyl = new DataTable();
        dtcyl.Columns.Add("txt");
        dtcyl.Columns.Add("val");
        float valcyl = 6;
        for (int i = 0; i < 49; i++)
        {
            dr1 = dtcyl.NewRow();
            if (valcyl > 0)
            {
                dr1["txt"] = "+" + String.Format("{0:f2}", valcyl);
                dr1["val"] = "+" + String.Format("{0:f2}", valcyl);
            }
            else
            {
                dr1["txt"] = String.Format("{0:f2}", valcyl);
                dr1["val"] = String.Format("{0:f2}", valcyl);
            }
            valcyl =(float)(valcyl - 0.25);
            dtcyl.Rows.Add(dr1);
        }

        dtaxis = new DataTable();
        dtaxis.Columns.Add("txt");
        dtaxis.Columns.Add("val");
        double valaxis = 180;
        for (int i = 0; i <=180; i++)
        {
            dr1 = dtaxis.NewRow();
            dr1["txt"] = valaxis;
            dr1["val"] = valaxis;
            valaxis = valaxis - 1;
            dtaxis.Rows.Add(dr1);
        }

        dtadd = new DataTable();
        dtadd.Columns.Add("txt");
        dtadd.Columns.Add("val");
        double valadd = 3.50;
        for (int i = 0; valadd>=0.00; i++)
        {
            dr1 = dtadd.NewRow();
            dr1["txt"] = String.Format("{0:f2}", valadd);
            dr1["val"] = String.Format("{0:f2}", valadd);
            valadd = valadd -0.25;
            dtadd.Rows.Add(dr1);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            // dt3.Columns.Add("").SetOrdinal(0);
            dt3 = new DataTable();

            dt3.Columns.Add("SPH");
            dt3.Columns.Add("CYL");
            dt3.Columns.Add("AXIS");
            dt3.Columns.Add("AD");

            dt3.ImportRow(ds.Tables[0].Rows[0]);
            dt3.ImportRow(ds.Tables[1].Rows[0]);
            //dt3.ImportRow(ds.Tables[2].Rows[0]);
            dt1 = new DataTable();
            dt1 = ds.Tables[2];
            //dt3.Columns.Add("");
            dt3.Columns.Add("").SetOrdinal(0);
            dt3.Rows[0][0] = "Right Eye";
            dt3.Rows[1][0] = "Left Eye";
           // dt3.Rows[2][0] = "";
            //for (int k = 0; k < dt3.Rows.Count; k++)
            //{
            //    for (int l = 0; k < dt3.Columns.Count; l++)
            //    { 
            //        dt[3]
            //    }
            //}
        }
        else
        {
            dt3 = new DataTable();
            dt3.Columns.Add("");
            dt3.Columns.Add("SPH");
            dt3.Columns.Add("CYL");
            dt3.Columns.Add("AXIS");
            dt3.Columns.Add("AD");
            DataRow dr;
            for (int i = 0; i < 2; i++)
            {
                dr = dt3.NewRow();
                dt3.Rows.Add(dr);
            }
            dt3.Rows[0][0] = "Right Eye";
            dt3.Rows[1][0] = "Left Eye";
            //dt3.Rows[2][0] = "IPD";
            dt1 = new DataTable();
            dt1 = null;
        }
        m = 0;
        gvPrescription.DataSource = dt3;
        gvPrescription.DataBind();
        gvPrescription.Visible = true;
    }
    int t = 0;
    protected void btnOrderLense_Click(object sender, EventArgs e)
    {

        try
        {
            dvOrderLense.Attributes["style"] = "display:block;overflow-y:auto;";
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "applystyle();", true);
            int saleid = Convert.ToInt32(HDSaleID.Value);
            ESales Sale = new ESales();
            Sale.SalesID = saleid;
            ds = new DataSet();
            ds = Sale.GetOrderLenseGrid();
            t = 0;
            if (ds.Tables[0].Rows.Count == 0)
            {
                 dt = new DataTable();
                dt.Columns.Add("OrderLenseID");
                dt.Columns.Add("Category");
                dt.Columns.Add("OrderLense");
                dt.Columns.Add("Price");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Total");
                DataRow dr;
                for (int i = 0; i < 6; i++)
                {
                    dr = dt.NewRow();
                    dr["OrderLenseID"] = 0;
                    dt.Rows.Add(dr);
                }
                gvOrderLense.DataSource = dt;
                gvOrderLense.DataBind();
                gvOrderLense1.Visible = false;
            }
            else
            {
                DataRow dr;
                if (ds.Tables[0].Rows.Count < 6)
                {
                    for (int i = ds.Tables[0].Rows.Count; i < 6; i++)
                    {
                        dr = ds.Tables[0].NewRow();
                        dr["OrderLenseID"] = 0;
                        ds.Tables[0].Rows.Add(dr);
                    }
                    dt = new DataTable();
                    dt = ds.Tables[0];

                }
                else
                {
                    dr = ds.Tables[0].NewRow();
                    dr["OrderLenseID"] = 0;
                    ds.Tables[0].Rows.Add(dr);

                    dt = new DataTable();
                    dt = ds.Tables[0];
                }
                orderlensetotal = 0;               
                gvOrderLense.DataSource = dt;
                gvOrderLense.DataBind();
              
            }
            //functionality for orderlense 

            //functionality for prescription
            Prescription();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void OrderLense()
    {
        try
        {
           
            int saleid = Convert.ToInt32(HDSaleID.Value);
            ESales Sale = new ESales();
            Sale.SalesID = saleid;
            ds = new DataSet();
            ds = Sale.GetOrderLenseGrid();
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("OrderLenseID");
                dt.Columns.Add("Category");
                dt.Columns.Add("OrderLense");
                dt.Columns.Add("Price");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Total");
                DataRow dr;
                //for (int i = 0; i < 5; i++)
                //{
                //    dr = dt.NewRow();
                //    dr["OrderLenseID"] = 0;
                //    dt.Rows.Add(dr);
                //}
                gvOrderLense1.DataSource = dt;
                gvOrderLense1.DataBind();
                gvOrderLense1.Visible = false;
            }
            else
            {
              
                dt = new DataTable();
                dt = ds.Tables[0];                
                orderlensetotal = 0;                
                gvOrderLense1.DataSource = dt;
                gvOrderLense1.DataBind();
                gvOrderLense1.Visible = true;

            }
            //functionality for orderlense 

            //functionality for prescription
            Prescription1();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void Prescription1()
    {
        try
        {
            
        Sale = new ESales();
        ds = new DataSet();
        dt3 = new DataTable();
        Sale.SalesID = Convert.ToInt32(HDSaleID.Value);
        ds = Sale.getPrescription();        
        if (ds.Tables[0].Rows.Count > 0)
        {
            // dt3.Columns.Add("").SetOrdinal(0);
            dt3 = new DataTable();

            dt3.Columns.Add("SPH");
            dt3.Columns.Add("CYL");
            dt3.Columns.Add("AXIS");
            dt3.Columns.Add("AD");

            dt3.ImportRow(ds.Tables[0].Rows[0]);
            dt3.ImportRow(ds.Tables[1].Rows[0]);
            dt3.ImportRow(ds.Tables[2].Rows[0]);
            dt1 = new DataTable();
            dt1 = ds.Tables[2];
            
            dt3.Columns.Add("").SetOrdinal(0);
            dt3.Rows[0][0] = "Right Eye";
            dt3.Rows[1][0] = "Left Eye";
            dt3.Rows[2][0] = "IPD";
            m = 0;
            gvPrescription1.DataSource = dt3;
            gvPrescription1.DataBind();
            gvPrescription1.Visible = true;
        }
        else
        {
            dt3 = new DataTable();
            dt3.Columns.Add("");
            dt3.Columns.Add("SPH");
            dt3.Columns.Add("CYL");
            dt3.Columns.Add("AXIS");
            dt3.Columns.Add("AD");
            DataRow dr;
            for (int i = 0; i < 3; i++)
            {
                dr = dt3.NewRow();
                dt3.Rows.Add(dr);
            }
            dt3.Rows[0][0] = "Right Eye";
            dt3.Rows[1][0] = "Left Eye";
            dt3.Rows[2][0] = "IPD";
            dt1 = new DataTable();
            dt1 = null;
            m = 0;
            gvPrescription1.DataSource = null;
            gvPrescription1.DataBind();
            gvPrescription1.Visible = false;
        }
     
       
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnSaveOrderLense_Click(object sender, EventArgs e)
    {
        try
        {
            string result = string.Empty;
            ESales sale = new ESales();
            ds = new DataSet();
            sale.SalesID = Convert.ToInt32(HDSaleID.Value);
            string GridData = string.Empty;
            int orderlenseid;
            float gv = 0;
            foreach (GridViewRow gr in gvOrderLense.Rows)
            {
                if (((HiddenField)gr.FindControl("hdOrderLenseID")).Value == "0")
                    if (((DropDownList)gr.FindControl("ddlCategoryOrd")).Text != "0" && ((TextBox)gr.FindControl("txtOrderLense")).Text != ""
                        && ((TextBox)gr.FindControl("txtPrice")).Text != "" && ((TextBox)gr.FindControl("txtQuantity")).Text != "" &&
                        ((TextBox)gr.FindControl("txtTotal")).Text != "")
                    {
                        GridData += ((DropDownList)gr.FindControl("ddlCategoryOrd")).Text + "~" + ((TextBox)gr.FindControl("txtOrderLense")).Text + "~" +
                           ((TextBox)gr.FindControl("txtPrice")).Text + "~" + ((TextBox)gr.FindControl("txtQuantity")).Text
                           + "~" + ((TextBox)gr.FindControl("txtTotal")).Text + "$";
                        //gv += Convert.ToSingle(((TextBox)gr.FindControl("txtTotal")).Text);
                    }
            }
            if (GridData != "")
            {
                GridData = GridData.Substring(0, GridData.Length - 1);
                //if(hdorderlensevalue.Value!="")
                //gv += Convert.ToSingle(hdorderlensevalue.Value);
                //hdorderlensevalue.Value = gv.ToString();

            }
            sale.GridData = GridData;
            ds = sale.SaveOrderLense();
            if (ds.Tables[0].Rows.Count > 0)
            {
               result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                if (result == "Success.")
                {
                    dvSuccess.Visible = true;
                    lblSuccess.Text = result;
                    dvOrderLense.Attributes["style"] = "display:none";
                    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true); 
                }
                else
                {
                    dvFailure.Visible = true;
                    lblStatus.Text = result;
                }
            }
            SavePrescription(result);
        }
        catch (Exception)
        {
            
            throw;
        }
    }

   
    //protected void btnCancelOrderLense_Click1(object sender, EventArgs e)
    //{
    //    dvOrderLense.Attributes["style"] = "display:none";
    //    ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true); 
    //}
    protected void gvOrderLense1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txttotal = (Label)e.Row.FindControl("txtTotal");
                if (txttotal.Text != "")
                    orderlensetotal += Convert.ToSingle(txttotal.Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label txt = (Label)e.Row.FindControl("lblOrderLenseTotal");
                txt.Text = orderlensetotal.ToString();
                if (txt.Text != "")
                {
                    float tgv = 0,tsp=0, tnv = 0;
                    foreach (GridViewRow g in gvSaleDetails.Rows)
                    {
                        TextBox txtsel = (TextBox)g.FindControl("txtSellingPrice");
                        float sp = Convert.ToSingle(txtsel.Text);
                        TextBox txtqty = (TextBox)g.FindControl("txtQuantity");
                        int qty1 = Convert.ToInt32(txtqty.Text);
                        TextBox txtpro = (TextBox)g.FindControl("txtProductValue");
                        float pv = Convert.ToSingle(txtpro.Text);
                        if (txtsel.Text != "" && txtsel.Text != "0.00")
                        {
                            tgv += Convert.ToSingle(qty1 * pv);
                            tsp += Convert.ToSingle(sp);
                            tnv = tsp;
                        }
                    }
                    txttotalgrossvalue.Text = (tgv + orderlensetotal).ToString();
                    txtNetValue.Text = (tnv + orderlensetotal).ToString();
                    hdorderlensevalue.Value = orderlensetotal.ToString();
                    float a=0;
                    if (Convert.ToSingle(txtadvancepaidamount.Text) != 0)
                        a = Convert.ToSingle(txtadvancepaidamount.Text);
                    float pa=0;
                    if(hdpaidamount.Value!="")
                    pa=Convert.ToSingle(hdpaidamount.Value);
                    float bal = 0;
                    bal = tnv + orderlensetotal - pa - a;
                    txtBalance.Text = bal.ToString();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
   
    protected void gvPrescription1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label l = new Label();
            l = (Label)e.Row.FindControl("lblSection");
            l.Text = Convert.ToString(dt3.Rows[m]["Column1"]);
            m++;
        }

    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        ShowTabs(1);
        FillGrid();
    }
}

