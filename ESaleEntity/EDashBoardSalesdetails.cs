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
    class EDashBoardSalesdetails
    {
        DLogin Sale;

        DataSet ds;
        Hashtable ht;
        string WhereCondition;
        string Transation;
        string _BrandName;
        //public string BrandName
        //{
        //    get { return _BrandName; }
        //    set { _BrandName = value; }
        //}
        //private int _StoreID;

        //public int StoreID
        //{
        //    get { return _StoreID; }
        //    set { _StoreID = value; }
        //}
    }
}
