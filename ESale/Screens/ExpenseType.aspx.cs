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

public partial class Screens_Expense : System.Web.UI.Page
{
    // Entities Declaration.
    DataSet dsex;
    EexpenseType exobj;
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
               // FillStore();
                lblStatus.Text = "";
                FillGridView();
                lblStatus.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }

    }
    public void ShowTab(int TabNum)
    {
        if (TabNum == 1)
        {
            panelAddExpense.Visible = false;
            panelSearchExpense.Visible = true;
            GridViewExpense.Visible = false;
            lnkAdd.Text = "Add";
            lnkAdd.Visible = true;
            txtSearchExpense.Text = string.Empty;
            FillGridView();
            dvFooter.Visible = false;
        }
        else if (TabNum == 2)
        {
            panelAddExpense.Visible = true;
            panelSearchExpense.Visible = false;
            panelGridExpense.Visible = false;
            GridViewExpense.Visible = false;
            lnkAdd.Text = "Add";
            lnkAdd.Visible = false;
            dvFooter.Visible = true;
        }
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Text = "Add";
        txtAddExpense.Text = string.Empty;
        ShowTab(2);
        ShowButtons("Add");
        lblStatus.Text = string.Empty;
        ddladdStore.SelectedValue = "0";
        ControlStatus(true);
    }
    protected void imgSearch_Click(object sender, EventArgs e)
    {

        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        FillGridView();
    }
    // Added the data into the Database.
    protected void imgExpense_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        dsex = new DataSet();
        exobj = new EexpenseType();
        try
        {
            exobj.ExpenseType = txtAddExpense.Text;
            //exobj.StoreID = Convert.ToInt32(ddladdStore.SelectedValue);
            exobj.LoginID = Convert.ToInt32(Session["LOGINID"].ToString());
            result = exobj.EAddExpense();
            if (result != "")
            {
                dvSuccess.Visible = true;
                lblSuccess.Text = result;
                //lblStatus.CssClass = "SuccessMsg";
                txtAddExpense.Text = string.Empty;
               // ddladdStore.SelectedValue = "0";
                ShowTab(1);
                FillGridView();
                
            }
            else
            {
                ShowTab(2);
                dvFailure.Visible = true;
                lblStatus.Text = "Details already Exist";
               // lblStatus.CssClass = "ErrorMsg";
                txtAddExpense.Text = string.Empty;
            }
            
        }
        catch (Exception ex)
        {

            lblStatus.Text = ex.Message;
            lblStatus.CssClass = "ErrorMsg";
        }
    }
    //Update the Modified Data back to the Database.
    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        dsex = new DataSet();
        exobj = new EexpenseType();
        try
        {
            exobj.ExpenseType = txtAddExpense.Text.Trim();
            //exobj.StoreID = Convert.ToInt32(ddladdStore.SelectedValue);
            exobj.LoginID = Convert.ToInt32(Session["LOGINID"].ToString());
            exobj.ExpenseTypeID = Convert.ToInt32(HiddenFieldExpeseTypeID.Value);
            result = exobj.EUpdateExpense().ToString();
            if (result.Trim() == "Success")
            {
                ShowTab(1);
                dvSuccess.Visible = true;
                lblSuccess.Text = "Brand Details Updated Successfully";
               // lblStatus.CssClass = "SuccessMsg";
                txtAddExpense.Text = string.Empty;
                FillGridView();
            }
            else
            {
                ShowTab(2);
                dvFailure.Visible = true;
                lblStatus.Text = "Brand Details already Exist";
               // lblStatus.Text = "ErrorMsg";
                txtAddExpense.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {

            lblStatus.CssClass = "ErrorMsg";
            lblStatus.Text = ex.Message;
        }
    }
    //Clear the controls Data.
    protected void imgClear_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        txtAddExpense.Text = string.Empty;
        lblStatus.Text = string.Empty;
        ddladdStore.SelectedValue = "0";
        ControlStatus(true);
    }
    protected void GridViewExpense_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        dsex = new DataSet();
        exobj = new EexpenseType();
        lblStatus.Text = string.Empty;

        try
        {
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int ID = Convert.ToInt32(GridViewExpense.DataKeys[index].Value.ToString());
                HiddenFieldExpeseTypeID.Value = ID.ToString();
                exobj.ExpenseTypeID = ID;
                dsex = exobj.EViewForEdit();
                if (dsex.Tables[0].Rows.Count > 0)
                {
                    txtAddExpense.Text = dsex.Tables[0].Rows[0]["ExpenseType"].ToString();
                   // ddladdStore.SelectedValue = dsex.Tables[0].Rows[0]["StoreID"].ToString();
                    txtAddExpense.Enabled = false;
                    ddladdStore.Enabled = false;
                    ShowTab(2);
                }
                //lnkAdd.Text = "View";
                ShowButtons("View");
            }
            else if (e.CommandName == "EditRow")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int ID = Convert.ToInt32(GridViewExpense.DataKeys[index].Value.ToString());
                HiddenFieldExpeseTypeID.Value = ID.ToString();
                exobj.ExpenseTypeID = ID;
                dsex = exobj.EViewForEdit();
                if (dsex.Tables[0].Rows.Count > 0)
                {
                    txtAddExpense.Text = dsex.Tables[0].Rows[0]["ExpenseType"].ToString();
                   // ddladdStore.SelectedValue = dsex.Tables[0].Rows[0]["StoreID"].ToString();
                    txtAddExpense.Enabled = true;
                   // ddladdStore.Enabled = true;
                    ShowTab(2);
                    panelGridExpense.Visible = false;
                    panelAddExpense.Visible = true;
                }
                //lnkAdd.Text = "Edit";
                ShowButtons("Edit");
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int ID = Convert.ToInt32(GridViewExpense.DataKeys[index].Value.ToString());
                HiddenFieldExpeseTypeID.Value = ID.ToString();
                exobj.ExpenseTypeID = ID;
                //exobj.StoreID = Convert.ToInt32(ddladdStore.SelectedValue);
                result = exobj.EDeleteExpense().ToString();
                if (result.Trim() == "Success")
                {
                    ShowTab(1);
                    dvSuccess.Visible = true;
                    lblSuccess.Text = "Delte Transaction Successfully Completed";
                   // lblStatus.CssClass = "SuccessMsg";
                    lblStatus.Visible = true;
                    FillGridView();
                }
                else
                {
                    dvFailure.Visible = true;
                    lblStatus.Text = "Delete Transaction failure";
                    //lblStatus.CssClass = "ErroMsg";
                    lblStatus.Visible = true;
                }
                txtAddExpense.Text = string.Empty;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void GridViewExpense_RowCreated(object sender, GridViewRowEventArgs e)
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
    }
    protected void GridViewExpense_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridViewExpense_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    // Boolean Status of the Contorls.
    public void ControlStatus(bool Status)
    {
        txtAddExpense.Enabled = Status;
        lblStatus.Enabled = Status;
       // ddladdStore.Enabled = Status;
    }
    // Getting the data to the database and filling into GridView control.
    protected DataSet FillGridView()
    {
        dsex = new DataSet();
        exobj = new EexpenseType();
        try
        {
            if (txtSearchExpense.Text != "")
            {
                exobj.ExpenseType = txtSearchExpense.Text;
            }
            else
            {
                exobj.ExpenseType = "";
            }
            //if (Convert.ToInt32(ddlStore.SelectedValue) != 0)
            //{
            //    exobj.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            //}
            //else
            //{
            //    exobj.StoreID = 0;
            //}
            dsex = exobj.EfillGridView();
            if (dsex.Tables[0].Rows.Count > 0)
            {
                txtSearchExpense.Text = string.Empty;
                panelGridExpense.Visible = true;
                GridViewExpense.Visible = true;
                GridViewExpense.DataSource = null;
                GridViewExpense.DataSource = dsex.Tables[0];
                GridViewExpense.DataBind();
                GridViewExpense.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                dvFailure.Visible = true;
               // lblStatus.CssClass = "ErroMsg";
                lblStatus.Text = "No Record(s) Found";
                panelGridExpense.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblStatus.CssClass = "ErrorMsg";
            lblStatus.Text = ex.Message;
        }
        return dsex;
    }
    // Fill Storename in dropdown list.
    //public void FillStore()
    //{
    //    exobj = new EexpenseType();
    //    dsex = new DataSet();
    //    try
    //    {
    //        exobj.LoginID = Convert.ToInt32(Session["LOGINID"]);
    //        dsex = exobj.EfillStore();
    //        if (dsex.Tables[0].Rows.Count>0)
    //        {
    //            ddlStore.DataSource = dsex.Tables[0];
    //            ddlStore.DataTextField = "StoreName";
    //            ddlStore.DataValueField = "StoreID";
    //            ddlStore.DataBind();
    //            ddladdStore.DataSource = dsex.Tables[0];
    //            ddladdStore.DataTextField = "StoreName";
    //            ddladdStore.DataValueField = "StoreID";
    //            ddladdStore.DataBind();
    //            ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));
    //            ddladdStore.Items.Insert(0, new ListItem("--Any--", "0"));
    //        }
    //        else
    //        {
    //            ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));
    //            ddladdStore.Items.Insert(0, new ListItem("--Any--", "0"));
    //        }
    //        if (Convert.ToInt32(Session["LOGINID"]) != 1 && dsex.Tables[0].Rows.Count == 1)
    //        {
    //            ddlStore.SelectedValue = Convert.ToString(dsex.Tables[0].Rows[0]["StoreID"]);
    //            ddladdStore.SelectedValue = Convert.ToString(dsex.Tables[0].Rows[0]["StoreID"]);
    //            ddlStore.Enabled = false;
    //        }
          
    //    }
    //    catch (Exception ex)
    //    {

    //        lblStatus.CssClass = "ErrorMsg";
    //        lblStatus.Text = ex.Message;
    //    }
    //}
    void ShowButtons(String Mode)
    {
        if (string.Compare(Mode, "Search", true) == 0)
        {
            imgClearExpense.Visible = true;
            dvclear.Visible = true;
            imgAddExpense.Visible = false;
            dvisave.Visible = false;
            imgUpdateExpense.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = false;
            btncan.Visible = false;
        }
        else if (string.Compare(Mode, "View", true) == 0)
        {
            imgClearExpense.Visible = false;
            dvclear.Visible = false;
            imgAddExpense.Visible = false;
            dvisave.Visible = false;
            imgUpdateExpense.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = true;
            btncan.Visible = true;
        }
        else if (string.Compare(Mode, "Edit", true) == 0)
        {
            imgClearExpense.Visible = true;
            dvclear.Visible = true;
            imgAddExpense.Visible = false;
            dvisave.Visible = false;
            imgUpdateExpense.Visible = true;
            dvupdate.Visible = true;
            dvcancel.Visible = true;
            btncan.Visible = true;
        }
        else if (string.Compare(Mode, "Add", true) == 0)
        {
            dvcancel.Visible = true;
            dvclear.Visible = true;
            btncan.Visible = true;
            imgClearExpense.Visible = true;
            imgAddExpense.Visible = true;
            dvisave.Visible = true;
            imgUpdateExpense.Visible = false;
            dvupdate.Visible = false;
        }
    }
    // Check user Login Permissions.
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
                imgUpdateExpense.Visible = EditPermission;
                imgClearExpense.Visible = EditPermission;

            }
            else
            {
                imgUpdateExpense.Visible = EditPermission;
                imgClearExpense.Visible = EditPermission;

            }
            if (addPermission == true)
            {
                imgAddExpense.Visible = addPermission;
                lnkAdd.Visible = addPermission;
            }
            else
            {
                imgAddExpense.Visible = addPermission;
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
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        FillGridView();
        ddlStore.SelectedIndex = 0;
        txtSearchExpense.Text = "";
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        ddlStore.SelectedIndex = 0;
        txtSearchExpense.Text = "";
        ShowTab(1);
        FillGridView();
    }
}
