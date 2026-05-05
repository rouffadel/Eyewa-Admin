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
using System.Globalization;
public partial class Screens_PettyExpenses : System.Web.UI.Page
{
    string loginid = "";
    DataSet ds;
    int k;
    EPettyExpenses Expenses;
    ECheckPermission ECPobj;
    static bool addPermission = false;
    static bool viewPermission = true;
    static bool EditPermission = true;
    static bool deletepermission = true;
    string ScreenUrl = string.Empty;
    DataTable dt;
    DataTable dtExpense;
    int l;
    string result;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LOGINID"] != null)
        {
            if (Session["LOGINID"].ToString() != "1" && Session["LOGINNAME"].ToString() != "admin")
            {
                //PCheckPermission();
            }

            if (!IsPostBack)
            {
                ViewState["LoginID"] = Session["LOGINID"].ToString();
                loginid = Session["LOGINID"].ToString();                
                lblStatus.Text = "";
                for (int month = 1; month <= 12; month++)
                {
                    string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                    ddlMonth.Items.Add(new ListItem(monthName, monthName));
                }
                int index = 0;
                for (int Year = 2013; Year <= DateTime.Now.Year + 2; Year++)
                {
                    ListItem li = new ListItem(Year.ToString(), Year.ToString());
                    ddlyear.Items.Insert(index, li);
                    index++;
                }
                CultureInfo ci = new CultureInfo("en-US");
                string currentmonth = DateTime.Now.ToString("MMMM", ci);

                string currentyear = DateTime.Now.Year.ToString();
                ddlMonth.SelectedValue = currentmonth;
                ddlyear.SelectedValue = currentyear;
                DropDownStore();
                if (ddlStore.SelectedValue != "0")
                {
                    LoadData();
                    gvPettyExpenses.Visible = true;
                    ImgSave.Visible = true;                   
                    save.Visible = true;
                    dvcancel.Visible = true;
                    btnClear.Visible = true;
                    btncan.Visible = true;
                    dvclear.Visible = true;
                }
                else
                {
                    gvPettyExpenses.Visible = false;
                    ImgSave.Visible = false;
                    save.Visible = false;
                    dvcancel.Visible = false;
                    btnClear.Visible = false;
                    btncan.Visible = false;
                    dvclear.Visible = false;
                }
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

    private void LoadData()
    {
        try
        {
            ds = new DataSet();
            Expenses = new EPettyExpenses();
            if (ddlStore.SelectedValue != "0")
                Expenses.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            else
                Expenses.StoreID = 0;
            Expenses.Month = ddlMonth.SelectedValue;
            Expenses.Year = ddlyear.SelectedValue;
            if (chkOldPendings.Checked)
                Expenses.Check = "Y";
            else
                Expenses.Check = "";

           
            Expenses.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            ds = Expenses.ddlExpense();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtExpense = ds.Tables[0];
            }
            else
            {
                dtExpense = new DataTable();
            }
            ds = Expenses.getGridData();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count < 5)
                {
                    DataRow dr;
                    for(int i=ds.Tables[0].Rows.Count;i<5;i++)
                    {
                        dr= ds.Tables[0].NewRow();
                        dr["PettyExpenseDetailsID"] = "0";
                        dr["ExpenseTypeID"] = "0";
                        dr["InvoiceNo"] = "";
                        dr["InvoiceDate"] = System.DateTime.Today.ToString("dd-MM-yyyy");
                        dr["PaymentDueDate"] = System.DateTime.Today.AddDays(7).ToString("dd-MM-yyyy");
                        dr["ExpenseAmount"] = "0.00";
                        dr["AmountPaid"] = "0.00";
                        dr["Balance"] = "0.00";
                        ds.Tables[0].Rows.Add(dr);
                        
                    }
                    dt = ds.Tables[0];
                    l = 0;
                    gvPettyExpenses.DataSource = dt;
                    gvPettyExpenses.DataBind();
                }
                   
                else
                {
                    DataRow dr = ds.Tables[0].NewRow();
                    dr["PettyExpenseDetailsID"] = "0";
                    dr["ExpenseTypeID"] = "0";
                    dr["InvoiceNo"] = "";
                    dr["InvoiceDate"] = System.DateTime.Today.ToString("dd-MM-yyyy");
                    dr["PaymentDueDate"] = System.DateTime.Today.AddDays(7).ToString("dd-MM-yyyy");
                    dr["ExpenseAmount"] = "0.00";
                    dr["AmountPaid"] = "0.00";
                    dr["Balance"] = "0.00";
                    ds.Tables[0].Rows.Add(dr);
                    dt = ds.Tables[0];
                    ds = Expenses.ddlExpense();
                    if (ds.Tables[0].Rows.Count > 0)
                        dtExpense = ds.Tables[0];
                    l = 0;
                    gvPettyExpenses.DataSource = dt;
                    gvPettyExpenses.DataBind();
                }
            }
            else
            {
                        
                DataRow dr;
                for (int i = 0; i < 5; i++)
                {
                    dr = ds.Tables[0].NewRow();
                    dr["PettyExpenseDetailsID"] = "0";
                    dr["ExpenseTypeID"] = "0";
                    dr["InvoiceNo"] = "";
                    dr["InvoiceDate"] = System.DateTime.Today.ToString("dd-MM-yyyy");
                    dr["PaymentDueDate"] = System.DateTime.Today.AddDays(7).ToString("dd-MM-yyyy");
                    dr["ExpenseAmount"] = "0.00";
                    dr["AmountPaid"] = "0.00";
                    dr["Balance"] = "0.00";
                    ds.Tables[0].Rows.Add(dr);
                }
                dt = ds.Tables[0];
                ds = Expenses.ddlExpense();
                if (ds.Tables[0].Rows.Count > 0)
                    dtExpense = ds.Tables[0];
                l = 0;
                gvPettyExpenses.DataSource = dt;
                gvPettyExpenses.DataBind();
            }
        }
        catch (Exception)
        {
            
            throw;
        }
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
    public void DropDownStore()
    {
        try
        {
            Expenses = new EPettyExpenses();
            Expenses.LoginID = Convert.ToString(Session["LOGINID"]);
            Expenses.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            ds = Expenses.FillddlStore();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStore.DataSource = ds.Tables[0];
                ddlStore.DataTextField = "StoreName";
                ddlStore.DataValueField = "StoreID";
                ddlStore.DataBind();
                ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                ddlStore.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            ddlStore.Enabled = true;
            if (Convert.ToInt32(Session["LOGINID"]) != 1 && ds.Tables[0].Rows.Count == 1)
            {
                ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);              
                ddlStore.Enabled = false;
            }
           // ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Expenses.Month = ddlMonth.SelectedValue;
        LoadData();
    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Expenses.Year = ddlyear.SelectedValue;
        LoadData();
    }

