<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="EmployeeSalary.aspx.cs" Inherits="Screens_EmployeeSalary" Title="Employee Salary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../js/jquery.js"></script>
    <asp:Label ID="lblTitle" CssClass="TitleClass" Text="Employee Salary" runat="server"
        Visible="false"></asp:Label>  
  
    <script type="text/javascript">
        function Numarics(aa) {
            var txtboxId = aa.value;
            if (isNaN(txtboxId)) {
                alert('Please Enter Numerics only');
                aa.value = '';
                Calculations();
                aa.focus();
                return false;
            }
        }
        function Calculations() {
            var BasicSal = document.getElementById('<%=txtBasicSal.ClientID%>').value;
            var GrassSalary = document.getElementById('<%=txtGrassSalary.ClientID%>').value;
            var Hra = document.getElementById('<%=txtHRA.ClientID%>').value;
            var Ta = document.getElementById('<%=txtTA.ClientID%>').value;
            var Da = document.getElementById('<%=txtDA.ClientID%>').value;
            var Tds = document.getElementById('<%=txtTDS.ClientID%>').value;




            if (GrassSalary != '') {

                document.getElementById('<%=txtYearlyGrassSalary.ClientID%>').value = parseFloat(GrassSalary * 12).toFixed(2);
                document.getElementById('<%=txtYearlyTDS.ClientID%>').value = parseFloat(Tds * 12).toFixed(2);
               
                document.getElementById('<%=txtBasicSal.ClientID%>').value = parseFloat(GrassSalary * 40 / 100).toFixed(2);
                document.getElementById('<%=txtHRA.ClientID%>').value = parseFloat(GrassSalary * 30 / 100).toFixed(2);
                document.getElementById('<%=txtTA.ClientID%>').value = parseFloat(GrassSalary * 10 / 100).toFixed(2);
                document.getElementById('<%=txtDA.ClientID%>').value = parseFloat(GrassSalary * 20 / 100).toFixed(2);


              
                document.getElementById('<%=txtYearlyBasicSal.ClientID%>').value = parseFloat((GrassSalary * 40 / 100) * 12).toFixed(2);
                document.getElementById('<%=txtYearlyHRA.ClientID%>').value = parseFloat((GrassSalary * 30 / 100) * 12).toFixed(2);
                document.getElementById('<%=txtYearlyTA.ClientID%>').value = parseFloat((GrassSalary * 10 / 100) * 12).toFixed(2);
                document.getElementById('<%=txtYearlyDA.ClientID%>').value = parseFloat((GrassSalary * 20 / 100) * 12).toFixed(2);
            }
            else {
                document.getElementById('<%=txtYearlyGrassSalary.ClientID%>').value = '';
                document.getElementById('<%=txtYearlyTDS.ClientID%>').value = '';

                document.getElementById('<%=txtBasicSal.ClientID%>').value = '';
                document.getElementById('<%=txtHRA.ClientID%>').value = '';
                document.getElementById('<%=txtTA.ClientID%>').value = '';
                document.getElementById('<%=txtDA.ClientID%>').value = '';



                document.getElementById('<%=txtYearlyBasicSal.ClientID%>').value = '';
                document.getElementById('<%=txtYearlyHRA.ClientID%>').value = '';
                document.getElementById('<%=txtYearlyTA.ClientID%>').value = '';
                document.getElementById('<%=txtYearlyDA.ClientID%>').value = '';

            }

            if (Tds != '' && GrassSalary != '') {
                document.getElementById('<%=txtNetSalary.ClientID%>').value = (parseFloat(GrassSalary) - parseFloat(GrassSalary * Tds / 100)).toFixed(2);
                document.getElementById('<%=txtYearlyNetSalary.ClientID%>').value = (parseFloat(GrassSalary) * 12 - parseFloat((GrassSalary * Tds / 100)) * 12).toFixed(2);
            }
            else {
                document.getElementById('<%=txtNetSalary.ClientID%>').value = '';
                document.getElementById('<%=txtYearlyNetSalary.ClientID%>').value = '';
            }




        }
    </script>

        <form runat="server" id="form">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Employee Salary</strong>
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

    <div id="pnladd" runat="server">

        <div class="page-content-wrap">
            <div class="row top">
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Employee:</label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="true"
                                class="form-control select" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"
                                Style="margin-bottom: 12px;">
                            </asp:DropDownList>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                            ErrorMessage="Please Select Employee" InitialValue="0" Text="*" ValidationGroup="G2" />

                    </div>
                </div>
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Employee No:</label>
                        <div class="col-md-8">
                            <div class="input-group ">
                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                <asp:TextBox ID="txtEmployeeID" CssClass="form-control "
                                    runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div>
                    </div>
                </div>
            </div>
            <div class="row top">
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Date Of Joining:</label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                <asp:TextBox CssClass="form-control" ID="txtDateOfJoining" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-4 control-label">
                            Date Of Birth:</label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                <asp:TextBox CssClass="form-control" ID="txtDOB" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <asp:Label ID="Label3" runat="server" align="right" Text="Employee Salary Details"
                CssClass="control-label" Style="margin: 10px 9px 0px 20px !important; font-weight: bold"></asp:Label>

            <div class="row top">
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                        </label>
                        <div class="col-md-8">
                            <asp:Label ID="Label4" runat="server" Text="Monthly:" class="control-label" Style="font-weight: bold;"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="form-group"> 
                            <asp:Label ID="Label5" runat="server" Text="Yearly:" class="control-label" Style="font-weight: bold;"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row top">
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            Gross Salary:</label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtGrassSalary" runat="server" onkeyup="Calculations();" 
                                 onchange="return Numarics(this);"/>
                            </div>
                              <asp:RequiredFieldValidator ID="rfvtxtGrassSalary" runat="server" ControlToValidate="txtGrassSalary"
                            ErrorMessage="Please Enter Gross Salary" Text="*" ValidationGroup="G2"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
               <%-- <div class="col-md-5">--%>
                    <div class="form-group">                       
                        <div class="col-md-5">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtYearlyGrassSalary" runat="server" onkeyup="Calculations();"
                              Enabled="false" onchange="return Numarics(this);"/>
                            </div>
                        </div>
                    </div>
               <%-- </div>--%>
            </div>

            <div class="row top">
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            Basic(40%):</label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtBasicSal" runat="server" onkeyup="Calculations();" onchange="return Numarics(this);"/>
                            </div>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBasicSal"
                            ErrorMessage="Please Enter Basic Salary" Text="*" ValidationGroup="G2">
                        </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <%--<div class="col-md-5">--%>
                    <div class="form-group">                        
                        <div class="col-md-5">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtYearlyBasicSal" runat="server" onkeyup="Calculations();"
                          Enabled="false" onchange="return Numarics(this);"/>
                            </div>
                        </div>
                    </div>
               <%-- </div>--%>
            </div>

            <div class="row top">
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            HRA(30%):</label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtHRA" runat="server" onkeyup="Calculations();" onchange="return Numarics(this);"/>
                            </div>
                               <asp:RequiredFieldValidator ID="rfvtxtHRA" runat="server" ControlToValidate="txtHRA"
                            ErrorMessage="Please Enter HRA" Text="*" ValidationGroup="G2"></asp:RequiredFieldValidator>
                    
                        </div>
                    </div>
                </div>
                <%--<div class="col-md-5">--%>
                    <div class="form-group">
                        <div class="col-md-5">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtYearlyHRA" runat="server" onkeyup="Calculations();"
                                Enabled="false" onchange="return Numarics(this);"/>
                            </div>
                        </div>
                    </div>
               <%-- </div>--%>
            </div>

            <div class="row top">
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            TA(10%):</label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtTA" runat="server" onkeyup="Calculations();" onchange="return Numarics(this);"/>
                            </div>
                              <asp:RequiredFieldValidator ID="rfvtxtTA" runat="server" ControlToValidate="txtTA"
                            ErrorMessage="Please Enter TA" Text="*" ValidationGroup="G2"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <%--<div class="col-md-5">--%>
                    <div class="form-group">
                        <div class="col-md-5">
                            <div class="input-group">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtYearlyTA" runat="server" onkeyup="Calculations();"
                                Enabled="false" onchange="return Numarics(this);"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row top">
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            DA(20%):</label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtDA" runat="server" onkeyup="Calculations();" onchange="return Numarics(this);"/>
                            </div>
                              <asp:RequiredFieldValidator ID="rfvtxtDA" runat="server" ErrorMessage="Please Enter DA"
                            Text="*" ControlToValidate="txtDA" ValidationGroup="G2" />
                        </div>
                    </div>
                </div>
              <%--  <div class="col-md-5">--%>
                    <div class="form-group">
                        <div class="col-md-5">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtYearlyDA" runat="server" onkeyup="Calculations();"
                                Enabled="false" onchange="return Numarics(this);"/>
                            </div>
                        </div>
                    </div>
               <%-- </div>--%>
            </div>

            <div class="row top">
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            TDS %:</label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtTDS" runat="server" onkeyup="Calculations();" onchange="return Numarics(this);"/>
                            </div>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTDS"
                            ErrorMessage="Please Enter TDS" Text="*" ValidationGroup="G2"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <%--<div class="col-md-5">--%>
                    <div class="form-group">                        
                        <div class="col-md-5">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtYearlyTDS" runat="server" onkeyup="Calculations();"
                                Enabled="false" onchange="return Numarics(this);"/>
                            </div>
                        </div>
                    </div>
                <%--</div>--%>
            </div>

            <div class="row top">
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                           Net Salary:</label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtNetSalary" runat="server" onkeyup="Calculations();" onchange="return Numarics(this);"/>
                            </div>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNetSalary"
                            ErrorMessage="Please Enter Net Salary" Text="*" ValidationGroup="G2">
                        </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
              <%--  <div class="col-md-5">--%>
                    <div class="form-group">
                        <div class="col-md-5">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtYearlyNetSalary" runat="server" onkeyup="Calculations();"
                                Enabled="false" onchange="return Numarics(this);"/>
                            </div>
                        </div>
                    </div>
               <%-- </div>--%>
            </div>

            <div class="row top" style="display:none">
                <div class="col-md-5">
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                           EducationCess:</label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="txtEducationCess" runat="server" onkeyup="Calculations();" onchange="return Numarics(this);"/>
                            </div>
                        </div>
                    </div>
                </div>      
            </div>
     
            <table border="0" align="center">
                <tr>
                    <td>
                        <asp:ValidationSummary ID="validSum1" runat="server" ValidationGroup="G2" CssClass="validationSummary"
                            ShowMessageBox="false" ShowSummary="true" DisplayMode="BulletList" Width="318px" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="pnlsearch" runat="server" class="panel-body">
        <div class="row top">
            <div class="col-md-5">
                <div class="form-group">
                    <label class="col-md-3 control-label">
                        Employee:</label>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlSearchEmploye" runat="server" AutoPostBack="True"
                            class="form-control select" OnSelectedIndexChanged="ddlSearchEmploye_SelectedIndexChanged"
                            Style="margin-bottom: 12px;">
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
        <div id="pnlrep" runat="server" class="table-responsive">
            <asp:GridView ID="gvSalaraySheet" DataKeyNames="EmployeeID"
                AutoGenerateColumns="False"
                runat="server" OnRowCommand="gvSalaraySheet_RowCommand"
                OnRowCreated="gvSalaraySheet_RowCreated" CssClass="table datatable  table-bordered table-striped table-actions"
                OnRowDeleting="gvSalaraySheet_RowDeleting"
                OnRowEditing="gvSalaraySheet_RowEditing">
                <Columns>
                    <asp:BoundField DataField="SNo" HeaderText="SNo" ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ItemStyle-CssClass="middle"
                        ItemStyle-Width="30" />
                    <asp:BoundField DataField="EmployeeNo" HeaderText="Employee No" ItemStyle-CssClass="middle"
                        ItemStyle-Width="30" />
                    <asp:BoundField DataField="DateOfJoining" HeaderText="Date Of Joining" ItemStyle-CssClass="middle"
                        ItemStyle-Width="30" />
                    <asp:BoundField DataField="BasicSalary" HeaderText="Basic Salary" ItemStyle-CssClass="middle"
                        ItemStyle-Width="30" />
                    <asp:BoundField DataField="GrossSalary" HeaderText="Gross Salary" ItemStyle-CssClass="middle"
                        ItemStyle-Width="30" />
                    <asp:BoundField DataField="NetSalary" HeaderText="Net Salary" ItemStyle-CssClass="middle"
                        ItemStyle-Width="30" />
                    <asp:TemplateField ItemStyle-Width="90">
                        <ItemTemplate>
                            <%--<asp:ImageButton runat="server" ID="" ToolTip="View" Text="View" ImageUrl="~/images/hammer_screwdriver.png"
                                CommandName="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            <asp:ImageButton runat="server" ID="" ToolTip="Edit" Text="Edit" ImageUrl="~/images/pencil.png"
                                CommandName="Edits" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                            <asp:ImageButton runat="server" ID="" ToolTip="Delete" Text="Delete"
                                ImageUrl="~/images/cross.png" CommandName="Deletes"
                                OnClientClick='return confirm("Do you want to delete the Record?");' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />--%>
                            <asp:LinkButton ID="imgBtnView" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                CommandName="View"
                                Style="text-decoration: none;" CssClass="fav fa-eye" ToolTip="View"></asp:LinkButton>
                            <asp:LinkButton ID="imgBtnEdit" runat="server" CommandName="Edits" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                Style="text-decoration: none;" CssClass="fae fa-pencil" ToolTip="Edit">
                            </asp:LinkButton>
                            <asp:LinkButton ID="imgBtnDelete" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                OnClientClick="return confirm('Do you want to Delete the record?');"
                                CommandName="Deletes" runat="server" ToolTip="Delete"
                                Style="text-decoration: none;"> <span class="fad fa-times"> </span></asp:LinkButton>

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
                <asp:Button ID="imgAdd" ValidationGroup="G2" runat="server" CssClass="btn btn-success btn-block pull-right"
                    Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" OnClientClick="this.disabled = true;" 
                                             UseSubmitBehavior="false" 
                    OnClick="imgAdd_Click" />
            </div>
            <div class="col-md-2" id="dvupdate" runat="server">
                <asp:Button ID="imgUpdate" ValidationGroup="G2" Visible="false" runat="server"
                    CssClass="btn btn-success btn-block pull-left"
                    Text="Update" CausesValidation="true"
                    Style="margin-bottom: 5px;" OnClick="imgUpdate_Click" />
            </div>
            <div class="col-md-2" id="dvclear" runat="server">
                <asp:Button ID="imgClear" runat="server" CssClass="btn btn-warning btn-block pull-left"
                    alt="Clear" Text="Clear" Style="margin-bottom: 5px;"
                    OnClick="imgClear_Click" />
            </div>
            <div class="col-md-2" id="dvcancel" runat="server">
                <asp:Button ID="btncan" runat="server" CssClass="btn btn-danger btn-block pull-left"
                    Text="Cancel" Style="margin-bottom: 5px;" OnClick="btncan_Click" />
            </div>
        </div>
    </div>

   
    </form>
    </div>
    </div>
    </div>
    </form>
    </asp:Content>
