using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using ESaleEntity;
using ESaleDAL;


namespace ESaleEntity
{
    public class EexpenseType
    {
        // Entites Declaration.
        DataSet ds;
        Hashtable ht;
        SQLHelper sqlobj;
        string result;
        string WhereCondition, Transaction;
        private int _ExpenseTypeID;
        private int _LoginID;
        private string _ExpenseType;
        private int _StoreID;
        // Properties Delcaration.
        public int ExpenseTypeID
        {
            get { return _ExpenseTypeID; }
            set { _ExpenseTypeID = value; }
        }
        public int LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        public string ExpenseType
        {
            get { return _ExpenseType; }
            set { _ExpenseType = value; }
        }
        public int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int OrganisationUser { get; set; }
        // Getting the data into the database and return back to the associate function.
        public DataSet EfillGridView()
        {
            ds = new DataSet();
            ht = new Hashtable();
            try
            {
                WhereCondition = string.Empty;
                //if (StoreID != 0)
                //{
                //    WhereCondition = "and et.StoreID=" + StoreID;
                //}
                if (ExpenseType != "")
                {
                    WhereCondition += " and et.ExpenseType LIKE '" + ExpenseType + "%'";
                }

                ht.Add("@Transaction", "GetExpenseTypeData");
                ht.Add("@WhereCondition", WhereCondition);
                ds = GetTransaction("Sp_GetExpenseType", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public string EAddExpense()
        {
            ht = new Hashtable();
            ds = new DataSet();
            try
            {
                ht.Add("@ExpenseTypeID", 0);
                ht.Add("@LoginID", LoginID);
                ht.Add("@StoreID", StoreID);
                ht.Add("@ExpenseType", ExpenseType);
                ht.Add("@Transaction", "INSERT");
                ds = GetTransaction("SP_ExpenseType", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return (ds.Tables[0].Rows[0]["Status"].ToString());
        }
        public string EUpdateExpense()
        {
            ht = new Hashtable();
            ds = new DataSet();
            try
            {
                ht.Add("@ExpenseTypeID", ExpenseTypeID);
                ht.Add("@ExpenseType", ExpenseType);
                ht.Add("@StoreID", StoreID);
                ht.Add("@LoginID", LoginID);
                ht.Add("@Transaction", "UPDATE");
                ds = GetTransaction("SP_ExpenseType", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return (ds.Tables[0].Rows[0]["Status"].ToString());
        }
        public string EDeleteExpense()
        {
            ht = new Hashtable();
            ds = new DataSet();
            try
            {
                ht.Add("@ExpenseTypeID", ExpenseTypeID);
                ht.Add("@ExpenseType", "");
                ht.Add("@LoginID", LoginID);
                ht.Add("@StoreID", StoreID);
                ht.Add("@Transaction", "DELETE");
                ds = GetTransaction("SP_ExpenseType", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return (ds.Tables[0].Rows[0]["Status"].ToString());
        }
        public DataSet EViewForEdit()
        {
            ds = new DataSet();
            ht = new Hashtable();
            try
            {

                WhereCondition = "and  et.ExpenseTypeID =" + ExpenseTypeID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetExpenseTypeData");
                ds = GetTransaction("SP_GetExpenseType", ht);

            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet EfillStore()
        {
            ht = new Hashtable();
            ds = new DataSet();
            try
            {
                if (LoginID != 1)
                {
                    WhereCondition = " and L.LoginID=" + LoginID;
                    Transaction = "GetStoreDataForUser";
                }
                else
                {
                    WhereCondition = string.Empty;
                    Transaction = "GetStoreData";
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = GetTransaction("SP_GetExpenseType", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        // Execute the Stored Procedure using SQLHepler Class.
        public DataSet GetTransaction(string SPName, Hashtable htParam)
        {
            DataSet dsGet = new DataSet();
            sqlobj = new SQLHelper();
            try
            {
                dsGet = sqlobj.ExecuteSP(SPName, htParam);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dsGet;
        }
        public DataSet GetExpenseGridReport()
        {
            
                ht = new Hashtable();
                ds = new DataSet();
                WhereCondition = string.Empty;
                string WhereCondition2 = string.Empty;
                try
                {
                    if (StoreID != 0)
                        WhereCondition = " and PE.StoreID =" + StoreID;
                    if(FromDate!=""&&ToDate!="")
                        WhereCondition += " and Convert(nvarchar(100),PD.CreatedDate,105) between '" + FromDate + "' and '" + ToDate + "'";
                    if (LoginID != 1)
                    {
                        if (OrganisationUser != 0)
                        {
                            WhereCondition = " and L.LoginID=" + LoginID;
                            Transaction = "GetExpenseReportForUser";
                        }
                        else
                        {
                            WhereCondition2 = " and L.LoginID=" + LoginID;
                            Transaction = "GetExpenseReportForOrgUser";
                        }
                    }
                    else
                    {

                        Transaction = "GetExpenseReport";
                    }
                    ht.Add("@WhereCondition2", WhereCondition2);
                    ht.Add("@WhereCondition", WhereCondition);
                    ht.Add("@Transaction", Transaction);
                    ds = GetTransaction("SP_GetDataStoreDeliveryNoteNew", ht);

                }
                catch (Exception)
                {

                    throw;
                }
                return ds;
            
        }
    }
}
