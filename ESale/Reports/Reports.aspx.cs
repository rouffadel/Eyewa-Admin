using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
public partial class Reports_Reports : System.Web.UI.Page
{

    string WhereCondition = string.Empty;
    string Transaction = string.Empty;
    string SpName = string.Empty;
    string Data = string.Empty;
    string Location = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{

        if (Session["LoginId"] == null)
        {
            Response.Redirect("~/Login.aspx", false);

        }
        else
        {
            //lblGeneratedDate.Text += getIndinStandardTime().ToString("dd-MM-yyyy");
            //lblGeneratedBy.Text += Session["LOGINNAME"].ToString();
        }
        if (Request.QueryString["PageName"] != null && Request.QueryString["PageName"].ToString().Trim() != null)
        {
            lblReportName.Text = Request.QueryString["PageName"].ToString().Trim();
            //if (Request.QueryString["lblText"].ToString() != null)
            //{
            //    lblText.Text = Request.QueryString["lblText"].ToString().Trim();
            //    if (lblText.Text != "")
            //    {
            //        lblText.Visible = true;
            //    }
            //}
            //if (Session["Calculate"] != null && (lblReportName.Text=="Supplier Due Date Report"||lblReportName.Text=="Customer Due Date Report"||lblReportName.Text=="Payable Report"||lblReportName.Text=="Receivable Report"||lblReportName.Text=="Available Stock Report"))
            //{
            //    string str = (string)Session["Calculate"];
            //    lblText.Text = str;
            //    lblText.Visible = true;
            //}
            
            Control ctrl=null;
            if(Session["ctrl"]!=null)
            {
               ctrl = (Control)Session["ctrl"];
            }
            Control ctrl1=null;
            Control ctrl2 = null;
            Control ctrl3 = null;
            if (Session["ctrl3"] != null)
            {
                ctrl3 = (Control)Session["ctrl3"];
            }
            if (Session["ctrl1"] != null)
            {
                ctrl1 = (Control)Session["ctrl1"];
            }
            if (Session["ctrl2"] != null)
            {
                ctrl2 = (Control)Session["ctrl2"];
            }
            StringWriter stringWrite = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            if (ctrl is WebControl)
            {
                Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
            }
            if (ctrl3 != null)
            {
                if (ctrl3 is WebControl)
                {
                    Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
                }
            }
            if (ctrl1 != null)
            {
                if (ctrl1 is WebControl)
                {
                    Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
                }
            }
            if (ctrl2 != null)
            {
                if (ctrl2 is WebControl)
                {
                    Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
                }
            }
            Page pg = new Page();
            pg.EnableEventValidation = false;
            HtmlForm frm = new HtmlForm();
            pg.Controls.Add(frm);
            
            frm.Attributes.Add("runat", "server");
            frm.Controls.Add(ctrl);
            //frm.Controls.Add("<br/>");
            if (ctrl3 != null)
            {
                frm.Controls.Add(new Literal() { ID = "br3", Text = "<br/>" });
                frm.Controls.Add(ctrl3);
            }
            if (ctrl1 != null)
            {
                frm.Controls.Add(new Literal() { ID = "br2", Text = "<br/>" });
                frm.Controls.Add(ctrl1);
            }
            if (ctrl2 != null)
            {
                frm.Controls.Add(new Literal() { ID = "br1", Text = "<br/>" });
                frm.Controls.Add(ctrl2);
            }
            pg.DesignerInitialize();
            pg.RenderControl(htmlWrite);
            string strHTML = stringWrite.ToString();
            tblData.InnerHtml = strHTML;
            Session["ctrl1"] = null;
            Session["ctrl2"] = null;
            Session["ctrl3"] = null;
            //Print.Visible=false;
            ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "printdoc()", true);
            //Print.Visible = true;
        }
    }
    public DateTime getIndinStandardTime()
    {
        TimeZoneInfo India_Zone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, India_Zone);

    }

}
