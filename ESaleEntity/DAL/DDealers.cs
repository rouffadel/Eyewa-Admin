using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ESaleDAL;
namespace ESaleEntity.DAL
{
    public class DDealers
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
