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
public partial class Screens_Supplier : System.Web.UI.Page
{
    int l = 0;
    ESupplier Supplier;
    DataSet ds;
    string loginid = "";
    DataTable dtCategory;
    DataTable dt;
    ECheckPermission ECPobj;
    static bool addPermission = false;
    static bool viewPermission = true;
    static bool EditPermission = true;
    static bool deletepermission = true;
    string ScreenUrl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScreenUrl = Request.FilePath;
        ScreenUrl = ScreenUrl.Substring(ScreenUrl.LastIndexOf('/') + 1);
        gvSupplierProduct.Visible = false;
       // ddlCategorySearch.Visible = false;
        //ddlProductSearch.Visible = false;
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
                FillSuppliers();
                ShowTabs(1);
                lblStatus.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }
    }
    protected void FillSuppliers()
    {
        try
        {
            Supplier = new ESupplier();
            ds = new DataSet();
            ds = Supplier.GetSupplierDDL();
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
                imgupdate.Visible = EditPermission;
                btncan.Visible = EditPermission;
                imgClear.Visible = EditPermission;
            }
            else
            {
                imgupdate.Visible = EditPermission;

            }
            if (addPermission == true)
            {
                imgSave.Visible = addPermission;
                btncan.Visible = addPermission;
                imgClear.Visible = addPermission;
                lirole.Visible = addPermission;
                lnkAdd.Visible = addPermission;
            }
            else
            {
                imgSave.Visible = addPermission;
                lirole.Visible = addPermission;
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
    void ShowTabs(int TabNum)
    {
        if (TabNum == 1)
        {
            pnlAdd.Visible = false;
            pnlSearch.Visible = true;
            pnlSearchGrid.Visible = false;
            ddlSupplier.SelectedIndex = 0;
            lnkAdd.Text = "Add";
            lnkAdd.Visible = true;
            FillGrid();
            dvFooter.Visible = false;
        }
        else if (TabNum == 2)
        {
            pnlAdd.Visible = true;
            pnlSearch.Visible = false;
            pnlSearchGrid.Visible = false;
            lnkAdd.Text = "Add"; 
            lnkAdd.Visible = false;
            dvFooter.Visible = true;
        }
    }
    void ClearControls()
    {
        txtSupplierName.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtContactNo.Text = string.Empty;
        txtContactPerson.Text= string.Empty;
        txtVatID.Text = string.Empty;
        txtZipCode.Text = string.Empty;
        txtMobileNo.Text = "";
        txtEmailID.Text = string.Empty;

    }
    void EnableControls(bool status)
    {
        txtSupplierName.Enabled = status;
        txtCity.Enabled = status;
        txtAddress.Enabled = status;
        txtContactNo.Enabled = status;
        txtContactPerson.Enabled = status;
        txtMobileNo.Enabled = status;
        txtVatID.Enabled = status;
        txtZipCode.Enabled = status;
        gvSupplierProduct.Enabled = status;
        txtEmailID.Enabled = status;
    }
    void showButtons(string Mode)
    {
        if (string.Compare(Mode, "Search", true) == 0)
        {
            imgClear.Visible = true;
            dvclear.Visible = true;
            imgSave.Visible = false;
            dvisave.Visible = false;
            imgupdate.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = false;
            btncan.Visible = false;
        }
        else if (string.Compare(Mode, "View", true) == 0)
        {
            imgClear.Visible = false;
            dvclear.Visible = false;
            imgSave.Visible = false;
            dvisave.Visible = false;
            imgupdate.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = true;
            btncan.Visible = true;
        }
        else if (string.Compare(Mode, "Edit", true) == 0)
        {
            imgClear.Visible = true;
            dvclear.Visible = true;
            imgSave.Visible = false;
            dvisave.Visible = false;
            imgupdate.Visible = true;
            dvupdate.Visible = true;
            dvcancel.Visible = true;
            btncan.Visible = true;
        }
        else if (string.Compare(Mode, "Add", true) == 0)
        {
            dvcancel.Visible = true;
            dvclear.Visible = true;
            btncan.Visible = true;
            imgClear.Visible = true;
            imgSave.Visible = true;
            dvisave.Visible = true;
            imgupdate.Visible = false;
            dvupdate.Visible = false;
        }

    }
    
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ShowTabs(2);
        ClearControls();
        imgSave.Enabled = true;
        EnableControls(true);
        showButtons("Add");
        lblStatus.Text = "";
        FillEmptyGrid();        
    }

    private void FillEmptyGrid()
    {
        dt = new DataTable();
        if (dt.Rows.Count == 0 || dt.Rows.Count < 5)
        {
            dt.Columns.Add("ProductID");
            dt.Columns.Add("ProductValue");
            dt.Columns.Add("SupplierProductID");
            dt.Columns.Add("BuyingDiscount");
            dt.Columns.Add("NetBuyingPrice");
            dt.Columns.Add("SellingDefaultDiscount");
            dt.Columns.Add("CategoryID");
        }
        DataRow dr;
        for (int i = dt.Rows.Count; i < 5; i++)
        {
            dr = dt.NewRow();
            dr["ProductID"] = "0";
            dr["ProductValue"] = "0.00";
            dr["SupplierProductID"] = "0";
            dr["BuyingDiscount"] = "0.00";
            dr["NetBuyingPrice"] = "0.00";
            dr["SellingDefaultDiscount"] = "0.00";
            dr["CategoryID"] = "0";
            dt.Rows.Add(dr);
        }
        Supplier=new ESupplier();
        ds = Supplier.GetCategoryDDL();
        dtCategory = ds.Tables[0];
        l = 0;
        gvSupplierProduct.DataSource = dt;
        gvSupplierProduct.DataBind();
    }

    protected void imgSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblStatus.Text = string.Empty;
            lblSuccess.Text = string.Empty;
            dvSuccess.Visible = false;
            dvFailure.Visible = false;
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
            Supplier = new ESupplier();
            ds = new DataSet();
            if (ddlSupplier.SelectedValue != "0")
                Supplier.SupplierID = Convert.ToInt32(ddlSupplier.SelectedValue);
            else
                Supplier.SupplierID = 0;
            if (txtContactNoSearch.Text != "")
                Supplier.ContactNo = txtContactNoSearch.Text;
            else
                Supplier.ContactNo = string.Empty;
            if (txtMobileNoSearch.Text != "")
                Supplier.MobileNo = txtMobileNoSearch.Text;
            else
                Supplier.MobileNo = string.Empty;
            if (txtEmailIDSearch.Text != "")
                Supplier.EmailID = txtEmailIDSearch.Text;
            else
                Supplier.EmailID = string.Empty;
            //if (ddlCategorySearch.SelectedValue != "0" && ddlCategorySearch.SelectedValue != "")
            //    Supplier.CategoryID = Convert.ToInt32(ddlCategorySearch.SelectedValue);
            //else
            //    Supplier.CategoryID = 0;

            //if (ddlProductSearch.SelectedValue != "0" && ddlProductSearch.SelectedValue != "")
            //    Supplier.ProductID = Convert.ToInt32(ddlProductSearch.SelectedValue);
            //else
            //    Supplier.ProductID = 0;
            ds = Supplier.GetSupplierGrid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSupplier.DataSource = ds.Tables[0];
                gvSupplier.DataBind();
               // lblStatus.Text = "";
                //pnlAdd.Visible = false;
                //pnlSearch.Visible = true;
                pnlSearchGrid.Visible = true;
                //ddlSupplier.SelectedValue = "0";
                //txtContactNoSearch.Text = "";
                //txtMobileNoSearch.Text = "";
                //txtEmailIDSearch.Text = "";
                gvSupplier.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                lblStatus.Text = "No Record Found.";
                dvFailure.Visible = true;
                gvSupplier.DataSource = null;
                gvSupplier.DataBind();               
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void imgSave_Click(object sender, EventArgs e)
    {
        imgSave.Enabled = false;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        try
        {
            Supplier = new ESupplier();
            if (txtSupplierName.Text != "")
                Supplier.SupplierName = txtSupplierName.Text;
            else
                Supplier.SupplierName = string.Empty;
            if (txtAddress.Text != "")
                Supplier.Address = txtAddress.Text;
            else
                Supplier.Address = string.Empty;
            if (txtCity.Text != "")
                Supplier.City = txtCity.Text;
            else
                Supplier.City = string.Empty;
            if (txtContactNo.Text != "")
                Supplier.ContactNo = txtContactNo.Text;
            else
                Supplier.ContactNo = string.Empty;
            if (txtContactPerson.Text != "")
                Supplier.ContactPerson = txtContactPerson.Text;
            else
                Supplier.ContactPerson = string.Empty;
            if (txtMobileNo.Text != "")
                Supplier.MobileNo = txtMobileNo.Text;
            else
                Supplier.MobileNo = string.Empty;
            if (txtZipCode.Text != "")
                Supplier.ZipCode = txtZipCode.Text;
            else
                Supplier.ZipCode = string.Empty;
            if (txtVatID.Text != "")
                Supplier.VatID = txtVatID.Text;
            else
                Supplier.VatID = string.Empty;
            if (txtEmailID.Text != "")
                Supplier.EmailID = txtEmailID.Text;
            else
                Supplier.EmailID = string.Empty;
            string GridData = string.Empty;
            int rowcount = gvSupplierProduct.Rows.Count;
            int supplierproductid;
            for (int i = 0; i < rowcount; i++)
            {
                supplierproductid = Convert.ToInt32(((HiddenField)gvSupplierProduct.Rows[i].FindControl("HDSupplierProductID")).Value);
                if (supplierproductid == 0)
                {
                    if (((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" && ((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlProduct")).SelectedValue!="0")
                    GridData += ((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlProduct")).SelectedValue + "~"
                              + ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtProductValue")).Text + "~"
                              + ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtBuyingDiscount")).Text + "~"
                              + ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtNetBuyingPrice")).Text + "$";
                }
            }
            if(GridData!="")
                GridData = GridData.Substring(0, GridData.Length - 1);
            if (GridData != "")
                Supplier.GridData = GridData;
            else
                Supplier.GridData = "";
            string result = Supplier.Insert();
            if (result == "Record inserted Successfully.")
            {
                
                ShowTabs(1);
                FillGrid();
                dvSuccess.Visible = true;
                lblSuccess.Text = result;
               
            }
            else
            {
                ShowTabs(2);
                dvFailure.Visible = true;
                lblStatus.Text = result;
            }
                
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void imgupdate_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        try
        {
            Supplier = new ESupplier();
            Supplier.SupplierID = Convert.ToInt32(HDSupplierID.Value);
            if (txtSupplierName.Text != "")
                Supplier.SupplierName = txtSupplierName.Text;
            else
                Supplier.SupplierName = string.Empty;
            if (txtAddress.Text != "")
                Supplier.Address = txtAddress.Text;
            else
                Supplier.Address = string.Empty;
            if (txtCity.Text != "")
                Supplier.City = txtCity.Text;
            else
                Supplier.City = string.Empty;
            if (txtContactNo.Text != "")
                Supplier.ContactNo = txtContactNo.Text;
            else
                Supplier.ContactNo = string.Empty;
            if (txtContactPerson.Text != "")
                Supplier.ContactPerson = txtContactPerson.Text;
            else
                Supplier.ContactPerson = string.Empty;
            if (txtMobileNo.Text != "")
                Supplier.MobileNo = txtMobileNo.Text;
            else
                Supplier.MobileNo = string.Empty;
            if (txtZipCode.Text != "")
                Supplier.ZipCode = txtZipCode.Text;
            else
                Supplier.ZipCode = string.Empty;
            if (txtVatID.Text != "")
                Supplier.VatID = txtVatID.Text;
            else
                Supplier.VatID = string.Empty;
            if (txtEmailID.Text != "")
                Supplier.EmailID = txtEmailID.Text;
            else
                Supplier.EmailID = string.Empty;
            string GridData = string.Empty;
            int rowcount = gvSupplierProduct.Rows.Count;
            int supplierproductid;
            for (int i = 0; i < rowcount; i++)
            {
                supplierproductid = Convert.ToInt32(((HiddenField)gvSupplierProduct.Rows[i].FindControl("HDSupplierProductID")).Value);
                if (supplierproductid == 0)
                {
                    if (((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlCategory")).SelectedValue != "0" && ((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlProduct")).SelectedValue!="0")
                    GridData += ((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlProduct")).SelectedValue + "~"
                              + ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtProductValue")).Text + "~"
                              + ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtBuyingDiscount")).Text + "~"
                              + ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtNetBuyingPrice")).Text + "$";
                }
            }
            if (GridData != "")
                GridData = GridData.Substring(0, GridData.Length - 1);
            else
                GridData = "";
            Supplier.GridData = GridData;
            string result = Supplier.Update();
            if (result == "Record Updated Successfully.")
            {               
                
                ShowTabs(1);
                FillGrid();
                dvSuccess.Visible = true;
                lblSuccess.Text = result;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void imgClear_Click(object sender, EventArgs e)
    {
        ClearControls();
    }
    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            lblSuccess.Text = "";
            lblStatus.Text = "";
            ds = new DataSet();
            Supplier = new ESupplier();
            if (e.CommandName == "View")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int supplierid = Convert.ToInt32(gvSupplier.DataKeys[rowindex].Value);
                Supplier.SupplierID = supplierid;
                ds = Supplier.GetSupplierForViewEdit();
                if (ds.Tables.Count > 0)
                {
                    txtSupplierName.Text = Convert.ToString(ds.Tables[0].Rows[0]["SupplierName"]);
                    txtAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                    txtCity.Text = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
                    txtContactNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactNo"]);
                    txtContactPerson.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactPerson"]);
                    txtEmailID.Text = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                    txtMobileNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["MobileNo"]);
                    txtVatID.Text = Convert.ToString(ds.Tables[0].Rows[0]["VATID"]);
                    txtZipCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["ZIPCode"]);

                    dt = ds.Tables[1];
                    ds = new DataSet();
                    ds = Supplier.GetCategoryDDL();
                    l = 0;
                    dtCategory = ds.Tables[0];
                    gvSupplierProduct.Columns[6].Visible = false;
                    gvSupplierProduct.DataSource = dt;
                    gvSupplierProduct.DataBind();
                    ShowTabs(2);
                    EnableControls(false);
                    //imgClear.Visible = false;
                    //imgSave.Visible = false;
                    //imgupdate.Visible = false;
                    //lnkAdd.Text = "View";
                   // lblStatus.Text = "";
                    showButtons("View");
                   
                }
            }
            else if (e.CommandName == "EditRow")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int supplierid = Convert.ToInt32(gvSupplier.DataKeys[rowindex].Value);
                Supplier.SupplierID = supplierid;
                HDSupplierID.Value = supplierid.ToString();
                ds = Supplier.GetSupplierForViewEdit();
                if (ds.Tables.Count > 0)
                {
                    txtSupplierName.Text = Convert.ToString(ds.Tables[0].Rows[0]["SupplierName"]);
                    txtAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                    txtCity.Text = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
                    txtContactNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactNo"]);
                    txtContactPerson.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactPerson"]);
                    txtEmailID.Text = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                    txtMobileNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["MobileNo"]);
                    txtVatID.Text = Convert.ToString(ds.Tables[0].Rows[0]["VATID"]);
                    txtZipCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["ZIPCode"]);

                    
                    dt = ds.Tables[1];
                    DataRow  dr = dt.NewRow();
                    dr["ProductID"] = "0";
                    dr["ProductValue"] = "0.00";
                    dr["SupplierProductID"] = "0";
                    dr["BuyingDiscount"] = "0.00";
                    dr["NetBuyingPrice"] = "0.00";
                    dr["SellingDefaultDiscount"] = "0.00";
                    dr["CategoryID"] = "0";
                    dt.Rows.Add(dr);
                    ds = new DataSet();
                    ds = Supplier.GetCategoryDDL();
                    l = 0;
                    dtCategory = ds.Tables[0];
                    gvSupplierProduct.Columns[6].Visible = true;
                    gvSupplierProduct.DataSource = dt;
                    gvSupplierProduct.DataBind();
                    ShowTabs(2);
                    EnableControls(true);
                    //imgupdate.Visible = true;
                    //imgSave.Visible = false;
                    //imgClear.Visible = true;
                    //lnkAdd.Text = "Edit";
                    //lblStatus.Text = "";
                    showButtons("Edit");
                }
            }
            else if (e.CommandName == "Deleting")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int supplierid = Convert.ToInt32(gvSupplier.DataKeys[rowindex].Value);
                Supplier.SupplierID = supplierid;
                string result = Supplier.Delete();
                if (result == "Record Deleted Successfully.")
                {                   
                    ShowTabs(1);
                    FillGrid();
                    dvSuccess.Visible = true;
                    lblSuccess.Text = result;
                }
                else
                {
                    dvFailure.Visible = true;
                    lblStatus.Visible = true;
                }
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void gvSupplier_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvSupplier_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvSupplier_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        DropDownList ddlcategory = (DropDownList)grow.FindControl("ddlCategory");
        DropDownList ddlproduct = (DropDownList)grow.FindControl("ddlProduct");
        int categoryid = Convert.ToInt32(ddlcategory.SelectedValue);
        if (categoryid != 0)
        {
            ds = new DataSet();
            Supplier = new ESupplier();
            Supplier.CategoryID = categoryid;
            ds = Supplier.GetProductDDL();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlproduct.DataSource = ds.Tables[0];
                ddlproduct.DataValueField = "ProductID";
                ddlproduct.DataTextField = "ProductName";
                ddlproduct.DataBind();
                ddlproduct.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            
        }
        
    }  
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        try
        {
            GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddlcategory = (DropDownList)grow.FindControl("ddlCategory");
            DropDownList ddlproduct = (DropDownList)grow.FindControl("ddlProduct");
            TextBox txtproductvalue = (TextBox)grow.FindControl("txtProductValue");
            TextBox txtsellingdiscount = (TextBox)grow.FindControl("txtSellingDiscount");
            int productid = Convert.ToInt32(ddlproduct.SelectedValue);
            int categoryid = Convert.ToInt32(ddlcategory.SelectedValue);
            int sum=0;

            int rowcount = gvSupplierProduct.Rows.Count;
            int cat, prod=0;
            for (int i = 0; i < rowcount; i++)
            {
                cat = Convert.ToInt32(((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlCategory")).SelectedValue);
                if(((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlProduct")).SelectedValue!="")
                    prod = Convert.ToInt32(((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlProduct")).SelectedValue);
                if (cat == categoryid && prod == productid)
                    sum++;
                if (sum > 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('This Product for this category already selected. Please Select Different Product.');", true);
                    ((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlCategory")).SelectedValue = "0";
                    ((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlProduct")).SelectedValue = "0";
                    return;
                }
            }
            ds = new DataSet();
            Supplier = new ESupplier();
            Supplier.ProductID = productid;
            ds = Supplier.GetProductValue();
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtproductvalue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                txtsellingdiscount.Text = Convert.ToString(ds.Tables[0].Rows[0]["SellingDefaultDiscount"]);

            }
             rowcount = gvSupplierProduct.Rows.Count;
            DropDownList ddlproducts = sender as DropDownList;
            string ID = ddlproducts.ClientID;
            ID = ID.Replace("ctl00_ContentPlaceHolder1_gvSupplierProduct_ctl", "");
            ID = ID.Replace("_ddlProduct", "");
            int maxrow = Convert.ToInt32(ID) - 1;
            if (maxrow == rowcount)
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
        try
        {
            dt = new DataTable();
            dt.Columns.Add("ProductID");
            dt.Columns.Add("ProductValue");
            dt.Columns.Add("SupplierProductID");
            dt.Columns.Add("BuyingDiscount");
            dt.Columns.Add("NetBuyingPrice");
            dt.Columns.Add("SellingDefaultDiscount");
            dt.Columns.Add("CategoryID");
            DataRow dr;
            for (int i = 0; i <= gvSupplierProduct.Rows.Count - 1; i++)
            {
                
                dr = dt.NewRow();
                dr["ProductID"] = ((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlProduct")).SelectedValue;
                dr["ProductValue"] = ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtProductValue")).Text;
                dr["SupplierProductID"] = ((HiddenField)gvSupplierProduct.Rows[i].FindControl("HDSupplierProductID")).Value;
                dr["BuyingDiscount"] = ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtBuyingDiscount")).Text;
                dr["NetBuyingPrice"] = ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtNetBuyingPrice")).Text;
                dr["SellingDefaultDiscount"] = ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtSellingDiscount")).Text;
                dr["CategoryID"] = ((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlCategory")).SelectedValue;
                dt.Rows.Add(dr);
            }
            dr = dt.NewRow();
           
            dr["ProductID"] = "0";
            dr["ProductValue"] = "0.00";
            dr["SupplierProductID"] = "0";
            dr["BuyingDiscount"] = "0.00";
            dr["NetBuyingPrice"] = "0.00";
            dr["SellingDefaultDiscount"] = "0.00";
            dr["CategoryID"] = "0";
            dt.Rows.Add(dr);
            Supplier = new ESupplier();
            ds = Supplier.GetCategoryDDL();
            dtCategory = ds.Tables[0];
            l = 0;
            gvSupplierProduct.DataSource = dt;
            gvSupplierProduct.DataBind();
        }
        catch (Exception)
        {
            
            throw;
        }
    }
   
    protected void gvSupplierProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            lblSuccess.Text = "";
            lblStatus.Text = "";
            TextBox tt;
            HiddenField hd;
            DropDownList ddlproducts;
            TextBox txtprodvalue, txtselldiscount, txtbuyingdisocunt, txtnetprice;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlcard = (DropDownList)e.Row.FindControl("ddlCategory");
                ddlcard.DataSource = dtCategory;
                ddlcard.DataValueField = "CategoryID";
                ddlcard.DataTextField = "CategoryName";
                ddlcard.DataBind();
                ddlcard.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlproducts = (DropDownList)e.Row.FindControl("ddlProduct");
                txtprodvalue = (TextBox)e.Row.FindControl("txtProductValue");
                txtselldiscount = (TextBox)e.Row.FindControl("txtSellingDiscount");
                txtbuyingdisocunt = (TextBox)e.Row.FindControl("txtBuyingDiscount");
                txtnetprice = (TextBox)e.Row.FindControl("txtNetBuyingPrice");
                if (dt.Rows[l]["CategoryID"] != "0")
                {
                    ddlcard.SelectedValue = Convert.ToString(dt.Rows[l]["CategoryID"]);
                    int categoryid = Convert.ToInt32(ddlcard.SelectedValue);
                    
                    fillproduct(categoryid, ddlproducts);
                    ddlproducts.SelectedValue = Convert.ToString(dt.Rows[l]["ProductID"]);
                }
                if (Convert.ToInt32(dt.Rows[l]["SupplierProductID"]) != 0)
                {
                    ddlcard.Enabled = false;
                    ddlproducts.Enabled = false;
                    txtprodvalue.Enabled = false;
                    txtselldiscount.Enabled = false;
                    txtbuyingdisocunt.Enabled = false;
                    txtnetprice.Enabled = false;
                }
                l++;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
        
    }
    protected void fillproduct(int categoryid,DropDownList ddlproducts)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        try
        {
            ds = new DataSet();
            Supplier = new ESupplier();
            Supplier.CategoryID = categoryid;
            ds = Supplier.GetProductDDL();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlproducts.DataSource = ds.Tables[0];
                ddlproducts.DataValueField = "ProductID";
                ddlproducts.DataTextField = "ProductName";
                ddlproducts.DataBind();
                ddlproducts.Items.Insert(0, new ListItem("--Any--", "0"));
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void gvSupplierProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        if (e.CommandName == "DeleteRow")
        {
            int rowcount = gvSupplierProduct.Rows.Count;
            if (rowcount != 1)
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int supplierproductid = Convert.ToInt32(((HiddenField)gvSupplierProduct.Rows[rowindex].FindControl("HDSupplierProductID")).Value);
                dt = new DataTable();
                dt.Columns.Add("ProductID");
                dt.Columns.Add("ProductValue");
                dt.Columns.Add("SupplierProductID");
                dt.Columns.Add("BuyingDiscount");
                dt.Columns.Add("NetBuyingPrice");
                dt.Columns.Add("SellingDefaultDiscount");
                dt.Columns.Add("CategoryID");
                DataRow dr;
                for (int i = 0; i <= gvSupplierProduct.Rows.Count-1; i++)
                {
                    if (rowindex != i)
                    {
                        dr = dt.NewRow();
                        dr["ProductID"] = ((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlProduct")).SelectedValue;
                        dr["ProductValue"] = ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtProductValue")).Text;
                        dr["SupplierProductID"] = ((HiddenField)gvSupplierProduct.Rows[i].FindControl("HDSupplierProductID")).Value;
                        dr["BuyingDiscount"] = ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtBuyingDiscount")).Text;
                        dr["NetBuyingPrice"] = ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtNetBuyingPrice")).Text;
                        dr["SellingDefaultDiscount"] = ((TextBox)gvSupplierProduct.Rows[i].FindControl("txtSellingDiscount")).Text;
                        dr["CategoryID"] = ((DropDownList)gvSupplierProduct.Rows[i].FindControl("ddlCategory")).SelectedValue;
                        dt.Rows.Add(dr);
                    }
                }
                Supplier = new ESupplier();
                ds = Supplier.GetCategoryDDL();
                dtCategory = ds.Tables[0];
                l = 0;
                gvSupplierProduct.DataSource = dt;
                gvSupplierProduct.DataBind();                
                if (supplierproductid != 0)
                {
                    Supplier.SupplierProductID = supplierproductid;
                    string result = Supplier.DeleteSupplierProduct();
                    dvSuccess.Visible = true;
                    lblSuccess.Text = result;
                    ShowTabs(1);
                    
                }
                else
                {

                }
            }
        }
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        ddlSupplier.SelectedIndex = 0;
        txtContactNoSearch.Text = "";
        txtMobileNoSearch.Text = "";
        txtEmailIDSearch.Text = "";
        ShowTabs(1);
        FillGrid();
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        txtEmailIDSearch.Text = "";
        FillGrid();
        ddlSupplier.SelectedIndex = 0;
        txtContactNoSearch.Text = "";
        txtMobileNoSearch.Text = "";
        txtEmailIDSearch.Text = "";
    }
    protected void gvSupplierProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    
}
