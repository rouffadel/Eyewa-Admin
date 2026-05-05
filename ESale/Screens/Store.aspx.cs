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
using System.IO;
using ESaleEntity;
public partial class Screens_Store : System.Web.UI.Page
{
    EStore store;
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
        int fileCount = Directory.GetFiles(Server.MapPath("~/images/SlideImages/")).Length-1;
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
                FillDDLOrganisation();
                lblStatus.Text = string.Empty;
                lblSuccess.Text = string.Empty;
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
                ShowTabs(1);
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }
    }
    public void FillDDLOrganisation()
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            store = new EStore();
            ds = new DataSet();
            ds = store.OrganizationDDL();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLOrganisation.DataSource = ds.Tables[0];
                DDLOrganisation.DataValueField = "OrganisationID";
                DDLOrganisation.DataTextField = "OrganisationName";
                DDLOrganisation.DataBind();
                DDLOrganisation.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                DDLOrganisation.Items.Insert(0, new ListItem("--Any--", "0"));
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
                //btncan.Visible = EditPermission;
                //imgClear.Visible = EditPermission;

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
            FillGrid();
        }
        else if (TabNum == 2)
        {
            pnlAdd.Visible = true;
            pnlSearch.Visible = false;
            pnlSearchGrid.Visible = false;
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            dvFooter.Visible = true;
            lnkAdd.Visible = false;
            lnkAdd.Text = "Add";
        }
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        txtStoreNameSearch.Text = "";
        txtContactNumberSearch.Text = "";
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
        txtStoreNameSearch.Text = "";
        txtContactNumberSearch.Text = "";
        txtEmailIDSearch.Text = "";
        FillGrid();
    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ShowTabs(2);
        ClearControls();
        showButtons("Add");
        imgStore.Visible = false;
        lblStatus.Text = "";
        lblSuccess.Text = "";
        dvFailure.Visible = false;
        dvSuccess.Visible = false; 
        fuStore.Visible = true;
        lnkAdd.Text = "Add";
        imgSave.Enabled = true;
        EnableControls(true);
    }
    void ClearControls()
    {
        imgSave.Enabled = true;
        txtStoreName .Text = string.Empty;
          txtAddress.Text = string.Empty;
         txtCity.Text = string.Empty;
         txtZipCode.Text = string.Empty;
        txtEmailID.Text = string.Empty;
        // ddlRole.SelectedIndex = 0;
        txtContactNo.Text = string.Empty;
        txtContactPerson.Text = string.Empty;
        DDLOrganisation.SelectedValue = "0";
        txtStoreCode.Text = "";
        EnableControls(true);

    }
    void EnableControls(bool status)
    {
        txtStoreName.Enabled = status;
        txtAddress.Enabled = status;
        txtCity.Enabled = status;
        txtZipCode.Enabled = status;
        txtEmailID.Enabled = status;
        DDLOrganisation.Enabled = status;
        txtContactPerson.Enabled = status;
        txtContactNo.Enabled = status;
        txtStoreCode.Enabled = status;

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

    protected void imgSave_Click(object sender, EventArgs e)
    {
        imgSave.Enabled = false;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        txtStoreNameSearch.Text = "";
        txtContactNumberSearch.Text = "";
        txtEmailIDSearch.Text = "";
        try
        {
            store = new EStore();
            int loginid = Convert.ToInt32(Session["LOGINID"]);
            if (DDLOrganisation.SelectedValue != "0")
                store.OrganisationID = Convert.ToInt32(DDLOrganisation.SelectedValue);
            else
                store.OrganisationID = 0;
            if (txtStoreName.Text != "")
                store.StoreName = txtStoreName.Text;
            else
                store.StoreName = "";
            if (txtAddress.Text != "")
                store.Address = txtAddress.Text;
            else
                store.Address = "";
            if (txtCity.Text != "")
                store.City = txtCity.Text;
            else
                store.City = "";
            if (txtZipCode.Text != "")
                store.ZipCode = txtZipCode.Text;
            else
                store.ZipCode = "";
            if (txtContactNo.Text != "")
                store.ContactNo = txtContactNo.Text;
            else
                store.ContactNo = "";
            if (txtContactPerson.Text != "")
                store.ContactPerson = txtContactPerson.Text;
            else
                store.ContactPerson = "";
            if (txtEmailID.Text != "")
                store.EmailID = txtEmailID.Text;
            else
                store.EmailID = "";
            store.LoginID = loginid;
            if (fuStore.HasFile)
            {
                store.Filename = Path.GetExtension(fuStore.FileName);
            }
            else
                store.Filename = string.Empty;
            if (txtStoreCode.Text != "")
                store.StoreCode = txtStoreCode.Text;
            else
                store.StoreCode="";
            ds = new DataSet();
            ds = store.Save();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0]["Status"]) == "Success")
                {
                    string file = Convert.ToString(ds.Tables[0].Rows[0]["FileName"]);
                    if (fuStore.HasFile)
                    {
                        if (File.Exists(Server.MapPath("~/images/StoreImages/" + file)))
                        {
                            File.Delete(Server.MapPath("~/images/StoreImages/" + file));
                        }
                        fuStore.SaveAs(Server.MapPath("~/images/StoreImages/" + file));
                    }
                    ShowTabs(1);
                    lblSuccess.Text = "Record Inserted successfully.";
                    dvSuccess.Visible = true;
                    imgStore.Visible = true;
                    imgSave.Enabled = false;
                    FillGrid();
                  
                }
                else
                {
                    dvFailure.Visible = true;
                    lblStatus.Text = "Record Already Exists";
                    //lblStatus.Visible = true;
                }
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
        txtStoreNameSearch.Text = "";
        txtContactNumberSearch.Text = "";
        txtEmailIDSearch.Text = "";
        try
        {
            store = new EStore();
            int loginid = Convert.ToInt32(Session["LOGINID"]);
            int storeid = Convert.ToInt32(HdStoreID.Value);
            store.StoreID = storeid;
            if (DDLOrganisation.SelectedValue != "0")
                store.OrganisationID = Convert.ToInt32(DDLOrganisation.SelectedValue);
            else
                store.OrganisationID = 0;
            if (txtStoreName.Text != "")
                store.StoreName = txtStoreName.Text;
            else
                store.StoreName = "";
            if (txtAddress.Text != "")
                store.Address = txtAddress.Text;
            else
                store.Address = "";
            if (txtCity.Text != "")
                store.City = txtCity.Text;
            else
                store.City = "";
            if (txtZipCode.Text != "")
                store.ZipCode = txtZipCode.Text;
            else
                store.ZipCode = "";
            if (txtContactNo.Text != "")
                store.ContactNo = txtContactNo.Text;
            else
                store.ContactNo = "";
            if (txtContactPerson.Text != "")
                store.ContactPerson = txtContactPerson.Text;
            else
                store.ContactPerson = "";
            if (txtEmailID.Text != "")
                store.EmailID = txtEmailID.Text;
            else
                store.EmailID = "";
            store.LoginID = loginid;
            if (fuStore.HasFile)
            {
                store.Filename = Path.GetExtension(fuStore.FileName);
            }
            else
                store.Filename = string.Empty;
            if (txtStoreCode.Text != "")
                store.StoreCode = txtStoreCode.Text;
            else
                store.StoreCode = "";
             ds = store.Update();
             if (ds.Tables[0].Rows.Count > 0)
             {
                 if (Convert.ToString(ds.Tables[0].Rows[0]["Status"]) == "Success")
                 {
                     string file = Convert.ToString(ds.Tables[0].Rows[0]["FileName"]);
                     if (fuStore.HasFile)
                     {
                         if (File.Exists(Server.MapPath("~/images/StoreImages/" + file)))
                         {
                             File.Delete(Server.MapPath("~/images/StoreImages/" + file));
                         }
                         fuStore.SaveAs(Server.MapPath("~/images/StoreImages/" + file));
                     }
                     ShowTabs(1);
                   
                     dvSuccess.Visible = true;
                     FillGrid();
                     lblSuccess.Text = "Record updated successfully.";
                 }
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
           
            store = new EStore();
            if (txtStoreNameSearch.Text != "")
                store.StoreName = txtStoreNameSearch.Text;
            else
                store.StoreName = string.Empty;
            if (txtContactNumberSearch.Text != "")
                store.ContactNo = txtContactNumberSearch.Text;
            else
                store.ContactNo = string.Empty;
            if (txtEmailIDSearch.Text != "")
                store.EmailID = txtEmailIDSearch.Text;
            else
                store.EmailID = string.Empty;
            store.LoginID = Convert.ToInt32(Session["LOGINID"]);
            ds = store.GetStoreGrid();
           // txtStoreNameSearch.Text = "";
           // txtContactNumberSearch.Text = "";
           // txtEmailIDSearch.Text = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvStore.DataSource = ds.Tables[0];
                gvStore.DataBind();
                pnlSearchGrid.Visible = true;
                gvStore.Visible = true;
                gvStore.HeaderRow.TableSection = TableRowSection.TableHeader;
                lblStatus.Text = "";

            }
            else
            {
                dvFailure.Visible = true;
                gvStore.DataSource = null;
                gvStore.DataBind();
                lblStatus.Text = "No Records Found.";
                //lblStatus.Visible = true;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    protected void gvStore_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        ds = new DataSet();
        store = new EStore();
        if (e.CommandName == "View")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int storeid =Convert.ToInt32( gvStore.DataKeys[rowindex].Value.ToString());
            store.StoreID = storeid;
            ds = store.GetStoreForViewEdit();
            if(ds.Tables[0].Rows.Count>0)
            {
                txtStoreName.Text = Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]);
                txtAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                txtCity.Text = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
                txtContactNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactNumber"]);
                txtContactPerson.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactPerson"]);
                txtEmailID.Text = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                txtZipCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["ZipCode"]);
                txtStoreCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["StoreCode"]);
                DDLOrganisation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationID"]);
                string filename=Convert.ToString(ds.Tables[0].Rows[0]["FileName"]);
                if (filename != "")
                {
                    imgStore.ImageUrl = "~/images/StoreImages/" + filename;
                    imgStore.Visible = true;
                    fuStore.Visible = false;
                }
                else
                {
                    imgStore.Visible = false;
                    fuStore.Visible = false;
                }
            }
            ShowTabs(2);
            showButtons("View");
            lblStatus.Text = "";
           // lnkAdd.Text = "View";
        }
        else if (e.CommandName == "EditRow")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int storeid = Convert.ToInt32(gvStore.DataKeys[rowindex].Value.ToString());
            store.StoreID = storeid;
            HdStoreID.Value = storeid.ToString();
            ds = store.GetStoreForViewEdit();
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtStoreName.Text = Convert.ToString(ds.Tables[0].Rows[0]["StoreName"]);
                txtAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                txtCity.Text = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
                txtContactNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactNumber"]);
                txtContactPerson.Text = Convert.ToString(ds.Tables[0].Rows[0]["ContactPerson"]);
                txtEmailID.Text = Convert.ToString(ds.Tables[0].Rows[0]["EmailID"]);
                txtZipCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["ZipCode"]);
                txtStoreCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["StoreCode"]);
                DDLOrganisation.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OrganisationID"]);
                string filename = Convert.ToString(ds.Tables[0].Rows[0]["FileName"]);
                if (filename != "")
                {
                    imgStore.ImageUrl = "~/images/StoreImages/" + filename;
                    imgStore.Visible = true;
                    fuStore.Visible = true;
                }
                else
                {
                    imgStore.Visible = false;
                    fuStore.Visible = true;
                }
               
            }
            ShowTabs(2);
            EnableControls(true);
            //imgupdate.Visible = true;
            //imgSave.Visible = false;
            //imgClear.Visible = true;
            lblStatus.Text = "";
            //lnkAdd.Text = "Edit";
            showButtons("Edit");
           
        }
        else if (e.CommandName == "Deleting")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int storeid = Convert.ToInt32(gvStore.DataKeys[rowindex].Value.ToString());
            store.StoreID = storeid;
            string result = store.Delete();
            if (result == "Success")
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
                dvFailure.Visible = true;
                lblStatus.Text = result;
            }
           
        }

    }
   
   
    protected void gvStore_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvStore_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvStore_RowCreated(object sender, GridViewRowEventArgs e)
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
