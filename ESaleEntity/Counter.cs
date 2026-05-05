using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESaleEntity.DAL;
namespace ESaleEntity
{
    public class Counter
    {
        DLogin ECounter;
        DataSet ds;
        Hashtable ht;
        string WhereCondition;
        string Transaction;
        string Result;
        private string _OpeningDate;

        public string OpeningDate
        {
            get { return _OpeningDate; }
            set { _OpeningDate = value; }
        }
        private float _OpeningValue;

        public float OpeningValue
        {
            get { return _OpeningValue; }
            set { _OpeningValue = value; }
        }
        private int _StoreID;

        public int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }
        private int _LoginID;

        public int LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        private string _OpenTime;

        public string OpenTime
        {
            get { return _OpenTime; }
            set { _OpenTime = value; }
        }
        private float _TotalSales;

        public float TotalSales
        {
            get { return _TotalSales; }
            set { _TotalSales = value; }
        }
        private float _TotalExpenses;

        public float TotalExpenses
        {
            get { return _TotalExpenses; }
            set { _TotalExpenses = value; }
        }
        private float _ClosingValue;

        public float ClosingValue
        {
            get { return _ClosingValue; }
            set { _ClosingValue = value; }
        }
        private float _WithDrawAmount;

        public float WithDrawAmount
        {
            get { return _WithDrawAmount; }
            set { _WithDrawAmount = value; }
        }
        private float _FinalAmount;

        public float FinalAmount
        {
            get { return _FinalAmount; }
            set { _FinalAmount = value; }
        }
        private int _CounterID;

        public int CounterID
        {
            get { return _CounterID; }
            set { _CounterID = value; }
        }
        private int _UserID;

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
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
        public string SaveOpeningCounter()
        {
            try
            {
                Result = string.Empty;
                ds = new DataSet();
                ht = new Hashtable();
                ECounter = new DLogin();
                ht.Add("@StoreID", StoreID);               
                ht.Add("@OpeningCounterValue", OpeningValue);
                ht.Add("@Transaction", "Insert");
                ht.Add("@LoginID", LoginID);
                ht.Add("@CounterID", 0);
                ht.Add("@Salesvalue", 0);
                ht.Add("@Expenses", 0);
                ht.Add("@ClosingValue", 0);
                ht.Add("@WithDraw", 0);
                ht.Add("@FinalAmount", 0);
                ds = ECounter.GetTransaction("SP_Counter", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {
                
                throw;
            }
            return Result;
        }
        public DataSet FillStore()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                ECounter=new DLogin();
                WhereCondition = string.Empty;
                if (LoginID != 1)
                {
                    WhereCondition = " and L.LoginID=" + LoginID;
                    Transaction = "ddlStoreForUser";
                }
                else
                {
                    Transaction = "ddlStore";
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = ECounter.GetTransaction("SP_GetDataCounter", ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }
        public DataSet FillUsers()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                ECounter = new DLogin();
                WhereCondition = string.Empty;
                if (StoreID != 0)
                    WhereCondition += " and StoreID =" + StoreID;
                ht.Add("@WhereCondition",WhereCondition);
                ht.Add("@Transaction", "ddlUser");
                ds = ECounter.GetTransaction("SP_GetDataCounter", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetClosingCounterGrid()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                ECounter = new DLogin();
                WhereCondition = string.Empty;
                if (StoreID != 0)
                    WhereCondition = " and S.StoreID =" + StoreID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetClosingCounter");
                ds = ECounter.GetTransaction("SP_GetDataCounter", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public string SaveClosingCounter()
        {
            try
            {
                Result = string.Empty;
                ds = new DataSet();
                ht = new Hashtable();
                ECounter = new DLogin();
                ht.Add("@StoreID", 0);
                ht.Add("@OpeningCounterValue", 0);
                ht.Add("@Transaction", "InsertCloseingCounter");
                ht.Add("@LoginID", LoginID);
                ht.Add("@CounterID", CounterID);
                ht.Add("@Salesvalue", TotalSales);
                ht.Add("@Expenses", TotalExpenses);
                ht.Add("@ClosingValue", ClosingValue);
                ht.Add("@WithDraw", WithDrawAmount);
                ht.Add("@FinalAmount", FinalAmount);
                ds = ECounter.GetTransaction("SP_Counter", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }
        public DataSet GetCounterReportGrid()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                ECounter = new DLogin();
                WhereCondition = string.Empty;
                if (StoreID != 0)
                    WhereCondition += " and S.StoreID =" + StoreID;
                if (UserID != 0)
                    WhereCondition += " and C.OpenedBy =" + UserID;
                if (FromDate != "" && ToDate != "")
                    WhereCondition += " and CONVERT(nvarchar(50),C.CreatedDate,105) between '" + FromDate + "' and '" + ToDate + "'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetCounterReportGrid");
                ds = ECounter.GetTransaction("SP_GetDataCounter", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetOpeningVal()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                ECounter = new DLogin();
                WhereCondition=string.Empty;
                if (StoreID != 0)
                    WhereCondition = " and StoreID=" + StoreID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "OpenValue");
                ds = ECounter.GetTransaction("SP_GetDataCounter", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds; 
        }
    }
}
