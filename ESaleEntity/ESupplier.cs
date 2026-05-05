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
    public class ESupplier
    {
        DUser Supplier;
        DataSet ds;
        Hashtable ht;
        string WhereCondition;
        string Transaction;
        string Result;

        int _SupplierID;

        public int SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        int _SupplierProductID;

        public int SupplierProductID
        {
            get { return _SupplierProductID; }
            set { _SupplierProductID = value; }
        }
        int _CategoryID;

        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        int _ProductID;

        public int ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        string _SupplierName;

        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        string _City;

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        string _VatID;

        public string VatID
        {
            get { return _VatID; }
            set { _VatID = value; }
        }
        string _ContactNo;

        public string ContactNo
        {
            get { return _ContactNo; }
            set { _ContactNo = value; }
        }
        string _ContactPerson;

        public string ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }
        string _MobileNo;

        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        string _ZipCode;

        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        string _EmailID;

        public string EmailID
        {
            get { return _EmailID; }
            set { _EmailID = value; }
        }
        int _LoginID;

        public int LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        string _GridData;

        public string GridData
        {
            get { return _GridData; }
            set { _GridData = value; }
        }
        //methds 

        public DataSet GetCategoryDDL()
        {   
            ds=new DataSet();
            try 
	        {
                ht = new Hashtable();
                Supplier = new DUser();
                WhereCondition=string.Empty;
                Transaction="DDLCategory";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction",Transaction);
                ds = Supplier.GetTransaction("SP_GetDataSuppliers", ht);
	        }
	        catch (Exception)
	        {
        		
		        throw;
	        }
            return ds;
        }

        public DataSet GetSupplierDDL()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                Supplier = new DUser();
                WhereCondition = string.Empty;
                Transaction = "DDLSuppliers";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = Supplier.GetTransaction("SP_GetDataSuppliers", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public DataSet GetProductDDL()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                Supplier = new DUser();
                WhereCondition = string.Empty;
                WhereCondition = " and CategoryID =" + CategoryID;
                Transaction = "DDLProduct";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = Supplier.GetTransaction("SP_GetDataSuppliers", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public DataSet GetProductValue()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                Supplier = new DUser();
                WhereCondition = string.Empty;
                WhereCondition = " and ProductID =" + ProductID;
                Transaction = "GetProductValueBasedOnProduct";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = Supplier.GetTransaction("SP_GetDataSuppliers", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public DataSet GetSupplierGrid()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                Supplier = new DUser();
                WhereCondition = string.Empty;
                if(SupplierID!=0)
                WhereCondition = " and S.SupplierId =" + SupplierID;
                if (ContactNo != "")
                    WhereCondition += " and S.ContactNo like '" + ContactNo + "%'";
                if (MobileNo != "")
                    WhereCondition += " and S.MobileNo like '" + MobileNo + "%'";
                if (EmailID != "")
                    WhereCondition += " and S.EmailID like '" + EmailID + "%'";
                if (CategoryID != 0)
                    WhereCondition += " and C.CategoryID=" + CategoryID;
                if (ProductID != 0)
                    WhereCondition += " and P.ProductID=" + ProductID;
                Transaction = "GetSupplierGrid";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = Supplier.GetTransaction("SP_GetDataSuppliers", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public DataSet GetSupplierForViewEdit()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                Supplier = new DUser();
                WhereCondition = string.Empty;
                WhereCondition = " and Sp.SupplierId =" + SupplierID;
                Transaction = "GetSupplierForViewEdit";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = Supplier.GetTransaction("SP_GetDataSuppliers", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }

        public string Insert()
        {
            Result = string.Empty;
            try
            {
                Supplier = new DUser();
                ht = new Hashtable();
                ds = new DataSet();
                ht.Add("@SupplierID", 0);
                ht.Add("@SupplierName", SupplierName);
                 ht.Add("@Address",Address);
                 ht.Add("@City",City );
                 ht.Add("@ContactPerson",ContactPerson);
                 ht.Add("@ContactNo",ContactNo);
                 ht.Add("@ZipCode",ZipCode);
                 ht.Add("@EmailID",EmailID);
                 ht.Add("@MobileNo",MobileNo);
                 ht.Add("@GridData",GridData);
                 ht.Add("@VatID",VatID);
                 ht.Add("@LoginID",LoginID );
                ht.Add("@SupplierProductID",SupplierProductID );
                ht.Add("@Transaction","INSERT");
                ds = Supplier.GetTransaction("SP_Supplier", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {
                
                throw;
            }
            return Result;
        }

        public string Update()
        {
            Result = string.Empty;
            try
            {
                Supplier = new DUser();
                ht = new Hashtable();
                ds = new DataSet();
                ht.Add("@SupplierID", SupplierID);
                ht.Add("@SupplierName", SupplierName);
                ht.Add("@Address", Address);
                ht.Add("@City", City);
                ht.Add("@ContactPerson", ContactPerson);
                ht.Add("@ContactNo", ContactNo);
                ht.Add("@ZipCode", ZipCode);
                ht.Add("@EmailID", EmailID);
                ht.Add("@MobileNo", MobileNo);
                ht.Add("@GridData", GridData);
                ht.Add("@VatID", VatID);
                ht.Add("@LoginID", LoginID);
                ht.Add("@SupplierProductID", SupplierProductID);
                ht.Add("@Transaction", "UPDATE");
                ds = Supplier.GetTransaction("SP_Supplier", ht);
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
                Supplier = new DUser();
                ht = new Hashtable();
                ds = new DataSet();
                ht.Add("@SupplierID", SupplierID);
                ht.Add("@SupplierName", "");
                ht.Add("@Address", "");
                ht.Add("@City", "");
                ht.Add("@ContactPerson", "");
                ht.Add("@ContactNo", "");
                ht.Add("@ZipCode", "");
                ht.Add("@EmailID", "");
                ht.Add("@MobileNo", "");
                ht.Add("@GridData", "");
                ht.Add("@VatID", "");
                ht.Add("@LoginID", "");
                ht.Add("@SupplierProductID", "");
                ht.Add("@Transaction", "DELETE");
                ds = Supplier.GetTransaction("SP_Supplier", ht);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }

        public string DeleteSupplierProduct()
        {
            Result = string.Empty;
            try
            {
                Supplier = new DUser();
                ht = new Hashtable();
                ds = new DataSet();
                ht.Add("@SupplierID", 0);
                ht.Add("@SupplierName", "");
                ht.Add("@Address", "");
                ht.Add("@City", "");
                ht.Add("@ContactPerson", "");
                ht.Add("@ContactNo", "");
                ht.Add("@ZipCode", "");
                ht.Add("@EmailID", "");
                ht.Add("@MobileNo", "");
                ht.Add("@GridData", "");
                ht.Add("@VatID", "");
                ht.Add("@LoginID", "");
                ht.Add("@SupplierProductID", SupplierProductID);
                ht.Add("@Transaction", "DELETESUPPLIERPRODUCT");
                ds = Supplier.GetTransaction("SP_Supplier", ht);
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
