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
    public class EChangePasssword
    {
        Hashtable ht;
        DataSet ds;
        SQLHelper SqlObj;
        DChangePassword Dobj;


        #region prvate variables
        string WhereCondition = string.Empty;
        private string _pwd;
        private string _LoginId;
        #endregion
        #region private Methods
        public string LoginId
        {
            get { return _LoginId; }
            set { _LoginId = value; }
        }

        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
        private string _NewPassword;

        public string NewPassword
        {
            get { return _NewPassword; }
            set { _NewPassword = value; }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        #endregion


        public bool EButtonSubmit()
        {
            ht = new Hashtable();
            SqlObj=new SQLHelper();
           // ds=new DataSet();
            
            bool isChanged= SqlObj.ExecuteNonQuery("Update Logins Set Password= '" + Pwd + "' where  LOGINID='" + LoginId + "'");


            return isChanged;

        }
        public DataSet EChangePassword()
        {
            Dobj=new DChangePassword();
            Hashtable ht = new Hashtable();
            ds = new DataSet();
            ht.Add("@LoginId", LoginId);
            ht.Add("@Password", Password);
            ht.Add("@NewPassword", NewPassword);          
            ht.Add("@Transaction", "ChangePassword");
            ds = Dobj.GetTransaction("SP_ChangePassword",ht);
            return ds;
        }
        public DataSet EGetOldPwd()
        {
            ht = new Hashtable();
            Dobj = new DChangePassword();
            ds = new DataSet();
            WhereCondition = "and  LOGINID='" + LoginId + "'";

            ht.Add("@WhereCondition", WhereCondition);
            ht.Add("@Transaction", "VERIFYLOGIN");
            ds = Dobj.GetTransaction("SP_GetDataNew", ht);

            return ds;
        }
        public DataSet EUserGetUserPreference()
        {
            ht = new Hashtable();
            Dobj = new DChangePassword();
            ht.Add("@wherecondition", LoginId);
            ht.Add("@Transaction", "GetUserPreference");
            ds = new DataSet();
            ds = Dobj.GetTransaction("SP_GetDataNew", ht);
            return ds;

        }

    }
    
}
