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
using ESaleEntity.DAL;


public partial class Admin_Employee : System.Web.UI.Page
{
    int num;
    string stnumber;
    EEmployee Eobj;
    DataSet ds;
    string LoginId;
    ECheckPermission ECPobj;
    static bool addPermission = false;
    static bool viewPermission = true;
    static bool EditPermission = true;
    static bool deletepermission = true;
    string ScreenUrl = string.Empty;



    protected void Page_Load(object sender, EventArgs e)
    {
      
            lblmsg.Text = "";
            //lblmsg.CssClass = "";


            if (Session["LOGINID"] == null)
            {
                Response.Redirect("~\\Login.aspx");
            }
            else
            {
                ScreenUrl = Request.FilePath;
                ScreenUrl = ScreenUrl.Substring(ScreenUrl.LastIndexOf('/') + 1);

                if (!IsPostBack)
                {

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
                    chkIsActive.Checked = true;
                    chkIsActive.Checked = false;
                    FillEmployeeSearchddl();
                    ShowTabs(1);
                    //getCountryDdl();               
                    fillDdlDesignation();
                    FillStoreName();
                    // Session["number"] = "";
                    lblmsg.Text = string.Empty;
                    lblSuccess.Text = string.Empty;
                    dvFailure.Visible = false;
                    dvSuccess.Visible = false;
                }
                LoginId = Session["LOGINID"].ToString();
            }
        

    }

    protected void FillStoreName()
    {
        try
        {
            Eobj = new EEmployee();
            DataSet dsForDdlStore = new DataSet();
            Eobj.LoginSessionId = Convert.ToInt32(Session["LOGINID"]);
            dsForDdlStore = Eobj.GetStoreName();
            if (dsForDdlStore.Tables[0].Rows.Count > 0)
            {
                ddlStoreName.DataSource = dsForDdlStore.Tables[0];
                ddlStoreName.DataValueField = "StoreID";
                ddlStoreName.DataTextField = "StoreName";
                ddlStoreName.DataBind();
                ddlStoreName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddlStoreName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

   
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        EnableControls();
        EEmployee eobj = new EEmployee();
        DataSet ds = new DataSet();
        ds=eobj.GetEmployeeNo();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtEmpID.Text = ds.Tables[0].Rows[0]["EmployeeNo"].ToString();
        }
        else 
        {
           
        }
        ShowTabs(2);
        showButtons("Add");
        gvNoOrganisationGrid.Visible = false;
        ClearControls();
        clearGrid();
        radioMale.Checked = true;
    }
    protected void imgSave_Click(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        if (Page.IsValid)
        {
            Eobj = new EEmployee();
            ds = new DataSet();
            lblmsg.Text = "";

            Eobj.FirstName = txtFirstName.Text;
            Eobj.MiddleName = txtMiddleName.Text;
            Eobj.LastName = txtLastName.Text;
            Eobj.Gender = radioMale.Checked ? "Male" : "Female";
            if (txtDob.Text != "")
            {
                Eobj.DOB = converttodate(txtDob.Text);
            }
            else
            {
                Eobj.DOB = "";
            }
            if (txtDoj.Text != "")
            {
                Eobj.DateOfJoining = converttodate(txtDoj.Text);
            }
            else
            {

                Eobj.DateOfJoining = "";
            }
            if (radioRelieving.Checked == true)
            {
                Eobj.RelievedandTerminated = "Relieved";
            }
            else if (radioTerminated.Checked == true)
            {
                Eobj.RelievedandTerminated = "Terminated";
            }
            else
            {
                Eobj.RelievedandTerminated = "";
            }

            Eobj.EmployeeNo = txtEmpID.Text;
            Eobj.DesignationTypeId = Convert.ToInt32(ddlEmpDesgn.SelectedValue);
            Eobj.StoreID = Convert.ToInt32(ddlStoreName.SelectedValue);
            Eobj.BankName = txtBnkName.Text;
            Eobj.Branch = txtBranch.Text;
            Eobj.AccountNo = txtAcNo.Text;
            Eobj.NameAsInAccount = txtNameInBranch.Text;
            Eobj.PANNo = txtPanNo.Text;

            Eobj.TempAddressLine1 = txtTempAddressLine1.Text;
            Eobj.TempAddressLine2 = txtTempAddressLine2.Text;

            Eobj.PermanentAddressLine1 = txtPermAddressLine1.Text;
            Eobj.PermanentAddressLine2 = txtPermAddressLine2.Text;

            Eobj.TempCountry = txtTempCountry.Text;
            Eobj.PermanentCountry = txtPermCountry.Text;

            Eobj.TempState = txtTempState.Text;
            Eobj.PermanentState = txtPermState.Text;

            Eobj.TempCity = txtTempCity.Text;
            Eobj.PermanentCity = txtPermCity.Text;

            Eobj.TempPinCode = txtTempZipCode.Text;
            Eobj.PermanentPinCode = txtPermZipCode.Text;

            Eobj.MobileNo = txtMobileNo.Text;
            Eobj.AlternateMobileNo = txtAltMobileNo.Text;

            Eobj.PersonalEmailID = txtPersonalEmail.Text;
            Eobj.WorkEmailID = txtWorkEmail.Text;

            if (txt10thPer.Text != "")
            {
                Eobj.TenthPercentage = Convert.ToDecimal(txt10thPer.Text);
            }
            else
            {

                Eobj.TenthPercentage = 0;
            }
            Eobj.TenthBord = txt10thBoard.Text;
            if (txt12thPer.Text != "")
            {
                Eobj.TwelfthPercentage = Convert.ToDecimal(txt12thPer.Text);
            }
            else
            {

                Eobj.TwelfthPercentage = 0;
            }
            Eobj.TwelfthBord = txt12thBoard.Text;
            Eobj.GraduationCourse = txtAddGradCourse.Text;
            if (txtGradPer.Text != "")
            {
                Eobj.GradutionPercentage = Convert.ToDecimal(txtGradPer.Text);
            }
            else
            {
                Eobj.GradutionPercentage = 0;
            }
            Eobj.GradutionBord = txtGradUniversity.Text;
            Eobj.PostGraduationCourse = txtAddpostGradCourse.Text;
            if (txtPostGradPer.Text != "")
            {
                Eobj.PostGradutionPercentage = Convert.ToDecimal(txtPostGradPer.Text);
            }
            else
            {

                Eobj.PostGradutionPercentage = 0;
            }
            Eobj.PostGradutionBord = txtPostGradUniversity.Text;
            Eobj.Others = txtQulaificationOthers.Text;

            Eobj.NoOfOrganisations = Convert.ToInt32(ddlNoOfOrgWorked.SelectedValue);

            Eobj.ContactName = txtEmergencyContactName.Text;
            Eobj.ContactNo = txtEmergencyContactNo.Text;
            Eobj.RelationShip = txtEmergencyContactRelation.Text;


            if (txtResignDate.Text != "")
            {
                Eobj.ResignationDate = converttodate(txtResignDate.Text);
            }
            else
            {
                Eobj.ResignationDate = "";
            }

            if (txtRelievingDate.Text != "")
            {

                Eobj.RelievingDate = converttodate(txtRelievingDate.Text);
            }
            else
            {
                Eobj.RelievingDate = "";
            }


            Eobj.ReasonsForLeaving = txtReasonForLeaving.Text;

            if (radioRehiredYes.Checked)
            {
                Eobj.CanBeReHired = "Yes";
            }
            else if (radioRehiredNo.Checked)
            {
                Eobj.CanBeReHired = "No";
            }
            else
            {

                Eobj.CanBeReHired = "";
            }


            if (radioExitFormalitiesCmpltdYes.Checked)
            {
                Eobj.ExitFormalitiesCompleted = "Yes";
            }
            else if (radioExitFormalitiesCmpltdNo.Checked)
            {
                Eobj.ExitFormalitiesCompleted = "No";
            }
            else
            {

                Eobj.ExitFormalitiesCompleted = "";
            }

            Eobj.EmployeeActive = chkIsActive.Checked;

            Eobj.LoginSessionId = Convert.ToInt32(Session["LOGINID"].ToString());

            string dscontents = string.Empty;

            if (ddlNoOfOrgWorked.SelectedIndex != 0)
            {

                foreach (GridViewRow gvr in gvNoOrganisationGrid.Rows)
                {

                    if ((((TextBox)gvr.FindControl("txtNameOfOrganisation")).Text) != string.Empty)
                    {

                        string strNameOfOrganisation = ((TextBox)gvr.FindControl("txtNameOfOrganisation")).Text;
                        dscontents += strNameOfOrganisation + "~";
                        string strDesignation = ((TextBox)gvr.FindControl("txtgvDesignation")).Text;
                        dscontents += strDesignation + "~";
                        string strFromDate = ((TextBox)gvr.FindControl("txtgvFromDate")).Text;
                        dscontents += converttodate(strFromDate) + "~";
                        string strToDate = ((TextBox)gvr.FindControl("txtgvToDate")).Text;
                        dscontents += converttodate(strToDate) + "~";

                        dscontents += "$";
                    }

                }
            }
            else
            {

                dscontents = string.Empty;
            }

            if (dscontents != "")
            {
                dscontents = dscontents.Substring(0, dscontents.Length - 1);
            }

            Eobj.GridWorkExperienceDetails = dscontents;

            ds = Eobj.insertEmployee();

            if (ds.Tables[0].Rows[0]["Status"].ToString() == "Success")
            {

                //Session["number"] = "";
                //lblmsg.CssClass = "SuccessMsg";
                dvSuccess.Visible = true;
                lblSuccess.Text = "Employee details saved successfully";
                ShowTabs(1);
            }
            else
            {
                //lblmsg.CssClass = "ErrorMsg";
                dvFailure.Visible = true;
                lblmsg.Text = "Employee already exists";
                ClearControls();
            }
        }

    }
    void fillDdlDesignation()
    {
        try
        {
            Eobj = new EEmployee();
            DataSet dsForDdlDesignation = new DataSet();

            dsForDdlDesignation = Eobj.getDesignation();

            if (dsForDdlDesignation.Tables[0].Rows.Count > 0)
            {

                ddlEmpDesgn.DataSource = dsForDdlDesignation.Tables[0];
                ddlEmpDesgn.DataValueField = "DesignationID";
                ddlEmpDesgn.DataTextField = "DesignationName";
                ddlEmpDesgn.DataBind();
                ddlEmpDesgn.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddlEmpDesgn.Items.Insert(0, new ListItem("--Select--", "0"));
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
            if (ds.Tables[0].Rows.Count > 0)
            {
                addPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["ADD"].ToString());
                viewPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["VIEW"].ToString());
                deletepermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["DELETE"].ToString());
                EditPermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["EDIT"].ToString());
                if (addPermission == true)
                {
                    lnkAdd.Visible = true;
                }
                else
                {
                    lnkAdd.Visible = false;
                }
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

    void FillEmployeeSearchddl()
    {
        try
        {
            Eobj = new EEmployee();
            ds = new DataSet();
            Eobj.LoginSessionId = Convert.ToInt32(Session["LOGINID"]);
            ds = Eobj.getEmployeesForDropDown();

            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlSearchEmployee.DataSource = ds.Tables[0];
                ddlSearchEmployee.DataValueField = "EmployeeID";
                ddlSearchEmployee.DataTextField = "Employee";
                ddlSearchEmployee.DataBind();
                ddlSearchEmployee.Items.Insert(0, new ListItem("--Any--", "0"));
                ddlSearchEmployee.SelectedIndex = 0;
            }
            else
            {
                ddlSearchEmployee.Items.Insert(0, new ListItem("--Any--", "0"));
            }


        }
        catch (Exception ex)
        {

            throw ex;
        }


    }

