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
public partial class Screens_OpeningCounter : System.Web.UI.Page
{
    DataSet ds;
    Counter ECounter;
    string Result;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
        if (Page.IsPostBack == false)
        {
            FillStore();
            GetOpeningValue();
        }
    }

    private void GetOpeningValue()
    {
        try
        {
            ds = new DataSet();
            ECounter = new Counter();
            if (ddlStore.SelectedValue != "0")
                ECounter.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            else
                ECounter.StoreID = 0;
            ds = ECounter.GetOpeningVal();
            float openingvalue = 0;
            float finalclosingvalue = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[0].Rows[0]["FinalClosingValue"]) != "")
                {
                    openingvalue = Convert.ToSingle(ds.Tables[0].Rows[0]["OpeningValue"]);
                    finalclosingvalue=Convert.ToSingle(ds.Tables[0].Rows[0]["FinalClosingValue"]);
                    if (openingvalue == 0)
                    {
                        imgSave.Visible = true;
                        txtOpeningCounterValue.Enabled = true;
                    }
                    else if (openingvalue != 0 && finalclosingvalue != 0)
                    {
                        imgSave.Visible = true;
                        txtOpeningCounterValue.Text = finalclosingvalue.ToString();
                        txtOpeningCounterValue.Enabled = false;
                    }
                    else
                    {
                        imgSave.Visible = true;
                        txtOpeningCounterValue.Text = openingvalue.ToString();
                        txtOpeningCounterValue.Enabled = false;
                    }
                }
                else
                {
                    openingvalue = Convert.ToSingle(ds.Tables[0].Rows[0]["OpeningValue"]);
                    imgSave.Visible = false;
                    txtOpeningCounterValue.Text = openingvalue.ToString();
                    txtOpeningCounterValue.Enabled = false;
                }
            }
            else
            {
                imgSave.Visible = true;
                txtOpeningCounterValue.Enabled = true;
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    private void FillStore()
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
            if (ddlStore.SelectedValue != "0")
                ECounter.StoreID = Convert.ToInt32(ddlStore.SelectedValue);
            else
                ECounter.StoreID = 0;
            ECounter.OpeningDate = txtDate.Text;
            if (txtOpeningCounterValue.Text != "")
                ECounter.OpeningValue = Convert.ToSingle(txtOpeningCounterValue.Text);
            else
                ECounter.OpeningValue = 0;
            ECounter.OpenTime = System.DateTime.Now.ToString("h:mm:ss tt");
            int loginid = Convert.ToInt32(Session["LOGINID"]);
            if (loginid != 0)
                ECounter.LoginID = loginid;
            else
                ECounter.LoginID = 0;
            Result = ECounter.SaveOpeningCounter();
            if (Result != "")
                lblStatus.Text = Result;
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}
