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
using ESaleEntity;
using System.Text;

public partial class Admin_Admin : System.Web.UI.MasterPage
{
    EDynamicLeftFrm objEDynamicLeftForm;
    StringBuilder SB;
    object obj;
    object objScreens;
    object objModules;
    DataSet dsScreens;
    DataSet dsModule;
    string DI;
    //CommonFunctions cf;
    protected void Page_Load(object sender, EventArgs e)
    {
        int storeid = Convert.ToInt32(Session["StoreID"]);
        string PageNameVar2="";
        if (Page.Request.Url.Segments[Page.Request.Url.Segments.Length - 1].ToString().Trim() != "Default.aspx")
        {
            //PageNameVar2 ="../"+Page.Request.Url.Segments[Page.Request.Url.Segments.Length - 2].ToString().Trim()+"/"+ Page.Request.Url.Segments[Page.Request.Url.Segments.Length - 1].ToString().Trim();
            PageNameVar2 =  Page.Request.Url.Segments[Page.Request.Url.Segments.Length - 1].ToString().Trim();
            hdcurrentpageurl.Value = PageNameVar2;
        }
        if (Convert.ToInt32(Session["LoginID"]) == 1)
        {
            //Hellouser.Visible = true;
            changerpassword.Visible = true;
            //img1.Visible = false;
        }
        else
        {
            //Hellouser.Visible = false;
            changerpassword.Visible = false;
            if (Convert.ToInt32(Session["StoreID"]) != 0)
            {
                string fileName = Convert.ToString(Session["StoreImage"]);
                if (fileName != "")
                {
                    //img1.Src = "~/images/StoreImages/" + fileName;
                    //img1.Visible = true;
                }
            }
            //else
                //img1.Visible = false;
        }
        if (!Page.IsPostBack)
        {
            GetUsers();
            if (!IsPostBack)
            {

                GenerateDynamicLeftForm();
            }
        }
        
        
    }
  


    public void GetUsers()
    {
        //int Loginid = Convert.ToInt32(Session["LOGINID"]);
        //Hellouser.Text = "| Hi " + Session["LOGINNAME"].ToString()+"|";
        //string Date = System.DateTime.Now.ToString("dd-MMM-yyyy") + " |";
        //lblToDayDate.InnerText = Date;



        string Date = System.DateTime.Now.ToString("dd-MMM-yyyy");
       // lblToDayDate.Text = Date;
        lbluser.Text = "Welcome " + Session["LOGINNAME"].ToString();
    }

    private void GenerateDynamicLeftForm()
    {

        objScreens = new object();
        objScreens = PGetAllScreens();
        dsScreens = (DataSet)objScreens;
        objModules = PGetAllModules();
        dsModule = new DataSet();
        dsModule = (DataSet)objModules;
        StringBuilder sb = new StringBuilder(string.Empty);
        int i = 1;

        //sb.Append("<ul id='menu'>");
        sb.Append("<ul class='x-navigation' id='menu' >");

        sb.Append(@"<li class='xn-logo'>
                                <a href='../Admin/Default.aspx'></a>
                                <a style='cursor:pointer;' class='x-navigation-control'></a>
                            </li>");
        foreach (DataRow drModule in dsModule.Tables[0].Rows)
        {
            string ModuleName = drModule["ModuleName"].ToString();
            bool btest = true;
            foreach (DataRow drRoleScreen in dsScreens.Tables[0].Rows)
            {
                if (ModuleName == drRoleScreen["ModuleName"].ToString())
                {
                    if (btest)
                    {
                        sb.Append("<li class='xn-openable'>");
                        sb.Append("<a href='#'> <span class='xn-text'>" + ModuleName + "</span></a> ");
                        sb.Append("<ul><li><a href='" + drRoleScreen["SCREENURL"] + "' >");
                        sb.Append(drRoleScreen["SCREENNAME"].ToString() + "<span></span></a></li>");
                        btest = false;
                        i++;
                    }
                    else
                    {
                        sb.Append("<li><a href='" + drRoleScreen["SCREENURL"] + "'>");
                        sb.Append(drRoleScreen["SCREENNAME"].ToString() + "<span></span></a></li>");
                    }
                }
            }
            sb.Append("</ul></li>");
        }
        sb.Append("</ul>");
        colapsableMenuBlock.InnerHtml = sb.ToString();
    }

    private object PGetAllScreens()
    {
        obj = new object();
        try
        {
            objEDynamicLeftForm = new EDynamicLeftFrm();
            //if (Session["LoginId"].ToString() == "1")
            // objEDynamicLeftForm.UserId = Session["LOGINTYPEID"].ToString();
            if (Session["LoginId"].ToString() == "1")
            {
                obj = objEDynamicLeftForm.EGetAllScreens(objEDynamicLeftForm);
            }
            else
            {

                objEDynamicLeftForm.RoleId = Session["RoleId"].ToString();
                //objEDynamicLeftForm.UserId = Session["LOGINTYPEID"].ToString();
                obj = objEDynamicLeftForm.EGetparticularUserScreens(objEDynamicLeftForm);
            }
            return obj;
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
        return obj;
    }

    private object PGetAllModules()
    {
        obj = new object();
        try
        {
            objEDynamicLeftForm = new EDynamicLeftFrm();
            //if (Session["LoginId"].ToString() == "1")
            //objEDynamicLeftForm.UserId = Session["LOGINTYPEID"].ToString();
            if (Session["LoginId"].ToString() == "1")
            {
                obj = objEDynamicLeftForm.EGetAllModules(objEDynamicLeftForm);
            }
            else
            {
                //objEDynamicLeftForm.UserId = Session["UserId"].ToString();

                objEDynamicLeftForm.RoleId = Session["RoleId"].ToString();
                obj = objEDynamicLeftForm.EGetparticularUserModules(objEDynamicLeftForm);
            }
            return obj;
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
        return obj;
    }
}
