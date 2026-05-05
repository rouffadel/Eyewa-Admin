<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Balance.aspx.cs" Inherits="Reports_Balance" Title="Balance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <asp:Label ID="lblTitle" Text="Balance Report" runat="server" CssClass="TitleClass"
        Visible="false"></asp:Label>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <%-- <asp:ToolkitScriptManager ID="tsm" runat="server"> </asp:ToolkitScriptManager>--%>
    <form runat="server" id="form">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Balance Report</strong>
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

                            <div class="panel-body">
                                <div class="row top">
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
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Invoice No</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtInvoiceNo" runat="server" Enabled="false" />
                                                </div>
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
                                                    <asp:TextBox ID="txtFromDate"
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
                                                            CssClass="btn btn-warning" OnClick="imgbtnClear_Click" />
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="pnlGrid" runat="server">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvBalance" runat="server" AutoGenerateColumns="false"
                                            ShowFooter="true" HorizontalAlign="Center" CssClass="table datatable table-bordered table-striped table-actions"
                                            OnRowDataBound="gvBalance_RowDataBound">
                                            <Columns>

                                               <%-- <asp:TemplateField HeaderText="SNO" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblText" runat="server" Text='<%#Eval("SNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblText" runat="server" Text="Total" ></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField="StoreName" HeaderText="Store Name" ItemStyle-CssClass="middle" ItemStyle-Width="30" FooterText="Total" FooterStyle-Font-Bold="true"/>
                                                <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" ItemStyle-CssClass="middle" ItemStyle-Width="30"/>
                                                <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" ItemStyle-CssClass="middle" ItemStyle-Width="30"/>
                                                <asp:TemplateField HeaderText="Invoice Amount" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceAmount" runat="server" Text='<%#Eval("InvoiceAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalInvoiceAmount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paid Amount" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaidAmount" runat="server" Text='<%#Eval("PaidAmount")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalPaidAmount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance Amount" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#Eval("BalanceAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalBalanceAmount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                     <center>
                                    <asp:Button ID="btnprint" runat="server" Text="Print"  class="btn btn-info" Visible="false"
                                        ToolTip="print report" OnClick="btnprint_Click" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-danger dropdown-toggle btnexportprint"
                                        Visible="false" ToolTip="export report" OnClick="btnExport_Click" />
                                </center>
                                    <%--<center>
                                        <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="BtnEmptyStyle" Visible="false"
                                            ToolTip="print report" OnClick="btnprint_Click" />
                                        <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="BtnEmptyStyle"
                                            Visible="false" ToolTip="export report" OnClick="btnExport_Click" />
                                    </center>--%>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
