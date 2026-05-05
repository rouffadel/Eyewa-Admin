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
   public class ERole
    {

        DRole  DObj;
        DataSet ds;
        Hashtable ht;
        string WhereCondition;
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        private string _Role;
        private string _RoleID;

        public string RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        private string _LoginId;
        private string _LoginSessionId;
        public string LoginSessionId
        {
            get { return _LoginSessionId; }
            set { _LoginSessionId = value; }
        }
        private string _SessionID;
        private int _UserID;
        private int _OrganizationId;
        private string _ROLENAME;

        public string ROLENAME
        {
            get { return _ROLENAME; }
            set { _ROLENAME = value; }
        }

        public DataSet EAddRoles()
        {
            DObj = new DRole();
            ht = new Hashtable();
            ds = new DataSet();
            ht.Add("@RoleId", "");
            ht.Add("@RoleName", ROLENAME);
            ht.Add("@LoginID", LoginSessionId);
            ht.Add("@Transaction", "Insert");
            ds = DObj.GetTransaction("Sp_Roles", ht);
            return ds;
        }

        public DataSet EroleGridview()
        {
            ht = new Hashtable();
            ds = new DataSet();
            DObj = new DRole();
            WhereCondition = "and ROLENAME like '" + ROLENAME + "%'";
            ht.Add("@WhereCondition", WhereCondition);
            ht.Add("@Transaction", "GetData");
            ds = new DataSet();
            ds = DObj.GetTransaction("sp_GetRoles", ht);
            return ds;
        }

        public DataSet EviewEditContacts()
        {
            DObj = new DRole();
            ht = new Hashtable();
            ds = new DataSet();
            WhereCondition = " and ROLEID=" + RoleID;
            ht.Add("wherecondition", WhereCondition);
            ht.Add("@Transaction", "GetData");
            ds = DObj.GetTransaction("sp_GetRoles", ht);
            return ds;
        }

        public DataSet EUpdateroles()
        {
            ht = new Hashtable();
            DObj = new DRole();
            ds = new DataSet();
            ht.Add("@RoleId", RoleID);
            ht.Add("@RoleName", ROLENAME);

            ht.Add("@LoginID", LoginSessionId);

            ht.Add("@Transaction", "UPDATE");
            ds = DObj.GetTransaction("Sp_Roles", ht);
            return ds;
        }

        public DataSet EDelete()
        {
            ht = new Hashtable();
            DObj = new DRole();
            ds = new DataSet();
            ht.Add("@RoleId", RoleID);
            ht.Add("@RoleName", "");
            ht.Add("@LoginID", LoginSessionId);
            ht.Add("@Transaction", "DELETE");
            ds = DObj.GetTransaction("Sp_Roles", ht);
            return ds;
        }



    }
}
