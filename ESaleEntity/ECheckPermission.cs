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
   public class ECheckPermission
    {
        Hashtable ht;
        DataSet ds;
        SQLHelper SqlObj;
        DCheckPermission Dobj;

        #region properties
        string WhereCondition = string.Empty;
        private int _RoleId;

        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        private string _ScreenUrl;

        public string ScreenUrl
        {
            get { return _ScreenUrl; }
            set { _ScreenUrl = value; }
        }

        public string addPermission { get; set; }
        public string viewPermission { get; set; }
        public string EditPermission { get; set; }
        public string deletepermission { get; set; }

        #endregion
        #region Methods
        public DataSet Echeckpermissions()
        {
            ht = new Hashtable();
            Dobj = new DCheckPermission();
            ds = new DataSet();
            WhereCondition = " and  R.RoleId='" + RoleId + "' and SM.ScreenUrl like '../Screens/" + ScreenUrl + "%'";
            ht.Add("@wherecondition", WhereCondition);
            ht.Add("@Transaction", "Checkpermissions");
            ds = Dobj.GetTransaction("SP_GetDataRoleScreenMapping", ht);
            return ds;
        }
        #endregion



    }
}
