using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using ESaleDAL;

namespace ESaleEntity
{
   public class ECategory
    {
        // Entities Declaration.
        DataSet ds;
        Hashtable ht;
        SQLHelper sqlobj;
        String WhereCondition;
        private  String _CategoryName;
        private int _LoginSessionId;
        private int _CategoryId;
        // Properties Declaration.
        public int PLoginSessionId
        {
            set { _LoginSessionId = value ;}
            get { return _LoginSessionId; }
        }
        public String PCategoryName 
        {
            set { _CategoryName = value; }
            get { return _CategoryName; }
        }
        public int PCategoryId
        {
            set { _CategoryId = value; }
            get { return _CategoryId; }
        }
        // Getting the data into the database and return back to the respective function.
        public DataSet EFillGridView()
        {
            ds = new DataSet();
            ht = new Hashtable();
            sqlobj = new SQLHelper();
            try
            {
                WhereCondition = "and CategoryName LIKE '" + PCategoryName + "%'";
                ht.Add("@Transaction", "GetCategoryDetails");
                ht.Add("@WhereCondition", WhereCondition);
                ds = GetTransaction("SP_GetCategory", ht);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return ds;
        }
        // Execute the Stored procedure.
        public DataSet GetTransaction(string SPName,Hashtable htparam)
        {
            DataSet dsGEt = new DataSet();
            sqlobj = new SQLHelper();
           
            try
            {

                dsGEt = sqlobj.ExecuteSP(SPName, ht);
            }
            catch (Exception ex)
            {
    
                throw ex;
            }

            return dsGEt;
        }
       //Add Category to the database.
        public string EAddCategory()
        {
            ds = new DataSet();
            sqlobj = new SQLHelper();
            ht = new Hashtable();
            try
            {
                ht.Add("@CategoryId", 0);
                ht.Add("@CategoryName",PCategoryName);
                ht.Add("@LoginId", PLoginSessionId);
                ht.Add("@Transaction", "INSERT");
                ds = GetTransaction("SP_Category",ht);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return (ds.Tables[0].Rows[0]["Status"].ToString());
        }
       //Getting the Record from the database.
        public DataSet EViewEditCategory()
        {
            ds = new DataSet();
            ht = new Hashtable();
            sqlobj = new SQLHelper();
            try
            {
                WhereCondition = "and CategoryId=" + PCategoryId;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetCategoryDetails");
                ds = GetTransaction("SP_GetCategory", ht);
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return ds;
        }
        // Update the Record to the database.
        public string EUpdateCategory()
        {
            ds = new DataSet();
            ht = new Hashtable();
            sqlobj = new SQLHelper();
            try
            {
                ht.Add("@CategoryId",PCategoryId);
                ht.Add("@CategoryName",PCategoryName);
                ht.Add("@LoginId",PLoginSessionId);
                ht.Add("@Transaction","UPDATE");
                ds = GetTransaction("SP_Category", ht);
                return (ds.Tables[0].Rows[0]["Status"].ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
       // Disable the user Record.
        public DataSet EDeleteCategory()
        {
            ds = new DataSet();
            ht = new Hashtable();
            sqlobj = new SQLHelper();
            try
            {
                ht.Add("@CategoryId",PCategoryId);
                ht.Add("@CategoryName", "");
                ht.Add("@LoginId", PLoginSessionId);
                ht.Add("@Transaction", "DELETE");
                ds = GetTransaction("SP_Category", ht);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return ds;
        }
    }
}
