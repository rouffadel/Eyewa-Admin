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

public partial class Admin_Roles : System.Web.UI.Page
{
    ERole eobj;
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
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }

        if (!IsPostBack)
        {
            if (Session["LOGINID"] != null)
            {
                ViewState["LoginID"] = Session["LOGINID"].ToString();
                loginid = Session["LOGINID"].ToString();
                ShowTabs(1);
                lblStatus.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
                FillRoleGrid();
            }
            else
            {
                Response.Redirect("~\\Login.aspx");
            }
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
            if (ds.Tables[0].Rows.Count > 0)
            {
                addPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["ADD"].ToString());
                viewPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["VIEW"].ToString());
                deletepermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["DELETE"].ToString());
                EditPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["EDIT"].ToString());

            }

            if (EditPermission == true)
            {
                imgUpdateRole.Visible = EditPermission;
                btncan.Visible = EditPermission;
                imgClear.Visible = EditPermission;

            }
            else
            {
                imgUpdateRole.Visible = EditPermission;
                //btncan.Visible = EditPermission;
                //imgClear.Visible = EditPermission;

            }
            if (addPermission == true)
            {
                imgAddRole.Visible = addPermission;
                btncan.Visible = addPermission;
                imgClear.Visible = addPermission;
                lirole.Visible = addPermission;
                lnkAdd.Visible = addPermission;
            }
            else
            {
                imgAddRole.Visible = addPermission;
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
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        FillRoleGrid();
        txSearchRole.Text = "";
        ShowTabs(1);
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        FillRoleGrid();
        txSearchRole.Text = "";

    }
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;

        FillRoleGrid();
       
    }

  protected void FillRoleGrid()
    {
        eobj = new ERole();
        ds = new DataSet();
        ds = new DataSet();
        try
        {
            if (txSearchRole.Text != "")
            {
                eobj.ROLENAME = txSearchRole.Text;
            }
            else
            {
                eobj.ROLENAME = "%";

            }

            ds = new DataSet();
            ds = eobj.EroleGridview();
            if (ds.Tables[0].Rows.Count > 0)
            {
                txSearchRole.Text = string.Empty;
                panleGridRoles.Visible = true;
                grdRole.Visible = true;
                grdRole.DataSource = null;
                grdRole.DataSource = ds;
                grdRole.DataBind();
                grdRole.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
            else
            {
                // lbldiv.Visible = true;
                //lblStatus.CssClass = "ErrorMsg";
                dvFailure.Visible = true;
                lblStatus.Text = "No records found";
                panleGridRoles.Visible = false;

            }
        }
        catch (Exception ex)
        {
            //lblStatus.CssClass = "ErrorMsg";
            lblStatus.Text = ex.Message;
        }
 
    }



    void ShowTabs(int TabNum)
    {
        if (TabNum == 1)
        {    
            panelAddRole.Visible = false;
            panelSearchRoles.Visible = true;
            grdRole.Visible = false;
            lnkAdd.Visible = true;
            lnkAdd.Text = "Add";
            txSearchRole.Text = string.Empty;
            FillRoleGrid();
            dvFooter.Visible = false;
        }
        else if (TabNum == 2)
        {
            panelAddRole.Visible = true; 
            panelSearchRoles.Visible = false;
            grdRole.Visible = false;
            panleGridRoles.Visible = false;
            lnkAdd.Visible = false;
            lnkAdd.Text = "Add";
            dvFooter.Visible = true;
        }
    }


  
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        lblSuccess.Text = "";
        dvFailure.Visible = false;
        dvSuccess.Visible = false; 
        lnkAdd.Text = "Add";
        txtRole.Text = string.Empty;
        ShowTabs(2);
        showButtons("Add");
    }
    protected void imgClear_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        lblSuccess.Text = "";
        dvFailure.Visible = false;
        dvSuccess.Visible = false; 
        txtRole.Text = string.Empty;
        lblStatus.Text = string.Empty;
    }
    protected void imgAddRole_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        lblSuccess.Text = "";
        dvFailure.Visible = false;
        dvSuccess.Visible = false; 
        eobj   = new ERole();
        ds = new DataSet();

        try
        {
            eobj.LoginSessionId = Session["LOGINID"].ToString();
            eobj.ROLENAME = txtRole.Text.Trim();

            ds = eobj.EAddRoles();

            if (ds.Tables[0].Rows[0]["Status"].ToString() == "Success")
            {
                ShowTabs(1);
                dvSuccess.Visible = true;
                lblSuccess.Text = " Role Details saved successfully";
                //lblStatus.CssClass = "SuccessMsg";
                txtRole.Text = string.Empty;
            }
            else
            {
                ShowTabs(2);
                dvFailure.Visible = false;
                lblStatus.Text = "Details already exists";
               // lblStatus.CssClass = "ErrorMsg";
                txtRole.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
            lblStatus.CssClass = "ErrorMsg";
        }

    }


    void showButtons(string Mode)
    {
        if (string.Compare(Mode, "Search", true) == 0)
        {
            imgClear.Visible = true;
            dvclear.Visible = true;
            imgAddRole.Visible = false;
            dvisave.Visible = false;
            imgUpdateRole.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = false;
            btncan.Visible = false;
        }
        else if (string.Compare(Mode, "View", true) == 0)
        {
            imgClear.Visible = false;
            dvclear.Visible = false;
            imgAddRole.Visible = false;
            dvisave.Visible = false;
            imgUpdateRole.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = true;
            btncan.Visible = true;
        }
        else if (string.Compare(Mode, "Edit", true) == 0)
        {
            imgClear.Visible = true;
            dvclear.Visible = true;
            imgAddRole.Visible = false;
            dvisave.Visible = false;
            imgUpdateRole.Visible = true;
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
            imgAddRole.Visible = true;
            dvisave.Visible = true;
            imgUpdateRole.Visible = false;
            dvupdate.Visible = false;
        }

    }

    protected void grdRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblStatus.Text = "";
        lblSuccess.Text = "";
        dvFailure.Visible = false;
        dvSuccess.Visible = false; 
        lblStatus.Text = "";
        eobj = new ERole();
        ds = new DataSet();

        try
        {
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int roleid = Convert.ToInt32(grdRole.DataKeys[index].Value.ToString());
                Session["RoleID1"] = roleid;
                eobj.RoleID = roleid.ToString();
                ds = eobj.EviewEditContacts();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRole.Text = ds.Tables[0].Rows[0]["ROLENAME"].ToString();

                    txtRole.Enabled = false;

                    ShowTabs(2);

                }
         
               //lnkAdd.Text = "View";
               showButtons("View");
            }
            else if (e.CommandName == "Edits")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int roleid = Convert.ToInt32(grdRole.DataKeys[index].Value.ToString());
                Session["RoleID1"] = roleid;
                eobj.RoleID = roleid.ToString();
                ds = new DataSet();
                ds = eobj.EviewEditContacts();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRole.Text = ds.Tables[0].Rows[0]["ROLENAME"].ToString();

                    txtRole.Enabled = true;

                    ShowTabs(2);

                    panleGridRoles.Visible = false;
                    panelAddRole.Visible = true;

                }

                //lnkAdd.Text = "Edit";
                showButtons("Edit");
            }

            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int roleid = Convert.ToInt32(grdRole.DataKeys[index].Value.ToString());
                Session["RoleID1"] = roleid;
                eobj.RoleID = roleid.ToString();
                eobj.LoginSessionId = Session["LOGINID"].ToString(); ;
                ds = eobj.EDelete();

                if (ds.Tables[0].Rows[0]["Status"].ToString() == "Success")
                {
                    ShowTabs(1);
                    //lblStatus.CssClass = "SuccessMsg";
                    lblSuccess.Text = "Delete Transaction Successful";
                    dvSuccess.Visible = true;
                }
                else if (ds.Tables[0].Rows[0]["Status"].ToString() == "Cannot Be Deleted")
                {
                   // lblStatus.CssClass = "ErrorMsg";
                    dvFailure.Visible = true;
                    lblStatus.Text = "Cannot Be Deleted as User Asscociated With This Role";

                }
                else
                {

                    //lblStatus.CssClass = "ErrorMsg";
                    dvFailure.Visible = true;
                    lblStatus.Text = "Delete Transaction UnSuccessful";
                }
                txtRole.Text = string.Empty;

            }

        }
        catch (Exception ex)
        {
            lblStatus.CssClass = "ErrorMsg";
            lblStatus.Text = ex.Message;
        }

    }
    protected void imgUpdateRole_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        lblSuccess.Text = "";
        dvFailure.Visible = false;
        dvSuccess.Visible = false; 
        eobj = new ERole();
        ds = new DataSet();

        try
        {
            eobj.LoginSessionId = Session["LOGINID"].ToString();
            eobj.ROLENAME = txtRole.Text.Trim();
            eobj.RoleID = Session["RoleID1"].ToString(); 


            ds = eobj.EUpdateroles();

            if (ds.Tables[0].Rows[0]["Status"].ToString() == "Success")
            {
                ShowTabs(1);
                dvSuccess.Visible = true;
                lblSuccess.Text = " Role Details Updated successfully";
                //lblStatus.CssClass = "SuccessMsg";
                txtRole.Text = string.Empty;
            }
            else
            {
                ShowTabs(2);
                dvFailure.Visible = true;
                lblStatus.Text = "Details already exists";
                //lblStatus.CssClass = "ErrorMsg";
                txtRole.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
            lblStatus.CssClass = "ErrorMsg";
        }


    }
    protected void grdRole_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }


    protected void grdRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void grdRole_RowCreated(object sender, GridViewRowEventArgs e)
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
   
}
