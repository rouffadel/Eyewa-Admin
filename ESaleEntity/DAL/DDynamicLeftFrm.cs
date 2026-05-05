using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESaleDAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ESaleEntity.DAL
{
   public  class DDynamicLeftFrm
    {

        SQLHelper objSqlHelper;
        object obj;
        public Object GetTransaction(Hashtable ht, string SpName)
        {
            obj = new object();
            objSqlHelper = new SQLHelper();
            obj = objSqlHelper.ExecuteSP(SpName, ht);
            return obj;
        }
    }
}
