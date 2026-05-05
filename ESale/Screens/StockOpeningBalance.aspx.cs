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
using GenCode128;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
//using BarcodeLib;
using System;
using ESaleEntity;

public partial class Screens_StockOpeningBalance : System.Web.UI.Page
{
    EStockOpeningBalance SOB;
    EProduct product;
    DataSet ds;
    string loginid = "";
    DataTable dtCategory, dtBrand;
    DataTable dt;
    ECheckPermission ECPobj;
    static bool addPermission = false;
    static bool viewPermission = true;
    static bool EditPermission = true;
    static bool deletepermission = true;
    string ScreenUrl = string.Empty;
    float totalgrossvalue = 0, totalnetbuyingprice = 0;
    int l = 0;
    int noneditablerows = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScreenUrl = Request.FilePath;
        ScreenUrl = ScreenUrl.Substring(ScreenUrl.LastIndexOf('/') + 1);

        if (Session["LOGINID"] != null)
        {
            if (Session["LOGINID"].ToString() != "1" && Session["LOGINNAME"].ToString() != "admin")
            {
                PCheckPermission();
            }
            else
            {
                viewPermission = true;
                EditPermission = true;
                deletepermission = true;
            }
            if (!IsPostBack)
            {
                ViewState["LoginID"] = Session["LOGINID"].ToString();
                loginid = Session["LOGINID"].ToString();
                FillOrganisation();
                FillStore();
                ShowTabs(1);
                //txtFromDate.Text = System.DateTime.Today.ToString("dd-MM-yyyy");
                // txtToDate.Text = System.DateTime.Today.ToString("dd-MM-yyyy");
                DateTime baseDate = DateTime.Today;
                var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
                string startdate = thisMonthStart.ToString("dd-MM-yyyy");
                txtFromDateSearch.Text = startdate;
                var thisMonthEnd = thisMonthStart.AddMonths(1).Date.AddSeconds(-1);
                string enddate = thisMonthEnd.ToString("dd-MM-yyyy");
                txtToDateSearch.Text = enddate;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
                lblSuccess.Text = "";
                lblStatus.Text = ""; ;
                FillGrid();
            }
            if (grdStoreDeliveryNote.HeaderRow != null)
                grdStoreDeliveryNote.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }
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
                imgClear.Visible = EditPermission;

            }
            else
            {
                imgSave.Visible = EditPermission;
                imgClear.Visible = EditPermission;

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
    protected void FillGrid()
    {
        try
        {
            SOB = new EStockOpeningBalance();
            if (ddlOrganisationSearch.SelectedValue != "0")
                SOB.OrganisationID = Convert.ToInt32(ddlOrganisationSearch.SelectedValue);
            else
                SOB.OrganisationID = 0;
            if (ddlSearchStore.SelectedValue != "0")
                SOB.StoreId = Convert.ToInt32(ddlSearchStore.SelectedValue);
            else
                SOB.StoreId = 0;

            if (txtSOBNOSearch.Text != "")
                SOB.SOBNo = txtSOBNOSearch.Text;
            else
                SOB.SOBNo = "";
            if (txtFromDateSearch.Text != "")
                SOB.FromDate = converttodate(txtFromDateSearch.Text);
            else
                SOB.FromDate = "";
            if (txtToDateSearch.Text != "")
                SOB.ToDate = converttodate(txtToDateSearch.Text);
            else
                SOB.ToDate = "";
            SOB.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            SOB.LoginID = Convert.ToInt32(Session["LoginId"]);
            ds = new DataSet();
            ds = SOB.Grid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdStoreDeliveryNote.DataSource = ds.Tables[0];
                grdStoreDeliveryNote.DataBind();
                lblStatus.Text = "";
                panelSearchSOB.Visible = true;
                grdStoreDeliveryNote.Visible = true;
                txtSOBNOSearch.Text = "";
                grdStoreDeliveryNote.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                grdStoreDeliveryNote.DataSource = null;
                grdStoreDeliveryNote.DataBind();
                dvFailure.Visible = true;
                lblStatus.Text = "No Records found.";
                panelSearchSOB.Visible = true;
                panelAddSOB.Visible = false;
                grdStoreDeliveryNote.Visible = false;   
                //ddlSupplierSearch.SelectedValue = "0";
                txtSOBNOSearch.Text = "";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void FillOrganisation()
    {
        try
        {
            SOB = new EStockOpeningBalance();
            ds = new DataSet();
            ds = SOB.ddlOrganisation();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlOrganisation.DataSource = ds.Tables[0];
                ddlOrganisation.DataTextField = "OrganisationName";
                ddlOrganisation.DataValueField = "OrganisationID";
                ddlOrganisation.DataBind();
                ddlOrganisation.Items.Insert(0, new ListItem("--Any--", "0"));

                ddlOrganisationSearch.DataSource = ds.Tables[0];
                ddlOrganisationSearch.DataTextField = "OrganisationName";
                ddlOrganisationSearch.DataValueField = "OrganisationID";
                ddlOrganisationSearch.DataBind();
                ddlOrganisationSearch.Items.Insert(0, new ListItem("--Any--", "0"));
                if (ds.Tables[0].Rows.Count == 1)
                {
                    ddlOrganisationSearch.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationID"]);
                    ddlOrganisation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationID"]);
                    ddlOrganisation.Enabled = false;
                    ddlOrganisationSearch.Enabled = false;
                }
            }
            else
            {
                ddlOrganisation.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlOrganisationSearch.Items.Insert(0, new ListItem("--Any--", "0"));
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
            SOB = new EStockOpeningBalance();
            ds = new DataSet();
            SOB.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            SOB.LoginID = Convert.ToInt32(Session["LoginId"]);
            ds = SOB.ddlStore();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStore.DataSource = ds.Tables[0];
                ddlStore.DataTextField = "StoreName";
                ddlStore.DataValueField = "StoreID";
                ddlStore.DataBind();
                ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));

                ddlSearchStore.DataSource = ds.Tables[0];
                ddlSearchStore.DataTextField = "StoreName";
                ddlSearchStore.DataValueField = "StoreID";
                ddlSearchStore.DataBind();
                ddlSearchStore.Items.Insert(0, new ListItem("--Any--", "0"));
                if (ds.Tables[0].Rows.Count == 1)
                {
                    ddlSearchStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);
                    ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);
                    ddlStore.Enabled = false;
                    ddlSearchStore.Enabled = false;
                }
            }
            else
            {
                ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlSearchStore.Items.Insert(0, new ListItem("--Any--", "0"));
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ShowTabs(int Num)
    {
          if (Num == 1)
        {
            panelAddSOB.Visible = false;
            panelSearchSOB.Visible = true;
            grdStoreDeliveryNote.Visible = true;
            lnkAdd.Visible = true;
            lnkAdd.Text = "Add";
            tbl.Visible = false;
            FillGrid();
        }
        else if (Num == 2)
        {
            //tbl.Visible = true;
            panelAddSOB.Visible = true;
            gvLineItems.Visible = false;
            tbl.Visible = false;
            panelSearchSOB.Visible = false;
            lnkAdd.Visible = false;
            lnkAdd.Text = "Add";

        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        txtSOBDate.Text = System.DateTime.UtcNow.AddHours(3).ToString("dd-MM-yyyy");
        txtSOBNo.Text = "";
        txtSOBNo.Enabled = true;
       // ddlStore.SelectedValue = "0";
        ShowTabs(2);
        btnSaveDeliveryNote.Visible = true;
        dvsave.Visible = true;
        dvcancel.Visible = true;
        btncancel1.Visible = true;
    }
    protected void ddlBrand_selectedindexchanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddlcat = (DropDownList)gr.FindControl("ddlCategory");
            DropDownList ddlprot = (DropDownList)gr.FindControl("ddlProduct");
            DropDownList ddlBrands = (DropDownList)gr.FindControl("ddlBrand");
            ddlprot.Items.Clear();
            int categoryid = Convert.ToInt32(ddlcat.SelectedValue);
            int brandid = Convert.ToInt32(ddlBrands.SelectedValue);
            if (categoryid != 0)
            {
                SOB = new EStockOpeningBalance();
                ds = new DataSet();
                SOB.CategoryID = categoryid;
                SOB.BrandID = brandid;
                ds = SOB.ddlProducts();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlprot.DataSource = ds.Tables[0];
                    ddlprot.DataTextField = "Productname";
                    ddlprot.DataValueField = "ProductID";
                    ddlprot.DataBind();
                    ddlprot.Items.Insert(0, new ListItem("--Any--", "0"));
                }
                else
                {
                    ddlprot.Items.Insert(0, new ListItem("--Any--", "0"));
                }
            }
            //else
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "CheckFields("+ddlBrands+")", true);
            //}

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlCategory_selectedindexchanged(object sender, EventArgs e)
    {
    }
    protected void btnSaveDeliveryNote_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
              ds=new DataSet();
            SOB=new EStockOpeningBalance();
            if (txtSOBNo.Text != "")
                SOB.SOBNo = txtSOBNo.Text;
            else
                SOB.SOBNo = "";
            if (ddlOrganisation.SelectedValue != "0")
                SOB.OrganisationID = Convert.ToInt32(ddlOrganisation.SelectedValue);
            else
                SOB.OrganisationID = 0;
           
            if (txtSOBDate.Text != "")
                SOB.SOBDate = converttodate(txtSOBDate.Text);
            else
                SOB.SOBDate = "";            
            SOB.LoginID = Convert.ToInt32(Session["LOGINID"]);
            if (ddlStore.SelectedValue != "0")
                SOB.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
            else
                SOB.StoreId = 0;
            ds = SOB.InsertSOB();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0]["Status"]) == "Recored Inserted successfully.")
                {
                    HDStockOpeningBalanceId.Value = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
                    txtSOBNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["SOBNo"]);
                    gvLineItems.Visible = true;
                    AddEmptyRows();
                    EnableControls(false);
                    txttotalbuyingprice.Text = "";
                   
                    txttotalsellingprice.Text = "";                    
                    txtRemarks.Text = "";
                    Session["save"] = "1";
                    //imgSave.Visible = true;
                   // imgSaveDisabled.Visible = false;
                    
                }
                else
                { 
                    
                }
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    void EnableControls(bool status)
    {
        dvcancel.Visible = status;
        btncancel1.Visible = status;
        dvsave.Visible = status;
        btnSaveDeliveryNote.Visible = status;
        ddlOrganisation.Enabled = status;
        txtSOBDate.Enabled = status;
        txtSOBNo.Enabled = status;

    }
    protected void AddEmptyRows()
    {
        try
        {
            dt = new DataTable();
            if (dt.Rows.Count == 0 || dt.Rows.Count < 5)
            {
                dt = new DataTable();
                dt.Columns.Add("StockOpeningBalanceDetailId");
                dt.Columns.Add("CategoryID");
                dt.Columns.Add("BrandID");
                dt.Columns.Add("ProductID");
                dt.Columns.Add("BrandName");
                dt.Columns.Add("ProductName");
                dt.Columns.Add("ProductValue");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("NetProductValue");
                dt.Columns.Add("BuyingPrice");
                dt.Columns.Add("NetBuyingPrice");

            }
            DataRow dr;
            for (int i = dt.Rows.Count; i < 5; i++)
            {
                dr = dt.NewRow();
                dr["StockOpeningBalanceDetailId"] = "0";
                dr["CategoryID"] = "0";
                dr["BrandID"] = "0";
                dr["ProductID"] = "0";
                dr["BrandName"] = "";
                dr["ProductName"] = "";
                dr["Quantity"] = "0";
                dr["ProductValue"] = "0.00";
                dr["NetProductValue"] = "0.00";
                dr["BuyingPrice"] = "0.00";
                dr["NetBuyingPrice"] = "0";
                dt.Rows.Add(dr);
            }
            tbl.Visible = true;
            dvisave.Visible = true;
            imgSave.Visible = true;
            Divcan1.Visible = true;
            btncancelgrid.Visible = true;
           // txttotalgrossvalue.Text = "0.00";
            SOB = new EStockOpeningBalance();
            ds = SOB.ddlCategory();
            dtCategory = ds.Tables[0];
            ds = SOB.ddlBrand();
            dtBrand = ds.Tables[0];
            l = 0;
            gvLineItems.DataSource = dt;
            gvLineItems.DataBind();
            gvLineItems.Visible = true;

            if (deletepermission == false)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        TextBox tt = (TextBox)gvLineItems.Rows[i].FindControl("txtProduct");
                        tt.Focus();
                    }
                    LinkButton im = (LinkButton)gvLineItems.Rows[i].FindControl("imgDeleteRow");
                    im.Visible = false;
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        TextBox tt = (TextBox)gvLineItems.Rows[i].FindControl("txtProduct");
                        tt.Focus();
                    }
                    LinkButton im = (LinkButton)gvLineItems.Rows[i].FindControl("imgDeleteRow");
                    im.Visible = true;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void gvLineItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        if (e.CommandName == "DeleteRow")
        {
            string result = string.Empty;
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int rowcount = gvLineItems.Rows.Count;
            if (rowcount != 1)
            {

                int stockopeningbalancedetailid = Convert.ToInt32(gvLineItems.DataKeys[rowindex].Value);
                if (stockopeningbalancedetailid != 0)
                {
                    SOB = new EStockOpeningBalance();
                    SOB.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
                    SOB.StockOpeningBalanceDetailId = stockopeningbalancedetailid;
                    SOB.LoginID = Convert.ToInt32(Session["LoginId"]);
                    result = SOB.DeleteSOBD();
                    int stockopeningbalanceid = Convert.ToInt32(HDStockOpeningBalanceId.Value);
                    EditFunction(stockopeningbalanceid);
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add("StockOpeningBalanceDetailId");
                    dt.Columns.Add("CategoryID");
                    dt.Columns.Add("BrandID");
                    dt.Columns.Add("ProductID");
                    dt.Columns.Add("ProductValue");
                    dt.Columns.Add("Quantity");
                    dt.Columns.Add("NetProductValue");
                    dt.Columns.Add("BuyingPrice");
                    dt.Columns.Add("NetBuyingPrice");
                    DataRow dr;
                    for (int i = 0; i <= gvLineItems.Rows.Count - 2; i++)
                    {

                        dr = dt.NewRow();
                        dr["StockOpeningBalanceDetailId"] = ((HiddenField)gvLineItems.Rows[i].FindControl("StockOpeningBalanceDetailId")).Value;
                        dr["CategoryID"] = ((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue;
                        if (((DropDownList)gvLineItems.Rows[i].FindControl("ddlProduct")).SelectedValue == "")
                            dr["ProductID"] = "0";
                        else
                            dr["ProductID"] = ((DropDownList)gvLineItems.Rows[i].FindControl("ddlProduct")).SelectedValue;
                        dr["BrandID"] = ((DropDownList)gvLineItems.Rows[i].FindControl("ddlBrand")).SelectedValue;
                        dr["ProductValue"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtProductValue")).Text;
                        dr["Quantity"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text;
                        dr["NetProductValue"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtGrossValue")).Text;
                        dr["BuyingPrice"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtBuyingPrice")).Text;
                        dr["NetBuyingPrice"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtNetBuyingPrice")).Text;
                        dt.Rows.Add(dr);
                    }
                    SOB = new EStockOpeningBalance();
                    ds = SOB.ddlCategory();
                    dtCategory = ds.Tables[0];
                    ds = SOB.ddlBrand();
                    dtBrand = ds.Tables[0];
                    l = 0;
                    gvLineItems.DataSource = dt;
                    gvLineItems.DataBind();
                }
            }
            else
            {
                int stockopeningbalancedetailid = Convert.ToInt32(((HiddenField)gvLineItems.Rows[rowindex].FindControl("StockOpeningBalanceDetailId")).Value);
                SOB = new EStockOpeningBalance();
                SOB.StockOpeningBalanceDetailId = stockopeningbalancedetailid;
                if (stockopeningbalancedetailid != 0)
                    result = SOB.DeleteSOBD();
                AddEmptyRows();
            }


            lblStatus.Text = result;
            //ShowTabs(1);               
        }
    }
    protected void EditFunction(int stockopeningbalanceid)
    {

        SOB = new EStockOpeningBalance();
        ds = new DataSet();
        SOB.StockOpeningBalanceId = stockopeningbalanceid;
        HDStockOpeningBalanceId.Value = stockopeningbalanceid.ToString();
        ds = SOB.GridForViewEdit();
        if (ds.Tables.Count > 0)
        {
            ddlOrganisation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationID"]);
            ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);
            txtSOBDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["StockOpeningBalanceDate"]);
            txtSOBNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["StockOpeningBalanceNo"]);           
            btnSaveDeliveryNote.Visible = false;
            dvsave.Visible = false;
            dvcancel.Visible = false;
            btncancel1.Visible = false;
            txttotalsellingprice.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalSellingPrice"]);
            txttotalbuyingprice.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalBuyingPrice"]);
            txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["Remarks"]);
            ShowTabs(2);
            imgClear.Visible = false;
            imgSave.Visible = true;
            dvisave.Visible = true;
            Divcan1.Visible = true;
            btncancelgrid.Visible = true;
            EnableControls(false);
            if (ds.Tables[1].Rows.Count > 0)
            {
                tbl.Visible = true;
                gvLineItems.Visible = true;
                dt = ds.Tables[1];
                noneditablerows = dt.Rows.Count;
                //dt.Columns.Add("GrossProductValue");

                DataRow dr = dt.NewRow();
                dr["StockOpeningBalanceDetailId"] = "0";
                dr["CategoryID"] = "0";
                dr["BrandID"] = "0";
                dr["ProductID"] = "0";
                dr["BrandName"] = "";
                dr["ProductName"] = "";
                dr["Quantity"] = "0";
                dr["ProductValue"] = "0.00";
                dr["NetProductValue"] = "0.00";
                dr["BuyingPrice"] = "0.00";
                dr["NetBuyingPrice"] = "0";
                dt.Rows.Add(dr);
                ds = SOB.ddlCategory();
                l = 0;
                dtCategory = ds.Tables[0];
                ds = SOB.ddlBrand();
                dtBrand = ds.Tables[0];
                //totalgrossvalue = 0;
                totalnetbuyingprice = 0;
                gvLineItems.DataSource = dt;
                gvLineItems.DataBind();
                //txttotalgrossvalue.Text = totalgrossvalue.ToString();
                ((TextBox)gvLineItems.Rows[gvLineItems.Rows.Count - 1].FindControl("txtProduct")).Focus();

            }
            else
            {
                noneditablerows = 0;
                AddEmptyRows();
                ((TextBox)gvLineItems.Rows[0].FindControl("txtProduct")).Focus();
            }
            if (deletepermission == false)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    LinkButton im = (LinkButton)gvLineItems.Rows[i].FindControl("imgDeleteRow");
                    im.Visible = false;
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    LinkButton im = (LinkButton)gvLineItems.Rows[i].FindControl("imgDeleteRow");
                    im.Visible = true;
                }
            }
           // ControlStatus(false);
        }
    }
    protected void gvLineItems_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvLineItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            TextBox tt, txtprod, txtbrand;
            HiddenField hd, hd1;
            DropDownList ddlproducts, ddlcard, ddlbrand = null;
            float productvalue = 0;
            int qunatity = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddlcard = (DropDownList)e.Row.FindControl("ddlCategory");
                ddlproducts = (DropDownList)e.Row.FindControl("ddlProduct");
                ddlcard.DataSource = dtCategory;
                ddlcard.DataValueField = "CategoryID";
                ddlcard.DataTextField = "CategoryName";
                ddlcard.DataBind();
                ddlcard.Items.Insert(0, new ListItem("--Any--", "0"));

                ddlbrand = (DropDownList)e.Row.FindControl("ddlBrand");
                ddlbrand.DataSource = dtBrand;
                ddlbrand.DataValueField = "BrandID";
                ddlbrand.DataTextField = "BrandName";
                ddlbrand.DataBind();
                ddlbrand.Items.Insert(0, new ListItem("--Any--", "0"));

                 hd = (HiddenField)e.Row.FindControl("StockOpeningBalanceDetailId");
                hd.Value = Convert.ToString(dt.Rows[l]["StockOpeningBalanceDetailId"]);
                if (Convert.ToInt32(dt.Rows[l]["ProductID"]) != 0)
                {

                    ddlcard.SelectedValue = Convert.ToString(dt.Rows[l]["CategoryID"]);
                    int categoryid = Convert.ToInt32(ddlcard.SelectedValue);


                    hd = (HiddenField)e.Row.FindControl("HDBrandID");
                    hd.Value = Convert.ToString(dt.Rows[l]["BrandID"]);
                    hd1 = (HiddenField)e.Row.FindControl("HDProductID");
                    hd1.Value = Convert.ToString(dt.Rows[l]["ProductID"]);
                    txtprod = (TextBox)e.Row.FindControl("txtProduct");
                    txtprod.Text = Convert.ToString(dt.Rows[l]["ProductName"]);
                    txtbrand = (TextBox)e.Row.FindControl("txtBrand");
                    txtbrand.Text = Convert.ToString(dt.Rows[l]["BrandName"]);

                    ddlbrand.SelectedValue = Convert.ToString(dt.Rows[l]["BrandID"]);
                    int brandid = Convert.ToInt32(ddlbrand.SelectedValue);

                    fillproduct(brandid, categoryid, ddlproducts);
                    ddlproducts.SelectedValue = Convert.ToString(dt.Rows[l]["ProductID"]);
                    productvalue = Convert.ToSingle(dt.Rows[l]["ProductValue"]);

                    if (Convert.ToString(dt.Rows[l]["Quantity"]) != "")
                        qunatity = Convert.ToInt32(dt.Rows[l]["Quantity"]);
                    else
                        qunatity = 0;
                    //float grossvalue = (productvalue * qunatity);
                    //tt = (TextBox)e.Row.FindControl("txtGrossValue");
                    //tt.Text = grossvalue.ToString();

                    //totalgrossvalue += grossvalue;
                    totalnetbuyingprice += Convert.ToSingle(dt.Rows[l]["NetBuyingPrice"]);

                    if (Convert.ToInt32(dt.Rows[l]["StockOpeningBalanceDetailId"]) != 0)
                    {
                        ddlproducts.Enabled = false;
                        ddlbrand.Enabled = false;
                        ddlcard.Enabled = false;
                        txtprod.Enabled = false;
                        txtbrand.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtSellingPrice");
                        tt.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtQuantity");
                        tt.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txttotalsellingprice");
                        tt.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtBuyingPrice");
                        tt.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txttotalBuyingprice");
                        tt.Enabled = false;

                    }
                   

                }
                else
                {
                    if (Convert.ToInt32(dt.Rows[l]["StockOpeningBalanceDetailId"]) == 0)
                    {
                        txtprod = (TextBox)e.Row.FindControl("txtProduct");
                        txtbrand = (TextBox)e.Row.FindControl("txtBrand");
                        if (Convert.ToInt32(dt.Rows[l]["CategoryID"]) != 0)
                            ddlcard.SelectedValue = Convert.ToString(dt.Rows[l]["CategoryID"]);
                        if (Convert.ToString(dt.Rows[l]["BrandName"]) != "")
                            txtbrand.Text = Convert.ToString(dt.Rows[l]["BrandName"]);
                        if (Convert.ToString(dt.Rows[l]["ProductName"]) != "")
                            txtprod.Text = Convert.ToString(dt.Rows[l]["ProductName"]);
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
    protected void fillproduct(int brandid, int categoryid, DropDownList ddlprod)
    {
        try
        {
            ds = new DataSet();
            SOB = new EStockOpeningBalance();
            SOB.BrandID = brandid;
            SOB.CategoryID = categoryid;
            ds = SOB.ddlProducts();
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
  
   
   
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddlprod = (DropDownList)gr.FindControl("ddlProduct");
            TextBox prodvalue = (TextBox)gr.FindControl("txtSellingPrice");
            //TextBox defaultdiscount = (TextBox)gr.FindControl("txtDefaultBuyingDiscount");
            SOB = new EStockOpeningBalance();
            if (ddlprod.SelectedValue != "0")
            {
                SOB.ProductID = Convert.ToInt32(ddlprod.SelectedValue);
                ds = new DataSet();
                ds = SOB.ProductValues();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    prodvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                    TextBox qty = (TextBox)gr.FindControl("txtQuantity");
                    qty.Text = "1";
                    TextBox tsp = (TextBox)gr.FindControl("txttotalsellingprice");
                    tsp.Text =Convert.ToString(Convert.ToInt32(qty.Text) * Convert.ToSingle(prodvalue.Text));
                    //defaultdiscount.Text = Convert.ToString(ds.Tables[0].Rows[0]["BuyingDiscount"]);
                    
                }
            }
             TextBox sp = null;
             float sellprice = 0;
            for (int i = 0; i < gvLineItems.Rows.Count; i++)
            {
                 sp = (TextBox)gvLineItems.Rows[i].FindControl("txttotalsellingprice");
                 if (sp.Text != ""||sp.Text!="0.00")
                 {
                     sellprice += Convert.ToSingle(sp.Text);
                 }
            }
            txttotalsellingprice.Text = Convert.ToString(sellprice);
            int rowcount = gvLineItems.Rows.Count;
            ddlprod = sender as DropDownList;
            string ID = ddlprod.ClientID;
            ID = ID.Replace("ctl00_ContentPlaceHolder1_gvLineItems_ctl", "");
            ID = ID.Replace("_ddlProduct", "");
            int maxrow = Convert.ToInt32(ID) - 1;
            if (rowcount == maxrow)
            {
                AddNewRow(prodvalue);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void txtProduct_ontextchanged(object sender, EventArgs e)
    {
        try
        {
          
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddl = (DropDownList)gr.FindControl("ddlCategory");
            TextBox txtproduct = (TextBox)gr.FindControl("txtProduct");
            TextBox txtbrand = (TextBox)gr.FindControl("txtBrand");
            TextBox txt = (TextBox)gr.FindControl("txtQuantity");
            TextBox prodvalue = (TextBox)gr.FindControl("txtSellingPrice");
            HiddenField hdprodid = (HiddenField)gr.FindControl("HDProductID");
            HiddenField hdbrandid = (HiddenField)gr.FindControl("HDBrandID");
            TextBox txtbp = (TextBox)gr.FindControl("txtBuyingPrice");
            ESupplierDeliveryNote SDN = new ESupplierDeliveryNote();
            if (txtproduct.Text != "")
            {
                SDN.ProductName = txtproduct.Text.Trim();
                int categoryid = 0, brandid = 0;
                if (ddl.SelectedValue != "0")
                    categoryid = Convert.ToInt32(ddl.SelectedValue);
                else
                    categoryid = 0;
                SDN.CategoryID = categoryid;
                if (hdbrandid.Value != "" && hdbrandid.Value != "0")
                    brandid = Convert.ToInt32(hdbrandid.Value);
                else
                    brandid = 0;

                if (txtbrand.Text == "")
                    brandid = 0;
                SDN.BrandID = brandid;
                SDN.BrandName = txtbrand.Text.Trim();
                ds = new DataSet();
                if (categoryid != 0 && brandid != 0)
                {
                    ds = SDN.GetProductIDandValue();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                      
                        ddl.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CategoryID"]);
                        hdbrandid.Value = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                        hdprodid.Value = Convert.ToString(ds.Tables[0].Rows[0]["ProductID"]);
                        prodvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                        txtbrand.Text = Convert.ToString(ds.Tables[0].Rows[0]["BrandName"]);
                        txt.Text = "1";
                        //for (int i = 0; i < gvLineItems.Rows.Count; i++)
                        //{
                        //    if (((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" &&
                        //        ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value != ""
                        //        && ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value != "")
                        //    {
                        //        if (i != gr.RowIndex)
                        //        {
                        //            if (((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue == ddl.SelectedValue &&
                        //               ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value == hdbrandid.Value &&
                        //               ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value == hdprodid.Value)
                        //            {
                        //                ddl.SelectedValue = "0";
                        //                txtbrand.Text = "";
                        //                prodvalue.Text = "0.00";
                        //                hdbrandid.Value = "";
                        //                hdprodid.Value = "";
                        //                txtproduct.Text = "";
                        //                txt.Text = "0";
                        //                txtproduct.Focus();
                        //                dvFailure.Visible = true;
                        //                lblStatus.Text = "Line Items must me unique";
                        //                return;
                        //            }
                        //            else {
                        //                dvFailure.Visible = false;
                        //                lblStatus.Text = "";
                        //            }
                        //        }

                        //    }

                        //}
                    }
                    else
                    {
                        hdbrandid.Value = "0";
                        hdprodid.Value = "0";
                        txt.Text = "0";
                        //ddl.SelectedValue = "0";
                       // txtbrand.Text = "";
                        prodvalue.Text = "0.00";
                       // txtproduct.Text = "";
                       // ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('No Stock Avaiable for this Product.');", true);
                       // return;
                    }
                }
                else if (categoryid == 0 || brandid == 0)
                {
                    ds = SDN.GetProductCategorybrandID();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CategoryID"]);
                        hdbrandid.Value = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                        hdprodid.Value = Convert.ToString(ds.Tables[0].Rows[0]["ProductID"]);
                        prodvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                        txtbrand.Text = Convert.ToString(ds.Tables[0].Rows[0]["BrandName"]);
                        txt.Text = "1";
                        //for (int i = 0; i < gvLineItems.Rows.Count; i++)
                        //{
                        //    if (((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" &&
                        //        ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value != ""
                        //        && ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value != "")
                        //    {
                        //        if (i != gr.RowIndex)
                        //        {
                        //            if (((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue == ddl.SelectedValue &&
                        //               ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value == hdbrandid.Value &&
                        //               ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value == hdprodid.Value)
                        //            {
                        //                ddl.SelectedValue = "0";
                        //                txtbrand.Text = "";
                        //                prodvalue.Text = "0.00";
                        //                hdbrandid.Value = "";
                        //                hdprodid.Value = "";
                        //                txtproduct.Text = "";
                        //                txt.Text = "0";
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

                        hdbrandid.Value = "0";
                        hdprodid.Value = "0";
                        txt.Text = "0";
                        //ddl.SelectedValue = "0";
                        //txtbrand.Text = "";
                        prodvalue.Text = "0.00";
                       // txtproduct.Text = "";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('No Stock Avaiable for this Product.');", true);
                        //return;
                    }
                }
                int qty = 0;
                //
                txtbp = (TextBox)gr.FindControl("txtBuyingPrice");
                //txtbp.Text = "0.00";
                TextBox txnbp = (TextBox)gr.FindControl("txttotalBuyingprice");
                txnbp.Text = "0";
                if (txt.Text != "0")
                    qty = Convert.ToInt32(((TextBox)gr.FindControl("txtQuantity")).Text);
                if (qty != 0)
                {
                    ((TextBox)gr.FindControl("txttotalsellingprice")).Text = Convert.ToString(qty * Convert.ToSingle(prodvalue.Text));
                }
            }
            int rowcount = gvLineItems.Rows.Count;
            txtproduct = sender as TextBox;
            string ID = txtproduct.ClientID;
            ID = ID.Replace("ctl00_ContentPlaceHolder1_gvLineItems_ctl", "");
            ID = ID.Replace("_txtProduct", "");
            int gvcount = gvLineItems.Rows.Count - 1;
            int gvrowindex = gr.RowIndex;
            if (gvcount == gvrowindex)
            {
                AddNewRow(txtbp);
            }
            else
            {
                txtbp.Focus();
            }

            //imgSave.Visible = true;
            //imgSaveDisabled.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "hideloader();", true);
        }
        catch (Exception)
        {

            throw;
        }
       
    }
    protected void AddNewRow(TextBox txtbp)
    {
        try
        {
            dt = new DataTable();
            dt.Columns.Add("StockOpeningBalanceDetailId");
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("BrandID");
            dt.Columns.Add("ProductID");
            dt.Columns.Add("BrandName");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ProductValue");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("NetProductValue");
            dt.Columns.Add("BuyingPrice");
            dt.Columns.Add("NetBuyingPrice");

            DataRow dr;
            for (int i = 0; i <= gvLineItems.Rows.Count - 1; i++)
            {

                dr = dt.NewRow();
                dr["StockOpeningBalanceDetailId"] = ((HiddenField)gvLineItems.Rows[i].FindControl("StockOpeningBalanceDetailId")).Value;
                dr["CategoryID"] = ((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue;
                dr["ProductID"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value;
                dr["BrandID"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value;
                dr["BrandName"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtBrand")).Text;
                dr["ProductName"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtProduct")).Text;
                dr["ProductValue"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtSellingPrice")).Text;                
                dr["Quantity"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text;
                dr["NetProductValue"] = ((TextBox)gvLineItems.Rows[i].FindControl("txttotalsellingprice")).Text;
                dr["BuyingPrice"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtBuyingPrice")).Text;
                dr["NetBuyingPrice"] = ((TextBox)gvLineItems.Rows[i].FindControl("txttotalBuyingprice")).Text;
                dt.Rows.Add(dr);
            }
            dr = dt.NewRow();

            dr["StockOpeningBalanceDetailId"] = "0";
            dr["CategoryID"] = "0";
            dr["BrandID"] = "0";
            dr["ProductID"] = "0";
            dr["BrandName"] = "";
            dr["ProductName"] = "";  
            dr["Quantity"] = "0";
            dr["ProductValue"] = "0.00";
            dr["NetProductValue"] = "0.00";
            dr["BuyingPrice"] = "0.00";
            dr["NetBuyingPrice"] = "0";
            
            dt.Rows.Add(dr);
            SOB = new EStockOpeningBalance();
            ds = SOB.ddlCategory();
            dtCategory = ds.Tables[0];
            ds = SOB.ddlBrand();
            dtBrand = ds.Tables[0];
            l = 0;
            gvLineItems.DataSource = dt;
            gvLineItems.DataBind();
            txtbp.Focus();
            //for (int i = 0; i < noneditablerows; i++)
            //    gvSDND.Rows[i].Enabled = false;
            ((TextBox)(gvLineItems.Rows[gvLineItems.Rows.Count - 2].FindControl("txtBuyingPrice"))).Focus();
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
        try
        {

            SOB = new EStockOpeningBalance();
            string GridData = string.Empty;
            int supplierdeliverynotedetailid = 0;
           // ControlStatus(true);
            for (int i = 0; i < gvLineItems.Rows.Count; i++)
            {
                if (((HiddenField)gvLineItems.Rows[i].FindControl("StockOpeningBalanceDetailId")).Value != "")
                {
                    supplierdeliverynotedetailid = Convert.ToInt32(((HiddenField)gvLineItems.Rows[i].FindControl("StockOpeningBalanceDetailId")).Value);
                    if (supplierdeliverynotedetailid == 0)
                    {
                        if (((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" && (((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value != "0" ||
                           ((TextBox)gvLineItems.Rows[i].FindControl("txtBrand")).Text != "")
                            && (((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value != "0" || ((TextBox)gvLineItems.Rows[i].FindControl("txtProduct")).Text != "")
                            && ((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text != "0")
                        {
                            if (((TextBox)gvLineItems.Rows[i].FindControl("txtSellingPrice")).Text == "")
                                ((TextBox)gvLineItems.Rows[i].FindControl("txtSellingPrice")).Text = "0";
                            if (((TextBox)gvLineItems.Rows[i].FindControl("txtBuyingPrice")).Text == "")
                                ((TextBox)gvLineItems.Rows[i].FindControl("txtBuyingPrice")).Text = "0";
                            GridData += ((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue + "~"
                                 + ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value + "~"
                                + ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value + "~"
                                + ((TextBox)gvLineItems.Rows[i].FindControl("txtSellingPrice")).Text + "~"
                                + ((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text + "~"
                                + ((TextBox)gvLineItems.Rows[i].FindControl("txtBuyingPrice")).Text + "~"
                                 + ((TextBox)gvLineItems.Rows[i].FindControl("txtBrand")).Text + "~"
                                  + ((TextBox)gvLineItems.Rows[i].FindControl("txtProduct")).Text + "$";
                            //+ ((TextBox)gvLineItems.Rows[i].FindControl("txtNetBuyingPrice")).Text + "$";
                            if (((TextBox)gvLineItems.Rows[i].FindControl("txtProduct")).Text != "")
                            {
                                string filename = ((TextBox)gvLineItems.Rows[i].FindControl("txtProduct")).Text + ".png";
                                if (!File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                                {
                                    TextBox tt = (TextBox)gvLineItems.Rows[i].FindControl("txtProduct");
                                    System.Drawing.Image img = Code128Rendering.MakeBarcodeImage(tt.Text, 2, true);
                                    if (File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                                    {
                                        File.Delete(Server.MapPath("~/images/BarCodeImages/" + filename));
                                    }
                                    string path = Server.MapPath("~/images/BarCodeImages");
                                    img.Save(path + "/" + filename, ImageFormat.Png);
                                }
                            }
                        }
                    }
                }
            }
            if (GridData != "")
            {
                GridData = GridData.Substring(0, GridData.Length - 1);
                SOB.GridData = GridData;
                if (txttotalbuyingprice.Text != "")
                    SOB.TotalBuyingPrice = Convert.ToDecimal(txttotalbuyingprice.Text);
                else
                    SOB.TotalBuyingPrice = 0;
               
                if (txttotalsellingprice.Text != "")
                {
                    SOB.TotalSellingPrice = Convert.ToDecimal(txttotalsellingprice.Text);
                }
                else
                    SOB.TotalSellingPrice = 0;
                SOB.Remarks = txtRemarks.Text;
                SOB.StockOpeningBalanceId = Convert.ToInt32(HDStockOpeningBalanceId.Value);
                SOB.LoginID = Convert.ToInt32(Session["LOGINID"]);
                SOB.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
                SOB.OrganisationID = Convert.ToInt32(ddlOrganisation.SelectedValue);
                string result =SOB.InsertSOBD();
                if (result == "Record Updated Successfully.")
                {
                    ShowTabs(1);
                    FillGrid();
                    dvSuccess.Visible = true;
                    lblSuccess.Text = result;
                    int StoreReturnId = Convert.ToInt32(HDStockOpeningBalanceId.Value);
                    if (StoreReturnId != 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Printalert", "StoreForPrint(" + StoreReturnId + ")", true);

                    } 
                }
                else
                {
                    lblStatus.Text = result;
                }
            }
            

        }
        catch (Exception)
        {

            throw;
        }
    }
    [WebMethod]
    public static GridDataSet StoreForPrint(int StoreReturnId)
    {

        DataSet dsforstore = new DataSet();
        EStockOpeningBalance Store = new EStockOpeningBalance();
        EStoreDeliveryNote estore = new EStoreDeliveryNote();
        EStoreDeliveryList liststore = new EStoreDeliveryList();
        List<EStoreDeliveryNote> estorelist = new List<EStoreDeliveryNote>();
        List<EStoreDeliveryList> estoretable = new List<EStoreDeliveryList>();
        GridDataSet ObjGridData = new GridDataSet();
        Store.StockOpeningBalanceId = StoreReturnId;
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
                    estore.DeliveryNoteNo = dsforSDN.Tables[1].Rows[0]["StockOpeningBalanceNo"].ToString();
                    estore.DeliveryNoteDate = dsforSDN.Tables[1].Rows[0]["StockOpeningBalanceDate"].ToString();
                    estore.Remarks = dsforSDN.Tables[1].Rows[0]["Remarks"].ToString();

                    estore.TotalGrossValue = Convert.ToDecimal(dsforSDN.Tables[1].Rows[0]["TotalSellingPrice"]);
                    estore.TotalValue = Convert.ToDecimal(dsforSDN.Tables[1].Rows[0]["TotalBuyingPrice"]);

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
                liststore.ProductValue = Convert.ToSingle(dsforSDN.Tables[2].Rows[i]["ProductValue"]);
                liststore.Quantity = Convert.ToSingle(dsforSDN.Tables[2].Rows[i]["Quantity"]);
                liststore.GrossValue = Convert.ToSingle(dsforSDN.Tables[2].Rows[i]["TotalSellingPrice"]);
                liststore.BuyingPrice = Convert.ToSingle(dsforSDN.Tables[2].Rows[i]["BuyingPrice"]);
                liststore.TotalBuyingPrice = Convert.ToSingle(dsforSDN.Tables[2].Rows[i]["TotalBuyingPrice"]);
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
    protected void lnkbtnPrint_Click(object sender, EventArgs e)
    {
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlOrganisationSearch.SelectedIndex = 0;
        ddlSearchStore.SelectedIndex = 0;
        txtSOBNOSearch.Text = "";
        txtFromDateSearch.Text = "";
        txtToDateSearch.Text = "";
        
        FillGrid();
    }
    protected void searchDeliveryNote_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        FillGrid();
    }
    protected void grdStoreDeliveryNote_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdStoreDeliveryNote_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        if (e.CommandName == "View")
        {
            noneditablerows = 0;
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int stockopeningbalanceid = Convert.ToInt32(grdStoreDeliveryNote.DataKeys[rowindex].Value);
            SOB = new EStockOpeningBalance();
            ds = new DataSet();
            SOB.StockOpeningBalanceId = stockopeningbalanceid;
            ds = SOB.GridForViewEdit();
            if (ds.Tables.Count > 0)
            {
                
                ddlOrganisation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationID"]);
                ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);
                txtSOBDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["StockOpeningBalanceDate"]);
                txtSOBNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["StockOpeningBalanceNo"]);
                //txtPaymentDueDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["PaymentDueDate"]);
                btnSaveDeliveryNote.Visible = false;
                dvsave.Visible = false;
                dvcancel.Visible = false;
                btncancel1.Visible = false;
                //txtHandlingCharges.Text = Convert.ToString(ds.Tables[0].Rows[0]["HandlingCharges"]);
                txttotalbuyingprice.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalBuyingPrice"]);
                txttotalsellingprice.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalSellingPrice"]);
                txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["Remarks"]);
                ShowTabs(2);
                EnableControls(false);
                imgClear.Visible = false;
                imgSave.Visible = false;
                dvisave.Visible = false;
                Divcan1.Visible = true;
                btncancelgrid.Visible = true;
                if (ds.Tables[1].Rows.Count > 0)
                {
                    noneditablerows = ds.Tables[1].Rows.Count;
                    tbl.Visible = true;
                    gvLineItems.Visible = true;
                    dt = ds.Tables[1];
                    //dt.Columns.Add("GrossProductValue");

                    ds = SOB.ddlCategory();
                    l = 0;
                    dtCategory = ds.Tables[0];
                    ds = SOB.ddlBrand();
                    dtBrand = ds.Tables[0];
                    totalgrossvalue = 0;
                    totalnetbuyingprice = 0;
                    gvLineItems.DataSource = dt;
                    gvLineItems.DataBind();
                   // txttotalgrossvalue.Text = totalgrossvalue.ToString();
                    //txttotaldiscountvalue.Text = totaldiscount.ToString();
                    txtRemarks.ReadOnly = true;
                    imgSave.Visible = false;
                    Divcan1.Visible = true;
                    dvisave.Visible = false;
                    btncancelgrid.Visible = true;
                    //txtHandlingCharges.ReadOnly = true;

                    for (int i = 0; i < noneditablerows; i++)
                    {
                        LinkButton im = (LinkButton)gvLineItems.Rows[i].FindControl("imgDeleteRow");
                        im.Visible = false;
                    }

                }
                else
                {
                    //gvSDND.Attributes["style"] = "display:none";
                    //tbl.Attributes["style"] = "display:none";

                    AddEmptyRows();
                    imgSave.Visible = false;
                    Divcan1.Visible = true;
                    dvisave.Visible = false;
                    btncancelgrid.Visible = true;
                }
                //ControlStatus(false);
            }
           // lnkAdd.Text = "View";
        }
        else if (e.CommandName == "EditRow")
        {
           // Session["save"] = "1";
          //  txtHandlingCharges.ReadOnly = false;
            txtRemarks.ReadOnly = false;
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int stockopeningbalanceid = Convert.ToInt32(grdStoreDeliveryNote.DataKeys[rowindex].Value);
            EditFunction(stockopeningbalanceid);
            //lnkAdd.Text = "Edit";
            imgSave.Visible = true;
            btncancelgrid.Visible = true;
            imgSaveDisabled.Visible = false;
        }
        else if (e.CommandName == "Deleting")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int stockopeningbalanceid = Convert.ToInt32(grdStoreDeliveryNote.DataKeys[rowindex].Value);
            SOB = new EStockOpeningBalance ();
            ds = new DataSet();
            SOB.StockOpeningBalanceId = stockopeningbalanceid;
            SOB.LoginID = Convert.ToInt32(Session["LoginId"]);
            string result = SOB.DeleteSOB();
            ShowTabs(1);
            FillGrid();
            dvSuccess.Visible = true;
            lblSuccess.Text = result;
            //lblStatus.Text = result;
        }
        else if (e.CommandName == "Print")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);
            int stockopeningbalanceid = Convert.ToInt32(grdStoreDeliveryNote.DataKeys[rowindex].Value);
            SOB = new EStockOpeningBalance();
            ds = new DataSet();
            SOB.StockOpeningBalanceId = stockopeningbalanceid;
            try
            {
                SOB.LoginID = Convert.ToInt32(Session["LOGINID"].ToString());
                SOB.StockOpeningBalanceId = stockopeningbalanceid;

                if (stockopeningbalanceid != 0)
                {

                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "StoreForPrint(" + stockopeningbalanceid + ");", true);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
    protected void grdStoreDeliveryNote_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdStoreDeliveryNote_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void grdStoreDeliveryNote_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (viewPermission == true && EditPermission == true && deletepermission == true)
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = true;
                    ForTdEdit.Visible = true;
                    ForTdDelete.Visible = true;
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
            else if (viewPermission == true && EditPermission == false && deletepermission == true)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = true;
                    ForTdEdit.Visible = false;
                    ForTdDelete.Visible = true;
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
            else if (viewPermission == false && EditPermission == true && deletepermission == true)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = false;
                    ForTdEdit.Visible = true;
                    ForTdDelete.Visible = true;
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
            else if (viewPermission == false && EditPermission == false && deletepermission == true)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = false;
                    ForTdEdit.Visible = false;
                    ForTdDelete.Visible = true;
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
    protected void grdStoreDeliveryNote_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}