using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESaleDAL;
using ESaleEntity.DAL;

namespace ESaleEntity
{
    public class EUsers
    {
        DUser objDUser;
        Hashtable ht;
        DataSet dsEusers;

        string Transaction;
        private string _UserName;
        private string _LoginName;
        private string _Password;
        private int _RoleID;
        private string _RoleName;
        private string _Email;
        private string _MobileNo;
        private string _ContactNo;
        private string _Active;
        private int _LoginSessionID;
        private int _LoginID;
        private int _StoreID;
        private string _StoreOrOrganisation;
        public string StoreOrOrganisation
        {
            get { return _StoreOrOrganisation; }
            set { _StoreOrOrganisation=value;}
        }

        public int LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        public int LoginSessionID
        {
            get { return _LoginSessionID; }
            set { _LoginSessionID = value; }
        }



        public string Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public string ContactNo
        {
            get { return _ContactNo; }
            set { _ContactNo = value; }
        }

        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }

        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
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

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }
        string WhereCondition = string.Empty;
        public DataSet FillRoleName()
        {
            ht = new Hashtable();
            objDUser = new DUser();
            WhereCondition = "";
            ht.Add("@WhereCondition", WhereCondition);
            ht.Add("@Transaction", "fillRoles");
            dsEusers = new DataSet();
            dsEusers = objDUser.GetTransaction("sp_GetUsers", ht);
            return dsEusers;
        }

        public DataSet GetGridData()
        {
            Transaction = string.Empty;
            WhereCondition = string.Empty;
            if (UserName != "")
                WhereCondition += "And L.UserName LIKE '" + UserName + "%'";
            if (LoginName != "")
                WhereCondition += "And L.LoginName LIKE '" + LoginName + "%'";
            if (RoleID != 0)
                WhereCondition += "And R.RoleID ='" + RoleID ;
            if (StoreID !=0)
            {
                WhereCondition += "And L.StoreID =" + StoreID ;
            }
            Transaction = "SELECTDATA";
            try
            {
                objDUser = new DUser();
                ht = new Hashtable();
                dsEusers = new DataSet();
                ht.Add("@wherecondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                dsEusers = objDUser.GetTransaction("sp_GetUsers", ht);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsEusers;
        }
        //function written to Save data 
        public DataSet ESaveUsers()
        {
            objDUser = new DUser();
            dsEusers = new DataSet();
            ht = new Hashtable();
            ht.Add("@LoginID", LoginID);
            ht.Add("@UserName", UserName);
            ht.Add("@LoginName", LoginName);
            ht.Add("@Password", Password);
            ht.Add("@RoleID", RoleID);
            ht.Add("@StoreID", StoreID);
            ht.Add("@Email", Email);
            ht.Add("@MobileNo", MobileNo);
            ht.Add("@ContactNo", ContactNo);
            ht.Add("@Active", Active);
            ht.Add("@LoginSessionID", LoginSessionID);
            ht.Add("@Transaction", "Insert");
            dsEusers = objDUser.GetTransaction("sp_Users", ht);
            return dsEusers;


        }
        //function written to Update data  
        public DataSet EUpdateUsers()
        {
            objDUser = new DUser();
            ht = new Hashtable();
            dsEusers = new DataSet();
            ht.Add("@LoginID", LoginID);
            ht.Add("@UserName", UserName);
            ht.Add("@LoginName", LoginName);
            ht.Add("@Password", Password);
            ht.Add("@RoleID", RoleID);
            ht.Add("@StoreID", StoreID);
            ht.Add("@Email", Email);
            ht.Add("@MobileNo", MobileNo);
            ht.Add("@ContactNo", ContactNo);
            ht.Add("@Active", Active);
            ht.Add("@LoginSessionID", LoginSessionID);
            ht.Add("@Transaction", "UPDATE");
            dsEusers = objDUser.GetTransaction("sp_Users", ht);
            return dsEusers;
        }
        public DataSet DeleteUser()
        {
            objDUser = new DUser();
            ht = new Hashtable();
            dsEusers = new DataSet();
            ht.Add("@LoginID", LoginID);
            ht.Add("@UserName", "");
            ht.Add("@LoginName", "");
            ht.Add("@Password", "");
            ht.Add("@RoleID", "");
            ht.Add("@Email", "");
            ht.Add("@MobileNo", "");
            ht.Add("@ContactNo", "");
            ht.Add("@Active", "");
            ht.Add("@StoreID", 0);
            ht.Add("@LoginSessionID", LoginSessionID);
            //ht.Add("@WhereCondition", WhereCondition);
            ht.Add("@Transaction", "DELETE");
            dsEusers = objDUser.GetTransaction("sp_Users", ht);
            return dsEusers;

        }

        public DataSet EviewEditContacts()
        {
            objDUser = new DUser();
            ht = new Hashtable();
            dsEusers = new DataSet();
            WhereCondition = " and L.LoginID=" + LoginID;
            ht.Add("wherecondition", WhereCondition);
            ht.Add("@Transaction", "ViewOrEditUser");
            dsEusers = objDUser.GetTransaction("sp_GetUsers", ht);
            return dsEusers;
        }
        // Fill Store Dropdownlist control.
        public DataSet EFillStoreDetails()
        {
            objDUser = new DUser();
            ht=new Hashtable();
            dsEusers = new DataSet();
            try
            {
                WhereCondition = "";
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "FillStore");
                dsEusers = objDUser.GetTransaction("sp_GetUsers", ht);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return dsEusers;
        }
        public DataSet EGetStoreImages()
        {
            objDUser = new DUser();
            ht = new Hashtable();
            dsEusers = new DataSet();
            try
            {
                WhereCondition = string.Empty;
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", "GetStoreImages");
                dsEusers = objDUser.GetTransaction("SP_GetDataLogin",ht);
            }
            catch (Exception)
            {
                
                throw;
            }
            return dsEusers;
        }
    }
}
