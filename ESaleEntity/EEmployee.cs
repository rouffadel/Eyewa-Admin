using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESaleEntity.DAL;
using System.Collections;
using System.Data;

namespace ESaleEntity
{
    public class EEmployee
    {

        DEmployee DObjEmployee;
        DataSet ds;
        Hashtable ht;
        string WhereCondition;
        string lblMsg;
        string Transaction;

        #region Private Properties
        string _IncentiveType;

        public string IncentiveType
        {
            get { return _IncentiveType; }
            set { _IncentiveType = value; }
        }

        string _RelievedandTerminated;

        public string RelievedandTerminated
        {
            get { return _RelievedandTerminated; }
            set { _RelievedandTerminated = value; }
        }

        private string _EmployeeID;

        public string EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        private string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _MiddleName;

        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        private string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _DOB;

        public string DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        private string _DateOfJoining;

        public string DateOfJoining
        {
            get { return _DateOfJoining; }
            set { _DateOfJoining = value; }
        }
        private string _EmployeeNo;

        public string EmployeeNo
        {
            get { return _EmployeeNo; }
            set { _EmployeeNo = value; }
        }
        private string _Gender;

        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        private string _PANNo;

        public string PANNo
        {
            get { return _PANNo; }
            set { _PANNo = value; }
        }
        private string _TempAddressLine1;

        public string TempAddressLine1
        {
            get { return _TempAddressLine1; }
            set { _TempAddressLine1 = value; }
        }
        private string _TempAddressLine2;

        public string TempAddressLine2
        {
            get { return _TempAddressLine2; }
            set { _TempAddressLine2 = value; }
        }
        private string _PermanentAddressLine1;

        public string PermanentAddressLine1
        {
            get { return _PermanentAddressLine1; }
            set { _PermanentAddressLine1 = value; }
        }
        private string _PermanentAddressLine2;

        public string PermanentAddressLine2
        {
            get { return _PermanentAddressLine2; }
            set { _PermanentAddressLine2 = value; }
        }
        private string _TempCountry;

        public string TempCountry
        {
            get { return _TempCountry; }
            set { _TempCountry = value; }
        }
        private string _TempState;

        public string TempState
        {
            get { return _TempState; }
            set { _TempState = value; }
        }
        private string _TempCity;

        public string TempCity
        {
            get { return _TempCity; }
            set { _TempCity = value; }
        }
        private string _PermanentCountry;

        public string PermanentCountry
        {
            get { return _PermanentCountry; }
            set { _PermanentCountry = value; }
        }
        private string _PermanentState;

        public string PermanentState
        {
            get { return _PermanentState; }
            set { _PermanentState = value; }
        }
        private string _PermanentCity;

        public string PermanentCity
        {
            get { return _PermanentCity; }
            set { _PermanentCity = value; }
        }
        private string _TempPinCode;

        public string TempPinCode
        {
            get { return _TempPinCode; }
            set { _TempPinCode = value; }
        }
        private string _PermanentPinCode;

        public string PermanentPinCode
        {
            get { return _PermanentPinCode; }
            set { _PermanentPinCode = value; }
        }
        private string _MobileNo;

        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        private string _AlternateMobileNo;

        public string AlternateMobileNo
        {
            get { return _AlternateMobileNo; }
            set { _AlternateMobileNo = value; }
        }
        private string _PersonalEmailID;

        public string PersonalEmailID
        {
            get { return _PersonalEmailID; }
            set { _PersonalEmailID = value; }
        }
        private string _WorkEmailID;

        public string WorkEmailID
        {
            get { return _WorkEmailID; }
            set { _WorkEmailID = value; }
        }
        private decimal _TenthPercentage;

        public decimal TenthPercentage
        {
            get { return _TenthPercentage; }
            set { _TenthPercentage = value; }
        }
        private string _TenthBord;

        public string TenthBord
        {
            get { return _TenthBord; }
            set { _TenthBord = value; }
        }
        private decimal _TwelfthPercentage;

        public decimal TwelfthPercentage
        {
            get { return _TwelfthPercentage; }
            set { _TwelfthPercentage = value; }
        }
        private string _TwelfthBord;

        public string TwelfthBord
        {
            get { return _TwelfthBord; }
            set { _TwelfthBord = value; }
        }
        private string _GraduationCourse;

        public string GraduationCourse
        {
            get { return _GraduationCourse; }
            set { _GraduationCourse = value; }
        }
        private decimal _GradutionPercentage;

