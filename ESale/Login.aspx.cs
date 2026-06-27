using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Collections;
using ESaleEntity;
public partial class Login : System.Web.UI.Page
{
    ELogin Eobj;
    DataSet ds;
    MySql objSQLHelper = new MySql();
    protected void Page_Load(object sender, EventArgs e)
    {
        //lblmsg.Text = string.Empty;
        //lblmsg.CssClass = "";
        if (!IsPostBack)
        {
            txtLogin.Focus();
        }

    }
    protected void lnkforgotpassword_Click(object sender, EventArgs e)
    {
        try
        {
            pnlgetpassword.Visible = true;
            pnlLogin.Visible = false;
            txtpassword.Enabled = false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            Response.Write("<!--" + ex.ToString() + "-->");
        }
    }

    protected void imgbtnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            loggingin();
            //pnlForgotPassword.Visible = false;
            pnlLogin.Visible = true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            // cf = new CommonFunctions();
            // cf.WriteError(ex.Message, ex.StackTrace.ToString(), Session["LOGINNAME"].ToString(), "Search");
            Response.Write("<!--" + ex.ToString() + "-->");
        }
        finally
        {

        }
    }
    private void loggingin()
    {
        try
        {
            string strWhereCond = string.Empty;
            strWhereCond = " and L.LoginName='" + txtLogin.Text.Trim() + "' and L.password='" + txtpassword.Text.Trim() + "' ";
            //Hashtable ht = new Hashtable();

            //ht.Add("@WhereCondition", strWhereCond);
            //ht.Add("@Transaction", "VERIFYLOGIN");

            ds = new DataSet();
            Eobj = new ELogin();
            Eobj.LoginName = txtLogin.Text.Trim();
            Eobj.Password = txtpassword.Text.Trim();
            ds = Eobj.EVerifyUserLogin();
            DataTable dt;
            int storeid = 0;
            //ds = ms.ExecuteSP("SP_GetDataNew", ht);
            if (ds.Tables.Count > 0)
            {

                //dt = ds.Tables[0];
                //if (dt.Rows.Count > 0)
                //{
                //    Session["StoreID"] = dt.Rows[0]["StoreID"];
                //    storeid = Convert.ToInt32(dt.Rows[0]["StoreID"]);
                //}
                //else
                //    Session["StoreID"] = null;

                //DataRow dr = ds.Tables[0].Rows[0];
                //if (dr["LOGINTYPE"].ToString().ToUpper() == "ADMIN")
                //{
                DataSet DsUser = new DataSet();
                DsUser = Eobj.EVerifyUserLogin();
                if (DsUser.Tables[0].Rows.Count > 0)
                {
                    DataRow druser = DsUser.Tables[0].Rows[0];
                    if (druser["LOGINID"].ToString() == '1'.ToString())
                    {
                        Session["LOGINID"] = druser["LOGINID"].ToString();
                        //Session["LOGINTYPE"] = druser["LOGINTYPE"].ToString();
                        //Session["LOGINTYPEID"] = druser["LOGINTYPEID"].ToString();
                        Session["LOGINNAME"] = druser["LOGINNAME"].ToString();
                        Session["RoleId"] = druser["RoleId"].ToString();
                        Session["Password"] = druser["Password"].ToString();
                        Response.Redirect("~/Admin/Default.aspx", false);
                    }
                    else if (druser["LOGINID"].ToString() != '1'.ToString())
                    {
                        //Session["LOGINID"] = dr["LOGINID"].ToString();
                        //Session["LOGINTYPE"] = dr["LOGINTYPE"].ToString();
                        //Session["LOGINTYPEID"] = dr["LOGINTYPEID"].ToString();
                        //Session["LOGINNAME"] = dr["LOGINNAME"].ToString();

                        Session["LOGINID"] = druser["LOGINID"].ToString();
                        //Session["LOGINTYPE"] = druser["LOGINTYPE"].ToString();
                        //Session["LOGINTYPEID"] = druser["LOGINTYPEID"].ToString();
                        Session["LOGINNAME"] = druser["LOGINNAME"].ToString();
                        Session["Password"] = druser["Password"].ToString();
                        Session["RoleId"] = druser["RoleId"].ToString();
                        Session["StoreID"] = druser["StoreID"].ToString();
                        Session["StoreImage"] = druser["FileName"].ToString();
                        //Session["LinkID"] = druser["LinkID"].ToString();
                        Response.Redirect("~/Admin/Default.aspx", false);

                    }

                    else
                    {
                       // lblmsg.Text = "Invalid Login. Please contact Adminstrator";
                    }
                }
                //else
                //{
                // //Session["LOGINID"] = dr["LOGINID"].ToString();
                // //Session["LOGINTYPE"] = dr["LOGINTYPE"].ToString();
                // //Session["LOGINTYPEID"] = dr["LOGINTYPEID"].ToString();
                // //Session["LOGINNAME"] = dr["LOGINNAME"].ToString();
                // //Response.Redirect("~/Admin/Default.aspx", false);
                //}
                //}

            }
           // else
                //lblmsg.Text = "Invalid User ID/ Password. Please try again.";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Login.aspx");
        }
        catch (Exception ex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            //cf = new CommonFunctions();
            //cf.WriteError(ex.Message, ex.StackTrace.ToString(), Session["LOGINNAME"].ToString(), "Search");
            Response.Write("<!--" + ex.ToString() + "-->");
        }
        finally
        {

        }

    }

    protected void Close_Click(object sender, EventArgs e)
    {
        try
        {
            pnlgetpassword.Visible = false;
            pnlLogin.Visible = true;
            txtpassword.Enabled = true;
        }
        catch (Exception ex)
        {
            Response.Write("<!--" + ex.ToString() + "-->");
        }
    }


    protected void imgbtnLogin1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            loggingin();
        }
        catch (Exception ex)
        {
           // lblmsg.Text = ex.Message;
        }
        finally
        {

        }
    }
    //protected void lnkforgotpassword_Click1(object sender, EventArgs e)
    //{
    //    pnlForgotPassword.Visible = true;
    //    pnlLogin.Visible = false;
    //}
    //protected void btnGetPassword_Click(object sender, EventArgs e)
    //{
    //    Eobj = new ELogin();
    //    ds = new DataSet();
    //    try
    //    {
    //        Eobj.LoginName = txtUserName.Text;
    //        Eobj.MobileNo = txtMobileNo.Text;
    //        ds = Eobj.EGetPassword();
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            GlobalDeclarations gc = new GlobalDeclarations();
    //            MailMessage mm = new MailMessage();
    //            string LoginName = "", password = "", MobileNo = "", ContactNo = "";
    //            LoginName = ds.Tables[0].Rows[0]["LoginName"].ToString();
    //            password = ds.Tables[0].Rows[0]["Password"].ToString();
    //            MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
    //            ContactNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
    //            mm.To.Add(ds.Tables[0].Rows[0]["Email"].ToString());

    //            //
    //            gc.SmsFunction(ContactNo, "Hi User:" + " " + LoginName + " On The Request Of the User the password is sent to your mobile.The password for your account:" + password);
    //            lblforgotmsg.CssClass = "SuccessMsg";
    //            //lblmsg.Text = "Message Sent Successfully!!!";
    //            lblforgotmsg.Text = "Password is sent to your Mobile";

    //            //
    //            //mm.Subject = "Mail From RJOnline Services";
    //            //mm.Body = "Hai   User:" + LoginName + "\n On The Request Of the User The Password is Maild to the User MailId.\n This is the password for your account:" + password;
    //            //if (gc.SendEmail(mm) == "Success")
    //            //{

    //            //    //lblgotpwd.Text = "Password is sent to your Email ";
    //            //    //pnlForgotPassword.Visible = false;
    //            //    pnlLogin.Visible = true;
    //            //    lblmsg.CssClass = "SuccessMsg";
    //            //    lblmsg.Text = "Password is sent to your Email ";
    //            //}
    //            //else
    //            //{
    //            //    lblmsg.CssClass = "ErrorMsg";
    //            //    lblmsg.Text = "Paasword cannot be retrieved";
    //            //}


                
    //        }
    //        else
    //        {
    //            lblforgotmsg.Visible = true;
    //            lblforgotmsg.CssClass = "lblBrandErrormsg";
    //            lblforgotmsg.Text = "No such details exist...!";
    //        }
    //        txtUserName.Text = string.Empty;
    //        txtMobileNo.Text = string.Empty;
            
    //    }
    //    catch (Exception ex)
    //    {
    //        lblforgotmsg.CssClass = "lblBrandErrormsg";
    //        lblforgotmsg.Text = ex.Message;
    //    }
    //}

    public string SendEmail(MailMessage mm)
    {
        string EmailFrom, EmailPassword, EmailSMTP;
        int EmailPort;
        
        try
        {
            objSQLHelper = new MySql();
            Hashtable ht = new Hashtable();
            ht.Add("@Transaction", "Emaildetails");
            string wherecondition = " Currentprefrence=1";
            ht.Add("@wherecondition", wherecondition);
            DataSet ds = new DataSet();

            ds = objSQLHelper.ExecuteSP("SP_GetDataNew", ht);
            EmailFrom = ds.Tables[0].Rows[0]["EmailFrom"].ToString();
            EmailPassword = ds.Tables[0].Rows[0]["EmailPassword"].ToString();
            EmailSMTP = ds.Tables[0].Rows[0]["EmailSMTP"].ToString();
            EmailPort = Convert.ToInt32(ds.Tables[0].Rows[0]["EmailPort"].ToString());
            mm.From = new MailAddress(EmailFrom);
            mm.IsBodyHtml = false;
            mm.Priority = MailPriority.High;
            SmtpClient sC = new SmtpClient();
            sC.Host = EmailSMTP;
            // SmtpDeliveryMethod sdm  = new SmtpDeliveryMethod();              
            sC.DeliveryMethod = SmtpDeliveryMethod.Network;
            sC.UseDefaultCredentials = false;
            sC.EnableSsl = false;
            sC.Port = EmailPort;
            sC.Credentials = new NetworkCredential(EmailFrom, EmailPassword);
            sC.Send(mm);
            return "Success";
        }
        catch (Exception ex)
        { throw (ex); }

    }
    //protected void lnkforgotpassword_Click1(object sender, EventArgs e)
    //{
    //    divGetPassword.Attributes["style"] = "display:block";
    //}
    //protected void btnGetPwdCancel_Click(object sender, EventArgs e)
    //{
    //    divGetPassword.Attributes["style"] = "display:none";
    //    txtUserName.Text = "";
    //    txtMobileNo.Text = "";
    //    lblforgotmsg.Text = "";
    //    lblforgotmsg.CssClass = "";
    //}
}
