using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESaleEntity.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
namespace ESaleEntity
{
    public class EPettyExpenses
    {
        DLogin PettyExpenses;
        DataSet ds;
        Hashtable ht;
        string Transaction, WhereCondition, Result,WhereCondition1;
        private int _PettyExpensesDetailsID;
        string _ExpenseName;
       public int OrganisationUser { get; set; }
        public string ExpenseName
        {
            get { return _ExpenseName; }
            set { _ExpenseName = value; }
        }
        public int PettyExpensesDetailsID
        {
            get { return _PettyExpensesDetailsID; }
            set { _PettyExpensesDetailsID = value; }
        }
        private int _PettyExpensesID;

        public int PettyExpensesID
        {
            get { return _PettyExpensesID; }
            set { _PettyExpensesID = value; }
        }
        private string _Month;

        public string Month
        {
            get { return _Month; }
            set { _Month = value; }
        }
        private string _Year;

        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        private int _StoreID;

        public int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }
        private string _GridData;
        public string GridData
        {
            get { return _GridData; }
            set { _GridData = value; }
        }
        private string _LoginID;
        public string LoginID
        {
            get{return _LoginID;}
            set { _LoginID = value; }
        }
        private string _InvoiceNo;
        public string InvoiceNo 
        {
            get { return _InvoiceNo ;}
            set {_InvoiceNo=value ;} 
        }
        private DateTime _InvoiceDate;
        public DateTime InvoiceDate 
        {
            get { return _InvoiceDate;}
            set { _InvoiceDate=value;} 
        }
        private DateTime _PaymedueDate;
        public DateTime PaymentdueDate 
        {
            get { return _PaymedueDate;}
            set { _PaymedueDate=value;} 
        }
        private decimal _Amount;
        public decimal Amount 
        {
            get { return _Amount; }
            set { _Amount=value; }
        }
        private decimal _AmountPaid;
        public decimal AmountPaid 
        {
            get { return _AmountPaid ;}
            set { _AmountPaid=value ; }
        }
        private decimal _Balance;
        public decimal Balance 
        {
            get { return _Balance ;}
            set { _Balance=value;} 
        }
        private string _Check;

        public string Check
        {
            get { return _Check; }
            set { _Check = value; }
        }
        public DataSet FillddlStore()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                PettyExpenses=new DLogin();
                WhereCondition = string.Empty;
                if (LoginID == "1")
                {
                    Transaction = "ddlStore";
                }
                else
                {
                    if (OrganisationUser == 0)
                    {
                        Transaction = "ddlStoreForOrgUser";
                    }
                    else
                    {
                        Transaction = "ddlStoreForUser";
                    }                    
                    WhereCondition = " and L.LoginID =" + LoginID;
                }
                