        public decimal GradutionPercentage
        {
            get { return _GradutionPercentage; }
            set { _GradutionPercentage = value; }
        }

        private string _GradutionBord;
        public string GradutionBord
        {
            get { return _GradutionBord; }
            set { _GradutionBord = value; }
        }
        private string _PostGraduationCourse;

        public string PostGraduationCourse
        {
            get { return _PostGraduationCourse; }
            set { _PostGraduationCourse = value; }
        }
        private decimal _PostGradutionPercentage;

        public decimal PostGradutionPercentage
        {
            get { return _PostGradutionPercentage; }
            set { _PostGradutionPercentage = value; }
        }
        private string _PostGradutionBord;

        public string PostGradutionBord
        {
            get { return _PostGradutionBord; }
            set { _PostGradutionBord = value; }
        }
        private int _NoOfOrganisations;

        public int NoOfOrganisations
        {
            get { return _NoOfOrganisations; }
            set { _NoOfOrganisations = value; }
        }
        private string _ContactName;

        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }
        private string _ContactNo;

        public string ContactNo
        {
            get { return _ContactNo; }
            set { _ContactNo = value; }
        }
        private string _RelationShip;

        public string RelationShip
        {
            get { return _RelationShip; }
            set { _RelationShip = value; }
        }

        private int _LoginSessionId;

        public int LoginSessionId
        {
            get { return _LoginSessionId; }
            set { _LoginSessionId = value; }
        }

        private string _GridWorkExperienceDetails;

        public string GridWorkExperienceDetails
        {
            get { return _GridWorkExperienceDetails; }
            set { _GridWorkExperienceDetails = value; }
        }

        private string _Others;

        public string Others
        {
            get { return _Others; }
            set { _Others = value; }
        }

        private int _WorkExperienceId;

        public int WorkExperienceId
        {
            get { return _WorkExperienceId; }
            set { _WorkExperienceId = value; }
        }


        private int _CountryId;

