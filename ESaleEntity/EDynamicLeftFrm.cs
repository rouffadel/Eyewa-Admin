using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESaleEntity.DAL;
using System.Collections;

namespace ESaleEntity
{
  public   class EDynamicLeftFrm
    {
        #region Objects

       DDynamicLeftFrm objDDynamicLeftForm;
        object obj;

        #endregion

        #region Public And Private Variables

        private string _RoleId;
        private string _ModuleId;
        string whereCondition;

        #endregion


        #region Properties

        public string ModuleId
        {
            get { return _ModuleId; }
            set { _ModuleId = value; }
        }

        public string RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        private string _UserId;

        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        #endregion

        public object EGetAllScreens(EDynamicLeftFrm objEDynamicLeftForm)
        {
            objDDynamicLeftForm = new DDynamicLeftFrm();
            obj = new object();
            Hashtable ht = new Hashtable();
            whereCondition = " ";
            ht.Add("@Transaction", "GetAllScreensForLeftForm");
            ht.Add("@wherecondition", whereCondition);
            obj = objDDynamicLeftForm.GetTransaction(ht, "SP_GetDataLeftFrm");
            return obj;
        }
        public object EGetAllModules(EDynamicLeftFrm objEDynamicLeftForm)
        {
            objDDynamicLeftForm = new DDynamicLeftFrm();
            obj = new object();
            Hashtable ht = new Hashtable();
            //whereCondition = " and LoginTypeId="+UserId;
            whereCondition = "";
            ht.Add("@Transaction", "GETALLMODULEFORPARTICULARROLE");
            ht.Add("@wherecondition", whereCondition);
            obj = objDDynamicLeftForm.GetTransaction(ht, "SP_GetDataLeftFrm");
            return obj;
        }
        public object EGetparticularUserModules(EDynamicLeftFrm objEDynamicLeftForm)
        {
            objDDynamicLeftForm = new DDynamicLeftFrm();
            obj = new object();
            Hashtable ht = new Hashtable();
            whereCondition = "  and  R.RoleId=" + RoleId; 
            ht.Add("@Transaction", "GETPUserModules");
            ht.Add("@wherecondition", whereCondition);
            obj = objDDynamicLeftForm.GetTransaction(ht, "SP_GetDataLeftFrm");
            return obj;
        }
        public object EGetparticularUserScreens(EDynamicLeftFrm objEDynamicLeftForm)
        {
            objDDynamicLeftForm = new DDynamicLeftFrm();
            obj = new object();
            Hashtable ht = new Hashtable();
            //whereCondition = " and MM.LoginTypeID=" + objEDynamicLeftForm.UserId;

            whereCondition = "  and  R.RoleId=" + RoleId; 
            ht.Add("@Transaction", "GETPUserScreens");
            ht.Add("@wherecondition", whereCondition);
            obj = objDDynamicLeftForm.GetTransaction(ht, "SP_GetDataLeftFrm");
            return obj;
        }
    }
}
