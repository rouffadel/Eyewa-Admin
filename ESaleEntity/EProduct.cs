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
    public class EProduct
    {
        DLogin product;
        DataSet ds;
        Hashtable ht;
        string WhereCondition;
        string Transaction;
        string Result;

        
        int _ProductID;

        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        string _ProductName;

        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        int _CategoryID;

        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        int _LoginID;

        public int LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        float _ProductValue;

        public float ProductValue
        {
            get { return _ProductValue; }
            set { _ProductValue = value; }
        }
        float _SellingDiscount;

        public float SellingDiscount
        {
            get { return _SellingDiscount; }
            set { _SellingDiscount = value; }
        }
        private int _BrandID;

        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }

        private decimal _MaxDiscountPer;

        public decimal MaxDiscountPer
        {
            get { return _MaxDiscountPer; }
            set { _MaxDiscountPer = value; }
        }
        bool _Active;

        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        public DataSet DDLCategory()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                product = new DLogin();
                WhereCondition=string.Empty;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "DDLCategory");
                ds = product.GetTransaction("SP_GetDataProducts", ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }

        public DataSet ddlBrand()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                product = new DLogin();
                WhereCondition = string.Empty;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "ddlBrand");
                ds = product.GetTransaction("SP_GetDataProducts", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetProductGrid()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                product = new DLogin();
                WhereCondition = string.Empty;
                if (CategoryID != 0)
                    WhereCondition = " and C.CategoryID =" + CategoryID;
                if (BrandID != 0)
                    WhereCondition += " and P.BrandID =" + BrandID;
                if (ProductName != "")
                    WhereCondition += " and P.ProductName like '" + ProductName + "%'";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetProductGrid");
                ds = product.GetTransaction("SP_GetDataProducts", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet GetProductForViewAndEdit()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                product = new DLogin();
                WhereCondition = string.Empty;
                WhereCondition = " and P.ProductID =" + ProductID;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetProductForViewEdit");
                ds = product.GetTransaction("SP_GetDataProducts", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public DataSet Insert()
        {
            Result = string.Empty;
            try
            {
                product = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@CategoryID", CategoryID);
                ht.Add("@ProductID",0 );
                ht.Add("@ProductName",ProductName );
                ht.Add("@ProductValue",ProductValue );
                ht.Add("@SellingDiscount",SellingDiscount );
                ht.Add("@LoginID",LoginID );
                ht.Add("@Transaction","INSERT" );
                ht.Add("@BrandID", BrandID);
                ht.Add("@Active", Active);
                //Parameter added by sajan on 28th oct 2014 for inserting maximum discount percentage value
                ht.Add("@MaxDiscountPer", MaxDiscountPer);
                ds = product.GetTransaction("SP_Products", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }

        public string Update()
        {
            Result = string.Empty;
            try
            {
                product = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@CategoryID", CategoryID);
                ht.Add("@ProductID", ProductID);
                ht.Add("@ProductName", ProductName);
                ht.Add("@ProductValue", ProductValue);
                ht.Add("@SellingDiscount", SellingDiscount);
                ht.Add("@LoginID", LoginID);
                ht.Add("@Transaction", "UPDATE");
                ht.Add("@BrandID", BrandID);
                ht.Add("@Active", Active);
                //Parameter added by sajan on 28th oct 2014 for inserting maximum discount percentage value
                ht.Add("@MaxDiscountPer", MaxDiscountPer);
                ds = product.GetTransaction("SP_Products", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }

        public string Delete()
        {
            Result = string.Empty;
            try
            {
                product = new DLogin();
                ds = new DataSet();
                ht = new Hashtable();
                ht.Add("@CategoryID", 0);
                ht.Add("@ProductID", ProductID);
                ht.Add("@ProductName", "");
                ht.Add("@ProductValue", 0);
                ht.Add("@SellingDiscount", 0);
                ht.Add("@LoginID", 0);
                ht.Add("@Transaction", "DELETE");
                ht.Add("@BrandID", 0);
                ht.Add("@Active", true);
                ht.Add("@MaxDiscountPer", 0);
                ds = product.GetTransaction("SP_Products", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }
    }
}
