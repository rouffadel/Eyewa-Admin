<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="AvailableStoreStock.aspx.cs" Inherits="Reports_AvailableStoreStock"
    Title="Available Store Stock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:Label ID="lblTitle" Text="Available Store Stock" runat="server" CssClass="TitleClass" Visible="false"></asp:Label>
    <script type="text/javascript" src="../js/jquery.js"></script>

    <%--    <asp:ToolkitScriptManager ID="tsm" runat="server">
    </asp:ToolkitScriptManager>--%>

    <script type="text/javascript">
        function dateselect(ev) {
            var calendarBehavior1 = $find("CStoreFromDate");
            var d = calendarBehavior1._selectedDate;
            var now = new Date();
            calendarBehavior1.get_element().value = d.format("dd-MM-yyyy") + " " + now.format("HH:mm:ss")
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
                                    <strong>Available Store Stock</strong>
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
                                    </div>
                                    <div class="row top">
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
                                                        <asp:TextBox ID="txtStoreFromDate" onchange="TotalDateChecking();datevalidation(this);"
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
                                                        <asp:TextBox ID="txtStoreToDate" CssClass="form-control datepicker" onchange="TotalDateChecking();datevalidation(this);"
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
                                                        <asp:Button ID="btnClear" runat="server" Text="Clear"
                                                            CssClass="btn btn-warning" OnClick="imgbtnClear_Click" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="pnlGrid" runat="server">
                                        <asp:GridView ID="gvStock" runat="server" AutoGenerateColumns="false"
                                            CssClass="table datatable table-bordered table-striped table-actions"
                                            OnRowCommand="gvStock_RowCommand" OnRowDataBound="gvStock_RowDataBound" ShowFooter="true">
                                            <Columns>

                                               <%-- <asp:TemplateField HeaderText="SNO" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblText" runat="server" Text='<%#Eval("SNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblText" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>--%>
                                                <%--<asp:BoundField DataField="SNo" HeaderText="SNo" />--%>
                                                <%--<asp:BoundField DataField="StoreName" HeaderText="Store Name" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />--%>
                                                     <asp:TemplateField HeaderText="Store Name" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblText" runat="server" Text='<%#Eval("StoreName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblText" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="BrandName" HeaderText="Brand Name" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:TemplateField HeaderText="Model No" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                 <%--   <FooterTemplate>
                                                        <asp:Label ID="lbltotal" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>--%>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Buying Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBuyingPrice" runat="server" Text='<%#Bind("BuyingPrice") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotalbuyingprice" runat="server"></asp:Label>
                                                    </FooterTemplate>
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
                                                <%-- <asp:BoundField DataField="Quantity" HeaderText="Available Quantity" />--%>
                                                <asp:TemplateField HeaderText="Available Quantity" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAvailableQuantity" runat="server" Text='<%#Bind("Quantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotalavailablequantity" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BP Total" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalBuyingPrice" runat="server" Text='<%#Bind("BPTotal") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFinalBuyingPrice" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SP Total" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" Text='<%#Bind("Total") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblFinalTotal" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="TotalValue" HeaderText="Amount" Visible="false"/>--%>
                                            </Columns>
                                         <%--   <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                            <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                            <HeaderStyle Height="25px" HorizontalAlign="Center" />--%>
                                        </asp:GridView>
                                        <center>
                                            <asp:Button ID="btnprint" runat="server" Text="Print" class="btn btn-info" Visible="false"
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