                ht.Add("@Transaction", Transaction);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition1", "");
                ds = PettyExpenses.GetTransaction("SP_GetDataPettyExpenses", ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }
        public DataSet ddlExpense()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                PettyExpenses = new DLogin();
                WhereCondition = string.Empty;
                Transaction = "ddlExpense";
                if (StoreID != 0)
                    WhereCondition = " and StoreID=" + StoreID;
                ht.Add("@Transaction", Transaction);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition1", "");
                ds = PettyExpenses.GetTransaction("SP_GetDataPettyExpenses", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet getGridData()
        {
            try
            {
                ds = new DataSet();
                ht = new Hashtable();
                PettyExpenses = new DLogin();
                WhereCondition = string.Empty;
                WhereCondition1=string.Empty;
                
                if (Check == "")
                {
                    WhereCondition += " and P.Month='" + Month + "'  and P.Year='" + Year + "'";
                    if (StoreID != 0)
                    {
                        WhereCondition += " and P.StoreID =" + StoreID ;
                    }
                    Transaction = "GetGridData";
                }
                else
                {
                    WhereCondition += " and P.Month='" + Month + "'  and P.Year='" + Year + "'";
                    Transaction = "GetGridDataChecked";
                    WhereCondition1 = "and DATEPART(MM,P.Month +''+P.Year)<=DATEPART(MM,'" + Month + "" + Year + "')and CONVERT(int,P.Year)<=CONVERT(int,'" + Year + "')";
                    if (StoreID != 0)
                    {
                        WhereCondition += " and P.StoreID =" + StoreID ;
                        WhereCondition1 += " and P.StoreID =" + StoreID;
                    }
                }               
                ht.Add("@Transaction", Transaction);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition1", WhereCondition1);
                ds = PettyExpenses.GetTransaction("SP_GetDataPettyExpenses", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public string EPettyInsert()
        {
            ds = new DataSet();
            ht = new Hashtable();
            PettyExpenses = new DLogin();
            try
            {
                ht.Add("@GridData",GridData); 
                ht.Add("@StoreID", StoreID);
                ht.Add("@Month", Month);
                ht.Add("@Year", Year);
                ht.Add("@PettyExpensesDetailsID", 0);
                ht.Add("@LoginID", LoginID);
                ht.Add("@Transaction","PettyInsert");
                ds = PettyExpenses.GetTransaction("SP_PettyExpenses", ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return (ds.Tables[0].Rows[0]["Status"].ToString());
        }
        public string EPettyUpdate()
        {
            ds = new DataSet();
            ht = new Hashtable();
            PettyExpenses = new DLogin();
            try
            {

            }
            catch (Exception)
            {
                
                throw;
            }
            return "";
        }
        public string EPettyDelete()
        {
            ds = new DataSet();
            ht = new Hashtable();
            PettyExpenses = new DLogin();
            try
            {
                ht.Add("@LoginID", LoginID);
                ht.Add("@PettyExpensesDetailsID", PettyExpensesDetailsID);
                ht.Add("@Month", "");
                ht.Add("@Year", "");
                ht.Add("@StoreID", "");
                ht.Add("@Transaction", "DeletePettyExpenses");
                ht.Add("@GridData", "");
                ds = PettyExpenses.GetTransaction("SP_PettyExpenses", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return (ds.Tables[0].Rows[0]["Status"].ToString());
        }
        public DataSet EPettyPayment()
        {
            ds = new DataSet();
            ht = new Hashtable();
            PettyExpenses = new DLogin();
            try
            {
                
                WhereCondition = string.Empty;
                if (PettyExpensesDetailsID != 0)
                {
                    WhereCondition = PettyExpensesDetailsID.ToString();  
                }

                Transaction = "GetPettyPayment";
                ht.Add("@Transaction", Transaction);
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@WhereCondition1", "");
                ds = PettyExpenses.GetTransaction("SP_GetDataPettyExpenses", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public string SavePettyPayment()
        {
            ds = new DataSet();
            ht = new Hashtable();
            PettyExpenses = new DLogin();
            try
            {
                ht.Add("@LoginID", LoginID);
                ht.Add("@PettyExpensesDetailsID",PettyExpensesDetailsID);
                ht.Add("@Month","");
                ht.Add("@Year","");
                ht.Add("@StoreID","");
                ht.Add("@Transaction", "PettySavePayment");
                ht.Add("@GridData",GridData);
                ds = PettyExpenses.GetTransaction("SP_PettyExpenses", ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return (ds.Tables[0].Rows[0]["Status"].ToString());
        }
        public DataSet EGetPettyAmount()
        {
            ht = new Hashtable();
            ds = new DataSet();
            PettyExpenses = new DLogin();
            WhereCondition=string.Empty;
            try
            {
                WhereCondition = PettyExpensesDetailsID.ToString();
                  if (WhereCondition == "")
                  {
                      WhereCondition = string.Empty;
                  }
    	
                ht.Add("@WhereCondition",WhereCondition);
                ht.Add("@Transaction", "GetPettyAmount");
                ht.Add("@WhereCondition1", "");
                ds = PettyExpenses.GetTransaction("SP_GetDataPettyExpenses", ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }
        public DataSet GetExpenseAutoComplete()
        {
            try
            {
                ht = new Hashtable();
                ds = new DataSet();
                PettyExpenses = new DLogin();
                WhereCondition=string.Empty;
                if (ExpenseName != "")
                    WhereCondition = " and ExpenseType like '" + ExpenseName + "%'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetExpenseAutoComplete");
                ht.Add("@WhereCondition1", "");
                ds = PettyExpenses.GetTransaction("SP_GetDataPettyExpenses", ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }
        public DataSet GetExpenseID()
        {
            try
            {
                ht = new Hashtable();
                ds = new DataSet();
                PettyExpenses = new DLogin();
                WhereCondition=string.Empty;
                if (ExpenseName != "")
                    WhereCondition = " and ExpenseType ='" + ExpenseName + "'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetExpenseID");
                ht.Add("@WhereCondition1", "");
                ds = PettyExpenses.GetTransaction("SP_GetDataPettyExpenses", ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }
    }
}
