<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="AvailableStock.aspx.cs" Inherits="Reports_AvailableStock" Title="Available Stock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../js/jquery.js"></script>
    <%--<asp:ToolkitScriptManager ID="tsm" runat="server"></asp:ToolkitScriptManager>--%>
    <asp:Label ID="lblTitle" Text="Available Stock" runat="server" CssClass="TitleClass"
        Visible="false"></asp:Label>

    <form runat="server" id="form">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Available Stock</strong>
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
                                <div class="row" style="display: none">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Supplier</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlSupplier" runat="server"
                                                    class="form-control select" Style="margin-bottom: 12px;">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
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
                                                    class="form-control select" Style="margin-bottom: 12px;" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
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
                                <div ID="pnlGrid" runat="server" >
                                <div class="table-responsive">
                                        <asp:GridView ID="gvStock" runat="server" AutoGenerateColumns="false"
                                            ShowFooter="true" OnRowCommand="gvStock_RowCommand" CssClass="table datatable table-bordered table-striped table-actions"
                                            OnRowDataBound="gvStock_RowDataBound">
                                            <Columns>
                                          <%--    <asp:TemplateField HeaderText="SNO"  ItemStyle-CssClass="middle" ItemStyle-Width="30" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblText" runat="server" Text='<%#Eval("SNO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblText" runat="server" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>--%>
                                              <%--  <asp:BoundField DataField="SNo" HeaderText="SNo" ItemStyle-CssClass="middle" ItemStyle-Width="30" />--%>
                                               <%-- <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-CssClass="middle" ItemStyle-Width="30"/>--%>
                                               <asp:TemplateField HeaderText="Category Name"  ItemStyle-CssClass="middle" ItemStyle-Width="30" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblText" runat="server" Text='<%#Eval("CategoryName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblText" runat="server" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                                <asp:BoundField DataField="BrandName" HeaderText="Brand Name" ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                <asp:TemplateField HeaderText="Model No" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                   <%-- <FooterTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>--%>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ProductValue" HeaderText="Selling Price" />--%>
                                                <asp:TemplateField HeaderText="Selling Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSellingPrice" runat="server" Text='<%#Bind("ProductValue") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotalsellingprice" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="AvailableQuantity" HeaderText="Available Quantity" />--%>
                                                <asp:TemplateField HeaderText="Available Quantity" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAvailableQuantity" runat="server" Text='<%#Bind("AvailableQuantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotalavailablequantity" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="TotalValue" HeaderText="Amount" Visible="false"/>--%>
                                            </Columns>
                                           <%-- <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                            <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                            <HeaderStyle Height="25px" HorizontalAlign="Center" />--%>
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
