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
   public class ERoleScreenMapping
    {
        Hashtable ht;
        DataSet ds;
        DRoleScreenMapping Dobj;
        string WhereCondition = string.Empty;
         private string _RoleId;

        public string RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        private int _LoginSessionId;

        public int LoginSessionId
        {
            get { return _LoginSessionId; }
            set { _LoginSessionId = value; }
        }
        private string _ScreenDetails;

        public string ScreenDetails
        {
            get { return _ScreenDetails; }
            set { _ScreenDetails = value; }
        }
       
        public DataSet EFillRoles()
        {
            Dobj = new DRoleScreenMapping();
            ht = new Hashtable();
            ds = new DataSet();
            ht.Add("wherecondition", WhereCondition);
            ht.Add("@Transaction", "FillRoles");
            ds = Dobj.GetTransaction("SP_GetDataRoleScreenMapping", ht);
            return ds;
        }
        public DataSet EFillRepScreens()
        {
            Dobj = new DRoleScreenMapping();
            ht = new Hashtable();
            ds = new DataSet();
            WhereCondition = " and RS.ROleId=" + RoleId;
            ht.Add("@WhereCondition", WhereCondition);
            ht.Add("@Transaction", "Checkpermissions");
            ds = Dobj.GetTransaction("SP_GetDataRoleScreenMapping", ht);
            return ds;
        }
        public DataSet EFillPuserScreens()
        {
            Dobj = new DRoleScreenMapping();
            ht = new Hashtable();
            ds = new DataSet();
            WhereCondition = "  and RoleId=" + RoleId;
            ht.Add("wherecondition", WhereCondition);
            ht.Add("@Transaction", "PUserSelectedScreens");
            ds = Dobj.GetTransaction("SP_GetDataScreens", ht);
            return ds;
        }
        public DataSet EInsertRoleScreenMapping()
        {         
            Dobj = new DRoleScreenMapping();
            ht = new Hashtable();
            ds = new DataSet();
            ht.Add("@Transaction", "INSERT");
            ht.Add("@RoleScreenId", 1);
            ht.Add("@LOGINSESSIONID", LoginSessionId);
            ht.Add("@RoleId", RoleId);               
            ht.Add("@SCREENDETAILS", ScreenDetails);
            ds = Dobj.GetTransaction("SP_RoleScreenMapping", ht);
            return ds;
           
        }

    }
    }


