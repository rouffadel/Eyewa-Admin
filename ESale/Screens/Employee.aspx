<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Employee.aspx.cs" Inherits="Admin_Employee" Title="Employee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--  <script src="../js/Validations.js" type="text/javascript"></script>

    <script src="../js/jquery-1.3.2.min.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        //        if (Page_ClientValidate()==true) {

        function validGender() {
            if (Page_ClientValidate() == true) {
                var x = document.getElementById("ctl00_ContentPlaceHolder1_radioMale").checked;
                var y = document.getElementById("ctl00_ContentPlaceHolder1_radioFemale").checked;
                if (x == false && y == false) {
                    alert("Please select any gender");
                    return false;

                }
                return false;
            }
            return true;
        }
        //  }


        function LettersWithSpaceOnly(evt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
          ((evt.which) ? evt.which : 0));
            if (charCode > 32 && (charCode < 65 || charCode > 90) &&
          (charCode < 97 || charCode > 122)) {
                alert('Enter Only Alphabets ');
                return false;
                evt.focus();
            }
            return true;
        }

        function ValidateUsernamePaste(obj) {
            var totalCharacterCount = window.clipboardData.getData('Text');
            var strValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-.";
            var strChar;
            var FilteredChars = "";
            for (i = 0; i < totalCharacterCount.length; i++) {
                strChar = totalCharacterCount.charAt(i);
                if (strValidChars.indexOf(strChar) != -1) {
                    FilteredChars = FilteredChars + strChar;
                }
            }
            obj.value = FilteredChars;
            return false;
        }

        function validateEmail(emailField) {

            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            if (reg.test(emailField.value) == false) {
                alert('Invalid Email Address');
                emailField.value = "";
                emailField.focus();
                return false;
            }
            return true;
        }

        function ValidateNumberOnly(evt) {
            if ((evt.keyCode < 48 || evt.keyCode > 57)) {
                alert('Enter Only Number ');
                evt.focus();
                evt.returnValue = false;
            }
        }
    </script>

    <asp:Label ID="lblTitle" CssClass="TitleClass" Text="Employee" Visible="false" runat="server"></asp:Label>


    <form runat="server" id="form">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Employee</strong>
                                </h3>
                                <ul class="nav nav-tabs pull-right" role="tablist" style="margin-top: 0px;">
                                    <li id="lirole" runat="server">

                                        <asp:Button ID="lnkAdd" runat="server" CssClass="btn btn-primary pull-right"
                                            Text="Add" OnClick="lnkAdd_Click" UseSubmitBehavior="false" />
                                    </li>
                                </ul>
                            </div>
                            <!-- Screen Heading  End-->

                            <!-- Notifications Start-->
                            <center>
                                <div class="col-md-12" id="dvFailure" style="text-align: center; margin-top: 1%"
                                    runat="server">
                                    <div class="alert alert-danger notification" role="alert">
                                        <button type="button" class="close" data-dismiss="alert">
                                            <span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                        <asp:Label ID="lblmsg" runat="server" Style="color: white;"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12" id="dvSuccess" runat="server" style="text-align: center; margin-top: 1%">
                                    <div class="alert alert-success notification" role="alert">
                                        <button type="button" class="close" data-dismiss="alert">
                                            <span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                        <asp:Label ID="lblSuccess" runat="server" Style="color: White;"></asp:Label>
                                    </div>
                                </div>
                            </center>

                            <div id="divAdd" class="page-content-wrap">
                                <div runat="server" id="pnlAdd" class="page-content-wrap">
                                    <br />
                                    <asp:Label ID="spnEmpDetails" runat="server" Style="font-size: 14px; margin-left: 25px;">Employee Details </asp:Label>
                                    <br />
                                    <br />
                                    <div id="pnlPersonalDetails" runat="server">
                                        <div style="border: 1px solid #ccc; padding: 15px;">
                                            <div class="row ">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            First Name</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtFirstName" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvtxtFirstName"
                                                                runat="server" ControlToValidate="txtFirstName"
                                                                ErrorMessage="Please Enter First Name" Text="*"
                                                                ValidationGroup="G2" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Middle Name</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtMiddleName" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <asp:RegularExpressionValidator ID="revtxtMiddleName"
                                                                runat="server" ControlToValidate="txtMiddleName"
                                                                ErrorMessage="Please Enter only Characteres"
                                                                Text="*" ValidationGroup="G2" ValidationExpression="^[a-zA-Z]+$"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Last Name</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtLastName" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvtxtLastName"
                                                                runat="server" ControlToValidate="txtLastName"
                                                                ErrorMessage="Please Enter Last Name" Text="*"
                                                                ValidationGroup="G2" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row ">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Gender</label>
                                                        <div class="col-md-8">
                                                            <asp:RadioButton ID="radioMale" runat="server" Text=""
                                                                GroupName="Gender"
                                                                CssClass="Label" Checked="true" AutoPostBack="True" />
                                                            <asp:Label ID="Label1" runat="server" Text="Male"
                                                                CssClass="Label"></asp:Label>

                                                            <asp:RadioButton ID="radioFemale" runat="server"
                                                                Text=""
                                                                GroupName="Gender" CssClass="Label" />
                                                            <asp:Label ID="Label2" runat="server" Text="Female"
                                                                CssClass="Label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            DOB</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group pull-right">
                                                                <asp:TextBox ID="txtDob" CssClass="form-control
                                datepicker"
                                                                    runat="server" AutoPostBack="true"
                                                                    onchange="return ValidateDate()"></asp:TextBox>
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvtxtDOB" runat="server"
                                                                ControlToValidate="txtDob"
                                                                ErrorMessage="Please Select Date Of Birth" Text="*"
                                                                ValidationGroup="G2" />
                                                            <asp:RegularExpressionValidator ID="rgetxtDOB" runat="server"
                                                                ControlToValidate="txtDob"
                                                                ErrorMessage="Date Of Birth Field is not in
                            correct Format "
                                                                Text="*" ValidationExpression="([1-9]|0[1-9]|[12][0-9]|3[01])[-
                                /.]([1-9]|0[1-9]|1[012])[- /.][0-9]{4}$"
                                                                ValidationGroup="G2"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Date Of Joining</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group pull-right">
                                                                <asp:TextBox ID="txtDoj" CssClass="form-control
                                datepicker"
                                                                    runat="server" AutoPostBack="true"
                                                                    onchange="return ValidateDate()"></asp:TextBox>
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvtxtDoj" runat="server"
                                                                ControlToValidate="txtDoj"
                                                                ErrorMessage="Please Select Date Of joining"
                                                                Text="*" ValidationGroup="G2" />
                                                            <asp:RegularExpressionValidator ID="rgetxtDoj" runat="server"
                                                                ControlToValidate="txtDoj"
                                                                ErrorMessage="Date Of Joining Field is not in
                            correct Format "
                                                                Text="*" ValidationExpression="([1-9]|0[1-9]|[12][0-9]|3[01])[-
                                /.]([1-9]|0[1-9]|1[012])[- /.][0-9]{4}$"
                                                                ValidationGroup="G2"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Employee ID</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtEmpID" runat="server" CssClass="form-control"
                                                                    ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Designation:</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlEmpDesgn" runat="server"
                                                                class="form-control select"
                                                                Style="margin-bottom: 12px;">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfvDesignation" runat="server"
                                                            ControlToValidate="ddlEmpDesgn"
                                                            ErrorMessage="Please Select a Designation" Text="*"
                                                            ValidationGroup="G2" InitialValue="0"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Store Name:</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlStoreName" runat="server"
                                                                class="form-control select"
                                                                Style="margin-bottom: 12px;">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfvStoreName" runat="server"
                                                            ControlToValidate="ddlStoreName"
                                                            ErrorMessage="Please Select a Store Name" Text="*"
                                                            ValidationGroup="G2" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Label ID="spnEmpAcDetails" runat="server" Style="font-size: 14px; margin-left: 25px;">Account Details </asp:Label>
                                    <br />
                                    <br />
                                    <div id="pnlAcDetails" runat="server">
                                        <div style="border: 1px solid #ccc; padding: 15px;">

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Bank Name</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtBnkName"
                                                                    onkeypress="return LettersWithSpaceOnly(event);"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Branch</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control"
                                                                    onkeypress="return LettersWithSpaceOnly(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Name As in Account</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtNameInBranch"
                                                                    onkeypress="return LettersWithSpaceOnly(event);"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Account No</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtAcNo" runat="server" CssClass="form-control"
                                                                    onkeypress="return ValidateNumberOnly(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            PAN No</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtPanNo"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <br />
                                    <asp:Label ID="Span1" runat="server" Style="font-size: 14px; margin-left: 25px;">
                                        Address
                                    </asp:Label>
                                    <br />
                                    <br />
                                    <div id="pnlAddress" runat="server">
                                        <div style="border: 1px solid #ccc; padding: 15px;">
                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-8 control-label">
                                                            Temporary Address</label>
                                                        <%--<div class="col-md-8">
                                                            <div class="input-group">
                                                            </div>
                                                        </div>--%>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-8 control-label">
                                                            Permanent Address</label>
                                                        <%--<div class="col-md-8">
                                                            <div class="input-group">
                                                            </div>
                                                        </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Address Line1</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <asp:TextBox CssClass="form-control" ID="txtTempAddressLine1"
                                                                    TextMode="MultiLine"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Address Line1</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtPermAddressLine1" runat="server"
                                                                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Address Line2</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <asp:TextBox CssClass="form-control" ID="txtTempAddressLine2"
                                                                    TextMode="MultiLine"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Address Line2</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtPermAddressLine2" runat="server"
                                                                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Country</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtTempCountry"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Country</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtPermCountry" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            State</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtTempState"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            State</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtPermState" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            city</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtTempCity"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            city</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtPermCity" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Zip Code</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtTempZipCode"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Zip Code</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtPermZipCode" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Mobile No</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtMobileNo"
                                                                    onkeypress="return ValidateNumberOnly(event);"
                                                                    MaxLength="10"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Alternative Mobile No</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtAltMobileNo" runat="server"
                                                                    CssClass="form-control" onkeypress="return ValidateNumberOnly(event);"
                                                                    MaxLength="10"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Personal Email</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtPersonalEmail"
                                                                    OnChange="validateEmail(this);"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Work Email</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtWorkEmail" runat="server"
                                                                    CssClass="form-control" OnChange="validateEmail(this);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Label ID="SpanQualification" runat="server" Style="font-size: 14px; margin-left: 25px;">Qualification </asp:Label>
                                    <br />
                                    <br />
                                    <div id="pnlQualification" runat="server">
                                        <div style="border: 1px solid #ccc; padding: 15px;">

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            10th %</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txt10thPer"
                                                                    onblur="onlyIntegersincludingdot(this); validTwoDecimal(this)" MaxLength="3"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            (Board)</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txt10thBoard" runat="server"
                                                                    onblur="alphabetswithdotsinglequoteandspace(this);"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            12th %</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txt12thPer"
                                                                    onblur="onlyIntegersincludingdot(this); validTwoDecimal(this)" MaxLength="3"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            (Board)</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txt12thBoard" runat="server"
                                                                    onblur="alphabetswithdotsinglequoteandspace(this);"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Graduation</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtAddGradCourse"
                                                                    onblur="onlyalphabetsandDots(this)"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            %</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtGradPer" runat="server"
                                                                    onblur="onlyIntegersincludingdot(this); validTwoDecimal(this)"
                                                                    CssClass="form-control" Text="%" MaxLength="3"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            University</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtGradUniversity" runat="server"
                                                                    onblur="alphabetswithdotsinglequoteandspace(this);"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Post Graduation</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtAddpostGradCourse"
                                                                    onblur="onlyalphabetsandDots(this)"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            %</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtPostGradPer" runat="server"
                                                                    onblur="onlyIntegersincludingdot(this); validTwoDecimal(this)"
                                                                    CssClass="form-control" Text="%" MaxLength="3"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            University</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtPostGradUniversity" runat="server"
                                                                    onblur="alphabetswithdotsinglequoteandspace(this);"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Others</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtQulaificationOthers"
                                                                    runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Label ID="SpanWorkExp" runat="server" Style="font-size: 14px; margin-left: 25px;">Work Experience </asp:Label>
                                    <br />
                                    <br />
                                    <div id="pnlWorkExp" runat="server">
                                        <div style="border: 1px solid #ccc; padding: 15px;">

                                            <div class="row">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            No Of Organizations worked in</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlNoOfOrgWorked" runat="server"
                                                                AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlNoOfOrgWorked_SelectedIndexChanged"
                                                                class="form-control select" Style="margin-bottom: 12px;">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:GridView ID="gvNoOrganisationGrid" runat="server"
                                                AutoGenerateColumns="false" OnRowCommand="gvNoOrganisationGrid_RowCommand"
                                                OnRowDeleting="gvNoOrganisationGrid_RowDeleting" OnSelectedIndexChanged="gvNoOrganisationGrid_SelectedIndexChanged"
                                                RowStyle-HorizontalAlign="Center" CssClass="table table-bordered table-striped table-actions"
                                                Visible="false">
                                                <%--     <RowStyle HorizontalAlign="Center" />--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="true"
                                                        ItemStyle-CssClass="middle" ItemStyle-Width="30" HeaderText="Name
                                Of Organisation">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtNameOfOrganisation"
                                                                runat="server" class="text-input small-input"
                                                                Text='<%#Eval("NameOfOrganisation")%>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvNameOfOrganization"
                                                                runat="server" ControlToValidate="txtNameOfOrganisation"
                                                                ErrorMessage="Please fill Name Of  Organization."
                                                                Text="*" ValidationGroup="G2"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                        HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvDesignation" runat="server"
                                                                class="text-input small-input"
                                                                Text='<%#Eval("Designation") %>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="txtgvDesignation"
                                                                ErrorMessage="Please fill Designation." Text="*" ValidationGroup="G2"></asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                        HeaderText="From Date">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvFromDate" runat="server"
                                                                class="text-input small-input" Text='<%#Eval("FromDate")
                                %>'></asp:TextBox>
                                                            <asp:CalendarExtender ID="txtgvFromDate_CalendarExtender"
                                                                runat="server" Enabled="True"
                                                                Format="dd-MM-yyyy" TargetControlID="txtgvFromDate">
                                                            </asp:CalendarExtender>
                                                            <asp:RegularExpressionValidator ID="rgetxtgvFromDate"
                                                                runat="server" ControlToValidate="txtgvFromDate"
                                                                ErrorMessage="From Date Field incorrect Format "
                                                                Text="*" ValidationExpression="([1-9]|0[1-9]|[12][0-9]|3[01])[- /.]([1-9]|0[1-9]|1[012])[-/.][0-9]{4}$"
                                                                ValidationGroup="G2"></asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                        HeaderText="To Date">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvToDate" runat="server"
                                                                class="text-input small-input" Text='<%#Eval("ToDate")
                                %>'></asp:TextBox>
                                                            <asp:CalendarExtender ID="txtgvToDate_CalendarExtender"
                                                                runat="server" Enabled="True"
                                                                Format="dd-MM-yyyy" TargetControlID="txtgvToDate">
                                                            </asp:CalendarExtender>
                                                            <asp:RegularExpressionValidator ID="rgetxtgvToDate"
                                                                runat="server" ControlToValidate="txtgvToDate"
                                                                ErrorMessage="To Date Field in correct Format "
                                                                Text="*" ValidationExpression="([1-9]|0[1-9]|[12][0-9]|3[01])[- /.]([1-9]|0[1-9]|1[012])[-/.][0-9]{4}$"
                                                                ValidationGroup="G2"></asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                        HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtWorkExperienceID"
                                                                runat="server" AutoPostBack="true" class="text-input
                                small-input"
                                                                ReadOnly="true" Text='<%#Eval("WorkExperienceID")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Font-Bold="false"
                                                        HeaderStyle-Width="16px" ItemStyle-Width="16px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="imgBtnDelete" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                OnClientClick="return confirm('Do you want to Delete the record?');"
                                                                CommandName="Deleting" ValidationGroup="False" runat="server"
                                                                ToolTip="Delete"
                                                                Style="text-decoration: none;"> 
                                                            <span class="fad fa-times"> </span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Label ID="Span2" runat="server" Style="font-size: 14px; margin-left: 25px;">
                                        Emergency Contact Details </asp:Label>
                                    <br />
                                    <br />
                                    <div id="pnlEmergencyContact" runat="server">
                                        <div style="border: 1px solid #ccc; padding: 15px;">
                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Contact Name</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtEmergencyContactName" runat="server"
                                                                    onkeypress="return LettersWithSpaceOnly(event);" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Contact No</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtEmergencyContactNo" runat="server"
                                                                    CssClass="form-control" onkeypress="return ValidateNumberOnly(event);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Relation</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa
                                fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtEmergencyContactRelation" runat="server"
                                                                    onkeypress="return LettersWithSpaceOnly(event);" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <br />
                                    <asp:Label ID="spnRelievingDetails" runat="server" Style="font-size: 14px; margin-left: 25px;">Relieving Details </asp:Label>
                                    <br />
                                    <br />
                                    <div id="pnlRelievingDetails" runat="server">
                                        <div style="border: 1px solid #ccc; padding: 15px;">
                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">
                                                            Relieving/Terminated</label>
                                                        <div class="col-md-8">
                                                            <asp:RadioButton ID="radioRelieving" Text="" GroupName="Relieving/Terminated"
                                                                runat="server" CssClass="Label" />
                                                            <asp:Label ID="Label4" runat="server" Text="Relieving"
                                                                CssClass="Label"></asp:Label>
                                                            <asp:RadioButton ID="radioTerminated" Text="" GroupName="Relieving/Terminated"
                                                                runat="server" CssClass="Label" />
                                                            <asp:Label ID="Label5" runat="server" Text="Terminated"
                                                                CssClass="Label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Resignation Date</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group pull-right">
                                                                <asp:TextBox ID="txtResignDate" CssClass="form-control datepicker"
                                                                    runat="server"></asp:TextBox>
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                            </div>
                                                            <asp:RegularExpressionValidator ID="rgetxtResignationDate"
                                                                runat="server" ControlToValidate="txtResignDate"
                                                                ErrorMessage="From Date Field incorrect Format"
                                                                Text="*" ValidationExpression="([1-9]|0[1-9]|[12][0-9]|3[01])[- /.]([1-9]|0[1-9]|1[012])[- /.][0-9]{4}$"
                                                                ValidationGroup="G2"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Relieving Date</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group pull-right">
                                                                <asp:TextBox ID="txtRelievingDate" CssClass="form-control datepicker"
                                                                    runat="server"></asp:TextBox>
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                            </div>
                                                            <asp:RegularExpressionValidator ID="rgetxtRelievingDate"
                                                                runat="server" ControlToValidate="txtRelievingDate"
                                                                ErrorMessage="From Date Field incorrect Format"
                                                                Text="*" ValidationExpression="([1-9]|0[1-9]|[12][0-9]|3[01])[- /.]([1-9]|0[1-9]|1[012])[- /.][0-9]{4}$"
                                                                ValidationGroup="G2"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Reason for leaving</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                <asp:TextBox ID="txtReasonForLeaving" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">
                                                            Can be Re-hired</label>
                                                        <div class="col-md-8">
                                                            <asp:RadioButton ID="radioRehiredYes" Text="" GroupName="Re-hired"
                                                                runat="server"
                                                                CssClass="Label" />
                                                            <asp:Label ID="Label6" runat="server" Text="Yes"
                                                                CssClass="Label"></asp:Label>
                                                            <asp:RadioButton ID="radioRehiredNo" Text="" GroupName="Re-hired"
                                                                runat="server"
                                                                CssClass="Label" />
                                                            <asp:Label ID="Label7" runat="server" Text="RNo"
                                                                CssClass="Label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">
                                                            Exit formalities completed</label>
                                                        <div class="col-md-8">
                                                            <asp:RadioButton ID="radioExitFormalitiesCmpltdYes"
                                                                Text="" GroupName="Exit-Formalities"
                                                                runat="server" CssClass="Label" />
                                                            <asp:Label ID="Label8" runat="server" Text="Yes"
                                                                CssClass="Label"></asp:Label>
                                                            <asp:RadioButton ID="radioExitFormalitiesCmpltdNo"
                                                                Text="" GroupName="Exit-Formalities"
                                                                runat="server" CssClass="Label" />
                                                            <asp:Label ID="Label9" runat="server" Text="No"
                                                                CssClass="Label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <br />

                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Is Active</label>
                                                <div class="col-md-8">
                                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:ValidationSummary ID="validSum1" runat="server" ValidationGroup="G2"
                                                    CssClass="validationSummary" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div id="divSearch" class="panel-body">
                                <div id="pnlSearch" runat="server">

                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Employee:</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlSearchEmployee" runat="server"
                                                        OnSelectedIndexChanged="ddlSearchEmployee_SelectedIndexChanged"
                                                        AutoPostBack="True"
                                                        class="form-control select" Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Employee No:</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa
                        fa-search"></span></span>
                                                        <asp:TextBox ID="txtsearchEmployeeNo" runat="server"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Active:</label>
                                                <div class="col-md-8">
                                                    <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="true"
                                                        Checked="true" OnCheckedChanged="chkActive_CheckedChanged" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    In Active:</label>
                                                <div class="col-md-8">
                                                    <asp:CheckBox ID="chkInActive" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                </label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <asp:Button ID="imgSearch" runat="server" Text="Search"
                                                            CssClass="btn btn-info" OnClick="imgSearch_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnClearSearch" runat="server" Text="Clear"
                                    CssClass="btn btn-warning" OnClick="btnClearSearch_Click" />
                                                    </div>
                                                    <label class="help-block" style="display: none;">
                                                        Search</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div id="pnlSearchEmployees" runat="server" class="table-responsive">
                                        <asp:GridView ID="gvSearchEmployee" RowStyle-BackColor="White" CssClass="table datatable table-bordered table-striped table-actions"
                                            DataKeyNames="EmployeeID" AllowPaging="True" AutoGenerateColumns="False"
                                            runat="server"
                                            OnRowCommand="gvSearchEmployee_RowCommand" OnRowEditing="gvSearchEmployee_RowEditing"
                                            OnRowDeleting="gvSearchEmployee_RowDeleting" OnPageIndexChanging="gvSearchEmployee_PageIndexChanging"
                                            OnRowCreated="gvSearchEmployee_RowCreated">
                                            <Columns>
                                                <asp:BoundField DataField="EmployeeNo" HeaderText="Employee No"
                                                    ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="Employee" HeaderText="Employee"
                                                    ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="StoreName" HeaderText="Store"
                                                    ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="DesignationName" HeaderText="Designation"
                                                    ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="DateOfJoining" HeaderText="Date
                        Of Joining"
                                                    ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="WorkEmailID" HeaderText="Email"
                                                    ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="MobileNo" HeaderText="MobileNo"
                                                    ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:TemplateField ItemStyle-Width="90">
                                                    <ItemTemplate>
                                                        <%-- <asp:ImageButton runat="server" ID="" ToolTip="View"
                        Text="View" ImageUrl="~/images/hammer_screwdriver.png"
                                                    CommandName="View" Width="16px" Height="16px" CommandArgument="<%#
                        ((GridViewRow) Container).RowIndex %>" />
                                                <asp:ImageButton runat="server" ID="" ToolTip="Edit" Text="Edit"
                        ImageUrl="~/images/pencil.png"
                                                    CommandName="Edits" Width="16px" Height="16px" CommandArgument="<%#
                        ((GridViewRow) Container).RowIndex %>" />
                                                <asp:ImageButton runat="server" ID="" ToolTip="Delete" Text="Delete"
                        ImageUrl="~/images/cross.png" CommandName="Deletes" Width="16px" Height="16px"
                                                    OnClientClick='return confirm("Do you want to delete
                        the Record?");' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />--%>
                                                        <asp:LinkButton ID="imgBtnView" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                            CommandName="View"
                                                            Style="text-decoration: none;" CssClass="fav
                        fa-eye"
                                                            ToolTip="View"></asp:LinkButton>
                                                        <asp:LinkButton ID="imgBtnEdit" runat="server" CommandName="Edits"
                                                            CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                            Style="text-decoration: none;" CssClass="fae
                        fa-pencil"
                                                            ToolTip="Edit">
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="imgBtnDelete" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                            OnClientClick="return confirm('Do you want to Delete the record?');"
                                                            CommandName="Deletes" runat="server" ToolTip="Delete"
                                                            Style="text-decoration: none;"> 
                                        <span class="fad fa-times"> </span></asp:LinkButton>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="panel-footer" id="dvFooter" runat="server">
                                <div class="form-group">

                                    <div class="col-md-2" id="dvisave" runat="server">
                                        <asp:Button ID="imgSave" ValidationGroup="G2" runat="server" CssClass="btn
                        btn-success btn-block pull-right"
                                            Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" OnClientClick="this.disabled = true;" 
                                             UseSubmitBehavior="false"
                                            OnClick="imgSave_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvupdate" runat="server">
                                        <asp:Button ID="imgUpdate" ValidationGroup="G2" Visible="false"
                                            runat="server"
                                            CssClass="btn btn-success btn-block pull-left"
                                            Text="Update" CausesValidation="true"
                                            Style="margin-bottom: 5px;" OnClick="imgUpdate_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvclear" runat="server">
                                        <asp:Button ID="imgClear" runat="server" CssClass="btn btn-warning
                        btn-block pull-left"
                                            alt="Clear" Text="Clear" Style="margin-bottom: 5px;"
                                            OnClick="imgClear_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvcancel" runat="server">
                                        <asp:Button ID="btncan" runat="server" CssClass="btn btn-danger
                        btn-block pull-left"
                                            Text="Cancel" Style="margin-bottom: 5px;" OnClick="btncan_Click" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </form>
                </div>
            </div>
        </div>
    </form>


</asp:Content>
