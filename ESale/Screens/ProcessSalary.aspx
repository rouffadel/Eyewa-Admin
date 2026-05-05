<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="ProcessSalary.aspx.cs" Inherits="Screens_ProcessSalary" Title="Process Saleary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../js/jquery.js"></script>
    <style type="text/css">
        .SalaryProcessPopupStyle
        {
            position: fixed;
            width: 900px;
            z-index: 500; /*height: 445px;*/
            height: 650px;
            padding: 0px;
            background-color: #fff;
            border: solid 6px #666;
            margin: 0px 0px 0px 9%;
            top: 0;
            bottom: 0%;
            text-align: center;
        }

            .SalaryProcessPopupStyle h2.headerstyle
            {
                width: 98.5%;
                float: left;
                margin: 0px;
                padding: 8px 0px 8px 10px;
                font: bold 14px arial;
                color: #666;
                background: url('../images/bg-content-box.gif') top left repeat-x;
                text-align: left;
            }

        .modalOverlay
        {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0px;
            left: 0px;
            background-color: rgba(0,0,0,0.3); /* black semi-transparent */
        }

        .TextBoxStyle
        {
            padding: 6px;
            font-size: 13px;
            background: white url('../images/bg-form-field.gif') repeat-x left top;
            border: 1px solid #D5D5D5;
            color: #333;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            margin-bottom: 0px; /*height: 15px;*/
            width: 100px;
        }

        .Label
        {
            color: #696969;
            font-size: 12px;
            font-family: Verdana;
            width: 100px;
        }

        .BtnEmptyStyle
        {
            font: bold 13px arial;
            color: #222;
            background: url(../Images/emptybtn.png) repeat-y;
            cursor: pointer;
            border: none;
            width: 99px;
            clear: none;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(function () {
            setInterval('pingSession()', 60000); // keep session alive by pinging every 60 sec
        });
        function pingSession() {
            $.ajax({
                url: "../Services/PreventSessionTimeOut.asmx/PingSession",
                data: {},
                type: "POST",
                processData: false,
                contentType: "charset=utf-8",
                timeout: 10000,
                success: function (response) {
                },
                failure: function (response) {
                }
            });
        };
    </script>

    <script type="text/javascript">
        function PaymentDetailsPrint() {
            document.getElementById('<%=divPaySlipPopup.ClientID %>').style.display = "none";
            var divToPrint = document.getElementById('ctl00_ContentPlaceHolder1_divPaymentDetails');
            var popupWin = window.open('', '_blank');
            popupWin.document.open();
            popupWin.document.write('<html><head></head><body onload="window.print();">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
            document.getElementById('divPaySlipPopup').style = "display:none";
        }
    </script>

    <script type="text/javascript">
        function calculatesalary(aa) {
            var workingdays = document.getElementById("ctl00_ContentPlaceHolder1_txtNoOfWorkingDays").value;
            var row = aa.parentNode.parentNode;
            var rowIndex = row.rowIndex + 1;
            var employeeid, lopdays, salary, incentive, tds;
            if (rowIndex < 10) {
                employeeid = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowIndex + "_lblMEmployeeID").innerHTML;
                lopdays = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowIndex + "_txtLOPDays").value;
                salary = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowIndex + "_lblMSalary").innerHTML;
                incentive = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowIndex + "_txtincentive").value;
                tds = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowIndex + "_txttds").value;
                otheraddition = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowIndex + "_txtOtherAddition").value;
                otherdeduction = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowIndex + "_txtOtherDeduction").value;
            }
            else {
                employeeid = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowIndex + "_lblMEmployeeID").innerHTML;
                lopdays = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowIndex + "_txtLOPDays").value;
                salary = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowIndex + "_lblMSalary").innerHTML;
                incentive = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowIndex + "_txtincentive").value;
                tds = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowIndex + "_txttds").value;
                otheraddition = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowIndex + "_txtOtherAddition").value;
                otherdeduction = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowIndex + "_txtOtherDeduction").value;

            }
            var wkingdys = parseInt(workingdays);
            var lopdys = parseInt(lopdays);
            if (workingdays == "") {
                alert("Enter No of Working days");
                //var wrkgdays = document.getElementById("ctl00_ContentPlaceHolder1_txtNoOfWorkingDays").value;
                aa.value = '';
                aa.focus();
                return false;
            }
            else if (wkingdys < lopdys) {
                alert("LOP Days Can not be greater than  Working Days");
                var ldays = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowIndex + "_txtLOPDays").value
                var ldyss = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowIndex + "_txtLOPDays");
                ldyss.value = "";
                ldyss.focus();
                ldays = "";

                return false;
            }

            else {
                var perdaysalary = (salary) / workingdays;
                var deductionsalary = (perdaysalary * lopdays);

                if (tds != "" && incentive != "") {
                    var netsalary = (salary - deductionsalary + parseFloat(incentive) - parseFloat(tds));
                } else if (tds != "") {
                    var netsalary = (salary - deductionsalary - parseFloat(tds));
                } else if (incentive != "") {
                    var netsalary = (salary - deductionsalary + parseFloat(incentive));
                }
                else {
                    var netsalary = (salary - deductionsalary);
                }
                if (otheraddition != "") {
                    var netsalary = (netsalary + parseFloat(otheraddition));
                }
                if (otherdeduction != "") {
                    var netsalary = (netsalary - parseFloat(otherdeduction));
                }

                //                if (otheraddition != "") {
                //                    netsalary = netsalary + otheraddition;
                //                }
                //                if (otherdeduction != "") {
                //                    netsalary = netsalary - otherdeduction;
                //                }

                var deducesal, netsal, otheraddition, otherdeduction;
                if (rowIndex < 10) {
                    deducesal = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowIndex + "_txtDeduction");
                    netsal = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowIndex + "_txtNetSalary");
                }
                else {
                    deducesal = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowIndex + "_txtDeduction");
                    netsal = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowIndex + "_txtNetSalary");
                }
                if (lopdays != "") {
                    deducesal.value = Math.round(deductionsalary * 100) / 100;
                    netsal.value = Math.round(netsalary * 100) / 100;
                }
                else {
                    netsal.value = "";
                    deducesal.value = "";
                }
                calculateTotals();
            }
        }
        function calculateTotals() {
            var Gridview = document.getElementById('<%=gvManualLineItems.ClientID %>');
            var rowcount = Gridview.rows.length;
            var totaldeduction = 0, totalincentive = 0, totalotherdeductions = 0, totalotheradditions = 0, totaltds = 0, totalnetsalary = 0;
            var td1, ti1, tod1, toa1, ttds1, tns1;
            for (var i = 2; i <= rowcount; i++) {
                if (i < 10) {
                    td1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + i + "_txtDeduction").value;
                    ti1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + i + "_txtincentive").value;
                    tod1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + i + "_txtOtherDeduction").value;
                    toa1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + i + "_txtOtherAddition").value;
                    ttds1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + i + "_txttds").value;
                    tns1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + i + "_txtNetSalary").value;
                }
                else {
                    td1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + i + "_txtDeduction").value;
                    ti1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + i + "_txtincentive").value;
                    tod1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + i + "_txtOtherDeduction").value;
                    toa1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + i + "_txtOtherAddition").value;
                    ttds1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + i + "_txttds").value;
                    tns1 = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + i + "_txtNetSalary").value;
                }
                if (td1 == "" && ti1 == "" && tod1 == "" && toa1 == "" && ttds1 == "" && tns1 == "")
                    break;
                else {
                    totaldeduction = totaldeduction + parseFloat(td1)
                    totalincentive = totalincentive + parseFloat(ti1);
                    totalotheradditions = totalotheradditions + parseFloat(toa1);
                    totalotherdeductions = totalotherdeductions + parseFloat(tod1);
                    totaltds = totaltds + parseFloat(ttds1);
                    totalnetsalary = totalnetsalary + parseFloat(tns1);
                    if (i < 10) {
                        if (!isNaN(totaldeduction))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowcount + "_txtTotalDeduction").value = totaldeduction.toFixed(2);
                        if (!isNaN(totalincentive))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowcount + "_txtTotalincentive").value = totalincentive.toFixed(2);
                        if (!isNaN(totalotheradditions))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowcount + "_txtTotalOtherAddition").value = totalotheradditions.toFixed(2);
                        if (!isNaN(totalotherdeductions))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowcount + "_txtTotalOtherDeduction").value = totalotherdeductions.toFixed(2);
                        if (!isNaN(totaltds))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowcount + "_txtTotalTds").value = totaltds.toFixed(2);
                        if (!isNaN(totalnetsalary))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + rowcount + "_txtTotalNetSalary").value = totalnetsalary.toFixed(2);
                    }
                    else {
                        if (!isNaN(totaldeduction))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowcount + "_txtTotalDeduction").value = totaldeduction.toFixed(2);
                        if (!isNaN(totalincentive))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowcount + "_txtTotalincentive").value = totalincentive.toFixed(2);
                        if (!isNaN(totalotheradditions))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowcount + "_txtTotalOtherAddition").value = totalotheradditions.toFixed(2);
                        if (!isNaN(totalotherdeductions))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowcount + "_txtTotalOtherDeduction").value = totalotherdeductions.toFixed(2);
                        if (!isNaN(totaltds))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowcount + "_txtTotalTds").value = totaltds.toFixed(2);
                        if (!isNaN(totalnetsalary))
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + rowcount + "_txtTotalNetSalary").value = totalnetsalary.toFixed(2);

                    }
                }


            }
        }
        function checksalary() {
            var workingdays = document.getElementById("ctl00_ContentPlaceHolder1_txtNoOfWorkingDays").value;
            var month = document.getElementById("ctl00_ContentPlaceHolder1_ddlMonth").value;
            var year = document.getElementById("ctl00_ContentPlaceHolder1_ddlYear").value;

            if (workingdays == "") {
                alert("Enter Number of Working days.");
                return false;
            }
            if (month == "0") {
                alert("Select Any Month.");
                return false;
            }
            if (year == "0") {
                alert("Select Any year.");
                return false;
            }

            var GridView = document.getElementById('<%=gvManualLineItems.ClientID %>');
            var rowscount = GridView.rows.length;

            var lopdays;
            var count = 0;

            for (i = 2; i <= rowscount; i++) {

                if (i <= 9) {

                    lopdays = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + i + "_txtLOPDays").value;

                    if (lopdays == "") {
                        if (count == 0) {
                            document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl0" + i + "_txtLOPDays").focus();
                            alert('Please Enter LOP Days');
                            return false;
                        }
                    }
                    else
                        count++;
                }
                else if (i > 9) {

                    lopdays = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + i + "_txtLOPDays").value;

                    if (lopdays == "") {
                        if (count == 0) {
                            var lpdys = document.getElementById("ctl00_ContentPlaceHolder1_gvManualLineItems_ctl" + i + "_txtLOPDays")
                            alert('Please Enter LOP Days');
                            lpdys.focus();
                            return false;
                        }
                    }
                    else
                        count++;

                } else {


                }

            }

        }

        function MonthDays(Val) {
            var txtWorkingDays = document.getElementById("ctl00_ContentPlaceHolder1_txtNoOfWorkingDays");
            var Mnth = document.getElementById("ctl00_ContentPlaceHolder1_ddlMonth").value;
            var Month = Mnth - 1;
            var Year = document.getElementById("ctl00_ContentPlaceHolder1_ddlYear").value;
            var MonthStart = new Date(Year, Month, 1);
            var MonthEnd = new Date(Year, Month + 1, 1);
            var MonthDays = (MonthEnd - MonthStart) / (1000 * 60 * 60 * 24);
            if (MonthDays < Val.value) {
                alert('Please Enter Valid Working Days');
                txtWorkingDays.value = '';
                txtWorkingDays.focus();
                return false;
            }
        }
    </script>

    <script type="text/javascript">

        function IsNumerics(a) {

            if (isNaN(a.value)) {
                alert('Please Enter Numerics Only');
                a.value = '';
            }

        }

    </script>
    <asp:Label ID="lblTitle" runat="server" Visible="false">Salary</asp:Label>

    <form runat="server" id="form">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Salary</strong>
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
                                        <asp:Label ID="lblStatus" runat="server" Style="color: white;"></asp:Label>
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
                            <div id="pnladd" runat="server" class="page-content-wrap">
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Month:</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlMonth" runat="server"
                                                    class="form-control select"
                                                    Style="margin-bottom: 12px;">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvddlMonth" runat="server" ControlToValidate="ddlMonth"
                                                ErrorMessage="Please Select Month" InitialValue="0" Text="*" ValidationGroup="r" />

                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Year:</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlYear" runat="server"
                                                    class="form-control select"
                                                    Style="margin-bottom: 12px;">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvddlYear" runat="server" ControlToValidate="ddlYear"
                                                ErrorMessage="Please Select Year" InitialValue="0" Text="*" ValidationGroup="r" />
                                        </div>
                                    </div>
                                </div>
                                      <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                WorkingDays:</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtNoOfWorkingDays" runat="server" onkeyup="IsNumerics(this)" onchange="return MonthDays(this)" />
                                                </div>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNoOfWorkingDays"
                                                    ErrorMessage="Please Enter No Of WorkingDays" Text="*" ValidationGroup="r" />
                                            </div>
                                        </div>
                                    </div>

                                    </div>
                                    <div class="row top" style="display:none">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                System Attendance:</label>
                                            <div class="col-md-8">
                                                 <asp:RadioButton ID="rbtnSystemAttendance" runat="server" GroupName="attendance"
                                                    Checked="true" AutoPostBack="true" OnCheckedChanged="rbtnSystemAttendance_Click" />
                                            </div>
                                           

                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Manual Attendance:</label>
                                            <div class="col-md-8">
                                               <asp:RadioButton ID="rbtnManualAttendance" runat="server" GroupName="attendance"
                                                    AutoPostBack="True" OnCheckedChanged="rbtnManualAttendance_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        

                                <asp:Button ID="btnProcess" runat="server" CssClass="btn btn-success" Visible="false"
                                    ValidationGroup="r" Text="Process" OnClick="btnProcess_Click" />

                                <br />
                                <table border="0" align="center">
                                    <tr>
                                        <td>
                                            <asp:ValidationSummary ID="validSum1" runat="server" ValidationGroup="r" CssClass="validationSummary" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div id="pnlManualLineItems" runat="server" class="table-responsive">
                                        <asp:GridView ID="gvManualLineItems" HeaderStyle-CssClass="Label" AutoGenerateColumns="False"
                                             runat="server" DataKeyNames="EmployeeID"
                                            ShowFooter="true" CssClass="table table-bordered table-striped table-actions">
                                            <%--<RowStyle HorizontalAlign="Center" />--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sno" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%#Eval("Sno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" Text="Total" Style="text-align: center;"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee ID" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMEmployeeID" runat="server" Text='<%#Eval("EmployeeID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMEmployeeName" runat="server" Text='<%#Eval("EmployeeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salary" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMSalary" runat="server" Text='<%#Eval("Salary") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LOP Days" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtLOPDays" runat="server" CssClass="TextBoxStyle" onkeyup="IsNumerics(this)"
                                                            HeaderStyle-Width="30px" ItemStyle-Width="50px" onchange="return calculatesalary(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deduction" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDeduction" runat="server" onkeyup="IsNumerics(this)" CssClass="TextBoxStyle"
                                                            Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <center>
                                                            <asp:TextBox ID="txtTotalDeduction" runat="server" Enabled="false" CssClass="TextBoxStyle"></asp:TextBox>
                                                        </center>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Incentive" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtincentive" runat="server" CssClass="TextBoxStyle" onkeyup="IsNumerics(this)"
                                                            onchange="return calculatesalary(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <center>
                                                            <asp:TextBox ID="txtTotalincentive" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                                        </center>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other Additions" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOtherAddition" runat="server" class="TextBoxStyle" onkeyup="IsNumerics(this)"
                                                            onchange="return calculatesalary(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <center>
                                                            <asp:TextBox ID="txtTotalOtherAddition" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                                        </center>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other Deductions" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOtherDeduction" runat="server" class="TextBoxStyle" onkeyup="IsNumerics(this)"
                                                            onchange="return calculatesalary(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <center>
                                                            <asp:TextBox ID="txtTotalOtherDeduction" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                                        </center>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TDS" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txttds" runat="server" onkeyup="IsNumerics(this)" onchange="return calculatesalary(this);"
                                                            class="TextBoxStyle"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <center>
                                                            <asp:TextBox ID="txtTotalTds" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                                        </center>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NetSalary" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNetSalary" onkeyup="IsNumerics(this)" runat="server" class="TextBoxStyle"
                                                            Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <center>
                                                            <asp:TextBox ID="txtTotalNetSalary" runat="server" CssClass="TextBoxStyle" Enabled="false"></asp:TextBox>
                                                        </center>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                </div>
                                <div id="pnlLineItems" runat="server" >
                                    <div class="table-responsive" runat="server" id="divLineItems">
                                        <asp:GridView ID="gvLineItems" runat="server" AutoGenerateColumns="false" CssClass="table datatable table-bordered table-striped table-actions">
                                            <%--  <RowStyle HorizontalAlign="Center" />--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="EmployeeI D" ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployeeID" runat="server"
                                                            Text='<%#Eval("EmployeeID") %>' CssClass="Label"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EmployeeSalaryID" ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployeeSalaryID" runat="server" Text='<%#Eval("EmployeeSalaryID") %>'
                                                            CssClass="Label"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEmployeeName" runat="server" Style="width: 150px;" Text='<%#Eval("EmployeeName") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Month" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtMonth" runat="server" Text='<%#Eval("Month") %>' class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Year" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtYear" runat="server" Style="width: 70px" Text='<%#Eval("Year") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GrossSalary" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtGrossSalary" runat="server" Style="width: 70px" Text='<%#Eval("GrossSalary") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Basic Salary" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBasicSalary" runat="server" Style="width: 70px" Text='<%#Eval("BasicSalary") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HRA" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtHRA" runat="server" Style="width: 70px" Text='<%#Eval("HRA") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="30" HeaderText="TA">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTA" runat="server" Style="width: 70px" Text='<%#Eval("TA") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DA" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDA" runat="server" Style="width: 70px" Text='<%#Eval("DA") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TDS" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTDS" runat="server" Style="width: 70px" Text='<%#Eval("TDS") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other Additions" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOtherAdditions" runat="server" Style="width: 70px" Text='<%#Eval("otherAdditions") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Other Deductions" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtOtherDeductions" runat="server" Style="width: 70px" Text='<%#Eval("otherDeduction") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No Of LOP" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtLOP" runat="server" Style="width: 70px" Text='<%#Eval("LOP") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deducation" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDeducation" runat="server" Style="width: 70px" Text='<%#Eval("Deducation") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Incentives" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIncentives" runat="server" Style="width: 70px" Text='<%#Eval("Incentive") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net Salary" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNetSalary" runat="server" Style="width: 70px" Text='<%#Eval("NetSalary") %>'
                                                            class="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                            <div id="pnlsearch" runat="server">
                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Employee:</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlSearchEmploye" runat="server"
                                                        class="form-control select" Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>

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
                                    <br />
                                    <div id="pnlrep" runat="server">
                                        <div class="table-responsive" style="overflow: auto; width: 100%;">
                                            <asp:GridView ID="gvSalaraySheet" runat="server" AllowPaging="true" AllowSorting="true"
                                                 DataKeyNames="SalaryID" CssClass="table datatable table-bordered table-striped table-actions"
                                                AutoGenerateColumns="False" OnRowCommand="gvSalaraySheet_RowCommand"
                                                OnRowCreated="gvSalaraySheet_RowCreated" OnRowDeleting="gvSalaraySheet_RowDeleting"
                                                OnRowEditing="gvSalaraySheet_RowEditing" OnPageIndexChanging="gvSalaraySheet_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="SNo" HeaderText="SNo" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name"
                                                        ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="EmployeeNo" HeaderText="Employee No"
                                                        ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="Month" HeaderText="Month"
                                                        ItemStyle-CssClass="middle" ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="Year" HeaderText="Year"
                                                        ItemStyle-CssClass="middle" ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="GrossSalary" HeaderText="Gross Salary"
                                                        ItemStyle-CssClass="middle" ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="TDS" HeaderText="TDS" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="NoOfWorkingDays" HeaderText="Working Days"
                                                        ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="NoOfDaysPresent" HeaderText="Days Present"
                                                        ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="NoOfDaysAbsent" HeaderText="Days Absent"
                                                        ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="NoOfLeavesAvailed" HeaderText="Leaves Availed"
                                                        ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="LOP" HeaderText="LOP" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="Deducation" HeaderText="Deducations"
                                                        ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="Incentive" HeaderText="Incentive"
                                                        ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="OtherAdditions" HeaderText="Other Addition"
                                                        ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="OtherDeduction" HeaderText="Other Deduction"
                                                        ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:BoundField DataField="NetSalary" HeaderText="Net Salary"
                                                        ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30"></asp:BoundField>
                                                    <asp:TemplateField ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="90">
                                                        <ItemTemplate>
                                                            <%--  <asp:ImageButton runat="server" ID="imgBtnView" ToolTip="View" Text="View" ImageUrl="~/images/hammer_screwdriver.png"
                                                                CommandName="View" Width="20px" Height="18px" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />--%>
                                                            <asp:LinkButton ID="imgBtnView" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                CommandName="View"
                                                                Style="text-decoration: none;" CssClass="fav fa-eye" ToolTip="View"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="False" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="90">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="imgBtnprint" ToolTip="Print" Text="Print" ImageUrl="~/images/print.jpg"
                                                                CommandName="Print" Width="20px" Height="18px" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer" id="dvFooter" runat="server">
                                <div class="form-group">
                                    <div class="col-md-2" id="Div1" runat="server">
                                        <asp:Button ID="btnMProcessSalary" runat="server" CssClass="btn btn-success btn-block pull-right"
                                            Text="Process" Style="margin-bottom: 5px;" OnClientClick="return checksalary();"
                                            OnClick="btnMProcessSalary_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvisave" runat="server">
                                        <asp:Button ID="imgAdd" runat="server" CssClass="btn btn-success btn-block pull-right"
                                            Text="Save" Style="margin-bottom: 5px;" ValidationGroup="r" OnClientClick="this.disabled = true;" 
                                          
                                            OnClick="imgAdd_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvupdate" runat="server">
                                        <asp:Button ID="imgUpdate" ValidationGroup="r" Visible="false" runat="server"
                                            CssClass="btn btn-success btn-block pull-left"
                                            Text="Update" CausesValidation="true"
                                            Style="margin-bottom: 5px;" OnClick="imgUpdate_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvclear" runat="server">
                                        <asp:Button ID="imgClear" runat="server" CssClass="btn btn-warning btn-block pull-left"
                                            alt="Clear" Text="Clear" Style="margin-bottom: 5px;" CausesValidation="false"
                                            OnClick="imgClear_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvcancel" runat="server">
                                        <asp:Button ID="imgCancel" runat="server" CssClass="btn btn-danger btn-block pull-left"
                                            Text="Cancel" Style="margin-bottom: 5px;" OnClick="imgCancel_Click" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </form>
                </div>
            </div>
            <div id="pnlprint" runat="server">
                <div id="divPaySlipPopup" style="display: none; overflow: auto;" runat="server">
                    <div id="divPaymentDetails" runat="server">
                        <table style="width: 800px; margin-left: 34px; margin-bottom: 35px;" align="left">
                            <tr>
                                <td align="left" style="width: 200px">
                                    <%-- <asp:Image ID="imgLogo" ImageUrl="../Images/logo.png" Width="200px" Height="50px"
                                            runat="server" />--%>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 800px; margin-left: 34px; margin-top: 30px;" align="center">
                            <tr>
                                <td colspan="4" style="background-color: rgb(197, 195, 195); text-align: center;
                                    width: 800px">
                                    <asp:Label ID="lblMonthYear" runat="server" Style="font-weight: bold; float: none"
                                        CssClass="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="lblEmployee" runat="server" Text="Employee No" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblEmployeeNo" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label1" runat="server" Text="Date Of Joining" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblDaetOfJoining" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label2" runat="server" Text="Employee Name" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblEmployeeName" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label4" runat="server" Text="PF No" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblPFNo" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label3" runat="server" Text="Designation" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblDesignation" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label6" runat="server" Text="LOP Days" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblLOPDays" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label5" runat="server" Text="Location" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblLocation" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label8" runat="server" Text="No Of Working Days" CssClass="Label"
                                        Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblNoOfDaysPaid" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table style="width: 800px; margin-left: 34px;" align="center">
                            <tr>
                                <td colspan="2" align="left" style="background-color: rgb(197, 195, 195); width: 400px">
                                    <asp:Label ID="Label7" runat="server" Text="Earnings" Style="font-weight: bold" CssClass="Label"></asp:Label>
                                </td>
                                <td colspan="2" align="left" style="background-color: rgb(197, 195, 195); width: 400px">
                                    <asp:Label ID="Label25" runat="server" Text="Deductions" Style="font-weight: bold"
                                        CssClass="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label9" runat="server" Text="Basic" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblBasic" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label10" runat="server" Text="TDS" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblTDS" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label13" runat="server" Text="HRA" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblHRA" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label14" runat="server" Text="Deductions" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblDeductions" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label15" runat="server" Text="TA" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblTA" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Other Deductions" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblOtherDeductions" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label17" runat="server" Text="Working Days" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblNoOfWorkingDaysforPrint" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label19" runat="server" Text="Days Present" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblNoOfDaysPresent" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label23" runat="server" Text="Days Absent" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblNoOfDaysAbsent" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label21" runat="server" Text="Leaves Availed" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblNoOfLeavesAvailed" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label11" runat="server" Text="DA" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblDA" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label20" runat="server" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="Label24" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label18" runat="server" Text="Incentive" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblincentive" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label22" runat="server" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="label" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label16" runat="server" Text="Other Additions" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblOtherAdditions" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                                <td style="background-color: rgb(197, 195, 195); width: 200px">
                                    <asp:Label ID="Label27" runat="server" Text="NetSalary" CssClass="Label" Style="font-weight: bold;"></asp:Label>
                                </td>
                                <td style="width: 200px; background-color: rgb(241, 241, 241);">
                                    <asp:Label ID="lblNetSalary" runat="server" CssClass="Label" Style="margin-left: 5px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4"></td>
                            </tr>
                            <tr style="margin-top: 20px">
                                <td colspan="4" align="left" style="background-color: rgb(197, 195, 195); width: 800px">
                                    <asp:Label ID="l" Text="FOR ESales" CssClass="Label" Style="font-weight: bold"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left" style="width: 800px">
                                    <asp:Label ID="lblMs" runat="server" CssClass="Label" Text="*This certificate is system generated and doesn’t require Signature or Stamp in Original"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <table style="width: 800px; margin-left: 34px;" align="center">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblAddress" Visible="false" runat="server" CssClass="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table style="width: 800px; margin-left: 34px;" align="center">
                        <tr>
                            <td align="left" style="width: 800px">
                                <input type="button" id="btnprint" value="Print" class="BtnEmptyStyle" onclick="PaymentDetailsPrint()" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="BtnEmptyStyle"
                                    OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>


        </div>
    </form>
</asp:Content>

