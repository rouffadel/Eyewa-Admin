using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using ESaleDAL;

namespace ESaleEntity
{
   public class EProcessSalary
    {
           DProcessSalary objDProcessSalary;
           DataSet ds;
           Hashtable ht;


           public string SalaryID { get; set; }
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


           public string Month { get; set; }
           public string Year { get; set; }
           public string GridData { get; set; }


           public decimal NoOfWorkingDays { get; set; }
           public string WhereCondition { get; set; }



           public DataSet EFillLineItemsGrid()
           {
               objDProcessSalary = new DProcessSalary();
               ds = new DataSet();
               ht = new Hashtable();
               if (WhereCondition == null)
                   WhereCondition = string.Empty;
               ht.Add("@WhereCondition", WhereCondition);
               ht.Add("@Transaction", "FillEmployeesSalaryProcessDetails");
               ds = objDProcessSalary.GetTransaction("SP_GetDataProcessSalary", ht);
               return ds;
           }

           public DataSet EFillEmployee()
           {
               objDProcessSalary = new DProcessSalary();
               WhereCondition = string.Empty;
               ds = new DataSet();
               ht = new Hashtable();
               try
               {
                   if (LoginId == "1")
                   {
                       WhereCondition = string.Empty;
                       ht.Add("@WhereCondition", WhereCondition);
                       ht.Add("@Transaction", "FillEmployees");
                   }
                   else
                   {
                       WhereCondition += " and L.LoginID =" + LoginId;
                       ht.Add("@WhereCondition", WhereCondition);
                       ht.Add("@Transaction", "FillEmployeesForUser");
                   }
                   ds = objDProcessSalary.GetTransaction("SP_GetDataProcessSalary", ht);
               }
               catch (Exception)
               {
                   
                   throw;
               }
              
               return ds;
           }




           public DataSet EAddProcessSalary()
           {

               objDProcessSalary = new DProcessSalary();
               ds = new DataSet();
               ht = new Hashtable();
               try
               {
                   ht.Add("@SalaryID", "");
                   ht.Add("@Month", Month);
                   ht.Add("@Year", Year);
                   ht.Add("@NoOfWorkingDays", NoOfWorkingDays);
                   ht.Add("@GridData", GridData);
                   ht.Add("@LoginSessionId", LoginId);
                   ht.Add("@Transaction", "Insert");
                   ds = objDProcessSalary.GetTransaction("SP_ProcessSalary", ht);
               }
               catch (Exception)
               {
                   
                   throw;
               }
               return ds;
           }

           public DataSet EUpdateProcessSalary()
           {
               objDProcessSalary = new DProcessSalary();
               ds = new DataSet();
               ht = new Hashtable();
               objDProcessSalary = new DProcessSalary();
               ds = new DataSet();
               ht = new Hashtable();
               try
               {
                   ht.Add("@SalaryID", SalaryID);
                   ht.Add("@Month", Month);
                   ht.Add("@Year", Year);
                   ht.Add("@NoOfWorkingDays", NoOfWorkingDays);
                   ht.Add("@GridData", GridData);
                   ht.Add("@LoginSessionId", LoginId);
                   ht.Add("@Transaction", "Update");
                   ds = objDProcessSalary.GetTransaction("SP_ProcessSalary", ht);
               }
               catch (Exception)
               {
                   
                   throw;
               }
               return ds;
           }
           public DataSet EDeleteProcessSalary()
           {
               objDProcessSalary = new DProcessSalary();
               ds = new DataSet();
               ht = new Hashtable();
               objDProcessSalary = new DProcessSalary();
               ds = new DataSet();
               ht = new Hashtable();
               try
               {
                   ht.Add("@SalaryID", SalaryID);
                   ht.Add("@Month", "");
                   ht.Add("@Year", "");
                   ht.Add("@NoOfWorkingDays", "");
                   ht.Add("@GridData", "");
                   ht.Add("@LoginSessionId", LoginId);
                   ht.Add("@Transaction", "Delete");
                   ds = objDProcessSalary.GetTransaction("SP_ProcessSalary", ht);
               }
               catch (Exception)
               {
                   
                   throw;
               }
               return ds;
           }

           public DataSet EViewEditProcessSalary()
           {
               objDProcessSalary = new DProcessSalary();
               ds = new DataSet();
               ht = new Hashtable();
               WhereCondition = string.Empty;

               try
               {
                  
                   if (LoginId == "1")
                   {
                       WhereCondition = string.Empty;
                       ht.Add("@WhereCondition", WhereCondition);
                       ht.Add("@Transaction", "FillGridData");
                   }
                   else 
                   {
                       WhereCondition = " and L.LoginID =" + LoginId;
                        ht.Add("@WhereCondition", WhereCondition);
                       ht.Add("@Transaction", "FillGridDataForUser"); 
                   }

                   ds = objDProcessSalary.GetTransaction("SP_GetDataProcessSalary", ht);
               }
               catch (Exception)
               {
                   
                   throw;
               }
               return ds;
           }


           public DataSet EPrintProcessSalary()
           {
               objDProcessSalary = new DProcessSalary();
               ds = new DataSet();
               ht = new Hashtable();
               try
               {
                   if (WhereCondition == null)
                       WhereCondition = string.Empty;
                   ht.Add("@WhereCondition", WhereCondition);
                   ht.Add("@Transaction", "PaySlipPrint");
                   ds = objDProcessSalary.GetTransaction("SP_GetDataProcessSalary", ht);
               }
               catch (Exception)
               {
                   
                   throw;
               }
               return ds;
           }

           public DataSet EGetGridDataForManualLineItems()
           {
               try
               {
                   objDProcessSalary = new DProcessSalary();
                   ds = new DataSet();
                   ht = new Hashtable();
                   WhereCondition = string.Empty;
                   if (LoginId != "1")
                   {
                       ht.Add("@WhereCondition", "");
                       ht.Add("@Transaction", "FillGridDataManualForUser");
                   }
                   else
                   {
                       ht.Add("@WhereCondition", "");
                       ht.Add("@Transaction", "FillGridManualData");
                   }
                   ds = objDProcessSalary.GetTransaction("SP_GetDataProcessSalary", ht);
               }
               catch (Exception ex)
               {

               }
               return ds;
           }

           public string SaveManualSalary()
           {
               string result = string.Empty;
               try
               {
                   objDProcessSalary = new DProcessSalary();
                   ds = new DataSet();
                   ht = new Hashtable();
                   ht.Add("@SalaryID", 0);
                   ht.Add("@Month", Month);
                   ht.Add("@Year", Year);
                   ht.Add("@NoOfWorkingDays", NoOfWorkingDays);
                   ht.Add("@GridData", GridData);
                   ht.Add("@LoginSessionId", LoginId);
                   ht.Add("@Transaction", "ManualInsert");
                   ds = objDProcessSalary.GetTransaction("SP_ProcessSalary", ht);
                   result = Convert.ToString(ds.Tables[1].Rows[0]["Status"]);
               }
               catch (Exception)
               {

                   throw;
               }
               return result;
           }
       
    }
   public class DProcessSalary
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
