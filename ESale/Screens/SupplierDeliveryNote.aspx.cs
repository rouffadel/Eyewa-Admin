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
using GenCode128;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
//using BarcodeLib;

public partial class Screens_SupplierDeliveryNote : System.Web.UI.Page
{
    int l = 0;
    EStoreDeliveryNote Store;
    DataSet dsforSDN;
    ESupplierDeliveryNote SDN;
    EProduct product; 
    DataSet ds;
    string loginid = "";
    DataTable dtCategory,dtBrand;
    DataTable dt;
    ECheckPermission ECPobj;
    static bool addPermission = false;
    static bool viewPermission = true;
    static bool EditPermission = true;
    static bool deletepermission = true;
    string ScreenUrl = string.Empty;
    float totalgrossvalue = 0, totalnetbuyingprice=0;
    int noneditablerows;
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
                FillSupplier();
                FillStore();
                ShowTabs(1);
                //txtFromDate.Text = System.DateTime.Today.ToString("dd-MM-yyyy");
               // txtToDate.Text = System.DateTime.Today.ToString("dd-MM-yyyy");
                DateTime baseDate = DateTime.UtcNow.AddHours(3);
                var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
                string startdate = thisMonthStart.ToString("dd-MM-yyyy");
                txtFromDate.Text = startdate;
                var thisMonthEnd = thisMonthStart.AddMonths(1).Date.AddSeconds(-1);
                string enddate = thisMonthEnd.ToString("dd-MM-yyyy");
                txtToDate.Text = enddate;
                lblStatus.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
                FillGrid();
            }
            if (gvSDN.HeaderRow != null)
                gvSDN.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }
       
    }

    private void FillSupplier()
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            SDN = new ESupplierDeliveryNote();
            ds = new DataSet();
            ds = SDN.ddlSupplier();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSupplier.DataSource = ds.Tables[0];
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataValueField = "SupplierID";
                ddlSupplier.DataBind();
                ddlSupplier.Items.Insert(0, new ListItem("--Any--", "0"));

                ddlSupplierSearch.DataSource = ds.Tables[0];
                ddlSupplierSearch.DataTextField = "SupplierName";
                ddlSupplierSearch.DataValueField = "SupplierID";
                ddlSupplierSearch.DataBind();
                ddlSupplierSearch.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                ddlSupplier.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlSupplierSearch.Items.Insert(0, new ListItem("--Any--", "0"));
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    public void FillStore()
    {
        dsforSDN = new DataSet();
        Store = new EStoreDeliveryNote();
        try
        {
            Store.OrganisationID = Convert.ToInt32(1);
            Store.LoginID = Convert.ToInt32(Session["LOGINID"]);
            dsforSDN = Store.ddlStore();
            if (dsforSDN.Tables[0].Rows.Count > 0)
            {
               
                ddlStore.DataSource = dsforSDN;
                ddlStore.DataValueField = "StoreID";
                ddlStore.DataTextField = "StoreName";
                ddlStore.DataBind();
                ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlStore.SelectedValue = Convert.ToString(7);
                ddlStore.Enabled = false;

            }
          

        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    private void FillOrganisation()
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            SDN = new ESupplierDeliveryNote();
            ds = new DataSet();
            ds = SDN.ddlOrganisation();
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
    protected void gvSDN_RowCreated(object sender, GridViewRowEventArgs e)
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
   
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        btnSaveDeliveryNote.Enabled = true;
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ShowTabs(2);
        gvSDND.Visible = false;
        dvisave.Visible = false;
        dvClear.Visible = false;
        imgSave.Visible = false;
        imgClear.Visible = false;
        tbl.Visible = false;
        txtDeliveryNoteDate.Text = System.DateTime.UtcNow.AddHours(3).ToString("dd-MM-yyyy");
        DateTime dt = DateTime.Now;
        dt.AddDays(15);
        txtPaymentDueDate.Text = DateTime.Today.AddDays(15).ToString("dd-MM-yyyy");
        btnSaveDeliveryNote.Visible = true;
        btncancel1.Visible = true;
        dvcancel.Visible = true;
        EnableControls(true);
        lblStatus.Text = "";
        ddlSupplier.SelectedValue = "0";
        txtDeliveryNoteNo.Text = "";
    }
    void ShowTabs(int TabNum)
    {
        if (TabNum == 1)
        {
            pnlAdd.Visible = false;
            pnlSearch.Visible = true;
            //pnlSearchGrid.Visible = false;
            ddlSupplierSearch.SelectedIndex = 0;
            lnkAdd.Visible = true;
            lnkAdd.Text = "Add";
            tbl.Visible = false;
          //  FillGrid();
        }
        else if (TabNum == 2)
        {
            pnlAdd.Visible = true;
            pnlSearch.Visible = false;
            pnlSearchGrid.Visible = false;
            lnkAdd.Text = "Add";
            lnkAdd.Visible = false;
        }
    }
    void ClearControls()
    {
        //DDLCategory.SelectedValue = "0";
        //txtDefaultSellingDiscount.Text = string.Empty;
        //txtProductName.Text = string.Empty;
        //txtProductValue.Text = string.Empty;
        // EnableControls(true);

    }
    void EnableControls(bool status)
    {
        btnSaveDeliveryNote.Visible = status;
        btncancel1.Visible = status;
        dvcancel.Visible = status;
        ddlOrganisation.Enabled = status;
        ddlSupplier.Enabled = status;
        
        txtDeliveryNoteDate.Enabled = status;
        txtPaymentDueDate.Enabled = status;

    }
    void showButtons(string Mode)
    {
        if (string.Compare(Mode, "Search", true) == 0)
        {
            imgClear.Visible = true;
            imgSave.Visible = false;
            imgupdate.Visible = false;
            dvClear.Visible = true;
            dvisave.Visible = false;
            dvupdate.Visible = false;
            Divcan1.Visible = false;
            btncancelgrid.Visible = false;
        }
        else if (string.Compare(Mode, "View", true) == 0)
        {
            dvClear.Visible = false;
            dvisave.Visible = false;
            dvupdate.Visible = false;
            imgClear.Visible = false;
            imgSave.Visible = false;
            imgupdate.Visible = false;
            Divcan1.Visible = true;
            btncancelgrid.Visible = true;
        }
        else if (string.Compare(Mode, "Edit", true) == 0)
        {
            imgClear.Visible = true;
            imgSave.Visible = false;
            imgupdate.Visible = true;
            dvClear.Visible = true;
            dvisave.Visible = false;
            dvupdate.Visible = true;
            Divcan1.Visible = true;
            btncancelgrid.Visible = true;
        }
        else if (string.Compare(Mode, "Add", true) == 0)
        {
             dvClear.Visible = true;
             dvisave.Visible = true;
             dvupdate.Visible = false;
            Divcan1.Visible = true;
            btncancelgrid.Visible = true;
            imgClear.Visible = true;
            imgSave.Visible = true;
            imgupdate.Visible = false;
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
    protected void btnSaveDeliveryNote_Click(object sender, EventArgs e)
    {
        btnSaveDeliveryNote.Enabled = false;
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            ds=new DataSet();
            SDN=new ESupplierDeliveryNote();
            string result = SDN.GetID();
            string suplier = Convert.ToString(ddlSupplier.SelectedItem);
            if (suplier.Length > 4)
                suplier = suplier.Substring(0, 4);
            else
                suplier = suplier.Substring(0, suplier.Length);
            txtDeliveryNoteNo.Text=suplier + "-" + DateTime.Now.ToString("ddMMyyyy")+"-"+result;
            if (ddlOrganisation.SelectedValue != "0")
                SDN.OrganisationID = Convert.ToInt32(ddlOrganisation.SelectedValue);
            else
                SDN.OrganisationID = 0;
            if (ddlSupplier.SelectedValue != "0")
                SDN.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
            else
                SDN.SupplierID = 0;
            if (txtDeliveryNoteDate.Text != "")
                SDN.SDNDate = converttodate(txtDeliveryNoteDate.Text);
            else
                SDN.SDNDate = "";
            if (txtDeliveryNoteNo.Text != "")
                SDN.SDNNo = txtDeliveryNoteNo.Text;             
            if (txtPaymentDueDate.Text != "")
                SDN.PaymentDueDate = converttodate(txtPaymentDueDate.Text);
            else
                SDN.PaymentDueDate = "";
            SDN.LoginID = Convert.ToInt32(Session["LOGINID"]);
            ds = SDN.InsertSDN();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lblStatus.Text = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
                txtDeliveryNoteNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["DeliveryNoteNo"]);
                HDSupplierDeliveryNoteID.Value = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
                gvSDND.Visible = true;
                AddEmptyRows();
                EnableControls(false);
                txttotalgrossvalue.Text = "";
               // txttotaldiscountvalue.Text = "";
                txtTotalPrice.Text = "";
                txtHandlingCharges.Text = "";
                txtTotalProductValue.Text = "";
                txtRemarks.Text = "";
                Session["save"] = "1";
                imgSave.Visible = true;
                dvisave.Visible = true;
                Divcan1.Visible = true;
                btncancelgrid.Visible = true;
                imgSaveDisabled.Visible = false;
                dvSaveDisabled.Visible = false;
                dvClear.Visible = false;
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
            dt.Columns.Add("SupplierDeliveryNoteDetailID");
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("BrandID");
            dt.Columns.Add("ProductID");
            dt.Columns.Add("BrandName");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ProductValue");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("GrossProductValue");
            dt.Columns.Add("BuyingPrice");
            dt.Columns.Add("NetBuyingPrice");  
           
        }
        DataRow dr;
        for (int i = dt.Rows.Count; i < 5; i++)
        {
            dr = dt.NewRow();
            dr["SupplierDeliveryNoteDetailID"] = "0";
            dr["CategoryID"] = "0";
            dr["BrandID"] = "0";
            dr["ProductID"] = "0";
            dr["BrandName"] = "";
            dr["ProductName"] = "";
            dr["Quantity"] = "0";
            dr["ProductValue"] = "0.00";
            dr["GrossProductValue"] = "0.00";
            dr["BuyingPrice"] = "0.00";
            dr["NetBuyingPrice"] = "0";
            dt.Rows.Add(dr);
        }
        tbl.Visible = true;
        imgSave.Visible = true;
        txttotalgrossvalue.Text = "0.00";
        SDN = new ESupplierDeliveryNote();
        ds = SDN.ddlCategory();
        dtCategory = ds.Tables[0];
        ds = SDN.ddlBrand();
        dtBrand = ds.Tables[0];
        l = 0;
        gvSDND.DataSource = dt;
        gvSDND.DataBind();
        gvSDND.Visible = true;
       
        if (deletepermission == false)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    TextBox tt = (TextBox)gvSDND.Rows[i].FindControl("txtProduct");
                    tt.Focus();
                }
                LinkButton im = (LinkButton)gvSDND.Rows[i].FindControl("imgDeleteRow");
                im.Visible = false;
            }
        }
        else
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    TextBox tt = (TextBox)gvSDND.Rows[i].FindControl("txtProduct");
                    tt.Focus();
                }
                LinkButton im = (LinkButton)gvSDND.Rows[i].FindControl("imgDeleteRow");
                im.Visible = true;
            }
        }
        }
        catch (Exception)
        {
            
            throw;
        }
        
    }
   
    protected void ddlProduct_selectedindexchanged(object sender, EventArgs e)
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
            //TextBox defaultdiscount = (TextBox)gr.FindControl("txtDefaultBuyingDiscount");
            SDN = new ESupplierDeliveryNote();
            if (ddlprod.SelectedValue != "0")
            {
                SDN.ProductID = Convert.ToInt32(ddlprod.SelectedValue);
                ds = new DataSet();
                ds = SDN.ProductValues();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    prodvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                    //defaultdiscount.Text = Convert.ToString(ds.Tables[0].Rows[0]["BuyingDiscount"]);
                }
            }
            int rowcount = gvSDND.Rows.Count;
            ddlprod = sender as DropDownList;
            string ID = ddlprod.ClientID;
            ID = ID.Replace("ctl00_ContentPlaceHolder1_gvSDND_ctl", "");
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
            //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "loader();", true);
             GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
             DropDownList ddl = (DropDownList)gr.FindControl("ddlCategory");
             TextBox txtproduct = (TextBox)gr.FindControl("txtProduct");
             TextBox txtbrand = (TextBox)gr.FindControl("txtBrand");
             TextBox txt = (TextBox)gr.FindControl("txtQuantity");
             TextBox prodvalue = (TextBox)gr.FindControl("txtProductValue");
             HiddenField hdprodid = (HiddenField)gr.FindControl("HDProductID");
             HiddenField hdbrandid = (HiddenField)gr.FindControl("HDBrandID");
             TextBox txtbp = (TextBox)gr.FindControl("txtBuyingPrice");
             SDN = new ESupplierDeliveryNote();
             if (txtproduct.Text != "")
             {
                 SDN.ProductName = txtproduct.Text.Trim();
                 int categoryid = 0, brandid = 0;
                 if(ddl.SelectedValue!="0")
                     categoryid=Convert.ToInt32(ddl.SelectedValue);
                 else
                     categoryid=0;
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
                         prodvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                         hdbrandid.Value = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                         hdprodid.Value = Convert.ToString(ds.Tables[0].Rows[0]["ProductID"]);
                        //txtbrand.Text = Convert.ToString(ds.Tables[0].Rows[0]["BrandName"]);
                        //hdbrandid.Value = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                        //ddl.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CategoryID"]);
                        //hdbrandid.Value = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                        //hdprodid.Value = Convert.ToString(ds.Tables[0].Rows[0]["ProductID"]);
                        txt.Text = "1";
                         //for (int i = 0; i < gvSDND.Rows.Count; i++)
                         //{

                         //    if (((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" &&
                         //        ((HiddenField)gvSDND.Rows[i].FindControl("HDBrandID")).Value != ""
                         //        && ((HiddenField)gvSDND.Rows[i].FindControl("HDProductID")).Value != "")
                         //    {
                         //        if (i != gr.RowIndex)
                         //        {
                         //            if (((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue == ddl.SelectedValue &&
                         //               ((HiddenField)gvSDND.Rows[i].FindControl("HDBrandID")).Value == hdbrandid.Value &&
                         //               ((HiddenField)gvSDND.Rows[i].FindControl("HDProductID")).Value == hdprodid.Value)
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
                         ds = new DataSet();
                         if (categoryid != 0)
                             ddl.SelectedValue = categoryid.ToString();
                         else
                             ddl.SelectedValue = "0";
                         //if(txtbrand.Text!="")
                         //txtbrand.Text = "";
                         prodvalue.Text = "0.00";
                         if (brandid != 0)
                             hdbrandid.Value = brandid.ToString();
                         else
                             hdbrandid.Value = "";
                         hdprodid.Value = "";
                         txt.Text = "0";
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
                         //for (int i = 0; i < gvSDND.Rows.Count; i++)
                         //{

                         //    if (((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" &&
                         //        ((HiddenField)gvSDND.Rows[i].FindControl("HDBrandID")).Value != ""
                         //        && ((HiddenField)gvSDND.Rows[i].FindControl("HDProductID")).Value != "")
                         //    {
                         //        if (i != gr.RowIndex)
                         //        {
                         //            if (((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue == ddl.SelectedValue &&
                         //               ((HiddenField)gvSDND.Rows[i].FindControl("HDBrandID")).Value == hdbrandid.Value &&
                         //               ((HiddenField)gvSDND.Rows[i].FindControl("HDProductID")).Value == hdprodid.Value)
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
                         
                         hdbrandid.Value = "-1";
                         hdprodid.Value = "-1";                         
                         //txt.Text = "0";
                         //ddl.SelectedValue = "0";
                         //txtbrand.Text = "";
                         //prodvalue.Text = "0.00";
                     }
                 }                 
                 int qty=0;
                 //
                  txtbp = (TextBox)gr.FindControl("txtBuyingPrice");
                 //txtbp.Text = " ";
                 TextBox txnbp = (TextBox)gr.FindControl("txtNetBuyingPrice");
                 txnbp.Text = "0";
                 if(txt.Text!="0")
                     qty= Convert.ToInt32(((TextBox)gr.FindControl("txtQuantity")).Text);
                 if (qty != 0)
                 {
                     ((TextBox)gr.FindControl("txtGrossValue")).Text = Convert.ToString(qty * Convert.ToSingle(prodvalue.Text));
                 }
             }
             int rowcount = gvSDND.Rows.Count;
             txtproduct = sender as TextBox;
             string ID = txtproduct.ClientID;
             ID = ID.Replace("ctl00_ContentPlaceHolder1_gvSDND_ctl", "");
             ID = ID.Replace("_txtProduct", "");
         //    int maxrow = Convert.ToInt32(ID) - 1;
             int gvcount = gvSDND.Rows.Count - 1;
             int gvrowindex = gr.RowIndex;
             if (gvcount == gvrowindex)
             {
                 AddNewRow(txtbp);
             }
             else
             {
                 txtbp.Focus();
             }
            // ((TextBox)gr.FindControl("txtBuyingPrice")).Focus();
             imgSave.Visible = true;
             dvisave.Visible = true;
             dvSaveDisabled.Visible = false;
             imgSaveDisabled.Visible = false;
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
            dt.Columns.Add("SupplierDeliveryNoteDetailID");
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("BrandID");
            dt.Columns.Add("ProductID");
            dt.Columns.Add("BrandName");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ProductValue");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("GrossProductValue");
            dt.Columns.Add("BuyingPrice");
            dt.Columns.Add("NetBuyingPrice");           
            
            DataRow dr;
            for (int i = 0; i <= gvSDND.Rows.Count - 1; i++)
            {

                dr = dt.NewRow();
                dr["SupplierDeliveryNoteDetailID"] = ((HiddenField)gvSDND.Rows[i].FindControl("HDSupplierDeliveryNoteDetailID")).Value;
                dr["CategoryID"] = ((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue;
                dr["ProductID"] = ((HiddenField)gvSDND.Rows[i].FindControl("HDProductID")).Value;
                dr["BrandID"] = ((HiddenField)gvSDND.Rows[i].FindControl("HDBrandID")).Value;
                dr["ProductValue"] = ((TextBox)gvSDND.Rows[i].FindControl("txtProductValue")).Text;
                dr["BrandName"] = ((TextBox)gvSDND.Rows[i].FindControl("txtBrand")).Text;
                dr["ProductName"] = ((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text;
                dr["Quantity"] = ((TextBox)gvSDND.Rows[i].FindControl("txtQuantity")).Text;
                dr["GrossProductValue"] = ((TextBox)gvSDND.Rows[i].FindControl("txtGrossValue")).Text;
                dr["BuyingPrice"] = ((TextBox)gvSDND.Rows[i].FindControl("txtBuyingPrice")).Text;
                dr["NetBuyingPrice"] = ((TextBox)gvSDND.Rows[i].FindControl("txtNetBuyingPrice")).Text;                
                dt.Rows.Add(dr);
            }
            dr = dt.NewRow();

            dr["SupplierDeliveryNoteDetailID"] = "0";
            dr["CategoryID"] = "0";
            dr["BrandID"] = "0";
            dr["ProductID"] = "0";
            dr["BrandName"] = "";
            dr["ProductName"] = "";
            dr["Quantity"] = "0";
            dr["ProductValue"] = "0";
            dr["GrossProductValue"] = "0";
            dr["BuyingPrice"] = "0";
            dr["NetBuyingPrice"] = "0";
            //dr["NetProductTotal"] = "0";
            dt.Rows.Add(dr);
            SDN = new ESupplierDeliveryNote();
            ds = SDN.ddlCategory();
            dtCategory = ds.Tables[0];
            ds = SDN.ddlBrand();
           dtBrand = ds.Tables[0];
            l = 0;
            gvSDND.DataSource = dt;
            gvSDND.DataBind();
            txtbp.Focus();
            ((TextBox)(gvSDND.Rows[gvSDND.Rows.Count - 2].FindControl("txtBuyingPrice"))).Focus();
            //for (int i = 0; i < noneditablerows; i++)
            //    gvSDND.Rows[i].Enabled = false;
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlBrand_selectedindexchanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
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
                SDN = new ESupplierDeliveryNote();
                ds = new DataSet();
                SDN.CategoryID = categoryid;
                SDN.BrandID = brandid;
                ds = SDN.ddlProducts();
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
    protected void gvSDND_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DeleteRow")
            {
                string result = string.Empty;
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int rowcount = gvSDND.Rows.Count;
                if (rowcount != 1)
                {
                    int supplierdeliverynotedetailid = Convert.ToInt32(((HiddenField)gvSDND.Rows[rowindex].FindControl("HDSupplierDeliveryNoteDetailID")).Value);
                    if (supplierdeliverynotedetailid != 0)
                    {
                        SDN = new ESupplierDeliveryNote();
                        SDN.SupplierDeliveryNoteDetailID = supplierdeliverynotedetailid;
                        SDN.SupplierDeliveryNoteID = Convert.ToInt32(HDSupplierDeliveryNoteID.Value);
                        result = SDN.DeleteSDND();
                        int supplierdeliverynoteid = Convert.ToInt32(HDSupplierDeliveryNoteID.Value);
                        EditFunction(supplierdeliverynoteid);
                    }
                    else
                    {
                        dt = new DataTable();
                        dt.Columns.Add("SupplierDeliveryNoteDetailID");
                        dt.Columns.Add("CategoryID");
                        dt.Columns.Add("BrandID");
                        dt.Columns.Add("ProductID");
                        dt.Columns.Add("BrandName");
                        dt.Columns.Add("ProductName");
                        dt.Columns.Add("ProductValue");
                        dt.Columns.Add("Quantity");
                        dt.Columns.Add("GrossProductValue");
                        dt.Columns.Add("BuyingPrice");
                        dt.Columns.Add("NetBuyingPrice");
                        DataRow dr;
                        for (int i = 0; i <= gvSDND.Rows.Count - 2; i++)
                        {

                            if (i != rowindex)
                            {
                                dr = dt.NewRow();
                                dr["SupplierDeliveryNoteDetailID"] = ((HiddenField)gvSDND.Rows[i].FindControl("HDSupplierDeliveryNoteDetailID")).Value;
                                dr["CategoryID"] = ((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue;
                                if (((DropDownList)gvSDND.Rows[i].FindControl("ddlProduct")).SelectedValue == "")
                                    dr["ProductID"] = "0";
                                else
                                    dr["ProductID"] = ((DropDownList)gvSDND.Rows[i].FindControl("ddlProduct")).SelectedValue;
                                dr["BrandID"] = ((DropDownList)gvSDND.Rows[i].FindControl("ddlBrand")).SelectedValue;
                                dr["BrandName"] = ((TextBox)gvSDND.Rows[i].FindControl("txtBrand")).Text;
                                dr["ProductName"] = ((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text;
                                dr["ProductValue"] = ((TextBox)gvSDND.Rows[i].FindControl("txtProductValue")).Text;
                                dr["Quantity"] = ((TextBox)gvSDND.Rows[i].FindControl("txtQuantity")).Text;
                                dr["GrossProductValue"] = ((TextBox)gvSDND.Rows[i].FindControl("txtGrossValue")).Text;
                                dr["BuyingPrice"] = ((TextBox)gvSDND.Rows[i].FindControl("txtBuyingPrice")).Text;
                                dr["NetBuyingPrice"] = ((TextBox)gvSDND.Rows[i].FindControl("txtNetBuyingPrice")).Text;
                                dt.Rows.Add(dr);
                            }
                           
                        }
                        dr = dt.NewRow();

                        dr["SupplierDeliveryNoteDetailID"] = "0";
                        dr["CategoryID"] = "0";
                        dr["BrandID"] = "0";
                        dr["ProductID"] = "0";
                        dr["BrandName"] = "";
                        dr["ProductName"] = "";
                        dr["Quantity"] = "0";
                        dr["ProductValue"] = "0.00";
                        dr["GrossProductValue"] = "0.00";
                        dr["BuyingPrice"] = "0.00";
                        dr["NetBuyingPrice"] = "0";
                        //dr["NetProductTotal"] = "0";
                        dt.Rows.Add(dr);
                        SDN = new ESupplierDeliveryNote();
                        ds = SDN.ddlCategory();
                        dtCategory = ds.Tables[0];
                        ds = SDN.ddlBrand();
                        dtBrand = ds.Tables[0];
                        l = 0;
                        gvSDND.DataSource = dt;
                        gvSDND.DataBind();
                        dvFailure.Visible = false;
                        lblStatus.Text = "";
                        dvSuccess.Visible = true;
                        lblSuccess.Text = "Record Deleted Successfully";
                    }
                }
                else
                {
                    int supplierdeliverynotedetailid = Convert.ToInt32(((HiddenField)gvSDND.Rows[rowindex].FindControl("HDSupplierDeliveryNoteDetailID")).Value);
                    SDN = new ESupplierDeliveryNote();
                    SDN.SupplierDeliveryNoteDetailID = supplierdeliverynotedetailid;
                    if (supplierdeliverynotedetailid != 0)
                        result = SDN.DeleteSDND();
                    AddEmptyRows();
                }

               
                //ShowTabs(1);               
            }
            else if (e.CommandName == "Product")
            {
                clearproductpopupfields();
                dvBrandPopUp.Attributes["style"] = "display:none;";
            }
            else if (e.CommandName == "Brand")
            {
                dvBrandPopUp.Attributes["style"] = "display:block;height:250px;width:600px;";
                lblStatusBrand.Text = "";
                dvProductPopUp.Attributes["style"] = "display:none;";
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void gvSDND_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            TextBox tt,txtprod,txtbrand;
            HiddenField hd,hd1;
            DropDownList ddlproducts, ddlcard,ddlbrand=null;
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


                if (Convert.ToInt32(dt.Rows[l]["CategoryID"]) != 0)
                {
                    
                    ddlcard.SelectedValue = Convert.ToString(dt.Rows[l]["CategoryID"]);
                    int categoryid = Convert.ToInt32(ddlcard.SelectedValue);
                   

                    hd = (HiddenField)e.Row.FindControl("HDBrandID");
                    hd.Value = Convert.ToString(dt.Rows[l]["BrandID"]);
                    hd1=(HiddenField)e.Row.FindControl("HDProductID");
                    hd1.Value=Convert.ToString(dt.Rows[l]["ProductID"]);
                    txtprod = (TextBox)e.Row.FindControl("txtProduct");
                    txtprod.Text = Convert.ToString(dt.Rows[l]["ProductName"]);
                    txtbrand = (TextBox)e.Row.FindControl("txtBrand");
                    txtbrand.Text = Convert.ToString(dt.Rows[l]["BrandName"]);

                    ddlbrand.SelectedValue = Convert.ToString(dt.Rows[l]["BrandID"]);
                    ddlbrand.Enabled = false;
                    int brandid = Convert.ToInt32(ddlbrand.SelectedValue);

                    fillproduct(brandid,categoryid,ddlproducts);
                    ddlproducts.SelectedValue = Convert.ToString(dt.Rows[l]["ProductID"]);               
                    productvalue = Convert.ToSingle(dt.Rows[l]["ProductValue"]);
                    ddlproducts.Enabled = false;
                    if (Convert.ToString(dt.Rows[l]["Quantity"]) != "")
                        qunatity = Convert.ToInt32(dt.Rows[l]["Quantity"]);
                    else
                        qunatity = 0;
                    float grossvalue = (productvalue * qunatity);
                    tt = (TextBox)e.Row.FindControl("txtGrossValue");
                    tt.Text = grossvalue.ToString();

                    totalgrossvalue += grossvalue;
                    totalnetbuyingprice += Convert.ToSingle(dt.Rows[l]["NetBuyingPrice"]);
                    
                    if (Convert.ToInt32(dt.Rows[l]["SupplierDeliveryNoteDetailID"]) != 0)
                    {
                        ddlcard.Enabled = false;
                        txtbrand.Enabled = false;
                        txtprod.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtProductValue");
                        tt.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtQuantity");
                        tt.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtGrossValue");
                        tt.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtBuyingPrice");
                        tt.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtNetBuyingPrice");
                        tt.Enabled = false; 
                        
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

   
    protected void fillproduct(int brandid,int categoryid,DropDownList ddlprod)
    {
        try
        {
            ds = new DataSet();
            SDN = new ESupplierDeliveryNote();
            SDN.BrandID = brandid;
            SDN.CategoryID = categoryid;
            ds = SDN.ddlProducts();
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
    protected void gvSDND_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void imgSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ds = new DataSet();
        string StoreDeliveryNoteNo = "";
            try
            {
            EStoreDeliveryNote ESDNobj = new EStoreDeliveryNote();
            dsforSDN = new DataSet();
            ESDNobj.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            dsforSDN = ESDNobj.GetID();
            string result1 = Convert.ToString(dsforSDN.Tables[0].Rows[0]["Status"]);
            string StoreCode = Convert.ToString(dsforSDN.Tables[0].Rows[0]["StoreCode"]);
            //if (StoreName.Length > 4)
            //    StoreName = StoreName.Substring(0, 4);
            //else
            StoreCode = StoreCode.Substring(0, StoreCode.Length);
            StoreDeliveryNoteNo = StoreCode + '-' + DateTime.Now.ToString("ddMMyyy") + '-' + result1;

            SDN = new ESupplierDeliveryNote();
                string GridDataDeliveryNote = string.Empty;
            string GridData = string.Empty;
            int supplierdeliverynotedetailid = 0;
                decimal netbuyingprice = 0;
                decimal totalvalue = 0;
                ControlStatus(true);
            
            for (int i = 0; i < gvSDND.Rows.Count; i++)
                {
                    supplierdeliverynotedetailid = Convert.ToInt32(((HiddenField)gvSDND.Rows[i].FindControl("HDSupplierDeliveryNoteDetailID")).Value);
                  //  ds = SDN.GetPurchaseDetailId(supplierdeliverynotedetailid);
                   // if (supplierdeliverynotedetailid == 0)
                   // {
                        if (((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" && ((TextBox)gvSDND.Rows[i].FindControl("txtBrand")).Text != "0" && ((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text != "0" && ((TextBox)gvSDND.Rows[i].FindControl("txtQuantity")).Text != "0")
                            GridData += ((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue + "~"
                                 + ((TextBox)gvSDND.Rows[i].FindControl("txtBrand")).Text + "~"
                                + ((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text + "~"
                                + ((TextBox)gvSDND.Rows[i].FindControl("txtProductValue")).Text + "~"
                                + ((TextBox)gvSDND.Rows[i].FindControl("txtQuantity")).Text + "~"
                                + ((TextBox)gvSDND.Rows[i].FindControl("txtBuyingPrice")).Text + "~"
                                + ((TextBox)gvSDND.Rows[i].FindControl("txtNetBuyingPrice")).Text + "$";
                        netbuyingprice = netbuyingprice + Convert.ToDecimal(((TextBox)gvSDND.Rows[i].FindControl("txtNetBuyingPrice")).Text);

                        if (((TextBox)gvSDND.Rows[i].FindControl("txtNetBuyingPrice")).Text != "")
                        {
                            totalvalue = totalvalue + Convert.ToDecimal(((TextBox)gvSDND.Rows[i].FindControl("txtNetBuyingPrice")).Text) * 1000 / 1000;
                        }
                        if (((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text != "")
                        {
                            string filename = ((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text + ".png";
                            //if (!File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                            //{
                            //    TextBox tt = (TextBox)gvSDND.Rows[i].FindControl("txtProduct");
                            //    System.Drawing.Image img = Code128Rendering.MakeBarcodeImage(tt.Text, 2, true);
                            //    if (File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                            //    {
                            //        File.Delete(Server.MapPath("~/images/BarCodeImages/" + filename));
                            //    }
                            //    string path = Server.MapPath("~/images/BarCodeImages");
                            //    img.Save(path + "/" + filename, ImageFormat.Png);
                            //}
                        }
                  //  }
                }
            //DeliveryNote Details
            for (int i = 0; i < gvSDND.Rows.Count; i++)
            {
                supplierdeliverynotedetailid = Convert.ToInt32(((HiddenField)gvSDND.Rows[i].FindControl("HDSupplierDeliveryNoteDetailID")).Value);
                if (supplierdeliverynotedetailid == 0)
                {
                    if (((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue != "0")
                        GridDataDeliveryNote += ((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue + "~"
                            + ((HiddenField)gvSDND.Rows[i].FindControl("HDBrandID")).Value + "~"
                            + ((HiddenField)gvSDND.Rows[i].FindControl("HDProductID")).Value + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtProductValue")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtQuantity")).Text + "~"
                            // + ((TextBox)gvLineItems.Rows[i].FindControl("txtAvailableQuantity")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtGrossValue")).Text + "$";
                }
            }

            if (GridDataDeliveryNote != "")
                GridDataDeliveryNote = GridDataDeliveryNote.Substring(0, GridDataDeliveryNote.Length - 1);
            //DeliveryENd
            if (GridData != "")
                {
                    GridData = GridData.Substring(0, GridData.Length - 1);
                    SDN.GridData = GridData;
                    if (txtTotalPrice.Text != "")
                        SDN.NetProductValue = netbuyingprice;
                    else
                        SDN.NetProductValue = 0;
                    if (txtHandlingCharges.Text != "")
                        SDN.HandlingCharges = Convert.ToDecimal(txtHandlingCharges.Text);
                    else
                        SDN.HandlingCharges = 0;
                    if (txtTotalProductValue.Text != "")
                    {
                        SDN.TotalValue = totalvalue;
                    }
                    else
                        SDN.TotalValue = 0;
                    SDN.Remarks = txtRemarks.Text;
                    SDN.SupplierDeliveryNoteID = Convert.ToInt32(HDSupplierDeliveryNoteID.Value);
                    SDN.LoginID = Convert.ToInt32(Session["LOGINID"]);
                SDN.store= Convert.ToInt32(ddlStore.SelectedValue);
                SDN.GridDataDeliveryNote = GridDataDeliveryNote;
                SDN.StoreDeliveryNoteNo = StoreDeliveryNoteNo;
                //char[] separatingChars = { '$' ,'~' };
                //string[] words = GridData.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                //ds = new DataSet();
                //ds = SDN.ECheckDuplicatesVIEWEDIT(words[0],words[1],words[2]);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    dvFailure.Visible = true;
                //    lblStatus.Text = "Record Already Exists";
                //}
                //else
                //{
                string result = SDN.InsertSDND();
                        if (result == "Record Updated Successfully.")
                        {
                            Session["save"] = null;
                            ShowTabs(1);
                            FillGrid();
                            dvSuccess.Visible = true;
                            lblSuccess.Text = result;
                            int supplierdeliverynoteid = Convert.ToInt32(HDSupplierDeliveryNoteID.Value);
                            SDN = new ESupplierDeliveryNote();
                            ds = new DataSet();
                            SDN.SupplierDeliveryNoteID = supplierdeliverynoteid;
                            try
                            {
                                SDN.LoginID = Convert.ToInt32(Session["LOGINID"].ToString());
                                SDN.SupplierDeliveryNoteID = supplierdeliverynoteid;

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

                        else
                        {
                            dvFailure.Visible = true;
                            lblStatus.Text = result;
                        }
                 //   }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "<script language='javascript'  type='text/javascript'>;alert('Please Fill the Goods Required to Purchase.');</script>", false);
                    return;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        //}
    }
    protected void ImgSearch_click(object sender, EventArgs e)
    {
        try
        {
            lblStatus.Text = string.Empty;
            lblSuccess.Text = string.Empty;
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            FillGrid();
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
            SDN = new ESupplierDeliveryNote();
            if (ddlOrganisationSearch.SelectedValue != "0")
                SDN.OrganisationID = Convert.ToInt32(ddlOrganisationSearch.SelectedValue);
            else
                SDN.OrganisationID = 0;
            if (ddlSupplierSearch.SelectedValue != "0")
                SDN.SupplierID = Convert.ToInt32(ddlSupplierSearch.SelectedValue);
            else
                SDN.SupplierID = 0;
            if (txtDeliveryNoteNoSearch.Text != "")
                SDN.SDNNo = txtDeliveryNoteNoSearch.Text;
            else
                SDN.SDNNo = "";
            if (txtFromDate.Text != "")
                SDN.FromDate = converttodate(txtFromDate.Text);
            else
                SDN.FromDate = "";
            if (txtToDate.Text != "")
                SDN.ToDate = converttodate(txtToDate.Text);
            else
                SDN.ToDate = "";
            ds = new DataSet();
            ds = SDN.Grid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlSearchGrid.Visible = true;
                gvSDN.DataSource = ds.Tables[0];
                gvSDN.DataBind();
                gvSDN.HeaderRow.TableSection = TableRowSection.TableHeader;
                lblStatus.Text = "";
               
                
            }
            else
            {
                gvSDN.DataSource = null;
                gvSDN.DataBind();
                dvFailure.Visible = true;
                lblStatus.Text = "No Records found.";
                pnlSearchGrid.Visible = false;
                ddlSupplierSearch.SelectedValue = "0";
                txtDeliveryNoteNoSearch.Text = "";
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
    protected void gvSDN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblStatus.Text = "";
        if (e.CommandName == "View")
        {
            noneditablerows = 0;
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int supplierdeliverynoteid = Convert.ToInt32(gvSDN.DataKeys[rowindex].Value);
            SDN = new ESupplierDeliveryNote();
            ds = new DataSet();
            SDN.SupplierDeliveryNoteID = supplierdeliverynoteid;
            ds = SDN.GridForViewEdit();
           
                if (ds.Tables.Count > 0)
                {
                    ddlOrganisation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationID"]);
                    ddlSupplier.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["SupplierID"]);
                    txtDeliveryNoteDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["SupplierDeliveryNoteDate"]);
                    txtDeliveryNoteNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["SupplierDeliveryNoteNo"]);
                    txtPaymentDueDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["PaymentDueDate"]);
                    btnSaveDeliveryNote.Visible = false;
                    btncancel1.Visible = false;
                    dvcancel.Visible = false;
                    txtHandlingCharges.Text = Convert.ToString(ds.Tables[0].Rows[0]["HandlingCharges"]);
                    txtTotalPrice.Text = Convert.ToString(ds.Tables[0].Rows[0]["NetProductValue"]);
                    txtTotalProductValue.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalValue"]);
                   
                    txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["Remarks"]);
                    ShowTabs(2);
                    EnableControls(false);
                    dvClear.Visible = false;
                    dvisave.Visible = false;
                    imgClear.Visible = false;
                    imgSave.Visible = false;
                    if (ds.Tables[1].Rows.Count > 0 || ds.Tables[2].Rows.Count > 0)
                    {
                        noneditablerows = ds.Tables[1].Rows.Count;
                        tbl.Visible = true;
                        gvSDND.Visible = true;
                        dt = ds.Tables[1];
                        dt1 = ds.Tables[2];
                        dt.Columns.Add("GrossProductValue");
                        dt1.Columns.Add("GrossProductValue");
                        ds = SDN.ddlCategory();
                        l = 0;
                        dtCategory = ds.Tables[0];
                        ds = SDN.ddlBrand();
                        dtBrand = ds.Tables[0];
                        totalgrossvalue = 0;
                        totalnetbuyingprice = 0;
                        dt.Merge(dt1);
                        dt.AcceptChanges();
                        gvSDND.DataSource = dt;
                        gvSDND.DataBind();
                        txttotalgrossvalue.Text = totalgrossvalue.ToString();
                        //txttotaldiscountvalue.Text = totaldiscount.ToString();
                        txtRemarks.ReadOnly = true;
                        txtHandlingCharges.ReadOnly = true;

                        for (int i = 0; i < noneditablerows; i++)
                        {
                            LinkButton im = (LinkButton)gvSDND.Rows[i].FindControl("imgDeleteRow");
                            im.Visible = false;
                            //im = (LinkButton)gvSDND.Rows[i].FindControl("imgBrand");
                            //im.Visible = false;
                            //im = (LinkButton)gvSDND.Rows[i].FindControl("imgProduct");
                            //im.Visible = false;
                        }

                    }
                    else
                    {
                        //gvSDND.Attributes["style"] = "display:none";
                        //tbl.Attributes["style"] = "display:none";
                        showButtons("View");
                        AddEmptyRows();
                        dvisave.Visible = false;
                        imgSave.Visible = false;
                    }
                    ControlStatus(false);
                    Divcan1.Visible = true;
                    btncancelgrid.Visible = true;
                }
           
            //lnkAdd.Text = "View";
        }
        else if (e.CommandName == "EditRow")
        {
            Session["save"] = "1";
            txtHandlingCharges.ReadOnly = false;
            txtRemarks.ReadOnly = false;
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int supplierdeliverynoteid = Convert.ToInt32(gvSDN.DataKeys[rowindex].Value);
            EditFunction(supplierdeliverynoteid);
            //lnkAdd.Text = "Edit";
            imgSave.Visible = true;
            dvisave.Visible = true;
            dvSaveDisabled.Visible = false;
            imgSaveDisabled.Visible = false;
            Divcan1.Visible = true;
            btncancelgrid.Visible = true;
            dvupdate.Visible = false;
            imgupdate.Visible = false;
            //showButtons("Edit");
        }
        else if (e.CommandName == "Deleting")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int supplierdeliverynoteid = Convert.ToInt32(gvSDN.DataKeys[rowindex].Value);
            SDN = new ESupplierDeliveryNote();
            ds = new DataSet();
            SDN.SupplierDeliveryNoteID = supplierdeliverynoteid;
            string result = SDN.DeleteSDN();
            ShowTabs(1);           
            FillGrid();
            dvSuccess.Visible = true;
            lblSuccess.Text = result;
        }
        else if(e.CommandName == "Print")
        {
           
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int supplierdeliverynoteid = Convert.ToInt32(gvSDN.DataKeys[rowindex].Value);
            SDN = new ESupplierDeliveryNote();
            ds = new DataSet();
            SDN.SupplierDeliveryNoteID = supplierdeliverynoteid;
            try
            {
                SDN.LoginID = Convert.ToInt32(Session["LOGINID"].ToString());
                SDN.SupplierDeliveryNoteID = supplierdeliverynoteid;
               
                if (supplierdeliverynoteid !=0)
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
                    SupplierPrint.SupplierDeliveryNoteDate = dsforSupID.Tables[0].Rows[0]["SupplierDeliveryNoteDate"].ToString();
                    //string supdelnotedate = ;
                    //DateTime MyDateTime = new DateTime();
                    //MyDateTime = Convert.ToDateTime(supdelnotedate);
                    //string date = MyDateTime.ToString("dd-MM-yyyy");
                    //SupplierPrint.SupplierDeliveryNoteDate = dsforSupID.Tables[0].Rows[0]["SupplierDeliveryNoteDate"].ToString();
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
                    listSupplier.ProductValue=Convert.ToDecimal(ds.Tables[0].Rows[i]["ProductValue"]);
                    listSupplier.BuyingPriceperPice =Convert.ToDecimal(ds.Tables[0].Rows[i]["BuyingPrice"]);
                    listSupplier.GrossValue = (Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalGrossValue"]));
                    listSupplier.NetBuyingPrice =Convert.ToDecimal((listSupplier.Quantity) * (listSupplier.BuyingPriceperPice));
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
    DataTable dt1;
    protected void EditFunction(int supplierdeliverynoteid)
    {
        
        SDN = new ESupplierDeliveryNote();
        ds = new DataSet();
        SDN.SupplierDeliveryNoteID = supplierdeliverynoteid;
        HDSupplierDeliveryNoteID.Value = supplierdeliverynoteid.ToString();
        ds = SDN.GridForViewEdit();
       
            if (ds.Tables.Count > 0)
            {
                ddlOrganisation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationID"]);
                ddlSupplier.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["SupplierID"]);
                txtDeliveryNoteDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["SupplierDeliveryNoteDate"]);
                txtDeliveryNoteNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["SupplierDeliveryNoteNo"]);
                txtPaymentDueDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["PaymentDueDate"]);
                btnSaveDeliveryNote.Visible = false;
                btncancel1.Visible = false;
                dvcancel.Visible = false;
                txtHandlingCharges.Text = Convert.ToString(ds.Tables[0].Rows[0]["HandlingCharges"]);
                txtTotalPrice.Text = Convert.ToString(ds.Tables[0].Rows[0]["NetProductValue"]);
                txtTotalProductValue.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalValue"]);
                txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["Remarks"]);
                ShowTabs(2);
                dvClear.Visible = false;
                imgClear.Visible = false;
                imgSave.Visible = true;
                EnableControls(false);

                if (ds.Tables[1].Rows.Count > 0 || ds.Tables[2].Rows.Count > 0)
                {
                    tbl.Visible = true;
                    gvSDND.Visible = true;
                    dt = ds.Tables[1];
                    dt1 = ds.Tables[2];
                    dt.Merge(dt1);
                    dt.AcceptChanges(); 
                    noneditablerows = dt.Rows.Count;
                    dt.Columns.Add("GrossProductValue");
                    dt1.Columns.Add("GrossProductValue");
                    DataRow dr = dt.NewRow();
                    dr["SupplierDeliveryNoteDetailID"] = "0";
                    dr["CategoryID"] = "0";
                    dr["ProductID"] = "0";
                    dr["ProductID"] = "0";
                    dr["Quantity"] = "0";
                    dr["ProductValue"] = "0.00";
                    dr["GrossProductValue"] = "0.00";
                    dr["BuyingPrice"] = "0.00";
                    dr["NetBuyingPrice"] = "0";
                    dt.Rows.Add(dr);
                    ds = SDN.ddlCategory();
                    l = 0;
                    dtCategory = ds.Tables[0];
                    ds = SDN.ddlBrand();
                    dtBrand = ds.Tables[0];
                    totalgrossvalue = 0;
                    totalnetbuyingprice = 0;
                    
                    gvSDND.DataSource = dt;
                    gvSDND.DataBind();
                    txttotalgrossvalue.Text = totalgrossvalue.ToString();
                    ((TextBox)gvSDND.Rows[gvSDND.Rows.Count - 1].FindControl("txtProduct")).Focus();
                }
                else
                {
                    noneditablerows = 0;
                    AddEmptyRows();
                    ((TextBox)gvSDND.Rows[0].FindControl("txtProduct")).Focus();
                }
                if (deletepermission == false)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        LinkButton im = (LinkButton)gvSDND.Rows[i].FindControl("imgDeleteRow");
                        im.Visible = false;
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        LinkButton im = (LinkButton)gvSDND.Rows[i].FindControl("imgDeleteRow");
                        im.Visible = true;
                    }
                }
                ControlStatus(false);
            }
      
    }
    protected void gvSDN_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvSDN_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    public void ControlStatus(bool Status)
    {
        txttotalgrossvalue.Enabled = Status;
        //txttotaldiscountvalue.Enabled = Status;
        txtTotalPrice.Enabled = Status;
        txtTotalProductValue.Enabled = Status;
    }
    protected void gvSDN_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void lnkbtnPrint_OnClick(object sender, EventArgs e)
    {
        //PrintpopExtender.Show();
        
    }
    //[WebMethod (EnableSession=true)]
    //public static void getBrandCategory(string Brandname, string Categoryid)
    //{
    //    try
    //    {
    //        HttpContext.Current.Session["CategoryID"] = Categoryid;
    //        HttpContext.Current.Session["BrandName"] = Brandname;
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    protected void btnClearProduct_onclick(object sender, EventArgs e)
    {
        dvProductPopUp.Attributes["style"] = "display:none";
    }
    protected void imgSaveProduct_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
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
             result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            if (result == "Record Inserted Succesfully.")
            {
                //ShowTabs(1);
                dvSuccess.Visible = true;
                lblSuccess.Text = result;

                //lblStatus.CssClass = "SuccessMsg";
                dvProductPopUp.Attributes["style"] = "display:none";
                //string filename = txtProductName.Text + ".png";//Convert.ToString(ds.Tables[0].Rows[0]["barcode"]);
                //System.Drawing.Image i = Code128Rendering.MakeBarcodeImage(txtProductName.Text, 2, true);
                //if (File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                //{
                //    File.Delete(Server.MapPath("~/images/BarCodeImages/" + filename));
                //}
                //i.Save(@"D:\Working\Esales\Esale\Images\BarCodeImages\" + filename, ImageFormat.Png);
                string filename = txtProductName.Text + ".png";
              /*  BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                System.Drawing.Image i = b.Encode(BarcodeLib.TYPE.CODE128, filename);*/
                //System.Drawing.Image i = Code128Rendering.MakeBarcodeImage(txtProductName.Text, 2, true);                
              /*  if (File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                {
                    File.Delete(Server.MapPath("~/images/BarCodeImages/" + filename));
                }
                string path = Server.MapPath("~/images/BarCodeImages"); */
               // i.Save(path + "/" + filename, ImageFormat.Png);
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true);

            }
            else
            {

                //lblStatusProduct.CssClass = "ErrorMsg";
                clearproductpopupfields();                
                lblStatusProduct.Text = "Details already Exist";
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
       // ddlOrganisationSearch.SelectedIndex = 0;
        ddlSupplierSearch.SelectedIndex = 0;
        txtDeliveryNoteNoSearch.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        FillGrid();
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
                //lblStatus.CssClass = "SuccessMsg";
                txtAddBrand.Text = string.Empty;
                dvBrandPopUp.Attributes["style"] = "display:none";
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true); 
            }
            else
            {
                //dvFailure.Visible = true;
                //lblStatusBrand.Text = result;
                lblStatusBrand.CssClass = "ErrorMsg";
                txtAddBrand.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            dvFailure.Visible = true;
            lblStatus.Text = ex.Message;
           // lblStatus.CssClass = "ErrorMsg";
        }
    }
    protected void btnCancelBrand_onclick(object sender, EventArgs  e)
    {
        txtAddBrand.Text = "";
        dvBrandPopUp.Attributes["style"] = "display:none";
    }
   
    protected void clearproductpopupfields()
    {
        txtProductName.Text = "";
        txtProductValue.Text = "";
        txtMaxDiscPer.Text = "0.00";
        chkActiveProduct.Checked = true;
        dvProductPopUp.Attributes["style"] = "display:block;height:380px;";
        FillBrand();
        FillCategory();
        DDLCategory.SelectedValue = "0";
        ddlBrand.SelectedValue = "0";
        lblStatusProduct.Text = "";
    }
    //protected void txtProductValue_ontextchanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
    //        DropDownList ddl = (DropDownList)gr.FindControl("ddlCategory");
    //        HiddenField hd = (HiddenField)gr.FindControl("HDBrandID");
    //        TextBox txtbrand = (TextBox)gr.FindControl("txtBrand");
    //        TextBox txtproduct = (TextBox)gr.FindControl("txtProduct");
    //        TextBox txtsp = sender as TextBox;
    //        if (txtbrand.Text != "" && ddl.SelectedValue != "0" && txtproduct.Text != "")
    //        {
    //            ds = new DataSet();
    //            EProduct product = new EProduct();
    //            product.BrandID = Convert.ToInt32(hd.Value);
    //            product.CategoryID = Convert.ToInt32(ddl.SelectedValue);
    //            product.ProductName = txtproduct.Text;
    //            product.ProductValue = Convert.ToSingle(txtsp.Text);
    //            product.Active = true;
    //            product.MaxDiscountPer = 0;
    //            product.LoginID = Convert.ToInt32(Session["LOGINID"]);
    //            ds = product.Insert();
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {

    //            }
    //        }
    //        else
    //        {
    //            string str = string.Empty;
    //            if (ddl.SelectedValue == "0")
    //            {
    //                str += " Please Select Category ";
    //                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('" + str + "');", true);
    //                txtsp.Text = "0.00";
    //                return;
    //            }
    //            if (txtbrand.Text == "")
    //            {
    //                str = " Please Enter Brand ";
    //                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('" + str + "');", true);
    //                txtsp.Text = "0.00";
    //                return;
    //            }
    //            if (txtproduct.Text == "")
    //            {
    //                str = " Please Enter Category ";
    //                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('" + str + "');", true);
    //                txtsp.Text = "0.00";
    //                return;
    //            }

    //        }

    //    }
    //    catch (Exception)
    //    {
            
    //        throw;
    //    }
    //}
    //protected void txtBrand_ontextchanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //         GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
    //         TextBox txtbrand = sender as TextBox;
    //         if (txtbrand.Text != "")
    //         {
    //             DropDownList ddl = (DropDownList)gr.FindControl("ddlCategory");
    //             HiddenField hd = (HiddenField)gr.FindControl("HDBrandID");
    //             SDN = new ESupplierDeliveryNote();
    //             SDN.BrandName = txtbrand.Text.Trim();
    //             ds = new DataSet();
    //             ds = SDN.GetBrandID();
    //             int brandid=0;
    //             if (ds.Tables[0].Rows.Count > 0)
    //             {
    //                 brandid = Convert.ToInt32(ds.Tables[0].Rows[0]["BrandID"]);
    //                 hd.Value = brandid.ToString();
    //                 AjaxControlToolkit.AutoCompleteExtender ae = (AjaxControlToolkit.AutoCompleteExtender)gr.FindControl("AutoCompleteExtender2");
    //                 ae.ContextKey = ddl.SelectedValue + "~" + brandid.ToString();
    //             }
    //             else
    //             {
    //                 ds = new DataSet();
    //                 EBrand brand= new EBrand();
    //                 brand.IsActive = true;
    //                 brand.BrandName = txtbrand.Text;
    //                  brandid=Convert.ToInt32(brand.EAddBrand());
    //                  hd.Value = brandid.ToString();
    //                  AjaxControlToolkit.AutoCompleteExtender ae = (AjaxControlToolkit.AutoCompleteExtender)gr.FindControl("AutoCompleteExtender2");
    //                  ae.ContextKey = ddl.SelectedValue + "~" + brandid.ToString();
    //             }
                 
    //         }
    //         //txtbrand.Focus();
    //    }
    //    catch (Exception)
    //    {
            
    //        throw;
    //    }
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true);
       
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {

            SDN = new ESupplierDeliveryNote();
            string GridData = string.Empty;
            int supplierdeliverynotedetailid = 0;
            ControlStatus(true);
            for (int i = 0; i < gvSDND.Rows.Count; i++)
            {
                supplierdeliverynotedetailid = Convert.ToInt32(((HiddenField)gvSDND.Rows[i].FindControl("HDSupplierDeliveryNoteDetailID")).Value);
                if (supplierdeliverynotedetailid == 0)
                {
                    if (((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" && ((TextBox)gvSDND.Rows[i].FindControl("txtBrand")).Text != "0" && ((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text != "0" && ((TextBox)gvSDND.Rows[i].FindControl("txtQuantity")).Text != "0")
                        GridData += ((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue + "~"
                             + ((TextBox)gvSDND.Rows[i].FindControl("txtBrand")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtProductValue")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtQuantity")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtBuyingPrice")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtNetBuyingPrice")).Text + "$";
                    if (((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text != "")
                    {
                        string filename = ((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text + ".png";
                        //if (!File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                        //{
                        //    TextBox tt = (TextBox)gvSDND.Rows[i].FindControl("txtProduct");
                        //    System.Drawing.Image img = Code128Rendering.MakeBarcodeImage(tt.Text, 2, true);
                        //    if (File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                        //    {
                        //        File.Delete(Server.MapPath("~/images/BarCodeImages/" + filename));
                        //    }
                        //    string path = Server.MapPath("~/images/BarCodeImages");
                        //    img.Save(path + "/" + filename, ImageFormat.Png);
                        //}
                    }
                }
            }
            if (GridData != "")
            {
                GridData = GridData.Substring(0, GridData.Length - 1);
                SDN.GridData = GridData;
                if (txtTotalPrice.Text != "")
                    SDN.NetProductValue = Convert.ToDecimal(txtTotalPrice.Text);
                else
                    SDN.NetProductValue = 0;
                if (txtHandlingCharges.Text != "")
                    SDN.HandlingCharges = Convert.ToDecimal(txtHandlingCharges.Text);
                else
                    SDN.HandlingCharges = 0;
                if (txtTotalProductValue.Text != "")
                {
                    SDN.TotalValue = Convert.ToDecimal(txtTotalProductValue.Text);
                }
                else
                    SDN.TotalValue = 0;
                SDN.Remarks = txtRemarks.Text;
                SDN.SupplierDeliveryNoteID = Convert.ToInt32(HDSupplierDeliveryNoteID.Value);
                SDN.LoginID = Convert.ToInt32(Session["LOGINID"]);
                //char[] separatingChars = { '$' ,'~' };
                //string[] words = GridData.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                //ds = new DataSet();
                //ds = SDN.ECheckDuplicatesVIEWEDIT(words[0],words[1],words[2]);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    dvFailure.Visible = true;
                //    lblStatus.Text = "Record Already Exists";
                //}
                //else
                //{
                string result = SDN.InsertSDNDTemp();
                //if (result == "Record Updated Successfully.")
                //{
                //    Session["save"] = null;
                //    ShowTabs(1);
                //    FillGrid();
                //    dvSuccess.Visible = true;
                //    lblSuccess.Text = result;
                //    int supplierdeliverynoteid = Convert.ToInt32(HDSupplierDeliveryNoteID.Value);
                //    SDN = new ESupplierDeliveryNote();
                //    ds = new DataSet();
                //    SDN.SupplierDeliveryNoteID = supplierdeliverynoteid;
                //    try
                //    {
                //        SDN.LoginID = Convert.ToInt32(Session["LOGINID"].ToString());
                //        SDN.SupplierDeliveryNoteID = supplierdeliverynoteid;

                //        if (supplierdeliverynoteid != 0)
                //        {

                //            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "SalesForPrint(" + supplierdeliverynoteid + ");", true);

                //        }

                //    }
                //    catch (Exception ex)
                //    {

                //        throw ex;
                //    }
                //}

                //else
                //{
                //    dvFailure.Visible = true;
                //    lblStatus.Text = result;
                //}
                //   }
            }
           // else
           // {
             //   ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "<script language='javascript'  type='text/javascript'>;alert('Please Fill the Goods Required to Purchase.');</script>", false);
              //  return;
           // }

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void txtBuyingPrice_TextChanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {

            SDN = new ESupplierDeliveryNote();
            string GridData = string.Empty;
            int supplierdeliverynotedetailid = 0;
            ControlStatus(true);
            for (int i = 0; i < gvSDND.Rows.Count; i++)
            {
                supplierdeliverynotedetailid = Convert.ToInt32(((HiddenField)gvSDND.Rows[i].FindControl("HDSupplierDeliveryNoteDetailID")).Value);
                if (supplierdeliverynotedetailid == 0)
                {
                    if (((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" && ((TextBox)gvSDND.Rows[i].FindControl("txtBrand")).Text != "0" && ((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text != "0" && ((TextBox)gvSDND.Rows[i].FindControl("txtQuantity")).Text != "0")
                        GridData += ((DropDownList)gvSDND.Rows[i].FindControl("ddlCategory")).SelectedValue + "~"
                             + ((TextBox)gvSDND.Rows[i].FindControl("txtBrand")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtProductValue")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtQuantity")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtBuyingPrice")).Text + "~"
                            + ((TextBox)gvSDND.Rows[i].FindControl("txtNetBuyingPrice")).Text + "$";
                    if (((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text != "")
                    {
                        string filename = ((TextBox)gvSDND.Rows[i].FindControl("txtProduct")).Text + ".png";
                        //if (!File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                        //{
                        //    TextBox tt = (TextBox)gvSDND.Rows[i].FindControl("txtProduct");
                        //    System.Drawing.Image img = Code128Rendering.MakeBarcodeImage(tt.Text, 2, true);
                        //    if (File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                        //    {
                        //        File.Delete(Server.MapPath("~/images/BarCodeImages/" + filename));
                        //    }
                        //    string path = Server.MapPath("~/images/BarCodeImages");
                        //    img.Save(path + "/" + filename, ImageFormat.Png);
                        //}
                    }
                }
            }
            if (GridData != "")
            {
                GridData = GridData.Substring(0, GridData.Length - 1);
                SDN.GridData = GridData;
                if (txtTotalPrice.Text != "")
                    SDN.NetProductValue = Convert.ToDecimal(txtTotalPrice.Text);
                else
                    SDN.NetProductValue = 0;
                if (txtHandlingCharges.Text != "")
                    SDN.HandlingCharges = Convert.ToDecimal(txtHandlingCharges.Text);
                else
                    SDN.HandlingCharges = 0;
                if (txtTotalProductValue.Text != "")
                {
                    SDN.TotalValue = Convert.ToDecimal(txtTotalProductValue.Text);
                }
                else
                    SDN.TotalValue = 0;
                SDN.Remarks = txtRemarks.Text;
                SDN.SupplierDeliveryNoteID = Convert.ToInt32(HDSupplierDeliveryNoteID.Value);
                SDN.LoginID = Convert.ToInt32(Session["LOGINID"]);
            
                string result = SDN.InsertSDNDTemp();
              
            }
          
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void txtProductValue_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
             DropDownList ddl = (DropDownList)gr.FindControl("ddlCategory");
             TextBox txtproduct = (TextBox)gr.FindControl("txtProduct");
             TextBox txtbrand = (TextBox)gr.FindControl("txtBrand");
             TextBox txt = (TextBox)gr.FindControl("txtQuantity");
             TextBox prodvalue = (TextBox)gr.FindControl("txtProductValue");
             HiddenField hdprodid = (HiddenField)gr.FindControl("HDProductID");
             HiddenField hdbrandid = (HiddenField)gr.FindControl("HDBrandID");
             TextBox txtbp = (TextBox)gr.FindControl("txtBuyingPrice");
             SDN = new ESupplierDeliveryNote();
             if (txtproduct.Text != "")
             {
                 SDN.ProductName = txtproduct.Text.Trim();
                 SDN.ProductValue = Convert.ToDecimal(prodvalue.Text.Trim());
                 ds = new DataSet();
                 ds = SDN.UpdateNewProductsValue();
                
             }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
