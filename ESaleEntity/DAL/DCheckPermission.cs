using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESaleDAL;
using System.Data;
using System.Collections;

namespace ESaleEntity.DAL
{
   public class DCheckPermission
    {
        SQLHelper SqlObj;
        DataSet ds;
        public DataSet GetTransaction(string SpName, Hashtable ht)
        {
            SqlObj = new SQLHelper();
            ds = new DataSet();
            ds = SqlObj.ExecuteSP(SpName, ht);
            return ds;
        }
    }
}
