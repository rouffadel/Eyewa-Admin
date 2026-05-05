using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using ESaleDAL;
using ESaleEntity.DAL;
using System.Data;
using System.Data.SqlClient;
namespace ESaleEntity
{
    public class EStore
    {
        //Entity properties
        string _StoreCode;

        public string StoreCode
        {
            get { return _StoreCode; }
            set { _StoreCode = value; }
        }

        string _Filename;

        public string Filename
        {
            get { return _Filename; }
            set { _Filename = value; }
        }
        int _StoreID;

        public int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }
        string _StoreName;
        public string StoreName
        {
          get { return _StoreName; }
          set { _StoreName = value; }
        }
        string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
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
        string _City;

        public string City
        {
            get { return _City; }
            set { _City = value; }
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
        int _OrganisationID;

        public int OrganisationID
        {
            get { return _OrganisationID; }
            set { _OrganisationID = value; }
        }
        string _OrganisationName;

        public string OrganisationName
        {
            get { return _OrganisationName; }
            set { _OrganisationName = value; }
        }
          string _StoreDeliveryNoteNo;
        public string StoreDeliveryNoteNo
        {
            set { _StoreDeliveryNoteNo = value; ;}
            get { return _StoreDeliveryNoteNo;}
        }
        DateTime _StoreDeliveryNoteDate;
        public DateTime StoreDeliveryNoteDate
        {
            set { _StoreDeliveryNoteDate = value; }
            get { return _StoreDeliveryNoteDate; }
        }
        DateTime _StorePaymentDate;
        public DateTime StorePaymentDate
        {
            set { _StorePaymentDate = value; }
            get { return _StorePaymentDate; }
        }
        //Entity object Declaration
        DLogin Dstore;
        DataSet ds;
        Hashtable ht;
        string WhereCondition;
        string Result;
        String Transaction;
        //Method to fill organisation dropdownlist
        public DataSet OrganizationDDL()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                Transaction = "OrganisationDDL";
                Dstore = new DLogin();
                ht.Add("@Transaction", Transaction);
                ht.Add("@WhereCondition", string.Empty);
                ds = Dstore.GetTransaction("SP_GetDataStore", ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }
        // Method to Fill Store DropDownList based on OrganisationId.
        public DataSet StoreDDL()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                WhereCondition = "and o.OrganisationID=" + OrganisationID;
                Transaction = "GetStoreDetails";
                Dstore = new DLogin();
                ht.Add("@Transaction",Transaction);
                ht.Add("@WhereCondition",WhereCondition);
                ds = Dstore.GetTransaction("SP_GetStore", ht);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return ds;
        }

        public DataSet GetStoreGrid()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
               
                WhereCondition = string.Empty;
                if (StoreName != "")
                    WhereCondition = " and StoreName like '" + StoreName + "%'";
                if (ContactNo != "")
                    WhereCondition += " and ContactNumber like '" + ContactNo + "%'";
                if (EmailID != "")
                    WhereCondition += " and EmailID like '" + EmailID + "%'";
                if (LoginID == 1)
                    Transaction = "GetStoreGrid";
                else
                {
                    WhereCondition += " and L.LoginID=" + LoginID;
                    Transaction = "GetStoreGridForUser";
                }
                ht.Add("@Transaction", Transaction);
                ht.Add("@WhereCondition", WhereCondition);
                Dstore = new DLogin();
                ds = Dstore.GetTransaction("SP_GetDataStore", ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return ds;
        }

        public DataSet GetStoreForViewEdit()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                Transaction = "GetStoreGridForViewEdit";
                if (StoreID != 0)
                    WhereCondition = " and StoreID =" + StoreID;                
                ht.Add("@Transaction", Transaction);
                ht.Add("@WhereCondition", WhereCondition);
                Dstore = new DLogin();
                ds = Dstore.GetTransaction("SP_GetDataStore", ht);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet Save()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                Transaction = "INSERT";
                ht.Add("@StoreID", 0);
                ht.Add("@StoreName", StoreName);
                ht.Add("@Address", Address);
                ht.Add("@City", City);
                ht.Add("@ZipCode", ZipCode);
                ht.Add("@ContactNo", ContactNo);
                ht.Add("@ContactPerson", ContactPerson);
                ht.Add("@EmailID", EmailID);
                ht.Add("@LoginID", LoginID);
                ht.Add("@OrganisationID", OrganisationID);
                ht.Add("@Transaction", Transaction);
                ht.Add("@FileName", Filename);
                ht.Add("@StoreCode", StoreCode);
                Dstore = new DLogin();
                ds = Dstore.GetTransaction("SP_Store", ht);
                Result = string.Empty;
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public DataSet Update()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                Transaction = "UPDATE";
                ht.Add("@StoreID", StoreID);
                ht.Add("@StoreName", StoreName);
                ht.Add("@Address", Address);
                ht.Add("@City", City);
                ht.Add("@ZipCode", ZipCode);
                ht.Add("@ContactNo", ContactNo);
                ht.Add("@ContactPerson", ContactPerson);
                ht.Add("@EmailID", EmailID);
                ht.Add("@LoginID", LoginID);
                ht.Add("@OrganisationID", OrganisationID);
                ht.Add("@Transaction", Transaction);
                ht.Add("@FileName", Filename);
                ht.Add("@StoreCode", StoreCode);
                Dstore = new DLogin();
                ds = Dstore.GetTransaction("SP_Store", ht);
                Result = string.Empty;
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            }
            catch (Exception)
            {

                throw;
            }
            return ds;
        }
        public string Delete()
        {
            ds = new DataSet();
            try
            {
                ht = new Hashtable();
                Transaction = "DELETE";
                ht.Add("@StoreID", StoreID);
                ht.Add("@StoreName", "");
                ht.Add("@Address", "");
                ht.Add("@City", "");
                ht.Add("@ZipCode", "");
                ht.Add("@ContactNo", "");
                ht.Add("@ContactPerson", "");
                ht.Add("@EmailID", "");
                ht.Add("@LoginID", 0);
                ht.Add("@OrganisationID", 0);
                ht.Add("@Transaction", Transaction);
                ht.Add("@FileName", "");
                ht.Add("@StoreCode", "");
                Dstore = new DLogin();
                ds = Dstore.GetTransaction("SP_Store", ht);
                Result = string.Empty;
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
