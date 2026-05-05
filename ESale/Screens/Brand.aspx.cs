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
using System.IO;

public partial class Screens_Brand : System.Web.UI.Page
{
    // Entities Declaration.
    DataSet dsBrand;
    EBrand objBrand = new EBrand();
    ECheckPermission ECPobj;
    static bool addPermission = false;
    static bool viewPermission = true;
    static bool EditPermission = true;
    static bool deletepermission = true;
    string ScreenUrl = string.Empty;
    string result;
    string loginid = "";
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
                ShowTab(1);
                lblStatus.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
                FillGridView();
            }
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }
    }
    
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        lblSuccess.Text = "";
        dvFailure.Visible = false;
        dvSuccess.Visible = false; 
        lnkAdd.Text = "Add";
        imgAddBrand.Enabled = true;
        txtAddBrand.Text = string.Empty;
        ShowTab(2);
        ShowButtons("Add");
        lblStatus.Text = string.Empty;
        ControlStatus(true);
    }
    // Search the Record that are avilable in database.
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvSuccess.Visible = false;
        dvFailure.Visible = false;
        FillGridView();
    }
    protected void GridViewBrand_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblStatus.Text = "";
        lblSuccess.Text = "";
        dvFailure.Visible = false;
        dvSuccess.Visible = false; 
        dsBrand = new DataSet();
        objBrand = new EBrand();
        lblStatus.Text = string.Empty;

        try
        {
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int ID = Convert.ToInt32(GridViewBrand.DataKeys[index].Value.ToString());
                HiddenFieldBrandId.Value = ID.ToString();
                objBrand.BrandID = ID;
                dsBrand = objBrand.EViewForEditBrand();
                if (dsBrand.Tables[0].Rows.Count > 0)
                {
                    txtAddBrand.Text = dsBrand.Tables[0].Rows[0]["BrandName"].ToString();
                    txtAddBrand.Enabled = false;
                    chkActive.Checked = Convert.ToBoolean(dsBrand.Tables[0].Rows[0]["IsActive"]);
                    chkActive.Enabled = false;
                    ShowTab(2);
                }
                //lnkAdd.Text = "View";
                ShowButtons("View");
            }
            else if (e.CommandName == "Edits")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int ID = Convert.ToInt32(GridViewBrand.DataKeys[index].Value.ToString());
                HiddenFieldBrandId.Value = ID.ToString();
                objBrand.BrandID = ID;
                dsBrand = objBrand.EViewForEditBrand();
                if (dsBrand.Tables[0].Rows.Count > 0)
                {
                    txtAddBrand.Text = dsBrand.Tables[0].Rows[0]["BrandName"].ToString();
                    txtAddBrand.Enabled = true;
                    chkActive.Checked = Convert.ToBoolean(dsBrand.Tables[0].Rows[0]["IsActive"]);
                    chkActive.Enabled = true;
                    ShowTab(2);
                    panelGridBrand.Visible = false;
                    panelAddBrand.Visible = true;
                }
                //lnkAdd.Text = "Edit";
                ShowButtons("Edit");
            }
            else if (e.CommandName == "Deletes")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int ID = Convert.ToInt32(GridViewBrand.DataKeys[index].Value.ToString());
                HiddenFieldBrandId.Value = ID.ToString();
                objBrand.BrandID = ID;
                result = objBrand.EDeleteBrand().ToString();
                if (result == "Success")
                {
                    ShowTab(1);
                    
                    //lblStatus.CssClass = "SuccessMsg";
                    dvSuccess.Visible = true;
                    FillGridView();
                    lblSuccess.Text = "Delete Transaction Successfully Completed";
                }
                else
                {
                   // lblStatus.Text = result;
                    //lblStatus.CssClass = "ErroMsg";
                    dvFailure.Visible = true;
                    lblStatus.Visible = true;
                }
                txtAddBrand.Text = string.Empty;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void GridViewBrand_RowCreated(object sender, GridViewRowEventArgs e)
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
                    LinkButton ForTdView=(LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible = true;
                    ForTdEdit.Visible = true;
                    ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission  == true && EditPermission == false && deletepermission == true)
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
                    LinkButton ForTdView=(LinkButton)e.Row.FindControl("lnkbtnView");
                    LinkButton ForTdEdit=(LinkButton)e.Row.FindControl("lnkbtnEdit");
                    LinkButton ForTdDelete=(LinkButton)e.Row.FindControl("lnkbtnDel");
                    ForTdView.Visible=true;
                    ForTdEdit.Visible=false;
                    ForTdDelete.Visible=false;
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
    }
    protected void GridViewBrand_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridViewBrand_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void imgAdd_Click(object sender, EventArgs e)
    {
        imgAddBrand.Enabled = false;
        lblStatus.Text = "";
        lblSuccess.Text = "";
        dvFailure.Visible = false;
        dvSuccess.Visible = false; 
        dsBrand = new DataSet();
        objBrand = new EBrand();
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
                ShowTab(1);
                dvSuccess.Visible = true;
                lblSuccess.Text = "Brand Details Inserted Successfully";
                //lblStatus.CssClass = "SuccessMsg";
                txtAddBrand.Text = string.Empty;
                FillGridView();
            }
            else
            {
                ShowTab(2);
                dvFailure.Visible = true;
                lblStatus.Text = "Details already Exist";
                //lblStatus.CssClass = "ErrorMsg";
                txtAddBrand.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            dvFailure.Visible = true;
            lblStatus.Text = ex.Message;
            //lblStatus.CssClass = "ErrorMsg";
        }
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        txtAddBrand.Text = "";
        ShowTab(1);
        FillGridView();
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        txtSearchBrand.Text = "";
        FillGridView();
    }
    // Update back to the database.
    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        lblSuccess.Text = "";
        dvFailure.Visible = false;
        dvSuccess.Visible = false; 
        dsBrand = new DataSet();
        objBrand = new EBrand();
        try
        {
            objBrand.BrandName = txtAddBrand.Text.Trim();
            objBrand.LoginID = Convert.ToInt32(Session["LOGINID"].ToString());
            objBrand.BrandID = Convert.ToInt32(HiddenFieldBrandId.Value);
            if (chkActive.Checked)
                objBrand.IsActive = true;
            else
                objBrand.IsActive = false;
            result = objBrand.EUpdateBrand().ToString();
            if (result == "Success")
            {
                ShowTab(1);
                dvSuccess.Visible = true;
                lblSuccess.Text = "Brand Details Updated Successfully";
                //lblStatus.CssClass = "SuccessMsg";
                txtAddBrand.Text = string.Empty;
                FillGridView();
            }
            else
            {
                ShowTab(2);
                dvFailure.Visible = true;
                lblStatus.Text = "Brand Details already Exist";
               // lblStatus.Text = "ErrorMsg";
                txtAddBrand.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {

            dvFailure.Visible = true;
            lblStatus.Text = ex.Message;
        }
    }
    // Clear the Conttlos Data.
    protected void imgClear_Click(object sender, EventArgs e)
    {
        txtAddBrand.Text = string.Empty;
        lblStatus.Text = string.Empty;
        chkActive.Checked = false;
        ControlStatus(true);
       // rfvAddBrand.
    }
    public void ControlStatus(bool Status)
    {
        txtAddBrand.Enabled = Status;
        lblStatus.Enabled = Status;
        chkActive.Enabled = Status;
    }
    public void ShowTab(int TabNum)
    {
        if (TabNum == 1)
        {
            dvFooter.Visible = false;
            panelAddBrand.Visible = false;
            panelSearchBrand.Visible = true;
            GridViewBrand.Visible = false;            
            lnkAdd.Text = "Add";
            lnkAdd.Visible = true;
            FillGridView();
            txtSearchBrand.Text = string.Empty;
            //lblStatus.Text = string.Empty;
        }
        else if (TabNum == 2)
        {
            dvFooter.Visible = true;
            panelAddBrand.Visible = true;
            panelSearchBrand.Visible = false;
            panelGridBrand.Visible = false;
            GridViewBrand.Visible = false;
            lnkAdd.Text = "Add";
            lnkAdd.Visible = false;
        }
    }
    // Implemetns the img buttons Status.
    void ShowButtons(String Mode)
    {
        if (string.Compare(Mode, "Search", true) == 0)
        {
            imgClearBrand.Visible = true;
            imgUpdateBrand.Visible = false;
            imgAddBrand.Visible = false;
            dvclear.Visible = true;
            dvisave.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = false;
            btncan.Visible = false;
        }
        else if (string.Compare(Mode, "View", true) == 0)
        {
            imgClearBrand.Visible = false;
            imgUpdateBrand.Visible = false;
            imgAddBrand.Visible = false;
            dvclear.Visible = false;
            dvisave.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = true;
            btncan.Visible = true;
        }
        else if (string.Compare(Mode, "Edit", true) == 0)
        {
            imgAddBrand.Visible = false;
            imgUpdateBrand.Visible = true;
            imgClearBrand.Visible = true;
            dvclear.Visible = true;
            dvisave.Visible = false;
            dvupdate.Visible = true;
            dvcancel.Visible = true;
            btncan.Visible = true;
        }
        else if (string.Compare(Mode, "Add", true) == 0)
        {
            imgClearBrand.Visible = true;
            imgUpdateBrand.Visible = false;
            imgAddBrand.Visible = true;
            dvcancel.Visible = true;
            dvclear.Visible = true;
            btncan.Visible = true;
            dvisave.Visible = true;
            dvupdate.Visible = false;
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
                imgUpdateBrand.Visible = EditPermission;
                imgClearBrand.Visible = EditPermission;
                btncan.Visible = EditPermission;
            }
            else
            {
                imgUpdateBrand.Visible = EditPermission;
               // imgClearBrand.Visible = EditPermission;

            }
            if (addPermission == true)
            {
                imgAddBrand.Visible = addPermission;
                lnkAdd.Visible = addPermission;
                btncan.Visible = addPermission;
                lirole.Visible = addPermission;
                lnkAdd.Visible = addPermission;
            }
            else
            {
                imgAddBrand.Visible = addPermission;
                lnkAdd.Visible = addPermission;
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
    // Getting the data to the database and filling into GridView control.
    protected DataSet FillGridView()
    {
        dsBrand = new DataSet();
        objBrand = new EBrand();
        try
        {
            if (txtSearchBrand.Text != "")
            {
                objBrand.BrandName = txtSearchBrand.Text;
            }
            else
            {
                objBrand.BrandName = "%";
            }
            dsBrand = objBrand.EFillGridView();
            if (dsBrand.Tables[0].Rows.Count > 0)
            {
               // txtSearchBrand.Text = string.Empty;
                panelGridBrand.Visible = true;
                GridViewBrand.Visible = true;
                GridViewBrand.DataSource = null;
                GridViewBrand.DataSource = dsBrand;
                GridViewBrand.DataBind();
                GridViewBrand.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                //lblStatus.CssClass = "ErroMsg";
                dvFailure.Visible = true;
                lblStatus.Text = "No Record(s) Found";
                panelGridBrand.Visible = false;
               // txtSearchBrand.Text = string.Empty;
            }
         

        }
        catch (Exception ex)
        {
            dvFailure.Visible = true;
            lblStatus.Text = ex.Message;
        }
        return dsBrand;
    }
}
