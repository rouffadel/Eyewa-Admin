using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ESaleDAL;
using System.Collections;
namespace ESaleEntity.DAL
{
   public class DLogin
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
