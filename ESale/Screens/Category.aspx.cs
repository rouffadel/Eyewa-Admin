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
public partial class Screens_Category : System.Web.UI.Page
{
    // Entities Declaration.
    DataSet dscategory;
    ECategory ECategoryObj;
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
                imgUpdateCategory.Visible = EditPermission;
                btncan.Visible = EditPermission;
                imgClearCategory.Visible = EditPermission;

            }
            else
            {
                imgUpdateCategory.Visible = EditPermission;
                //btncan.Visible = EditPermission;
                //imgClear.Visible = EditPermission;

            }
            if (addPermission == true)
            {
                imgAddCategory.Visible = addPermission;
                btncan.Visible = addPermission;
                imgClearCategory.Visible = addPermission;
                lirole.Visible = addPermission;
                lnkAdd.Visible = addPermission;
            }
            else
            {
                imgAddCategory.Visible = addPermission;
                //btncan.Visible = addPermission;
                //imgClear.Visible = addPermission;
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
    
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        lnkAdd.Text = "Add";
        txtAddCategory.Text = string.Empty;
        imgAddCategory.Enabled = true;
        ShowTab(2);
        ShowButtons("Add");
        lblStatus.Text = "";
        lblSuccess.Text = "";
        dvFailure.Visible = false;
        dvSuccess.Visible = false; 
        ControlStatus(true);

    }
    public void ShowTab(int TabNum)
    {
        if (TabNum == 1)
        {
            dvFooter.Visible = false;
            panelAddCategory.Visible = false;
            panelSearchCategory.Visible = true;
            //panelGridOrganization.Visible = false;
            GridViewCategory.Visible = false;
            FillGridView();
            lnkAdd.Visible = true;
            lnkAdd.Text = "Add";
            txtSearchCategory.Text = string.Empty;
        }
        else if (TabNum == 2)
        {
            dvFooter.Visible = true;
            panelAddCategory.Visible = true;
            panelSearchCategory.Visible = false;
            panelGridCategory.Visible = false;
            GridViewCategory.Visible = false;
            lnkAdd.Visible = false;
            lnkAdd.Text = "Add";
        }
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        txtSearchCategory.Text = "";
        FillGridView();
    }
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvSuccess.Visible = false;
        dvFailure.Visible = false;
        FillGridView();
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        //txtSearchCategory.Text = "";
        ShowTab(1);        
        FillGridView();
    }
    protected void GridViewCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        ECategoryObj = new ECategory();
        dscategory = new DataSet();
        try
        {
            // Click on View Button on GridView View command will be executed.
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int ID = Convert.ToInt32(GridViewCategory.DataKeys[index].Value.ToString());
                HiddenFieldCategoryId.Value = ID.ToString();
                ECategoryObj.PCategoryId = ID;
                dscategory = ECategoryObj.EViewEditCategory();
                if (dscategory.Tables[0].Rows.Count > 0)
                {
                    txtAddCategory.Text = dscategory.Tables[0].Rows[0]["CategoryName"].ToString();
                    txtAddCategory.Enabled = false;
                    ShowTab(2);
                }
                //lnkAdd.Text = "View";
                ShowButtons("View");

            }
            // Click on GridView  Delete Button this command will be executed.
            else if (e.CommandName == "Edits")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int ID = Convert.ToInt32(GridViewCategory.DataKeys[index].Value.ToString());
                HiddenFieldCategoryId.Value = ID.ToString();
                ECategoryObj.PCategoryId = ID;
                dscategory = ECategoryObj.EViewEditCategory();
                if (dscategory.Tables[0].Rows.Count>0)
                {
                    txtAddCategory.Text = dscategory.Tables[0].Rows[0]["CategoryName"].ToString();
                    txtAddCategory.Enabled = true;
                    ShowTab(2);
                    panelGridCategory.Visible = false;
                    panelAddCategory.Visible = true;
                }
               // lnkAdd.Text = "Edit";
                ShowButtons("Edit");
            }
            // Click on Delete Button on GridView this command will be executed.
            else if (e.CommandName == "Deletes")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int ID = Convert.ToInt32(GridViewCategory.DataKeys[index].Value.ToString());
                HiddenFieldCategoryId.Value = ID.ToString();
                ECategoryObj.PCategoryId = ID;
                dscategory = ECategoryObj.EDeleteCategory();
                if (dscategory.Tables[0].Rows[0]["Status"].ToString() == "Success")
                {
                    ShowTab(1);
                   // lblStatus.CssClass = "SuccessMsg";
                    FillGridView();
                   dvSuccess.Visible = true;                   
                   lblSuccess.Text = "Delete Transaction Successful Completed";
                }
                else if (dscategory.Tables[0].Rows[0]["Status"].ToString() == "Record cannot be deleted as it is referenced with other transactions")
                {

                    //lblStatus.CssClass = "ErrorMsg";
                    dvFailure.Visible=true;
                    lblStatus.Text = "Record cannot be deleted as it is referenced with other transactions";
                }
                else
                {
                   dvFailure.Visible=true;
                    lblStatus.Text = "Delete Transaction UnSuccessful";
                }
                txtAddCategory.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }
    protected void GridViewCategory_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void GridViewCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridViewCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    // Record(s) Inseted to the database.
    protected void imgAdd_Click(object sender,EventArgs e)
    {
        imgAddCategory.Enabled = false;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        ECategoryObj = new ECategory();
        dscategory = new DataSet();
        try
        {
            ECategoryObj.PLoginSessionId = Convert.ToInt32(Session["LOGINID"].ToString());
            ECategoryObj.PCategoryName = txtAddCategory.Text.Trim();
            result = ECategoryObj.EAddCategory();
            if (result == "Success")
            {
                ShowTab(1);
                dvSuccess.Visible = true;
                lblSuccess.Text = "Category Details inserted Successfully";
               // lblStatus.CssClass = "SuccessMsg";
                txtAddCategory.Text = string.Empty;
                FillGridView();
            }
            else
            {
                ShowTab(2);
                lblStatus.Text = "Details already exist";
                dvFailure.Visible = true;
               // lblStatus.CssClass = "ErrorMsg";
                txtAddCategory.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {

            dvFailure.Visible = true;
            lblStatus.CssClass = "ErrorMsg";
        }
        
    }
    // Update the record to the Database.
    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        ECategoryObj = new ECategory();
        dscategory = new DataSet();
        try
        {
            ECategoryObj.PLoginSessionId = Convert.ToInt32(Session["LOGINID"].ToString());
            ECategoryObj.PCategoryName = txtAddCategory.Text.Trim();
            ECategoryObj.PCategoryId = Convert.ToInt32(HiddenFieldCategoryId.Value);
            result = ECategoryObj.EUpdateCategory().ToString();
            if (result == "Success")
            {
                ShowTab(1);
                dvSuccess.Visible = true;
                lblSuccess.Text = "Category Details Updated Successfully";
               // lblStatus.CssClass = "SuccessMsg";
                txtAddCategory.Text = string.Empty;
                FillGridView();

            }
            else
            {
                ShowTab(2);
                dvFailure.Visible = true;
                lblStatus.Text = "Category Details already Exist";
                //lblStatus.CssClass = "ErroMsg";
                txtAddCategory.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            dvFailure.Visible = true;
            //lblStatus.Text = ex.Message;
            lblStatus.CssClass = "ErrorMsg";
        }
    }
    // Clear the Controls Data.
    protected void imgClear_Click(object sender, EventArgs e)
    {
        txtAddCategory.Text = string.Empty;
        //dvFailure
        //lblStatus.Text = string.Empty;
        ControlStatus(true);
    }
    // Getting the data to the database and filling into GridView control.
    protected void FillGridView()
    {
        dscategory = new DataSet();
        ECategoryObj = new ECategory();
        try
        {
            if (txtSearchCategory.Text !="")
            {
                ECategoryObj.PCategoryName = txtSearchCategory.Text;
            }
            else
            {
                ECategoryObj.PCategoryName = "%";
            }
            dscategory = new DataSet();
            dscategory = ECategoryObj.EFillGridView();
            if (dscategory.Tables[0].Rows.Count>0)
            {
               // txtSearchCategory.Text = string.Empty;
                panelGridCategory.Visible = true;
                GridViewCategory.Visible = true;
                GridViewCategory.DataSource = null;
                GridViewCategory.DataSource = dscategory;
                GridViewCategory.DataBind();
                GridViewCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {

                dvFailure.Visible = true;//lblStatus.CssClass = "ErrorMsg";
                lblStatus.Text = "No Records Found";
                panelGridCategory.Visible = false;
               // txtSearchCategory.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            dvFailure.Visible = true;
            lblStatus.Text = ex.Message;
            
        }
        
    }
    void ShowButtons(String Mode)
    {
        if (string.Compare(Mode, "Search", true) == 0)
        {
            imgClearCategory.Visible = true;
            imgUpdateCategory.Visible = false;
            imgAddCategory.Visible = false;           
            dvclear.Visible = true;           
            dvisave.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = false;
            btncan.Visible = false;
        }
        else if (string.Compare(Mode,"View",true) == 0)
        {
            imgClearCategory.Visible = false;
            imgUpdateCategory.Visible = false;
            imgAddCategory.Visible = false;
            dvclear.Visible = false;
            dvisave.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = true;
            btncan.Visible = true;
        }
        else if (string.Compare(Mode,"Edit",true) == 0)
        {
            imgClearCategory.Visible = true;
            dvclear.Visible = true;
            imgAddCategory.Visible = false;
            dvisave.Visible = false;
            imgUpdateCategory.Visible = true;
            dvupdate.Visible = true;
            dvcancel.Visible = true;
            btncan.Visible = true;
        }
        else if (string.Compare(Mode,"Add",true) == 0)
        {
            dvcancel.Visible = true;
            dvclear.Visible = true;
            btncan.Visible = true;
            imgClearCategory.Visible = true;
            imgAddCategory.Visible = true;
            dvisave.Visible = true;
            imgUpdateCategory.Visible = false;
            dvupdate.Visible = false;
        }
    }
    void ControlStatus(bool status)
    {
        txtAddCategory.Enabled = status;
       // lblStatus.Enabled = status;
    }
}