    protected void txtAmount_TextChanged1(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        lblStatus.Text = "";
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            TextBox txtamount = (TextBox)gr.FindControl("txtAmount");
            TextBox txtBal = (TextBox)gr.FindControl("txtBalance");
            HiddenField hd = (HiddenField)gr.FindControl("hdBalance");
            txtBal.Text = txtamount.Text;
            hd.Value = txtamount.Text;
            int rowcount = gvPettyExpenses.Rows.Count;
            txtamount = sender as TextBox;
            string ID = txtamount.ClientID;
            ID = ID.Replace("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl", "");
            ID = ID.Replace("_txtAmount", "");
            int maxrow = Convert.ToInt32(ID) - 1;
            if (rowcount == maxrow)
            {
                AddNewEmptyRow();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void AddNewEmptyRow()
    {
        try
        {
            dt = new DataTable();
            dt.Columns.Add("PettyExpenseDetailsID");
            dt.Columns.Add("ExpenseType");
            dt.Columns.Add("ExpenseTypeID");
            dt.Columns.Add("InvoiceNo");
            dt.Columns.Add("InvoiceDate");
            dt.Columns.Add("PaymentDueDate");
            dt.Columns.Add("ExpenseAmount");
            dt.Columns.Add("AmountPaid");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Status");
            DataRow dr;
            for (int i = 0; i <= gvPettyExpenses.Rows.Count - 1; i++)
            {

                dr = dt.NewRow();
                dr["PettyExpenseDetailsID"] = ((HiddenField)gvPettyExpenses.Rows[i].FindControl("hdnpettyexpensedetailsID")).Value;
                dr["ExpenseTypeID"] = ((HiddenField)gvPettyExpenses.Rows[i].FindControl("HDExpenseTypeID")).Value;
                dr["ExpenseType"] = ((TextBox)gvPettyExpenses.Rows[i].FindControl("txtExpenses")).Text;
                dr["InvoiceNo"] = ((TextBox)gvPettyExpenses.Rows[i].FindControl("txtInvoiceNo")).Text;
                dr["InvoiceDate"] = ((TextBox)gvPettyExpenses.Rows[i].FindControl("txtInvoiceDate")).Text;
                dr["PaymentDueDate"] = ((TextBox)gvPettyExpenses.Rows[i].FindControl("txtPaymentDueDate")).Text;
                dr["ExpenseAmount"] = ((TextBox)gvPettyExpenses.Rows[i].FindControl("txtAmount")).Text;
                dr["AmountPaid"] = ((TextBox)gvPettyExpenses.Rows[i].FindControl("txtAmountPaid")).Text;
                dr["Balance"] = ((TextBox)gvPettyExpenses.Rows[i].FindControl("txtBalance")).Text;

                dr["Status"] = ((DropDownList)gvPettyExpenses.Rows[i].FindControl("ddlStatus")).SelectedValue;
                dt.Rows.Add(dr);
            }
            dr = dt.NewRow();
            dr["PettyExpenseDetailsID"] = "0";
            dr["ExpenseTypeID"] = "0";
            dr["ExpenseType"] = "";
            dr["InvoiceNo"] = "";
            dr["InvoiceDate"] = System.DateTime.Now.ToString("dd-MM-yyyy");
            dr["PaymentDueDate"] = System.DateTime.Now.ToString("dd-MM-yyyy");
            dr["ExpenseAmount"] = "0.00";
            dr["AmountPaid"] = "0.00";
            dr["Balance"] = "0.00";
            dr["Status"] = "0";
            dt.Rows.Add(dr);
            k = 0;
            ds = new DataSet();
            Expenses = new EPettyExpenses();
            ds = Expenses.ddlExpense();
            if (ds.Tables[0].Rows.Count > 0)
                dtExpense = ds.Tables[0];
            l = 0;
            gvPettyExpenses.DataSource = dt;
            gvPettyExpenses.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ImgSave_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        Expenses = new EPettyExpenses();
        string GridData = string.Empty;
        Expenses.LoginID = (Session["LOGINID"]).ToString();
        int PaymentDetailsID;
        string expensetype = string.Empty;
        try
        {
            for (int i = 0; i < gvPettyExpenses.Rows.Count; i++)
            {
                expensetype = Convert.ToString(((TextBox)gvPettyExpenses.Rows[i].FindControl("txtExpenses")).Text);
                if (expensetype !="")
                {
                    //if (((HiddenField)gvPettyExpenses.Rows[i].FindControl("HDExpenseTypeID")).Value != "")
                             GridData += ((HiddenField)gvPettyExpenses.Rows[i].FindControl("HDExpenseTypeID")).Value + "~";
                            //+ ((TextBox)gvPettyExpenses.Rows[i].FindControl("txtInvoiceNo")).Text + "~"
                            GridData+= converttodate(((TextBox)gvPettyExpenses.Rows[i].FindControl("txtInvoiceDate")).Text) + "~";
                            //+((TextBox)gvPettyExpenses.Rows[i].FindControl("txtPaymentDueDate")).Text + "~"
                           GridData+= ((TextBox)gvPettyExpenses.Rows[i].FindControl("txtAmount")).Text + "~";
                           GridData+= ((TextBox)gvPettyExpenses.Rows[i].FindControl("txtAmountPaid")).Text + "~";
                           GridData+=((TextBox)gvPettyExpenses.Rows[i].FindControl("txtBalance")).Text + "~";
                           GridData += ((HiddenField)gvPettyExpenses.Rows[i].FindControl("hdnpettyexpensedetailsID")).Value + "~";
                           if (((TextBox)gvPettyExpenses.Rows[i].FindControl("txtPayAmount")).Text != "")
                               GridData += ((TextBox)gvPettyExpenses.Rows[i].FindControl("txtPayAmount")).Text + "$";
                           else GridData += "0" + "$";
                }
            }
            if (GridData != "")
            {
                GridData = GridData.Substring(0, GridData.Length - 1);
            }
            else
            {
                GridData = "";
                lblStatus.Text = "No Insertion Made.";
                return;
            }
            Expenses.GridData = GridData;
            if (ddlMonth.SelectedValue != "0")
            {
                Expenses.Month = ddlMonth.SelectedValue;
            }
            else
            {
                Expenses.Month = "0";
            }
            if (ddlStore.SelectedValue != "0")
            {
                Expenses.StoreID=Convert.ToInt32(ddlStore.SelectedValue);
            }
            else
            {
                Expenses.StoreID = 0;
            }
            if (ddlyear.SelectedValue != "0")
            {
                Expenses.Year = ddlyear.SelectedValue;
            }
            else
            {
                Expenses.Year = "0";
            }
            Expenses.LoginID = loginid;
            result =Convert.ToString( Expenses.EPettyInsert());
            if (result == "Success" || result == "Record Already Exists")
            {
                lblStatus.Text = "Information inserted successfully.";
                LoadData();
            }
            else
            {
                lblStatus.Text = "Error to insert";
               // LoadData();
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void gvPettyExpenses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HiddenField hd = null;
            DropDownList ddlexpense = null;
            DropDownList ddlStatus = null;
            LinkButton imgdelete = null;
            LinkButton lnk = null;
            Image imginvdate=null, imgduedate = null;
            TextBox tt = null;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddlexpense = (DropDownList)e.Row.FindControl("ddlExpense");
                ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                imginvdate = (Image)e.Row.FindControl("");
                imgduedate = (Image)e.Row.FindControl("");
                if (dtExpense.Rows.Count > 0)
                {
                    ddlexpense.DataSource = dtExpense;
                    ddlexpense.DataTextField = "ExpenseType";
                    ddlexpense.DataValueField = "ExpenseTypeID";
                    ddlexpense.DataBind();
                    ddlexpense.Items.Insert(0, new ListItem("--Any--", "0"));
                }
                else
                {
                    ddlexpense.Items.Insert(0, new ListItem("--Any--", "0"));
                }
                if (Convert.ToString(dt.Rows[l]["ExpenseType"]) != "")
                {
                    tt = (TextBox)e.Row.FindControl("txtExpenses");
                    tt.Text = Convert.ToString(dt.Rows[l]["ExpenseType"]);
                }
                if (Convert.ToInt32(dt.Rows[l]["ExpenseTypeID"]) != 0)
                {
                    hd = (HiddenField)e.Row.FindControl("HDExpenseTypeID");
                    hd.Value = Convert.ToString(dt.Rows[l]["ExpenseTypeID"]);
                }
                if (Convert.ToInt32(dt.Rows[l]["PettyExpenseDetailsID"]) != 0)
                {
                    ddlexpense.SelectedValue = Convert.ToString(dt.Rows[l]["ExpenseTypeID"]);
                    ddlStatus.SelectedValue = Convert.ToString(dt.Rows[l]["Status"]);
                    if (Convert.ToString(dt.Rows[l]["Status"]) == "CLOSED")
                        ddlStatus.SelectedValue = "CLOSED";
                    ddlexpense.Enabled = false;
                    tt = (TextBox)e.Row.FindControl("txtAmount");
                    tt.Enabled = false;
                   // if(Convert.ToString(dt.Rows[l]["AmountPaid"])!="")

                    CultureInfo ci = new CultureInfo("en-US");
                    string cudate = System.DateTime.Today.ToString("MM-dd-yy");
                    DateTime currentdate = Convert.ToDateTime(cudate, ci);
                    string paymentduedate = ((TextBox)e.Row.FindControl("txtPaymentDueDate")).Text;
                    string[] dttemp = paymentduedate.Split('-');
                    string status = dt.Rows[l]["Status"].ToString();
                    
                    if (paymentduedate != "")
                    {
                        paymentduedate = dttemp[1] + "-" + dttemp[0] + "-" + dttemp[2];
                    }
                    //string paymentduedate1 = paymentduedate;
                    //DateTime pDate = Convert.ToDateTime(paymentduedate1, ci);
                    //if (pDate > currentdate)
                    //{
                    //    e.Row.BackColor = System.Drawing.Color.White;
                    //}
                    //else if (pDate == currentdate)
                    //{
                    //    e.Row.BackColor = System.Drawing.Color.Orange;
                    //}
                    //else
                    //{
                    //    e.Row.BackColor = System.Drawing.Color.Red;
                    //}


                    if (status.Trim().ToUpper()=="OPEN")
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                    }
                    else if (status.Trim().ToUpper() == "CLOSED")
                    {
                        e.Row.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }




                    if (ddlStatus.SelectedValue == "OPEN")
                    {
                        imgdelete = (LinkButton)e.Row.FindControl("imgDelete");
                        imgdelete.Visible = true;
                        lnk = (LinkButton)e.Row.FindControl("lnkPay");
                        lnk.Visible = true;
                    }
                    else if (ddlStatus.SelectedValue == "PARTIAL")
                    {
                        imgdelete = (LinkButton)e.Row.FindControl("imgDelete");
                        imgdelete.Visible = false;
                        lnk = (LinkButton)e.Row.FindControl("lnkPay");
                        lnk.Visible = true;
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
                    else if(ddlStatus.SelectedValue=="CLOSED")
                    {
                        imgdelete = (LinkButton)e.Row.FindControl("imgDelete");
                        imgdelete.Visible = false;
                        lnk = (LinkButton)e.Row.FindControl("lnkPay");
                        lnk.Visible = true;
                    }
                    
                }
                else
                {
                    imgdelete = (LinkButton)e.Row.FindControl("imgDelete");
                    imgdelete.Visible = false;
                    lnk = (LinkButton)e.Row.FindControl("lnkPay");
                    lnk.Visible = false;
                    ddlexpense = (DropDownList)e.Row.FindControl("ddlExpense");
                    ddlexpense.SelectedValue = Convert.ToString(dt.Rows[l]["ExpenseTypeID"]);
                    ddlStatus.SelectedValue = "OPEN";
                }
                l++;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void gvPettyExpenses_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        Expenses = new EPettyExpenses();
        ds = new DataSet();
        dt = new DataTable();
        try
        {
            if (e.CommandName == "pay")
            {
                HtmlGenericControl mydiv = new HtmlGenericControl("DIV");
                mydiv.ID = "Divid";
                mydiv.Attributes.Add("class", "modalOverlay");
                this.Controls.Add(mydiv);
                lblmsg1.Text = string.Empty;
                int index = Convert.ToInt32(e.CommandArgument);
                Session["index"] = index;
                Expenses.PettyExpensesDetailsID = Convert.ToInt32(gvPettyExpenses.DataKeys[index].Value);
                ViewState["PettyExpensesDetailsID"] = Expenses.PettyExpensesDetailsID;
                dvPaymentDetails.Attributes["style"] = "display:block;overflow:auto;width:1075px;margin-left:2%;height:520px;";
                gvPettyPayment.Visible = true;
                LoadPayments(Expenses.PettyExpensesDetailsID);
                
            }
            else if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Expenses.PettyExpensesDetailsID = Convert.ToInt32(gvPettyExpenses.DataKeys[index].Value);
                Expenses.LoginID = Convert.ToString(Session["LOGINID"]);
                result = Expenses.EPettyDelete();
                if(result!="")
                {
                    LoadData();
                    lblmsg1.Text=result;


                }
            }
            
        }
        catch (Exception)
        {

            throw;
        }
        
    }
    protected void CreateEmptyRows()
    {
        try
        {
            dt = new DataTable();
            if (dt.Rows.Count == 0 || dt.Rows.Count < 5)
            {
                dt = new DataTable();
                //dt.Columns.Add("PettyExpensePaymentsID");
                dt.Columns.Add("PaymentAmount");
                dt.Columns.Add("PaymentDate");
                dt.Columns.Add("PaymentMode");
                dt.Columns.Add("ReceiptNo");
                dt.Columns.Add("Balance");
                dt.Columns.Add("PettyExpensePaymentsID");
            }
            DataRow dr;
            for (int i = dt.Rows.Count; i < 5; i++)
            {
                dr = dt.NewRow();
                //dr["InvoicePaymentID"] = "0";
                dr["PaymentAmount"] = "0.00";
                dr["PaymentDate"] = System.DateTime.Now.ToString("dd-MM-yyyy");
                dr["PaymentMode"] = "0";
                dr["ReceiptNo"] = "";
                dr["Balance"] = "0.00";
                dr["PettyExpensePaymentsID"] = "0";
                dt.Rows.Add(dr);
            }
            k = 0;
            gvPettyPayment.DataSource = dt;
            gvPettyPayment.DataBind();
            gvPettyPayment.Visible = true;
        }
        catch (Exception)
        {

            throw;
        }
    }
   
    protected void AddNewEmptyRowForPayments()
    {
        try
        {
            dt = new DataTable();
            dt.Columns.Add("PaymentAmount");
            dt.Columns.Add("PaymentDate");
            dt.Columns.Add("PaymentMode");
            dt.Columns.Add("ReceiptNo");
            dt.Columns.Add("Balance");
            dt.Columns.Add("PettyExpensePaymentsID");
            DataRow dr;
            for (int i = 0; i <= gvPettyPayment.Rows.Count - 1; i++)
            {

                dr = dt.NewRow();
                dr["PettyExpensePaymentsID"] = ((HiddenField)gvPettyPayment.Rows[i].FindControl("hdnPettyExpensePaymentsID")).Value;
                dr["PaymentAmount"] = ((TextBox)gvPettyPayment.Rows[i].FindControl("txtPettyPaymentAmount")).Text;
                dr["PaymentDate"] = ((TextBox)gvPettyPayment.Rows[i].FindControl("txtPopupPaymentDate")).Text;
                dr["PaymentMode"] = ((DropDownList)gvPettyPayment.Rows[i].FindControl("ddlPaymentMode")).SelectedValue;
                dr["ReceiptNo"] = ((TextBox)gvPettyPayment.Rows[i].FindControl("txtPettyRecieptNo")).Text;
                dr["Balance"] = ((TextBox)gvPettyPayment.Rows[i].FindControl("txtPettyBalance")).Text;
                dt.Rows.Add(dr);
            }
            dr = dt.NewRow();
            dr["PettyExpensePaymentsID"] = "0";
            dr["PaymentAmount"] = "0.00";
            dr["PaymentDate"] = System.DateTime.Now.ToString("dd-MM-yyyy");
            dr["PaymentMode"] = "0";
            dr["ReceiptNo"] = "";
            dr["Balance"] = "0.00";
            dt.Rows.Add(dr);
            k = 0;
            gvPettyPayment.DataSource = dt;
            gvPettyPayment.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void gvPettyExpenses_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStore.SelectedValue != "0")
        {
            LoadData();
            gvPettyExpenses.Visible = true;
            ImgSave.Visible = true;
            save.Visible = true;
            dvcancel.Visible = true;
            btnClear.Visible = true;
            btncan.Visible = true;
            dvclear.Visible = true;
                
        }
        else
        {
            gvPettyExpenses.Visible = false;
            ImgSave.Visible = false;
            save.Visible = false;
            dvcancel.Visible = false;
            btnClear.Visible = false;
            btncan.Visible = false;
            dvclear.Visible = false;
        }
    }
    protected void gvPettyPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = null;
            ddl = (DropDownList)e.Row.FindControl("ddlPaymentMode");
            if (Convert.ToInt32(dt.Rows[k]["PettyExpensePaymentsID"]) != 0)
            {

                ddl.SelectedValue = Convert.ToString(dt.Rows[k]["PaymentMode"]);
                e.Row.Enabled = false;
            }
            else
            {
                ddl.SelectedValue = Convert.ToString(dt.Rows[k]["PaymentMode"]);
            }
            k++;
        }
    }
    protected void gvPettyPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
    protected void btnSavePayments_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            Expenses = new EPettyExpenses();
            string Griddata = string.Empty;
            int PaymentId = 0;
            for (int i = 0; i < gvPettyPayment.Rows.Count; i++)
            {
                
                PaymentId = Convert.ToInt32(((HiddenField)gvPettyPayment.Rows[i].FindControl("hdnPettyExpensePaymentsID")).Value);
                if (PaymentId == 0)
                {
                    if (((DropDownList)gvPettyPayment.Rows[i].FindControl("ddlPaymentMode")).SelectedValue != "0" && ((TextBox)gvPettyPayment.Rows[i].FindControl("txtPettyPaymentAmount")).Text != "" && ((TextBox)gvPettyPayment.Rows[i].FindControl("txtPettyBalance")).Text != "")
                        Griddata += converttodate(((TextBox)gvPettyPayment.Rows[i].FindControl("txtPopupPaymentDate")).Text) + "~"
                             + ((DropDownList)gvPettyPayment.Rows[i].FindControl("ddlPaymentMode")).SelectedValue + "~"
                            + ((TextBox)gvPettyPayment.Rows[i].FindControl("txtPettyPaymentAmount")).Text + "~"
                            + ((TextBox)gvPettyPayment.Rows[i].FindControl("txtPettyRecieptNo")).Text + "~"
                            + ((TextBox)gvPettyPayment.Rows[i].FindControl("txtPettyBalance")).Text + "$";
                }
            }
            if (Griddata != "")
                Griddata = Griddata.Substring(0, Griddata.Length - 1);
            Expenses.GridData = Griddata;
            Expenses.PettyExpensesDetailsID = Convert.ToInt32(ViewState["PettyExpensesDetailsID"]);
            Expenses.LoginID = Session["LOGINID"].ToString();
           
            result = Expenses.SavePettyPayment().ToString();
            if (result !="")
            {
                
                //LoadPayments(Convert.ToInt32(ViewState["PettyExpensesDetailsID"]));
                dvPaymentDetails.Attributes["style"] = "display:none";
                LoadData();
                dvSuccess.Visible = true;
                lblSuccess.Text = "Records inserted successfully.";
            }
            else
            {
                lblmsg1.Text = "Insertion Fails";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void LoadPayments(int pettyexpensedetailid)
    {
        try
        {
            Expenses = new EPettyExpenses();
            ds = new DataSet();
            Expenses.PettyExpensesDetailsID = pettyexpensedetailid;
            ds = Expenses.EPettyPayment();
            if (ds.Tables[0].Rows.Count > 0)
            {
                int index = Convert.ToInt32(Session["index"]);
                DropDownList drdnstatus = new DropDownList();
                drdnstatus = (DropDownList)gvPettyExpenses.Rows[index].FindControl("ddlStatus");

                dt = ds.Tables[0];
                if(drdnstatus.SelectedValue != "CLOSED")
                {
                    btnSavePayments.Visible = true;
                    //btnCancelPayments.Visible = true;   
                    DataRow dr = dt.NewRow();
                    //dr[""] = "0";
                    dr["PaymentAmount"] = "0.00";
                    dr["PaymentDate"] = System.DateTime.Now.ToString("dd-MM-yyyy");
                    dr["PaymentMode"] = "0";
                    dr["ReceiptNo"] = "";
                    dr["Balance"] = "0.00";
                    dr["PettyExpensePaymentsID"] = "0";
                    dt.Rows.Add(dr);
                }
                else
                {
                    btnSavePayments.Visible = false;
                    //btnCancelPayments.Visible = false;
                }
                k = 0;
               
                gvPettyPayment.DataSource = ds.Tables[0];
                gvPettyPayment.DataBind();
                gvPettyPayment.Visible = true;
            }
            //lblCapPaymentDetailsId.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
            else
            {
                CreateEmptyRows();
            }
            ds = Expenses.EGetPettyAmount();
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtPopupAmount.Text = ds.Tables[0].Rows[0]["ExpenseAmount"].ToString();
                txtPopupInvoiceNo.Text = ds.Tables[0].Rows[0]["InvoiceNo"].ToString();

            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void ddlPaymentMode_selectedindexchanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            DropDownList ddlpaymentmode = (DropDownList)gr.FindControl("ddlPaymentMode");
            int rowcount = gvPettyPayment.Rows.Count;
            ddlpaymentmode = sender as DropDownList;
            string ID = ddlpaymentmode.ClientID;
            ID = ID.Replace("ctl00_ContentPlaceHolder1_gvPettyPayment_ctl", "");
            ID = ID.Replace("_ddlPaymentMode", "");
            int maxrow = Convert.ToInt32(ID) - 1;
            if (rowcount == maxrow)
            {
                AddNewEmptyRowForPayments();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnCancelPayments_Click(object sender, EventArgs e)
    {
        dvPaymentDetails.Attributes["style"] = "display:none;";
        gvPettyPayment.Visible = false;
        lblmsg1.Text = "";
    }

    protected void gvPettyExpenses_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void chkOldPendings_CheckedChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void txtExpenses_ontextchanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
            TextBox txtExpense = (TextBox)gr.FindControl("txtExpenses");
            HiddenField hdexpensetypeid = (HiddenField)gr.FindControl("HDExpenseTypeID");
            Expenses = new EPettyExpenses();
            if (txtExpense.Text != "")
            {
                Expenses.ExpenseName = txtExpense.Text.Trim();
                ds = new DataSet();
                ds = Expenses.GetExpenseID();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdexpensetypeid.Value = Convert.ToString(ds.Tables[0].Rows[0]["ExpenseTypeID"]);
                }
            }
            
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void imgPettyExpenses_onclick(object sender, EventArgs e)
    {
        try
        {
            dvExpensePopUP.Attributes["style"] = "display:block";
        }
        catch (Exception)
        {
            
            throw;
        }  
    }
    protected void imgAddExpenses_onclick(object sender, EventArgs e)
    {
        try
        {
            DataSet dsex = new DataSet();
            EexpenseType exobj = new EexpenseType();
            if (txtAddExpense.Text != "")
                exobj.ExpenseType = txtAddExpense.Text;
            else
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('please enter expense type.');", true);
                return;
            }
            //exobj.StoreID = Convert.ToInt32(ddladdStore.SelectedValue);
            exobj.LoginID = Convert.ToInt32(Session["LOGINID"].ToString());
            result = exobj.EAddExpense();
            if (result != "")
            {

                lblSuccess.Text = result;
                dvSuccess.Visible = true;
                //lblStatus.CssClass = "SuccessMsg";
                txtAddExpense.Text = string.Empty;
                dvExpensePopUP.Attributes["style"] = "display:none";
            }
            else
            {
                dvFailure.Visible = true;
                lblStatus.Text = "Details already Exist";
                //lblStatus.CssClass = "ErrorMsg";
                txtAddExpense.Text = string.Empty;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnExpensesCancel_onclick(object sender, EventArgs e)
    {
        dvExpensePopUP.Attributes["style"] = "display:none";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblSuccess.Text = "";
        lblStatus.Text = "";
        ddlStore.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        ddlyear.SelectedIndex = 0;

    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;        
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        ddlStore.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;
        ddlyear.SelectedIndex = 0;
       // gvPettyExpenses.Visible = false;
        //pnlGrid.Visible = false;
    }
    //protected void txtPayAmount_TextChanged(object sender, EventArgs e)
    //{
       
    //    try
    //    {
    //        GridViewRow gr = (GridViewRow)((Control)sender).NamingContainer;
    //        TextBox txtamount = (TextBox)gr.FindControl("txtAmount");
    //        TextBox txtBal = (TextBox)gr.FindControl("txtBalance");
    //        TextBox txtPayAmt = (TextBox)gr.FindControl("txtPayAmount");
    //        TextBox txtAmtPaid = (TextBox)gr.FindControl("txtAmountPaid");
    //        HiddenField hdbal = (HiddenField)gr.FindControl("hdBalance");
    //        HiddenField hdpaidamnt=(HiddenField)gr.FindControl("hdamountpaid");
    //        if (txtPayAmt.Text != "" && hdbal.Value != "" && hdpaidamnt.Value != "")
    //        {
    //            decimal amount = Convert.ToDecimal(txtPayAmt.Text);
    //            decimal balance = Convert.ToDecimal(txtBal.Text);
    //            decimal paidamount = Convert.ToDecimal(txtPayAmt.Text);
    //            if (txtPayAmt.Text != "" && txtPayAmt.Text != null && txtPayAmt.Text != "0")
    //            {

    //                if (paidamount == 0)
    //                {
    //                    txtAmtPaid.Text = paidamount.ToString();
    //                }
    //                else
    //                {
    //                    txtAmtPaid.Text = (Convert.ToDecimal(txtPayAmt.Text) + paidamount).ToString();
    //                }
    //                if (amount > balance)
    //                {
    //                    dvFailure.Visible = true;
    //                    lblStatus.Text = "Pay amount cannot be greater than Balance.";
    //                    txtPayAmt.Text = "";
    //                    if (paidamount == 0)
    //                        txtAmtPaid.Text = "0.00";
    //                    else
    //                        txtAmtPaid.Text = paidamount.ToString();

    //                    txtBal.Text = balance.ToString();

    //                    return;
    //                }
    //                if (balance - amount > 0)
    //                {
    //                    txtBal.Text = (balance - amount).ToString();
    //                }
    //                else
    //                {
    //                    txtBal.Text = "0.00";
    //                }
    //            }
    //            else
    //            {
    //                txtPayAmt.Text = "";
    //                if (paidamount == 0)
    //                {
    //                    txtAmtPaid.Text = "0.00";
    //                }
    //                else
    //                {
    //                    txtAmtPaid.Text = paidamount.ToString();
    //                }
    //                HiddenField hdbal1 = (HiddenField)gr.FindControl("hdBalance");
    //                hdbal1.Value = balance.ToString();
    //            }
    //        }
           
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
}
