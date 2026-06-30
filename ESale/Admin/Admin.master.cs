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

        sb.Append("<div class='sidebar-header'>");
        sb.Append("<div><img src='../assets/images/logo-icon.png' class='logo-icon' alt='logo icon'></div>");
        sb.Append("<div><h4 class='logo-text'>Eyewa ERP</h4></div>");
        sb.Append("<div class='toggle-icon ms-auto'><i class='bx bx-arrow-back'></i></div>");
        sb.Append("</div>");

        sb.Append("<ul class='metismenu' id='menu'>");
        
        foreach (DataRow drModule in dsModule.Tables[0].Rows)
        {
            string ModuleName = drModule["ModuleName"].ToString();
            string iconClass = GetModuleIcon(ModuleName);
            
            bool hasScreens = false;
            StringBuilder screensSb = new StringBuilder();
            
            foreach (DataRow drRoleScreen in dsScreens.Tables[0].Rows)
            {
                if (ModuleName == drRoleScreen["ModuleName"].ToString())
                {
                    hasScreens = true;
                    screensSb.Append("<li><a href='" + drRoleScreen["SCREENURL"] + "'><i class='bx bx-radio-circle'></i>" + drRoleScreen["SCREENNAME"].ToString() + "</a></li>");
                }
            }
            
            if (hasScreens)
            {
                sb.Append("<li>");
                sb.Append("<a href='javascript:;' class='has-arrow'>");
                sb.Append("<div class='parent-icon'><i class='" + iconClass + "'></i></div>");
                sb.Append("<div class='menu-title'>" + ModuleName + "</div></a>");
                sb.Append("<ul>" + screensSb.ToString() + "</ul>");
                sb.Append("</li>");
            }
            else
            {
                sb.Append("<li>");
                sb.Append("<a href='javascript:;'>");
                sb.Append("<div class='parent-icon'><i class='" + iconClass + "'></i></div>");
                sb.Append("<div class='menu-title'>" + ModuleName + "</div></a>");
                sb.Append("</li>");
            }
        }
        sb.Append("</ul>");
        colapsableMenuBlock.InnerHtml = sb.ToString();
    }

    private string GetModuleIcon(string moduleName)
    {
        moduleName = moduleName.ToLower();
        if (moduleName.Contains("dashboard")) return "bx bx-home-alt";
        if (moduleName.Contains("application")) return "bx bx-category";
        if (moduleName.Contains("widget")) return "bx bx-cookie";
        if (moduleName.Contains("ecommerce") || moduleName.Contains("sale")) return "bx bx-cart";
        if (moduleName.Contains("component")) return "bx bx-bookmark-heart";
        if (moduleName.Contains("content")) return "bx bx-repeat";
        if (moduleName.Contains("icon")) return "bx bx-donate-blood";
        if (moduleName.Contains("form")) return "bx bx-message-square-edit";
        if (moduleName.Contains("table")) return "bx bx-grid-alt";
        if (moduleName.Contains("auth") || moduleName.Contains("security")) return "bx bx-lock";
        if (moduleName.Contains("user") || moduleName.Contains("profile")) return "bx bx-user-circle";
        if (moduleName.Contains("chart")) return "bx bx-line-chart";
        if (moduleName.Contains("map")) return "bx bx-map-alt";
        if (moduleName.Contains("report")) return "bx bx-file";
        if (moduleName.Contains("setting")) return "bx bx-cog";
        
        return "bx bx-grid-alt"; // Default icon
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
