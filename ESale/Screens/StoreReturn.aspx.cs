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


public partial class Screens_StoreReturn : System.Web.UI.Page
{
    int l = 0;
    EStoreReturn Store;
    DataSet dsforSDN;
    DataTable dtCategory, dtBrand;
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
    int noneditablerows = 0;
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
            lblStatus.Text = string.Empty;
            lblSuccess.Text = string.Empty;
            dvFailure.Visible = false;
            dvSuccess.Visible = false;

            DateTime baseDate = DateTime.Today;
            var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
            string startdate = thisMonthStart.ToString("dd-MM-yyyy");
            txtFromDate.Text = startdate;
            var thisMonthEnd = thisMonthStart.AddMonths(1).Date.AddSeconds(-1);
            string enddate = thisMonthEnd.ToString("dd-MM-yyyy");
            txtToDate.Text = enddate;
            FillGrid();
            ShowTabs(1);
        }
        if (gvStoreReturn.HeaderRow != null)
        gvStoreReturn.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    private void FillOrganisation()
    {
        try
        {
            Store = new EStoreReturn();
            ds = new DataSet();
            ds = Store.ddlOrganisation();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlOrganisation.DataSource = ds.Tables[0];
                ddlOrganisation.DataTextField = "OrganisationName";
                ddlOrganisation.DataValueField = "OrganisationID";
                ddlOrganisation.DataBind();
                ddlOrganisation.Items.Insert(0, new ListItem("--Any--", "0"));

                ddlOraganisationSearch.DataSource = ds.Tables[0];
                ddlOraganisationSearch.DataTextField = "OrganisationName";
                ddlOraganisationSearch.DataValueField = "OrganisationID";
                ddlOraganisationSearch.DataBind();
                ddlOraganisationSearch.Items.Insert(0, new ListItem("--Any--", "0"));
                if (ds.Tables[0].Rows.Count == 1)
                {
                    ddlOraganisationSearch.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationID"]);
                    ddlOrganisation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationID"]);
                    ddlOrganisation.Enabled = false;
                    ddlOraganisationSearch.Enabled = false;
                }
            }
            else
            {
                ddlOrganisation.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlOraganisationSearch.Items.Insert(0, new ListItem("--Any--", "0"));
            }
        }
        catch (Exception)
        {

            throw;
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
    public void FillStore()
    {
        dsforSDN = new DataSet();
        Store = new EStoreReturn();
        try
        {
            Store.OrganisationId = Convert.ToInt32(ddlOraganisationSearch.SelectedValue);
            Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
            Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            dsforSDN = Store.ddlStore();
            if (dsforSDN.Tables[0].Rows.Count > 0)
            {
                ddlStoreSearch.DataSource = dsforSDN;
                ddlStoreSearch.DataValueField = "StoreID";
                ddlStoreSearch.DataTextField = "StoreName";
                ddlStoreSearch.DataBind();
                ddlStoreSearch.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlStore.DataSource = dsforSDN;
                ddlStore.DataValueField = "StoreID";
                ddlStore.DataTextField = "StoreName";
                ddlStore.DataBind();
                ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));

            }
            else
            {
                ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlStoreSearch.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            if (Convert.ToInt32(Session["LOGINID"]) != 1 && dsforSDN.Tables[0].Rows.Count == 1)
            {
                ddlStore.SelectedValue = Convert.ToString(dsforSDN.Tables[0].Rows[0]["StoreID"]);
                ddlStoreSearch.SelectedValue = Convert.ToString(dsforSDN.Tables[0].Rows[0]["StoreID"]);
                ddlStoreSearch.Enabled = false;
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
            FillGrid();
            //grdStoreDeliveryNote.PageIndex = 0;
            //txtDeliveryNoteFromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            //txtDeliveryNoteToDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");


            //FillGrid();
        }
        else if (Num == 2)
        {
            panelAddUser.Visible = true;
            gvLineItems.Visible = false;
            panelSearchDeliveryNote.Visible = false;
            lnkAdd.Visible = false;
            lnkAdd.Text = "Add";
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

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblStatus.Text = string.Empty;
        txtReturnDate.Text = System.DateTime.UtcNow.AddHours(3).ToString("dd-MM-yyyy");
        txtReturnNo.Text = "";
        txtReturnNo.Enabled = true;
        ddlStore.SelectedValue = "0";
        ShowTabs(2);
        STRCalculation.Visible = false;
        btnSaveReturn.Visible = true;
        dvsave.Visible = true;
        dvcancel.Visible = true;
        btncancel1.Visible = true;
      //  btncancelgrid.Visible = false;
    }
    protected void FillGrid()
    {
        try
        {
            Store = new EStoreReturn();
            if (ddlOraganisationSearch.SelectedValue != "0")
                Store.OrganisationId = Convert.ToInt32(ddlOraganisationSearch.SelectedValue);
            else
                Store.OrganisationId = 0;
            if (ddlStoreSearch.SelectedValue != "0")
                Store.StoreId = Convert.ToInt32(ddlStoreSearch.SelectedValue);
            else
                Store.StoreId = 0;

            if (txtReturnNoSearch.Text != "")
                Store.ReturnNumber = txtReturnNoSearch.Text;
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
            Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            Store.LoginId = Convert.ToInt32(Session["LoginId"]);
            Store.StoreId = Convert.ToInt32(ddlStoreSearch.SelectedValue);
            ds = new DataSet();
            ds = Store.Grid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvStoreReturn.DataSource = ds.Tables[0];
                gvStoreReturn.DataBind();
                gvStoreReturn.HeaderRow.TableSection = TableRowSection.TableHeader;
                lblStatus.Text = "";
                panelSearchDeliveryNote.Visible = true;
                gvStoreReturn.Visible = true;
                txtReturnNoSearch.Text = "";

            }
            else
            {
                gvStoreReturn.DataSource = null;
                gvStoreReturn.DataBind();
                dvFailure.Visible = true;
                lblStatus.Text = "No Records found.";
                panelSearchDeliveryNote.Visible = true;
                panelAddUser.Visible = false;
                gvStoreReturn.Visible = false;
                //ddlSupplierSearch.SelectedValue = "0";
                txtReturnNoSearch.Text = "";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        FillGrid();
    }
    protected void gvStoreReturn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        if (e.CommandName == "View")
        {
            noneditablerows = 0;
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int storereturnid = Convert.ToInt32(gvStoreReturn.DataKeys[rowindex].Value);
            Store = new  EStoreReturn();
            ds = new DataSet();
            Store.StoreReturnId = storereturnid;
            ds = Store.GridForViewEdit();
            if (ds.Tables.Count > 0)
            {

                //ddlOrganisation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationId"]);
                ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreId"]);
                txtReturnDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["ReturnDate"]);
                txtReturnNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ReturnNumber"]);
                //txtPaymentDueDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["PaymentDueDate"]);
                btnSaveReturn.Visible = false;
                //txtHandlingCharges.Text = Convert.ToString(ds.Tables[0].Rows[0]["HandlingCharges"]);
                txtSellingprice.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalSellingPrice"]);
                txtTotalQuantity.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalQty"]);
                txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["Remarks"]);
                ShowTabs(2);
                EnableControls(false);
               // imgClear.Visible = false;
                imgSave.Visible = false;
                if (ds.Tables[1].Rows.Count > 0)
                {
                    noneditablerows = ds.Tables[1].Rows.Count;
                    STRCalculation.Visible = true;
                    gvLineItems.Visible = true;
                    dt = ds.Tables[1];
                    //dt.Columns.Add("GrossProductValue");
                    Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
                    Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
                    Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
                    ds = Store.ddlCategory();
                    l = 0;
                    dtCategory = ds.Tables[0];
                    ds = Store.ddlBrand();
                    dtBrand = ds.Tables[0];
                    //totalgrossvalue = 0;
                   // totalnetbuyingprice = 0;
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
            lnkAdd.Text = "View";
        }
        else if (e.CommandName == "EditRow")
        {
            // Session["save"] = "1";
            //  txtHandlingCharges.ReadOnly = false;
            txtRemarks.ReadOnly = false;
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int StoreReturnId = Convert.ToInt32(gvStoreReturn.DataKeys[rowindex].Value);
            EditFunction(StoreReturnId);
            lnkAdd.Text = "Edit";

            imgSave.Visible = true;
            btncancelgrid.Visible = true;
           // imgSaveDisabled.Visible = false;
            //imgSaveDisabled.Visible = false;
        }
        else if (e.CommandName == "Deleting")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int StoreReturnid = Convert.ToInt32(gvStoreReturn.DataKeys[rowindex].Value);
            Store = new EStoreReturn();
            ds = new DataSet();
            Store.StoreReturnId = StoreReturnid;
            Store.LoginId = Convert.ToInt32(Session["LoginId"]);
            string result = Store.DeleteSOB();
            ShowTabs(1);
            FillGrid();
            dvSuccess.Visible = true;
            lblSuccess.Text = result;
        }
        else if (e.CommandName == "Print")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);
            int StoreReturnId = Convert.ToInt32(gvStoreReturn.DataKeys[rowindex].Value);
            Store = new EStoreReturn();
            ds = new DataSet();
            Store.StoreReturnId = StoreReturnId;
            try
            {
                Store.LoginId = Convert.ToInt32(Session["LOGINID"].ToString());
                Store.StoreReturnId = StoreReturnId;

                if (StoreReturnId != 0)
                {

                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "StoreForPrint(" + StoreReturnId + ");", true);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
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
    protected void gvStoreReturn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvStoreReturn_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvStoreReturn_DataBound(object sender, EventArgs e)
    {

    }
    protected void gvStoreReturn_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void btnSaveDeliveryNote_Click(object sender, EventArgs e)
    {
        try
        {
            lblStatus.Text = string.Empty;
            lblSuccess.Text = string.Empty;
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            ds = new DataSet();
            Store = new EStoreReturn();
            if (txtReturnNo.Text != "")
                Store.ReturnNumber = txtReturnNo.Text;
            else
                Store.ReturnNumber = "";
            if (ddlOrganisation.SelectedValue != "0")
                Store.OrganisationId = Convert.ToInt32(ddlOrganisation.SelectedValue);
            else
                Store.OrganisationId = 0;

            if (txtReturnDate.Text != "")
                Store.ReturnDate = converttodate(txtReturnDate.Text);
            else
                Store.ReturnDate = "";
            Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
            if (ddlStore.SelectedValue != "0")
                Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
            else
                Store.StoreId = 0;
            ds = Store.InsertSOB();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0]["Status"]) == "Recored Inserted successfully.")
                {
                    HDStoreReturnId.Value = Convert.ToString(ds.Tables[0].Rows[0]["ID"]);
                    txtReturnNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ReturnNumber"]);
                    gvLineItems.Visible = true;
                    AddEmptyRows();
                    EnableControls(false);
                    txtTotalQuantity.Text = "";
                    txtSellingprice.Text = "";
                    txtRemarks.Text = "";
                    //Session["save"] = "1";
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
        btnSaveReturn.Visible = status;
        ddlOrganisation.Enabled = status;
        txtReturnNo.Enabled = status;
        txtReturnDate.Enabled = status;

    }
    protected void AddEmptyRows()
    {
        try
        {
            dt = new DataTable();
            if (dt.Rows.Count == 0 || dt.Rows.Count < 5)
            {
                dt = new DataTable();
                dt.Columns.Add("StoreReturnDetailId");
                dt.Columns.Add("CategoryId");
                dt.Columns.Add("BrandId");
                dt.Columns.Add("ProductId");
                dt.Columns.Add("BrandName");
                dt.Columns.Add("ProductName");
                dt.Columns.Add("SellingPrice");
                dt.Columns.Add("BuyingPrice");
                dt.Columns.Add("Quantity");
            }
            DataRow dr;
            for (int i = dt.Rows.Count; i < 5; i++)
            {
                dr = dt.NewRow();
                dr["StoreReturnDetailId"] = "0";
                dr["CategoryId"] = "0";
                dr["BrandId"] = "0";
                dr["ProductId"] = "0";
                dr["BrandName"] = "";
                dr["ProductName"] = "";
                dr["Quantity"] = "0";
                dr["SellingPrice"] = "0.00";              
                dr["BuyingPrice"] = "0.00";              
                dt.Rows.Add(dr);
            }
            STRCalculation.Visible = true;
            imgSave.Visible = true;
            // txttotalgrossvalue.Text = "0.00";
            Store = new EStoreReturn();
            Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
            Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
            Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            ds = Store.ddlCategory();
            dtCategory = ds.Tables[0];
            ds = Store.ddlBrand();
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
    protected void EditFunction(int StoreReturnId)
    {

        Store = new EStoreReturn();
        ds = new DataSet();
        Store.StoreReturnId = StoreReturnId;
        HDStoreReturnId.Value = StoreReturnId.ToString();
        ds = Store.GridForViewEdit();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlOrganisation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationId"]);
                ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreId"]);
                txtReturnDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["ReturnDate"]);
                txtReturnNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ReturnNumber"]);
                btnSaveReturn.Visible = false;

                txtSellingprice.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalSellingPrice"]);
                txtTotalQuantity.Text = Convert.ToString(ds.Tables[0].Rows[0]["TotalQty"]);
                txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["Remarks"]);

                ShowTabs(2);
                //imgClear.Visible = false;
                imgClear.Visible = false;
                imgSave.Visible = true;
                dvisave.Visible = true;
                Divcan1.Visible = true;
                btncancelgrid.Visible = true;
                EnableControls(false);
                if (ds.Tables[1].Rows.Count > 0)
                {
                    STRCalculation.Visible = true;
                    gvLineItems.Visible = true;
                    dt = ds.Tables[1];
                    noneditablerows = dt.Rows.Count;
                    //dt.Columns.Add("GrossProductValue");

                    DataRow dr = dt.NewRow();
                    dr["StoreReturnDetailId"] = "0";
                    dr["CategoryId"] = "0";
                    dr["BrandId"] = "0";
                    dr["ProductId"] = "0";
                    dr["BrandName"] = "";
                    dr["ProductName"] = "";
                    dr["Quantity"] = "0";
                    dr["SellingPrice"] = "0.00";
                    dr["BuyingPrice"] = "0.00";
                    Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
                    Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
                    Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
                    dt.Rows.Add(dr);

                    ds = Store.ddlCategory();
                    l = 0;
                    dtCategory = ds.Tables[0];
                    ds = Store.ddlBrand();
                    dtBrand = ds.Tables[0];
                    //totalgrossvalue = 0;
                    // totalnetbuyingprice = 0;
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
            }
            else {
                FillGrid();
                ShowTabs(1);
            }
            // ControlStatus(false);
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

            Store = new EStoreReturn();
            string GridData = string.Empty;
            int supplierdeliverynotedetailid = 0;
            // ControlStatus(true);
            for (int i = 0; i < gvLineItems.Rows.Count; i++)
            {
                if (((HiddenField)gvLineItems.Rows[i].FindControl("StoreReturnDetailId")).Value != "")
                {
                    supplierdeliverynotedetailid = Convert.ToInt32(((HiddenField)gvLineItems.Rows[i].FindControl("StoreReturnDetailId")).Value);
                    if (supplierdeliverynotedetailid == 0)
                    {
                        if (((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" && ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value != "0"
                            && ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value != "0" && ((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text != "0")
                        {
                            if (((TextBox)gvLineItems.Rows[i].FindControl("txtProductValue")).Text == "")
                                ((TextBox)gvLineItems.Rows[i].FindControl("txtProductValue")).Text = "0";
                            GridData += ((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue + "~"
                                 + ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value + "~"
                                + ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value + "~"
                                + ((TextBox)gvLineItems.Rows[i].FindControl("txtProductValue")).Text + "~"
                                + ((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text + "~"
                                + ((HiddenField)gvLineItems.Rows[i].FindControl("HDBuyingPrice")).Value + "$";
                            //+ ((TextBox)gvLineItems.Rows[i].FindControl("txtNetBuyingPrice")).Text + "$";
                        }
                    }
                }
            }
            if (GridData != "")
            {
                GridData = GridData.Substring(0, GridData.Length - 1);
                Store.GridData = GridData;
                if (txtTotalQuantity.Text != "")
                    Store.TotalQuantity = Convert.ToDecimal(txtTotalQuantity.Text);
                else
                    Store.TotalQuantity = 0;

                if (txtSellingprice.Text != "")
                {
                    Store.TotalSellingPrice = Convert.ToDecimal(txtSellingprice.Text);
                }
                else
                    Store.TotalSellingPrice = 0;
                Store.Remarks = txtRemarks.Text;
                Store.StoreReturnId = Convert.ToInt32(HDStoreReturnId.Value);
                Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
                Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
                Store.OrganisationId = Convert.ToInt32(ddlOrganisation.SelectedValue);
                string result =Store.InsertSOBD();
                if (result == "Record inserted successfully.")
                {
                    ShowTabs(1);
                    FillGrid();
                    dvSuccess.Visible = true;
                    lblSuccess.Text = result;
                    //lblStatus.Text = result;
                    int StoreReturnId = Convert.ToInt32(HDStoreReturnId.Value);
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
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
       // ddlOraganisationSearch.SelectedIndex = 0;
        ddlStoreSearch.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtToDate.Text = "";

        FillGrid();
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

                int StoreReturnDetailId = Convert.ToInt32(gvLineItems.DataKeys[rowindex].Value);
                if (StoreReturnDetailId != 0)
                {
                    Store = new EStoreReturn();
                    Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
                    Store.StoreReturnId = Convert.ToInt32(HDStoreReturnId.Value);
                    Store.StoreReturnDetailId = StoreReturnDetailId;
                    Store.LoginId = Convert.ToInt32(Session["LoginId"]);
                    result = Store.DeleteSOBD();
                    int StoreReturnId = Convert.ToInt32(HDStoreReturnId.Value);
                    EditFunction(StoreReturnId);
                    lblStatus.Text = result;
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add("StoreReturnDetailId");
                    dt.Columns.Add("CategoryId");
                    dt.Columns.Add("BrandId");
                    dt.Columns.Add("ProductId");
                    dt.Columns.Add("BrandName");
                    dt.Columns.Add("ProductName");
                    dt.Columns.Add("SellingPrice");
                    dt.Columns.Add("Quantity");                 
                    dt.Columns.Add("BuyingPrice");
                  
                    DataRow dr;
                    for (int i = 0; i <= gvLineItems.Rows.Count - 2; i++)
                    {
                        if (i != rowindex)
                        {
                            dr = dt.NewRow();
                            dr["StoreReturnDetailId"] = ((HiddenField)gvLineItems.Rows[i].FindControl("StoreReturnDetailId")).Value;
                            dr["CategoryId"] = ((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue;
                            //if (((DropDownList)gvLineItems.Rows[i].FindControl("ddlProduct")).SelectedValue == "")
                            //    dr["ProductId"] = "0";
                            //else
                            dr["ProductId"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value;
                            dr["BrandId"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value;
                            dr["BrandName"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtBrand")).Text;
                            dr["ProductName"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtProduct")).Text;
                            dr["SellingPrice"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtProductValue")).Text;
                            dr["Quantity"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text;
                            dr["BuyingPrice"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDBuyingPrice")).Value;
                            dt.Rows.Add(dr);
                        }
                    }
                    dr = dt.NewRow();
                    dr["StoreReturnDetailId"] = "0";
                    dr["CategoryId"] = "0";
                    dr["BrandId"] = "0";
                    dr["ProductId"] = "0";
                    dr["BrandName"] = "";
                    dr["ProductName"] = "";
                    dr["Quantity"] = "0";
                    dr["SellingPrice"] = "0.00";
                    dr["BuyingPrice"] = "0.00";
                    dt.Rows.Add(dr);
                    Store = new EStoreReturn();
                    ds = Store.ddlCategory();
                    dtCategory = ds.Tables[0];
                    ds = Store.ddlBrand();
                    dtBrand = ds.Tables[0];
                    l = 0;
                    gvLineItems.DataSource = dt;
                    gvLineItems.DataBind();
                }
            }
            else
            {
                int StoreReturnDetailId = Convert.ToInt32(((HiddenField)gvLineItems.Rows[rowindex].FindControl("StoreReturnDetailId")).Value);
                Store = new  EStoreReturn();
                Store.StoreReturnDetailId = StoreReturnDetailId;
                if (StoreReturnDetailId != 0)
                    result = Store.DeleteSOBD();
                AddEmptyRows();
            }


            lblStatus.Text = result;
            //ShowTabs(1);               
        }
    }   
    protected void gvLineItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

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
                ddlcard.DataValueField = "CategoryId";
                ddlcard.DataTextField = "CategoryName";
                ddlcard.DataBind();
                ddlcard.Items.Insert(0, new ListItem("--Any--", "0"));

                ddlbrand = (DropDownList)e.Row.FindControl("ddlBrand");
                ddlbrand.DataSource = dtBrand;
                ddlbrand.DataValueField = "BrandId";
                ddlbrand.DataTextField = "BrandName";
                ddlbrand.DataBind();
                ddlbrand.Items.Insert(0, new ListItem("--Any--", "0"));

                hd = (HiddenField)e.Row.FindControl("StoreReturnDetailId");
                hd.Value = Convert.ToString(dt.Rows[l]["StoreReturnDetailId"]);
                if (Convert.ToInt32(dt.Rows[l]["ProductId"]) != 0)
                {

                    ddlcard.SelectedValue = Convert.ToString(dt.Rows[l]["CategoryId"]);
                    int categoryid = Convert.ToInt32(ddlcard.SelectedValue);

                    hd = (HiddenField)e.Row.FindControl("HDBrandID");
                    hd.Value = Convert.ToString(dt.Rows[l]["BrandID"]);
                    hd1 = (HiddenField)e.Row.FindControl("HDProductID");
                    hd1.Value = Convert.ToString(dt.Rows[l]["ProductID"]);
                    txtprod = (TextBox)e.Row.FindControl("txtProduct");
                    txtprod.Text = Convert.ToString(dt.Rows[l]["ProductName"]);
                    txtbrand = (TextBox)e.Row.FindControl("txtBrand");
                    txtbrand.Text = Convert.ToString(dt.Rows[l]["BrandName"]);

                    ddlbrand.SelectedValue = Convert.ToString(dt.Rows[l]["BrandId"]);
                    int brandid = Convert.ToInt32(ddlbrand.SelectedValue);

                    fillproduct(brandid, categoryid, ddlproducts);
                    ddlproducts.SelectedValue = Convert.ToString(dt.Rows[l]["ProductId"]);
                    productvalue = Convert.ToSingle(dt.Rows[l]["SellingPrice"]);

                    if (Convert.ToString(dt.Rows[l]["Quantity"]) != "")
                        qunatity = Convert.ToInt32(dt.Rows[l]["Quantity"]);
                    else
                        qunatity = 0;                   

                    if (Convert.ToInt32(dt.Rows[l]["StoreReturnDetailId"]) != 0)
                    {
                        ddlproducts.Enabled = false;
                        ddlbrand.Enabled = false;
                        ddlcard.Enabled = false;
                        txtprod.Enabled = false;
                        txtbrand.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtProductValue");
                        tt.Enabled = false;
                        tt = (TextBox)e.Row.FindControl("txtQuantity");
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
    protected void fillproduct(int brandid, int categoryid, DropDownList ddlprod)
    {
        try
        {
            ds = new DataSet();
            Store = new EStoreReturn();
            Store.BrandId = brandid;
            Store.CategoryId = categoryid;
            Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
            Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
            ds = Store.ddlProducts();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlprod.DataSource = ds.Tables[0];
                ddlprod.DataValueField = "ProductId";
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
    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        DropDownList ddlcategory = (DropDownList)grow.FindControl("ddlCategory");
        DropDownList ddlbrand = (DropDownList)grow.FindControl("ddlBrand");
        DropDownList ddlProd = (DropDownList)grow.FindControl("ddlProduct");
        ddlProd.Items.Clear();
        int categoryid = Convert.ToInt32(ddlcategory.SelectedValue);
        int brandid = Convert.ToInt32(ddlbrand.SelectedValue);
        if (categoryid != 0)
        {
            Store = new EStoreReturn();
            dsforSDN = new DataSet();
            Store.CategoryId = categoryid;
            Store.BrandId = brandid;
            Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
            Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
            dsforSDN = Store.ddlProducts();
            if (dsforSDN.Tables[0].Rows.Count > 0)
            {
                ddlProd.DataSource = dsforSDN.Tables[0];
                ddlProd.DataTextField = "Productname";
                ddlProd.DataValueField = "ProductId";
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
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddlprod = (DropDownList)gr.FindControl("ddlProduct");
            HiddenField hdbp = (HiddenField)gr.FindControl("HDBuyingPrice");
            int productid = Convert.ToInt32(ddlprod.SelectedValue);
            if (productid != 0)
            {
                Store = new EStoreReturn();
                dsforSDN = new DataSet();
                Store.ProductId = productid;
                Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
                Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
                Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
                dsforSDN = Store.ProductValues();
                if (dsforSDN.Tables[0].Rows.Count > 0)
                {
                    int availqty = 0;                   
                    TextBox tt = (TextBox)gr.FindControl("txtProductValue");
                    tt.Text = Convert.ToString(dsforSDN.Tables[0].Rows[0]["ProductValue"]);
                    hdbp.Value = Convert.ToString(dsforSDN.Tables[0].Rows[0]["BuyingPrice"]);
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
    protected void txtBrand_ontextchanged(object sender, EventArgs e)
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
            TextBox txt = (TextBox)gr.FindControl("txtQuantity");
            TextBox prodvalue = (TextBox)gr.FindControl("txtProductValue");
            HiddenField hdprodid = (HiddenField)gr.FindControl("HDProductID");
            HiddenField hdbrandid = (HiddenField)gr.FindControl("HDBrandID");
            HiddenField txtbp = (HiddenField)gr.FindControl("HDBuyingPrice");
            EStoreReturn SDN = new EStoreReturn();
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
                SDN.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
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
                        txtbp.Value = Convert.ToString(ds.Tables[0].Rows[0]["BuyingPrice"]);
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
                        ddl.SelectedValue = "0";
                        txtbrand.Text = "";
                        prodvalue.Text = "0.00";
                        txtproduct.Text = "";
                        txtproduct.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('No Stock Avaiable for this Product.');", true);
                        return;
                    }
                }
                else    if (categoryid == 0 || brandid == 0)
                {
                    ds = SDN.GetProductCategorybrandID();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CategoryID"]);
                        hdbrandid.Value = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                        hdprodid.Value = Convert.ToString(ds.Tables[0].Rows[0]["ProductID"]);
                        prodvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                        txtbrand.Text = Convert.ToString(ds.Tables[0].Rows[0]["BrandName"]);
                        txtbp.Value = Convert.ToString(ds.Tables[0].Rows[0]["BuyingPrice"]);
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
                        ddl.SelectedValue = "0";
                        txtbrand.Text = "";
                        prodvalue.Text = "0.00";
                        txtproduct.Text = "";
                        txtproduct.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "alert('No Stock Avaiable for this Product.');", true);
                        return;
                    }
                }
               
                //
                //txtbp = (TextBox)gr.FindControl("txtBuyingPrice");
                //txtbp.Text = "0.00";
                //TextBox txnbp = (TextBox)gr.FindControl("txtSellingprice");
                //txnbp.Text = "0";
                //if (txt.Text != "0")
                //    qty = Convert.ToInt32(((TextBox)gr.FindControl("txtQuantity")).Text);
                //if (qty != 0)
                //{
                //    ((TextBox)gr.FindControl("txttotalsellingprice")).Text = Convert.ToString(qty * Convert.ToSingle(prodvalue.Text));
                //}
                float sp = 0, tqty = 0,tsp=0,qty=0;
                for (int i = 0; i < gvLineItems.Rows.Count; i++)
                {
                    if (Convert.ToSingle(((TextBox)gvLineItems.Rows[i].FindControl("txtProductValue")).Text) != 0 &&
                        Convert.ToInt32(((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text) != 0)
                    {
                        sp = Convert.ToSingle(((TextBox)gvLineItems.Rows[i].FindControl("txtProductValue")).Text);
                        qty = Convert.ToInt32(((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text);

                    }
                    else if (Convert.ToInt32(((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text) != 0)
                    {
                        qty = Convert.ToInt32(((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text);
                    }
                    else
                    {
                        sp = 0;
                        qty = 0;
                    }
                    tsp += sp;
                    tqty += qty;
                }
                txtTotalQuantity.Text = Convert.ToString(tqty);
                txtSellingprice.Text = Convert.ToString(tsp);
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
                AddNewRow();
                
                
            }
            else
            {
                txt.Focus();
            }

            //imgSave.Visible = true;
            //imgSaveDisabled.Visible = false;
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
            dt.Columns.Add("StoreReturnDetailId");
            dt.Columns.Add("CategoryId");
            dt.Columns.Add("BrandId");
            dt.Columns.Add("ProductId");
            dt.Columns.Add("BrandName");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("SellingPrice");
            dt.Columns.Add("Quantity");           
            dt.Columns.Add("BuyingPrice");          

            DataRow dr;
            for (int i = 0; i <= gvLineItems.Rows.Count - 1; i++)
            {

                dr = dt.NewRow();
                dr["StoreReturnDetailId"] = ((HiddenField)gvLineItems.Rows[i].FindControl("StoreReturnDetailId")).Value;
                dr["CategoryId"] = ((DropDownList)gvLineItems.Rows[i].FindControl("ddlCategory")).SelectedValue;
                dr["ProductId"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDProductID")).Value;
                dr["BrandId"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDBrandID")).Value;
                dr["ProductName"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtProduct")).Text;
                dr["BrandName"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtBrand")).Text;
                dr["SellingPrice"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtProductValue")).Text;
                dr["Quantity"] = ((TextBox)gvLineItems.Rows[i].FindControl("txtQuantity")).Text;
                dr["BuyingPrice"] = ((HiddenField)gvLineItems.Rows[i].FindControl("HDBuyingPrice")).Value;              
                dt.Rows.Add(dr);
            }
            dr = dt.NewRow();

            dr["StoreReturnDetailId"] = "0";
            dr["CategoryId"] = "0";
            dr["BrandId"] = "0";
            dr["ProductId"] = "0";
            dr["BrandName"] = "";
            dr["ProductName"] = "";
            dr["Quantity"] = "0";
            dr["SellingPrice"] = "0.00";         
            dr["BuyingPrice"] = "0.00";
          

            dt.Rows.Add(dr);
            Store = new EStoreReturn();
            Store.StoreId = Convert.ToInt32(ddlStore.SelectedValue);
            Store.LoginId = Convert.ToInt32(Session["LOGINID"]);
            Store.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            ds = Store.ddlCategory();
            dtCategory = ds.Tables[0];
            ds = Store.ddlBrand();
            dtBrand = ds.Tables[0];
            l = 0;
            gvLineItems.DataSource = dt;
            gvLineItems.DataBind();
            //txtbp.Focus();
            //for (int i = 0; i < noneditablerows; i++)
            //    gvSDND.Rows[i].Enabled = false;
            ((TextBox)(gvLineItems.Rows[gvLineItems.Rows.Count - 2].FindControl("txtQuantity"))).Focus();
        }
        catch (Exception)
        {

            throw;
        }
    }
}