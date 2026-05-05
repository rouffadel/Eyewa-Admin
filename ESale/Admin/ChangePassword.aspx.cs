using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Resources;
using ESaleEntity;
public partial class Admin_ChangePassword : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    MySql ms = new MySql();
    string cv = "";
    ResourceManager obj;
    CommonFunctions cf;
    static string pwd = "";
    EChangePasssword EChangepwdObj;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["LOGINID"] = dr["LOGINID"].ToString();
        //Session["LOGINTYPE"] = dr["LOGINTYPE"].ToString();
        //Session["LOGINNAME"] = dr["LOGINNAME"].ToString();

        try
        {
            lblStatus.Text = string.Empty;
            lblSuccess.Text = string.Empty;
            dvFailure.Visible = false;
            dvSuccess.Visible = false;
            if (!Page.IsPostBack)
            {
                if (Session["LOGINTYPE"] != null && Session["LOGINTYPE"].ToString().ToUpper() == "ADMIN")
                {
               
                   GetOldPassword();
                }
                else if (Session["LOGINTYPE"].ToString() != null && Session["LOGINTYPE"].ToString().ToUpper() == "DEALER")
                {
                  

                    GetOldPassword();
                }
                else if (Session["LOGINTYPE"].ToString() != null && Session["LOGINTYPE"].ToString().ToUpper() == "USERS")
                {
                 
                    GetOldPassword();
                }
                else
                { Response.Redirect("~/Login.aspx"); }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            cf = new CommonFunctions();
            cf.WriteError(ex.Message, ex.StackTrace.ToString(), Session["LOGINNAME"].ToString(), "Search");
            Response.Write("<!--" + ex.ToString() + "-->");
        }
        finally
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        EChangepwdObj = new EChangePasssword();
        DataSet ds = new DataSet();
        
        try
        {
            GetOldPassword();
            //if (txtOldPwd.Text == pwd)
            //{
                //if (ms.ExecuteNonQuery("Update Logins Set Password= '" + txtNewPwd.Text + "' where  LOGINID='" + Session["LOGINID"].ToString() + "'"))
                ////if(EChangepwdObj.EButtonSubmit())
                //{ 
                //    lblmsg.Text = "Password changed successfully !!!!"; 
                //}
                EChangepwdObj.LoginId = Session["LOGINID"].ToString();
                EChangepwdObj.Password = txtOldPwd.Text;
                EChangepwdObj.NewPassword = txtNewPwd.Text;
                ds = EChangepwdObj.EChangePassword();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "Password changed successfully")
                    {
                        dvSuccess.Visible = true;
                        lblSuccess.Text = "Password changed successfully !!!!";
                    }
                    else if (ds.Tables[0].Rows[0]["Status"].ToString() == "Invalid username or password")
                    {
                        dvFailure.Visible = true;
                        lblStatus.Text = "Invalid username or password !!!!";
                    }

                }
            //}
            else
                {
                    dvFailure.Visible = true;
                lblStatus.Text = "No Users !!!!";
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            cf = new CommonFunctions();
            cf.WriteError(ex.Message, ex.StackTrace.ToString(), Session["LOGINNAME"].ToString(), "Search");
            Response.Write("<!--" + ex.ToString() + "-->");
        }
    finally
    {

    }
    }

    private void GetOldPassword()
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        try
        {
            pwd = "";
            //string whereCondition = "and  LOGINID='" + Session["LOGINID"].ToString() + "'";
            //Hashtable ht = new Hashtable();
            //ht.Add("@WhereCondition", whereCondition);
            //ht.Add("@Transaction", "VERIFYLOGIN");
            DataSet ds = new DataSet();
            EChangepwdObj = new EChangePasssword();
            EChangepwdObj.LoginId = Session["LOGINID"].ToString();
            //ds = ms.ExecuteSP("SP_GetDataNew", ht);
            ds = EChangepwdObj.EGetOldPwd();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                pwd = dr["Password"].ToString();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            cf = new CommonFunctions();
            cf.WriteError(ex.Message, ex.StackTrace.ToString(), Session["LOGINNAME"].ToString(), "Search");
            Response.Write("<!--" + ex.ToString() + "-->");
        }
        finally
        {

        }
          
           
    }

  

  
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        lblSuccess.Text = string.Empty;
        dvFailure.Visible = false;
        dvSuccess.Visible = false;
        txtConfrmPwd.Text = "";
        txtNewPwd.Text = "";
        txtOldPwd.Text = "";
    }
}
