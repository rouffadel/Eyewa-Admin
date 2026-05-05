<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Expense.aspx.cs" Inherits="Reports_Expense" Title="Expense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:Label ID="lblTitle" Text="Expense Report" runat="server" CssClass="TitleClass" Visible="false"></asp:Label>
    <form runat="server" id="form">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Expense Report</strong>
                                </h3>
                            </div>
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
                             <div id="pnlSearch" runat="server">
                                <div class="panel-body">


                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Store</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlStore" runat="server"
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
                                                    From Date</label>
                                                <div class="col-md-8">
                                                    <div class="input-group pull-right">
                                                        <asp:TextBox ID="txtFromDate" onchange="TotalDateChecking();datevalidation(this);"
                                                            CssClass="form-control datepicker" runat="server"></asp:TextBox><%--onchange="CompareDate()"--%>
                                                        <span class="input-group-addon"><span
                                                            class="fa fa-calendar"></span></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    To Date</label>
                                                <div class="col-md-8">
                                                    <div class="input-group pull-right">
                                                        <asp:TextBox ID="txtToDate" CssClass="form-control datepicker" onchange="TotalDateChecking();datevalidation(this);"
                                                            runat="server"></asp:TextBox>
                                                        <%--onchange="CompareDate()"--%>
                                                        <span class="input-group-addon"><span
                                                            class="fa fa-calendar"></span></span>
                                                    </div>
                                                    <label class="help-block">
                                                    </label>
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
                                                </label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <asp:Button ID="btnReport" runat="server" Text="Search"
                                                            CssClass="btn btn-info" OnClick="btnReport_Click" />&nbsp;&nbsp;
                                                        <asp:Button ID="imgbtnClear" runat="server" Text="Clear"
                                                            CssClass="btn btn-warning"  OnClick="imgbtnClear_Click" />
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                <%--<table style="width: 1060px;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblStore" runat="server" Text="Store" CssClass="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="" runat="server" CssClass="DropDownClass"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFromDate" CssClass="Label" runat="server" Text="From Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="" onchange="TotalDateChecking();datevalidation(this);"
                                                runat="server" CssClass="TextBoxStyle">
                                            </asp:TextBox>
                                            <asp:Image ID="imgDeliveryNoteFromDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromDate"
                                                PopupButtonID="imgDeliveryNoteFromDate" Enabled="true" EnabledOnClient="true"
                                                Format="dd-MM-yyyy">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblToDate" CssClass="Label" runat="server" Text="To Date"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="" onchange="TotalDateChecking();datevalidation(this);"
                                                runat="server" CssClass="TextBoxStyle">
                                            </asp:TextBox>
                                            <asp:Image ID="imgDeliveryNoteToDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" />
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                                PopupButtonID="imgDeliveryNoteToDate" Enabled="true" EnabledOnClient="true" Format="dd-MM-yyyy">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td></td>

                                        <td>
                                            <asp:Button ID="btnReport" Text="Report" runat="server" CssClass="BtnEmptyStyle"
                                                ToolTip="show report" OnClick="btnReport_Click" Style="float: right;" />
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:ImageButton ID="imgbtnClear" ImageUrl="~/images/clearBtn.png" AlternateText="Clear"
                                                runat="server" ToolTip="clear fields" OnClick="imgbtnClear_Click" />
                                        </td>
                                        <td></td>
                                    </tr>

                                </table>--%>
                            <br />
                            <div ID="pnlGrid" runat="server"  class="table-responsive">
                                    <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="false"  CssClass="table datatable table-bordered table-striped table-actions"
                                        ShowFooter="true"  OnRowDataBound="gvExpense_RowDataBound">
                                        <Columns>

                                            <asp:TemplateField HeaderText="SNO"  ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblText" runat="server" Text='<%#Eval("SNO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblText" runat="server" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StoreName" HeaderText="Store"  ItemStyle-CssClass="middle" ItemStyle-Width="30"/>
                                            <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type"  ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                            <asp:TemplateField HeaderText="Expense Amount"  ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpenseAmount" runat="server" Text='<%#Eval("ExpenseAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalExpenseAmount" runat="server" ></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ExpenseDate" HeaderText="Date"  ItemStyle-CssClass="middle" ItemStyle-Width="30"/>

                                            <asp:TemplateField HeaderText="Paid Amount"  ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaidAmount" runat="server" Text='<%#Eval("PaidAmount")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalPaidAmount" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Amount"  ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#Eval("Balance") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalBalanceAmount" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                <center>
                                    <asp:Button ID="btnprint" runat="server" Text="Print"  class="btn btn-info" Visible="false"
                                        ToolTip="print report" OnClick="btnprint_Click" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-danger dropdown-toggle btnexportprint"
                                        Visible="false" ToolTip="export report" OnClick="btnExport_Click" />
                                </center>
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

