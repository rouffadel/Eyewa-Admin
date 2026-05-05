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

public partial class Admin_Users : System.Web.UI.Page
{
    EUsers eobj;
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

            if (!IsPostBack)
            {
                ViewState["LoginID"] = Session["LOGINID"].ToString();
                loginid = Session["LOGINID"].ToString();     
                FillDdlRole();
                FillStoreDetails();
                //FillGridUser();
                lblStatus.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
                ShowTabs(1);
                lblStatus.Text = "";
                FillGridUser();
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
            if (ds.Tables[0].Rows.Count > 0)
            {
                addPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["ADD"].ToString());
                viewPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["VIEW"].ToString());
                deletepermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["DELETE"].ToString());
                EditPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["EDIT"].ToString());

            }

            if (EditPermission == true)
            {
                imgupdate.Visible = EditPermission;
                imgClear.Visible = EditPermission;
                btncan.Visible = EditPermission;
            }
            else
            {
                imgupdate.Visible = EditPermission;
                // imgClearBrand.Visible = EditPermission;

            }
            if (addPermission == true)
            {
                imgSave.Visible = addPermission;
                lnkAdd.Visible = addPermission;
                btncan.Visible = addPermission;
                lirole.Visible = addPermission;
                lnkAdd.Visible = addPermission;
            }
            else
            {
                imgSave.Visible = addPermission;
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


    void FillGridUser()
    {
        try
        {
            ds = new DataSet();
            eobj = new EUsers();

            if (txtSearchUserName.Text != "")
            {
                eobj.UserName = txtSearchUserName.Text;
            }
            else
            {
                eobj.UserName = "";
            }

            if (txtSearchLoginName.Text != "")
            {
                eobj.LoginName = txtSearchLoginName.Text;
            }
            else
            {
                eobj.LoginName = "";
            }
            if (ddlsearchrole.SelectedIndex != 0)
            {
                eobj.RoleID =Convert.ToInt32( ddlsearchrole.SelectedValue);
            }
            else
            {
                eobj.RoleID = 0 ;

            }
            if (drdnSearchStore.SelectedIndex != 0)
            {
                eobj.StoreID =Convert.ToInt32(drdnSearchStore.SelectedValue);
            }
            else
            {
                eobj.StoreID = 0;
            }

            ds = eobj.GetGridData();

            if (ds.Tables[0].Rows.Count > 0)
            {      
                grdRole.DataSource = ds;
                grdRole.DataBind();
                grdRole.HeaderRow.TableSection = TableRowSection.TableHeader;
                txtSearchLoginName.Text = "";
                txtSearchUserName.Text = "";
                ddlsearchrole.SelectedIndex = 0;
            }
            else
            {
                panelgrid.Visible = false;
                grdRole.Visible = false;
                dvFailure.Visible = true;
                lblStatus.Text = "No Records Found";
            }

        }
        catch (Exception ex)
        {
            lblStatus.Text= ex.Message;
        }


    }

    void FillDdlRole()
    {
        try
        {
            ds = new DataSet();
            eobj = new EUsers();

           ds= eobj.FillRoleName();

           if (ds.Tables[0].Rows.Count > 0)
           {
               ddlRole.DataSource = ds;
               ddlRole.DataTextField = "RoleName";
               ddlRole.DataValueField = "RoleId";
               ddlRole.DataBind();
               ddlRole.Items.Insert(0, new ListItem("--Select--", "0"));

               ddlsearchrole.DataSource = ds;
               ddlsearchrole.DataTextField = "RoleName";
               ddlsearchrole.DataValueField = "RoleId";
               ddlsearchrole.DataBind();
               ddlsearchrole.Items.Insert(0, new ListItem("--Any--", "0"));
           }
   
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }
    }



    void ShowTabs(int TabNum)
    {
        if (TabNum == 1)
        {
            panelAddUser.Visible = false;
            panelSearchUser.Visible = true;
            panelgrid.Visible = true;
            grdRole.Visible = true;
            lnkAdd.Visible = true;
            showButtons("Search");
            FillGridUser();
        }
        else if (TabNum == 2)
        {
            panelAddUser.Visible = true;
            panelSearchUser.Visible = false;
            panelgrid.Visible = false;           
            lnkAdd.Text = "Add";
            lnkAdd.Visible = false;

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
            ds = new DataSet();
            eobj = new EUsers();
            eobj.UserName = txtAddUser.Text;
            eobj.LoginName = txtAddLogin.Text;
            eobj.Password = txtPassword.Text;

            if (radioStoreUser.Checked)
            {
                //eobj.StoreOrOrganisation = radioStoreUser.Text;
                eobj.StoreID =Convert.ToInt32(drdnStoreName.SelectedValue);

            }
            else 
            {
                //eobj.StoreOrOrganisation = radioOrganisation.Text;
                eobj.StoreID = 0;
            }
            eobj.LoginID =Convert.ToInt32(Session["LOGINID"]);
            eobj.RoleID =Convert.ToInt32(ddlRole.SelectedValue);
            eobj.Email = txtAddEmail.Text;
            eobj.StoreID =Convert.ToInt32(drdnStoreName.SelectedValue);
            eobj.MobileNo = txtAddMobile.Text;
            eobj.ContactNo = txtAddContact.Text;
            eobj.Active = (checkActive.Checked) ? "1" : "0";
            eobj.LoginSessionID = Convert.ToInt32(Session["LOGINID"]);
            ds = eobj.ESaveUsers();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Status"].ToString().Trim() == "Success")
                {
                    
                    ShowTabs(1);
                    FillGridUser();
                    dvSuccess.Visible = true;
                    lblSuccess.Text = "User Details Inserted Successfully";
                    
                }
                else
                {
                    dvFailure.Visible = true;
                   lblStatus.Text =" UserName Already Exists";

                }
            }
        }
        catch (Exception ex)
        {
           // lblStatus.CssClass = "ErrorMsg";
            //lblStatus.Text = ex.Message;
        }

    }
    protected void imgClear_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        txtAddUser.Text = string.Empty;
        txtAddEmail.Text = string.Empty;
        txtAddLogin.Text = string.Empty;
        txtAddMobile.Text = string.Empty;
        txtPassword.Text = string.Empty;
        ddlRole.SelectedValue ="0";
        drdnStoreName.SelectedValue ="0";
        txtAddContact.Text = string.Empty;
        checkActive.Checked = false;
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ShowTabs(2);
        ClearControls();
        showButtons("Add");
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
    }

    void ClearControls()
    {
        txtAddUser.Text = string.Empty;
        txtAddEmail.Text = string.Empty;
        txtAddLogin.Text = string.Empty;
        txtAddMobile.Text = string.Empty;
        txtPassword.Text = string.Empty;
        ddlRole.SelectedValue ="0";
        txtAddContact.Text = string.Empty;
        drdnSearchStore.SelectedValue ="0";
        drdnStoreName.SelectedValue ="0";
        checkActive.Checked = false;
        radioStoreUser.Checked = false;
        radioOrganisation.Checked = false;
        EnableControls(true);

    }


    protected void imgupdate_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            ds = new DataSet();
            eobj = new EUsers();
            eobj.UserName = txtAddUser.Text;
            eobj.LoginName = txtAddLogin.Text;
            if (radioStoreUser.Checked)
            {
                //eobj.StoreOrOrganisation = radioStoreUser.Text;
                eobj.StoreID =Convert.ToInt32( drdnStoreName.SelectedValue);

            }
            else
            {
                //eobj.StoreOrOrganisation = radioOrganisation.Text;
                eobj.StoreID = 0;
            }
            eobj.Password = txtPassword.Text;
            eobj.RoleID =Convert.ToInt32(ddlRole.SelectedValue);
            eobj.Email = txtAddEmail.Text;
            eobj.MobileNo = txtAddMobile.Text;
            eobj.ContactNo = txtAddContact.Text;            
            eobj.Active = (checkActive.Checked) ? "1" : "0";
            eobj.LoginID = Convert.ToInt32( Session["LoginId"].ToString());
            eobj.LoginSessionID = Convert.ToInt32(Session["LOGINID"]);
            ds = eobj.EUpdateUsers();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Status"].ToString() == "Success")
                {
                    dvSuccess.Visible = true;
                    lblSuccess.Text = "User Details Updated Successfully";
                    FillGridUser();
                    ShowTabs(1);
                }
                else
                {
                    dvFailure.Visible = false;
                    lblStatus.Text = " UserName Already Exists";

                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.CssClass = "ErrorMsg";
            lblStatus.Text = ex.Message;

        }

    }

    void showButtons(string Mode)
    {
        if (string.Compare(Mode, "Search", true) == 0)
        {
            imgClear.Visible = false;
            imgSave.Visible = false;
            imgupdate.Visible = false;
            dvclear.Visible = true;
            dvisave.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = false;
            btncan.Visible = false;
        }
        else if (string.Compare(Mode, "View", true) == 0)
        {
            imgClear.Visible = false;
            imgSave.Visible = false;
            imgupdate.Visible = false;
            dvclear.Visible = false;
            dvisave.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = true;
            btncan.Visible = true;
        }
        else if (string.Compare(Mode, "Edit", true) == 0)
        {
            imgClear.Visible = true;
            imgSave.Visible = false;
            imgupdate.Visible = true;
            dvclear.Visible = true;
            dvisave.Visible = false;
            dvupdate.Visible = true;
            dvcancel.Visible = true;
            btncan.Visible = true;
        }
        else if (string.Compare(Mode, "Add", true) == 0)
        {
            dvcancel.Visible = true;
            dvclear.Visible = true;
            btncan.Visible = true;
            dvisave.Visible = true;
            dvupdate.Visible = false;
            imgClear.Visible = true;
            imgSave.Visible = true;
            imgupdate.Visible = false;
        }

    }

    public void FillStoreDetails()
    {
        ds = new DataSet();
        eobj = new EUsers();
        try
        {
            ds = eobj.EFillStoreDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                drdnStoreName.DataSource = ds;
                drdnStoreName.DataTextField = "StoreName";
                drdnStoreName.DataValueField = "StoreID";
                drdnStoreName.DataBind();
                drdnStoreName.Items.Insert(0, new ListItem("--Select--", "0"));

                drdnSearchStore.DataSource = ds;
                drdnSearchStore.DataValueField = "StoreID";
                drdnSearchStore.DataTextField = "StoreName";
                drdnSearchStore.DataBind();
                drdnSearchStore.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                drdnSearchStore.Items.Insert(0, new ListItem("--Any--", "0"));
                drdnStoreName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch ( Exception ex)
        {

            lblStatus.Text = ex.Message; 
        }
    }

    protected void grdRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        eobj = new EUsers();
        ds = new DataSet();

        try
        {
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int loginid = Convert.ToInt32(grdRole.DataKeys[index].Value.ToString());
                Session["LoginId"] = loginid;
                eobj.LoginID = Convert.ToInt32( loginid);
                ds = eobj.EviewEditContacts();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtAddUser.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                    txtAddLogin.Text = ds.Tables[0].Rows[0]["LoginName"].ToString();
                    txtAddMobile.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    if (ds.Tables[0].Rows[0]["StoreID"].ToString().Trim() != "0")
                    {
                        radioStoreUser.Checked = true;
                        drdnStoreName.SelectedValue = ds.Tables[0].Rows[0]["StoreID"].ToString();                       
                        drdnStoreName.Visible = true;
                        drdnStoreName.SelectedValue = ds.Tables[0].Rows[0]["StoreID"].ToString();
                        lblStoreName.Visible = true;
                    }
                    else 
                    {
                        radioOrganisation.Checked = true;
                        drdnStoreName.Visible = false;
                        lblStoreName.Visible = false;                        
                    }
                    txtAddEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                    txtAddContact.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    ddlRole.SelectedValue = ds.Tables[0].Rows[0]["RoleID"].ToString();
                    checkActive.Checked = (ds.Tables[0].Rows[0]["Active"].ToString() == "True") ? true : false;
                    EnableControls(false);
                    ShowTabs(2);
                    panelgrid.Visible = false;
                    panelAddUser.Visible = true;
                  //  lnkAdd.Text = "View";

                }
                //if (addPermission == false)
                //{
                //    lnkSearch.CssClass = "ActiveClass";
                //    lnkSearch.Text = "View";
                //}
                //else
                //{
                //    lnkAdd.CssClass = "ActiveClass";
                //    lnkAdd.Text = "View";
                //}
                

                showButtons("View");
                //lnkAdd.Text = "View";
            }
            else if (e.CommandName == "Edits")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int loginid = Convert.ToInt32(grdRole.DataKeys[index].Value.ToString());
                Session["LoginId"] = loginid;
                eobj.LoginID =Convert.ToInt32( loginid);
                ds = new DataSet();
                ds = eobj.EviewEditContacts();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtAddUser.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                    txtAddLogin.Text = ds.Tables[0].Rows[0]["LoginName"].ToString();
                    txtAddMobile.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    if (ds.Tables[0].Rows[0]["StoreID"].ToString().Trim() != "0")
                    {
                        radioStoreUser.Checked = true;
                        drdnStoreName.SelectedValue = ds.Tables[0].Rows[0]["StoreID"].ToString();
                        drdnStoreName.Visible = true;
                        drdnStoreName.SelectedValue = ds.Tables[0].Rows[0]["StoreID"].ToString();
                        lblStoreName.Visible = true;
                    }
                    else
                    {
                        radioOrganisation.Checked = true;
                        drdnStoreName.Visible = false;
                       lblStoreName.Visible = false;
                    }

                    txtAddEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                    txtAddContact.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    ddlRole.SelectedValue = ds.Tables[0].Rows[0]["RoleID"].ToString();
                    checkActive.Checked = (ds.Tables[0].Rows[0]["Active"].ToString() == "True") ? true : false;
                    EnableControls(true);
                    ShowTabs(2);
                    panelgrid.Visible = false;
                    panelAddUser.Visible = true;


                }

                //if (addPermission == false)
                //{
                //    lnkSearch.CssClass = "ActiveClass";
                //    lnkSearch.Text = "Edit";
                //}
                //else
                //{
                //    lnkAdd.Text = "Edit";
                //    lnkAdd.CssClass = "ActiveClass";
                //}

                
                showButtons("Edit");
               // lnkAdd.Text = "Edit";
            }

            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int loginid = Convert.ToInt32(grdRole.DataKeys[index].Value.ToString());
                Session["LoginId"] = loginid;
                eobj.LoginID =Convert.ToInt32( loginid);
                eobj.LoginSessionID = Convert.ToInt32(Session["LOGINID"].ToString());
                ds = eobj.DeleteUser();

                if (ds.Tables[0].Rows[0]["Status"].ToString() == "Success")
                {
                    ShowTabs(1);
                    //lblStatus.CssClass = "SuccessMsg";
                    dvSuccess.Visible = true;
                    lblSuccess.Text = "Delete Transaction Successful";
                    //lblStatus.Visible = true;
                    FillGridUser();
                }
                else if (ds.Tables[0].Rows[0]["Status"].ToString() == "Cannot Be Deleted as User Asscociated with the RoleID")
                {
                    dvFailure.Visible = true;
                    //lblStatus.CssClass = "ErrorMsg";
                   lblStatus.Text = "Cannot Be Deleted as User Asscociated with the RoleID";

                }
                else
                {
                    dvFailure.Visible = true;
                    //lblStatus.CssClass = "ErrorMsg";
                    lblStatus.Text = "Delete Transaction UnSuccessful";
                }
            }

        }
        catch (Exception ex)
        {
            lblStatus.CssClass = "ErrorMsg";
            lblStatus.Text = ex.Message;
        }

    }


    void EnableControls(bool status)
    {
            txtAddUser.Enabled = status;
            txtAddLogin.Enabled = status;
            txtAddMobile.Enabled = status;
            txtPassword.Enabled = status;
            txtAddEmail.Enabled = status;
            ddlRole.Enabled = status;
            txtAddContact.Enabled =status;
            checkActive.Enabled = status;
            drdnStoreName.Enabled = status;
            radioOrganisation.Enabled = status;
            radioStoreUser.Enabled = status;
    
    }
    protected void searchuser_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        FillGridUser();
        lblStatus.Text = "";
        txtSearchLoginName.Text = string.Empty;
        txtSearchUserName.Text = string.Empty;
        ddlsearchrole.SelectedValue = "0";
        drdnSearchStore.SelectedValue = "0";
        panelgrid.Visible = true;
        grdRole.Visible = true;
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
    protected void drdnStoreName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void radioStoreUser_CheckedChanged(object sender, EventArgs e)
    {
        lblStoreName.Visible = true;
        drdnStoreName.Visible = true;
    }
    protected void radioOrganisation_CheckedChanged(object sender, EventArgs e)
    {
        lblStoreName.Visible = false;
        drdnStoreName.Visible = false;

    }
    protected void txtSearchUserName_TextChanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
    }
    protected void drdnSearchStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
    }
    protected void ddlsearchrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        ddlsearchrole.SelectedIndex = 0;
        txtSearchLoginName.Text = "";
        txtSearchLoginName.Text = "";
        ddlsearchrole.SelectedIndex = 0;
        ShowTabs(1);
        FillGridUser();
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlsearchrole.SelectedIndex = 0;
        txtSearchLoginName.Text = "";
        txtSearchLoginName.Text = "";
        ddlsearchrole.SelectedIndex = 0;
       // txtSearchBrand.Text = "";
       // FillGridView();
        FillGridUser();
    }
    protected void txtSearchLoginName_TextChanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
    }
    protected void grdRole_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //DropDownList lblstore;
        //Label lblLoginName;
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    lblstore = (DropDownList)e.Row.FindControl("drdnStoreName");
        //    lblLoginName = (Label)e.Row.FindControl("lblLoginName");
        //    if (lblLoginName.Text == "admin")
        //    {
        //        lblstore.Text = string.Empty;
        //    }
        //}
    }
}
