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
    public class EBrand
    {
        // Entities Declaration.
        DataSet dsforBrand;
        Hashtable htforBrand;
        SQLHelper sqlobj;
        string WhereCondition;
        private string _BrandName;
        private int _LoginID;
        private int _BrandID;
        bool _IsActive;

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        // Properties Declaration.
        public string BrandName 
        { 
            get { return _BrandName; } 
            set { _BrandName=value; } 
        }
        public int LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        public int BrandID

        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }
        // Getting the data into the database and return back to the respective function.
        public DataSet EFillGridView()
        {
            dsforBrand = new DataSet();
            htforBrand = new Hashtable();
            sqlobj = new SQLHelper();
            try
            {
                WhereCondition = "and BrandName LIKE'" + BrandName + "%'";
                htforBrand.Add("@Transaction", "GetBrandData");
                htforBrand.Add("@WhereCondition", WhereCondition);
                dsforBrand=GetTransaction("Sp_GetDataBrand",htforBrand);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return dsforBrand;
        }
        // Execute the Stored Procedure using SQLHepler Class.
        public DataSet GetTransaction(string SPName,Hashtable htParam)
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
        // Adding data to the database.
        public string EAddBrand()
        {
            dsforBrand = new DataSet();
            sqlobj = new SQLHelper();
            htforBrand = new Hashtable();
            try
            {
                htforBrand.Add("@BrandID", 0);
                htforBrand.Add("@BrandName", BrandName);
                htforBrand.Add("@LoginID", LoginID);
                htforBrand.Add("@Active", IsActive);
                htforBrand.Add("@Transaction", "INSERT");
                dsforBrand = GetTransaction("SP_Brand", htforBrand);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return (dsforBrand.Tables[0].Rows[0]["Status"].ToString());
        }
        // Update the Brand Details.
        public string EUpdateBrand()
        {
            dsforBrand = new DataSet();
            htforBrand = new Hashtable();
            sqlobj = new SQLHelper();
            try
            {
                htforBrand.Add("@BrandID",BrandID);
                htforBrand.Add("@BrandName", BrandName);
                htforBrand.Add("@Active", IsActive);
                htforBrand.Add("@LoginID",0);
                htforBrand.Add("@Transaction", "UPDATE");
                dsforBrand = GetTransaction("SP_Brand", htforBrand);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return (dsforBrand.Tables[0].Rows[0]["Status"].ToString());
        }
        // Getting the Record from the Database.
        public DataSet EViewForEditBrand()
        {
            dsforBrand = new DataSet();
            htforBrand = new Hashtable();
            sqlobj = new SQLHelper();
            try
            {
                WhereCondition = "and BrandID=" + BrandID;
                htforBrand.Add("@WhereCondition",WhereCondition);
                htforBrand.Add("@Transaction", "GetBrandData"); 
                dsforBrand=GetTransaction("SP_GetDataBrand",htforBrand);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return dsforBrand;
        }
        // Disable the Brand Record.
        public string EDeleteBrand()
        {
            dsforBrand = new DataSet();
            htforBrand = new Hashtable();
            sqlobj = new SQLHelper();
            try
            {
                htforBrand.Add("@BrandID", BrandID);
                htforBrand.Add("@BrandName", "");
                htforBrand.Add("@LoginID", LoginID);
                htforBrand.Add("@Transaction", "DELETE");
                htforBrand.Add("@Active", true);
                dsforBrand = sqlobj.ExecuteSP("SP_Brand", htforBrand);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return (dsforBrand.Tables[0].Rows[0]["Status"].ToString());
        }
    }
}
