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
using ESaleEntity;
using System.Runtime.InteropServices;


public class TSCLIB_DLL
{
    [DllImport("TSCLIB.dll", EntryPoint = "about")]
    public static extern int about();

    [DllImport("TSCLIB.dll", EntryPoint = "openport")]
    public static extern int openport(string printername);

    [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
    public static extern int barcode(string x, string y, string type,
                string height, string readable, string rotation,
                string narrow, string wide, string code);

    [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
    public static extern int clearbuffer();

    [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
    public static extern int closeport();

    [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
    public static extern int downloadpcx(string filename, string image_name);

    [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
    public static extern int formfeed();

    [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
    public static extern int nobackfeed();

    [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
    public static extern int printerfont(string x, string y, string fonttype,
                    string rotation, string xmul, string ymul,
                    string text);

    [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
    public static extern int printlabel(string set, string copy);

    [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
    public static extern int sendcommand(string printercommand);

    [DllImport("TSCLIB.dll", EntryPoint = "setup")]
    public static extern int setup(string width, string height,
              string speed, string density,
              string sensor, string vertical,
              string offset);

    [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
    public static extern int windowsfont(int x, int y, int fontheight,
                    int rotation, int fontstyle, int fontunderline,
                    string szFaceName, string content);

}
public partial class Screens_StoreDeliveryNote : System.Web.UI.Page
{
    int l = 0;
    EStoreDeliveryNote Store;
    DataSet dsforSDN;
    DataTable dtCategory,dtBrand;
    DataTable dt;
    ECheckPermission ECPobj;
    DataSet Total;
    EProduct product;
    DataSet ds;
    static bool addPermission = false;
    static bool viewPermission = true;
    static bool EditPermission = true;
    static bool deletepermission = true;
    string ScreenUrl = string.Empty;
    float totalgrossvalue = 0, totaldiscount = 0;
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
            
            FillOrganisation();
            FillStore();
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            lblSuccess.Text = "";
            lblStatus.Text = "";
            DateTime baseDate = DateTime.Today;
            var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
            string startdate = thisMonthStart.ToString("dd-MM-yyyy");
            txtDeliveryNoteFromDate.Text = startdate;
            var thisMonthEnd = thisMonthStart.AddMonths(1).Date.AddSeconds(-1);
            string enddate = thisMonthEnd.ToString("dd-MM-yyyy");
            txtDeliveryNoteToDate.Text = enddate;
            FillGrid();
            ShowTabs(1);
           
        }
        if (grdStoreDeliveryNote.HeaderRow != null)
        grdStoreDeliveryNote.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    // Fill Organisation Dropdownlist.
    public void FillOrganisation()
    {
        dsforSDN = new DataSet();
        Store = new EStoreDeliveryNote();
        try
        {
            dsforSDN = Store.ddlOrganisation();
            if (dsforSDN.Tables[0].Rows.Count > 0)
            {
                if (dsforSDN.Tables[0].Rows.Count == 1)
                {
                    ddlSearchOraganisation.DataSource = dsforSDN;
                    ddlSearchOraganisation.DataValueField = "OrganisationID";
                    ddlSearchOraganisation.DataTextField = "OrganisationName";
                    ddlSearchOraganisation.DataBind();
                    ddlOrganisation.DataSource = dsforSDN;
                    ddlOrganisation.DataValueField = "OrganisationID";
                    ddlOrganisation.DataTextField = "OrganisationName";
                    ddlOrganisation.DataBind();

                }
                else if (dsforSDN.Tables[0].Rows.Count > 1)
                {
                    ddlSearchOraganisation.DataSource = dsforSDN;
                    ddlSearchOraganisation.DataValueField = "OrganisationID";
                    ddlSearchOraganisation.DataTextField = "OrganisationName";
                    ddlSearchOraganisation.DataBind();
                    ddlSearchOraganisation.Items.Insert(0, new ListItem("--Any--", "0"));
                    ddlOrganisation.DataSource = dsforSDN;
                    ddlOrganisation.DataValueField = "OrganisationID";
                    ddlOrganisation.DataTextField = "OrganisationName";
                    ddlOrganisation.DataBind();
                    ddlOrganisation.Items.Insert(0, new ListItem("--Any--", "0"));
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }


    }
    // Method to fill the Store dropdownlist.
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
               // imgClear.Visible = EditPermission;

            }
            else
            {
                imgSave.Visible = EditPermission;
                //imgClear.Visible = EditPermission;

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
        dsforSDN = new DataSet();
        Store = new EStoreDeliveryNote();
        try
        {
            Store.OrganisationID = Convert.ToInt32(ddlSearchOraganisation.SelectedValue);
            Store.LoginID = Convert.ToInt32(Session["LOGINID"]);
            dsforSDN = Store.ddlStore();
            if (dsforSDN.Tables[0].Rows.Count > 0)
            {
                ddlSearchStore.DataSource = dsforSDN;
                ddlSearchStore.DataValueField = "StoreID";
                ddlSearchStore.DataTextField = "StoreName";
                ddlSearchStore.DataBind();
                ddlSearchStore.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlStore.DataSource = dsforSDN;
                ddlStore.DataValueField = "StoreID";
                ddlStore.DataTextField = "StoreName";
                ddlStore.DataBind();
                ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));

            }
            if(Convert.ToInt32(Session["LOGINID"])!=1 && dsforSDN.Tables[0].Rows.Count == 1)           
            {
                ddlStore.SelectedValue = Convert.ToString(dsforSDN.Tables[0].Rows[0]["StoreID"]);
                ddlSearchStore.SelectedValue = Convert.ToString(dsforSDN.Tables[0].Rows[0]["StoreID"]);
                ddlSearchStore.Enabled = false;
                ddlStore.Enabled = false;
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    public void ShowTabs(int Num)
    {
        if (Num == 1)
        {
            panelAddUser.Visible = false;
            panelSearchDeliveryNote.Visible = true;
            lnkAdd.Visible = true;
            lnkAdd.Text = "Add";
            //grdStoreDeliveryNote.PageIndex = 0;
            //txtDeliveryNoteFromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            //txtDeliveryNoteToDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            FillGrid();

            //FillGrid();
        }
        else if (Num == 2)
        {
            panelAddUser.Visible = true;
            panelSearchDeliveryNote.Visible = false;
            lnkAdd.Visible = false;
            lnkAdd.Text = "Add";
        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        ShowTabs(2);
        txtDeliveryDate.Text = System.DateTime.UtcNow.AddHours(3).ToString("dd-MM-yyyy");
        //txtPaymentDueDate.Text = DateTime.Today.AddDays(15).ToString("dd-MM-yyyy");

        gvLineItems.Visible = false;
        btnSaveBatch.Visible = false;
        btnCancel.Visible = false;
        SDNCalculation.Visible = false;
        btnSaveDeliveryNote.Visible = true;
        EnableControls(true);
        lblStatus.Text = "";
        ddlStore.SelectedValue = "0";
        txtDeliveryNo.Text = "";
        ddlStore.Enabled = true;
        if (Convert.ToString(Session["LOGINID"]) != "1" && ddlStore.Items.Count == 2)
        {
            ddlStore.SelectedIndex = 1;
            ddlSearchStore.SelectedIndex = 1;
            ddlStore.Enabled = false;
        }
        imgSave.Visible = true;
        dvcancel.Visible = true;
        btncancel1.Visible = true;
    }
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        DropDownList ddlcategory = (DropDownList)grow.FindControl("ddlCategory");
        DropDownList ddlbrand = (DropDownList)grow.FindControl("ddlBrand");
        DropDownList ddlProd = (DropDownList)grow.FindControl("ddlProduct");
        ddlProd.Items.Clear();
        int categoryid = Convert.ToInt32(ddlcategory.SelectedValue);
        int brandid = Convert.ToInt32(ddlbrand.SelectedValue);
        if (categoryid != 0)
        {
            Store = new EStoreDeliveryNote();
            dsforSDN = new DataSet();
            Store.CategoryID = categoryid;
            Store.BrandID = brandid;
            dsforSDN = Store.ddlProduct();
            if (dsforSDN.Tables[0].Rows.Count > 0)
            {
                ddlProd.DataSource = dsforSDN.Tables[0];
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
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddlprod = (DropDownList)gr.FindControl("ddlProduct");
             int productid = Convert.ToInt32(ddlprod.SelectedValue);
            if (productid != 0)
            {
                Store = new EStoreDeliveryNote();
                dsforSDN = new DataSet();
                Store.ProductID = productid;
                dsforSDN = Store.GetProductValue();
                if (dsforSDN.Tables[0].Rows.Count > 0)
                {
                    int availqty = 0;
                    //for (int i = 0; i < dsforSDN.Tables[0].Rows.Count; i++)
                    //{
                    //    availqty += Convert.ToInt32(dsforSDN.Tables[0].Rows[i]["AvailableQuantity"]);
                    //}
                    TextBox tt = (TextBox)gr.FindControl("txtProductValue");
                    tt.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["ProductValue"]);
                    //tt = (TextBox)gr.FindControl("txtDefaultSellingDiscount");
                    //tt.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["SellingDefaultDiscount"]);
                    //tt = (TextBox)gr.FindControl("txtAvailableQuantity");
                    //tt.Text = availqty.ToString();
                }
            }
            int rowcount = gvLineItems.Rows.Count;
            ddlprod = sender as DropDownList;
            string ID = ddlprod.ClientID;
            ID = ID.Replace("ctl00_ContentPlaceHolder1_gvLineItems_ctl", "");
            ID = ID.Replace("_ddlProduct", "");
            int maxrow = Convert.ToInt32(ID) - 1;
            if (rowcount == maxrow)
            {
                AddNewRow();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void AddNewRow()
    {
       
             dt=new DataTable();
             dt.Columns.Add("StoreDeliveryNoteDetailID");
             dt.Columns.Add("CategoryID");
             dt.Columns.Add("BrandID");
             dt.Columns.Add("ProductID");
             dt.Columns.Add("BrandName");
             dt.Columns.Add("ProductName");
             dt.Columns.Add("ProductValue");
             //dt.Columns.Add("AvailableQuantity");
             dt.Columns.Add("Quantity");
             dt.Columns.Add("NetProductTotal");
            DataRow dr;
            for (int i = 0; i <= gvLineItems.Rows.Count - 1; i++)
            {

                dr = dt.NewRow();
                dr["StoreDeliveryNoteDetailID"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDStoreDeliveryNoteDetailID")).Value;
                dr["CategoryID"] = ((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue;
                dr["ProductID"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value;
                dr["BrandID"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value;
                dr["BrandName"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtBrand")).Text;
                dr["ProductName"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtProduct")).Text;
                dr["ProductValue"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtProductValue")).Text;
                //dr["AvailableQuantity"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtAvailableQuantity")).Text;
                dr["Quantity"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtQunatity")).Text;
                dr["NetProductTotal"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtTotal")).Text;                
                dt.Rows.Add(dr);
            }
            dr = dt.NewRow();

            dr["StoreDeliveryNoteDetailID"] = "0";
            dr["CategoryID"] = "0";
            dr["BrandID"] = "0";
            dr["ProductID"] = "0";
            dr["BrandName"] = "";
            dr["ProductName"] = "";
            dr["ProductValue"] = "0.00";
            dr["Quantity"] = "0";
            //dr["AvailableQuantity"] = "0";
            dr["NetProductTotal"] = "0.00";
           
            dt.Rows.Add(dr);
            Store = new EStoreDeliveryNote();
            dsforSDN = Store.ddlCategory();
            dtCategory = dsforSDN.Tables[0];
            dsforSDN = Store.ddlBrand();
            dtBrand = dsforSDN.Tables[0];
            l = 0;
            gvLineItems.DataSource = dt;
            gvLineItems.DataBind();
            ((TextBox)(gvLineItems.Rows[gvLineItems.Rows.Count - 2].FindControl("txtQunatity"))).Focus();
        
    }
    protected void ddlProductSecond_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    public static string convertToDate(string date)
    {
        string[] _date = date.Split('-');
        if (_date[0].Length == 1)
        {
            _date[0] = 0 + _date[0];
        }
        date = _date[1] + '-' + _date[0] + '-' + _date[2];
        return date;
    }
    protected void btnSaveDeliveryNote_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        try
        {
            hiddenStoreDeliveryNoteId.Value = null;
            EStoreDeliveryNote ESDNobj = new EStoreDeliveryNote();
            dsforSDN = new DataSet();
            ESDNobj.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
             dsforSDN = ESDNobj.GetID();
             string result = Convert.ToString(dsforSDN.Tables[0].Rows[0]["Status"]);
            string StoreCode = Convert.ToString(dsforSDN.Tables[0].Rows[0]["StoreCode"]);
            //if (StoreName.Length > 4)
            //    StoreName = StoreName.Substring(0, 4);
            //else
            StoreCode = StoreCode.Substring(0, StoreCode.Length);
            txtDeliveryNo.Text = StoreCode + '-' + DateTime.Now.ToString("ddMMyyy") + '-' + result;
            if (ddlOrganisation.SelectedValue != "0")
            {
                ESDNobj.OrganisationID = Convert.ToInt32(ddlOrganisation.SelectedValue);
            }
            else
            {
                ESDNobj.OrganisationID = 0;
            }
            if (ddlStore.SelectedValue != "0")
            {
                ESDNobj.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            }
            else
            {
                ESDNobj.StoreID = 0;
            }
            if (txtDeliveryNo.Text != "")
            {
                ESDNobj.DeliveryNoteNo = txtDeliveryNo.Text;
            }
            else
            {
                ESDNobj.DeliveryNoteNo = "";
            }
            if (txtDeliveryDate.Text != "")
            {
                ESDNobj.DeliveryNoteDate = convertToDate(txtDeliveryDate.Text);
            }
            else
            {
                ESDNobj.DeliveryNoteDate = "";
            }
            if (txtPaymentDueDate.Text != "")
            {
                ESDNobj.PaymentDueDate = convertToDate(txtPaymentDueDate.Text);
            }
            else
            {
                ESDNobj.PaymentDueDate = "";
            }

            ESDNobj.LoginID = Convert.ToInt32(Session["LOGINID"]);
            dsforSDN = ESDNobj.InsertSDN();
            if (dsforSDN.Tables[0].Rows.Count > 0)
            {
               // lblStatus.Text = dsforSDN.Tables[0].Rows[0]["Status"].ToString();
                txtDeliveryNo.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["DeliveryNoteNo"]);
                hiddenStoreDeliveryNoteId.Value = Convert.ToString(dsforSDN.Tables[0].Rows[0]["ID"]);
                gvLineItems.Visible = true;
                AddEmtyRows();
                EnableControls(false);
                txttotalgrossvalue.Text = "";
                //txttotaldiscountvalue.Text = "";
                //txtTotalPrice.Text = "";
                txtTotalProductValue.Text = "";
                txtHandlingCharges.Text = "";
                txtRemarks.Text = "";
                SDNCalculation.Visible = true;
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    public void AddEmtyRows()
    {
        try
        {
             dt = new DataTable();
            if (dt.Rows.Count == 0 || dt.Rows.Count < 5)
            {
                dt.Columns.Add("StoreDeliveryNoteDetailID");
                dt.Columns.Add("CategoryID");
                dt.Columns.Add("BrandID");
                dt.Columns.Add("ProductID");
                dt.Columns.Add("BrandName") ;
                dt.Columns.Add("ProductName");
                dt.Columns.Add("ProductValue");
               // dt.Columns.Add("AvailableQuantity");
                dt.Columns.Add("Quantity");              
                dt.Columns.Add("NetProductTotal");
            }
            DataRow dr;
            for (int i = dt.Rows.Count; i < 5; i++)
            {
                dr = dt.NewRow();
                dr["StoreDeliveryNoteDetailID"] = "0";
                dr["CategoryID"] = "0";
                dr["BrandID"] = "0";
                dr["ProductID"] = "0";
                dr["BrandName"] = "";
                dr["ProductName"] = "";
                dr["ProductValue"] = "0.00";
                dr["Quantity"] = "0";
                //dr["AvailableQuantity"] = "0";
                dr["NetProductTotal"] = "0.00";
                
                dt.Rows.Add(dr);
            }
            SDNCalculation.Visible = true;
            dvisave.Visible = true;
            imgSave.Visible = true;
            Divcan1.Visible = true;
            btncancelgrid.Visible = true;
            Store = new EStoreDeliveryNote();
            dsforSDN = Store.ddlCategory();
            dtCategory = dsforSDN.Tables[0];
            dsforSDN = Store.ddlBrand();
            dtBrand = dsforSDN.Tables[0];
            l = 0;
            gvLineItems.DataSource = dt;
            gvLineItems.DataBind();
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
        catch (Exception ex)
        {

            throw ex;
        }
    }
    public void EnableControls(bool status)
    {
        btnSaveDeliveryNote.Visible = status;
        ddlOrganisation.Enabled = status;
        ddlStore.Enabled = status;
        btncancel1.Visible = status;
        txtDeliveryDate.Enabled = status;
        txtPaymentDueDate.Enabled = status;

    }
    protected void gvLineItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        try
        {
            TextBox tt, txtprod, txtbrand;
            HiddenField hd, hd1;
            DropDownList ddlproducts, ddlcard,ddlbrand;            
            int qunatity = 0;
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

                if (Convert.ToInt32(dt.Rows[l]["ProductID"]) != 0)
                {
                    ddlcard.SelectedValue = Convert.ToString(dt.Rows[l]["CategoryID"]);
                    int categoryid = Convert.ToInt32(ddlcard.SelectedValue);
                    ddlbrand.SelectedValue = Convert.ToString(dt.Rows[l]["BrandID"]);
                    int brandid=Convert.ToInt32(ddlbrand.SelectedValue);
                    fillproduct(categoryid,brandid ,ddlproducts);

                    hd = (HiddenField)e.Row.FindControl("HDBrandID");
                    hd.Value = Convert.ToString(dt.Rows[l]["BrandID"]);
                    hd1 = (HiddenField)e.Row.FindControl("HDProductID");
                    hd1.Value = Convert.ToString(dt.Rows[l]["ProductID"]);
                    txtprod = (TextBox)e.Row.FindControl("txtProduct");
                    txtprod.Text = Convert.ToString(dt.Rows[l]["ProductName"]);
                    txtbrand = (TextBox)e.Row.FindControl("txtBrand");
                    txtbrand.Text = Convert.ToString(dt.Rows[l]["BrandName"]);


                    ddlproducts.SelectedValue = Convert.ToString(dt.Rows[l]["ProductID"]);
                    totalgrossvalue += Convert.ToSingle(dt.Rows[l]["NetProductTotal"]);
                    //qunatity = Convert.ToInt32(dt.Rows[l]["Quantity"]);
                    if (Convert.ToString(dt.Rows[l]["Quantity"]) != "")
                        qunatity = Convert.ToInt32(dt.Rows[l]["Quantity"]);
                    else
                        qunatity = 0;

                    if (Convert.ToInt32(dt.Rows[l]["StoreDeliveryNoteDetailID"]) != 0)
                    {
                        txtbrand.Enabled = false;
                        txtprod.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtProductValue");
                        tt.Enabled = false;
                       TextBox tt1 = (TextBox)e.Row.FindControl("txtQunatity");
                        tt1.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtTotal");
                        tt.Enabled = false;
                        //tt = (TextBox)e.Row.FindControl("txtBuyingPrice");
                        //tt.Enabled = false;
                        //tt = (TextBox)e.Row.FindControl("txtNetBuyingPrice");
                        //tt.Enabled = false;

                    }
                    
                }
                if (Convert.ToInt32(dt.Rows[l]["StoreDeliveryNoteDetailID"]) == 0)
                {
                    ImageButton img = (ImageButton)e.Row.FindControl("ImgPrint");
                    img.Visible = false;
                }

                l++;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void fillproduct(int categoryid,int brandid ,DropDownList ddlprod)
    {
        try
        {
            dsforSDN = new DataSet();
            Store = new EStoreDeliveryNote();
            Store.CategoryID = categoryid;
            Store.BrandID = brandid;
            dsforSDN = Store.ddlProduct();
            if (dsforSDN.Tables[0].Rows.Count > 0)
            {
                ddlprod.DataSource = dsforSDN.Tables[0];
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
    protected void gvLineItems_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvLineItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        try
        {
            if (e.CommandName == "DeleteRow")
            {
                string result = string.Empty;
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int rowcount = gvLineItems.Rows.Count;
                DropDownList ddlprod = (DropDownList)gvLineItems.Rows[rowindex].FindControl("ddlProduct");

                if (rowcount != 1)
                {
                    int storedeliverynotedeatilid = Convert.ToInt32(((HiddenField)gvLineItems.Rows[rowindex].FindControl("HDStoreDeliveryNoteDetailID")).Value);
                    if (storedeliverynotedeatilid != 0)
                    {
                        Store = new EStoreDeliveryNote();
                        Store.LoginID = Convert.ToInt32(Session["LOGINID"]);
                        Store.StoreDeliveryNoteDetailID = storedeliverynotedeatilid;
                        Store.ProductID = Convert.ToInt32(ddlprod.SelectedValue);
                        Store.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
                        result = Store.DeleteSDND();
                        int storedeliverynoteid = Convert.ToInt32(hiddenStoreDeliveryNoteId.Value);
                        EditFunction(storedeliverynoteid);
                    }
                    else
                    {
                        dt = new DataTable();
                        dt.Columns.Add("StoreDeliveryNoteDetailID");
                        dt.Columns.Add("CategoryID");
                        dt.Columns.Add("BrandID");
                        dt.Columns.Add("ProductID");
                        dt.Columns.Add("ProductValue");
                        dt.Columns.Add("BrandName");
                        dt.Columns.Add("ProductName");
                        dt.Columns.Add("Quantity");
                        dt.Columns.Add("NetProductTotal");
                        DataRow dr;
                        for (int i = 0; i <= gvLineItems.Rows.Count - 2; i++)
                        {

                            dr = dt.NewRow();                            
                            dr["StoreDeliveryNoteDetailID"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDStoreDeliveryNoteDetailID")).Value;
                            dr["CategoryID"] = ((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue;
                            dr["BrandID"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value;
                            //if (((DropDownList)gvLineItems.Rows[i].FindControl("ddlProduct")).SelectedValue == "")
                            //    dr["ProductID"] = "0";
                            //else
                            dr["ProductID"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value;
                            dr["ProductValue"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtProductValue")).Text;
                            dr["BrandName"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtBrand")).Text;
                            dr["ProductName"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtProduct")).Text;
                            //dr["AvailableQuantity"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtAvailableQuantity")).Text;
                            dr["Quantity"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtQunatity")).Text;
                            dr["NetProductTotal"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtTotal")).Text;
                            dt.Rows.Add(dr);
                        }
                        Store = new EStoreDeliveryNote();
                        dsforSDN = Store.ddlCategory();
                        dtCategory = dsforSDN.Tables[0];
                        dsforSDN = Store.ddlBrand();
                        dtBrand = dsforSDN.Tables[0];
                        l = 0;
                        gvLineItems.DataSource = dt;
                        gvLineItems.DataBind();
                    }
                }
                else
                {
                    int storedeliverynotedeatilid = Convert.ToInt32(((HiddenField)gvLineItems.Rows[rowindex].FindControl("HDStoreDeliveryNoteDetailID")).Value);
                    if (storedeliverynotedeatilid != 0)
                    {
                        Store = new EStoreDeliveryNote();
                        Store.StoreDeliveryNoteDetailID = storedeliverynotedeatilid;
                        result = Store.DeleteSDND();
                    }
                    AddEmtyRows();
                }
                lblStatus.Text = result;
            }
            else if (e.CommandName == "PrintRow")
            {
                int rowindex =Convert.ToInt32(e.CommandArgument);
                string productname = Convert.ToString(((TextBox)(gvLineItems.Rows[rowindex].FindControl("txtProduct"))).Text);
                if (productname == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "<script language='javascript'  type='text/javascript'>;alert('Please Fill Modal No.');</script>", false);
                    return;
                }
                else
                {


                    int storedeliverynotedetailie = Convert.ToInt32(((HiddenField)gvLineItems.Rows[rowindex].FindControl("HDStoreDeliveryNoteDetailID")).Value);
                    Store = new EStoreDeliveryNote();
                    if (storedeliverynotedetailie != 0)
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Printalert", "StoreForPrint1(" + storedeliverynotedetailie + ")", true);

                    }
                }
            }
        }
        catch (Exception ex)
        {

            lblStatus.Text = ex.Message;
        }
    }
    protected void ddlCardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        
    }
   
    protected void searchDeliveryNote_Click(object sender, EventArgs e)
    {
        try
        {
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            lblSuccess.Text = "";
            lblStatus.Text = "";   
            FillGrid();
        }
        catch (Exception ex)
        {

            lblStatus.Text = ex.Message;
        }
    }
    protected void FillGrid()
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        try
        {
             Store = new EStoreDeliveryNote();
            if (ddlSearchOraganisation.SelectedValue != "0" )
            {
                Store.OrganisationID = Convert.ToInt32(ddlSearchOraganisation.SelectedValue);
            }
            else
            {
                Store.OrganisationID = 0;
            }
            if (ddlSearchStore.SelectedValue != "0")
            {
                if(ddlSearchStore.SelectedValue != "")
                Store.StoreID = Convert.ToInt32(ddlSearchStore.SelectedValue);
            }
            else
            {
                Store.StoreID = 0;
            }
            if (txtSearchDNNO.Text != "")
            {
                Store.DeliveryNoteNo = txtSearchDNNO.Text;
            }
            else
            {
                Store.DeliveryNoteNo = "";
            }
            if (txtDeliveryNoteFromDate.Text != "")
            {
                Store.FromDate =convertToDate(txtDeliveryNoteFromDate.Text);
            }
            else
            {
                Store.FromDate = "";
            }
            if (txtDeliveryNoteToDate.Text != "")
            {
                Store.ToDate =convertToDate(txtDeliveryNoteToDate.Text);
            }
            else
            {
                Store.ToDate = "";
            }
            Store.LoginID = Convert.ToInt32(Session["LOGINID"]);
           
                Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            
            dsforSDN = new DataSet();
            dsforSDN = Store.StorGrid();
            if (dsforSDN.Tables[0].Rows.Count > 0)
            {
                grdStoreDeliveryNote.Visible = true;
                grdStoreDeliveryNote.DataSource = dsforSDN.Tables[0];
                grdStoreDeliveryNote.DataBind();
                lblStatus.Text = "";
                panelSearchDeliveryNote.Visible = true;
                ddlSearchStore.SelectedValue = "0";
                txtSearchDNNO.Text = "";
               
            }
            else
            {
                grdStoreDeliveryNote.DataSource = null;
                grdStoreDeliveryNote.DataBind();
                dvFailure.Visible = true;
                lblStatus.Text = "No Record(s) Found";
                grdStoreDeliveryNote.Visible = false;
                ddlSearchStore.SelectedValue = "0";
                txtSearchDNNO.Text = "";
            }
            if (grdStoreDeliveryNote.HeaderRow != null)
                grdStoreDeliveryNote.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void grdStoreDeliveryNote_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdStoreDeliveryNote_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string remarks = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        try
        {
            if (e.CommandName == "View")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int storedeliverynoteid = Convert.ToInt32(grdStoreDeliveryNote.DataKeys[rowindex].Value);
                Store = new EStoreDeliveryNote();
                dsforSDN = new DataSet();
                Store.StoreDeliveryNoteID = storedeliverynoteid;
                dsforSDN = Store.StorGridForViewEdit();
                if (dsforSDN.Tables[0].Rows.Count > 0)
                {
                    ddlOrganisation.SelectedValue = Convert.ToString(dsforSDN.Tables[0].Rows[0]["OrganisationID"]);
                    ddlStore.SelectedValue = Convert.ToString(dsforSDN.Tables[0].Rows[0]["StoreID"]);
                    txtDeliveryNo.Text = dsforSDN.Tables[0].Rows[0]["StoreDeliveryNoteNO"].ToString();
                    txtDeliveryDate.Text = dsforSDN.Tables[0].Rows[0]["StoreDeliveryNoteDate"].ToString();
                    txtPaymentDueDate.Text = dsforSDN.Tables[0].Rows[0]["PaymentDueDate"].ToString();
                    btnSaveDeliveryNote.Visible = false;

                    txtHandlingCharges.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["HandlingCharges"]);
                    //txtTotalPrice.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["NetProductValue"]);
                    txtTotalProductValue.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["TotalValue"]);
                    txtRemarks.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["Remarks"]);
                    ShowTabs(2);
                    EnableControls(false);
                    btnCancel.Visible = false;
                    dvcancel.Visible = false;
                    btnSaveBatch.Visible = false;
                    int noneditablerows = 0;
                    if (dsforSDN.Tables[1].Rows.Count > 0)
                    {
                        noneditablerows = dsforSDN.Tables[1].Rows.Count;
                        SDNCalculation.Visible = true;
                        gvLineItems.Visible = true;
                        dt = dsforSDN.Tables[1];
                        dsforSDN = Store.ddlCategory();
                        l = 0;
                        dtCategory = dsforSDN.Tables[0];
                        dsforSDN = Store.ddlBrand();
                        dtBrand = dsforSDN.Tables[0];
                        totalgrossvalue = 0;
                        totaldiscount = 0;
                        gvLineItems.DataSource = dt;
                        gvLineItems.DataBind();
                        gvLineItems.Visible = true;
                        txttotalgrossvalue.Text = totalgrossvalue.ToString();
                        //txttotaldiscountvalue.Text = totaldiscount.ToString();
                        for (int i = 0; i < noneditablerows; i++)
                        {
                            LinkButton im = (LinkButton)gvLineItems.Rows[i].FindControl("imgDeleteRow");
                            im.Visible = false;
                            //im = (LinkButton)gvLineItems.Rows[i].FindControl("imgBrand");
                            //im.Visible = false;
                            //im = (LinkButton)gvLineItems.Rows[i].FindControl("imgProduct");
                            //im.Visible = false;
                        }

                    }
                    else
                    {
                        AddEmtyRows();
                        gvLineItems.Visible = true;
                    }
                    ControlStatus();
                    imgSave.Visible = false;
                    dvisave.Visible = false;
                    dvcancel.Visible = false;
                    btncancel1.Visible = false;
                    btncancelgrid.Visible = true;
                }
                lnkAdd.Text = "View";
            }
            else if (e.CommandName == "Editing")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int storedeliverynoteid = Convert.ToInt32(grdStoreDeliveryNote.DataKeys[rowindex].Value);
                EditFunction(storedeliverynoteid);
                lnkAdd.Text = "Edit";
                dvisave.Visible = true;
                imgSave.Visible = true;
                btncancel1.Visible = false;
                btncancelgrid.Visible = true;
                //imgSaveDisabled.Visible = false;
            }
            else if (e.CommandName == "Deleting")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int storedeliverynoteid = Convert.ToInt32(grdStoreDeliveryNote.DataKeys[rowindex].Value);
                Store = new EStoreDeliveryNote();
                dsforSDN = new DataSet();
                Store.StoreDeliveryNoteID = storedeliverynoteid;
                Store.LoginID = Convert.ToInt32(Session["LoginId"]);
                string result = Store.DeleteSDN();
                
                ShowTabs(1);
                FillGrid();
                dvSuccess.Visible = true;
                lblSuccess.Text = result;
                // lblStatus.Text = result;
            }
            else if (e.CommandName == "Print")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int storedeliverynoteid = Convert.ToInt32(grdStoreDeliveryNote.DataKeys[rowindex].Value);
                Store = new EStoreDeliveryNote();

                if (storedeliverynoteid != 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Printalert", "StoreForPrint(" + storedeliverynoteid + ")", true);

                }

            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    [WebMethod]
    public static GridDataSet StoreForPrint(int Storeid)
    {
        
        DataSet dsforstore = new DataSet();
        EStoreDeliveryNote estore = new EStoreDeliveryNote();
        EStoreDeliveryList liststore = new EStoreDeliveryList();
        List<EStoreDeliveryNote> estorelist = new List<EStoreDeliveryNote>();
        List<EStoreDeliveryList> estoretable = new List<EStoreDeliveryList>();
        GridDataSet ObjGridData = new GridDataSet();
        estore.StoreDeliveryNoteID = Storeid;
        DataSet dsforSDN = new DataSet();
        DataSet ds = new DataSet();
        DataSet dstotal = new DataSet();
        
           
        try
        {
            dsforSDN = estore.DataToPrint();
            if (dsforSDN.Tables.Count > 0)
            {
                if (dsforSDN.Tables[0].Rows.Count > 0)
                {

                    estore.StoreName = dsforSDN.Tables[0].Rows[0]["StoreName"].ToString();
                    estore.Vatid = dsforSDN.Tables[0].Rows[0]["VATID"].ToString();
                    estore.DeliveryNoteNo = dsforSDN.Tables[0].Rows[0]["StoreDeliveryNoteNo"].ToString();
                    
                    estore.DeliveryNoteDate = dsforSDN.Tables[0].Rows[0]["DeliveryNoteDate"].ToString();
                    estore.Remarks = dsforSDN.Tables[0].Rows[0]["Remarks"].ToString();
 
                }
                if (dsforSDN.Tables[1].Rows.Count > 0)
                {
                    estore.OrganisationName = dsforSDN.Tables[1].Rows[0]["OrganisationName"].ToString();
                    estore.Address = dsforSDN.Tables[1].Rows[0]["Address"].ToString();
                    estore.City = dsforSDN.Tables[1].Rows[0]["City"].ToString();
                    estore.ContactNum = dsforSDN.Tables[1].Rows[0]["ContactNumber"].ToString();
                    estore.Email = dsforSDN.Tables[1].Rows[0]["Email"].ToString();
                    
                }
               
            }
            ds = estore.PrintGridStoreDetails();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                liststore = new EStoreDeliveryList();
                liststore.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                liststore.BrandName = ds.Tables[0].Rows[i]["BrandName"].ToString();
                liststore.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
                liststore.ProductValue = Convert.ToSingle(ds.Tables[0].Rows[i]["ProductValue"]);
                liststore.Quantity = Convert.ToSingle(ds.Tables[0].Rows[i]["Quantity"]);
                liststore.GrossValue = Convert.ToSingle(ds.Tables[0].Rows[i]["TotalGrossValue"]);
                estoretable.Add(liststore);
            }
            dstotal = estore.PrintTotalCharges();
            if (dstotal.Tables[0].Rows.Count > 0)
            {
                estore.TotalGrossValue=Convert.ToDecimal(dstotal.Tables[0].Rows[0]["TotalNetProductValue"]);
                estore.HandlingCharges = Convert.ToDecimal(dstotal.Tables[0].Rows[0]["TotalHandlingCharges"]);
                estore.TotalValue = Convert.ToDecimal(dstotal.Tables[0].Rows[0]["Total"]);
               
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
    public void btnPrintQuantity_Click(object sender, EventArgs e)
    {
        try
        {
           
            int productId = Convert.ToInt32(hdProductIdforprint.Value);
            EStoreDeliveryNote estore = new EStoreDeliveryNote();
            estore.ProductID = productId;
            ds = estore.GetProductDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                string productname = ds.Tables[0].Rows[0]["ProductName"].ToString();
                string productvalue = ds.Tables[0].Rows[0]["ProductValue"].ToString();
                string brandname = ds.Tables[0].Rows[0]["BrandName"].ToString();
                TSCLIB_DLL.openport("TSC TA200");                                           //Open specified printer driver
                TSCLIB_DLL.setup("40", "20", "4", "8", "0", "1", "0");                           //Setup the media size and sensor type info
                TSCLIB_DLL.clearbuffer();                                                           //Clear image buffer
                TSCLIB_DLL.barcode("300", "35", "128", "36", "0", "0", "1", "2", productname); //Drawing barcode
                TSCLIB_DLL.printerfont("300", "80", "3", "0", "1", "1",productname);
                TSCLIB_DLL.printerfont("100", "35", "3", "0", "1", "1", brandname);
                TSCLIB_DLL.printerfont("100", "80", "3", "0", "1", "1", "SR-"+productvalue);//Drawing printer font            
                TSCLIB_DLL.printlabel("1", "1");                                                    //Print labels
                TSCLIB_DLL.closeport();
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    [WebMethod]
    public static GridDataSet StoreForPrint1(int Storeid)
    {

        DataSet dsforstore = new DataSet();
        EStoreDeliveryNote estore = new EStoreDeliveryNote();
        EStoreDeliveryList liststore = new EStoreDeliveryList();
        List<EStoreDeliveryNote> estorelist = new List<EStoreDeliveryNote>();
        List<EStoreDeliveryList> estoretable = new List<EStoreDeliveryList>();
        GridDataSet ObjGridData = new GridDataSet();
        estore.StoreDeliveryNoteDetailID = Storeid;
        DataSet dsforSDN = new DataSet();
        DataSet ds = new DataSet();
        DataSet dstotal = new DataSet();


        try
        {
            dsforSDN = estore.DataToPrint1();
            if (dsforSDN.Tables.Count > 0)
            {
                if (dsforSDN.Tables[0].Rows.Count > 0)
                {

                    estore.StoreName = dsforSDN.Tables[0].Rows[0]["StoreName"].ToString();
                    estore.Vatid = dsforSDN.Tables[0].Rows[0]["VATID"].ToString();
                    estore.DeliveryNoteNo = dsforSDN.Tables[0].Rows[0]["StoreDeliveryNoteNo"].ToString();

                    estore.DeliveryNoteDate = dsforSDN.Tables[0].Rows[0]["DeliveryNoteDate"].ToString();
                    estore.Remarks = dsforSDN.Tables[0].Rows[0]["Remarks"].ToString();
                    estore.imagename = dsforSDN.Tables[0].Rows[0]["BarcodePath"].ToString();
                }
                if (dsforSDN.Tables[1].Rows.Count > 0)
                {
                    estore.OrganisationName = dsforSDN.Tables[1].Rows[0]["OrganisationName"].ToString();
                    estore.Address = dsforSDN.Tables[1].Rows[0]["Address"].ToString();
                    estore.City = dsforSDN.Tables[1].Rows[0]["City"].ToString();
                    estore.ContactNum = dsforSDN.Tables[1].Rows[0]["ContactNumber"].ToString();
                    estore.Email = dsforSDN.Tables[1].Rows[0]["Email"].ToString();

                }

            }
            ds = estore.PrintGridStoreDetails1();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                liststore = new EStoreDeliveryList();
                liststore.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                liststore.BrandName = ds.Tables[0].Rows[i]["BrandName"].ToString();
                liststore.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
                liststore.ProductValue = Convert.ToSingle(ds.Tables[0].Rows[i]["ProductValue"]);
                liststore.Quantity = Convert.ToSingle(ds.Tables[0].Rows[i]["Quantity"]);
                liststore.GrossValue = Convert.ToSingle(ds.Tables[0].Rows[i]["TotalGrossValue"]);
                liststore.ProductId = Convert.ToInt32(ds.Tables[0].Rows[i]["ProductID"]);
                //txtQuantityForPrint.Text = Convert.ToString(ds.Tables[0].Rows[i]["Quantity"]);
                estoretable.Add(liststore);
            }
            dstotal = estore.PrintTotalCharges1();
            if (dstotal.Tables[0].Rows.Count > 0)
            {
                estore.TotalGrossValue = Convert.ToDecimal(dstotal.Tables[0].Rows[0]["TotalNetProductValue"]);
                estore.HandlingCharges = Convert.ToDecimal(dstotal.Tables[0].Rows[0]["TotalHandlingCharges"]);
                estore.TotalValue = Convert.ToDecimal(dstotal.Tables[0].Rows[0]["Total"]);

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
    protected void lnkbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        //Mp1.Show();
    }
    //protected void close_OnClick(object sender, EventArgs e)
    //{
    //    Mp1.Hide();
    //}

   



    protected void EditFunction(int storedeliverynoteid)
    {
        try
        {
            Store = new EStoreDeliveryNote();
            dsforSDN = new DataSet();
            Store.StoreDeliveryNoteID = storedeliverynoteid;
            hiddenStoreDeliveryNoteId.Value = storedeliverynoteid.ToString();
            dsforSDN = Store.StorGridForViewEdit();
            if (dsforSDN.Tables[0].Rows.Count > 0)
            {
                ddlOrganisation.SelectedValue = Convert.ToString(dsforSDN.Tables[0].Rows[0]["OrganisationID"]);
                ddlStore.SelectedValue = Convert.ToString(dsforSDN.Tables[0].Rows[0]["StoreID"]);
                txtDeliveryNo.Text = dsforSDN.Tables[0].Rows[0]["StoreDeliveryNoteNO"].ToString();
                txtDeliveryDate.Text = dsforSDN.Tables[0].Rows[0]["StoreDeliveryNoteDate"].ToString();
                txtPaymentDueDate.Text = dsforSDN.Tables[0].Rows[0]["PaymentDueDate"].ToString();
                btnSaveDeliveryNote.Visible = false;

                txtHandlingCharges.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["HandlingCharges"]);
                //txtTotalPrice.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["NetProductValue"]);
                txtTotalProductValue.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["TotalValue"]);
                txtRemarks.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["Remarks"]);
                ShowTabs(2);
                EnableControls(false);
                btnCancel.Visible = false;
                btnSaveBatch.Visible = false;
                imgSave.Visible = true;
                if (dsforSDN.Tables[1].Rows.Count > 0)
                {
                    SDNCalculation.Visible = true;
                    gvLineItems.Visible = true;
                    dt = dsforSDN.Tables[1];

                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();
                    dr["StoreDeliveryNoteDetailID"] = "0";
                    dr["CategoryID"] = "0";
                    dr["BrandID"] = "0";
                    dr["ProductID"] = "0";
                    dr["ProductValue"] = "0.00";
                    dr["Quantity"] = "0";
                   // dr["AvailableQuantity"] = "0";
                    dr["NetProductTotal"] = "0.00";
                    dt.Rows.Add(dr);
                    dsforSDN = Store.ddlCategory();
                    l = 0;
                    dtCategory = dsforSDN.Tables[0];
                    dsforSDN = Store.ddlBrand();
                    dtBrand = dsforSDN.Tables[0];
                    totalgrossvalue = 0;
                    totaldiscount = 0;
                    gvLineItems.DataSource = dt;
                    gvLineItems.DataBind();
                    gvLineItems.Visible = true;
                    txttotalgrossvalue.Text = totalgrossvalue.ToString();
                    //txttotaldiscountvalue.Text = totaldiscount.ToString();
                    ((TextBox)gvLineItems.Rows[gvLineItems.Rows.Count - 1].FindControl("txtProduct")).Focus();
                }
                else
                {
                    AddEmtyRows();
                    gvLineItems.Visible = true;
                    ((TextBox)gvLineItems.Rows[0].FindControl("txtProduct")).Focus();

                }
                imgSave.Visible = true;
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
            }
        }
        catch (Exception)
        {
            
            throw;
        }
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
        try
        {
            TextBox txt;
            DropDownList drdnlCategory;
            DropDownList drdnlProduct;
            HiddenField HD;
            float ProductValue = 0;
            float BuyingDiscount = 0;
            int Quantity = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                drdnlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                drdnlProduct = (DropDownList)e.Row.FindControl("ddlProduct");
                
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }
    }
    protected void grdStoreDeliveryNote_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdStoreDeliveryNote_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    //protected void btnprint_Click(object sender, EventArgs e)
    //{

    //}
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
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlSearchOraganisation.SelectedIndex = 0;
        ddlSearchStore.SelectedIndex = 0;
        txtSearchDNNO.Text = "";
        txtDeliveryNoteFromDate.Text = "";
        txtDeliveryNoteToDate.Text = "";
        FillGrid();
        if (grdStoreDeliveryNote.HeaderRow != null)
            grdStoreDeliveryNote.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
   protected void imgSave_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        try
        {
            Store = new EStoreDeliveryNote();

            string GridData = string.Empty;
            int storedeliverynotedetailid = 0;
            for (int i = 0; i < gvLineItems.Rows.Count; i++)
            {
                storedeliverynotedetailid = Convert.ToInt32(((HiddenField)gvLineItems.Rows[i].FindControl("HDStoreDeliveryNoteDetailID")).Value);
                if (storedeliverynotedetailid == 0)
                {
                    if (((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" && ((DropDownList)gvLineItems.Rows[i].FindControl("ddlProduct")).SelectedValue != "0")
                        GridData += ((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue + "~"
                            + ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value + "~"
                            + ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value + "~"
                            + ((TextBox)gvLineItems.Rows[i].FindControl("txtProductValue")).Text + "~"
                            + ((TextBox)gvLineItems.Rows[i].FindControl("txtQunatity")).Text + "~"
                           // + ((TextBox)gvLineItems.Rows[i].FindControl("txtAvailableQuantity")).Text + "~"
                            + ((TextBox)gvLineItems.Rows[i].FindControl("txtTotal")).Text + "$";
                }
            }
            if (GridData != "")
                GridData = GridData.Substring(0, GridData.Length - 1);
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "<script language='javascript'  type='text/javascript'>;alert('Please Fill Required Fields');</script>", false);
                return;
            }
            Store.GridData = GridData;
            Store.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            if (txttotalgrossvalue.Text != "")
                Store.NetProductValue = Convert.ToDecimal(txttotalgrossvalue.Text);
            else
                Store.NetProductValue = 0;
            if (txtHandlingCharges.Text != "")
                Store.HandlingCharges = Convert.ToDecimal(txtHandlingCharges.Text);
            else
                Store.HandlingCharges = 0;
            if (txtTotalProductValue.Text != "")
            {
                Store.TotalValue = Convert.ToDecimal(txtTotalProductValue.Text);
            }
            else
                Store.TotalValue = 0;
            Store.Remarks = txtRemarks.Text;
            Store.StoreDeliveryNoteID = Convert.ToInt32(hiddenStoreDeliveryNoteId.Value);
            Store.LoginID = Convert.ToInt32(Session["LOGINID"]);
            string result = Store.InsertSDND();
            if (result == "Record Updated Successfully.")
            {
                ShowTabs(1);
                FillGrid();
                dvSuccess.Visible = true;
               lblSuccess.Text = result;
                int storedeliverynoteid = Convert.ToInt32(hiddenStoreDeliveryNoteId.Value);
                if (storedeliverynoteid != 0)
                {
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Printalert", "StoreForPrint(" + storedeliverynoteid + ")", true);

                } 
            }
            else
            {
                dvFailure.Visible = true;
                lblStatus.Text = result;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void ControlStatus()
    {
        txttotalgrossvalue.ReadOnly = true;
        //txttotaldiscountvalue.ReadOnly = true;
        //txtTotalPrice.ReadOnly = true;
        txtHandlingCharges.ReadOnly = true;
        txtTotalProductValue.ReadOnly = true;
        txtRemarks.ReadOnly = true;
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
            result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            if (result == "Record Inserted Succesfully.")
            {
                //ShowTabs(1);
                dvSuccess.Visible = true;
                lblSuccess.Text = result;
                //lblStatus.CssClass = "SuccessMsg";
                dvProductPopUp.Attributes["style"] = "display:none";
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "removestyle();", true);

            }
            else
            {

                //lblStatusProduct.CssClass = "ErrorMsg";
                clearproductpopfields();
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
    protected void imgAddBrand_Click(object sender, ImageClickEventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
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
        dvBrandPopUp.Attributes["style"] = "display:block;width:600px;height:250px;";
        lblStatusBrand.Text = "";
    }
    protected void imgProduct_onclick(object sender, ImageClickEventArgs e)
    {
        clearproductpopfields();
    }
    protected void clearproductpopfields()
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
    protected void txtProduct_ontextchanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddl = (DropDownList)gr.FindControl("ddlCategory");
            TextBox txtproduct = (TextBox)gr.FindControl("txtProduct");
            TextBox txtbrand = (TextBox)gr.FindControl("txtBrand");
            TextBox prodvalue = (TextBox)gr.FindControl("txtProductValue");
            TextBox txt = (TextBox)gr.FindControl("txtQunatity");
            HiddenField hdprodid = (HiddenField)gr.FindControl("HDProductID");
            HiddenField hdbrandid = (HiddenField)gr.FindControl("HDBrandID");
            
            Store = new EStoreDeliveryNote();
            bool ok=false;
            if (txtproduct.Text != "")
            {
                Store.ProductName = txtproduct.Text.Trim();
                int categoryid = 0, brandid = 0;
                if (ddl.SelectedValue != "0")
                    categoryid = Convert.ToInt32(ddl.SelectedValue);
                else
                    categoryid = 0;
                Store.CategoryID = categoryid;
                if (hdbrandid.Value != "" && hdbrandid.Value != "0")
                    brandid = Convert.ToInt32(hdbrandid.Value);
                else
                    brandid = 0;

                Store.BrandName = txtbrand.Text.Trim();
                if (txtbrand.Text == "")
                    brandid = 0;
                Store.BrandID = brandid;
                ds = new DataSet();
                if (categoryid == 0 || brandid == 0)
                {
                    ds = Store.GetProductCategorybrandID();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CategoryID"]);
                        hdbrandid.Value = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                        hdprodid.Value = Convert.ToString(ds.Tables[0].Rows[0]["ProductID"]);
                        prodvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                        txtbrand.Text = Convert.ToString(ds.Tables[0].Rows[0]["BrandName"]);
                        ok = true;
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
                        ok = false;
                        txt.Text = "0";
                        txtproduct.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "<script language='javascript' type='text/javascript'>;alert('Product Quantity Not available');</script>", false);
                        return;
                    }
                }
                else
                {
                    ds = Store.GetProductIDandValue();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        prodvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                        //hdbrandid.Value = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                        hdprodid.Value = Convert.ToString(ds.Tables[0].Rows[0]["ProductID"]);
                        //txtbrand.Text = Convert.ToString(ds.Tables[0].Rows[0]["BrandName"]);
                        //hdbrandid.Value = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                        //ddl.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CategoryID"]);
                        ok = true;
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
                        ok = false;
                        txtproduct.Focus();
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "<script language='javascript' type='text/javascript'>;alert('Product Quantity Not available');</script>", false);
                        return;
                       
                    }
                }
                int qty = 0;

                if (txt.Text != "0" && txt.Text != "")
                    qty = Convert.ToInt32(txt.Text);
                if (qty != 0)
                {
                    ((TextBox)gr.FindControl("txtTotal")).Text = Convert.ToString(qty * Convert.ToSingle(prodvalue.Text));
                }

                int rowcount = gvLineItems.Rows.Count;
                txtproduct = sender as TextBox;
                string ID = txtproduct.ClientID;
                ID = ID.Replace("ctl00_ContentPlaceHolder1_gvLineItems_ctl", "");
                ID = ID.Replace("_txtProduct", "");
                int gvcount = gvLineItems.Rows.Count - 1;
                int gvrowindex = gr.RowIndex;
           
               // int maxrow = Convert.ToInt32(ID) + 1;
                if (ok)
                {
                    if (gvcount == gvrowindex)
                    {
                        AddNewRow();
                    }
                    else
                    {
                        txt.Focus();
                    }
                }
                
                float totalgrossvalue = 0, handlingcharges = 0, totalvalue = 0;

                foreach (GridViewRow g in gvLineItems.Rows)
                {
                    TextBox txtbox = (TextBox)g.FindControl("txtTotal");
                    if (txtbox.Text != "" && txtbox.Text != "0.00")
                    {
                        totalgrossvalue += Convert.ToSingle(txtbox.Text);

                    }
                }
                txttotalgrossvalue.Text = totalgrossvalue.ToString();
                if (txtHandlingCharges.Text != "0.00" && txtHandlingCharges.Text != "")
                    handlingcharges = Convert.ToSingle(txtHandlingCharges.Text);
                else
                    handlingcharges = 0;
                txtTotalProductValue.Text = Convert.ToString(totalgrossvalue + Convert.ToSingle(handlingcharges));
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
                Store = new EStoreDeliveryNote();
                Store.BrandName = txtbrand.Text.Trim();
                ds = new DataSet();
                ds = Store.GetBrandID();
                int brandid = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    brandid = Convert.ToInt32(ds.Tables[0].Rows[0]["BrandID"]);
                    hd.Value = brandid.ToString();
                    AjaxControlToolkit.AutoCompleteExtender ae = (AjaxControlToolkit.AutoCompleteExtender)gr.FindControl("AutoCompleteExtender2");
                    ae.ContextKey = ddl.SelectedValue + "~" + brandid.ToString();
                }

            }
            txtbrand.Focus();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
