<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Sales.aspx.cs" Inherits="Reports_Sales" Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ToolkitScriptManager ID="tsm" runat="server"> </asp:ToolkitScriptManager>--%>
    <asp:Label ID="lblTitle" Text="Date Sales " runat="server" CssClass="TitleClass"
        Visible="false"></asp:Label>
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SalesForPrint(salesid) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{salesid:'" + salesid + "'}",
                dataType: "json",
                url: "Sales.aspx/SalesPrint",
                success: function (data) {
                    if (data.d.length != 0) {
                        var sale = data.d.eSales;
                        var Salelist = data.d.eSaleslist;
                        var salepriscription = data.d.eSalePriscription;


                        if (Salelist.length != 0) {
                            $("body").append('<div id="modalOverlay"  class="modalOverlay">');
                            $("#divprintarea").show();
                            $("#<%=lblStoreNamePopup.ClientID%>").html(sale[0].StoreName);
                            $("#<%=lblAddressPopup.ClientID%>").html(sale[0].Address);
                            $("#<%=lblCustomerNamePopup.ClientID%>").html(":" + sale[0].CustomerName);
                            $("#<%=lblCustomerNoPopup.ClientID%>").html(":" + sale[0].CustomerNo);
                            $("#<%=lblTotalGrossValue.ClientID%>").html(sale[0].GrossTotal);
                            $("#<%=lblDiscount.ClientID%>").html(sale[0].Discount);
                            $("#<%=lblNetValue.ClientID%>").html(sale[0].NetTotal);
                            $("#<%=lblLoginUser.ClientID%>").html(sale[0].LoginName);
                            $("#<%=lblprintInvoiceNo.ClientID %>").html(sale[0].InvoiceNo);
                            $("#lblprintInvoiceDate").html(sale[0].InvoiceDate);

                            var AmountPaid = parseInt((sale[0].PaidAmount));

                            var Balance = parseInt(Salelist[0].Balance);

                            $("#<%=lblsavePopupAmountPaid.ClientID %>").html(AmountPaid);
                            $("#<%=lblsavePopupBal.ClientID %>").html(Balance);
                            $("#tblSalesPrint > tbody").empty();
                            for (var i = 0; i < Salelist.length; i++) {
                                $("#tblSalesPrint > tbody").append("<tr><td style='text-align:center;'><lable style='color:black;font-size:12px;font-family:verdana;'>" + Salelist[i].CategoryName + "</label></td><td style='text-align:center;'><lable style='color:black;font-size:12px;font-family:verdana;'>" + Salelist[i].BrandName + "</label></td><td style='text-align:center;'><lable style='color:black;font-size:12px;font-family:verdana;'>" +
                          Salelist[i].ProductName + "</label></td><td style='text-align:center;'><lable style='color:black;font-size:12px;font-family:verdana;'>" + Salelist[i].ProductValue + "</label></td><td style='text-align:center;'><lable style='color:black;font-size:12px;font-family:verdana;'>" + Salelist[i].Quantity + "</label></td><td style='text-align:center;'><lable style='color:black;font-size:12px;font-family:verdana;'>" + Salelist[i].SellingPrice
                          + "</label></td>" + "</tr>");
                            }
                            for (var i = 0; i < Salelist.length; i++) {
                                var str = "LENSE";
                                var str2 = Salelist[i].CategoryName.toUpperCase();
                                $("#tblSalesPrint > tbody").append(
                                "<tr style='font-size:12px;font-family:verdana'>" +
                                "<td align='left' colspan='8'></td>" +
                               " <table border='1' width='400px'><tr style='font-size:12px;font-family:verdana;color:White;background-color:Black;'><th  style='background-color:black;color:white;'>Prescription Details</th><th style='background-color:black;color:white;'>SPH</th><th style='background-color:black;color:white;'>CYL</th><th style='background-color:black;color:white;'>AXIS</th><th style='background-color:black;color:white;'>ADD</th><th style='background-color:black;color:white;'></th></tr><tr style='font-size:12px;font-family:verdana;'><td>Right Eye</td><td>" + salepriscription[0].SPH_RightEye +
                               "</td><td>" + salepriscription[0].CYL_RightEye + "</td><td>" + salepriscription[0].AXIS_RightEye + "</td><td>" + salepriscription[0].ADD_RightEye + "</td><td></td></tr>" +
                               "<tr style='font-size:12px;font-family:verdana;'><td>Left Eye</td><td>" + salepriscription[0].SPH_LeftEye + "</td><td>" + salepriscription[0].CYL_LeftEye + "</td><td>" + salepriscription[0].AXIS_LeftEye + "</td><td>" + salepriscription[0].ADD_LeftEye + "</td><td></td></tr>" +
                                "<tr style='font-size:12px;font-family:verdana;'><td>IPD</td><td>" + salepriscription[0].SPH_IPD + "</td><td>" + salepriscription[0].CYL_IPD + "</td><td>" + salepriscription[0].AXIS_IPD + "</td><td>" + salepriscription[0].ADD_IPD + "</td><td></td></tr>" +
                              +"</table></td></tr>");
                                break;

                            }
                            $("table#tblSalesPrint tr:even").css("background-color", "#F3F3F3");
                            $("table#tblSalesPrint tr:odd").css("background-color", "#ffffff");
                        }
                    }
                    else {
                        $("#divprintarea").hide();
                        $("body").append('<div id="modalOverlay"  class="">');
                    }
                },
                error: function (result)
                { }
            });
        }
        function printDiv() {
            $("#btnPrint").hide();
            $("#<%=btnCancel.ClientID%>").hide();
            $("#invpopup").css("display", "none");
            $("#dvpopup2").css("display", "block");
            var printarea = $("#divprintarea").html();
            var w = window.open();
            $("#tblSalesPrint th").css("background-color", "black");
            $("#tblSalesPrint th").css("color", "white");
            $("#tblSalesPrint tr:even").css("background-color", "White");
            $("#tblSalesPrint tr:odd").css("background-color", "#f2f2f2");

            w.document.writeln(printarea);
            w.print();
            $("#btnPrint").show();
            $("#<%=btnCancel.ClientID%>").show();
            $("#invpopup").css("display", "block");
            $("#dvpopup2").css("display", "none");
        }


        function SalesPrint(salesid) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{salesid:'" + salesid + "'}",
                dataType: "json",
                url: "SalesDetails.aspx/SalesPrint",
                success: function (data) {
                    if (data.d.length != 0) {
                        var sale = data.d.eSales;
                        var Salelist = data.d.eSaleslist;
                        $("#divPrint").show();
                        $("#lblprintStoreName").html(sale[0].StoreName);
                        $("#lblCustomerprintName").html(sale[0].CustomerName);
                        $("#lblprintCustomerCustomerNo").html(sale[0].CustomerNo);
                        $("#lblCustomerInvoiceNO").html(sale[0].InvoiceNo);
                        $("#lblStoreprintAddress").html(sale[0].Address);
                        $("#lblStoreVATID").html(sale[0].GrossTotal);
                        $("#lblCountry").html(sale[0].Country);
                        $("#lblStoreprintCity").html(sale[0].City);
                        $("#lblContNum").html(sale[0].ContactNum);
                        $("#lblEmail").html(sale[0].Email);
                        $("#lblInvoiceNoteDate").html(sale[0].InvoiceDate);
                        if (data.d.eSaleslist.length == 0) {
                            $("#divPrint").hide();
                            //$("#divPrint").css("cursor", "pointer");
                            return false;
                        }
                        else {
                            $("body").append('<div id="Overlay" class="modalOverlay">');
                            $("#tblPrintSales > tbody").empty();
                            for (var i = 0; i < Salelist.length; i++) {
                                $("#tblPrintSales > tbody").append("<tr><td>" + Salelist[i].CategoryName + "</td><td>" + Salelist[i].BrandName + "</td><td>" +
                          Salelist[i].ProductName + "</td><td>" + Salelist[i].ProductValue + "</td><td>" + Salelist[i].Quantity + "</td><td>" + Salelist[i].SellingPrice);
                            }
                            for (var i = 0; i < Salelist.length; i++) {
                                var str = "LENSE";
                                var str2 = Salelist[i].CategoryName.toUpperCase();
                                if (str2 == str) {
                                    $("#tblPrintSales > tbody").append("<tr style='background-color:White;height:20px'>" +
                                         "<td align='center' colspan='6'></td></tr>" +
                                  "<tr>" +
                                  "<td align='left' colspan='6'>Prescription Details" +
                                 " <table width='750px'><tr><th>Right Eye</th><td>" + sale[0].SPH_RightEye +
                                 "</td><td>" + sale[0].CYL_RightEye + "</td><td>" + sale[0].AXIS_RightEye + "</td><td>" + sale[0].ADD_RightEye + "</td></tr>" +
                                 "<tr><th>Left Eye</th><td>" + sale[0].SPH_LeftEye + "</td><td>" + sale[0].CYL_LeftEye + "</td><td>" + sale[0].AXIS_LeftEye + "</td><td>" + sale[0].ADD_LeftEye + "</td></tr>" +
                                  "<tr><th>IPD</th><td>" + sale[0].SPH_IPD + "</td><td>" + sale[0].CYL_IPD + "</td><td>" + sale[0].AXIS_IPD + "</td><td>" + sale[0].ADD_IPD + "</td></tr>" +
                                +"</table></td></tr>");
                                    break;
                                }

                            }
                            var Remarks = Salelist[0].Remarks
                            if (Remarks == "" || Remarks == null) {
                                Remarks = "";
                            }
                            var PaidAmount = parseFloat($("txtPaidAmount").value);
                            $("#tblPrintSales > tfoot").append("<tr style='background-color:White;height:20px'>" +
                                         "<td align='center' colspan='6'></td>" +
                                        "</tr>");

                            $("#tblPrintSales > tfoot").append("<tr>" +
                                         "<td align='center' colspan='6'><b>Details:</b></td>" +
                                        "</tr>");


                            $("#tblPrintSales > tfoot").append("<tr style='background-color:White;'>" +
                                                            "<td align='left' colspan='6'><b>Remarks:</b></td>" +
                                                              "</tr>");
                            $("#tblPrintSales > tfoot").append("<tr>" +
                                         "<td align='left' colspan='3' rowspan='6'>" + Remarks + "</td>" +
                                        "</tr>");

                            $("#tblPrintSales > tfoot").append("<tr>" +
                                                             "<td align='right' colspan='2'><b>Selling Price:</b></td>" +
                                                               "<td align='center'>" + Salelist[0].TotalGrossValue + "</td>" +
                                                             "</tr>");
                            $("#tblPrintSales > tfoot").append("<tr>" +
                                       "<td   align='right' colspan='2'><b>Discount (%):</b></td>" +
                                      "<td  align='center'>" + Salelist[0].Discount + "</td>" +
                                         "</tr>");
                            $("#tblPrintSales > tfoot").append("<tr>" +
                                       "<td   align='right' colspan='2'><b>Net Price:</b></td>" +
                                      "<td  align='center'>" + Salelist[0].NetValue + "</td>" +
                                         "</tr>");
                            $("#tblPrintSales > tfoot").append("<tr>" +
                                      "<td align='right' colspan='2'><b>Amount Paid:</b></td>" +
                                      "<td align='center'>" + sale[0].PaidAmount + "</td>" +
                                          "</tr>");
                            $("#tblPrintSales > tfoot").append("<tr>" +
                                                             "<td align='right' colspan='2'><b>Balance:</b></td>" +
                                                               "<td align='center'>" + Salelist[0].Balance + "</td>" +
                                                             "</tr>");

                        }
                    }
                    else {
                        $("#divPrint").hide();
                    }
                },
                error: function (result)
                { }
            });
        }
        function Divprint() {
            $("#PrintBtn").hide();
            $("#<%=btnCancel.ClientID%>").hide();
            var printarea = $("#divPrint").html();
            var w = window.open();
            $("#tblPrintSales th").css("background-color", "#ccc");
            $("#tblPrintSales tr:even").css("background-color", "White");
            $("#tblPrintSales tr:odd").css("background-color", "#f2f2f2");

            w.document.writeln(printarea);
            w.print();
            $("#PrintBtn").show();
            $("#<%=btnCancel.ClientID%>").show();

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
                                    <strong>Date Sales</strong>
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
                                                Customer Name</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtCustomerName" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Customer Name</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtCustomerNo" runat="server" Enabled="false" />
                                                </div>
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
                                                <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                                    AutoPostBack="True"
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
                                                <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"
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
                                                Invoice No</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="txtInvoiceNo" runat="server" AutoPostBack="True"
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
                                                        CssClass="form-control datepicker" runat="server" onchange="TotalDateChecking();datevalidation(this);"></asp:TextBox><%--onchange="CompareDate()"--%>
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
                                                <asp:RadioButton ID="rbtnSummary" runat="server" Checked="true"
                                                    CssClass="Label" GroupName="SDN"
                                                    OnCheckedChanged="rbtnSummary_CheckedChanged" AutoPostBack="true" />
                                                <asp:Label ID="Label3" runat="server" Text="Summary" CssClass="Label"></asp:Label>
                                                <asp:RadioButton ID="rbtnDetailed" runat="server" Text=""
                                                    Checked="false" CssClass="Label" GroupName="SDN"
                                                    OnCheckedChanged="rbtnDetailed_CheckedChanged" AutoPostBack="true" />
                                                <asp:Label ID="Label4" runat="server" Text="Detailed" CssClass="Label"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                            </label>
                                            <div class="col-md-8">
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
                                    <div class="table-responsive" id="dvsales">
                                        <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                            CssClass="table datatable table-bordered table-striped table-actions" OnRowDataBound="gvSales_RowDataBound"
                                            OnRowCommand="gvSales_RowCommand"
                                            DataKeyNames="SaleID">
                                            <Columns>
                                                <%-- <asp:BoundField DataField="SNo" HeaderText="SNo" Visible="false" />--%>
                                                <%-- <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />--%>
                                                <%--<asp:BoundField DataField="StoreName" HeaderText="Store Name" />--%>
                                                <asp:TemplateField HeaderText="Store Name" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupplierName" runat="server" Text='<%#Bind("StoreName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotal" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="CustomerNo" HeaderText="Customer No" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <%--<asp:BoundField DataField="TotalValue" HeaderText="Amount" Visible="false"/>--%>
                                                <asp:TemplateField
                                                    HeaderText="Invoice No" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("InvoiceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("InvoiceNo") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <%--  <FooterTemplate>
                                                        <asp:Label ID="tottext" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>--%>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Amount" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNetTotal" runat="server" Text='<%# Bind("NetTotal") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("NetTotal") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalNetTotal" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Previous Paid Amount" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpreviousamt" runat="server" Text='<%# Bind("PreviousAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("PreviousAmount") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalpreviousamt" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paid Amount" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGross" runat="server" Text='<%# Bind("PaymentAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("PaymentAmount") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalGross" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance Value" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscount" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalDiscount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkbtnPrint" runat="server" ToolTip="Print" Text="Print" ImageUrl="~/images/print.jpg"
                                                            Style="height: 20px; width: 20px" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                            CommandName="Print" ValidationGroup="False" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblDetails" CssClass="Label2" runat="server" Text="Details" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                            CommandName="Detail"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <%-- <HeaderStyle ForeColor="Gray" />
                                                    <ItemStyle ForeColor="Gray" HorizontalAlign="Center" />--%>
                                                </asp:TemplateField>
                                            </Columns>
                                            <%-- <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                            <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                            <HeaderStyle Height="25px" HorizontalAlign="Center" />--%>
                                        </asp:GridView>
                                    </div>
                                    <br />

                                    <div class="table-responsive" id="Div11">
                                        <asp:GridView ID="gvPreviousInvoicePayments" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="true"  CssClass="table table-bordered table-striped table-actions"
                                            DataKeyNames="SaleID" OnRowDataBound="gvPreviousInvoicePayments_RowDataBound"> 
                                            <Columns>
                                            
                                                    <asp:TemplateField HeaderText="Store Name" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupplierName" runat="server" Text='<%#Bind("StoreName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotal" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name"  ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                <asp:BoundField DataField="CustomerNo" HeaderText="Customer No"  ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                               
                                                <asp:TemplateField
                                                    HeaderText="Invoice No"  ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPreInvoiceNo" runat="server" Text='<%# Bind("InvoiceNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtPreInvoiceNo" runat="server" Text='<%# Bind("InvoiceNo") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                   
                                                </asp:TemplateField>
                                              <%--  <asp:TemplateField
                                                    HeaderText="Invoice Date" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPreInvoiceDate" runat="server" Text='<%# Bind("InvoiceDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtPreInvoiceDate" runat="server" Text='<%# Bind("InvoiceDate") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="Pretottext" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Invoice Amount"  ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPreNetTotal" runat="server" Text='<%# Bind("NetTotal") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtPreNetTotal" runat="server" Text='<%# Bind("NetTotal") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblPreTotalNetTotal" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paid Amount"  ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPreGross" runat="server" Text='<%# Bind("PaymentAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtPreGross" runat="server" Text='<%# Bind("PaymentAmount") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalGross" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance Value"  ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPreBalance" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblPreTotalDiscount" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                        </div>

                                      <br />
                                    <div class="table-responsive" id="Div22">
                                        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered table-striped table-actions">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Today Invoice Payment" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTodayInvoicePayment" runat="server" Text='<%#Eval("TodayInvPaidAmount") %>'
                                                     ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Previous Invoice Payment" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPreviousInvoicePayment" runat="server" Text='<%#Eval("PreviousInvPaidAmount") %>'
                                                          ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Payment" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalPayment" runat="server" Text='<%#Eval("Total") %>'
                                                            ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>                                 
                                          </div>
                                          <div class="table-responsive">
                                             <asp:GridView ID="gvCashCard" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered table-striped table-actions">
                                            <Columns>
                                                <asp:TemplateField HeaderText="PaymentMethod" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCashInvoicePayment" runat="server" Text='<%#Eval("PaymentMode") %>'
                                                     ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCardInvoicePayment" runat="server" Text='<%#Eval("Amount") %>'
                                                          ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                          </div>

                                    <div class="table-responsive" id="Div1">
                                        <asp:GridView ID="gvSaleDetails" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                            CssClass="table datatable table-bordered table-striped table-actions" OnRowDataBound="gvSaleDetails_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="SNo" HeaderText="SNo" Visible="false" />
                                                <asp:TemplateField HeaderText="Store Name" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStoreName" runat="server" Text='<%#Bind("StoreName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotal" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />--%>
                                                <asp:BoundField DataField="StoreName" HeaderText="Store Name" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="CustomerName" HeaderText="Customer" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="CustomerNo" HeaderText="Customer No" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="CategoryName" HeaderText="Category" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="BrandName" HeaderText="Brand " ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:TemplateField HeaderText="Model No " FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>

                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ProductName") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="tottext" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProductValue" HeaderText="Selling Price" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <asp:TemplateField HeaderText="Gross Total" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGross" runat="server" Text='<%# Bind("GrossTotal") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                        <%--Text='<%# Bind("GrossTotal") %>'--%>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalGross" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Discount" HeaderText="Discount(%)" ItemStyle-CssClass="middle"
                                                    ItemStyle-Width="30" />
                                                <%--<asp:BoundField DataField="TotalValue" HeaderText="Amount" Visible="false"/>--%><asp:TemplateField
                                                    HeaderText="Tot. Selling Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSellingPrice" runat="server" Text='<%# Bind("SellingPrice") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("SellingPrice") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalSellingPrice" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net Total" ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNetTotal" runat="server" Text='<%# Bind("NetTotal") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("NetTotal") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalNetTotal" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>                                           
                                        </asp:GridView>
                                    </div>
                                    <center>
                                        <asp:Button ID="btnprint" runat="server" Text="Print" class="btn btn-info" Visible="false"
                                            ToolTip="print report" OnClick="btnprint_Click" />
                                        <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-danger dropdown-toggle btnexportprint"
                                            Visible="false" ToolTip="export report" OnClick="btnExport_Click" />
                                    </center>
                                </div>
                                <div id="dvDetails" style="display: none; z-index: 1040;" class="modal fade in"
                                    role="dialog" tabindex="-1"
                                    aria-hidden="false" runat="server">                                    
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
                                                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false"  CssClass="table table-bordered table-striped table-actions">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Payment Date " ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="InvoiceNo" runat="server" CssClass="Label1" Text='<%#Eval("PaymentDate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment Amount" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="InvoiceDate" runat="server" CssClass="Label1" Text='<%#Eval("PaymentAmount")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Balance" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Quantity" runat="server" CssClass="Label1" Text='<%#Eval("Balance")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <br />
                                                <center>
                                                    <asp:Button ID="btnDetailCancel" runat="server" CssClass="btn btn-danger" OnClick="btnDetailCancel_Click"
                                                        Text="Cancel" />
                                                </center>
                                                <h4></h4>
                                                <h4></h4>
                                                <h4></h4>
                                                <h4></h4>
                                                <h4></h4>
                                                <h4></h4>
                                                <h4></h4>
                                                <h4></h4>
                                                <h4></h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divprintarea1" class="modal" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-lg modal-position">
                                        <div class="modal-content" id="printdiv">
                                            <div class="modal-header">
                                               
                                                <center>
                                                    <h2 class="modal-title">Invoice
                                                    </h2>
                                                </center>
                                              
                                                <div id="dvpopup2" style="display: none; height: 70px;"></div>
                                                <br />

                                                <div id="dvheight" style="display: none; height: 20px;"></div>
                                                <table width="800px" align="center" id="table1">
                                                    <tr>
                                                        <td style="width: 30px;"></td>
                                                        <td align="left" class="Label">
                                                          
                                                            <asp:Label ID="lblStoreNamePopup" runat="server" Style="display: none;"></asp:Label>
                                                            <br />
                                                            
                                                            <label class="Label">
                                                                Invoice No:</label>
                                                            <asp:Label ID="lblprintInvoiceNo1" runat="server" Style="float: none;"></asp:Label>
                                                            <br />
                                                            <label class="Label">
                                                                Invoice Date:</label>
                                                         
                                                            <label id="lblprintInvoiceDate1" style="float: none;"></label>
                                                            <br />
                                                            <asp:Label ID="lblAddressPopup" runat="server" Style="margin-top: auto; display: none;"></asp:Label>
                                                            <br />
                                                            <label id="Label27" class="Label">
                                                            </label>
                                                        </td>
                                                        <td width="100px"></td>
                                                        <td style="width: 300px;">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 120px;">
                                                                        <label class="Label2" style="color: Black; float: left; margin-bottom: -5px;">
                                                                            Customer
                                    Name</label></td>
                                                                    <td style="width: 160px;">
                                                                        <asp:Label ID="lblCustomerNamePopup1" runat="server" Style="color: Black; float: left;
                                                                            margin-bottom: -5px;"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label class="Label" style="color: Black; float: left;">Contact No</label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblCustomerNoPopup1" runat="server" CssClass="" Style="color: Black;
                                                                            float: left;"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="modal-body">
                                                <div class="table-responsive">
                                                    <table id="tblSalesPrint1" width="750px" align="center" border="1px;" style="border-spacing: 1px;">
                                                        <thead>
                                                            <tr>
                                                                <th style="color: white; background-color: black;">Category
                                                                </th>
                                                                <th style="color: white; background-color: black;">Brand
                                                                </th>
                                                                <th style="color: white; background-color: black;">Model No
                                                                </th>
                                                                <th style="color: white; background-color: black;">Selling Price
                                                                </th>
                                                                <th style="color: white; background-color: black;">Quantity
                                                                </th>
                                                                <th style="color: white; background-color: black;">Total
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                        </tbody>
                                                        <tfoot>
                                                           
                                                        </tfoot>
                                                    </table>
                                                </div>
                                            </div>
                                          
                                        </div>
                                    </div>
                                </div>
                                <form name="SubOrgForm" novalidate>
                                <div class="modal" id="divprintarea" tabindex="-1" role="dialog" aria-hidden="true" style="height: 100%; overflow-y: auto;">
                                    <div class="modal-dialog modal-lg modal-position" id="Div2">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" style="display:none;"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                                <center><h4 class="modal-title">Invoice</h4></center>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                     <div class="col-md-6" style="width:35%;float:left;margin-left:15%;">
                                                        <div class="row">
                                                           <%-- <div class="row">
                                                                <div class="form-group">
                                                                     
                                                                    <div class="row"--%>                                                                      
                                                                        <div class="input-group">
                                                                            <label id="Label19" class="Label">Invoice No</label>
                                                                            <label id="lblprintInvoiceNo" runat="server" class="Label"></label>
                                                                        <%--</div>
                                                                    </div>
                                                                </div>--%>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="row">
                                                               <%-- <div class="form-group">
                                                                  
                                                                    <div class="row">
                                                                        <div class="input-group">--%>
                                                                              <label id="Label18" class="Label pull-left">Invoice Date</label>
                                                                            <label id="lblprintInvoiceDate" class="Label"></label>
                                                                       <%-- </div>
                                                                    </div>
                                                                </div>--%>
                                                            </div>
                                                        </div>
                                                       
                                                    </div>
                                                    <div class="col-md-6" style="width:46%;float:right;">
                                                        <div class="row " style="margin-top: 1%">
                                                            <div class="col-md-12">
                                                                <div class="form-group">                                                                    
                                                                    <div class="col-md-12">
                                                                        <div class="input-group">
                                                                             <label id="Label23" class="Label">Customer Name</label>
                                                                            <label id="lblCustomerNamePopup" runat="server" class="Label"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row " style="margin-top: 1%">
                                                            <div class="col-md-12">
                                                                <div class="form-group">                                                                      
                                                                    <div class="col-md-12">
                                                                        <div class="input-group">
                                                                            <label id="Label25" class="Label">Contact No</label>
                                                                            <label id="lblCustomerNoPopup" runat="server" class="Label"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>                                                      
                                                    </div>
                                                </div>                                                
                                            <div class="dataTables_wrapper no-footer" style="margin-top: 10px;">
                                                <div class="panel-body panel-body-table" style="border:hidden !important;">
                                                    <div class="table-bordered">
                                                        <div class="table-responsive">
                                                             <table width="750px" align="center" id="tblSalesPrint" border="1" class="table table-bordered table-striped table-actions">
                                                                  <thead>
                                                                    <tr>
                                                                        <th style="color: white; background-color: black;">Category
                                                                        </th>
                                                                        <th style="color: white; background-color: black;">Brand
                                                                        </th>
                                                                        <th style="color: white; background-color: black;">Model No
                                                                        </th>
                                                                        <th style="color: white; background-color: black;">Selling Price
                                                                        </th>
                                                                        <th style="color: white; background-color: black;">Quantity
                                                                        </th>
                                                                       
                                                                        <th style="color: white; background-color: black;">Total
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                  <tbody>
                                                                </tbody>
                                                                <tfoot>
                                                                     <tr style="display: none;">
                                                                <td colspan="5" align="right">
                                                                    <label id="Label7">
                                                                        Total Selling Price:</label>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblTotalGrossValue" runat="server" CssClass="Label2" Style="margin-top: 0px;
                                                                        color: Black;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none;">
                                                                <td align="right" colspan="5">
                                                                    <label id="Label8">
                                                                        <b>Discount (%):</b></label>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblDiscount" runat="server" CssClass="Label2" Style="margin-top: 0px;
                                                                        color: Black;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style='color: black; font-size: 12px; font-family: verdana;'>Details</td>
                                                                <td colspan="4" align="right">
                                                                    <label id="Label9" style="float: right;">
                                                                        Total Amount:</label>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblNetValue" runat="server" CssClass="Label2" Style='color: black;
                                                                        font-size: 12px; font-family: verdana;'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" align="right">
                                                                    <label id="LabelsavePopupAmountPaid">
                                                                        Amount Paid:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblsavePopupAmountPaid" runat="server" CssClass="Label2" Style='color: black;
                                                                        font-size: 12px; font-family: verdana;'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbluse" runat="server" CssClass="Label2" Style='color: black; font-size: 12px;
                                                                        font-family: verdana;'>User:</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblLoginUser" runat="server" CssClass="Label" Style="color: Black;"></asp:Label>
                                                                </td>

                                                                <td colspan="3" align="right">
                                                                    <label id="LabelBalance">
                                                                        Balance:</label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblsavePopupBal" runat="server" CssClass="Label2" Style='color: black;
                                                                        font-size: 12px; font-family: verdana;'></asp:Label>
                                                                </td>
                                                            </tr>
                                                                </tfoot>
                                                             </table>
                                                          </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                             <div class="modal-footer">
                                                <input type="button" id="Button1" value="Print" class="BtnEmptyStyle" title="Print"
                                                    onclick="printDiv()" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="BtnEmptyStyle"
                                                    OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                                </form>
                            </div>
                            </div>
                    </form>
                </div>
            </div>
        </div>
    </form>
</asp:Content>


