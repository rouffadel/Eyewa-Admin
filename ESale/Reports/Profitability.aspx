<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="Profitability.aspx.cs" Inherits="Reports_Profitability"
    Title="Profit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../js/jquery.js"></script>

    <%--  <asp:ToolkitScriptManager ID="tsm" runat="server">
    </asp:ToolkitScriptManager>--%>

    <asp:Label ID="lblTitle" Text="Profit" runat="server" CssClass="TitleClass" Visible="false"></asp:Label>

    <form runat="server" id="form">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Profit</strong>
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
                                                    Organisation</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlOrganisation" runat="server"
                                                        class="form-control select" Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
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
                                                    Category</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlCategory" runat="server"
                                                        class="form-control select" Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Brand</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlBrand" runat="server"
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
                                                    Model No</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlProduct" runat="server"
                                                        class="form-control select" Style="margin-bottom: 12px;">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Sales Man</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlSalesMan" runat="server"
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
                                                        <asp:TextBox ID="txtToDate" CssClass="form-control datepicker"
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
                                    <div class="row top"></div>
                                    <div id="pnlGrid" runat="server">
                                        <div id="gridClass1" class="table-responsive">
                                            <asp:GridView ID="gvProfitability" runat="server" AutoGenerateColumns="false" DataKeyNames="ProductID"
                                                OnRowCommand="gvProfitability_RowCommand" ShowFooter="true" OnRowDataBound="gvProditablility_RowDataBound"
                                                CssClass="table datatable table-bordered table-striped table-actions">
                                                <Columns>
                                                    <%--<asp:TemplateField HeaderText="SNo" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" CssClass="control-label" Text="Total"
                                                                Style="color: Black;"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Store" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("StoreName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField DataField="StoreName" HeaderText="Store " ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />--%>
                                                    <asp:BoundField DataField="CategoryName" HeaderText="Category " ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField DataField="BrandName" HeaderText="Brand " ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField DataField="ProductName" HeaderText="Model No" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:TemplateField HeaderText="Selling Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductValue" runat="server" Text='<%#Eval("ProductValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalProductValue" runat="server"
                                                                Visible="false"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalQuantity" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount(%)" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiscount" runat="server" Text='<%#Eval("Discount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotalDiscount" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Selling Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSellingPrice" runat="server" Text='<%#Eval("Sellingprice")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalSellingPrice" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Buying Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrossBuyingPrice" runat="server" Text='<%#Eval("BuyingPrice")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalGrossBuyingPrice" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Profit" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProfit" runat="server" Text='<%#Eval("Profit")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalProfit" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="30" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblDetails" CssClass="control-label" runat="server" Text="Details"
                                                                CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                                Visible="false" CommandName="Detail"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <%--<RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                                <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                                <HeaderStyle Height="25px" HorizontalAlign="Center" />--%>
                                            </asp:GridView>
                                        </div>
                                        <br />
                                        <br />
                                        <div class="row top"></div>
                                        <div class="gridClass1">
                                            <asp:GridView ID="gvOrderLense" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                                ShowFooter="true"
                                                CssClass="table table-bordered table-striped table-actions" OnRowDataBound="gvOrderLense_RowDataBound"
                                                OnRowCreated="gvOrderLense_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Store" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstorename" runat="server" Text='<%#Eval("StoreName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblExpenseType" runat="server" Text="Total" CssClass="control-label"
                                                                Style="color: Black;"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Invoice No" DataField="InvoiceNo" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField HeaderText="Category" DataField="Category" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField HeaderText="Order Lense" DataField="OrderLense" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:BoundField HeaderText="Price" DataField="Price" ItemStyle-CssClass="middle"
                                                        ItemStyle-Width="30" />
                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotalquantity" runat="server" CssClass="control-label"
                                                                Style="color: Black;"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblfinaltotal" runat="server" CssClass="control-label"
                                                                Style="color: Black;"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <%--      <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                                <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                                <HeaderStyle Height="25px" HorizontalAlign="Center" />--%>
                                            </asp:GridView>
                                        </div>
                                        <br />
                                        <br />
                                        <div class="row top"></div>
                                        <div class="gridClass1">
                                            <asp:GridView ID="gvExpenses" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                                CssClass="table table-bordered table-striped table-actions"
                                                HorizontalAlign="Center" OnRowDataBound="gvExpenses_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Store" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstorename" runat="server" Text='<%#Eval("StoreName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblExpenseType" runat="server" Text="Total" CssClass="control-label"
                                                                Style="color: Black;"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Expense Type " ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExpenseType" runat="server" Text='<%#Eval("ExpenseType")%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Date" HeaderText="Date" />
                                                    <asp:TemplateField HeaderText="Expense Amount " ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExpenseAmount" runat="server" Text='<%#Eval("ExpenseAmount")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalExpenseAmount" runat="server" CssClass="control-label"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Paid Amount " ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPaidAmount" runat="server" Text='<%#Eval("PaidAmount")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalPaidAmount" runat="server" CssClass="control-label"
                                                                Style="color: Black;"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance " ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBalance" runat="server" CssClass="control-label" Text='<%#Eval("Balance")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalBalance" runat="server" CssClass="control-label"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <%-- <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                                <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                                <HeaderStyle Height="25px" HorizontalAlign="Center" />--%>
                                            </asp:GridView>
                                        </div>

                                        <br />
                                        <div class="gridClass1">
                                            <asp:GridView ID="gvProfit" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                                CssClass="table table-bordered table-striped table-actions" OnRowDataBound="gvProfit_RowDataBound"
                                                OnRowCreated="gvProfit_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sales Profit" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSalesProfit" runat="server" Text="0" CssClass="control-label" Style="color: Black;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Lense" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrderlense" runat="server" Text="0" CssClass="control-label" Style="color: Black;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Expenses" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExpenses" runat="server" Text="0" CssClass="control-label" Style="color: Black;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Final Profit" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFinalProfit" runat="server" Text="0" CssClass="control-label"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <%-- <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                                <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                                <HeaderStyle Height="25px" HorizontalAlign="Center" />--%>
                                            </asp:GridView>
                                            <center>
                                                <asp:Table ID="tblprofit" runat="server" Visible="false">
                                                    <asp:TableHeaderRow runat="server">
                                                        <asp:TableHeaderCell>
                                     Sales Profit
                                                        </asp:TableHeaderCell>
                                                        <asp:TableHeaderCell>
                                     Expenses
                                                        </asp:TableHeaderCell>
                                                        <asp:TableHeaderCell>
                                      Final Report
                                                        </asp:TableHeaderCell>
                                                    </asp:TableHeaderRow>

                                                    <asp:TableRow>

                                                        <asp:TableCell>
                                                            <asp:Label ID="lblSalesProfit" runat="server" Text="0"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow>

                                                        <asp:TableCell>
                                                            <asp:Label ID="lblExpensesProfit" runat="server" Text="0"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>

                                                    <asp:TableRow>

                                                        <asp:TableCell>
                                                            <asp:Label ID="lblFinalProfit" runat="server" Text="0"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>




                                                </asp:Table>
                                            </center>
                                        </div>
                                        <center>
                                            <center>
                                                <asp:Button ID="btnprint" runat="server" Text="Print" class="btn btn-info" Visible="false"
                                                    ToolTip="print report" OnClick="btnprint_Click" />
                                                <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-danger dropdown-toggle btnexportprint"
                                                    Visible="false" ToolTip="export report" OnClick="btnExport_Click" />
                                            </center>
                                            <%--  <table>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="BtnEmptyStyle" Visible="false"
                                                            ToolTip="print report" OnClick="btnprint_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="BtnEmptyStyle"
                                                            Visible="false" ToolTip="export report" OnClick="btnExport_Click" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>--%>
                                        </center>
                                    </div>
                                </div>
                            </div>
                            <div id="dvDetails" style="display: none; z-index: 1040;" class="modal fade in"
                                role="dialog" tabindex="-1"
                                aria-hidden="false" runat="server">
                                <%-- <h4 class="Label">
                                    <center>
                                        Details
                                    </center>
                                </h4>--%>
                                <div class="modal-backdrop fade in" style="height: 100%;">
                                </div>
                                <div class="modal-dialog modal-lg ">
                                    <div class="modal-content" style="height: 600px;">
                                        <div class="modal-header">
                                            <h4 class="modal-title">Details </h4>
                                        </div>
                                        <asp:Label ID="lblmsgprescriptin" runat="server"></asp:Label>
                                        <div class="modal-body" style="height: 520px; overflow-y: auto;">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" Width="600px"
                                                    HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Invoice No " ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvoiceNo" runat="server" CssClass="Label1" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Date" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="InvoiceDate" runat="server" CssClass="Label1" Text='<%#Eval("InvoiceDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Quantity" runat="server" CssClass="Label1" Text='<%#Eval("Quantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="SellingPrice" runat="server" CssClass="Label1" Text='<%#Eval("SellingPrice")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="SalesMan" HeaderStyle-CssClass="Label1" ItemStyle-CssClass="Label1"
                                                            DataField="SalesMan" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <center>
                                                <%--<asp:Button ID="btnPrescriptionSave" runat="server" Text="Save" 
                             CssClass="BtnEmptyStyle" onclick="btnPrescriptionSave_Click" />--%>
                                                <asp:Button ID="btnDetailCancel" runat="server" Text="Cancel" CssClass="BtnEmptyStyle"
                                                    OnClick="btnDetailCancel_Click" />
                                            </center>
                                        </div>
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
