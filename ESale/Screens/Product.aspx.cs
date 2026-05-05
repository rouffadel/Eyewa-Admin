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
using GenCode128;
using System.Drawing.Imaging;
using System.IO;
using ESaleEntity;

public partial class Screens_Product : System.Web.UI.Page
{
    
    EProduct product;
    DataSet ds;
    string loginid = "";

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
                FillCategory();
                FillBrand();
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
    public void FillCategory()
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
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
                DDLCategory.Items.Insert(0, new ListItem("--Any--","0" ));

                DDLCategorySearch.DataSource = ds.Tables[0];
                DDLCategorySearch.DataTextField = "CategoryName";
                DDLCategorySearch.DataValueField = "CategoryID";
                DDLCategorySearch.DataBind();
                DDLCategorySearch.Items.Insert(0, new ListItem("--Any--","0"));
            }
            else
            {
                DDLCategory.Items.Insert(0, new ListItem( "--Any--","0"));
                DDLCategorySearch.Items.Insert(0, new ListItem( "--Any--","0"));
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    public void FillBrand()
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            ds = new DataSet();
            product = new EProduct();
            ds = product.ddlBrand();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBrandSearch.DataSource = ds.Tables[0];
                ddlBrandSearch.DataTextField = "BrandName";
                ddlBrandSearch.DataValueField = "BrandID";
                ddlBrandSearch.DataBind();
                ddlBrandSearch.Items.Insert(0, new ListItem("--Any--", "0"));

                ddlBrand.DataSource = ds.Tables[0];
                ddlBrand.DataTextField = "BrandName";
                ddlBrand.DataValueField = "BrandID";
                ddlBrand.DataBind();
                ddlBrand.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                ddlBrand.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlBrandSearch.Items.Insert(0, new ListItem("--Any--", "0"));
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
               // imgClear.Visible = EditPermission;

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
            dvFooter.Visible = false;
            pnlAdd.Visible = false;
            pnlSearch.Visible = true;
            pnlSearchGrid.Visible = false;
            lnkAdd.Text = "Add";
            lnkAdd.Visible = true;
        }
        else if (TabNum == 2)
        {
            dvFooter.Visible = true;
            pnlAdd.Visible = true;
            pnlSearch.Visible = false;
            pnlSearchGrid.Visible = false;
            lnkAdd.Text = "Add";
            lnkAdd.Visible = false;
        }
    }
   
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ShowTabs(2);
        ClearControls();
        imgSave.Enabled = true;
        showButtons("Add");
        EnableControls(true);
        lblStatus.Text = "";
        chkActive.Checked = true;
        lblBarCode.Visible = false;
        imgBarcode.Visible = false;
        lblProduct.Visible = false;
        lblPrice.Visible = false;
    }
    void ClearControls()
    {
        DDLCategory.SelectedValue = "0";
        txtDefaultSellingDiscount.Text = string.Empty;
        txtProductName.Text = string.Empty;
        txtProductValue.Text = string.Empty;
        ddlBrand.SelectedValue = "0";
        txtMaxDiscPer.Text = "75.00";
        chkActive.Checked = false;

    }
    void EnableControls(bool status)
    {
        DDLCategory.Enabled = status;
        txtDefaultSellingDiscount.Enabled = status;
        txtProductName.Enabled = status;
        txtProductValue.Enabled = status;
        ddlBrand.Enabled = status;
        txtMaxDiscPer.Enabled = status;
        chkActive.Enabled = status;
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
            ds = new DataSet();
            product = new EProduct();
            if (DDLCategorySearch.SelectedValue != "0")
                product.CategoryID = Convert.ToInt32(DDLCategorySearch.SelectedValue);
            if (txtProductSearch.Text != "")
                product.ProductName = txtProductSearch.Text;
            if (ddlBrandSearch.SelectedValue != "0")
                product.BrandID = Convert.ToInt32(ddlBrandSearch.SelectedValue);
            if (txtProductSearch.Text != "")
                product.ProductName = txtProductSearch.Text;
            else
                product.ProductName = "";
            ds = product.GetProductGrid();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvProduct.DataSource = ds.Tables[0];                
                gvProduct.DataBind();
                //lblStatus.Text = "";
                //pnlAdd.Visible = false;
                //pnlSearch.Visible = true;
                //ClearControls();
                //DDLCategorySearch.SelectedValue = "0";
                //ddlBrandSearch.SelectedValue = "0";
                //txtProductSearch.Text = "";
                pnlSearchGrid.Visible = true;
                gvProduct.HeaderRow.TableSection = TableRowSection.TableHeader;
                //ds.Columns[7].Visible = false;
            }
            else
            {
                gvProduct.DataSource = null;
                gvProduct.DataBind();
                dvFailure.Visible = true;
                lblStatus.Text = "No Record Found.";
                pnlAdd.Visible = false;
                pnlSearch.Visible = true;
                pnlSearchGrid.Visible = false;
                DDLCategorySearch.SelectedValue = "0";
                ddlBrandSearch.SelectedValue = "0";
                txtProductSearch.Text = "";
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
        string result = string.Empty;
        try
        {
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
                product.ProductValue =Convert.ToSingle(txtProductValue.Text);
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
            product.MaxDiscountPer =Convert.ToDecimal(txtMaxDiscPer.Text);
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
                 ShowTabs(1);                
                
                 string filename = txtProductName.Text + ".png";
                 /*BarcodeLib.Barcode b=new BarcodeLib.Barcode();
                  System.Drawing.Image i=b.Encode(BarcodeLib.TYPE.CODE128,filename);*/
                 System.Drawing.Image i = Code128Rendering.MakeBarcodeImage(txtProductName.Text, 2, true);
                 if (File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                 {
                     File.Delete(Server.MapPath("~/images/BarCodeImages/" + filename));
                 }
                 string path = Server.MapPath("~/images/BarCodeImages");
                 i.Save(path + "/" + filename, ImageFormat.Png);
                 ClearControls();
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
        string result = string.Empty;
        try
        {
            product = new EProduct();
            product.ProductID = Convert.ToInt32(HDProductID.Value);
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
            if (chkActive.Checked)
                product.Active = true;
            else
                product.Active = false;
            product.MaxDiscountPer = Convert.ToDecimal(txtMaxDiscPer.Text);
            product.LoginID = loginid;
            result = product.Update();
            if (result == "Record Updated successfully.")
            {
                string filename = txtProductName.Text + ".png";
                System.Drawing.Image i = Code128Rendering.MakeBarcodeImage(txtProductName.Text, 2, true);
                if (File.Exists(Server.MapPath("~/images/BarCodeImages/" + filename)))
                {
                    File.Delete(Server.MapPath("~/images/BarCodeImages/" + filename));
                }
                string path = Server.MapPath("~/images/BarCodeImages");
                i.Save(path + "/" + filename, ImageFormat.Png);              
                ShowTabs(1);
                ClearControls();                
                FillGrid();
                dvSuccess.Visible = true;
                lblSuccess.Text = result;
            }
            else
            {
                ShowTabs(2);
                dvFailure.Visible = false;
                lblStatus.Text = result;
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
    protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            lblSuccess.Text = "";
            lblStatus.Text = "";
            ds = new DataSet();
            product = new EProduct();
            if (e.CommandName == "View")
            {
              
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int productid = Convert.ToInt32(gvProduct.DataKeys[rowindex].Value);
                product.ProductID = productid;
                ds = product.GetProductForViewAndEdit();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtProductName.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductName"]);
                    txtProductValue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                    txtDefaultSellingDiscount.Text = Convert.ToString(ds.Tables[0].Rows[0]["SellingDefaultDiscount"]);
                    txtMaxDiscPer.Text=Convert.ToString(ds.Tables[0].Rows[0]["MaxDiscount"]);
                    DDLCategory.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CategoryID"]);
                    ddlBrand.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                    chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);
                    if (Convert.ToString(ds.Tables[0].Rows[0]["BarCodePath"]) != "")
                    {
                        imgBarcode.ImageUrl = "~/images/BarCodeImages/" + Convert.ToString(ds.Tables[0].Rows[0]["BarCodePath"]);
                        imgBarcode.Visible = true;
                    }
                    else
                        imgBarcode.Visible = false;                   
                }
                ShowTabs(2);
                EnableControls(false);
                showButtons("View");
                //imgClear.Visible = false;
                //imgSave.Visible = false;
                //imgupdate.Visible = false;
                //lnkAdd.Text = "View";
                //lblStatus.Text = "";
                lblProduct.Visible = true;
                lblProduct.Text = txtProductName.Text;
                lblPrice.Visible = true;
                lblPrice.Text = txtProductValue.Text;
            }
            else if (e.CommandName == "EditRow")
            {
                
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int productid = Convert.ToInt32(gvProduct.DataKeys[rowindex].Value);
                product.ProductID = productid;
                ds = product.GetProductForViewAndEdit();
                HDProductID.Value = productid.ToString();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtProductName.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductName"]);
                    txtProductValue.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductValue"]);
                    txtDefaultSellingDiscount.Text = Convert.ToString(ds.Tables[0].Rows[0]["SellingDefaultDiscount"]);
                    txtMaxDiscPer.Text = Convert.ToString(ds.Tables[0].Rows[0]["MaxDiscount"]);
                    DDLCategory.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["CategoryID"]);
                    ddlBrand.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["BrandID"]);
                    chkActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);
                    if (Convert.ToString(ds.Tables[0].Rows[0]["BarCodePath"]) != "")
                    {
                        imgBarcode.ImageUrl = "~/images/BarCodeImages/" + Convert.ToString(ds.Tables[0].Rows[0]["BarCodePath"]);
                        imgBarcode.Visible = true;
                    }
                    else
                        imgBarcode.Visible = false;
                }
                ShowTabs(2);
                EnableControls(true);
                showButtons("Edit");
                //imgupdate.Visible = true;
                //imgSave.Visible = false;
                //imgClear.Visible = true;
                //lnkAdd.Text = "Edit";
                //lblStatus.Text = "";
                lblProduct.Visible = true;
                lblProduct.Text = txtProductName.Text;
                lblPrice.Visible = true;
                lblPrice.Text = txtProductValue.Text;
            }
            else if (e.CommandName == "Deleting")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                int productid = Convert.ToInt32(gvProduct.DataKeys[rowindex].Value);
                product.ProductID = productid;
                string result = product.Delete();
                if (result == "Record Deleted Successfully.")
                {                    
                    ShowTabs(1);
                    EnableControls(true);
                    //imgClear.Visible = true;
                    //imgSave.Visible = true;
                    //imgupdate.Visible = false;
                    FillGrid();
                    dvSuccess.Visible = true;
                    lblSuccess.Text = result;
                }
                else
                {
                   // lblStatus.Text = result;
                    //lblStatus.CssClass = "ErroMsg";
                    dvFailure.Visible = true;
                    lblStatus.Text = result;
                }                
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void gvProduct_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        ddlBrandSearch.SelectedIndex = 0;
        DDLCategorySearch.SelectedIndex = 0;
        txtProductSearch.Text = "";
        ShowTabs(1);
        FillGrid();
    }
    protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvProduct_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlBrandSearch.SelectedIndex = 0;
        DDLCategorySearch.SelectedIndex = 0;
        txtProductSearch.Text = "";
        FillGrid();
    }
    protected void DDLCategorySearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        lblStatus.Text = string.Empty;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
    }
    protected void ddlBrandSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        lblStatus.Text = string.Empty;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
    }
  
}
