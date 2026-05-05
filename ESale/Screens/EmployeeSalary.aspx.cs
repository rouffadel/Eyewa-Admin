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

public partial class Screens_EmployeeSalary : System.Web.UI.Page
{
    EEmployeeSalary objESalarySheet;
    DataSet dsforSalarySheet;
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

            if (!IsPostBack)
            {

             
                FillSearchEmployees();

                ViewState["LoginID"] = Session["LOGINID"].ToString();
                loginid = Session["LOGINID"].ToString();
                dvFailure.Visible = false;
                dvSuccess.Visible = false;
                lblSuccess.Text = "";
                lblStatus.Text = "";
                FillEmployees();
                ShowTab(1);


            }
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }

    }

    public void FillSearchEmployees()
    {
        try
        {
           
            objESalarySheet = new EEmployeeSalary();
            DataSet dsforClients = new DataSet();
            ddlEmployee.Items.Clear();
            objESalarySheet.LoginId = Convert.ToString(Session["LOGINID"]);
            dsforClients = objESalarySheet.EFillSearchEmployee();
            if (dsforClients.Tables[0].Rows.Count > 0)
            {
                ddlSearchEmploye.DataSource = dsforClients;
                ddlSearchEmploye.DataTextField = "EmployeeName";
                ddlSearchEmploye.DataValueField = "EmployeeID";
                ddlSearchEmploye.DataBind();

                ddlSearchEmploye.Items.Insert(0, new ListItem("--Any--", "0"));

            }
            else
            {

                ddlSearchEmploye.Items.Insert(0, new ListItem("--Any--", "0"));

            }
        }
        catch (Exception ex)
        {

            lblStatus.Text = ex.Message;
        }
    }

    public void FillEmployees()
    {
        try
        {
            objESalarySheet = new EEmployeeSalary();
            DataSet ds = new DataSet();
            ddlEmployee.Items.Clear();

            objESalarySheet.LoginId = loginid;
            objESalarySheet.LoginId = Convert.ToString(Session["LOGINID"]);
            ds = objESalarySheet.EFillEmployee();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataTextField = "EmployeeName";
                ddlEmployee.DataValueField = "EmployeeID";
                ddlEmployee.DataBind();

                ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            else
            {
                ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));

            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
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
                imgUpdate.Visible = EditPermission;
                btncan.Visible = EditPermission;
                imgClear.Visible = EditPermission;
            }
            else
            {
                imgUpdate.Visible = EditPermission;

            }
            if (addPermission == true)
            {
                imgAdd.Visible = addPermission;
                btncan.Visible = addPermission;
                imgClear.Visible = addPermission;
                lirole.Visible = addPermission;
                lnkAdd.Visible = addPermission;
            }
            else
            {
                imgAdd.Visible = addPermission;
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

    public void ShowTab(int TabNum)
    {
        if (TabNum == 1)
        {
            pnladd.Visible = false;
            pnlsearch.Visible = true;
            pnlrep.Visible = false;
            gvSalaraySheet.Visible = false;
            lnkAdd.Text = "Add";
            dvFooter.Visible = false;
            FillSalarySheetGridView();
            lnkAdd.Visible = true;

        }
        else if (TabNum == 2)
        {
            pnladd.Visible = true;
            pnlsearch.Visible = false;
            pnlrep.Visible = false;
            gvSalaraySheet.Visible = false;
            dvFooter.Visible = true;
            lnkAdd.Text = "Add";
            lnkAdd.Visible = false;
        }
    }

    public void EnalbleContols(bool flag)
    {


        ddlEmployee.Enabled = flag;
        txtEmployeeID.Enabled = flag;
        txtDateOfJoining.Enabled = flag;
        txtDOB.Enabled = flag;
        txtBasicSal.Enabled = flag;

        txtHRA.Enabled = flag;
        txtTA.Enabled = flag;
        txtDA.Enabled = flag;
        txtGrassSalary.Enabled = flag;
        txtEducationCess.Enabled = flag;
        txtTDS.Enabled = flag;
        txtNetSalary.Enabled = flag;


    }

    public void ClearControls()
    {

        ddlEmployee.SelectedIndex = 0;
        txtEmployeeID.Text = string.Empty;
        txtDateOfJoining.Text = string.Empty;
        txtDOB.Text = string.Empty;
        txtBasicSal.Text = string.Empty;

        txtHRA.Text = string.Empty;
        txtTA.Text = string.Empty;
        txtDA.Text = string.Empty;
        txtGrassSalary.Text = string.Empty;
        txtEducationCess.Text = string.Empty;
        txtTDS.Text = "0.00";
        txtNetSalary.Text = string.Empty;

        txtYearlyBasicSal.Text = string.Empty;

        txtYearlyHRA.Text = string.Empty;
        txtYearlyTA.Text = string.Empty;
        txtYearlyDA.Text = string.Empty;
        txtYearlyGrassSalary.Text = string.Empty;
        txtYearlyTDS.Text = string.Empty;
        txtYearlyNetSalary.Text = string.Empty;

    }

    void showButtons(string Mode)
    {
        if (string.Compare(Mode, "Search", true) == 0)
        {

            imgClear.Visible = false;
            imgAdd.Visible = false;
            imgUpdate.Visible = false;
            dvisave.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = false;
            btncan.Visible = false;
            dvclear.Visible = false;
        }
        else if (string.Compare(Mode, "View", true) == 0)
        {
            dvisave.Visible = false;
            dvupdate.Visible = false;
            dvcancel.Visible = true;
            btncan.Visible = true;
            imgClear.Visible = false;
            imgAdd.Visible = false;
            imgUpdate.Visible = false;
            dvclear.Visible = false;
        }
        else if (string.Compare(Mode, "Edit", true) == 0)
        {
            dvisave.Visible = false;
            dvupdate.Visible = true;
            dvcancel.Visible = true;
            btncan.Visible = true;
            imgClear.Visible = false;
            imgAdd.Visible = false;
            imgUpdate.Visible = true;
            dvclear.Visible = false;
        }
        else if (string.Compare(Mode, "Add", true) == 0)
        {
            dvisave.Visible = true;
            dvupdate.Visible = false;
            dvcancel.Visible = true;
            btncan.Visible = true;
            dvclear.Visible = true;
            imgClear.Visible = true;
            imgAdd.Visible = true;
            imgUpdate.Visible = false;
        }

    }


    private object FillSalarySheetGridView()
    {
        objESalarySheet = new EEmployeeSalary();
        dsforSalarySheet = new DataSet();

        try
        {
            objESalarySheet.LoginId = Convert.ToString(Session["LOGINID"]); ;

            if (ddlSearchEmploye.SelectedIndex != 0)
            {
                objESalarySheet.EmployeeID = ddlSearchEmploye.SelectedValue;
            }
            else
                objESalarySheet.EmployeeID = "0";

            dsforSalarySheet = objESalarySheet.EViewEditSalarySheet();
            if (dsforSalarySheet.Tables[0].Rows.Count > 0)
            {
                dsforSalarySheet.Tables[0].Columns.Add("SNo", typeof(string));
                int i = 1;
                foreach (DataRow dr in dsforSalarySheet.Tables[0].Rows)
                {
                    dr["SNo"] = i;
                    i++;
                }
                pnlrep.Visible = true;
                gvSalaraySheet.Visible = true;
                gvSalaraySheet.DataSource = null;
                gvSalaraySheet.DataSource = dsforSalarySheet;
                gvSalaraySheet.DataBind();
                lblStatus.Text = string.Empty;
                gvSalaraySheet.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
            else
            {
                dvFailure.Visible = true;
                lblStatus.Text = "No records found";
                pnlrep.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }
        return dsforSalarySheet;
    }


    void FillSearchEmployeeViewEdit()
    {
        try
        {
            objESalarySheet = new EEmployeeSalary();
            DataSet dsforClients = new DataSet();
            ddlEmployee.Items.Clear();
            objESalarySheet.LoginId = Convert.ToString(Session["LoginId"]);
            dsforClients = objESalarySheet.EFillSearchEmployee();
            if (dsforClients.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = dsforClients;
                ddlEmployee.DataTextField = "EmployeeName";
                ddlEmployee.DataValueField = "EmployeeID";
                ddlEmployee.DataBind();
            }
            else
            {

                ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));


            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }

    }




    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        ShowTab(1);
        lblStatus.Text = "";
        ClearControls();
        
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ShowTab(2);
        //lblStatus.Text = "";
        showButtons("Add");
        FillEmployees();


        EnalbleContols(false);
        ClearControls();

        ddlEmployee.Enabled = true;
        txtGrassSalary.Enabled = true;
        txtTDS.Enabled = true;
        txtNetSalary.Enabled = false;
    }
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        try
        {
          
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            lblSuccess.Text = "";
            lblStatus.Text = "";
            pnlrep.Visible = true;
            FillSalarySheetGridView();
            ddlSearchEmploye.SelectedIndex = 0; 

        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }

    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {

        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";

        try
        {
            objESalarySheet = new EEmployeeSalary();
            DataSet dsforClients = new DataSet();
            if (ddlEmployee.SelectedIndex != 0)
            {

                objESalarySheet.LoginId = Session["LOGINID"].ToString();

                objESalarySheet.WhereCondition = "and EmployeeID=" + ddlEmployee.SelectedValue;

                try
                {

                    dsforClients = objESalarySheet.EFillEmployee();

                    if (dsforClients.Tables[0].Rows.Count > 0)
                    {
                        txtEmployeeID.Text = dsforClients.Tables[0].Rows[0]["EmployeeNo"].ToString();
                        txtDateOfJoining.Text = dsforClients.Tables[0].Rows[0]["DateOfJoining"].ToString();
                        txtDOB.Text = dsforClients.Tables[0].Rows[0]["DOB"].ToString();

                    }
                    else
                    {
                        txtEmployeeID.Text = string.Empty;
                        txtDateOfJoining.Text = string.Empty;
                        txtDOB.Text = string.Empty;
                        ddlEmployee.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = ex.Message;
                }
            }
            else
            {
                txtEmployeeID.Text = string.Empty;
                txtDateOfJoining.Text = string.Empty;
                txtDOB.Text = string.Empty;
                ddlEmployee.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }

    }
    protected void imgAdd_Click(object sender, EventArgs e)
    {
        try
        {
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            lblSuccess.Text = "";
            lblStatus.Text = "";
            objESalarySheet = new EEmployeeSalary();
            dsforSalarySheet = new DataSet();


            objESalarySheet.EmployeeID = ddlEmployee.SelectedValue;
            objESalarySheet.BasicSalary = Convert.ToDecimal(txtBasicSal.Text);
            objESalarySheet.HRA = Convert.ToDecimal(txtHRA.Text);
            objESalarySheet.TA = Convert.ToDecimal(txtTA.Text);
            objESalarySheet.DA = Convert.ToDecimal(txtDA.Text);
            objESalarySheet.GrossSalary = Convert.ToDecimal(txtGrassSalary.Text);
            objESalarySheet.EducationCess = Convert.ToDecimal(0);
            objESalarySheet.TDS = Convert.ToDecimal(txtTDS.Text);
            objESalarySheet.NetSalary = Convert.ToDecimal(txtNetSalary.Text);
            objESalarySheet.LoginId = loginid;
            dsforSalarySheet = objESalarySheet.EAddSalarySheet();

            if (dsforSalarySheet.Tables[0].Rows[0]["Status"].ToString() == "Success")
            {
                dvSuccess.Visible = true;
                lblStatus.Text = "Salary details saved successfully";
                ShowTab(1);
                ClearControls();
            }
            else
            {

                dvFailure.Visible = true;
                lblStatus.Text = "Employee already exists";
            }

        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }

    }
    protected void imgUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            objESalarySheet = new EEmployeeSalary();
            dsforSalarySheet = new DataSet();

            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            lblSuccess.Text = "";
            lblStatus.Text = "";


            objESalarySheet.EmployeeSalaryID = Session["nEmployeeSalaryID"].ToString();
            objESalarySheet.EmployeeID = ddlEmployee.SelectedValue;
            objESalarySheet.BasicSalary = Convert.ToDecimal(txtBasicSal.Text);
            objESalarySheet.HRA = Convert.ToDecimal(txtHRA.Text);
            objESalarySheet.TA = Convert.ToDecimal(txtTA.Text);
            objESalarySheet.DA = Convert.ToDecimal(txtDA.Text);
            objESalarySheet.GrossSalary = Convert.ToDecimal(txtGrassSalary.Text);
            objESalarySheet.EducationCess = Convert.ToDecimal(0);
            objESalarySheet.TDS = Convert.ToDecimal(txtTDS.Text);
            objESalarySheet.NetSalary = Convert.ToDecimal(txtNetSalary.Text);
            objESalarySheet.LoginId = loginid;

            dsforSalarySheet = objESalarySheet.EUpdateSalarySheet();

            if (dsforSalarySheet.Tables[0].Rows[0]["Status"].ToString() == "Success")
            {
                dvSuccess.Visible = true;
                lblSuccess.Text = "Salary details Updated successfully";
                ShowTab(1);
                ClearControls();
            }
            else
            {
                dvFailure.Visible = true;
                lblStatus.Text = "Employee  already exists";
            }

            if (addPermission == false)
            {
                //lnkSearch.Text = "Search";
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }
    }
    protected void imgClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();
            ddlSearchEmploye.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            lblStatus.Text = ex.Message;
        }


    }
    protected void gvSalaraySheet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblStatus.Text = "";
        objESalarySheet = new EEmployeeSalary();
        dsforSalarySheet = new DataSet();
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        try
        {
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int nEmployeeID = Convert.ToInt32(gvSalaraySheet.DataKeys[index].Value.ToString());
                Session["nEmployeeID"] = nEmployeeID;
                objESalarySheet.EmployeeID = nEmployeeID.ToString();
                objESalarySheet.LoginId = Convert.ToString(Session["LOGINID"]);
                dsforSalarySheet = objESalarySheet.EViewEditSalarySheet();

                if (dsforSalarySheet.Tables[0].Rows.Count > 0)
                {
                    FillSearchEmployeeViewEdit();
                    ddlEmployee.SelectedValue = dsforSalarySheet.Tables[0].Rows[0]["EmployeeID"].ToString();
                    txtEmployeeID.Text = dsforSalarySheet.Tables[0].Rows[0]["EmployeeID"].ToString();
                    txtDateOfJoining.Text = dsforSalarySheet.Tables[0].Rows[0]["DateOfJoining"].ToString();
                    txtDOB.Text = dsforSalarySheet.Tables[0].Rows[0]["DOB"].ToString();



                    txtBasicSal.Text = dsforSalarySheet.Tables[0].Rows[0]["BasicSalary"].ToString();
                    txtHRA.Text = dsforSalarySheet.Tables[0].Rows[0]["HRA"].ToString();
                    txtTA.Text = dsforSalarySheet.Tables[0].Rows[0]["TA"].ToString();
                    txtDA.Text = dsforSalarySheet.Tables[0].Rows[0]["DA"].ToString();
                    txtGrassSalary.Text = dsforSalarySheet.Tables[0].Rows[0]["GrossSalary"].ToString();
                    txtEducationCess.Text = dsforSalarySheet.Tables[0].Rows[0]["EducationCess"].ToString();
                    txtTDS.Text = dsforSalarySheet.Tables[0].Rows[0]["TDS"].ToString();
                    txtNetSalary.Text = dsforSalarySheet.Tables[0].Rows[0]["NetSalary"].ToString();


                    decimal gs = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["GrossSalary"]) * Convert.ToDecimal(12);
                    decimal bs = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["BasicSalary"]) * Convert.ToDecimal(12);
                    decimal hra = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["HRA"]) * Convert.ToDecimal(12);
                    decimal ta = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["TA"]) * Convert.ToDecimal(12);
                    decimal da = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["DA"]) * Convert.ToDecimal(12);
                    decimal tds = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["TDS"]) * Convert.ToDecimal(12);
                    decimal ns = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["NetSalary"]) * Convert.ToDecimal(12);

                    txtYearlyGrassSalary.Text = gs.ToString();

                    txtYearlyBasicSal.Text = bs.ToString();
                    txtYearlyHRA.Text = hra.ToString();
                    txtYearlyTA.Text = ta.ToString();
                    txtYearlyDA.Text = da.ToString();
                    txtYearlyTDS.Text = tds.ToString();
                    txtYearlyNetSalary.Text = ns.ToString();


                    EnalbleContols(false);
                    ddlEmployee.Enabled = false;
                    txtGrassSalary.Enabled = false;
                    txtTDS.Enabled = false;
                    ShowTab(2);



                }
                if (Session["LOGINID"].ToString() != "1")
                {
                    if (addPermission == false)
                    {
                        //lnkSearch.CssClass = "ActiveClass";
                      //  lnkSearch.Text = "View";
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
                showButtons("View");
            }
            else if (e.CommandName == "Edits")
            {


                int index = Convert.ToInt32(e.CommandArgument);
                int nEmployeeID = Convert.ToInt32(gvSalaraySheet.DataKeys[index].Value.ToString());
                Session["nEmployeeSalaryID"] = nEmployeeID;
                Session["nEmployeeID"] = nEmployeeID;
                objESalarySheet.LoginId = Convert.ToString(Session["LOGINID"]);
                objESalarySheet.EmployeeID = nEmployeeID.ToString();
                dsforSalarySheet = objESalarySheet.EViewEditSalarySheet();

                if (dsforSalarySheet.Tables[0].Rows.Count > 0)
                {


                    FillSearchEmployeeViewEdit();
                    ddlEmployee.SelectedValue = dsforSalarySheet.Tables[0].Rows[0]["EmployeeID"].ToString();
                    txtEmployeeID.Text = dsforSalarySheet.Tables[0].Rows[0]["EmployeeID"].ToString();
                    txtDateOfJoining.Text = dsforSalarySheet.Tables[0].Rows[0]["DateOfJoining"].ToString();
                    txtDOB.Text = dsforSalarySheet.Tables[0].Rows[0]["DOB"].ToString();
                    txtBasicSal.Text = dsforSalarySheet.Tables[0].Rows[0]["BasicSalary"].ToString();

                    txtHRA.Text = dsforSalarySheet.Tables[0].Rows[0]["HRA"].ToString();
                    txtTA.Text = dsforSalarySheet.Tables[0].Rows[0]["TA"].ToString();
                    txtDA.Text = dsforSalarySheet.Tables[0].Rows[0]["DA"].ToString();
                    txtGrassSalary.Text = dsforSalarySheet.Tables[0].Rows[0]["GrossSalary"].ToString();
                    txtEducationCess.Text = dsforSalarySheet.Tables[0].Rows[0]["EducationCess"].ToString();
                    txtTDS.Text = dsforSalarySheet.Tables[0].Rows[0]["TDS"].ToString();
                    txtNetSalary.Text = dsforSalarySheet.Tables[0].Rows[0]["NetSalary"].ToString();


                    decimal gs = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["GrossSalary"]) * Convert.ToDecimal(12);
                    decimal bs = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["BasicSalary"]) * Convert.ToDecimal(12);
                    decimal hra = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["HRA"]) * Convert.ToDecimal(12);
                    decimal ta = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["TA"]) * Convert.ToDecimal(12);
                    decimal da = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["DA"]) * Convert.ToDecimal(12);
                    decimal tds = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["TDS"]) * Convert.ToDecimal(12);
                    decimal ns = Convert.ToDecimal(dsforSalarySheet.Tables[0].Rows[0]["NetSalary"]) * Convert.ToDecimal(12);

                    txtYearlyGrassSalary.Text = gs.ToString();

                    txtYearlyBasicSal.Text = bs.ToString();
                    txtYearlyHRA.Text = hra.ToString();
                    txtYearlyTA.Text = ta.ToString();
                    txtYearlyDA.Text = da.ToString();
                    txtYearlyTDS.Text = tds.ToString();
                    txtYearlyNetSalary.Text = ns.ToString();

                    EnalbleContols(false);
                    ddlEmployee.Enabled = true;
                    txtGrassSalary.Enabled = true;
                    txtTDS.Enabled = true;
                    ShowTab(2);



                }
                else
                {
                    dvFailure.Visible = true;
                    lblStatus.Text = "No Records are available";
                   // ShowTab(2);
                }
                if (Session["LOGINID"].ToString() != "1")
                {
                    if (addPermission == false)
                    {
                        //lnkSearch.CssClass = "ActiveClass";
                       // lnkSearch.Text = "Edit";
                    }
                    else
                    {
                        //lnkAdd.Text = "Edit";
                        //lnkAdd.CssClass = "ActiveClass";
                    }
                }
                else
                {
                   // lnkAdd.Text = "Edit";
                }
                showButtons("Edit");
            }
            else if (e.CommandName == "Deletes")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int nEmployeeID = Convert.ToInt32(gvSalaraySheet.DataKeys[index].Value.ToString());
                Session["nEmployeeID"] = nEmployeeID;
                objESalarySheet.EmployeeID = Convert.ToInt32(nEmployeeID).ToString();
                objESalarySheet.LoginId = loginid;
                dsforSalarySheet = objESalarySheet.EDeleteSalarySheet();

                if (dsforSalarySheet.Tables[0].Rows[0]["Status"].ToString() == "Success")
                {
                    dvSuccess.Visible = true;
                    lblSuccess.Text = "Delete Transaction Successful";

                    ShowTab(1);
                }
                else
                {
                    dvFailure.Visible = true;
                    lblStatus.Text = "Delete Transaction UnSuccessful";
                }


            }

        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }
    }
    protected void gvSalaraySheet_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvSalaraySheet_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

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
    protected void ddlSearchEmploye_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        if (gvSalaraySheet.HeaderRow != null)
            gvSalaraySheet.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlSearchEmploye.SelectedIndex = 0;
        FillSalarySheetGridView();
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lnkAdd.Visible = true;
        ddlSearchEmploye.SelectedIndex = 0;
        FillSalarySheetGridView();
        ShowTab(1);
    }
}
