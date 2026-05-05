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

public partial class Admin_RoleScreenMapping : System.Web.UI.Page
{
    ERoleScreenMapping objERSM;
    DataSet dsforRSM;
    string LoginId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LOGINID"] != null)
        {
            LoginId = Session["LOGINID"].ToString();
        }
        else
        {
            Response.Redirect("~\\Login.aspx");
        }
        if (!IsPostBack)
        {
            FillRoles();
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            lblmsg.Text = "";
            lblStatus.Text = "";
        }
        pnlSelect.Visible = false;
    }
    public void FillRoles()
    {
        objERSM = new ERoleScreenMapping();
        dsforRSM = new DataSet();
        dsforRSM = objERSM.EFillRoles();
        if (dsforRSM.Tables[0].Rows.Count > 0)
        {
            ddlRoles.DataSource = dsforRSM;
            ddlRoles.DataTextField = "RoleName";
            ddlRoles.DataValueField = "RoleId";
            ddlRoles.DataBind();
            ddlRoles.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddlRoles.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblmsg.Text = "";
        lblStatus.Text = "";
        objERSM = new ERoleScreenMapping();
        if (ddlRoles.SelectedIndex != 0)
        {

            PFillRepScrenes();
            lblmsg.Text = "";
            // pnlSelect.Visible = true;
            pnlrepScreens.Visible = true;
            chkSelection.Checked = false;
        }
        else
        {
            lblmsg.Text = "";
            repScreens.DataSource = null;
            repScreens.DataBind();
            pnlSelect.Visible = false;
            pnlrepScreens.Visible = false;
        }
    }
    public void PFillRepScrenes()
    {
        try
        {
            repScreens.Visible = true;
            // pnlSelect.Visible = true;
            objERSM = new ERoleScreenMapping();
            dsforRSM = new DataSet();
            objERSM.RoleId = ddlRoles.SelectedValue;
            dsforRSM = objERSM.EFillRepScreens();
            repScreens.DataSource = dsforRSM;
            repScreens.DataBind();

            for (int i = 0; i < repScreens.Items.Count; i++)
            {
                Object objchkbxAdd = repScreens.Items[i].FindControl("chkBoxAdd");
                if (objchkbxAdd != null && objchkbxAdd is HtmlInputCheckBox)
                {
                    if (dsforRSM.Tables[0].Rows[i]["Add"].ToString() == "True")
                    {
                        HtmlInputCheckBox HIAdd = (HtmlInputCheckBox)objchkbxAdd;
                        HIAdd.Checked = true;
                    }
                }
                Object objchkbxView = repScreens.Items[i].FindControl("chkBoxView");
                if (objchkbxView != null && objchkbxView is HtmlInputCheckBox)
                {
                    if (dsforRSM.Tables[0].Rows[i]["View"].ToString() == "True")
                    {
                        HtmlInputCheckBox HIView = (HtmlInputCheckBox)objchkbxView;
                        HIView.Checked = true;
                    }
                }
                Object objchkbxDelete = repScreens.Items[i].FindControl("chkBoxDelete");
                if (objchkbxDelete != null && objchkbxDelete is HtmlInputCheckBox)
                {
                    if (dsforRSM.Tables[0].Rows[i]["Delete"].ToString() == "True")
                    {
                        HtmlInputCheckBox HIDelete = (HtmlInputCheckBox)objchkbxDelete;
                        HIDelete.Checked = true;
                    }

                }
                Object objchkbxAlert = repScreens.Items[i].FindControl("chkAlert");
                if (objchkbxAlert != null && objchkbxAlert is HtmlInputCheckBox)
                {
                    if (dsforRSM.Tables[0].Rows[i]["Edit"].ToString() == "True")
                    {
                        HtmlInputCheckBox HIAlert = (HtmlInputCheckBox)objchkbxAlert;
                        HIAlert.Checked = true;
                    }
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

    protected void chkSelection_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = sender as CheckBox;
        if (chkAll.Checked == true)
        {
            for (int i = 0; i < repScreens.Items.Count; i++)
            {

                Object objchkbxAdd = repScreens.Items[i].FindControl("chkBoxAdd");
                if (objchkbxAdd != null && objchkbxAdd is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIAdd = (HtmlInputCheckBox)objchkbxAdd;
                    HIAdd.Checked = true;
                }
                Object objchkbxView = repScreens.Items[i].FindControl("chkBoxView");
                if (objchkbxView != null && objchkbxView is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIView = (HtmlInputCheckBox)objchkbxView;
                    HIView.Checked = true;
                }
                Object objchkbxDelete = repScreens.Items[i].FindControl("chkBoxDelete");
                if (objchkbxDelete != null && objchkbxDelete is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIDelete = (HtmlInputCheckBox)objchkbxDelete;
                    HIDelete.Checked = true;
                }
                Object objchkbxAlert = repScreens.Items[i].FindControl("chkAlert");
                if (objchkbxAlert != null && objchkbxAlert is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIAlert = (HtmlInputCheckBox)objchkbxAlert;
                    HIAlert.Checked = true;
                }

            }

        }
        else
        {
            for (int i = 0; i < repScreens.Items.Count; i++)
            {
                Object objchkbxAdd = repScreens.Items[i].FindControl("chkBoxAdd");
                if (objchkbxAdd != null && objchkbxAdd is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIAdd = (HtmlInputCheckBox)objchkbxAdd;
                    HIAdd.Checked = false;
                }
                Object objchkbxView = repScreens.Items[i].FindControl("chkBoxView");
                if (objchkbxView != null && objchkbxView is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIView = (HtmlInputCheckBox)objchkbxView;
                    HIView.Checked = false;
                }
                Object objchkbxDelete = repScreens.Items[i].FindControl("chkBoxDelete");
                if (objchkbxDelete != null && objchkbxDelete is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIDelete = (HtmlInputCheckBox)objchkbxDelete;
                    HIDelete.Checked = false;
                }
                Object objchkbxAlert = repScreens.Items[i].FindControl("chkAlert");
                if (objchkbxAlert != null && objchkbxAlert is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIAlert = (HtmlInputCheckBox)objchkbxAlert;
                    HIAlert.Checked = false;
                }

            }
        }

    }



    protected void chkAdd_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = sender as CheckBox;
        if (chkAll.Checked == true)
        {
            for (int i = 0; i < repScreens.Items.Count; i++)
            {

                Object objchkbxAdd = repScreens.Items[i].FindControl("chkBoxAdd");
                if (objchkbxAdd != null && objchkbxAdd is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIAdd = (HtmlInputCheckBox)objchkbxAdd;
                    HIAdd.Checked = true;
                }

            }

        }
        else
        {
            for (int i = 0; i < repScreens.Items.Count; i++)
            {
                Object objchkbxAdd = repScreens.Items[i].FindControl("chkBoxAdd");
                if (objchkbxAdd != null && objchkbxAdd is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIAdd = (HtmlInputCheckBox)objchkbxAdd;
                    HIAdd.Checked = false;
                }
            }
        }

    }
    protected void chkView_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = sender as CheckBox;
        if (chkAll.Checked == true)
        {
            for (int i = 0; i < repScreens.Items.Count; i++)
            {

                Object objchkbxView = repScreens.Items[i].FindControl("chkBoxView");
                if (objchkbxView != null && objchkbxView is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIView = (HtmlInputCheckBox)objchkbxView;
                    HIView.Checked = true;
                }

            }

        }
        else
        {
            for (int i = 0; i < repScreens.Items.Count; i++)
            {
                Object objchkbxView = repScreens.Items[i].FindControl("chkBoxView");
                if (objchkbxView != null && objchkbxView is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIView = (HtmlInputCheckBox)objchkbxView;
                    HIView.Checked = false;
                }
            }
        }

    }
    protected void chkEdit_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = sender as CheckBox;
        if (chkAll.Checked == true)
        {
            for (int i = 0; i < repScreens.Items.Count; i++)
            {

                Object objchkbxDelete = repScreens.Items[i].FindControl("chkBoxDelete");
                if (objchkbxDelete != null && objchkbxDelete is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIDelete = (HtmlInputCheckBox)objchkbxDelete;
                    HIDelete.Checked = true;
                }

            }

        }
        else
        {
            for (int i = 0; i < repScreens.Items.Count; i++)
            {
                Object objchkbxDelete = repScreens.Items[i].FindControl("chkBoxDelete");
                if (objchkbxDelete != null && objchkbxDelete is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIDelete = (HtmlInputCheckBox)objchkbxDelete;
                    HIDelete.Checked = false;
                }
            }
        }

    }
    protected void chkDelete_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = sender as CheckBox;
        if (chkAll.Checked == true)
        {
            for (int i = 0; i < repScreens.Items.Count; i++)
            {

                Object objchkbxAlert = repScreens.Items[i].FindControl("chkAlert");
                if (objchkbxAlert != null && objchkbxAlert is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIAlert = (HtmlInputCheckBox)objchkbxAlert;
                    HIAlert.Checked = true;
                }

            }

        }
        else
        {
            for (int i = 0; i < repScreens.Items.Count; i++)
            {
                Object objchkbxAlert = repScreens.Items[i].FindControl("chkAlert");
                if (objchkbxAlert != null && objchkbxAlert is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIAlert = (HtmlInputCheckBox)objchkbxAlert;
                    HIAlert.Checked = false;
                }
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblmsg.Text = "";
        lblStatus.Text = "";
        objERSM = new ERoleScreenMapping();
        dsforRSM = new DataSet();
        lblmsg.Text = "";

        try
        {
            objERSM.RoleId = ddlRoles.SelectedValue;
            objERSM.LoginSessionId = Convert.ToInt32(Session["LOGINID"]);
            string str = "";
            for (int i = 0; i < repScreens.Items.Count; i++)
            {

                Object objchkbxAdd = repScreens.Items[i].FindControl("chkBoxAdd");
                if (objchkbxAdd != null && objchkbxAdd is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIAdd = (HtmlInputCheckBox)objchkbxAdd;
                    str += HIAdd.Value + "," + HIAdd.Checked + ",";
                }
                Object objchkbxDelete = repScreens.Items[i].FindControl("chkBoxDelete");
                if (objchkbxDelete != null && objchkbxDelete is HtmlInputCheckBox)
                {

                    HtmlInputCheckBox HIDelete = (HtmlInputCheckBox)objchkbxDelete;
                    str += HIDelete.Checked + ",";
                }

                Object objchkbxView = repScreens.Items[i].FindControl("chkBoxView");
                if (objchkbxView != null && objchkbxView is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIView = (HtmlInputCheckBox)objchkbxView;

                    str += HIView.Checked + ",";
                }
                Object objchkbxAlert = repScreens.Items[i].FindControl("chkAlert");
                if (objchkbxAlert != null && objchkbxAlert is HtmlInputCheckBox)
                {
                    HtmlInputCheckBox HIAlert = (HtmlInputCheckBox)objchkbxAlert;

                    str += HIAlert.Checked + ",";
                }

            }
            str = str.Substring(0, str.Length - 1);
            objERSM.ScreenDetails = str;
            dsforRSM = objERSM.EInsertRoleScreenMapping();
            if (dsforRSM.Tables[0].Rows[0]["Status"].ToString() == "SUCCESS")
            {
                ddlRoles.SelectedIndex = 0;
                repScreens.DataSource = null;
                repScreens.DataBind();
                dvSuccess.Visible = true;
                lblmsg.Text = "Transaction Successfull";



            }
            else if (dsforRSM.Tables[0].Rows[0]["Status"].ToString() == "Error")
            {

                ddlRoles.SelectedIndex = 0;
                repScreens.DataSource = null;
                repScreens.DataBind();
                dvFailure.Visible = true;
                lblStatus.Text = "Error in the Transaction.";

            }
            pnlSelect.Visible = false;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblmsg.Text = string.Empty;
        //ShowTabs(1);
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        pnlrepScreens.Visible = false;
        ddlRoles.SelectedIndex = -1;
        // ShowTabs(1);
        //lnkAdd.Visible = true;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        lblmsg.Text = "";
        lblStatus.Text = "";
        ddlRoles.SelectedIndex = 0;
        repScreens.Visible = false;
        pnlSelect.Visible = false;

        for (int i = 0; i < repScreens.Items.Count; i++)
        {

            Object objchkbxView = repScreens.Items[i].FindControl("chkBoxView");
            if (objchkbxView != null && objchkbxView is HtmlInputCheckBox)
            {
                HtmlInputCheckBox HIView = (HtmlInputCheckBox)objchkbxView;
                HIView.Checked = false;
            }
            Object objchkbxView1 = repScreens.Items[i].FindControl("chkBoxAdd");
            if (objchkbxView1 != null && objchkbxView1 is HtmlInputCheckBox)
            {
                HtmlInputCheckBox HIView1 = (HtmlInputCheckBox)objchkbxView1;
                HIView1.Checked = false;
            }
            Object objchkbxView2 = repScreens.Items[i].FindControl("chkAlert");
            if (objchkbxView2 != null && objchkbxView2 is HtmlInputCheckBox)
            {
                HtmlInputCheckBox HIView2 = (HtmlInputCheckBox)objchkbxView2;
                HIView2.Checked = false;
            }
            Object objchkbxView3 = repScreens.Items[i].FindControl("chkBoxDelete");
            if (objchkbxView3 != null && objchkbxView3 is HtmlInputCheckBox)
            {
                HtmlInputCheckBox HIView3 = (HtmlInputCheckBox)objchkbxView3;
                HIView3.Checked = false;
            }
        }
    }

}
