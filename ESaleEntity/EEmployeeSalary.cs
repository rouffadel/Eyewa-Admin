using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESaleEntity.DAL;
using System.Collections;
using System.Data;
using ESaleDAL;

namespace ESaleEntity
{
    public class EEmployeeSalary
    {
        DSalarySheet objDSalarySheet;
        DataSet ds;
        Hashtable ht;
        string Transaction;

        public string EmployeeSalaryID { get; set; }
        public string EmployeeID { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HRA { get; set; }
        public decimal TA { get; set; }
        public decimal DA { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal EducationCess { get; set; }
        public decimal TDS { get; set; }
        public decimal NetSalary { get; set; }
        public string LoginId { get; set; }

        public string RoleName { get; set; }



        public string WhereCondition { get; set; }

        public DataSet EFillEmployee()
        {
            objDSalarySheet = new DSalarySheet();
            ds = new DataSet();
            ht = new Hashtable();
            if (WhereCondition == null)
                WhereCondition = string.Empty;
            if (LoginId == "1")
                Transaction = "FillEmployees";
            else
            {
                WhereCondition += " and L.LoginID=" + LoginId;
                Transaction = "FillEmployeesForUser1";
            }
                ht.Add("@WhereCondition", WhereCondition);  
                ht.Add("@Transaction", Transaction);
   
            ds = objDSalarySheet.GetTransaction("SP_GetDataEmployeeSalary", ht);
            return ds;
        }


        public DataSet EFillSearchEmployee()
        {
            objDSalarySheet = new DSalarySheet();
            ds = new DataSet();
            ht = new Hashtable();
            try
            {
                WhereCondition = string.Empty;
                if (LoginId == "1")
                {
                    ht.Add("@WhereCondition", WhereCondition);
                    ht.Add("@Transaction", "FillSearchEmployees");
                }
                else
                {
                    WhereCondition = " and L.LoginId=" + LoginId;
                    ht.Add("@WhereCondition", WhereCondition);
                    ht.Add("@Transaction", "FillEmploy");
                }
                ds = objDSalarySheet.GetTransaction("SP_GetDataEmployeeSalary", ht);
            }
            catch (Exception ex)
            { 
            
            }
            return ds;
        }



        public DataSet EAddSalarySheet()
        {

            objDSalarySheet = new DSalarySheet();
            ds = new DataSet();
            ht = new Hashtable();
            ht.Add("@EmployeeSalaryID", "");
            ht.Add("@EmployeeID", EmployeeID);
            ht.Add("@BasicSalary", BasicSalary);
            ht.Add("@HRA", HRA);
            ht.Add("@TA", TA);
            ht.Add("@DA", DA);
            ht.Add("@GrossSalary", GrossSalary);
            ht.Add("@TDS", TDS);
            ht.Add("@EducationCess", EducationCess);
            ht.Add("@NetSalary", NetSalary);
            ht.Add("@LoginSessionId", LoginId);
            ht.Add("@Transaction", "Insert");
            ds = objDSalarySheet.GetTransaction("SP_EmployeeSalary", ht);
            return ds;
        }

        public DataSet EUpdateSalarySheet()
        {
            objDSalarySheet = new DSalarySheet();
            ds = new DataSet();
            ht = new Hashtable();
            objDSalarySheet = new DSalarySheet();
            ds = new DataSet();
            ht = new Hashtable();
            ht.Add("@EmployeeSalaryID", EmployeeSalaryID);
            ht.Add("@EmployeeID", EmployeeID);
            ht.Add("@BasicSalary", BasicSalary);
            ht.Add("@HRA", HRA);
            ht.Add("@TA", TA);
            ht.Add("@DA", DA);
            ht.Add("@GrossSalary", GrossSalary);
            ht.Add("@EducationCess", EducationCess);
            ht.Add("@TDS", TDS);
            ht.Add("@NetSalary", NetSalary);
            ht.Add("@LoginSessionId", LoginId);
            ht.Add("@Transaction", "Update");
            ds = objDSalarySheet.GetTransaction("SP_EmployeeSalary", ht);
            return ds;
        }
        public DataSet EDeleteSalarySheet()
        {
            objDSalarySheet = new DSalarySheet();
            ds = new DataSet();
            ht = new Hashtable();
            objDSalarySheet = new DSalarySheet();
            ds = new DataSet();
            ht = new Hashtable();
            EmployeeSalaryID=string.Empty;
            ht.Add("@EmployeeSalaryID",EmployeeSalaryID);
            ht.Add("@EmployeeID", EmployeeID);
            ht.Add("@BasicSalary", 0);
            ht.Add("@HRA", 0);
            ht.Add("@TA", 0);
            ht.Add("@DA", 0);
            ht.Add("@GrossSalary", 0);
            ht.Add("@EducationCess", 0);
            ht.Add("@TDS", 0);
            ht.Add("@NetSalary", 0);
            ht.Add("@LoginSessionId", LoginId);
            ht.Add("@Transaction", "Delete");
            ds = objDSalarySheet.GetTransaction("SP_EmployeeSalary", ht);
            return ds;
        }

        public DataSet EViewEditSalarySheet()
        {
            objDSalarySheet = new DSalarySheet();
            ds = new DataSet();
            ht = new Hashtable();            
               WhereCondition = string.Empty;
                Transaction = string.Empty;
                if (EmployeeID != "0")
                    WhereCondition = " and S.EmployeeID=" + EmployeeID;
                if (LoginId == "1")
                    Transaction = "FillGridData";
                else
                {
                    Transaction = "FillGridDataForUser";
                    WhereCondition += " and L.LoginID =" + LoginId;
                }
                ht.Add("@WhereCondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                   
            ds = objDSalarySheet.GetTransaction("SP_GetDataEmployeeSalary", ht);
            return ds;
        }



    }
    public class DSalarySheet
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