        public int CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }

        private int _StateId;

        public int StateId
        {
            get { return _StateId; }
            set { _StateId = value; }
        }


        private int _CityId;

        public int CityId
        {
            get { return _CityId; }
            set { _CityId = value; }
        }

        private string _RelievingDate;

        public string RelievingDate
        {
            get { return _RelievingDate; }
            set { _RelievingDate = value; }
        }

        private string _ResignationDate;

        public string ResignationDate
        {
            get { return _ResignationDate; }
            set { _ResignationDate = value; }
        }

        private string _ReasonsForLeaving;

        public string ReasonsForLeaving
        {
            get { return _ReasonsForLeaving; }
            set { _ReasonsForLeaving = value; }
        }


        private string _CanBeReHired;

        public string CanBeReHired
        {
            get { return _CanBeReHired; }
            set { _CanBeReHired = value; }
        }

        private string _ExitFormalitiesCompleted;

        public string ExitFormalitiesCompleted
        {
            get { return _ExitFormalitiesCompleted; }
            set { _ExitFormalitiesCompleted = value; }
        }

        private bool _EmployeeActive;

        public bool EmployeeActive
        {
            get { return _EmployeeActive; }
            set { _EmployeeActive = value; }
        }


        private int _DesignationTypeId;

        public int DesignationTypeId
        {
            get { return _DesignationTypeId; }
            set { _DesignationTypeId = value; }
        }

        private string _Designation;

        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        private string _BankName;

        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

        private string _Branch;

        public string Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }

        private string _NameAsInAccount;

        public string NameAsInAccount
        {
            get { return _NameAsInAccount; }
            set { _NameAsInAccount = value; }
        }

        private string _AccountNo;

        public string AccountNo
        {
            get { return _AccountNo; }
            set { _AccountNo = value; }
        }
        private int _StoreID;
        public int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }
        private bool _Active;

        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        private bool _InActive;

        public bool InActive
        {
            get { return _InActive; }
            set { _InActive = value; }
        }
        #endregion


        public DataSet insertEmployee()
        {

            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;


                ht.Add("@EmployeeID", 0);
                ht.Add("@FirstName", FirstName);
                ht.Add("@MiddleName", MiddleName);
                ht.Add("@LastName", LastName);
                ht.Add("@DOB", DOB);
                ht.Add("@DateOfJoining", DateOfJoining);
                ht.Add("@EmployeeNo", EmployeeNo);
                ht.Add("@Gender", Gender);

                ht.Add("@DesignationTypeId", DesignationTypeId);
                ht.Add("@StoreID", StoreID);
                ht.Add("@BankName", BankName);
                ht.Add("@Branch", Branch);
                ht.Add("@NameAsInAccount", NameAsInAccount);
                ht.Add("@AccountNo", AccountNo);

                ht.Add("@PANNo", PANNo);

                ht.Add("@TempAddressLine1", @TempAddressLine1);
                ht.Add("@TempAddressLine2", TempAddressLine2);
                ht.Add("@PermanentAddressLine1", PermanentAddressLine1);
                ht.Add("@PermanentAddressLine2", PermanentAddressLine2);
                ht.Add("@TempCountry", TempCountry);
                ht.Add("@TempState", TempState);
                ht.Add("@TempCity", TempCity);
                ht.Add("@PermanentCountry", PermanentCountry);
                ht.Add("@PermanentState", PermanentState);
                ht.Add("@PermanentCity", PermanentCity);
                ht.Add("@TempPinCode", TempPinCode);
                ht.Add("@PermanentPinCode", PermanentPinCode);
                ht.Add("@MobileNo", MobileNo);
                ht.Add("@AlternateMobileNo", AlternateMobileNo);
                ht.Add("@PersonalEmailID", PersonalEmailID);
                ht.Add("@WorkEmailID", WorkEmailID);
                ht.Add("@TenthPercentage", TenthPercentage);
                ht.Add("@TenthBord", TenthBord);
                ht.Add("@TwelfthPercentage", TwelfthPercentage);
                ht.Add("@TwelfthBord", TwelfthBord);
                ht.Add("@GraduationCourse", GraduationCourse);
                ht.Add("@RelievedOrTerminated", RelievedandTerminated);




                ht.Add("@GradutionPercentage", GradutionPercentage);
                ht.Add("@GradutionBord", GradutionBord);
                ht.Add("@PostGraduationCourse", PostGraduationCourse);
                ht.Add("@PostGradutionPercentage", PostGradutionPercentage);
                ht.Add("@PostGradutionBord", PostGradutionBord);
                ht.Add("@Others", Others);
                ht.Add("@NoOfOrganisations", NoOfOrganisations);
                ht.Add("@ContactName", ContactName);
                ht.Add("@ContactNo", ContactNo);
                ht.Add("@RelationShip", RelationShip);
                ht.Add("@Transaction", "INSERT");
                ht.Add("@LoginSessionId", LoginSessionId);
                ht.Add("@GridExperienceDetails", GridWorkExperienceDetails);
                ht.Add("@WorkExperienceId", 0);


                ht.Add("@ResignationDate", ResignationDate);
                ht.Add("@RelievingDate", RelievingDate);
                ht.Add("@ReasonForLeaving", ReasonsForLeaving);
                ht.Add("@CanbeHired", CanBeReHired);
                ht.Add("@ExitFormalitiesCompleted", ExitFormalitiesCompleted);

                ht.Add("@EmployeeActive", EmployeeActive);

                ds = DObjEmployee.GetTransaction("sp_Employee", ht);
            }
            catch (Exception)
            {

                throw;
            }

            return ds;
        }

        public DataSet updateEmployee()
        {

            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;

                ht.Add("@EmployeeID", EmployeeID);
                ht.Add("@FirstName", FirstName);
                ht.Add("@MiddleName", MiddleName);
                ht.Add("@LastName", LastName);
                ht.Add("@DOB", DOB);
                ht.Add("@DateOfJoining", DateOfJoining);
                ht.Add("@EmployeeNo", @EmployeeNo);
                ht.Add("@Gender", Gender);

                ht.Add("@DesignationTypeId", DesignationTypeId);
                ht.Add("@StoreID", StoreID);
                ht.Add("@BankName", BankName);
                ht.Add("@Branch", Branch);
                ht.Add("@NameAsInAccount", NameAsInAccount);
                ht.Add("@AccountNo", AccountNo);

                ht.Add("@PANNo", PANNo);


                ht.Add("@TempAddressLine1", @TempAddressLine1);
                ht.Add("@TempAddressLine2", TempAddressLine2);
                ht.Add("@PermanentAddressLine1", PermanentAddressLine1);
                ht.Add("@PermanentAddressLine2", PermanentAddressLine2);
                ht.Add("@TempCountry", TempCountry);
                ht.Add("@TempState", TempState);
                ht.Add("@TempCity", TempCity);
                ht.Add("@PermanentCountry", PermanentCountry);
                ht.Add("@PermanentState", PermanentState);
                ht.Add("@PermanentCity", PermanentCity);
                ht.Add("@TempPinCode", TempPinCode);
                ht.Add("@PermanentPinCode", PermanentPinCode);
                ht.Add("@MobileNo", MobileNo);
                ht.Add("@AlternateMobileNo", AlternateMobileNo);
                ht.Add("@PersonalEmailID", PersonalEmailID);
                ht.Add("@WorkEmailID", WorkEmailID);
                ht.Add("@TenthPercentage", TenthPercentage);
                ht.Add("@TenthBord", TenthBord);
                ht.Add("@TwelfthPercentage", TwelfthPercentage);
                ht.Add("@TwelfthBord", TwelfthBord);
                ht.Add("@GraduationCourse", GraduationCourse);
                ht.Add("@RelievedOrTerminated", RelievedandTerminated);

                ht.Add("@GradutionPercentage", GradutionPercentage);
                ht.Add("@GradutionBord", GradutionBord);
                ht.Add("@PostGraduationCourse", PostGraduationCourse);
                ht.Add("@PostGradutionPercentage", PostGradutionPercentage);
                ht.Add("@PostGradutionBord", PostGradutionBord);
                ht.Add("@Others", Others);
                ht.Add("@NoOfOrganisations", NoOfOrganisations);

                ht.Add("@ContactName", ContactName);

                ht.Add("@ContactNo", ContactNo);
                ht.Add("@RelationShip", RelationShip);
                ht.Add("@Transaction", "UPDATE");
                ht.Add("@LoginSessionId", LoginSessionId);
                ht.Add("@GridExperienceDetails", GridWorkExperienceDetails);
                ht.Add("@WorkExperienceId", 0);

                ht.Add("@ResignationDate", ResignationDate);
                ht.Add("@RelievingDate", RelievingDate);
                ht.Add("@ReasonForLeaving", ReasonsForLeaving);
                ht.Add("@CanbeHired", CanBeReHired);
                ht.Add("@ExitFormalitiesCompleted", ExitFormalitiesCompleted);

               
                ht.Add("@EmployeeActive", EmployeeActive);

                ds = DObjEmployee.GetTransaction("sp_Employee", ht);
            }
            catch (Exception)
            {

                throw;
            }

            return ds;

        }

        public DataSet deleteEmployee()
        {

            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;

                ht.Add("@EmployeeID", EmployeeID);
                ht.Add("@FirstName", "");
                ht.Add("@MiddleName", "");
                ht.Add("@LastName", "");
                ht.Add("@DOB", "");
                ht.Add("@DateOfJoining", "");
                ht.Add("@EmployeeNo", "");
                ht.Add("@Gender", "");

                ht.Add("@DesignationTypeId", 0);

                ht.Add("@BankName", "");
                ht.Add("@Branch", "");
                ht.Add("@NameAsInAccount", "");
                ht.Add("@AccountNo", "");

                ht.Add("@PANNo", "");


                ht.Add("@TempAddressLine1", "");
                ht.Add("@TempAddressLine2", "");
                ht.Add("@PermanentAddressLine1", "");
                ht.Add("@PermanentAddressLine2", "");
                ht.Add("@TempCountry", 0);
                ht.Add("@TempState", 0);
                ht.Add("@TempCity", 0);
                ht.Add("@PermanentCountry", 0);
                ht.Add("@PermanentState", 0);
                ht.Add("@PermanentCity", 0);
                ht.Add("@TempPinCode", "");
                ht.Add("@PermanentPinCode", "");
                ht.Add("@MobileNo", "");
                ht.Add("@AlternateMobileNo", "");
                ht.Add("@PersonalEmailID", "");
                ht.Add("@WorkEmailID", "");
                ht.Add("@TenthPercentage", "0.00");
                ht.Add("@TenthBord", "");
                ht.Add("@TwelfthPercentage", "0.00");
                ht.Add("@TwelfthBord", "");
                ht.Add("@GraduationCourse", "");
                ht.Add("@RelievedOrTerminated", "");

                ht.Add("@GradutionPercentage", "0.00");
                ht.Add("@GradutionBord", "");
                ht.Add("@PostGraduationCourse", "");
                ht.Add("@PostGradutionPercentage", "0.00");
                ht.Add("@PostGradutionBord", "");
                ht.Add("@Others", "");
                ht.Add("@NoOfOrganisations", 0);
                ht.Add("@ContactName", "");
                ht.Add("@ContactNo", "");
                ht.Add("@RelationShip", "");
                ht.Add("@Transaction", "DELETE");
                ht.Add("@LoginSessionId", LoginSessionId);
                ht.Add("@GridExperienceDetails", "");
                ht.Add("@WorkExperienceId", 0);

                ht.Add("@ResignationDate", "");
                ht.Add("@RelievingDate ", "");
                ht.Add("@ReasonForLeaving", "");
                ht.Add("@CanbeHired", "");
                ht.Add("@ExitFormalitiesCompleted", "");
               
                ht.Add("@EmployeeActive", "");

                ds = DObjEmployee.GetTransaction("sp_Employee", ht);
            }
            catch (Exception)
            {
                throw;
            }

            return ds;

        }

        public DataSet getId()
        {
            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;
                WhereCondition = string.Empty;

                ht.Add("@wherecondition", WhereCondition);

                ht.Add("@Transaction", "getEMPid");

                ds = DObjEmployee.GetTransaction("Sp_GetEmployee", ht);
            }
            catch (Exception)
            {

                throw;
            }

            return ds;

        }

        public DataSet getEmployees()
        {

            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;
                WhereCondition = string.Empty;
                Transaction = string.Empty;
                WhereCondition += " where E.EmployeeNo  like '" + EmployeeNo + "%'";

                if (EmployeeID != "0")
                {
                    WhereCondition += " and E.EmployeeID=" + EmployeeID;
                }
                //if (LoginSessionId!=0)
                //{
                //    WhereCondition += "and U.UserId=" + LoginSessionId;
                //}
                //if (Active == true && InActive == true)
                //    WhereCondition += " and E.isActive=1 or E.isDeleted=0";
                if (Active == true && InActive==false)
                    WhereCondition += " and E.isActive=1 ";
                if (InActive == true && Active==false)
                    WhereCondition += " and E.isActive=0 ";
                if (LoginSessionId == 1)
                    Transaction = "getEmployees";
                else
                {
                    Transaction = "getEmployeesForUser";
                    WhereCondition += " and L.LoginID =" + LoginSessionId;
                }
                ht.Add("@wherecondition", WhereCondition);

                ht.Add("@Transaction", Transaction);

                ds = DObjEmployee.GetTransaction("Sp_GetEmployee", ht);
            }
            catch (Exception)
            {

                throw;
            }

            return ds;

        }

        public DataSet getEmployeesForDropDown()
        {
            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;
                WhereCondition = string.Empty;
                //if (LoginSessionId != 0)
                //{
                //    WhereCondition += "and U.UserId=" + LoginSessionId;
                //}
                //else
                //{
                //    WhereCondition = string.Empty;
                //}
                if (LoginSessionId == 1)
                    Transaction = "getEmployeeForDropDown";
                else
                {
                    Transaction = "getEmployeeForDropDownForUser";
                    WhereCondition += " and L.LoginID =" + LoginSessionId;
                }
                
                ht.Add("@wherecondition", WhereCondition);
                ht.Add("@Transaction", Transaction);
                ds = DObjEmployee.GetTransaction("Sp_GetEmployee", ht);

            }
            catch (Exception)
            {

                throw;
            }

            return ds;
        }

        public DataSet viewEditEmployee()
        {
            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;
                WhereCondition = EmployeeID;
                ht.Add("@wherecondition", WhereCondition);
                ht.Add("@Transaction", "getEmployeeDetails");

                ds = DObjEmployee.GetTransaction("Sp_GetEmployee", ht);

            }
            catch (Exception)
            {

                throw;
            }

            return ds;
        }

        public DataSet deleteWorkExperienceDetail()
        {
            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;

                ht.Add("@EmployeeID", 0);
                ht.Add("@FirstName", "");
                ht.Add("@MiddleName", "");
                ht.Add("@LastName", "");
                ht.Add("@DOB", "12-12-2012");
                ht.Add("@DateOfJoining", "12-12-2012");
                ht.Add("@EmployeeNo", "");
                ht.Add("@Gender", "");

                ht.Add("@DesignationTypeId", 0);

                ht.Add("@BankName", "");
                ht.Add("@Branch", "");
                ht.Add("@NameAsInAccount", "");
                ht.Add("@AccountNo", "");

                ht.Add("@PANNo", "");


                ht.Add("@TempAddressLine1", "");
                ht.Add("@TempAddressLine2", "");
                ht.Add("@PermanentAddressLine1", "");
                ht.Add("@PermanentAddressLine2", "");
                ht.Add("@TempCountry", 0);
                ht.Add("@TempState", 0);
                ht.Add("@TempCity", 0);
                ht.Add("@PermanentCountry", 0);
                ht.Add("@PermanentState", 0);
                ht.Add("@PermanentCity", 0);
                ht.Add("@TempPinCode", "");
                ht.Add("@PermanentPinCode", "");
                ht.Add("@MobileNo", "");
                ht.Add("@AlternateMobileNo", "");
                ht.Add("@PersonalEmailID", "");
                ht.Add("@WorkEmailID", "");
                ht.Add("@TenthPercentage", "0.00");
                ht.Add("@TenthBord", "");
                ht.Add("@TwelfthPercentage", "0.00");
                ht.Add("@TwelfthBord", "");
                ht.Add("@GraduationCourse", "");


                ht.Add("@GradutionPercentage", "0.00");
                ht.Add("@GradutionBord", "");
                ht.Add("@PostGraduationCourse", "");
                ht.Add("@PostGradutionPercentage", "0.00");
                ht.Add("@PostGradutionBord", "");
                ht.Add("@Others", "");
                ht.Add("@NoOfOrganisations", "");
                ht.Add("@ContactName", "");
                ht.Add("@ContactNo", "");
                ht.Add("@RelationShip", "");
                ht.Add("@Transaction", "DELETEWorkExperienceDetail");
                ht.Add("@LoginSessionId", LoginSessionId);
                ht.Add("@GridExperienceDetails", "");
                ht.Add("@WorkExperienceId", WorkExperienceId);


                ht.Add("@ResignationDate", "");
                ht.Add("@RelievingDate", "");
                ht.Add("@ReasonForLeaving", "");
                ht.Add("@CanbeHired", "");
                ht.Add("@ExitFormalitiesCompleted", "");
              
                ht.Add("@EmployeeActive", 1);

                ds = DObjEmployee.GetTransaction("sp_Employee", ht);


            }
            catch (Exception)
            {

                throw;
            }

            return ds;

        }

        public DataSet getCountryDdl()
        {

            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;

                WhereCondition = string.Empty;

                ht.Add("@wherecondition", WhereCondition);

                ht.Add("@Transaction", "getCountryDdl");

                ds = DObjEmployee.GetTransaction("Sp_GetEmployee", ht);
            }
            catch (Exception)
            {

                throw;
            }

            return ds;

        }

        public DataSet getStateDdl()
        {

            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;

                WhereCondition = CountryId.ToString();

                ht.Add("@wherecondition", WhereCondition);

                ht.Add("@Transaction", "getStateDdl");

                ds = DObjEmployee.GetTransaction("Sp_GetEmployee", ht);
            }
            catch (Exception)
            {

                throw;
            }

            return ds;

        }

        public DataSet getCityDdl()
        {

            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;

                WhereCondition = StateId.ToString();

                ht.Add("@wherecondition", WhereCondition);

                ht.Add("@Transaction", "getCityDdl");

                ds = DObjEmployee.GetTransaction("Sp_GetEmployee", ht);
            }
            catch (Exception)
            {

                throw;
            }

            return ds;

        }

        public DataSet getDesignation()
        {

            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;

                WhereCondition = String.Empty;

                ht.Add("@wherecondition", WhereCondition);

                ht.Add("@Transaction", "getDesignation");

                ds = DObjEmployee.GetTransaction("Sp_GetEmployee", ht);
            }
            catch (Exception)
            {

                throw;
            }

            return ds;
        }
        public DataSet GetStoreName()
        {
            try
            {
                DObjEmployee = new DEmployee();
                ht = new Hashtable();
                ds = new DataSet();
                lblMsg = string.Empty;
                WhereCondition = string.Empty;
                Transaction = string.Empty;
                if (LoginSessionId == 1)
                {
                    Transaction = "GetStoreName";
                }
                else
                {
                    Transaction = "GetStoreNameForUser";
                    WhereCondition += " and L.LoginID =" + LoginSessionId;
                }
                ht.Add("@Transaction",Transaction);
                ht.Add("@WhereCondition",WhereCondition);
                ds = DObjEmployee.GetTransaction("Sp_GetEmployee", ht);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return ds;
        }

        public DataSet GetEmployeeNo()
        {
           DataSet ds = new DataSet();
           DEmployee DObjEmployee = new DEmployee();
           Hashtable ht = new Hashtable();
            WhereCondition=string.Empty;
            ht.Add("@WhereCondition", WhereCondition);
           ds = DObjEmployee.GetTransaction("Sp_GetEmpNo", ht);

            return ds;
        }

    }
}