    void ShowTabs(int TabNum)
    {
        try
        {
            if (TabNum == 1)
            {
                pnlAdd.Visible = false;
                pnlSearch.Visible = true;
                pnlSearchEmployees.Visible = true;
                lnkAdd.Text = "Add";
                lnkAdd.Visible = true;
                showButtons("Search");
                //FillEmployeeSearchddl();
                fillEmployeeSearchGrid();
                dvFooter.Visible = false;

            }
            else if (TabNum == 2)
            {
                pnlSearch.Visible = false;
                pnlSearchEmployees.Visible = false;
                pnlAdd.Visible = true;
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
            btncan.Visible = false;
            imgClear.Visible = true;
            imgSave.Visible = false;
            imgUpdate.Visible = false;
            dvisave.Visible = false;
            dvclear.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = false;
        }
        else if (string.Compare(Mode, "View", true) == 0)
        {
            btncan.Visible = true;
            imgClear.Visible = false;
            imgSave.Visible = false;
            imgUpdate.Visible = false;
            dvisave.Visible = false;
            dvclear.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = true;
        }
        else if (string.Compare(Mode, "Edit", true) == 0)
        {
            btncan.Visible = true;
            imgClear.Visible = false;
            imgSave.Visible = false;
            imgUpdate.Visible = true;
            dvisave.Visible = false;
            dvclear.Visible = false;
            dvupdate.Visible = true;
            dvcancel.Visible = true;
        }
        else if (string.Compare(Mode, "Add", true) == 0)
        {
            imgSave.Visible = true;
            btncan.Visible = true;
            imgClear.Visible = true;
            imgUpdate.Visible = false;
            dvisave.Visible = true;
            dvclear.Visible = true;
            dvupdate.Visible = false;
            dvcancel.Visible = true;
        }

    }


    void ClearControls()
    {
        txtFirstName.Text = string.Empty;
        txtMiddleName.Text = string.Empty;
        txtLastName.Text = string.Empty;
       // radioMale.Checked = false;

        txtDob.Text = string.Empty;
        txtDoj.Text = string.Empty;
        //txtEmpID.Text = string.Empty;
        txtPanNo.Text = string.Empty;


        txtTempAddressLine1.Text = string.Empty;
        txtPermAddressLine1.Text = string.Empty;
        txtTempAddressLine2.Text = string.Empty;
        txtPermAddressLine2.Text = string.Empty;
        txtTempCountry.Text = string.Empty;
        txtPermCountry.Text = string.Empty;
        txtTempState.Text = string.Empty;
        txtPermState.Text = string.Empty;
        txtTempCity.Text = string.Empty;
        txtPermCity.Text = string.Empty;
        txtTempZipCode.Text = string.Empty;
        txtPermZipCode.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtAltMobileNo.Text = string.Empty;

        txtPersonalEmail.Text = string.Empty;
        txtWorkEmail.Text = string.Empty;

        ddlNoOfOrgWorked.SelectedValue = "0";
       
        
       
        txt10thPer.Text = string.Empty;
        txt10thBoard.Text = string.Empty;
        txt12thPer.Text = string.Empty;
        txt12thBoard.Text = string.Empty;
        txtAddGradCourse.Text = string.Empty;
        txtGradPer.Text = string.Empty;
        txtGradUniversity.Text = string.Empty;
        txtAddpostGradCourse.Text = string.Empty;
        txtPostGradPer.Text = string.Empty;
        txtPostGradUniversity.Text = string.Empty;
        txtQulaificationOthers.Text = string.Empty;


        txtEmergencyContactName.Text = string.Empty;
        txtEmergencyContactNo.Text = string.Empty;
        txtEmergencyContactRelation.Text = string.Empty;

        txtRelievingDate.Text = "";
        txtResignDate.Text = "";
        txtReasonForLeaving.Text = "";


        chkIsActive.Checked = false;

        radioRehiredNo.Checked = false;

        radioRehiredYes.Checked = false;

        radioExitFormalitiesCmpltdYes.Checked = false;

        radioExitFormalitiesCmpltdNo.Checked = false;



        ddlEmpDesgn.SelectedIndex = 0;
        ddlStoreName.SelectedIndex = 0;
        txtBnkName.Text = string.Empty;

        txtBranch.Text = string.Empty;
        txtAcNo.Text = string.Empty;

        txtNameInBranch.Text = string.Empty;
        if (radioRelieving.Checked == true)
        {
            radioRelieving.Checked = false;
        }
        if (radioTerminated.Checked == true)
        {
            radioTerminated.Checked = false;
        }

        //ddlSearchEmployee.SelectedIndex = 0;
        //txtsearchEmployeeNo.Text ="";


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


    protected void ddlNoOfOrgWorked_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        {
            if (ddlNoOfOrgWorked.SelectedIndex == 0)
            {
                gvNoOrganisationGrid.DataSource = null;
                gvNoOrganisationGrid.DataBind();
                gvNoOrganisationGrid.Visible = false;
            }
            else
            {
                int noOfRows = Convert.ToInt32(ddlNoOfOrgWorked.SelectedValue);

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                dt.Columns.Add("NameOfOrganisation", typeof(string));
                dt.Columns.Add("Designation", typeof(string));
                dt.Columns.Add("FromDate", typeof(string));
                dt.Columns.Add("ToDate", typeof(string));
                dt.Columns.Add("WorkExperienceID", typeof(string));



                for (int i = 0; i <= gvNoOrganisationGrid.Rows.Count - 1; i++)
                {

                    DataRow dr;
                    dr = dt.NewRow();

                    dr["NameOfOrganisation"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtNameOfOrganisation")).Text;
                    dr["Designation"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtgvDesignation")).Text;
                    dr["FromDate"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtgvFromDate")).Text;
                    dr["ToDate"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtgvToDate")).Text;
                    dr["WorkExperienceID"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtWorkExperienceID")).Text;

                    dt.Rows.Add(dr);

                }

                if (noOfRows > gvNoOrganisationGrid.Rows.Count)
                {
                    for (int i = 0; i < noOfRows - gvNoOrganisationGrid.Rows.Count; i++)
                    {

                        dt.Rows.Add("", "", "", "", 0);

                    }
                }
                if (noOfRows < gvNoOrganisationGrid.Rows.Count)
                {
                    dt.Rows.Clear();

                    for (int i = 0; i <noOfRows; i++)
                    {

                        dt.Rows.Add("","","", 0);
                    }
                }


                DataSet dsForDelete = new DataSet();
                dsForDelete.Tables.Add(dt);
                gvNoOrganisationGrid.DataSource = dsForDelete.Tables[0];
               // gvNoOrganisationGrid.DataBind();

                gvNoOrganisationGrid.Visible = true;
            }

        }
    }

    void fillEmployeeSearchGrid()
    {
      
       EEmployee Eobj = new EEmployee();
       DataSet ds = new DataSet();
        try
        {
            if (ddlSearchEmployee.SelectedIndex != 0)
            {
                Eobj.EmployeeID = ddlSearchEmployee.SelectedValue;
            }
            else
            {
                Eobj.EmployeeID = "0";
            }
            if (txtsearchEmployeeNo.Text != string.Empty)
            {
                Eobj.EmployeeNo = txtsearchEmployeeNo.Text;
            }
            else
            {
                Eobj.EmployeeNo = string.Empty;
            }
            Eobj.Active = chkActive.Checked;
            Eobj.InActive = chkInActive.Checked;
            Eobj.LoginSessionId = Convert.ToInt32(Session["LOGINID"]);
            ds = Eobj.getEmployees();


            if (ds.Tables[0].Rows.Count > 0 && ds.Tables.Count > 0)
            {
                pnlSearchEmployees.Visible = true;
                gvSearchEmployee.Visible = true;
                //gvSearchEmployee.DataSource = null;
                gvSearchEmployee.DataSource = ds;
                gvSearchEmployee.DataBind();
                ddlSearchEmployee.SelectedIndex = 0;
                txtsearchEmployeeNo.Text = "";
                lblmsg.Text = string.Empty;
                gvSearchEmployee.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
               // lblmsg.CssClass = "ErrorMsg";
                dvFailure.Visible = true;
                lblmsg.Text = "No records found";
                pnlSearchEmployees.Visible = false;
                ddlSearchEmployee.SelectedIndex = 0;
                txtsearchEmployeeNo.Text = "";
            }
        }
        catch (Exception ex)
        {
            //lblmsg.CssClass = "ErrorMsg";
            lblmsg.Text = ex.Message;
        }


    }

    void EnableControls()
    {
        txtFirstName.Enabled = true;
        txtMiddleName.Enabled = true;
        txtLastName.Enabled = true;
        radioMale.Enabled = true;
        radioFemale.Enabled = true;

        txtDob.Enabled = true;
        txtDoj.Enabled = true;
        txtEmpID.Enabled = true;
        ddlEmpDesgn.Enabled = true;
        ddlStoreName.Enabled = true;
        txtBnkName.Enabled = true;

        txtBranch.Enabled = true;
        txtAcNo.Enabled = true;

        txtNameInBranch.Enabled = true;

        txtPanNo.Enabled = true;

        txtTempAddressLine1.Enabled = true;
        txtPermAddressLine1.Enabled = true;
        txtTempAddressLine2.Enabled = true;
        txtPermAddressLine2.Enabled = true;
        txtTempCountry.Enabled = true;
        txtPermCountry.Enabled = true;
        txtPermState.Enabled = true;
        txtTempState.Enabled = true;
        txtTempCity.Enabled = true;
        txtPermCity.Enabled = true;
        txtTempZipCode.Enabled = true;
        txtPermZipCode.Enabled = true;
        txtMobileNo.Enabled = true;
        txtAltMobileNo.Enabled = true;

        txtWorkEmail.Enabled = true;
        txtPersonalEmail.Enabled = true;


        txt10thPer.Enabled = true;
        txt10thBoard.Enabled = true;
        txt12thPer.Enabled = true;
        txt12thBoard.Enabled = true;
        txtAddGradCourse.Enabled = true;
        txtGradPer.Enabled = true;
        txtGradUniversity.Enabled = true;
        txtAddpostGradCourse.Enabled = true;
        txtPostGradPer.Enabled = true;
        txtPostGradUniversity.Enabled = true;
        txtQulaificationOthers.Enabled = true;

        ddlNoOfOrgWorked.Enabled = true;

        txtEmergencyContactName.Enabled = true;
        txtEmergencyContactNo.Enabled = true;
        txtEmergencyContactRelation.Enabled = true;

        txtRelievingDate.Enabled = true;
        txtResignDate.Enabled = true;
        txtReasonForLeaving.Enabled = true;

        chkIsActive.Enabled = true;

        radioRehiredNo.Enabled = true;

        radioRehiredYes.Enabled = true;

        radioExitFormalitiesCmpltdYes.Enabled = true;

        radioExitFormalitiesCmpltdNo.Enabled = true;

        radioRelieving.Enabled = true;
        radioTerminated.Enabled = true;

    }

    void DisableControls()
    {
        txtFirstName.Enabled = false;
        txtMiddleName.Enabled = false;
        txtLastName.Enabled = false;
        radioMale.Enabled = false;
        radioFemale.Enabled = false;

        txtDob.Enabled = false;
        txtDoj.Enabled = false;
        txtEmpID.Enabled = true;
        ddlEmpDesgn.Enabled = false;
        ddlStoreName.Enabled = false;
        txtBnkName.Enabled = false;

        txtBranch.Enabled = false;
        txtAcNo.Enabled = false;

        txtNameInBranch.Enabled = false;

        txtPanNo.Enabled = false;

        txtTempAddressLine1.Enabled = false;
        txtPermAddressLine1.Enabled = false;
        txtTempAddressLine2.Enabled = false;
        txtPermAddressLine2.Enabled = false;
        txtTempCountry.Enabled = false;
        txtPermCountry.Enabled = false;
        txtPermState.Enabled = false;
        txtTempState.Enabled = false;
        txtTempCity.Enabled = false;
        txtPermCity.Enabled = false;
        txtTempZipCode.Enabled = false;
        txtPermZipCode.Enabled = false;
        txtMobileNo.Enabled = false;
        txtAltMobileNo.Enabled = false;

        txtWorkEmail.Enabled = false;
        txtPersonalEmail.Enabled = false;


        txt10thPer.Enabled = false;
        txt10thBoard.Enabled = false;
        txt12thPer.Enabled = false;
        txt12thBoard.Enabled = false;
        txtAddGradCourse.Enabled = false;
        txtGradPer.Enabled = false;
        txtGradUniversity.Enabled = false;
        txtAddpostGradCourse.Enabled = false;
        txtPostGradPer.Enabled = false;
        txtPostGradUniversity.Enabled = false;
        txtQulaificationOthers.Enabled = false;

        ddlNoOfOrgWorked.Enabled = false;

        txtEmergencyContactName.Enabled = false;
        txtEmergencyContactNo.Enabled = false;
        txtEmergencyContactRelation.Enabled = false;

        txtRelievingDate.Enabled = false;
        txtResignDate.Enabled = false;
        txtReasonForLeaving.Enabled = false;

        chkIsActive.Enabled = false;

        radioRehiredNo.Enabled = false;

        radioRehiredYes.Enabled = false;

        radioExitFormalitiesCmpltdYes.Enabled = false;

        radioExitFormalitiesCmpltdNo.Enabled = false;

        radioRelieving.Enabled = false;
        radioTerminated.Enabled = false;

    }


    protected void gvSearchEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblmsg.Text = "";
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblmsg.Text = "";
        Eobj = new EEmployee();
        ds = new DataSet();

        try
        {
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int employeeId = Convert.ToInt32(gvSearchEmployee.DataKeys[index].Value.ToString());
                Session["processid"] = employeeId;
                Eobj.EmployeeID = employeeId.ToString();

                ds = Eobj.viewEditEmployee();


                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFirstName.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    txtMiddleName.Text = ds.Tables[0].Rows[0]["MiddleName"].ToString();
                    txtLastName.Text = ds.Tables[0].Rows[0]["LastName"].ToString();

                    if (ds.Tables[0].Rows[0]["Gender"].ToString() == "Male")
                    {
                        radioMale.Checked = true;
                    }
                    else
                    {
                        radioFemale.Checked = true;
                    }
                    if (ds.Tables[0].Rows[0]["RelievedOrTerminated"].ToString() == "Relieved")
                    {
                        radioRelieving.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[0]["RelievedOrTerminated"].ToString() == "Terminated")
                    {
                        radioTerminated.Checked = true;
                    }

                    else
                    {
                        radioRelieving.Checked = false;
                        radioTerminated.Checked = false;
                    }



                    if (Convert.ToString(ds.Tables[0].Rows[0]["DOB"]) != "01-01-1900")
                        txtDob.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                    else
                        txtDob.Text = "";
                    if (Convert.ToString(ds.Tables[0].Rows[0]["DateOfJoining"]) != "01-01-1900")
                        txtDoj.Text = ds.Tables[0].Rows[0]["DateOfJoining"].ToString();
                    else
                        txtDoj.Text = "";
                    //lblEmpId.Visible = true;
                    txtEmpID.Visible = true;
                    txtEmpID.Text = ds.Tables[0].Rows[0]["EmployeeNo"].ToString();
                    // fillDdlDesignation();
                    ddlEmpDesgn.SelectedValue = ds.Tables[0].Rows[0]["DesignationTypeId"].ToString();
                    ddlStoreName.SelectedValue = ds.Tables[0].Rows[0]["StoreID"].ToString();
                    txtBnkName.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                    txtBranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                    txtNameInBranch.Text = ds.Tables[0].Rows[0]["NameAsInAccount"].ToString();
                    txtAcNo.Text = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                    txtPanNo.Text = ds.Tables[0].Rows[0]["PANNo"].ToString();

                    txtTempAddressLine1.Text = ds.Tables[0].Rows[0]["TempAddressLine1"].ToString();
                    txtPermAddressLine1.Text = ds.Tables[0].Rows[0]["PermanentAddressLine1"].ToString();
                    txtTempAddressLine2.Text = ds.Tables[0].Rows[0]["TempAddressLine2"].ToString();
                    txtPermAddressLine2.Text = ds.Tables[0].Rows[0]["PermanentAddressLine2"].ToString();
                    txtTempCountry.Text = ds.Tables[0].Rows[0]["TempCountry"].ToString();
                    txtPermCountry.Text = ds.Tables[0].Rows[0]["PermanentCountry"].ToString();
                    //fillstateDdlP();
                    //fillstateDdlT();
                    txtTempState.Text = ds.Tables[0].Rows[0]["TempState"].ToString();
                    txtPermState.Text = ds.Tables[0].Rows[0]["PermanentState"].ToString();
                    //fillcityDdlP();
                    //fillcityDdlT();
                    txtTempCity.Text = ds.Tables[0].Rows[0]["TempCity"].ToString();
                    txtPermCity.Text = ds.Tables[0].Rows[0]["PermanentCity"].ToString();

                    txtTempZipCode.Text = ds.Tables[0].Rows[0]["TempPinCode"].ToString();
                    txtPermZipCode.Text = ds.Tables[0].Rows[0]["PermanentPinCode"].ToString();
                    txtMobileNo.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    txtAltMobileNo.Text = ds.Tables[0].Rows[0]["AlternateMobileNo"].ToString();
                    txtWorkEmail.Text = ds.Tables[0].Rows[0]["WorkEmailID"].ToString();
                    txtPersonalEmail.Text = ds.Tables[0].Rows[0]["PersonalEmailID"].ToString();


                    txt10thPer.Text = ds.Tables[0].Rows[0]["TenthPercentage"].ToString();
                    txt10thBoard.Text = ds.Tables[0].Rows[0]["TenthBord"].ToString();
                    txt12thPer.Text = ds.Tables[0].Rows[0]["TwelfthPercentage"].ToString();
                    txt12thBoard.Text = ds.Tables[0].Rows[0]["TwelfthBord"].ToString();
                    txtAddGradCourse.Text = ds.Tables[0].Rows[0]["GraduationCourse"].ToString();
                    txtGradPer.Text = ds.Tables[0].Rows[0]["GradutionPercentage"].ToString();
                    txtGradUniversity.Text = ds.Tables[0].Rows[0]["GradutionBord"].ToString();
                    txtAddpostGradCourse.Text = ds.Tables[0].Rows[0]["PostGraduationCourse"].ToString();
                    txtPostGradPer.Text = ds.Tables[0].Rows[0]["PostGradutionPercentage"].ToString();
                    txtPostGradUniversity.Text = ds.Tables[0].Rows[0]["PostGradutionBord"].ToString();
                    txtQulaificationOthers.Text = ds.Tables[0].Rows[0]["Others"].ToString();


                    ddlNoOfOrgWorked.SelectedValue = ds.Tables[0].Rows[0]["NoOfOrganisations"].ToString();


                    txtEmergencyContactName.Text = ds.Tables[0].Rows[0]["ContactName"].ToString();
                    txtEmergencyContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    txtEmergencyContactRelation.Text = ds.Tables[0].Rows[0]["RelationShip"].ToString();

                    txtResignDate.Text = ds.Tables[0].Rows[0]["ResignationDate"].ToString();

                    txtRelievingDate.Text = ds.Tables[0].Rows[0]["RelievingDate"].ToString();

                    txtReasonForLeaving.Text = ds.Tables[0].Rows[0]["ReasonForLeaving"].ToString();

                    chkIsActive.Checked = ds.Tables[0].Rows[0]["IsActive"].ToString() == "True" ? true : false;

                    if (ds.Tables[0].Rows[0]["CanBeReHired"].ToString() == "Yes")
                    {
                        radioRehiredYes.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[0]["CanBeReHired"].ToString() == "No")
                    {
                        radioRehiredNo.Checked = true;

                    }
                    else if (ds.Tables[0].Rows[0]["CanBeReHired"].ToString() == "")
                    {

                    }

                    if (ds.Tables[0].Rows[0]["ExitFormalitiesCompleted"].ToString() == "Yes")
                    {
                        radioExitFormalitiesCmpltdYes.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[0]["ExitFormalitiesCompleted"].ToString() == "No")
                    {
                        radioExitFormalitiesCmpltdNo.Checked = true;

                    }
                    else if (ds.Tables[0].Rows[0]["ExitFormalitiesCompleted"].ToString() == "")
                    {

                    }
                    
                    DisableControls();
                    ShowTabs(2);
                    
                   
                }
                if (Session["LOGINID"].ToString() != "1")
                {
                    if (addPermission == false)
                    {
                        //lnkSearch.CssClass = "ActiveClass";
                        //lnkSearch.Text = "View";
                    }
                    else
                    {
                        //lnkAdd.CssClass = "ActiveClass";
                      //  lnkAdd.Text = "View";
                    }
                }
                else
                {
                    //lnkAdd.Text = "View";
                }

                //if (ds.Tables[1].Rows.Count > 0)
                //{
                //    gvNoOrganisationGrid.Visible = true;
                //    gvNoOrganisationGrid.DataSource = ds.Tables[1];
                //    gvNoOrganisationGrid.DataBind();
                //}

                showButtons("View");

                gvNoOrganisationGrid.Enabled = false;
                lnkAdd.Text = "View";
            }
            else if (e.CommandName == "Edits")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int employeeId = Convert.ToInt32(gvSearchEmployee.DataKeys[index].Value.ToString());
                Session["EmployeeID"] = employeeId;
                Eobj.EmployeeID = employeeId.ToString();

                DataSet dsForEditable = new DataSet();

                ds = Eobj.viewEditEmployee();


                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvNoOrganisationGrid.Visible = true;
                    gvNoOrganisationGrid.DataSource = ds.Tables[1];
                    gvNoOrganisationGrid.DataBind();
                }
                else 
                {
                    gvNoOrganisationGrid.Visible = false;
                }


                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtFirstName.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    txtMiddleName.Text = ds.Tables[0].Rows[0]["MiddleName"].ToString();
                    txtLastName.Text = ds.Tables[0].Rows[0]["LastName"].ToString();

                    if (ds.Tables[0].Rows[0]["Gender"].ToString() == "Male")
                        radioMale.Checked = true;
                    else radioFemale.Checked = true;


                    if (ds.Tables[0].Rows[0]["RelievedOrTerminated"].ToString() == "Relieved")
                    {
                        radioRelieving.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[0]["RelievedOrTerminated"].ToString() == "Terminated")
                    {
                        radioTerminated.Checked = true;
                    }

                    else
                    {
                        radioRelieving.Checked = false;
                        radioTerminated.Checked = false;
                    }



                    if (Convert.ToString(ds.Tables[0].Rows[0]["DOB"]) != "01-01-1900")
                        txtDob.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                    else
                        txtDob.Text = "";
                    if (Convert.ToString(ds.Tables[0].Rows[0]["DateOfJoining"]) != "01-01-1900")
                        txtDoj.Text = ds.Tables[0].Rows[0]["DateOfJoining"].ToString();
                    else
                        txtDoj.Text = "";
                   // lblEmpId.Visible = true;
                    txtEmpID.Visible = true;
                    txtEmpID.Text = ds.Tables[0].Rows[0]["EmployeeNo"].ToString();
                    // fillDdlDesignation();
                    ddlEmpDesgn.SelectedValue = ds.Tables[0].Rows[0]["DesignationTypeId"].ToString();
                    ddlStoreName.SelectedValue = ds.Tables[0].Rows[0]["StoreID"].ToString();
                    txtBnkName.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                    txtBranch.Text = ds.Tables[0].Rows[0]["Branch"].ToString();
                    txtNameInBranch.Text = ds.Tables[0].Rows[0]["NameAsInAccount"].ToString();
                    txtAcNo.Text = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                    txtPanNo.Text = ds.Tables[0].Rows[0]["PANNo"].ToString();

                    txtTempAddressLine1.Text = ds.Tables[0].Rows[0]["TempAddressLine1"].ToString();
                    txtPermAddressLine1.Text = ds.Tables[0].Rows[0]["PermanentAddressLine1"].ToString();
                    txtTempAddressLine2.Text = ds.Tables[0].Rows[0]["TempAddressLine2"].ToString();
                    txtPermAddressLine2.Text = ds.Tables[0].Rows[0]["PermanentAddressLine2"].ToString();
                    txtTempCountry.Text = ds.Tables[0].Rows[0]["TempCountry"].ToString();
                    txtPermCountry.Text = ds.Tables[0].Rows[0]["PermanentCountry"].ToString();
                    //fillstateDdlP();
                    //fillstateDdlT();
                    txtTempState.Text = ds.Tables[0].Rows[0]["TempState"].ToString();
                    txtPermState.Text = ds.Tables[0].Rows[0]["PermanentState"].ToString();
                    //fillcityDdlP();
                    //fillcityDdlT();
                    txtTempCity.Text = ds.Tables[0].Rows[0]["TempCity"].ToString();
                    txtPermCity.Text = ds.Tables[0].Rows[0]["PermanentCity"].ToString();
                    txtTempZipCode.Text = ds.Tables[0].Rows[0]["TempPinCode"].ToString();
                    txtPermZipCode.Text = ds.Tables[0].Rows[0]["PermanentPinCode"].ToString();
                    txtMobileNo.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    txtAltMobileNo.Text = ds.Tables[0].Rows[0]["AlternateMobileNo"].ToString();
                    txtWorkEmail.Text = ds.Tables[0].Rows[0]["WorkEmailID"].ToString();
                    txtPersonalEmail.Text = ds.Tables[0].Rows[0]["PersonalEmailID"].ToString();


                    txt10thPer.Text = ds.Tables[0].Rows[0]["TenthPercentage"].ToString();
                    txt10thBoard.Text = ds.Tables[0].Rows[0]["TenthBord"].ToString();
                    txt12thPer.Text = ds.Tables[0].Rows[0]["TwelfthPercentage"].ToString();
                    txt12thBoard.Text = ds.Tables[0].Rows[0]["TwelfthBord"].ToString();
                    txtAddGradCourse.Text = ds.Tables[0].Rows[0]["GraduationCourse"].ToString();
                    txtGradPer.Text = ds.Tables[0].Rows[0]["GradutionPercentage"].ToString();
                    txtGradUniversity.Text = ds.Tables[0].Rows[0]["GradutionBord"].ToString();
                    txtAddpostGradCourse.Text = ds.Tables[0].Rows[0]["PostGraduationCourse"].ToString();
                    txtPostGradUniversity.Text = ds.Tables[0].Rows[0]["PostGradutionPercentage"].ToString();
                    txtPostGradUniversity.Text = ds.Tables[0].Rows[0]["PostGradutionBord"].ToString();
                    txtQulaificationOthers.Text = ds.Tables[0].Rows[0]["Others"].ToString();


                    ddlNoOfOrgWorked.SelectedValue = ds.Tables[0].Rows[0]["NoOfOrganisations"].ToString();


                    txtEmergencyContactName.Text = ds.Tables[0].Rows[0]["ContactName"].ToString();
                    txtEmergencyContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    txtEmergencyContactRelation.Text = ds.Tables[0].Rows[0]["RelationShip"].ToString();

                    txtResignDate.Text = ds.Tables[0].Rows[0]["ResignationDate"].ToString();

                    txtRelievingDate.Text = ds.Tables[0].Rows[0]["RelievingDate"].ToString();

                    txtReasonForLeaving.Text = ds.Tables[0].Rows[0]["ReasonForLeaving"].ToString();

                    chkIsActive.Checked = ds.Tables[0].Rows[0]["IsActive"].ToString() == "True" ? true : false;

                    if (ds.Tables[0].Rows[0]["CanBeReHired"].ToString() == "Yes")
                    {
                        radioRehiredYes.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[0]["CanBeReHired"].ToString() == "No")
                    {
                        radioRehiredNo.Checked = true;

                    }
                    else if (ds.Tables[0].Rows[0]["CanBeReHired"].ToString() == "")
                    {

                    }

                    if (ds.Tables[0].Rows[0]["ExitFormalitiesCompleted"].ToString() == "Yes")
                    {
                        radioExitFormalitiesCmpltdYes.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[0]["ExitFormalitiesCompleted"].ToString() == "No")
                    {
                        radioExitFormalitiesCmpltdNo.Checked = true;

                    }
                    else if (ds.Tables[0].Rows[0]["ExitFormalitiesCompleted"].ToString() == "")
                    {

                    }

                    gvNoOrganisationGrid.Enabled = true;

                  //  gvNoOrganisationGrid.Visible = true;
                    
                    EnableControls();
                    ShowTabs(2);
                    
                }
                else
                {
                    lblmsg.CssClass = "ErrorMsg";
                    lblmsg.Text = "No Records are available";
                    ShowTabs(2);
                }
                if (Session["LoginId"].ToString() != "1")
                {
                    if (addPermission == false)
                    {
                        //lnkSearch.CssClass = "ActiveClass";
                       // lnkSearch.Text = "Edit";
                    }
                    else
                    {
                       // lnkAdd.Text = "Edit";
                       // lnkAdd.CssClass = "ActiveClass";
                    }
                }
                else
                {
                  //  lnkAdd.Text = "Edit";
                }
                showButtons("Edit");
                //lnkAdd.Text = "Edit";
            }
            else if (e.CommandName == "Deletes")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["RowIndex"] = index;
                int employeeId = Convert.ToInt32(gvSearchEmployee.DataKeys[index].Value.ToString());
                Session["processid"] = employeeId;
                Eobj.EmployeeID = employeeId.ToString();

                Eobj.LoginSessionId = Convert.ToInt32(Session["UserId"]);

                ds = Eobj.deleteEmployee();

                if (ds.Tables[0].Rows[0]["Status"].ToString() == "Success")
                {
                   // lblmsg.CssClass = "SuccessMsg";
                    dvSuccess.Visible=true;
                    lblSuccess.Text = "Delete Transaction Successful";
                    ShowTabs(1);
                    fillEmployeeSearchGrid();
                }
                else
                {
                    dvFailure.Visible=true;
                    //lblmsg.CssClass = "ErrorMsg";
                    lblmsg.Text = "Delete Transaction UnSuccessful";
                }

                ClearControls();
            }

        }
        catch (Exception ex)
        {
            //lblmsg.CssClass = "ErrorMsg";
            lblmsg.Text = ex.Message;
        }
    }

    protected void gvSearchEmployee_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvNoOrganisationGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        lblmsg.Text = "";
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            lblmsg.Text = "";
            Eobj = new EEmployee();
            ds = new DataSet();


            if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                string workExperienceId = ((TextBox)gvNoOrganisationGrid.Rows[index].FindControl("txtWorkExperienceID")).Text;



                Eobj.WorkExperienceId = Convert.ToInt32(workExperienceId);

                if (workExperienceId != "0")
                {
                    ds = Eobj.deleteWorkExperienceDetail();
                }



                DataTable dtRowdelete = new DataTable();

                dtRowdelete.Columns.Add("NameOfOrganisation", typeof(string));
                dtRowdelete.Columns.Add("Designation", typeof(string));
                dtRowdelete.Columns.Add("FromDate", typeof(string));
                dtRowdelete.Columns.Add("ToDate", typeof(string));
                dtRowdelete.Columns.Add("WorkExperienceID", typeof(int));


                for (int i = 0; i <= gvNoOrganisationGrid.Rows.Count - 1; i++)
                {
                    if (i != index)
                    {
                        DataRow dr;
                        dr = dtRowdelete.NewRow();

                        dr["NameOfOrganisation"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtNameOfOrganisation")).Text;
                        dr["Designation"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtgvDesignation")).Text;
                        dr["FromDate"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtgvFromDate")).Text;
                        dr["ToDate"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtgvToDate")).Text;
                        dr["WorkExperienceID"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtWorkExperienceID")).Text;

                        dtRowdelete.Rows.Add(dr);
                    }
                }

                DataSet dsForGrid = new DataSet();
                dsForGrid.Tables.Add(dtRowdelete);
                gvNoOrganisationGrid.DataSource = dsForGrid.Tables[0];
                gvNoOrganisationGrid.DataBind();

                gvNoOrganisationGrid.Visible = true;

                ddlNoOfOrgWorked.SelectedValue = gvNoOrganisationGrid.Rows.Count.ToString();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void gvNoOrganisationGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvSearchEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvSearchEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSearchEmployee.PageIndex = e.NewPageIndex;
        fillEmployeeSearchGrid();
    }
    public void getMaxEMPid()
    {
        Eobj = new EEmployee();
        DataSet ds = new DataSet();
        string strENO = "";
        ds = Eobj.getId();
        if (ds.Tables[0].Rows.Count > 0)
        {
            int Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
            if (Id < 10)
            {
                strENO = "00" + Id;
            }
            else if (Id > 9 && Id < 100)
            {
                strENO = "0" + Id;
            }
            else if (Id > 99 && Id < 1000)
            {
                strENO = Id.ToString();
            }
            txtEmpID.Text = strENO;
        }
        else
        {
            txtEmpID.Text = "";
        }

    }

    protected void gvSearchEmployee_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (viewPermission == true && EditPermission == true && deletepermission == true)
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = true;
                    ForTdEdit.Visible = true;
                    ForTdDelete.Visible = true;
                }
            }
            else if (viewPermission == true && EditPermission == true && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = true;
                    ForTdEdit.Visible = true;
                    ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == true && EditPermission == false && deletepermission == true)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = true;
                    ForTdEdit.Visible = false;
                    ForTdDelete.Visible = true;
                }
            }
            else if (viewPermission == true && EditPermission == false && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = true;
                    ForTdEdit.Visible = false;
                    ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == false && EditPermission == true && deletepermission == true)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = false;
                    ForTdEdit.Visible = true;
                    ForTdDelete.Visible = true;
                }
            }
            else if (viewPermission == false && EditPermission == true && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = false;
                    ForTdEdit.Visible = true;
                    ForTdDelete.Visible = false;
                }
            }
            else if (viewPermission == false && EditPermission == false && deletepermission == true)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
                    ForTdView.Visible = false;
                    ForTdEdit.Visible = false;
                    ForTdDelete.Visible = true;
                }
            }
            else if (viewPermission == false && EditPermission == false && deletepermission == false)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton ForTdView = (LinkButton)e.Row.FindControl("imgBtnView");
                    LinkButton ForTdEdit = (LinkButton)e.Row.FindControl("imgBtnEdit");
                    LinkButton ForTdDelete = (LinkButton)e.Row.FindControl("imgBtnDelete");
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


    protected void imgSearch_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        fillEmployeeSearchGrid();
    }

    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        //if (chkActive.Checked == true)
        //    chkActive.Checked = false;
        //if (chkActive.Checked == false)
        //    chkActive.Checked = true;
        lblmsg.Text = "";
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
    }
    protected void gvNoOrganisationGrid_SelectedIndexChanged(object sender, EventArgs e)
    {

        {
            if (ddlNoOfOrgWorked.SelectedIndex == 0)
            {
                gvNoOrganisationGrid.DataSource = null;
                gvNoOrganisationGrid.DataBind();
                gvNoOrganisationGrid.Visible = false;
            }
            else
            {
                int noOfRows = Convert.ToInt32(ddlNoOfOrgWorked.SelectedValue);

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                dt.Columns.Add("NameOfOrganisation", typeof(string));
                dt.Columns.Add("Designation", typeof(string));
                dt.Columns.Add("FromDate", typeof(string));
                dt.Columns.Add("ToDate", typeof(string));
                dt.Columns.Add("WorkExperienceID", typeof(int));



                for (int i = 0; i <= gvNoOrganisationGrid.Rows.Count - 1; i++)
                {

                    DataRow dr;
                    dr = dt.NewRow();

                    dr["NameOfOrganisation"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtNameOfOrganisation")).Text;
                    dr["Designation"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtgvDesignation")).Text;
                    dr["FromDate"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtgvFromDate")).Text;
                    dr["ToDate"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtgvToDate")).Text;
                    dr["WorkExperienceID"] = ((TextBox)gvNoOrganisationGrid.Rows[i].FindControl("txtWorkExperienceID")).Text;

                    dt.Rows.Add(dr);

                }

                if (noOfRows > gvNoOrganisationGrid.Rows.Count)
                {
                    for (int i = 0; i < noOfRows - gvNoOrganisationGrid.Rows.Count; i++)
                    {

                        dt.Rows.Add("", "", "", "", 0);

                    }
                }


                DataSet dsForDelete = new DataSet();
                dsForDelete.Tables.Add(dt);
                gvNoOrganisationGrid.DataSource = dsForDelete.Tables[0];
                gvNoOrganisationGrid.DataBind();

                gvNoOrganisationGrid.Visible = true;
            }

        }
    }


    protected void ddlSearchEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
    }
    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        
            Eobj = new EEmployee();
            ds = new DataSet();
            lblmsg.Text = "";

            Eobj.FirstName = txtFirstName.Text;
            Eobj.MiddleName = txtMiddleName.Text;
            Eobj.LastName = txtLastName.Text;
            Eobj.Gender = radioMale.Checked ? "Male" : "Female";
            if (txtDob.Text != "")
            {
                Eobj.DOB = converttodate(txtDob.Text);
            }
            else
            {
                Eobj.DOB = "";
            }
            if (txtDoj.Text != "")
            {
                Eobj.DateOfJoining = converttodate(txtDoj.Text);
            }
            else
            {

                Eobj.DateOfJoining = "";
            }
            if (radioRelieving.Checked == true)
            {
                Eobj.RelievedandTerminated = "Relieved";
            }
            else if (radioTerminated.Checked == true)
            {
                Eobj.RelievedandTerminated = "Terminated";
            }
            else
            {
                Eobj.RelievedandTerminated = "";
            }

            Eobj.EmployeeNo = txtEmpID.Text;
            Eobj.DesignationTypeId = Convert.ToInt32(ddlEmpDesgn.SelectedValue);
            Eobj.StoreID = Convert.ToInt32(ddlStoreName.SelectedValue);
            Eobj.BankName = txtBnkName.Text;
            Eobj.Branch = txtBranch.Text;
            Eobj.AccountNo = txtAcNo.Text;
            Eobj.NameAsInAccount = txtNameInBranch.Text;
            Eobj.PANNo = txtPanNo.Text;

            Eobj.TempAddressLine1 = txtTempAddressLine1.Text;
            Eobj.TempAddressLine2 = txtTempAddressLine2.Text;

            Eobj.PermanentAddressLine1 = txtPermAddressLine1.Text;
            Eobj.PermanentAddressLine2 = txtPermAddressLine2.Text;

            Eobj.TempCountry = txtTempCountry.Text;
            Eobj.PermanentCountry = txtPermCountry.Text;

            Eobj.TempState = txtTempState.Text;
            Eobj.PermanentState = txtPermState.Text;

            Eobj.TempCity = txtTempCity.Text;
            Eobj.PermanentCity = txtPermCity.Text;

            Eobj.TempPinCode = txtTempZipCode.Text;
            Eobj.PermanentPinCode = txtPermZipCode.Text;

            Eobj.MobileNo = txtAltMobileNo.Text;
            Eobj.AlternateMobileNo = txtMobileNo.Text;

            Eobj.PersonalEmailID = txtPersonalEmail.Text;
            Eobj.WorkEmailID = txtWorkEmail.Text;

            if (txt10thPer.Text != "")
            {
                Eobj.TenthPercentage = Convert.ToDecimal(txt10thPer.Text);
            }
            else
            {

                Eobj.TenthPercentage = 0;
            }
            Eobj.TenthBord = txt10thBoard.Text;
            if (txt12thPer.Text != "")
            {
                Eobj.TwelfthPercentage = Convert.ToDecimal(txt12thPer.Text);
            }
            else
            {

                Eobj.TwelfthPercentage = 0;
            }
            Eobj.TwelfthBord = txt12thBoard.Text;
            Eobj.GraduationCourse = txtAddGradCourse.Text;
            if (txtGradPer.Text != "")
            {
                Eobj.GradutionPercentage = Convert.ToDecimal(txtGradPer.Text);
            }
            else
            {
                Eobj.GradutionPercentage = 0;
            }
            Eobj.GradutionBord = txtGradUniversity.Text;
            Eobj.PostGraduationCourse = txtAddpostGradCourse.Text;
            if (txtPostGradPer.Text != "")
            {
                Eobj.PostGradutionPercentage = Convert.ToDecimal(txtPostGradPer.Text);
            }
            else
            {

                Eobj.PostGradutionPercentage = 0;
            }
            Eobj.PostGradutionBord = txtPostGradUniversity.Text;
            Eobj.Others = txtQulaificationOthers.Text;

            Eobj.NoOfOrganisations = Convert.ToInt32(ddlNoOfOrgWorked.SelectedValue);

            Eobj.ContactName = txtEmergencyContactName.Text;
            Eobj.ContactNo = txtEmergencyContactNo.Text;
            Eobj.RelationShip = txtEmergencyContactRelation.Text;


            if (txtResignDate.Text != "")
            {
                Eobj.ResignationDate = converttodate(txtResignDate.Text);
            }
            else
            {
                Eobj.ResignationDate = "";
            }

            if (txtRelievingDate.Text != "")
            {

                Eobj.RelievingDate = converttodate(txtRelievingDate.Text);
            }
            else
            {
                Eobj.RelievingDate = "";
            }


            Eobj.ReasonsForLeaving = txtReasonForLeaving.Text;

            if (radioRehiredYes.Checked)
            {
                Eobj.CanBeReHired = "Yes";
            }
            else if (radioRehiredNo.Checked)
            {
                Eobj.CanBeReHired = "No";
            }
            else
            {

                Eobj.CanBeReHired = "";
            }


            if (radioExitFormalitiesCmpltdYes.Checked)
            {
                Eobj.ExitFormalitiesCompleted = "Yes";
            }
            else if (radioExitFormalitiesCmpltdNo.Checked)
            {
                Eobj.ExitFormalitiesCompleted = "No";
            }
            else
            {

                Eobj.ExitFormalitiesCompleted = "";
            }

            Eobj.EmployeeActive = chkIsActive.Checked;

            Eobj.LoginSessionId = Convert.ToInt32(Session["LOGINID"].ToString());

            string dscontents = string.Empty;


            if (ddlNoOfOrgWorked.SelectedIndex != 0)
            {

                foreach (GridViewRow gvr in gvNoOrganisationGrid.Rows)
                {

                    if ((((TextBox)gvr.FindControl("txtNameOfOrganisation")).Text) != string.Empty)
                    {
                        string strWorkExperienceId = ((TextBox)gvr.FindControl("txtWorkExperienceID")).Text;
                        dscontents += strWorkExperienceId + "~";
                        string strNameOfOrganisation = ((TextBox)gvr.FindControl("txtNameOfOrganisation")).Text;
                        dscontents += strNameOfOrganisation + "~";
                        string strDesignation = ((TextBox)gvr.FindControl("txtgvDesignation")).Text;
                        dscontents += strDesignation + "~";
                        string strFromDate = ((TextBox)gvr.FindControl("txtgvFromDate")).Text;
                        dscontents += converttodate(strFromDate) + "~";
                        string strToDate = ((TextBox)gvr.FindControl("txtgvToDate")).Text;
                        dscontents += converttodate(strToDate) + "~";

                        dscontents += "$";
                    }

                }
            }
            else
            {

                dscontents = string.Empty;
            }

            if (dscontents != "")
            {
                dscontents = dscontents.Substring(0, dscontents.Length - 1);
            }

            Eobj.GridWorkExperienceDetails = dscontents;

            Eobj.EmployeeID = Session["EmployeeID"].ToString();


            ds = Eobj.updateEmployee();

            if (ds.Tables[0].Rows[0]["Status"].ToString() == "Success")
            {
               // lblmsg.CssClass = "SuccessMsg";
                dvSuccess.Visible = true;
                lblSuccess.Text = "Employee details updated successfully";
                ShowTabs(1);
                clearGrid();

            }
            else
            {
               // lblmsg.CssClass = "ErrorMsg";
                dvFailure.Visible = true;
                lblmsg.Text = "User already exists";
                ClearControls();
                clearGrid();
               
            }
        }

    

    protected void imgClear_Click(object sender, EventArgs e)
    {
        ClearControls();
    }


    public void clearGrid()
    {
         //for clearin data in gridview organisation
                foreach (GridViewRow gvr in gvNoOrganisationGrid.Rows)
                {

                    for (int i = 0; i < gvNoOrganisationGrid.Rows.Count; i++)
                    {
                        for (int j = 0; j < gvNoOrganisationGrid.Rows[i].Cells.Count; j++)
                        {
                            gvNoOrganisationGrid.Rows[i].Cells[j].Text = "";
 
                        }
 

                    }

                }
   
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        ddlSearchEmployee.SelectedIndex = 0;
        txtsearchEmployeeNo.Text = "";
        fillEmployeeSearchGrid();
        ShowTabs(1);
      
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        ddlSearchEmployee.SelectedIndex = 0;
        txtsearchEmployeeNo.Text = "";
        fillEmployeeSearchGrid();
    }
}
