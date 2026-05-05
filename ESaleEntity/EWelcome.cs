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
    public class EWelcome
    {
        DDealers Dobj;
        DataSet ds;
        Hashtable ht;
        private string _DealerId;

        public string DealerId
        {
            get { return _DealerId; }
            set { _DealerId = value; }
        }
        string WhereCondition;
        public DataSet EFilldealers()
        {
           Dobj = new DDealers();
            ht = new Hashtable();      
           
            ht.Add("@Transaction", "GETDealers");
            ds = new DataSet();
            ds = Dobj.GetTransaction("SP_GetDataWelcome", ht);
            return ds;
        }
        public DataSet EFillalldealers()
        {
            Dobj = new DDealers();
            ht = new Hashtable();

            ht.Add("@Transaction", "GETAllDealers");
            ds = new DataSet();
            ds = Dobj.GetTransaction("SP_GetDataWelcome", ht);
            return ds;
        }
        public DataSet EFillvendorsPins()
        {
            Dobj = new DDealers();
            ht = new Hashtable();

            ht.Add("@Transaction", "GETVendorspins");
            ds = new DataSet();
            ds = Dobj.GetTransaction("SP_GetDataWelcome", ht);
            return ds;
        }

        public DataSet EFillavaliableamountandcreditlimt()
        {
            Dobj = new DDealers();
            ht = new Hashtable();
            ds = new DataSet();
            //WhereCondition = " AND DenominationCode LIKE '" + DenominationCode + "' And Category like '" + Category + "'and VoucherDetails.VendorID  like '%" + VendorID + "' ";

            WhereCondition = "and DealerID like '" + DealerId + "%'";
            //WhereCondition = "";
            ht.Add("@WhereCondition", WhereCondition);
            ht.Add("@Transaction", "Getavaliableamountandcreditlimt");
           
            ds = Dobj.GetTransaction("Sp_GetWelcome", ht);
            return ds;
        }
    }
}
