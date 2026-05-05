<%@ Page Title="Stock Opening Balance" Language="C#" MasterPageFile="~/Admin/Admin.master"
    AutoEventWireup="true" CodeFile="StockOpeningBalance.aspx.cs" Inherits="Screens_StockOpeningBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblScreenName" runat="server" Text="Stock Opening Balance" Visible="false"></asp:Label>
    <%--    <asp:ScriptManager ID="tsm" runat="server"></asp:ScriptManager>--%>

    <script src="../js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
    <style type="text/css">
        .completionList {
            border: solid 1px Gray;
            margin: 0px;
            padding: 3px;
            list-style-type: none;
            border-radius: 4px;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #191919;
        }

        .itemHighlighted {
            background-color: #BDBABA;
        }
    </style>
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }



        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 128px;
                width: 128px;
            }
    </style>
    <script language="javascript">
        function calculatebuyingprice(aa) {
            if (aa.value == "0.00" || aa.value == "") {
                //alert("Please Enter Buying price");
                // aa.value="";
                //return false;
            }
            if (isNaN(parseFloat(aa.value))) {
                //alert("Please Enter any Decimal value for Buying price.");
                aa.value = "";
                return false;
            }
            var id = aa.id.split('_')[3].split("l")[1];
            var buyingprice = parseFloat(aa.value);
            var qty = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtQuantity").value;
            if (qty == "" || isNaN(qty) || qty == "0") {
                //alert("please enter quantity.");
                document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtQuantity").value = "";
                return false;
            }

            var sellingprice = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtSellingPrice").value;
            var tbuyingprice = buyingprice * qty;
            var tsellingprice = qty * parseFloat(sellingprice);
            if (buyingprice > parseFloat(sellingprice)) {
                alert("Buying price cannot be greater than selling price.");
                aa.value = "";
                return false;
            }
            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txttotalBuyingprice").value = tbuyingprice;
            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txttotalsellingprice").value = tsellingprice;
            calculateTotal();
        }
        function calculateTotal() {
            var grid = document.getElementById('<%=gvLineItems.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvLineItems.ClientID%>').rows.length;
            var totalsp = 0, totalbp = 0;
            for (var i = 2; i < gridrowcount; i++) {

                if (i < 10) {
                    var sp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtSellingPrice").value;
                    var bp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtBuyingPrice").value;
                    var qty = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtQuantity").value;
                    if (!isNaN(parseFloat(bp))) {
                        if (!isNaN(parseInt(qty)))
                            totalbp += parseFloat(bp) * parseInt(qty);
                    }
                    if (!isNaN(parseFloat(sp))) {
                        if (!isNaN(parseInt(qty)))
                            totalsp += parseFloat(sp) * parseInt(qty);
                    }
                }
                else {
                    var sp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtSellingPrice").value;
                    var bp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtBuyingPrice").value;
                    var qty = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtQuantity").value;
                    if (!isNaN(parseFloat(bp))) {
                        if (!isNaN(parseInt(qty)))
                            totalbp += parseFloat(bp) * parseInt(qty);
                    }
                    if (!isNaN(parseFloat(sp))) {
                        if (!isNaN(parseInt(qty)))
                            totalsp += parseFloat(sp) * parseInt(qty);
                    }
                }
            }
            document.getElementById("ctl00_ContentPlaceHolder1_txttotalbuyingprice").value = totalbp;
            document.getElementById("ctl00_ContentPlaceHolder1_txttotalsellingprice").value = totalsp;
        }
        function Calculateqty(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            if (aa.value == "0" || aa.value == "") {
                //alert("Please Enter Quantity");
                //aa.value = "";
                //return false;
            }
            if (isNaN(parseFloat(aa.value))) {
                //alert("Please Enter any numeric value for quantity.");
                aa.value = "";
                return false;
            }

            var buyingprice = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtBuyingPrice").value;
            if (buyingprice == "0.00" || buyingprice == "" || isNaN(parseFloat(buyingprice))) {
                //alert("Please Enter Buying price");
                //aa.value = "";
                return false;
            }

            var qty = parseFloat(aa.value);
            buyingprice = parseFloat(buyingprice);
            var totalbuyingprice = qty * buyingprice;
            var sellingprice = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtSellingPrice").value);
            var totalsellingprice = qty * sellingprice;
            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txttotalsellingprice").value = totalsellingprice;
            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txttotalBuyingprice").value = totalbuyingprice;
            calculateTotal();
        }
        function NullValidation() {
            var grid = document.getElementById('<%=gvLineItems.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvLineItems.ClientID%>').rows.length;
            var cat, brand, prod, bp, qty, hd, sp;
            for (var i = 2; i < gridrowcount; i++) {
                if (i < 10) {
                    hd = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_StockOpeningBalanceDetailId").value;
                    cat = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_ddlCategory").value;
                    brand = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtBrand").value;
                    prod = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtProduct").value;
                    if (cat != "0" && brand != "0" && prod != "0") {
                        bp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtBuyingPrice").value;
                        qty = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtQuantity").value;
                        sp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtSellingPrice").value;
                        if (bp == "0.00" || bp == "" || bp == "0" || bp == "0.0" || bp == "0.") {
                            // alert("please enter buying price for row " + (i - 1));
                            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtBuyingPrice").value = "";
                            // return false;
                        }
                        if (qty == "" || qty == "0") {
                            alert("please enter quantity for row " + (i - 1));
                            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtQuantity").value = "";
                            return false;
                        }
                        if (sp == "0.00" || sp == "" || sp == "0" || sp == "0.0" || sp == "0.") {
                            //alert("please enter Selling price for row " + (i - 1));
                            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtSellingPrice").value = "";
                            // return false;
                        }
                    }
                }
                else {
                    cat = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_ddlCategory").value;
                    brand = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtBrand").value;
                    prod = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtProduct").value;
                    if (cat != "0" && brand != "0" && prod != "0") {
                        bp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtBuyingPrice").value;
                        qty = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtQuantity").value;
                        sp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtSellingPrice").value;
                        if (bp == "0.00" || bp == "" || bp == "0" || bp == "0.0" || bp == "0.") {
                            // alert("please enter buying price for row " + (i - 1));
                            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtBuyingPrice").value = "";
                            // return false;
                        }
                        if (qty == "" || qty == "0") {
                            alert("please enter quantity for row " + (i - 1));
                            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtQuantity").value = "";
                            return false;
                        }
                        if (sp == "0.00" || sp == "" || sp == "0" || sp == "0.0" || sp == "0.") {
                            //alert("please enter Selling price for row " + (i - 1));
                            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtSellingPrice").value = "";
                            // return false;
                        }
                    }
                }
            }
        }
        function calculatetotalsellingprice(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            if (!isNaN(aa.value) && aa.value != "") {

                var qty = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtQuantity").value;
                if (qty == "" || parseInt(qty) == 0) {
                    aa.value = "";
                    document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txttotalsellingprice").value = ""
                }
                else {
                    document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txttotalsellingprice").value = parseInt(qty) * parseFloat(aa.value);
                }

            }
            else {
                aa.value = "";
                document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txttotalsellingprice").value = ""
            }
            calculateTotal();
        }
        function loader(aa) {
            // document.getElementById("ctl00_ContentPlaceHolder1_imgloader").style = "display:block;";
            var id = aa.id.split('_')[3].split("l")[1];
            $("#ctl00_ContentPlaceHolder1_ajaxloader").show();
            $("#dvimg").show();
            __doPostBack("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtProduct", "txtProduct_ontextchanged");
            return true;

        }
        function hideloader() {
            $("#ctl00_ContentPlaceHolder1_ajaxloader").hide();
            $("#dvimg").hide();
        }
    </script>
    <script type="text/javascript">
        function StoreForPrint(storedeliverynoteid) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{StoreReturnId:'" + storedeliverynoteid + "'}",
                dataType: "json",
                url: "StockOpeningBalance.aspx/StoreForPrint",
                success: function (data) {
                    if (data.d.length != 0) {

                        var Supplier = data.d.eStore;
                        var Supplierlist = data.d.eStoreList;

                        var TotalGrossValue = 0;
                        if (Supplierlist.length == 0) {
                            $("#divprintarea").hide();
                            return false;
                        }
                        else {
                            $("body").append('<div id="modalOverlay" class="modalOverlay">');
                            $("#divprintarea").show();

                            $("#lblprintStoreName").html(Supplier[0].StoreName);
                            $("#lblprintDeliveryNoteNo").html(Supplier[0].DeliveryNoteNo);
                            $("#lblprintVATID ").html(Supplier[0].Vatid);
                            $("#lblorganisationprintName").html(Supplier[0].OrganisationName);
                            $("#lblownerprintAddress").html(Supplier[0].Address);
                            $("#lblownerprintCity").html(Supplier[0].City);
                            $("#lblContNum").html(Supplier[0].ContactNum);
                            $("#lblEmail").html(Supplier[0].Email);
                            $("#lblDeliveryNoteDate").html(Supplier[0].DeliveryNoteDate);
                            $("#tblStorePrint > tbody").empty();
                            for (var i = 0; i < Supplierlist.length; i++) {
                                $("#tblStorePrint > tbody").append("<tr style='font-size:12px;font-family:verdana;'><td><lable style='color:black;'>" + Supplierlist[i].CategoryName + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].BrandName + "</label></td><td><lable style='color:black;'>" +
                        Supplierlist[i].ProductName + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].ProductValue + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].Quantity + "</label></td><td><lable style='color:black;'>" +
                        Supplierlist[i].GrossValue + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].BuyingPrice + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].TotalBuyingPrice + "</label></td> </tr>");

                            }

                            //var TotalProductValue = parseFloat(Supplier[0].NetProductValue);
                            TotalGrossValue = parseFloat(Supplier[0].TotalGrossValue);
                            var TotalPrice = parseFloat(Supplier[0].TotalValue);
                            var HandlingCharges = parseFloat(Supplier[0].HandlingCharges);
                            var Remarks = Supplier[0].Remarks
                            if (Remarks == "" || Remarks == null) {
                                Remarks = "";
                            }


                            $("#tblStorePrint > tfoot").append("<tr style='background-color:White;height:20px;font-size:12px;font-family:verdana;'>" +
                                       "<td colspan='8'><b></b></td>" +
                                      "</tr>");
                            $("#tblStorePrint > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                       "<td align='center' colspan='8'>Details:</td>" +
                                      "</tr>");
                            $("#tblStorePrint > tfoot").append("<tr style='background-color:White;font-size:12px;font-family:verdana;'>" +
                                                             "<td align='left' colspan='8' rowspan='1'>Remarks:</td>" +

                                                                  "</tr>");

                            $("#tblStorePrint > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                                             "<td align='left' colspan='6' rowspan='5'>" + Remarks + "</td>" +

                                                                  "</tr>");
                            $("#tblStorePrint > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                                          "<td align='right' colspan='1'>Total Buying Price:</td>" +
                                                            "<td align='center'>" + TotalPrice + "</td>" +
                                                          "</tr>");
                            $("#tblStorePrint > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                                           "<td align='right' colspan='1'>Total Selling Price:</td>" +
                                                             "<td align='center'>" + TotalGrossValue + "</td>" +
                                                           "</tr>");

                            //                            $("#tblStorePrint > tfoot").append("<tr>" +
                            //                                     "<td   align='right' colspan='2'><b>Total ProductValue</b></td>" +
                            //                                    "<td  align='center'>" + TotalProductValue + "</td>" +
                            //                                       "</tr>");
                            //                            $("#tblStorePrint > tfoot").append("<tr>" +
                            //                                                           "<td align='right' colspan='1'><b>Handling Charges:</b></td>" +
                            //                                                             "<td align='center'>" + HandlingCharges + "</td>" +
                            //                                                           "</tr>");
                            //                            $("#tblStorePrint > tfoot").append("<tr>" +
                            //                                                           "<td align='right' colspan='1'><b>Total Price:</b></td>" +
                            //                                                             "<td align='center'>" + TotalPrice + "</td>" +
                            //                                                           "</tr>");
                            $("table#tblStorePrint tr:even").css("background-color", "#F3F3F3");
                            $("table#tblStorePrint tr:odd").css("background-color", "#ffffff");
                        }
                    }
                    else {
                        $("#divprintarea").hide();
                        removestyle();
                    }
                },
                error: function (result)
                { }
            });
        }

        function printDiv() {
            $("#btnPrint").hide();
            $("#Button1").hide();
            $("#<%=Cancelbtn.ClientID%>").hide();
            var printarea = $("#divprintarea").html();
            var w = window.open();
            $("#tblSalesPrint th").css("background-color", "black");
            $("#tblSalesPrint th").css("color", "white");
            $("#tblSalesPrint tr:even").css("background-color", "White");
            $("#tblSalesPrint tr:odd").css("background-color", "#f2f2f2");

            w.document.writeln(printarea);
            w.print();
            $("#btnPrint").show();
            $("#Button1").show();
            $("#<%=Cancelbtn.ClientID%>").show();

        }
    </script>

    <form runat="server" id="form">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Stock Opening Balance</strong>
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

                            <div id="panelAddSOB" runat="server" class="page-content-wrap">
                                <center>
                                    <div id="dvimg" style="z-index: 9999; display: none;" class="modal">
                                        <asp:Image ID="ajaxloader" runat="server" ImageUrl="~/images/ajax-loader.gif" Style="display: none; width: 100px; height: 100px; margin-top: 250px;" />
                                    </div>
                                </center>


                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Organisation</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlOrganisation" runat="server"
                                                    class="form-control select"
                                                    Style="margin-bottom: 12px;">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvOrganisation" runat="server" ControlToValidate="ddlOrganisation"
                                                InitialValue="0" ValidationGroup="r" ErrorMessage="Please Select Organization"
                                                Text="*"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Store</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlStore" runat="server"
                                                    class="form-control select"
                                                    Style="margin-bottom: 12px;">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvSotre" runat="server" ControlToValidate="ddlStore"
                                                ErrorMessage="Please Select Store" ValidationGroup="r" Text="*" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                SOB No</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtSOBNo" runat="server" Enabled="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                SOB Date</label>
                                            <div class="col-md-8">
                                                <div class="input-group pull-right">
                                                    <asp:TextBox ID="txtSOBDate"
                                                        CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                </div>
                                                <label class="help-block">
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="form-group">
                                        <div class="col-md-2" id="dvsave" runat="server">
                                            <asp:Button ID="btnSaveDeliveryNote" runat="server" CssClass="btn btn-success btn-block pull-right"
                                                Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" OnClientClick="this.disabled = true;" 
                                             UseSubmitBehavior="false"
                                                OnClick="btnSaveDeliveryNote_Click" />
                                            <asp:HiddenField ID="HDStockOpeningBalanceId" runat="server" />
                                        </div>
                                        <div class="col-md-2" id="dvcancel" runat="server">
                                            <asp:Button ID="btncancel1" runat="server" CssClass="btn btn-danger btn-block pull-right"
                                                Text="Cancel" Style="margin-bottom: 5px;" OnClick="btncancel1_Click" />
                                        </div>
                                    </div>
                                </div>

                                <br />
                                <div id="divAddSOBLineItems" runat="server">
                                    <div id="divADDSOBDetails" runat="server" class="table-responsive">
                                        <asp:GridView ID="gvLineItems" runat="server" AutoGenerateColumns="false"
                                            DataKeyNames="StockOpeningBalanceDetailId" CssClass="table table-bordered table-striped table-actions"
                                            OnRowDataBound="gvLineItems_RowDataBound" OnRowCommand="gvLineItems_RowCommand"
                                            OnRowEditing="gvLineItems_RowEditing">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Category" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control select"
                                                            OnSelectedIndexChanged="ddlCategory_selectedindexchanged" AutoPostBack="true"
                                                            TabIndex="-1">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="StockOpeningBalanceDetailId" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Brand" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control select"
                                                            OnSelectedIndexChanged="ddlBrand_selectedindexchanged" AutoPostBack="True" Visible="false">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control " AutoComplete="on"
                                                            TabIndex="-1"></asp:TextBox>
                                                        <asp:HiddenField ID="HDBrandID" runat="server" Value='<%#Eval("BrandID") %>' />
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="AutoCompleteAjaxRequest"
                                                            ServicePath="~/Screens/AutoComplete.asmx" MinimumPrefixLength="1" CompletionInterval="100"
                                                            EnableCaching="false" CompletionSetCount="10" TargetControlID="txtBrand" FirstRowSelected="false"
                                                            CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </asp:AutoCompleteExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Model No">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" CssClass="form-control select"
                                                            OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" Visible="false">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control " AutoComplete="on"
                                                            AutoPostBack="true" OnTextChanged="txtProduct_ontextchanged" onchange="return loader(this);"></asp:TextBox>
                                                        <asp:HiddenField ID="HDProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="AutoCompleteProductRequest"
                                                            ServicePath="~/Screens/AutoComplete.asmx" MinimumPrefixLength="1" CompletionInterval="100"
                                                            EnableCaching="false" CompletionSetCount="10" TargetControlID="txtProduct" FirstRowSelected="false"
                                                            UseContextKey="true" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </asp:AutoCompleteExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Buying Price">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBuyingPrice" runat="server" CssClass="form-control "
                                                            Text='<%#Eval("BuyingPrice") %>' onkeyup="return calculatebuyingprice(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control "
                                                            Text='<%#Eval("Quantity") %>' onkeyup="return Calculateqty(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Tot. Buying price">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txttotalBuyingprice" runat="server" CssClass="form-control "
                                                            Text='<%#Eval("NetBuyingPrice") %>' Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Selling Price">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSellingPrice" runat="server" CssClass="form-control " Text='<%#Eval("ProductValue") %>'
                                                            onkeyup="return calculatetotalsellingprice(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Tot. Selling Price">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txttotalsellingprice" runat="server" CssClass="form-control " Text='<%#Eval("NetProductValue") %>'
                                                            Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <%--  <asp:ImageButton ID="imgDeleteRow" ToolTip="Delete Row" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="DeleteRow" OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                                                runat="server" ImageUrl="../Images/cross.png" Style="width: 20px; height: 20px"
                                                                TabIndex="-1" />--%>
                                                        <asp:LinkButton ID="imgDeleteRow" runat="server" CommandName="DeleteRow"
                                                            OnClientClick="return confirm('Do you want to Delete the record?');" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'><span class="fad fa-times"> </span></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>


                                    <div id="tbl" runat="server" align="left">
                                        <div class="row top">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Total Buying Price:</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox CssClass="form-control" ID="txttotalbuyingprice" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Total Selling Price:</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox CssClass="form-control" ID="txttotalsellingprice" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row top">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Remarks:</label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox CssClass="form-control" ID="txtRemarks" runat="server" TextMode="MultiLine" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <div id="Div1" runat="server">
                                            <div class="form-group">
                                                <div class="col-md-2" id="dvClear" runat="server" style="display: none">
                                                    <asp:Button ID="imgClear" runat="server" Visible="false" CssClass="btn btn-warning btn-block pull-left"
                                                        alt="Clear" Text="Clear" Style="margin-bottom: 5px;" />
                                                </div>
                                                <div class="col-md-2" id="dvisave" runat="server">
                                                    <asp:Button ID="imgSave" ValidationGroup="r" runat="server" CssClass="btn btn-success btn-block pull-right"
                                                        Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" OnClientClick="return NullValidation();this.disabled = true;"
                                                        OnClick="imgSave_Click" />
                                                </div>
                                                <div class="col-md-2" id="dvSaveDisabled" runat="server" visible="false">
                                                    <asp:Button ID="imgSaveDisabled" runat="server" CssClass="btn btn-success btn-block pull-left"
                                                        Text="Cancel" Style="display: none;" />
                                                </div>
                                                <div class="col-md-2" id="dvupdate" runat="server" visible="false">
                                                    <asp:Button ID="imgupdate" ValidationGroup="r" Visible="false" runat="server"
                                                        CssClass="btn btn-success btn-block pull-left"
                                                        Text="Update" CausesValidation="true"
                                                        Style="margin-bottom: 5px;" />
                                                </div>
                                                <div class="col-md-2" id="Divcan1" runat="server">
                                                    <asp:Button ID="btncancelgrid" Visible="false" runat="server"
                                                        CssClass="btn btn-danger btn-block pull-left"
                                                        Text="Cancel" OnClick="btncancelgrid_Click"
                                                        Style="margin-bottom: 5px;" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <center>
                                    <div id="dvSummary" align="center">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="r"
                                            CssClass="validationSummary" />
                                    </div>
                                </center>
                            </div>

                            <div id="panelSearchSOB" runat="server">
                                <div class="panel-body">
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    Organisation</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlOrganisationSearch"
                                                        runat="server" class="form-control select">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    Store</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlSearchStore"
                                                        runat="server" class="form-control select">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    SOB No</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtSOBNOSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    From Date</label>
                                                <div class="col-md-8">
                                                    <div class="input-group pull-right">
                                                        <asp:TextBox ID="txtFromDateSearch"
                                                            CssClass="form-control datepicker" runat="server"></asp:TextBox><%--onchange="CompareDate()"--%>
                                                        <span class="input-group-addon"><span
                                                            class="fa fa-calendar"></span></span>
                                                    </div>
                                                    <label class="help-block">
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    To Date</label>
                                                <div class="col-md-8">
                                                    <div class="input-group pull-right">
                                                        <asp:TextBox ID="txtToDateSearch" CssClass="form-control datepicker"
                                                            runat="server"></asp:TextBox>
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
                                                <label class="col-md-4 control-label">
                                                </label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <asp:Button ID="searchDeliveryNote" runat="server" Text="Search"
                                                            CssClass="btn btn-info" OnClick="searchDeliveryNote_Click" />&nbsp;&nbsp;
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
                                    <div id="gridClass" class="table-responsive">
                                        <asp:GridView ID="grdStoreDeliveryNote" runat="server" DataKeyNames="StockOpeningBalanceId"
                                            AutoGenerateColumns="false"
                                            OnPageIndexChanging="grdStoreDeliveryNote_PageIndexChanging" OnRowCommand="grdStoreDeliveryNote_RowCommand"
                                            OnRowCreated="grdStoreDeliveryNote_RowCreated" OnRowDataBound="grdStoreDeliveryNote_RowDataBound"
                                            OnRowDeleting="grdStoreDeliveryNote_RowDeleting" OnRowEditing="grdStoreDeliveryNote_RowEditing"
                                            CssClass="table datatable  table-bordered table-striped table-actions">
                                            <Columns>
                                                <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                    DataField="StoreName" HeaderText="StoreName"></asp:BoundField>
                                                <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                    DataField="StockOpeningBalanceNo" HeaderText="Stock OpeningBalance No."></asp:BoundField>
                                                <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                    DataField="StockOpeningBalanceDate" HeaderText="Stock OpeningBalance Date"></asp:BoundField>
                                                <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                    DataField="TotalBuyingPrice" HeaderText="Total Value"></asp:BoundField>
                                                <asp:TemplateField ItemStyle-Width="90">
                                                    <ItemTemplate>
                                                        <%-- <asp:ImageButton ID="lnkbtnView" runat="server" ImageUrl="~/images/hammer_screwdriver.png"
                                                        Text="View" ToolTip="View" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                        CommandName="View" ValidationGroup="False" />
                                                    <asp:ImageButton ID="lnkbtnEdit" runat="server" ImageUrl="~/images/pencil.png" ToolTip="Edit"
                                                        CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="EditRow"
                                                        Text="Edit" ValidationGroup="False" />
                                                    <asp:ImageButton ID="lnkbtnDel" runat="server" ImageUrl="~/images/cross.png" Text="Delete"
                                                        ToolTip="Delete" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="Deleting"
                                                        ValidationGroup="False" OnClientClick="return confirm('Do you want to Delete the record?');" />
                                                    <asp:ImageButton ID="lnkbtnPrint" runat="server" ImageUrl="~/images/print.jpg" Width="20px"
                                                        Height="20px" ToolTip="Print" Text="Print" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                        CommandName="Print" OnClick="lnkbtnPrint_Click" ValidationGroup="False" />--%>
                                                        <asp:LinkButton ID="lnkbtnView" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                            CommandName="View"
                                                            Style="text-decoration: none;" CssClass="fav fa-eye" ToolTip="View"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditRow" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                            Style="text-decoration: none;" CssClass="fae fa-pencil" ToolTip="Edit">
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnDel" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
                                                            OnClientClick="return confirm('Do you want to Delete the record?');"
                                                            CommandName="Deleting" ValidationGroup="False" runat="server" ToolTip="Delete"
                                                            Style="text-decoration: none;"> 
                                                            <span class="fad fa-times"> </span></asp:LinkButton>
                                                        <asp:ImageButton ID="lnkbtnPrint" runat="server" ImageUrl="~/images/print.jpg" Text="Print"
                                                            ToolTip="Print" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="Print"
                                                            Style="width: 20px; height: 20px;" OnClick="lnkbtnPrint_Click" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                            <%-- <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                        <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#E5E5E5" Height="25px" HorizontalAlign="Center" />--%>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </form>
                </div>
            </div>
            <div id="divprintarea1" class="modal" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-position">
                    <div class="modal-content" id="printdiv">
                        <div class="modal-header">
                            <center>
                                <h2 class="modal-title">Stock Opening Balance
                                </h2>
                            </center>

                            <br />
                            <br />
                            <%--<table width="800px" align="center" id="Table1">
                                <tr>
                                    <td style="width: 30px"></td>
                                    <td align="left">
                                        <label id="lblprintStoreName" class="Label">
                                        </label>
                                        <br />
                                        <label id="Label17" class="Label">
                                            SOB No.:</label>
                                        <label id="lblprintDeliveryNoteNo" class="Label">
                                        </label>
                                        <br />
                                        <label id="lblDelNoteDate" class="Label">
                                            SOB Date:</label>
                                        <label id="lblDeliveryNoteDate" class="Label">
                                        </label>
                                        <br />
                                        <label id="lblprintStoreAddress" class="Label">
                                        </label>
                                        <br />
                                        <label id="lblprintStorePincode" class="Label">
                                        </label>

                                        <label id="lblprintCity" class="Label">
                                        </label>
                                        <br />
                                        <br />
                                        <br />

                                    </td>
                                    <td width="300px"></td>
                                    <td align="left">
                                        <label id="lblorganisationprintName" class="Label">
                                        </label>
                                        <br />
                                        <label id="lblownerprintAddress" class="Label">
                                        </label>
                                        <br />
                                        <label id="label20" class="Label">
                                        </label>
                                        <label id="lblownerprintCity" class="Label">
                                        </label>
                                        <br />
                                        <label id="lblCountry" class="Label">
                                            K.S.A</label>
                                        <br />
                                        <label id="lblContNum" class="Label">
                                            Cont Num:
                                        </label>
                                        <label id="Label24" class="Label">
                                        </label>
                                        <br />

                                        <label id="lblEmail" class="Label">
                                            Email:</label><label id="Label28" class="Label"></label>
                                        <br />

                                    </td>
                                </tr>
                            </table>--%>
                            <br />
                        </div>
                        <div class="modal-body">
                            <div id="printSupplierDeliveryNote" class="gridClass1" style="display: block;">
                                <table width="750px" align="center" id="tblStorePrint1" border="1" style="border-spacing: 0px;">
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
                                            <th style="color: white; background-color: black;">Tot Selling Price
                                            </th>
                                            <th style="color: white; background-color: black;">Buying Price
                                            </th>
                                            <th style="color: white; background-color: black;">Tot Buying Price
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
                        <br />
                        <div class="modal-footer">
                            <input type="button" id="Button1" value="Print" class="BtnEmptyStyle" title="Print"
                                onclick="printDiv()" />
                            <asp:Button ID="Cancelbtn1" runat="server" Text="Cancel" CssClass="BtnEmptyStyle"
                                OnClientClick="return removestyle();" />
                        </div>
                    </div>
                </div>
            </div>
            <form name="SubOrgForm" novalidate>
                <div class="modal" id="divprintarea" tabindex="-1" role="dialog" aria-hidden="true" style="height: 100%; overflow-y: auto;">
                    <div class="modal-dialog modal-lg modal-position" id="Div3">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" style="display:none;"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                <center><h4 class="modal-title">Stock opening balance</h4></center>
                            </div>
                       
                         <div class="modal-body">
                             <div class="row">
                                    <div class="col-md-6" style="width:35%;float:left;margin-left:15%;">
                                        <div class="row">
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="input-group">
                                                            <label id="lblprintStoreName" class="Label"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label id="Label1" class="Label pull-left"> SOB No.:</label>
                                                    <div class="row">
                                                        <div class="input-group">
                                                            <label id="lblprintDeliveryNoteNo" class="lblprintDeliveryNoteNo"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label id="Label3" class="Label pull-left">SOB Date:</label>
                                                    <div class="row">
                                                        <div class="input-group">
                                                            <label id="lblDeliveryNoteDate" class="lblDeliveryNoteDate"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="row">
                                                <div class="form-group">

                                                    <div class="row">
                                                        <div class="input-group">
                                                            <label id="lblprintStoreAddress" class="Label"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="row">
                                                <div class="form-group">

                                                    <div class="row">
                                                        <div class="input-group">
                                                            <label id="lblprintStorePincode" class="Label"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="row">
                                                <div class="form-group">

                                                    <div class="row">
                                                        <div class="input-group">
                                                            <label id="lblprintCity" class="Label"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6"  style="width:46%;float:right;">
                                        <div class="row " style="margin-top: 1%">

                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <div class="input-group">
                                                            <label id="lblorganisationprintName" class="Label"></label>
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
                                                            <label id="lblownerprintAddress" class="Label"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 1%">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <div class="input-group ">
                                                            <label id="lblownerprintCity" class="Label"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 1%">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <div class="input-group ">
                                                            <label id="lblCountry" class="Label">K.S.A</label>
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
                                                            <label id="lblContNum" class="Label">Cont Num: </label>
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
                                                            <label id="lblEmail" class="Label">Email:</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        <div class="dataTables_wrapper no-footer" style="margin-top: 10px;">
                            <div class="panel-body panel-body-table" style="border: hidden !important;">
                                <div class="table-bordered">
                                    <div class="table-responsive">
                                          <table width="750px" align="center" id="tblStorePrint" border="1" class="table table-bordered table-striped table-actions">
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
                                                        <th style="color: white; background-color: black;">Tot Selling Price
                                                        </th>
                                                        <th style="color: white; background-color: black;">Buying Price
                                                        </th>
                                                        <th style="color: white; background-color: black;">Tot Buying Price
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
                        <div class="modal-footer">
                            <input type="button" id="btnprint" value="Print" class="BtnEmptyStyle" title="Print"
                                onclick="printDiv()" />
                            <asp:Button ID="Cancelbtn" runat="server" Text="Cancel" CssClass="BtnEmptyStyle"
                                OnClientClick="return removestyle();" />
                        </div>
                             </div>
                    </div>
                </div>
            </form>
        </div>
    </form>



</asp:Content>

