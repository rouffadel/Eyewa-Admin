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
using System.Globalization;
using ESaleEntity;

public partial class Screens_ProcessSalary : System.Web.UI.Page
{
    EProcessSalary objEProcessSalary;
    ECheckPermission ECPobj;
    DataSet dsforSalary;
    string LoginId;
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
                LoginId = Session["LOGINID"].ToString();
                FillMothandYears();
                FillSearchEmployees();   
                ShowTabs(1);
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
                lblSuccess.Text = "";
                lblStatus.Text = "";
                //lblStatus.Text = "";
               
            }
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }
    }
    //Search the Process Salary Data.

    // Display the Add GridView Control.
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ShowTabs(2);
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        
        try
        {
            divPaymentDetails.Attributes["style"] = "display:none;";
            divPaySlipPopup.Attributes["style"] = "display:none;";
            //pnlLineItems.Visible = false;
            GetGridDataForManualLineItems();
            EnableControls(true);
            ClearControls();
            
            btnProcess.Visible = false;
            rbtnSystemAttendance.Checked = true;
            rbtnManualAttendance.Checked = false;
        }
        catch (Exception ex)
        {
            
            lblStatus.Text=ex.Message;
        }

    }
    protected void gvSalaraySheet_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvSalaraySheet_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        objEProcessSalary = new EProcessSalary();
        dsforSalary = new DataSet();
        btnProcess.Visible = false;
        gvLineItems.DataSource = null;
        gvLineItems.DataBind();

        try
        {
            if (e.CommandName == "View")
            {
             
                int index = Convert.ToInt32(e.CommandArgument);
                int nSalaryID = Convert.ToInt32(gvSalaraySheet.DataKeys[index].Value.ToString());
                Session["nSalaryID"] = nSalaryID;
                objEProcessSalary.WhereCondition = "and S.SalaryID=" + nSalaryID;
                objEProcessSalary.LoginId = Convert.ToString(Session["LoginId"]);
                dsforSalary = objEProcessSalary.EViewEditProcessSalary();

                if (dsforSalary.Tables[0].Rows.Count > 0)
                {

                    string MonthName= dsforSalary.Tables[0].Rows[0]["Month"].ToString();
                    
                    ddlYear.SelectedValue = dsforSalary.Tables[0].Rows[0]["Year"].ToString();
                    txtNoOfWorkingDays.Text = dsforSalary.Tables[0].Rows[0]["NoOfWorkingDays"].ToString();
                    int MonthNum = Convert.ToDateTime("01-" + (MonthName) + "-" + ddlYear.SelectedItem.Value).Month;
                    ddlMonth.SelectedValue = MonthNum.ToString();
                    pnlLineItems.Visible = true;
                    gvLineItems.Visible = true;
                    gvLineItems.DataSource = dsforSalary;
                    gvLineItems.DataBind(); 
                    pnlprint.Visible = false;
                    EnableControls(false);
                    ShowTabs(2);
                    pnlManualLineItems.Visible = false;
                    gvLineItems.Enabled = false;

                }
                if (Session["LOGINID"].ToString() != "1")
                {
                    if (addPermission == false)
                    {
                       // lnkSearch.CssClass = "ActiveClass";
                       // lnkSearch.Text = "View";
                    }
                    else
                    {
                       // lnkAdd.CssClass = "ActiveClass";
                        //lnkAdd.Text = "View";
                    }
                }
                else
                {
                    //lnkAdd.Text = "View";
                }
               //showButtons("View");
               // lnkAdd.Text = "View";
            }
           /* else if (e.CommandName == "Edit")
            {


                int index = Convert.ToInt32(e.CommandArgument);
                int nSalaryID = Convert.ToInt32(gvSalaraySheet.DataKeys[index].Value.ToString());
                Session["nSalaryID"] = nSalaryID;
                objEProcessSalary.WhereCondition = "and S.SalaryID=" + nSalaryID;
                dsforProcessSalary = objEProcessSalary.EViewEditProcessSalary();

                if (dsforProcessSalary.Tables[0].Rows.Count > 0)
                {

                    ddlMonth.SelectedValue = dsforProcessSalary.Tables[0].Rows[0]["Month"].ToString();
                    ddlYear.SelectedValue = dsforProcessSalary.Tables[0].Rows[0]["Year"].ToString();
                    txtNoOfWorkingDays.Text = dsforProcessSalary.Tables[0].Rows[0]["NoOfWorkingDays"].ToString(); 

                    pnlLineItems.Visible = true;
                    gvLineItems.Visible = true;
                    gvLineItems.DataSource = dsforProcessSalary;
                    gvLineItems.DataBind();
                   

                    EnalbleContols(true);

                    ShowTabs(2);



                }
                else
                {
                    lblStatus.CssClass = "ErrorMsg";
                    lblStatus.Text = "No Records are available";
                    ShowTabs(2);
                }
                if (Session["UserID"].ToString() != "1")
                {
                    if (addPermission == false)
                    {
                        lnkSearch.CssClass = "ActiveClass";
                        lnkSearch.Text = "Edit";
                    }
                    else
                    {
                        lnkAdd.Text = "Edit";
                        lnkAdd.CssClass = "ActiveClass";
                    }
                }
                else
                {
                    lnkAdd.Text = "Edit";
                }
                showButtons("Edit");
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int nSalaryID = Convert.ToInt32(gvSalaraySheet.DataKeys[index].Value.ToString());
                Session["nSalaryID"] = nSalaryID;
                objEProcessSalary.SalaryID = Convert.ToInt32(nSalaryID).ToString();
                objEProcessSalary.LoginId = LoginId;
                dsforProcessSalary = objEProcessSalary.EDeleteProcessSalary();

                if (dsforProcessSalary.Tables[0].Rows[0]["Status"].ToString() == "Success")
                {
                    lblStatus.CssClass = "SuccessMsg";
                    lblStatus.Text = "Delete Transaction Successful";

                    ShowTabs(1);
                }
                else
                {

                    lblStatus.CssClass = "ErrorMsg";
                    lblStatus.Text = "Delete Transaction UnSuccessful";
                }


            }*/



            else if (e.CommandName == "Print")
            {
                pnlprint.Visible = true;
                decimal NoOfDaysToPaid = 0;
                int index = Convert.ToInt32(e.CommandArgument);
                int nSalaryID = Convert.ToInt32(gvSalaraySheet.DataKeys[index].Value.ToString());
                Session["nSalaryID"] = nSalaryID;
                objEProcessSalary.WhereCondition = "and SP.SalaryID=" + nSalaryID;
                dsforSalary = objEProcessSalary.EPrintProcessSalary();
                if (dsforSalary.Tables[0].Rows.Count > 0)
                {

                    lblMonthYear.Text = "Pay Slip for the Month of –" + dsforSalary.Tables[0].Rows[0]["MonthYear"].ToString();
                    lblEmployeeNo.Text = dsforSalary.Tables[0].Rows[0]["EmployeeNo"].ToString();
                    lblDaetOfJoining.Text = dsforSalary.Tables[0].Rows[0]["DateOfJoin"].ToString();
                    lblEmployeeName.Text = dsforSalary.Tables[0].Rows[0]["EmployeeName"].ToString();
                    lblPFNo.Text = "12345";
                    lblDesignation.Text = dsforSalary.Tables[0].Rows[0]["DesignationName"].ToString();
                    lblLOPDays.Text = dsforSalary.Tables[0].Rows[0]["NoOfLOPDays"].ToString();
                    lblLocation.Text = "Hyderabad";
                    NoOfDaysToPaid = Convert.ToDecimal(dsforSalary.Tables[0].Rows[0]["NoOfDaysPresent"]) + Convert.ToDecimal(dsforSalary.Tables[0].Rows[0]["NoOfLeavesAvailed"]);
                    lblNoOfDaysPaid.Text = NoOfDaysToPaid.ToString();

                    lblOtherAdditions.Text = Convert.ToString(dsforSalary.Tables[0].Rows[0]["OtherAdditions"]);
                    lblOtherDeductions.Text = Convert.ToString(dsforSalary.Tables[0].Rows[0]["otherDeduction"]);


                    lblBasic.Text = dsforSalary.Tables[0].Rows[0]["Basic"].ToString();
                    lblHRA.Text = dsforSalary.Tables[0].Rows[0]["HRA"].ToString();
                    lblTA.Text = dsforSalary.Tables[0].Rows[0]["TA"].ToString();
                    lblDA.Text = dsforSalary.Tables[0].Rows[0]["DA"].ToString();
                    lblNoOfWorkingDaysforPrint.Text = dsforSalary.Tables[0].Rows[0]["NoOfWorkingDays"].ToString();
                    lblNoOfDaysPresent.Text = dsforSalary.Tables[0].Rows[0]["NoOfDaysPresent"].ToString();
                    lblNoOfDaysAbsent.Text = dsforSalary.Tables[0].Rows[0]["NoOfDaysAbsent"].ToString();
                    lblNoOfLeavesAvailed.Text = dsforSalary.Tables[0].Rows[0]["NoOfLeavesAvailed"].ToString();
                    lblTDS.Text = dsforSalary.Tables[0].Rows[0]["TDS"].ToString();
                    lblDeductions.Text = dsforSalary.Tables[0].Rows[0]["Deductions"].ToString();
                    lblNetSalary.Text = dsforSalary.Tables[0].Rows[0]["NetSalary"].ToString();

                    lblincentive.Text = dsforSalary.Tables[0].Rows[0]["Incentive"].ToString() == "0" ? "0.00" : dsforSalary.Tables[0].Rows[0]["Incentive"].ToString();

                    lblAddress.Text = dsforSalary.Tables[1].Rows[0]["OrganisationName"].ToString() + "," + dsforSalary.Tables[1].Rows[0]["Address"].ToString() + "," + dsforSalary.Tables[1].Rows[0]["City"].ToString() + "," + dsforSalary.Tables[1].Rows[0]["ZipCode"].ToString() + "," + dsforSalary.Tables[1].Rows[0]["ContactNumber"].ToString();


                     divPaymentDetails.Attributes["style"] = "display:block;";
                     divPaySlipPopup.Attributes["style"] = "display:block;";


                    ScriptManager.RegisterStartupScript(this, typeof(Page), "mykey1", "PaymentDetailsPrint()", true);

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "mykey1", "window.print()", true); 
                    //ScriptManager.RegisterStartupScript(this, typeof(Page),"Get", "PaymentDetailsPrint()", true);
                    //System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "PaymentDetailsPrint();", true); 
                }
                else
                {
                    //lblStatus.CssClass = "ErrorMsg";
                    dvFailure.Visible = true;
                    lblStatus.Text = "Error In Binding";
                    divPaymentDetails.Attributes["style"] = "display:none;";
                    divPaySlipPopup.Attributes["style"] = "display:none;";

                }


            }

        }
        catch (Exception ex)
        {
            lblStatus.CssClass = "ErrorMsg";
            lblStatus.Text = ex.Message;
        }

    }
    protected void gvSalaraySheet_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (viewPermission == true && EditPermission == true && deletepermission == true)
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    //LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    //LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = true;
                    //ForTdEdit.Visible = true;
                    //ForTdDelete.Visible = true;
                }
            }
            else if (viewPermission == true && EditPermission == true && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    // LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    //LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = true;
                    // ForTdEdit.Visible = true;
                    // ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == true && EditPermission == false && deletepermission == true)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    // LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    // LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = true;
                    // ForTdEdit.Visible = false;
                    // ForTdDelete.Visible = true;
                }
            }
            else if (viewPermission == true && EditPermission == false && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    //LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    //LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = true;
                    // ForTdEdit.Visible = false;
                    // ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == false && EditPermission == true && deletepermission == true)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    //LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    //LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = false;
                    //ForTdEdit.Visible = true;
                    //ForTdDelete.Visible = true;
                }
            }
            else if (viewPermission == false && EditPermission == true && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    // LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    // LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = false;
                    // ForTdEdit.Visible = true;
                    // ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == false && EditPermission == false && deletepermission == true)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    //LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    //LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = false;
                    // ForTdEdit.Visible = false;
                    //ForTdDelete.Visible = true;
                }
            }
            else if (viewPermission == false && EditPermission == false && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    // LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    //LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = false;
                    //ForTdEdit.Visible = false;
                    //ForTdDelete.Visible = false;
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
    protected void gvSalaraySheet_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvSalaraySheet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSalaraySheet.PageIndex = e.NewPageIndex;
        FillProcessSalaryGrid();
    }
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        dsforSalary = new DataSet();
         //object obj=new object();
        dsforSalary = FillProcessSalaryGrid();
        if (dsforSalary.Tables[0].Rows.Count > 0)
        {

            lblStatus.Text = string.Empty;
        }
        pnlprint.Visible = false;
        
        divPaymentDetails.Attributes["style"] = "display:none;";
        divPaySlipPopup.Attributes["style"] = "display:none;";
        ddlSearchEmploye.SelectedValue = "0";
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



            //if (EditPermission == true)
            //{
            //    imgUpdate.Visible = EditPermission;
            //    imgClear.Visible = EditPermission;
                
            //}
            //else
            //{
            //    imgUpdate.Visible = EditPermission;
            //    imgClear.Visible = EditPermission;

            //}
            //if (addPermission == true)
            //{
            //    imgAdd.Visible = addPermission;
            //    lnkAdd.Visible = addPermission;
            //}
            //else
            //{
            //    imgAdd.Visible = addPermission;
            //    lnkAdd.Visible = addPermission;
            //}


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
    protected void imgAdd_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        objEProcessSalary = new EProcessSalary();
        dsforSalary = new DataSet();
       
        try
        {
            objEProcessSalary.Month = ddlMonth.SelectedItem.ToString();
            objEProcessSalary.Year = ddlYear.SelectedItem.ToString();
            objEProcessSalary.NoOfWorkingDays = Convert.ToDecimal(txtNoOfWorkingDays.Text);
            objEProcessSalary.GridData = GridData();
            objEProcessSalary.LoginId = LoginId;
            dsforSalary = objEProcessSalary.EAddProcessSalary();
            if (dsforSalary.Tables[0].Rows[0]["Status"].ToString() == "Success")
            {
                
                ShowTabs(1);
                ClearControls();
            }
            else
            {
                //lblStatus.CssClass = "ErrorMsg";
                dvFailure.Visible = true;
                lblStatus.Text = "Salary Already Paid";
            }
        }
        catch (Exception ex)
        {

            lblStatus.Text = ex.Message;
        }
    }
    protected void imgUpdate_Click(object sender,EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        objEProcessSalary = new EProcessSalary();
        dsforSalary = new DataSet();

        lblStatus.Text = "";
        lblStatus.CssClass = "";

        objEProcessSalary.SalaryID = Session["nSalaryID"].ToString();
        objEProcessSalary.Month = ddlMonth.SelectedItem.ToString();
        objEProcessSalary.Year = ddlYear.SelectedItem.ToString();
        objEProcessSalary.GridData = GridData();
        objEProcessSalary.LoginId = LoginId;
        objEProcessSalary.NoOfWorkingDays = Convert.ToDecimal(txtNoOfWorkingDays.Text);
        dsforSalary = objEProcessSalary.EUpdateProcessSalary();

        if (dsforSalary.Tables[0].Rows[0]["Status"].ToString() == "Success")
        {

           // lblStatus.CssClass = "SuccessMsg";
            dvSuccess.Visible = true;
            lblSuccess.Text = "Salary details Updated successfully";
            ShowTabs(1);
            ClearControls();
        }
        else
        {

            //lblStatus.CssClass = "ErrorMsg";
            dvFailure.Visible = true;
            lblStatus.Text = "Salary Already Paid";
        }

        if (addPermission == false)
        {
            //lnkSearch.Text = "Search";
            pnlsearch.Visible = true;
            pnladd.Visible = false;
        }
    }
    //Clear the Controls Data.
    protected void imgClear_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        string s = ddlMonth.SelectedValue;
        ClearControls();
        ddlSearchEmploye.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
       // pnlManualLineItems.Visible = false;
    }
    protected void rbtnSystemAttendance_Click(object sender, EventArgs e)
    {

    }
    protected void rbtnManualAttendance_Click(object sender, EventArgs e)
    {

    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {

    }
    void ShowTabs(int TabNum)
    {
        try
        {
            if (TabNum == 1)
            {
                dvFooter.Visible = false;
                pnlrep.Visible = true;
                pnladd.Visible = false;
                pnlsearch.Visible = true;
                pnlManualLineItems.Visible = false;
                gvManualLineItems.Visible = false;
                pnlprint.Visible = false;
                lnkAdd.Visible = true;

                showButtons("Search");
                FillProcessSalaryGrid();

            }
            else if (TabNum == 2)
            {
                pnlrep.Visible = false;
                pnlsearch.Visible = false;
                pnladd.Visible = true;                
                lnkAdd.Text = "Add";
                lnkAdd.Visible = false;
                dvFooter.Visible = true;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    void showButtons(string Mode)
    {
        if (string.Compare(Mode, "Search", true) == 0)
        {
            //imgCancel.Visible = true;
            imgClear.Visible = true;
            imgAdd.Visible = false;
            imgUpdate.Visible = false;
            dvclear.Visible = true;
            dvisave.Visible = false;
            dvupdate.Visible = false;
        }
        else if (string.Compare(Mode, "View", true) == 0)
        {
            dvcancel.Visible = true;
            imgCancel.Visible = true;
            imgClear.Visible = true;
            imgAdd.Visible = false;
            imgUpdate.Visible = false;
            dvclear.Visible = true;
            dvisave.Visible = false;
            dvupdate.Visible = false;
        }
        else if (string.Compare(Mode, "Edit", true) == 0)
        {
            //imgCancel.Visible = true;
            imgClear.Visible = false;
            imgAdd.Visible = false;
            imgUpdate.Visible = false;
            dvclear.Visible = false;
            dvisave.Visible = false;
            dvupdate.Visible = false;
        }
        else if (string.Compare(Mode, "Add", true) == 0)
        {
            dvcancel.Visible = true;
            imgCancel.Visible = true;
            imgClear.Visible = true;
            imgAdd.Visible = false;
            imgUpdate.Visible = false;
            dvclear.Visible = true;
            dvisave.Visible = false;
            dvupdate.Visible = false;
        }

    }
    //Fill Salary Sheet Gridview.
    private DataSet FillProcessSalaryGrid()
    {
        objEProcessSalary = new EProcessSalary();
        dsforSalary = new DataSet();

        try
        {
            if (ddlSearchEmploye.SelectedIndex != 0)
            {
                objEProcessSalary.WhereCondition += "and S.EmployeeID=" + ddlSearchEmploye.SelectedValue;
            }
            //if (Session["LOGINID"] != null)
            //{
            //    if (Session["LOGINID"].ToString() != "1")
            //    {
            //        objEProcessSalary.LoginId = Session["LOGINID"].ToString();
            //        objEProcessSalary.WhereCondition += "and L.LoginID=" + Session["LOGINID"].ToString();
            //    }
            //}
            objEProcessSalary.LoginId = Convert.ToString(Session["LOGINID"]);
            dsforSalary = objEProcessSalary.EViewEditProcessSalary();
            if (dsforSalary.Tables[0].Rows.Count > 0)
            {
                dsforSalary.Tables[0].Columns.Add("SNo", typeof(string));
                int i = 1;
                foreach (DataRow dr in dsforSalary.Tables[0].Rows)
                {
                    dr["SNo"] = i;
                    i++;
                    if (dr["OtherAdditions"] == System.DBNull.Value)
                        dr["OtherAdditions"] = Convert.ToString(0.00);
                    if (dr["OtherDeduction"] == System.DBNull.Value)
                        dr["OtherDeduction"] = Convert.ToString(0.00);

                }
                pnlrep.Visible = true;
                gvSalaraySheet.Visible = true;
                gvSalaraySheet.DataSource = null;
                gvSalaraySheet.DataSource = dsforSalary;
                gvSalaraySheet.DataBind();
                gvSalaraySheet.HeaderRow.TableSection = TableRowSection.TableHeader;
                //ddlSearchEmploye.SelectedIndex = 0;
            }
            else
            {

                //lblStatus.CssClass = "ErrorMsg";
                dvFailure.Visible = true;
                lblStatus.Text = "No records found";
                pnlrep.Visible = false;

                //ddlSearchEmploye.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblStatus.CssClass = "ErrorMsg";
            //lblStatus.Text = ex.Message;
        }
        return dsforSalary;
    }
    protected void imgCancel_Click(object sender, ImageClickEventArgs e)
    {
        ClearControls();
        ShowTabs(1);
    }
    
    protected string GridData()
    {
        string GridData = "";
        try
        {
            foreach (GridView row in gvLineItems.Rows)
            {
                GridData += ((Label)row.FindControl("lblEmployeeID")).Text + "~";
                GridData += ((Label)row.FindControl("lblEmployeeSalaryID")).Text + "~";
                GridData += ((TextBox)row.FindControl("txtMonth")).Text + "~";
                GridData += ((TextBox)row.FindControl("txtYear")).Text + "~";
                GridData += Convert.ToDecimal(((TextBox)row.FindControl("txtGrossSalary")).Text) + "~";
                GridData += Convert.ToDecimal(((TextBox)row.FindControl("txtBasicSalary")).Text) + "~";
                GridData += Convert.ToDecimal(((TextBox)row.FindControl("txtHRA")).Text)+ "~";
                GridData += Convert.ToDecimal(((TextBox)row.FindControl("txtTA")).Text) + "~";
                GridData += Convert.ToDecimal(((TextBox)row.FindControl("txtDA")).Text )+ "~";
                GridData += Convert.ToDecimal(((TextBox)row.FindControl("txtTDS")).Text) + "~";
                GridData += Convert.ToDecimal(((TextBox)row.FindControl("txtDeducation")).Text) + "~";
                GridData += Convert.ToDecimal(((TextBox)row.FindControl("txtIncentives")).Text) + "~";
                GridData += Convert.ToDecimal(((TextBox)row.FindControl("txtNetSalary")).Text) + "~";
                GridData += "$";
                GridData = GridData.Substring(0, GridData.Length - 1);
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }
        return GridData;
    }
    //Used to Clear the Controls Data.
    protected void ClearControls()
    {
        ddlMonth.SelectedIndex=0;
        ddlYear.SelectedIndex = 0;
        txtNoOfWorkingDays.Text = "";
        pnlLineItems.Visible = false;
        gvLineItems.DataSource = null;
        gvLineItems.DataBind();

    }
    protected void GetGridDataForManualLineItems()
    {
        objEProcessSalary = new EProcessSalary();
        dsforSalary = new DataSet();
        try
        {
            objEProcessSalary.LoginId = (Session["LOGINID"].ToString());
            dsforSalary = objEProcessSalary.EGetGridDataForManualLineItems();
            if (dsforSalary.Tables[0].Rows.Count > 0)
            {
                dsforSalary.Tables[0].Columns.Add("Sno");
                int i = 1;
                foreach (DataRow  dr in dsforSalary.Tables[0].Rows)
                {
                    dr["Sno"] = i.ToString();
                    i++;
                }
                gvManualLineItems.DataSource = dsforSalary.Tables[0];
                gvManualLineItems.DataBind();
                pnlManualLineItems.Visible = true;
                gvManualLineItems.Visible = true;
            }
            else
            {
                gvManualLineItems.DataSource = null;
                gvManualLineItems.DataBind();
                gvManualLineItems.Visible = false;
            }
        }
        catch (Exception ex)
        {

            lblStatus.Text = ex.Message ;
        }
    }
    //Implement Sataus of the Controls.
    protected void EnableControls(bool status)
    {
        ddlMonth.Enabled = status;
        ddlYear.Enabled = status;
        txtNoOfWorkingDays.Enabled = status;
    }
    // Save the Process Salay Information into the Database.
    protected void btnMProcessSalary_Click(object sender, EventArgs e)
    {


        try
        {
            objEProcessSalary = new EProcessSalary();
            dsforSalary = new DataSet();

            //int MonthNum = Convert.ToDateTime("01-" + (ddlMonth.SelectedItem.Text) + "-" + ddlYear.SelectedItem.Value).Month;
            //int NumofDaysinMonth = System.DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedItem.Value), MonthNum);
           
             
                objEProcessSalary.Month = ddlMonth.SelectedItem.Text;
                objEProcessSalary.Year = ddlYear.SelectedValue;
                objEProcessSalary.NoOfWorkingDays = Convert.ToDecimal(txtNoOfWorkingDays.Text);
                string griddata = string.Empty;
                foreach (GridViewRow gr in gvManualLineItems.Rows)
                {
                    Label lblempid = (Label)gr.FindControl("lblMEmployeeID");
                    TextBox txtlop = (TextBox)gr.FindControl("txtLOPDays");
                    TextBox txtdeduct = (TextBox)gr.FindControl("txtDeduction");
                    if (txtdeduct.Text == "")
                        txtdeduct.Text = "0.00";
                    TextBox txtincentive = (TextBox)gr.FindControl("txtincentive");
                    if (txtincentive.Text == "")
                        txtincentive.Text = "0.00";

                    TextBox txtotheraddition = (TextBox)gr.FindControl("txtOtherAddition");
                    if (txtotheraddition.Text == "")
                        txtotheraddition.Text = "0.00";
                    TextBox txtotherdeduction = (TextBox)gr.FindControl("txtOtherDeduction");
                    if (txtotherdeduction.Text == "")
                        txtotherdeduction.Text = "0.00";

                    TextBox txttds = (TextBox)gr.FindControl("txttds");
                    if (txttds.Text == "")
                        txttds.Text = "0.00";
                    TextBox txtnetsal = (TextBox)gr.FindControl("txtNetSalary");
                    if (txtnetsal.Text == "")
                        txtnetsal.Text = "0.00";
                    if (txtlop.Text != "")
                        griddata += lblempid.Text + "~" + txtlop.Text + "~" + txtdeduct.Text + "~" + txtincentive.Text + "~" + txtotheraddition.Text + "~" + txtotherdeduction.Text + "~" + txttds.Text + "~" + txtnetsal.Text + "$";
                }
                if (griddata.Length != 0)
                {
                    griddata = griddata.Substring(0, griddata.Length - 1);
                }
                else
                {
                    dvFailure.Visible = true;
                    lblStatus.Text = "No data inserted.";
                    //lblStatus.CssClass = "ErrorMsg";
                    return;
                }
                objEProcessSalary.GridData = griddata;
                objEProcessSalary.LoginId = Convert.ToString(Session["LOGINID"]);
                string result = objEProcessSalary.SaveManualSalary();
                if (result == "Success")
                {
                    dvSuccess.Visible = true;
                    lblSuccess.Text = "Payable Information Saved Successfully.";
                    //lblStatus.CssClass = "SuccessMsg";
                    ShowTabs(1);
                }
                else
                {
                    dvFailure.Visible = true;
                    lblStatus.Text = result;
                    //lblStatus.CssClass = "ErrorMsg";
                    ShowTabs(2);
                    pnlLineItems.Visible = true;
                    GetGridDataForManualLineItems();

                }
            }
        
        catch (Exception ex)
        {
            throw ex;
        }
     
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divPaymentDetails.Attributes["style"] = "display:none;";
        divPaySlipPopup.Attributes["style"] = "display:none;";
    }
    //Bind the Month and Year DropDown list Contorls.
    private void FillMothandYears()
    {
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddlMonth.Items.Add(new ListItem(monthName, month.ToString()));
            //ddlMonth.Items.Add(new ListItem(monthName, month));
           
        }
        int index = 1;
        for (int Year = 2014; Year <= DateTime.Now.Year + 2; Year++)
        {
            ListItem li = new ListItem(Year.ToString(), Year.ToString());
            ddlYear.Items.Insert(index, li);
            index++;
        }
        CultureInfo ci = new CultureInfo("en-US");
        string currentmonth = DateTime.Now.ToString("MMMM", ci);

        string currentyear = DateTime.Now.Year.ToString();
        ddlMonth.SelectedValue = currentmonth;
        ddlYear.SelectedValue = currentyear;
    }
    //Search Employee Name and Fill into Employee Dropdown List.
    public void FillSearchEmployees()
    {
        try
        {
            objEProcessSalary = new EProcessSalary();
            DataSet dsforClients = new DataSet();

            //if (Session["LOGINID"] != null)
            //{
            //    if (Session["LOGINID"].ToString() != "1")
            //    {
            //        objEProcessSalary.LoginId = Session["LOGINID"].ToString();
            //        //objEProcessSalary.WhereCondition += "and U.UserID=" + Session["LOGINID"].ToString();
            //    }
            //}
            objEProcessSalary.LoginId = Session["LOGINID"].ToString();
            dsforClients = objEProcessSalary.EFillEmployee();

            if (dsforClients.Tables[0].Rows.Count > 0)
            {
                ddlSearchEmploye.DataSource = dsforClients;
                ddlSearchEmploye.DataTextField = "EmployeeName";
                ddlSearchEmploye.DataValueField = "EmployeeID";
                ddlSearchEmploye.DataBind();
               
                ddlSearchEmploye.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            else
            {
                ddlSearchEmploye.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            lblStatus.CssClass = "ErrorMsg";
            lblStatus.Text = ex.Message;
        }
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlSearchEmploye.SelectedIndex = 0;
        if (gvSalaraySheet.HeaderRow != null)
            gvSalaraySheet.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void imgCancel_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        ddlSearchEmploye.SelectedIndex = 0;
        ShowTabs(1);
        
    }
}
