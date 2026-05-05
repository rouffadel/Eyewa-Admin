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
public partial class Screens_ClosingCounter : System.Web.UI.Page
{
    DataSet ds;
    Counter ECounter;
    string Result;
    DataTable dt;
    int l = 0;
    float totalbalance = 0;
    float totalcredit=0, totaldebit = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        int count = Convert.ToInt32(Session["Count"]);
        //if (count == 1)
        //{
        //    Session["Count"] = null;
        //}
        //else
        //{
        //    string url = "../Login.aspx";
        //    Response.Write("<script>");
        //    Response.Write("parent.location.replace('" + url + "');");
        //    Response.Write("</script>");
        //}
        if (Page.IsPostBack == false)
        {
            txtDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            GetStore();
            LoadData();
            GetUser();
        }
    }

    private void GetUser()
    {
        try
        {
            ds = new DataSet();
            ECounter = new Counter();
            if (ddlStore.SelectedValue != "0")
                ECounter.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            else
                ECounter.StoreID = 0;
            ECounter.LoginID = Convert.ToInt32(Session["LOGINID"]);
            ds = ECounter.FillUsers();
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlUser.DataSource = ds.Tables[0];
                ddlUser.DataValueField = "UserID";
                ddlUser.DataTextField = "UserName";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            else
            {
                ddlUser.Items.Insert(0, new ListItem("--Any--", "0"));
            }
            ddlUser.Enabled = true;
            if (Convert.ToInt32(Session["LOGINID"]) != 1 && ds.Tables[0].Rows.Count == 1)
            {
                ddlUser.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["UserID"]);
               
                ddlUser.Enabled = false;
            }
            ddlUser.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["UserID"]);
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void LoadData()
    {
        try
        {
            ds = new DataSet();
            ECounter = new Counter();
            if (ddlStore.SelectedValue != "0")
                ECounter.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            else
                ECounter.StoreID = 0;
            ECounter.LoginID = Convert.ToInt32(Session["LOGINID"]);
            ds = ECounter.GetClosingCounterGrid();
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                DataRow dr = ds.Tables[1].NewRow();
                dr["Reason"] = ds.Tables[0].Rows[0]["Reason"];
                dr["Credit"] = ds.Tables[0].Rows[0]["Credit"];
                dr["Debit"] = ds.Tables[0].Rows[0]["Debit"];
                dr["CounterID"] = ds.Tables[0].Rows[0]["CounterID"];
               // ds.Tables[1].Rows.Add(dr);
                ds.Tables[1].Rows.InsertAt(dr, 0);
                dt = ds.Tables[1];
                l = 0;
                totalbalance = 0;
                totalcredit = 0;
                totaldebit = 0;
                gvcounter.DataSource = dt;
                gvcounter.DataBind();
                txtClosingCounterValue.Text = totalbalance.ToString();
                txtClosingCounterBalance.Text = totalbalance.ToString();
                lblStatus.Text = "";
            }
            else
            {
                gvcounter.DataSource = null;
                gvcounter.DataBind();
                lblStatus.Text = "No Sales Done.";
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void GetStore()
    {
        try
        {
            ds = new DataSet();
            ECounter = new Counter();
            ECounter.LoginID = Convert.ToInt32(Session["LOGINID"]);
            ds = ECounter.FillStore();
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                ddlStore.DataSource = ds.Tables[0];
                ddlStore.DataValueField = "StoreID";
                ddlStore.DataTextField = "StoreName";
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
            ddlStore.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["StoreID"]);
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ECounter = new Counter();
            Result = string.Empty;
            float totalsales = 0;
            float totalexpenses = 0;
            int counterid = 0;
            foreach (GridViewRow gr in gvcounter.Rows)
            {
                HiddenField hd = (HiddenField)gr.FindControl("HDCounterID");
                if (hd.Value !="0")
                    counterid = Convert.ToInt32(hd.Value);
                else 
                {
                    Label lblsales = (Label)gr.FindControl("lblCredit");
                    Label lblexp = (Label)gr.FindControl("lblDebit");
                    if(lblsales.Text!="0.00")
                        totalsales +=Convert.ToSingle(lblsales.Text);
                    if (lblexp.Text != "0.00")
                        totalexpenses += Convert.ToSingle(lblexp.Text);
                }
            }
            ECounter.CounterID = counterid;
            ECounter.TotalSales = totalsales;
            ECounter.TotalExpenses = totalexpenses;
            ECounter.ClosingValue = Convert.ToSingle(txtClosingCounterValue.Text);
            if (txtCashWithdrawal.Text != "")
                ECounter.WithDrawAmount = Convert.ToSingle(txtCashWithdrawal.Text);
            else
                ECounter.WithDrawAmount = 0;
            ECounter.FinalAmount = Convert.ToSingle(txtClosingCounterBalance.Text);
            ECounter.LoginID = Convert.ToInt32(Session["LOGINID"]);
            Result = ECounter.SaveClosingCounter();
            if (Result != "")
            {
                lblStatus.Text = Result;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void gvcounter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcredit = (Label)e.Row.FindControl("lblCredit");
            Label lbldebit = (Label)e.Row.FindControl("lblDebit");
            Label lblbalance1 = (Label)e.Row.FindControl("lblBalance");
            float credit = 0, debit = 0, balance = 0;
            if (Convert.ToInt32(dt.Rows[l]["CounterID"]) != 0)
            {
                lblbalance1.Text = lblcredit.Text;
                totalcredit += Convert.ToSingle(dt.Rows[l]["Credit"]);
                totalbalance += Convert.ToSingle(dt.Rows[l]["Credit"]);
            }
            if (Convert.ToInt32(dt.Rows[l]["CounterID"]) == 0)
            {
                if (Convert.ToSingle(dt.Rows[l]["Credit"]) != 0)
                {
                    totalbalance += Convert.ToSingle(dt.Rows[l]["Credit"]);
                    lblbalance1.Text = totalbalance.ToString();
                    totalcredit += Convert.ToSingle(dt.Rows[l]["Credit"]);
                }
                else if (Convert.ToSingle(dt.Rows[l]["Debit"]) != 0)
                {
                    totalbalance -= Convert.ToSingle(dt.Rows[l]["Debit"]);
                    lblbalance1.Text = totalbalance.ToString();
                    totaldebit += Convert.ToSingle(dt.Rows[l]["Debit"]);
                }
                else
                {
                    lblbalance1.Text = totalbalance.ToString();
                }
            }
            l++;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotal = (Label)e.Row.FindControl("lblTotalCredit");
            lblTotal.Text = totalcredit.ToString();
            lblTotal = (Label)e.Row.FindControl("lblTotalDebit");
            lblTotal.Text = totaldebit.ToString();
            lblTotal = (Label)e.Row.FindControl("lblTotalBal");
            lblTotal.Text = totalbalance.ToString();
            
        }
        
    }
}
