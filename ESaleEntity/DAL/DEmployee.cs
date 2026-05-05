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
   public class DEmployee
    {
        SQLHelper objSQLHelper;
        DataSet ds;
        public DataSet GetTransaction(string SpName, Hashtable ht)
        {
            objSQLHelper = new SQLHelper();
            ds = objSQLHelper.ExecuteSP(SpName, ht);
            return ds;

        }

    }
}
