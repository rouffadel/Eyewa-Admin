using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using ESaleEntity;
using System.Text;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

public partial class Admin_Default : System.Web.UI.Page
{
    ELogin Eobj;
    DataSet ds;
    MySql objSQLHelper = new MySql();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (!IsPostBack)
            {
                ddlmonth.SelectedValue = System.DateTime.Now.Month.ToString();
                ddlyear.SelectedValue = System.DateTime.Now.Year.ToString();
                if (GetDashboardCredentials())
                {
                    FillStore();
                    DashBoardSalesdetails();
                    DashBoardSalesToatldetails();
                    GetInvoiceBarGraph();

                    GetInvoicePieGraph();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "HideDivs()", true);
                }
            }
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy");

            //lblToDayDate.InnerText = Date;
            //Hellouser.Text = "Welcome " + Session["LOGINNAME"].ToString();

            int storeid = Convert.ToInt32(Session["StoreID"]);
            string fileName = Convert.ToString(Session["StoreImage"]);
            if (Convert.ToString(Session["LoginID"]) == "1")
            {
                imgStore.ImageUrl = "~/images/cityvision.png";
                //Image1.ImageUrl = "~/images/gulfvision.png";
                Image2.ImageUrl = "~/images/naimat al-basar.png";
                imgStore.Visible = true;
                Image1.Visible = true;
                Image2.Visible = true;
            }
            if (Convert.ToInt32(Session["LoginID"]) != 1)
            {
                if (storeid == 0)
                {
                    imgStore.ImageUrl = "~/images/cityvision.png";
                    //Image1.ImageUrl = "~/images/gulfvision.png";
                    Image2.ImageUrl = "~/images/naimat al-basar.png";
                }
                else
                {
                    if (fileName != "")
                    {
                        imgStore.ImageUrl = "~/images/StoreImages/" + fileName;
                        imgStore.Visible = true;
                        Image1.Visible = false;
                        Image2.Visible = false;
                    }
                    else
                    {
                        imgStore.ImageUrl = "~/images/cityvision.png";
                        //Image1.ImageUrl = "~/images/gulfvision.png";
                        Image2.ImageUrl = "~/images/naimat al-basar.png";
                    }

                }
            }

            if (!Page.IsPostBack)
            {
                if (Session["LOGINID"] == null)
                {
                    Response.Redirect("~/Login.aspx");

                }
            }
            //int StoreID = Convert.ToInt32(Session["StoreID"]);
            //if (StoreID != 0)
            //    frmMain.Attributes["src"] = "../Screens/OpeningCounter.aspx";
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //MySql objMySql = new MySql();
        //Hashtable ht = new Hashtable();        
        //DataSet ds = new DataSet();
        //try
        //{
        //    if (Session["LOGINID"] == null)
        //    {

        //        Response.Redirect("~/Login.aspx", false);
        //    }
        //    else
        //    {

        //    }
        //}
        //catch (Exception ex)
        //{
        //    System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
        //    CommonFunctions   cf = new CommonFunctions();
        //    cf.WriteError(ex.Message, ex.StackTrace.ToString(), Session["LOGINNAME"].ToString(), "Search");
        //    Response.Write("<!--" + ex.ToString() + "-->");
        //}
        //finally
        //{

        //}


    }
    public void FillStore()
    {
        ds = new DataSet();
        ESales Sale = new ESales();
        try
        {

            Sale.LoginID = Convert.ToInt32(Session["LOGINID"]);
            if (DrpStroe.SelectedValue == "")
                Sale.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
            else
                Sale.OrganisationUser = Convert.ToInt32(DrpStroe.SelectedValue);
            ds = Sale.ddlStore();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    DrpStroe.DataSource = ds;
                    DrpStroe.DataValueField = "StoreID";
                    DrpStroe.DataTextField = "StoreName";
                    DrpStroe.DataBind();
                    DrpStroe.Items.Insert(0, new ListItem("--Any--", "0"));
                }
                else if (ds.Tables[0].Rows.Count > 1)
                {
                    DrpStroe.DataSource = ds;
                    DrpStroe.DataValueField = "StoreID";
                    DrpStroe.DataTextField = "StoreName";
                    DrpStroe.DataBind();
                    DrpStroe.Items.Insert(0, new ListItem("--Any--", "0"));
                }

            }
            else
            {
                DrpStroe.Items.Insert(0, new ListItem("--Any--", "0"));
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    private bool GetDashboardCredentials()
    {
        bool res = false;
        try
        {
            DataSet ds = new DataSet();
            Eobj = new ELogin();
            Eobj.LoginID = Convert.ToInt32(Session["LoginID"]);
            ds = Eobj.GetDashboardAccess();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                res = Convert.ToBoolean(ds.Tables[0].Rows[0]["view"]);
            }
        }
        catch (Exception ex)
        {
            Context.Response.Write(ex.ToString());
        }
        return res;
    }
    //public void GetStroeList()
    //{
    //    try
    //    {
    //        ds = new DataSet();
    //        Eobj = new ELogin();
    //        Eobj.LoginID = Convert.ToInt32(Session["LOGINID"]);
    //        Eobj.OrganisationUser = Convert.ToInt32(Session["StoreID"]);
    //        ds = Eobj.ddlStore();

    //        ds = Eobj.StoreList();


    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
    public void GetInvoiceBarGraph()
    {

        StringBuilder str = new StringBuilder();
        ds = new DataSet();
        Eobj = new ELogin();
        if (DrpStroe.SelectedValue == "")
            Eobj.StoreID = Convert.ToInt32(Session["StoreID"]);
        else
            Eobj.StoreID = Convert.ToInt32(DrpStroe.SelectedValue);
        // Chart1.Visible = true;
        var now = DateTime.Now;

        //ddlmonth.SelectedValue = System.DateTime.Now.Month.ToString();
        //ddlyear.SelectedValue = System.DateTime.Now.Year.ToString();        
        Eobj.Year = ddlyear.SelectedValue;
        Eobj.Month = ddlmonth.SelectedValue;
        var currentYear = now.Year;
        var currentMonth = now.Month;
        ds = Eobj.GraphData();
        DataTable dt = ds.Tables[0];

        
        int mon = Convert.ToInt32(Eobj.Month);
        int yr = Convert.ToInt32(Eobj.Year);
        if (mon != 0 && yr != 0)
        {
            List<DateTime> dat = GetDates(yr, mon);

            str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
            google.setOnLoadCallback(drawChart);
            function drawChart() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Date');
            data.addColumn('number', 'InvoiceAmount');     
 
            data.addRows(" + dat.Count + ");");//+ dt.Rows.Count +

            for (int j = 0; j < dat.Count; j++)
            {
                for (var k = 0; k < dt.Rows.Count; k++)
                {
                    if (dt.Rows[k]["PaymentDate"].ToString() == dat[j].Date.ToString())//j < dt.Rows.Count
                    {
                        DateTime d = Convert.ToDateTime(dt.Rows[k]["PaymentDate"].ToString());
                        if (dat[j].Date.Day == Convert.ToInt32(d.ToString("dd")))
                        {
                            str.Append("data.setValue( " + j + "," + 0 + "," + "'" + dat[j].Date.Day.ToString() + "');");
                            str.Append("data.setValue(" + j + "," + 1 + "," + dt.Rows[k]["PaymentAmount"].ToString() + ") ;");
                            break;
                        }
                    }
                    else
                    {
                        str.Append("data.setValue( " + j + "," + 0 + "," + "'" + dat[j].Date.Day.ToString() + "');");
                        str.Append("data.setValue(" + j + "," + 1 + "," + "0" + ") ;");
                    }
                }
            }

            str.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));");
            str.Append(" chart.draw(data,{isStacked:true, hAxis: {showTextEvery:3}});}");

            str.Append("</script>");
            lt.Text = str.ToString().Replace('*', '"');
        }
    }

    public static List<DateTime> GetDates(int year, int month)
    {
        return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                         .Select(day => new DateTime(year, month, day)) // Map each day to a date
                         .ToList(); // Load dates into a list
    }
    public void LogOut()
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void DrpStroe_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillStore();
        DashBoardSalesdetails();
        DashBoardSalesToatldetails();
        GetInvoiceBarGraph();
        GetInvoicePieGraph();
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        DashBoardSalesdetails();
        DashBoardSalesToatldetails();
        GetInvoiceBarGraph();
        GetInvoicePieGraph();
    }

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DashBoardSalesdetails();
        DashBoardSalesToatldetails();
        GetInvoiceBarGraph();
        GetInvoicePieGraph();
    }
    private void GetInvoicePieGraph()
    {
        try
        {
            ds = new DataSet();
            Eobj = new ELogin();
            // Chart1.Visible = true;
            var now = DateTime.Now;
            if (DrpStroe.SelectedValue == "")
                Eobj.StoreID = Convert.ToInt32(Session["StoreID"]);
            else
                Eobj.StoreID = Convert.ToInt32(DrpStroe.SelectedValue);
            //ddlmonth.SelectedValue = System.DateTime.Now.Month.ToString();
            //ddlyear.SelectedValue = System.DateTime.Now.Year.ToString();
            Eobj.Year = ddlyear.SelectedValue;
            Eobj.Month = ddlmonth.SelectedValue;

            var currentYear = now.Year;
            var currentMonth = now.Month;
            ds = Eobj.PieGraphData();

            DataTable ChartData = ds.Tables[0];
            string[] XPointMember = new string[ChartData.Rows.Count];
            int[] YPointMember = new int[ChartData.Rows.Count];

            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = ChartData.Rows[count]["ColName"].ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["Total"]);

            }
            //binding chart control  
            Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

            //Setting width of line  
            Chart1.Series[0].BorderWidth = 8;
            //setting Chart type   
            Chart1.Series[0].ChartType = SeriesChartType.Pie;


            foreach (Series charts in Chart1.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    switch (point.AxisLabel)
                    {
                        case "Q1": point.Color = Color.RoyalBlue; break;
                        case "Q2": point.Color = Color.SaddleBrown; break;
                    }
                    point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void DashBoardSalesToatldetails()
    {
        try
        {

            ds = new DataSet();
            Eobj = new ELogin();

            if (DrpStroe.SelectedValue == "")
                Eobj.StoreID = Convert.ToInt32(Session["StoreID"]);
            else
                Eobj.StoreID = Convert.ToInt32(DrpStroe.SelectedValue);

            Eobj.Month = ddlmonth.SelectedValue.Trim();
            Eobj.Year = ddlyear.SelectedValue.Trim();
            ds = Eobj.EGetSalesTotal();
            //int DsUser =
            string i = ds.Tables[0].Rows[0]["NetTotal"].ToString();
            ds = Eobj.EGetSalesTotalcoolec();
            string j = ds.Tables[0].Rows[0]["Collec"].ToString();
            if (i == "")
            {
                lblTodaySales.Text = "0";
            }
            else
            {
                lblTodaySales.Text = i;
            }
            if (j == "")
            {
                lblTodaycollec.Text = "0";
            }
            else
            {
                lblTodaycollec.Text = j;
            }
            ds = new DataSet();
            Eobj = new ELogin();
            if (DrpStroe.SelectedValue == "")
                Eobj.StoreID = Convert.ToInt32(Session["StoreID"]);
            else
                Eobj.StoreID = Convert.ToInt32(DrpStroe.SelectedValue);
            Eobj.Month = ddlmonth.SelectedValue.Trim();
            Eobj.Year = ddlyear.SelectedValue.Trim();
            ds = Eobj.EGetSalesTodayTotal();
            int DsUser1 = ds.Tables[0].Rows.Count;
            i = ds.Tables[0].Rows[0]["NetTotal"].ToString();
            ds = Eobj.EGetSalesTodayTotalCoolec();
            j = ds.Tables[0].Rows[0]["Collec"].ToString();
            if (i == "")
            {
                lblMonthSales.Text = "0";
            }
            else
            {
                lblMonthSales.Text = i;
            }
            if (j == "")
            {
                lblmonthcollec.Text = "0";
            }
            else
            {
                lblmonthcollec.Text = j;
            }

            var now =System.DateTime.Now;
            string date = now.ToString("yyyy-MM-dd 00:00:00.000");
            int currentYear = Convert.ToInt32(ddlyear.SelectedValue);
            int currentMonth = Convert.ToInt32(ddlmonth.SelectedValue);
            var currentdate = now.Day;

            if (currentYear != 0 && currentMonth != 0)
            {
                DateTime dt = new DateTime(currentYear, currentMonth, currentdate);
                DateTime? dt2;
                dt2 = null;
                DayOfWeek dt1 = dt.DayOfWeek;
                string day = dt1.ToString();

                if (day == "Monday")
                {
                    // dt2 = dt;
                    dt2 = dt.AddDays(-1);
                }
                else if (day == "Tuesday")
                {
                    dt2 = dt.AddDays(-2);
                    //dt1 = dt2.DayOfWeek;
                }
                else if (day == "Wednesday")
                {
                    dt2 = dt.AddDays(-3);
                    // dt1 = dt2.DayOfWeek;
                }
                else if (day == "Thursday")
                {
                    dt2 = dt.AddDays(-4);
                    //dt1 = dt2.DayOfWeek;
                }
                else if (day == "Friday")
                {
                    dt2 = dt.AddDays(-5);
                    //dt1 = dt2.DayOfWeek;
                    //dt1 = dt2.DayOfWeek;
                }
                else if (day == "Saturday")
                {
                    dt2 = dt.AddDays(-6);
                    //dt1 = dt2.DayOfWeek;
                }
                else if (day == "Sunday")
                {
                    // dt2 = dt.AddDays(-1);
                    //dt1 = dt2.DayOfWeek;
                }

                ds = new DataSet();
                Eobj = new ELogin();
                if (day != "Sunday")
                {
                    string datt = String.Format("{0:yyyy-MM-dd}", dt2);
                    string dattt = String.Format("{0:yyyy-MM-dd}", dt);
                    Eobj.dt1 = dattt;
                    Eobj.dt2 = datt;
                }
                else
                {
                    string dattt = String.Format("{0:yyyy-MM-dd}", dt);
                    Eobj.dt1 = dattt;
                    Eobj.dt2 = "";
                }

                if (DrpStroe.SelectedValue == "")
                    Eobj.StoreID = Convert.ToInt32(Session["StoreID"]);
                else
                    Eobj.StoreID = Convert.ToInt32(DrpStroe.SelectedValue);
                Eobj.Month = ddlmonth.SelectedValue.Trim();
                Eobj.Year = ddlyear.SelectedValue.Trim();
                ds = Eobj.EGetSalesWeekTotal();
                int DsUser2 = ds.Tables[0].Rows.Count;
                i = ds.Tables[0].Rows[0]["NetTotal"].ToString();
                ds = Eobj.EGetSalesWeekTotalcoolec();
                DsUser2 = ds.Tables[0].Rows.Count;
                j = ds.Tables[0].Rows[0]["Collec"].ToString();
                if (i == "")
                {
                    lblWeekSales.Text = "0";
                }
                else
                {
                    lblWeekSales.Text = i;
                }
                if (j == "")
                {
                    lblweekcollec.Text = "0";
                }
                else
                {
                    lblweekcollec.Text = j;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void DashBoardSalesdetails()
    {
        try
        {
            ds = new DataSet();
            Eobj = new ELogin();
            if (DrpStroe.SelectedValue == "")
                Eobj.StoreID = Convert.ToInt32(Session["StoreID"]);
            else
                Eobj.StoreID = Convert.ToInt32(DrpStroe.SelectedValue);
            Eobj.Month = ddlmonth.SelectedValue.Trim();
            Eobj.Year = ddlyear.SelectedValue.Trim();
            ds = Eobj.EGetSalesCount();
            int DsUser = ds.Tables[0].Rows.Count;
            monthbill.Text = DsUser.ToString();

            ds = new DataSet();
            Eobj = new ELogin();
            if (DrpStroe.SelectedValue == "")
                Eobj.StoreID = Convert.ToInt32(Session["StoreID"]);
            else
                Eobj.StoreID = Convert.ToInt32(DrpStroe.SelectedValue);
            Eobj.Month = ddlmonth.SelectedValue.Trim();
            Eobj.Year = ddlyear.SelectedValue.Trim();
            ds = Eobj.EGetSalesDayCount();
            int DsUser1 = ds.Tables[0].Rows.Count;
            TodayBill.Text = DsUser1.ToString();

            var now = DateTime.Now;
            string date = now.ToString("yyyy-MM-dd 00:00:00.000");
            int currentYear =Convert.ToInt32(ddlyear.SelectedValue);
            int currentMonth = Convert.ToInt32(ddlmonth.SelectedValue);
            var currentdate = now.Day;
            if (currentYear != 0 && currentMonth != 0)
            {
                DateTime dt = new DateTime(currentYear, currentMonth, currentdate);
                DateTime? dt2;
                dt2 = null;
                DayOfWeek dt1 = dt.DayOfWeek;
                string day = dt1.ToString();

                if (day == "Monday")
                {
                    // dt2 = dt;
                    dt2 = dt.AddDays(-1);
                }
                else if (day == "Tuesday")
                {
                    dt2 = dt.AddDays(-2);
                    //dt1 = dt2.DayOfWeek;
                }
                else if (day == "Wednesday")
                {
                    dt2 = dt.AddDays(-3);
                    // dt1 = dt2.DayOfWeek;
                }
                else if (day == "Thursday")
                {
                    dt2 = dt.AddDays(-4);
                    //dt1 = dt2.DayOfWeek;
                }
                else if (day == "Friday")
                {
                    dt2 = dt.AddDays(-5);
                    //dt1 = dt2.DayOfWeek;
                    //dt1 = dt2.DayOfWeek;
                }
                else if (day == "Saturday")
                {
                    dt2 = dt.AddDays(-6);
                    //dt1 = dt2.DayOfWeek;
                }
                else if (day == "Sunday")
                {
                    //dt2 = dt.AddDays(-1);
                    //dt1 = dt2.DayOfWeek;
                }

                ds = new DataSet();
                Eobj = new ELogin();
                if (day != "Sunday")
                {
                    string datt = String.Format("{0:yyyy-MM-dd}", dt2);
                    string dattt = String.Format("{0:yyyy-MM-dd}", dt);
                    Eobj.dt1 = dattt;
                    Eobj.dt2 = datt;
                }
                else
                {
                    string dattt = String.Format("{0:yyyy-MM-dd}", dt);
                    Eobj.dt1 = dattt;
                    Eobj.dt2 = "";
                }
                if (DrpStroe.SelectedValue == "")
                    Eobj.StoreID = Convert.ToInt32(Session["StoreID"]);
                else
                    Eobj.StoreID = Convert.ToInt32(DrpStroe.SelectedValue);
                Eobj.Month = ddlmonth.SelectedValue.Trim();
                Eobj.Year = ddlyear.SelectedValue.Trim();
                ds = Eobj.EGetSalesWeekCount();
                int DsUser2 = ds.Tables[0].Rows.Count;
                weekbill.Text = DsUser2.ToString();
                //y,m,d
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }





}
