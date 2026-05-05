using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using ESaleEntity.DAL;
namespace ESaleEntity
{
    public class ELogin
    {
        #region private variblaes
        private string _LoginName;
        private string _Password;
        private string _LoginType;
        private string _MobileNo;
        private string _Year;
        private string _Month;
        #endregion
        #region public methods
        private string _FromDate;

        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        private string _ToDate;

        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public string LoginName
        {
            get { return _LoginName; }
            set { _LoginName = value; }
        }
        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        public string Month
        {
            get { return _Month; }
            set { _Month = value; }
        }
        private int _LoginID;

        public int LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        public int StoreID { get; set; }
        public string dt1 { get; set; }
        public string dt2 { get; set; }
        public string LoginType
        {
            get { return _LoginType; }
            set { _LoginType = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        #endregion

        Hashtable ht;
        DataSet ds;
        DLogin Dobj;
        string WhereCondition = string.Empty;

        public DataSet EVerifyUserLogin()
        {
            try
            {
                WhereCondition = "";
                Dobj = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                if (LoginName != null)
                    WhereCondition = " and L.LoginName='" + LoginName + "' and L.password='" + Password + "' ";
                //else if (UserId != null)
                //    WhereCondition = " and L.LoginID=" + UserId;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "VerifyRoleswiseUserlogin");
                ds = Dobj.GetTransaction("SP_GetDataLogin", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;

        }
        public DataSet EGetSalesCount()
        {
            try
            {
                WhereCondition = "";
                Dobj = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@Year", Year);
                ht.Add("@Month", Month);
                var now = DateTime.Now;
                var currentYear = now.Year;
                var currentMonth = now.Month;
                if (StoreID != 0)
                    WhereCondition = " DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + " and Storeid=" + StoreID + " ";
                else
                    WhereCondition = " DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + "  ";
                // WhereCondition = " DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year +"and Storeid="+StoreID+" ";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "Month");
                ds = Dobj.GetTransaction("Sp_GetBills", ht);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }

        }
        public DataSet EGetSalesDayCount()
        {
            try
            {
                WhereCondition = "";
                Dobj = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@Year", Year);
                ht.Add("@Month", Month);
                var now = DateTime.Now;
                string date = now.ToString("yyyy-MM-dd 00:00:00.000");
                var currentYear = now.Year;
                var currentMonth = now.Month;
                FromDate = System.DateTime.UtcNow.AddHours(10).ToString("dd-MM-yyyy");
                ToDate = System.DateTime.UtcNow.AddHours(10).ToString("dd-MM-yyyy");
                FromDate = converttodate(FromDate);
                ToDate = converttodate(ToDate);
                WhereCondition = "";
                if (StoreID != 0)
                    WhereCondition = "and Storeid= " + StoreID + "";
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and cast(invoicedate as date) between'" + FromDate + "' and '" + ToDate + "'";
                //if (StoreID != 0)
                //    WhereCondition = " and Storeid= " + StoreID + "";
                //else
                //    WhereCondition = " CONVERT(DATE,InvoiceDate) = '" + date + "'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "Today");
                ds = Dobj.GetTransaction("Sp_GetBills", ht);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }

        }
        public DataSet EGetSalesWeekCount()
        {
            try
            {
                WhereCondition = "";
                Dobj = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@Year", Year);
                ht.Add("@Month", Month);
                var now = DateTime.Now;
                string date = now.ToString("yyyy-MM-dd 00:00:00.000");
                var currentYear = now.Year;
                var currentMonth = now.Month;
                var currentdate = now.Day;



                // WhereCondition = " CONVERT(DATE,InvoiceDate) =  '" + date + "'and Storeid= " + StoreID + "";
                //if (StoreID != 0)
                //    WhereCondition = " CONVERT(DATE,InvoiceDate) = '" + date + "'and Storeid= " + StoreID + "";
                //else
                //    WhereCondition = " CONVERT(DATE,InvoiceDate) = '" + date + "'";

                //if (StoreID != 0)
                //    WhereCondition = "DATEPART(dd,InvoiceDate)=" + currentdate + "and DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + " and Storeid= " + StoreID + " and InvoiceDate  BETWEEN DATEADD(DAY, -7, " + currentdate + "-" + currentMonth + "-" + currentYear + ") AND " + currentdate + "-" + currentMonth + "-" + currentYear + "";
                //else
                //    WhereCondition = "DATEPART(dd,InvoiceDate)=" + currentdate + " and DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + " and InvoiceDate  BETWEEN DATEADD(DAY, -7, " + currentdate + "-" + currentMonth + "-" + currentYear + ") AND " + currentdate + "-" + currentMonth + "-" + currentYear + "";

                if (StoreID != 0 && dt2 != "")
                    WhereCondition += "  CONVERT(DATE,InvoiceDate) between'" + dt2 + "' and '" + dt1 + "'and Storeid= " + StoreID + "";
                // WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and CONVERT(DATE,InvoiceDate) <'" + dt1 + "' and Storeid= " + StoreID + " ";
                else if (StoreID != 0 && dt2 == "")
                    //WhereCondition += " and CONVERT(DATE,InvoiceDate) between'" + dt2 + "' and '" + dt1 + "'and Storeid= " + StoreID + "";     
                    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and Storeid= " + StoreID + " ";
                else if (StoreID == 0 && dt2 != "")
                    //WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and CONVERT(DATE,InvoiceDate) <'" + dt1 + "' ";
                    WhereCondition += "  CONVERT(DATE,InvoiceDate) between'" + dt2 + "' and '" + dt1 + "'";
                else
                    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt1 + "' ";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "Week");
                ds = Dobj.GetTransaction("Sp_GetBills", ht);
                return ds;

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
            date = _date[2] + "-" + _date[1] + "-" + _date[0];
            return date;
        }
        public DataSet EGetSalesTotal()
        {

            try
            {
                WhereCondition = "";
                Dobj = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@Year", Year);
                ht.Add("@Month", Month);
                var now = DateTime.Now;
                string date = now.ToString("yyyy-MM-dd 00:00:00.000");
                var currentYear = now.Year;
                var currentMonth = now.Month;
                FromDate = System.DateTime.UtcNow.AddHours(10).ToString("dd-MM-yyyy");
                ToDate = System.DateTime.UtcNow.AddHours(10).ToString("dd-MM-yyyy");
                FromDate = converttodate(FromDate);
                ToDate = converttodate(ToDate);
                WhereCondition = "";
                if (StoreID != 0)
                    WhereCondition = "and Storeid= " + StoreID + "";
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and cast(invoicedate as date) between'" + FromDate + "' and '" + ToDate + "'";
                //else
                //    WhereCondition = " CONVERT(DATE,InvoiceDate) = '" + date + "'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "MonthTotal");
                ds = Dobj.GetTransaction("Sp_GetBills", ht);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataSet EGetSalesTotalcoolec()
        {

            try
            {
                WhereCondition = "";
                Dobj = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@Year", Year);
                ht.Add("@Month", Month);
                var now = DateTime.Now;
                string date = now.ToString("yyyy-MM-dd 00:00:00.000");
                var currentYear = now.Year;
                var currentMonth = now.Month;
                FromDate = System.DateTime.UtcNow.AddHours(10).ToString("dd-MM-yyyy");
                ToDate = System.DateTime.UtcNow.AddHours(10).ToString("dd-MM-yyyy");
                FromDate = converttodate(FromDate);
                ToDate = converttodate(ToDate);
                if (StoreID != 0)
                    WhereCondition = "and Storeid= " + StoreID + "";
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and cast(invoicedate as date) between'" + FromDate + "' and '" + ToDate + "'";
                //else
                //    WhereCondition = "and CONVERT(DATE,PaymentDate) = '" + date + "'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "MonthTotalcollec");
                ds = Dobj.GetTransaction("Sp_GetBills", ht);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataSet EGetSalesTodayTotal()
        {
            try
            {
                WhereCondition = "";
                Dobj = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@Year", Year);
                ht.Add("@Month", Month);
                var now = DateTime.Now;
                var currentYear = now.Year;
                var currentMonth = now.Month;
                if (StoreID != 0)
                    WhereCondition = " DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + " and Storeid=" + StoreID + " ";
                else
                    WhereCondition = " DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + "  ";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "TodayTotal");
                ds = Dobj.GetTransaction("Sp_GetBills", ht);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataSet EGetSalesTodayTotalCoolec()
        {
            try
            {
                Dobj = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@Year", Year);
                ht.Add("@Month", Month);
                var now = DateTime.Now;
                var currentYear = now.Year;
                var currentMonth = now.Month;
                WhereCondition = "";
                if (StoreID != 0)
                    WhereCondition = "and DATEPART(mm,PaymentDate)=" + Month + "and  DATEPART(YYYY,PaymentDate)=" + Year + " and Storeid=" + StoreID + " ";
                else
                    WhereCondition = "and DATEPART(mm,PaymentDate)=" + Month + "and  DATEPART(YYYY,PaymentDate)=" + Year + "  ";
                //if (StoreID != 0)
                //    WhereCondition = "  and Storeid=" + StoreID + " ";
                //else 
                //    WhereCondition = " DATEPART(mm,PaymentDate)=" + Month + "and  DATEPART(YYYY,PaymentDate)=" + Year + "  ";
                ////if (StoreID != 0)
                //  //  WhereCondition = " and Storeid=" + StoreID + " ";//DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + " and
                //else
                //    WhereCondition = " DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + "  ";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "TodayTotalcollec");
                ds = Dobj.GetTransaction("Sp_GetBills", ht);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataSet EGetSalesWeekTotal()
        {
            try
            {
                WhereCondition = "";
                Dobj = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@Year", Year);
                ht.Add("@Month", Month);
                var now = DateTime.Now;
                string date = now.ToString("yyyy-MM-dd 00:00:00.000");
                var currentYear = now.Year;
                var currentMonth = now.Month;
                var currentdate = now.Day;
                //WhereCondition = " CONVERT(DATE,InvoiceDate) =  '" + date + "'and Storeid= " + StoreID + "";
                //if (StoreID != 0)
                //    WhereCondition = " CONVERT(DATE,InvoiceDate) = '" + date + "'and Storeid= " + StoreID + "";
                //else
                //    WhereCondition = " CONVERT(DATE,InvoiceDate) = '" + date + "'";
                // if (StoreID != 0)
                //     WhereCondition = "DATEPART(dd,InvoiceDate)=" + currentdate + "and DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + " and Storeid= " + StoreID + "and InvoiceDate  BETWEEN DATEADD(DAY, -7, " + currentdate + "-" + currentMonth + "-" + currentYear + ") AND " + currentdate + "-" + currentMonth + "-" + currentYear + "";
                //else
                //     WhereCondition = "DATEPART(dd,InvoiceDate)=" + currentdate + " and DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + "and InvoiceDate  BETWEEN DATEADD(DAY, -7, " + currentdate + "-" + currentMonth + "-" + currentYear + ") AND " + currentdate + "-" + currentMonth + "-" + currentYear + "";
                //if (StoreID != 0 && dt2 != "")
                //    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and CONVERT(DATE,InvoiceDate) <'" + dt1 + "' and Storeid= " + StoreID + " ";
                //else if (StoreID != 0 && dt2 == "")
                //    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and Storeid= " + StoreID + " ";
                //else if (StoreID == 0 && dt2 != "")
                //    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and CONVERT(DATE,InvoiceDate) <'" + dt1 + "' ";
                //else
                //    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt1 + "' ";
                if (StoreID != 0 && dt2 != "")
                    WhereCondition += "  CONVERT(DATE,InvoiceDate) between'" + dt2 + "' and '" + dt1 + "'and Storeid= " + StoreID + "";
                // WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and CONVERT(DATE,InvoiceDate) <'" + dt1 + "' and Storeid= " + StoreID + " ";
                else if (StoreID != 0 && dt2 == "")
                    //WhereCondition += " and CONVERT(DATE,InvoiceDate) between'" + dt2 + "' and '" + dt1 + "'and Storeid= " + StoreID + "";     
                    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and Storeid= " + StoreID + " ";
                else if (StoreID == 0 && dt2 != "")
                    //WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and CONVERT(DATE,InvoiceDate) <'" + dt1 + "' ";
                    WhereCondition += " CONVERT(DATE,InvoiceDate) between'" + dt2 + "' and '" + dt1 + "'";
                else
                    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt1 + "' ";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "WeekTotal");
                ds = Dobj.GetTransaction("Sp_GetBills", ht);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataSet EGetSalesWeekTotalcoolec()
        {
            try
            {
                Dobj = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@Year", Year);
                ht.Add("@Month", Month);
                var now = DateTime.Now;
                string date = now.ToString("yyyy-MM-dd 00:00:00.000");
                var currentYear = now.Year;
                var currentMonth = now.Month;
                var currentdate = now.Day;
                WhereCondition = "";
                //WhereCondition = " CONVERT(DATE,InvoiceDate) =  '" + date + "'and Storeid= " + StoreID + "";
                //if (StoreID != 0)
                //    WhereCondition = " CONVERT(DATE,InvoiceDate) = '" + date + "'and Storeid= " + StoreID + "";
                //else
                //    WhereCondition = " CONVERT(DATE,InvoiceDate) = '" + date + "'";
                // if (StoreID != 0)
                //     WhereCondition = "DATEPART(dd,InvoiceDate)=" + currentdate + "and DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + " and Storeid= " + StoreID + "and InvoiceDate  BETWEEN DATEADD(DAY, -7, " + currentdate + "-" + currentMonth + "-" + currentYear + ") AND " + currentdate + "-" + currentMonth + "-" + currentYear + "";
                //else
                //     WhereCondition = "DATEPART(dd,InvoiceDate)=" + currentdate + " and DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + "and InvoiceDate  BETWEEN DATEADD(DAY, -7, " + currentdate + "-" + currentMonth + "-" + currentYear + ") AND " + currentdate + "-" + currentMonth + "-" + currentYear + "";
                ////if (StoreID != 0 && dt2 != "")
                ////    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and CONVERT(DATE,InvoiceDate) <'" + dt1 + "' and Storeid= " + StoreID + " ";
                ////else if (StoreID != 0 && dt2 == "")
                ////    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and Storeid= " + StoreID + " ";
                ////else if (StoreID == 0 && dt2 != "")
                ////    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and CONVERT(DATE,InvoiceDate) <'" + dt1 + "' ";
                ////else
                ////    WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt1 + "' ";
                if (StoreID != 0 && dt2 != "")
                    WhereCondition += "  CONVERT(DATE,PaymentDate) between'" + dt2 + "' and '" + dt1 + "'and Storeid= " + StoreID + "";
                // WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and CONVERT(DATE,InvoiceDate) <'" + dt1 + "' and Storeid= " + StoreID + " ";
                else if (StoreID != 0 && dt2 == "")
                    //WhereCondition += " and CONVERT(DATE,InvoiceDate) between'" + dt2 + "' and '" + dt1 + "'and Storeid= " + StoreID + "";     
                    WhereCondition = "CONVERT(DATE,PaymentDate)>'" + dt2 + "'and Storeid= " + StoreID + " ";
                else if (StoreID == 0 && dt2 != "")
                    //WhereCondition = "CONVERT(DATE,InvoiceDate)>'" + dt2 + "'and CONVERT(DATE,InvoiceDate) <'" + dt1 + "' ";
                    WhereCondition += "  CONVERT(DATE,PaymentDate) between'" + dt2 + "' and '" + dt1 + "'";
                else
                    WhereCondition = "CONVERT(DATE,PaymentDate)>'" + dt1 + "' ";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "WeekTotalcollec");
                ds = Dobj.GetTransaction("Sp_GetBills", ht);
                return ds;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataSet EGetPassword()
        {
            Dobj = new DLogin();
            ds = new DataSet();
            WhereCondition = "and  L.LoginName like'" + LoginName + "' and R.MobileNo like'" + MobileNo + "'";
            ht = new Hashtable();
            ht.Add("@wherecondition", WhereCondition);
            ht.Add("@Transaction", "GETPASSWORD");

            ds = Dobj.GetTransaction("SP_GetDataLogin", ht);
            return ds;
        }
        public DataSet GraphData()
        {
            try
            {
                WhereCondition = "";
                Dobj = new DLogin();
                ds = new DataSet();
                var now = DateTime.Now;
                ht = new Hashtable();
                ht.Add("@Year", Year);
                ht.Add("@Month", Month);
                var currentYear = now.Year;
                var currentMonth = now.Month;
                if (StoreID != 0)
                    WhereCondition = " DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + " and Storeid=" + StoreID + " ";
                else
                    WhereCondition = " DATEPART(mm,InvoiceDate)=" + Month + "and  DATEPART(YYYY,InvoiceDate)=" + Year + "  ";
                //WhereCondition = "DATEPART(mm,PaymentDate)=" + Month + "and  DATEPART(YYYY,PaymentDate)=" + Year + "and s.StoreID="+StoreID+"";               
                ht.Add("@wherecondition", WhereCondition);
                ht.Add("@Transaction", "GetBarData");
                ds = Dobj.GetTransaction("Sp_GetBills", ht);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet PieGraphData()
        {
            try
            {
                WhereCondition = "";
                Dobj = new DLogin();
                ds = new DataSet();
                var now = DateTime.Now;
                ht = new Hashtable();
                //ht.Add("@Year", 1);
                //ht.Add("@Month", 2);
                ht.Add("@Year", Year);
                ht.Add("@Month", Month);
                var currentYear = now.Year;
                var currentMonth = now.Month;
                WhereCondition = "";
                FromDate = System.DateTime.UtcNow.AddHours(10).ToString("dd-MM-yyyy");
                ToDate = System.DateTime.UtcNow.AddHours(10).ToString("dd-MM-yyyy");
                FromDate = converttodate(FromDate);
                ToDate = converttodate(ToDate);
                if (StoreID != 0)
                    WhereCondition = "and Storeid= " + StoreID + "";
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and cast(invoicedate as date) between'" + FromDate + "' and '" + ToDate + "'";
                ht.Add("@wherecondition", WhereCondition);
                ht.Add("@Transaction", "GetPieData");
                ds = Dobj.GetTransaction("Sp_GetBills", ht);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet GetDashboardAccess()
        {
            try
            {
                WhereCondition = string.Empty;
                Dobj = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                if (LoginID != 0)
                {
                    ht.Add("@LoginId", LoginID);
                    ds = Dobj.GetTransaction("Sp_CheckDashboard", ht);
                }
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
