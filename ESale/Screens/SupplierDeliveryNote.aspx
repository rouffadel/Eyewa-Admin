<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="SupplierDeliveryNote.aspx.cs" Inherits="Screens_SupplierDeliveryNote" EnableEventValidation="false"
    Title="Purchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js">
    </script>

    <link href="modalPopLite1.3.1/modalPopLite.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="modalPopLite1.3.1/modalPopLite.min.js"></script>--%>

  

    <%--<asp:ToolkitScriptManager ID="tsm" runat="server">
    </asp:ToolkitScriptManager>--%>
    <style type="text/css">
        /*.p1, .p2, .p3 {
          width: 100px;
          height: 100px;
          border: 1px solid red;
          margin-right: 10px;
          float: left;
        }
        .c1 {
          float: left;
          margin-right: 5px;
        }*/
        #sides {
            margin: 0;
        }

        #left {
            float: left;
            width: 400px;
            overflow: auto;
        }

        #right {
            float: left;
            width: 500px;
            overflow: auto;
            margin-right: 70px;
            margin-left: 10px;
        }

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
        .Popupclass {
            background-color: Black;
        }
    </style>
    <style type="text/css">
        .modalOverlay {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0px;
            left: 0px;
            background-color: rgba(0,0,0,0.3); /* black semi-transparent */
        }
    </style>
    <style>
        .modalOverlay {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0px;
            left: 0px;
            background-color: rgba(0,0,0,0.3); /* black semi-transparent */
        }

        .style2 {
            width: 334px;
        }

        .style3 {
            width: 193px;
        }

        .style4 {
            width: 189px;
        }

        #tbl {
            width: 965px;
        }
    </style>

      <script src="../js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
    <%-- <script type="text/javascript">
        function Brandapplystyle() {

            $("body").append('<div id="modalOverlay" class="modalOverlay">');
        }
        function Productapplystyle(aa) {


            $("body").append('<div id="modalOverlay" class="modalOverlay">');
            //return false;
        }
        function removestyle() {
            $("#modalOverlay").remove();
            $("#<%=dvBrandPopUp.ClientID%>").hide();
            $("#<%=dvProductPopUp.ClientID%>").hide();
            return false;
        }
        function showbrandpopup() {
            $("#<%=dvBrandPopUp.ClientID%>").show();

        }
    </script>--%>
    <script type="text/javascript">
        function SalesForPrint(supplierdeliverynoteid) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{supplierdeliverynoteid:'" + supplierdeliverynoteid + "'}",
                dataType: "json",
                url: "SupplierDeliveryNote.aspx/SupplierForPrint",
                success: function (data) {
                    if (data.d.length != 0) {

                        var Supplier = data.d.eSupplier;
                        var Supplierlist = data.d.eSupplierlist;
                        var SupplierlistCalc = data.d.eSupplierlistCals;
                        var TotalGrossValue = 0;
                        if (data.d.eSupplierlist.length == 0) {
                            $("#divprintarea").hide();
                            return false;
                        }
                        else {
                            $("body").append('<div id="modalOverlay" class="modalOverlay">');
                            $("#divprintarea").show();
                            //$('#popup-wrapper').modalPopLite({ printButton: '', cancelButton: '' }); 
                            //$("body").css("background-color", "black");
                            $("#lblprintSupplierName").html(Supplier[0].SupplierName);
                            $("#lblprintDeliveryNoteNo").html(Supplier[0].SupplierDeliveryNoteNo);
                            $("#lblprintVATID ").html(Supplier[0].Vatid);
                            $("#lblorganisationprintName").html(Supplier[0].OrganisationName);
                            $("#lblownerprintAddress").html(Supplier[0].Address);
                            $("#lblownerprintCity").html(Supplier[0].City);
                            $("#lblContNum").html(Supplier[0].ContactNumber);
                            $("#lblEmail").html(Supplier[0].Email);
                            $("#lblDeliveryNoteDate").html(Supplier[0].SupplierDeliveryNoteDate);
                            $("#tblSalesPrint > tbody").empty();
                            for (var i = 0; i < Supplierlist.length; i++) {
                                $("#tblSalesPrint > tbody").append("<tr style='font-size:12px;font-family:verdana;'><td><lable style='color:black;'>" + Supplierlist[i].CategoryName + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].BrandName + "</label></td><td><lable style='color:black;'>" +
                        Supplierlist[i].ProductName + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].BuyingPriceperPice + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].Quantity + "</label></td><td><lable style='color:black;'>" +
                        Supplierlist[i].NetBuyingPrice + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].ProductValue + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].GrossValue + "</label></td></tr>");
                                TotalGrossValue += parseFloat(Supplierlist[i].GrossValue);
                            }

                            var TotalProductValue = parseFloat(Supplier[0].NetProductValue);
                            var TotalPrice = parseFloat(Supplier[0].TotalValue);
                            var HandlingCharges = parseFloat(Supplier[0].HandlingCharges);
                            var Remarks = Supplier[0].Remarks
                            if (Remarks == "" || Remarks == null) {
                                Remarks = "";
                            }


                            $("#tblSalesPrint > tfoot").append("<tr style='background-color:White;height:20pxfont-size:12px;font-family:verdana;'>" +
                                       "<td colspan='9'><b></b></td>" +
                                      "</tr>");
                            $("#tblSalesPrint > tfoot").append("<tr style='font-size:12;font-family:verdana;'>" +
                                       "<td align='center' colspan='9' >Details:</td>" +
                                      "</tr>");
                            $("#tblSalesPrint > tfoot").append("<tr style='background-color:White;font-size:12px;font-family:verdana;'>" +
                                                             "<td align='left' colspan='8' rowspan='1'>Remarks:</td>" +

                                                                  "</tr>");

                            $("#tblSalesPrint > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                                             "<td align='left' colspan='5' rowspan='5' >" + Remarks + "</td>" +

                                                                  "</tr>");
                            $("#tblSalesPrint > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                     "<td   align='right' colspan='2'>Total Buying Price:</td>" +
                                    "<td  align='center'>" + TotalProductValue + "</td>" +
                                       "</tr>");
                            $("#tblSalesPrint > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                                           "<td align='right' colspan='2'>Total Selling Price:</td>" +
                                                             "<td align='center'>" + TotalGrossValue + "</td>" +
                                                           "</tr>");


                            $("#tblSalesPrint > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                                           "<td align='right' colspan='2'>Handling Charges:</td>" +
                                                             "<td align='center'>" + HandlingCharges + "</td>" +
                                                           "</tr>");
                            $("#tblSalesPrint > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                                           "<td align='right' colspan='2'>Total Price:</td>" +
                                                             "<td align='center' >" + TotalPrice + "</td>" +
                                                           "</tr>");

                            $("table#tblSalesPrint tr:even").css("background-color", "#F3F3F3");
                            $("table#tblSalesPrint tr:odd").css("background-color", "#ffffff");
                        }
                    }
                    else {
                        $("#divprintarea").hide();
                    }
                },
                error: function (result)
                { }
            });
        }
        function printdivhide() {
            $("#divprintarea").hide();
            removestyle();
            return false;
        }
        function printDiv() {
            $("#btnPrint").hide();
            $("#<%=btnCancel.ClientID%>").hide();
            var printarea = $("#divprintarea").html();
            var w = window.open();
            $("#tblSalesPrint th").css("background-color", "black");
            $("#tblSalesPrint th").css("color", "white");
            $("#tblSalesPrint tr:even").css("background-color", "White");
            $("#tblSalesPrint tr:odd").css("background-color", "#f2f2f2");
            //$("#tblSalesPrint").css("font-weight", "bold");
            w.document.writeln(printarea);
            w.print();
            $("#btnPrint").show();
            $("#<%=btnCancel.ClientID%>").show();

        }
    </script>

    <%-- <script language="javascript" src="../js/jquery.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        function ValidateFields() {
            if ($("#<%=ddlOrganisation.ClientID%>").val() == "0") {
                alert("Please Select Organisation.");
                return false;
            }
            if ($("#<%=ddlSupplier.ClientID%>").val() == "0") {
                alert("Please Select Supplier.");
                return false;
            }
            return true;
        }
        function Calculate1(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            var quantity = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtQuantity").value);
            if (!isNaN(aa.value) && aa.value != "") {
                var sp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtProductValue").value);
                if (!isNaN(quantity)) {
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtGrossValue").value = (sp * quantity);
                }
                else {
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtGrossValue").value = "0.00";
                }
            }
            else {
                aa.value = "";
                document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtGrossValue").value = "0.00";
            }
            calci();
            return false;
        }
        function Calculate(aa) {
            debugger;
            if (aa.value != "" && (!isNaN(aa.value))) {
                var id = aa.id.split('_')[3].split("l")[1];
                var quantity = parseInt(aa.value);
                //var p = aa.parentElement.parentElement.children[6].children[0].id
                //var productvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtProductValue").value);
                var productvalue = parseFloat(document.getElementById(aa.parentElement.parentElement.children[6].children[0].id).value);

                if (isNaN(productvalue))
                    productvalue = 0
                //var buyingprice = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtBuyingPrice").value);
                var buyingprice = parseFloat(document.getElementById(aa.parentElement.parentElement.children[3].children[0].id).value);

                var grossproductvalue, totalbuyingprice;
                if (!isNaN(quantity)) {
                    if (!isNaN(buyingprice)) {
                        grossproductvalue = (productvalue * quantity);
                        totalbuyingprice = (buyingprice * quantity);
                    }
                    else {
                        grossproductvalue = "0.00";
                        totalbuyingprice = "0";
                    }
                }
                else {
                    grossproductvalue = "0.00";
                    totalbuyingprice = "0";
                }
                //document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtGrossValue").value = grossproductvalue;
                //document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtNetBuyingPrice").value = totalbuyingprice;
                document.getElementById(aa.parentElement.parentElement.children[7].children[0].id).value = grossproductvalue;
                document.getElementById(aa.parentElement.parentElement.children[5].children[0].id).value = totalbuyingprice;
                calci();
            }
            else {
                // alert("Please enter Numeric value");
                var id = aa.id.split('_')[3].split("l")[1];
                //document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtGrossValue").value = "0.00";
                //document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtNetBuyingPrice").value = "0.00"
                document.getElementById(aa.parentElement.parentElement.children[7].children[0].id).value = "0.00";
                document.getElementById(aa.parentElement.parentElement.children[5].children[0].id).value = "0.00"
                aa.value = "";
                calci();
                //aa.value = "";
                return false;
            }

            return false;
        }
        function calc(aa) {
            if (aa.value != "" && (!isNaN(aa.value))) {
                var id = aa.id.split('_')[3].split("l")[1];
                var buyingprice = parseFloat(aa.value);
                var quantity = document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtQuantity").value;
                //                if (quantity == "0" ) {
                //                    alert("Please enter Positive Integer value for quantity.");
                //                    aa.value = "0.00";
                //                    return false;
                //                }
                //                var bp = parseFloat(aa.value);
                //                var pv = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtProductValue").value);
                //                if (bp > pv) {
                //                    alert("Buying Price cannot be Greater than Selling Price.");
                //                    aa.value = "";
                //                    return false;
                //                }
                quantity = parseInt(quantity);
                if (!isNaN(quantity)) {
                    var netbuyingprice = parseFloat(quantity * buyingprice);
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtNetBuyingPrice").value = netbuyingprice
                }
                else {
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtNetBuyingPrice") = "0";
                }
            }
            else {
                //alert("Please enter Buying Price per Piece.");
                aa.value = "";
                var id = aa.id.split('_')[3].split("l")[1];
                document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtNetBuyingPrice").value = 0.00
                calci();
                return false;
            }
            calci();
            return true;
        }
        <%--function calci() {
            debugger;
            var grid = document.getElementById('<%=gvSDND.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvSDND.ClientID%>').rows.length;
            var totalgrossvalue = 0, totalnetbuyingprice = 0, totalprice = 0;
            for (var i = 2; i <= gridrowcount; i++) {
                if (i < 10) {
                    quat = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl0" + i + "_txtQuantity").value);
                    totalprice = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl0" + i + "_txtBuyingPrice").value);
                    if (!isNaN(quat) && quat != 0 && totalprice != 0 && !isNaN(totalprice)) {
                        totalgrossvalue = totalgrossvalue + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl0" + i + "_txtGrossValue").value);
                        totalnetbuyingprice = totalnetbuyingprice + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl0" + i + "_txtNetBuyingPrice").value);
                    }
                }
                else {
                    quat = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + i + "_txtQuantity").value);
                    totalprice = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + i + "_txtBuyingPrice").value);
                    if (!isNaN(quat) && quat != 0 && totalprice != 0 && !isNaN(totalprice)) {
                        totalgrossvalue = totalgrossvalue + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + i + "_txtGrossValue").value);
                        totalnetbuyingprice = totalnetbuyingprice + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + i + "_txtNetBuyingPrice").value);
                    }
                }
            }
            var handlingcharges = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtHandlingCharges").value);
            document.getElementById("ctl00_ContentPlaceHolder1_txttotalgrossvalue").value = totalgrossvalue.toFixed(2);
            document.getElementById("ctl00_ContentPlaceHolder1_txtTotalPrice").value = totalnetbuyingprice.toFixed(2);
            var valu = 0;
            if (!isNaN(handlingcharges))
                valu = parseFloat(totalnetbuyingprice.toFixed(2)) + parseFloat(handlingcharges.toFixed(2));
            if (!isNaN(handlingcharges))
                document.getElementById("ctl00_ContentPlaceHolder1_txtTotalProductValue").value = valu.toFixed(2);
            else
                document.getElementById("ctl00_ContentPlaceHolder1_txtTotalProductValue").value = totalnetbuyingprice.toFixed(2);
        }--%>
        function calci() {
            debugger;
            var grid = document.getElementById('<%=gvSDND.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvSDND.ClientID%>').rows.length;
            var totalgrossvalue = 0, totalnetbuyingprice = 0, totalprice = 0;
            for (var i = 0; i <= gridrowcount-2; i++) {
                //if (i < 10) {
                    quat = parseFloat(document.getElementById("ContentPlaceHolder1_gvSDND_txtQuantity_"+ i).value);
                    totalprice = parseFloat(document.getElementById("ContentPlaceHolder1_gvSDND_txtBuyingPrice_" + i).value);
                    if (!isNaN(quat) && quat != 0 && totalprice != 0 && !isNaN(totalprice)) {
                        totalgrossvalue = totalgrossvalue + parseFloat(document.getElementById("ContentPlaceHolder1_gvSDND_txtGrossValue_" + i).value);
                        totalnetbuyingprice = totalnetbuyingprice + parseFloat(document.getElementById("ContentPlaceHolder1_gvSDND_txtNetBuyingPrice_" + i).value);
                    }
                //}
                else {
                    quat = parseFloat(document.getElementById("ContentPlaceHolder1_gvSDND_txtQuantity_" + i).value);
                    totalprice = parseFloat(document.getElementById("ContentPlaceHolder1_gvSDND_txtBuyingPrice_" + i).value);
                    if (!isNaN(quat) && quat != 0 && totalprice != 0 && !isNaN(totalprice)) {
                        totalgrossvalue = totalgrossvalue + parseFloat(document.getElementById("ContentPlaceHolder1_gvSDND_txtGrossValue_" + i).value);
                        totalnetbuyingprice = totalnetbuyingprice + parseFloat(document.getElementById("ContentPlaceHolder1_gvSDND_txtNetBuyingPrice_" + i).value);
                    }
                }
            }
            var handlingcharges = parseFloat(document.getElementById("ContentPlaceHolder1_txtHandlingCharges").value);
            document.getElementById("ContentPlaceHolder1_txttotalgrossvalue").value = totalgrossvalue.toFixed(2);
            document.getElementById("ContentPlaceHolder1_txtTotalPrice").value = totalnetbuyingprice.toFixed(2);
            var valu = 0;
            if (!isNaN(handlingcharges))
                valu = parseFloat(totalnetbuyingprice.toFixed(2)) + parseFloat(handlingcharges.toFixed(2));
            if (!isNaN(handlingcharges))
                document.getElementById("ContentPlaceHolder1_txtTotalProductValue").value = valu.toFixed(2);
            else
                document.getElementById("ContentPlaceHolder1_txtTotalProductValue").value = totalnetbuyingprice.toFixed(2);
         }
        //This is for Multiplication
        function mult() {

            var HandlinCharges = document.getElementById('<%=txtHandlingCharges.ClientID %>').value;
            var TotalProductValue = document.getElementById('<%=txtTotalPrice.ClientID %>').value;
            if (!isNaN(HandlinCharges) == true) {
                if (!(isNaN(HandlinCharges) || isNaN(TotalProductValue))) {
                    var aa = Math.round(TotalProductValue * 1000) / 1000 + Math.round(HandlinCharges * 1000) / 1000;
                    // var TotalValue = (parseFloat(HandlinCharges) + parseFloat(TotalProductValue));
                    document.getElementById('<%=txtTotalProductValue.ClientID %>').value = aa.toFixed(2);
                }
                else {
                    document.getElementById('<%=txtTotalProductValue.ClientID %>').value = TotalProductValue.toFixed(2);
                }
            }
            else {
                alert("Enter numeric values only");
                document.getElementById('<%=txtHandlingCharges.ClientID %>').value = '0.00';
                return false;
            }
        }
        function CheckFields(aa) {
            var id = aa.id.split('_')[3].split("l")[1];

            var category = document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_ddlCategory");
            if (category.selectedIndex == 0) {
                alert("Please Select Category.");
                aa.selectedIndex = 0;
                return false;
            }
            return true;
        }

        function NullValidation() {

            var GridData = document.getElementById("<%=gvSDND.ClientID %>");
            var GridRowsCount = document.getElementById("<%=gvSDND.ClientID %>").rows.length;
            var quntity;
            var SellingPrice, prod, cat;
            for (var i = 2; i <= GridRowsCount; i++) {
                if (i < 10) {
                    cat = document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl0" + i + "_ddlCategory").value;
                    prod = document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl0" + i + "_txtProduct").value;
                    quntity = document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl0" + i + "_txtQuantity").value;
                    SellingPrice = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl0" + i + "_txtBuyingPrice").value);
                    var productvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl0" + i + "_txtProductValue").value);

                    if (prod != "") {
                        if (productvalue == 0 || (isNaN(productvalue)) || productvalue == "") {
                            //alert("Please Enter Selling Price.");
                            //return false;
                        }
                        if (isNaN(SellingPrice)) {
                            //alert("Buying price cannot be Empty.");
                            //return false;
                        }
                        if (quntity != "0" && SellingPrice == 0) {
                            //alert('Buying Price  Cannot be Empty');
                            //return false;
                        }
                        if ((quntity == "0" || quntity == "") && SellingPrice != 0) {
                            alert("Quantity cannot be 0 or Empty.");
                            return false;
                        }
                        if (quntity == "0" && SellingPrice == 0) {
                            alert("Quantity cannot be 0.");
                            return false;
                        }
                        var bp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl0" + i + "_txtBuyingPrice").value);
                        var pv = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl0" + i + "_txtProductValue").value);
                        if (bp > pv) {
                            alert("Buying Price cannot be Greater than Selling Price.");
                            return false;
                        }
                    }
                }
                else {
                    prod = document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + i + "_txtProduct").value;
                    quntity = document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + i + "_txtQuantity").value;
                    SellingPrice = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + i + "_txtBuyingPrice").value);
                    var productvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + i + "_txtProductValue").value);
                    if (prod != "") {
                        if (productvalue == 0 || (isNaN(productvalue)) || productvalue == "") {
                            //alert("Please Enter Selling Price.");
                            // return false;
                        }
                        if (quntity != "0" && SellingPrice == 0) {
                            // alert('Buying Price  Cannot be Empty');
                            // return false;
                        }
                        if (quntity == "0" && SellingPrice != 0) {
                            alert("Quantity cannot be 0.");
                            return false;
                        }
                        if (quntity == "0" && SellingPrice == 0) {
                            alert("Quantity cannot be 0.");
                            return false;
                        }
                        var bp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + i + "_txtBuyingPrice").value);
                        var pv = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + i + "_txtProductValue").value);
                        if (bp > pv) {
                            alert("Buying Price cannot be Greater than Selling Price.");
                            return false;
                        }
                    }
                }
            }
            $("#<%=imgSave.ClientID%>").attr("display", "none");
            $("#<%=imgSaveDisabled.ClientID%>").attr("display", "block");

        }


    </script>
    <script type="text/javascript">
        function setcategoryandbrand(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            var categoryid = document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_ddlCategory").value;
            // alert(categoryid);
            var brandname = document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtBrand").value;
            document.cookie = "categoryid=" + categoryid;
            document.cookie = "brandname=" + brandname;
            //alert(brandname);
            //            $.ajax({
            //                type: "POST",
            //                dataType: "json",
            //                contentType: "application/json; charset=utf-8",
            //                data: "{Brandname:'" + brandname + "',Categoryid:'" + categoryid + "'}",
            //                url: "SupplierDeliveryNote.aspx/getBrandCategory",
            //                success: function(data) {
            //                },
            //                failure: function(result) {
            //                    alert(result);
            //                }
            //            });
        }
        function CheckValue(aa) {
            var validationsummary = $("#<%=ValidationSummary1.ClientID%>");

            var elementvalue = aa.value;
            if (isNaN(elementvalue)) {
                aa.value = "";
                // $("#dvSummary #<%=ValidationSummary1.ClientID%>").append("<ul>Please Enter numeric Value.</ul>");
                alert("Please Enter Numeric Value.");
                return false;
            }
            else {
                if (checkDecimals(aa, aa.value) == true)
                    return true;
                else
                    return false;
            }
        }
        function checkDecimals(fieldName, fieldValue) {
            decallowed = 2;
            if (isNaN(fieldValue) || fieldValue == "") {

                //$("#dvSummary>ul").append("<li>Enter valid Numeric value upto 2 decimal points.</li>");
                alert("Enter Valid Numeric Value upto 2 decimal places.");
                fieldValue = "";
                fieldName.select();
                fieldName.focus();
                return false;
            }
            else {
                if (fieldValue.indexOf('.') == -1) fieldValue += ".";
                dectext = fieldValue.substring(fieldValue.indexOf('.') + 1, fieldValue.length);

                if (dectext.length > decallowed) {

                    // $("#dvSummary>ul").append("<li>Enter a number with up to " + decallowed + " decimal places.</li> ");
                    alert("Enter a number up to " + decallowed + " decimal places.");
                    fieldName.value = "";
                    fieldName.select();
                    fieldName.focus();
                    return false;
                }
                else {

                    return true;
                }
            }
        }
        function validDecimal() {

            var reg = /(^100([.]0{1,2})?)$|(^\d{1,2}([.]\d{1,2})?)$/;
            var val = document.getElementById("ctl00_ContentPlaceHolder1_txtMaxDiscPer").value;
            if (reg.test(val) == false) {
                alert("Enter valid Decimal no upto two decimal places and below hundred");
                document.getElementById("ctl00_ContentPlaceHolder1_txtMaxDiscPer").value = "0.00";
                document.getElementById("ctl00_ContentPlaceHolder1_txtMaxDiscPer").focus();
                return false;
            }
            return true;

        }
        function SetContextKey(aa) {

        }
        function loader(aa) {
            // document.getElementById("ctl00_ContentPlaceHolder1_imgloader").style = "display:block;";
            var id = aa.id.split('_')[3].split("l")[1];
            var product = document.getElementById("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtProduct").value;
            $("#ctl00_ContentPlaceHolder1_ajaxloader").show();
            $("#dvimg").show();
            __doPostBack("ctl00_ContentPlaceHolder1_gvSDND_ctl" + id + "_txtProduct", "txtProduct_ontextchanged");
            //setTimeout(hideloader, 2000);
            return true;

        }
        function hideloader() {
            $("#ctl00_ContentPlaceHolder1_ajaxloader").hide();
            $("#dvimg").hide();
        }
    </script>

   <%-- <script type="text/javascript" language="javascript">
        System.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
</script>--%>

    <form runat="server" id="form">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
         <script type="text/javascript" language="javascript">
             Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
             function EndRequestHandler(sender, args) {
                 if (args.get_error() != undefined) {
                     args.set_errorHandled(true);
                 }
             }
</script>  
        <div class="page-content-wrap">
            <asp:UpdatePanel ID="up1" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnClearSearch" />
                    <asp:PostBackTrigger ControlID="ImgSearch" />

                </Triggers>
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-12">
                            <form class="form-horizontal">
                                <div class="panel panel-default">
                                    <!-- Screen Heading  Start-->
                                    <div class="panel-heading">
                                        <h3 class="panel-title">
                                            <strong>Goods Received</strong>
                                        </h3>
                                        <ul class="nav nav-tabs pull-right" role="tablist" style="margin-top: 0px;">
                                            <li id="lirole" runat="server">

                                                <asp:Button ID="lnkAdd" runat="server" CssClass="btn btn-primary pull-right"
                                                    Text="Add" OnClick="lnkAdd_Click" />

                                            </li>
                                        </ul>

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
                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">
                                                            Organisation</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlOrganisationSearch"
                                                                runat="server" class="form-control select">
                                                                <%--  <asp:ListItem Value="">Select</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">
                                                            Supplier</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlSupplierSearch"
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
                                                            DeliveryNote No</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                                <asp:TextBox ID="txtDeliveryNoteNoSearch" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                                <asp:TextBox ID="txtFromDate"
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
                                                        <label class="col-md-4 control-label">
                                                        </label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <asp:Button ID="ImgSearch" runat="server" Text="Search"
                                                                    CssClass="btn btn-info" OnClick="ImgSearch_click" />&nbsp;&nbsp;
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
                                            <div id="pnlSearchGrid" runat="server">

                                                <div id="gridClass1" class="table-responsive">
                                                    <asp:GridView ID="gvSDN" runat="server" AutoGenerateColumns="false"
                                                        DataKeyNames="SupplierDeliveryNoteID" CssClass="table datatable  table-bordered table-striped table-actions"
                                                        OnRowCommand="gvSDN_RowCommand" OnRowDeleting="gvSDN_RowDeleting" OnRowEditing="gvSDN_RowEditing"
                                                        OnRowCreated="gvSDN_RowCreated" OnRowDataBound="gvSDN_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name"
                                                                ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                            <asp:BoundField DataField="SupplierDeliveryNoteNo" HeaderText="Delivery Note No"
                                                                ItemStyle-CssClass="middle"
                                                                ItemStyle-Width="30" />
                                                            <asp:BoundField DataField="SupplierDeliveryNoteDate" HeaderText="Delivery Note Date"
                                                                ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                            <asp:BoundField DataField="TotalValue" HeaderText="Total Value"
                                                                ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                                            <asp:TemplateField ItemStyle-Width="90">
                                                                <ItemTemplate>
                                                                    <%--<asp:ImageButton ID="lnkbtnView" runat="server" ImageUrl="../Images/hammer_screwdriver.png"
                                                                        Text="View" CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' CommandName="View"
                                                                        Style="width: 16px" />
                                                                    <asp:ImageButton ID="lnkbtnEdit" runat="server" ImageUrl="../Images/pencil.png" Text="Edit"
                                                                        CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' CommandName="EditRow" />
                                                                    <asp:ImageButton ID="lnkbtnDel" runat="server" ImageUrl="../Images/cross.png" Text="Delete"
                                                                        CommandArgument='<%# ((GridViewRow)Container).RowIndex%>' OnClientClick="return confirm('Do you want to Delete the record?');"
                                                                        CommandName="Deleting" />
                                                                    --%>
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
                                                                        Style="width: 20px; height: 20px;" OnClick="lnkbtnPrint_OnClick" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div id="pnlAdd" runat="server">
                                        <div class="page-content-wrap">
                                            <br />
                                            <center>
                                                <div id="dvimg" style="z-index: 9999; display: none;" class="modal">
                                                    <asp:Image ID="ajaxloader" runat="server" ImageUrl="~/images/ajax-loader.gif" Style="display: none; width: 100px; height: 100px; margin-top: 250px;" />
                                                </div>
                                            </center>

                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Organisation:</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlOrganisation" runat="server"
                                                                class="form-control select"
                                                                Style="margin-bottom: 12px;">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="ddlOrganisation" InitialValue="0" ErrorMessage="Please Select Organisation"
                                                            Text="*"></asp:RequiredFieldValidator>
                                                        <asp:HiddenField ID="HDSupplierDeliveryNoteID" runat="server" />

                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Supplier:</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlSupplier" runat="server"
                                                                class="form-control select"
                                                                Style="margin-bottom: 12px;">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator3" runat="server"
                                                            ControlToValidate="ddlSupplier" InitialValue="0" ErrorMessage="Please Select Supplier"
                                                            Text="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Delivery Note No:</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                <asp:TextBox CssClass="form-control" ID="txtDeliveryNoteNo" runat="server" Enabled="false" />
                                                            </div>
                                                            <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txtDeliveryNoteNo" InitialValue="" ErrorMessage="Please Enter Delivery Note no."
                                                                Text="*"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Delivery Note Date:</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group pull-right">
                                                                <asp:TextBox ID="txtDeliveryNoteDate"
                                                                    CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                            </div>
                                                            <label class="help-block">
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row top" style="display: none">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Payment Due Date:</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group pull-right">
                                                                <asp:TextBox ID="txtPaymentDueDate" CssClass="form-control datepicker"
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
                                                <div class="form-group">
                                                    <div class="col-md-2" id="btn1" runat="server">
                                                        <asp:Button ID="btnSaveDeliveryNote" runat="server" CssClass="btn btn-success btn-block pull-right"
                                                            Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" 
                                            
                                                            OnClick="btnSaveDeliveryNote_Click" OnClientClick="return ValidateFields();this.disabled = true;" />
                                                    </div>
                                                    <div class="col-md-2" id="dvcancel" runat="server">
                                                        <asp:Button ID="btncancel1" runat="server" CssClass="btn btn-danger btn-block pull-right"
                                                            Text="Cancel" Style="margin-bottom: 5px;" OnClick="btncancel1_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                              
                                            <br />
                                            <div id="gridClass" class="table-responsive">
                                                <asp:GridView ID="gvSDND" runat="server" AutoGenerateColumns="false"
                                                    DataKeyNames="SupplierDeliveryNoteDetailID" AlternatingRowStyle-BackColor="White"
                                                    CellPadding="1" CssClass="table datatable table-bordered table-striped table-actions"
                                                    AlternatingRowStyle-Height="10px" OnRowCommand="gvSDND_RowCommand" OnRowDataBound="gvSDND_RowDataBound"
                                                    OnRowDeleting="gvSDND_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Category" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                            <%--<ControlStyle Width="150px" />
                                                <ItemStyle Width="150px" />--%>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control select"
                                                                    TabIndex="-1">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="HDSupplierDeliveryNoteDetailID" runat="server" Value='<%#Eval("SupplierDeliveryNoteDetailID")%>' />
                                                               
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Brand" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                            <%--  <ItemStyle Width="190px" />--%>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control select" AutoPostBack="true"
                                                                    Visible="false"
                                                                    OnSelectedIndexChanged="ddlBrand_selectedindexchanged">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control" AutoComplete="on"
                                                                    TabIndex="-1"></asp:TextBox>
                                                                <%--AutoPostBack="true" OnTextChanged="txtBrand_ontextchanged"--%>
                                                                <asp:ImageButton ID="imgBrand" runat="server" ImageUrl="~/images/plus.png"
                                                                    Style="width: 20px; float: right; margin-top: -25px; margin-left: 160px;"
                                                                    OnClientClick="return Brandapplystyle(this);" CommandName="Brand" Visible="false" />
                                                                <asp:HiddenField ID="HDBrandID" runat="server" />
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="AutoCompleteAjaxRequest"
                                                                    ServicePath="~/Screens/AutoComplete.asmx" MinimumPrefixLength="1" CompletionInterval="100"
                                                                    EnableCaching="false" CompletionSetCount="10" TargetControlID="txtBrand" FirstRowSelected="false"
                                                                    CompletionListCssClass="completionList"
                                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                </asp:AutoCompleteExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Model No" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control select" AutoPostBack="true"
                                                                    Visible="false"
                                                                    OnSelectedIndexChanged="ddlProduct_selectedindexchanged">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control" AutoComplete="on"
                                                                    AutoPostBack="true" OnTextChanged="txtProduct_ontextchanged"
                                                                   ></asp:TextBox>
                                                                <%-- " --%>
                                                                <asp:ImageButton ID="imgProduct" runat="server" ImageUrl="~/images/plus.png" Style="width: 20px; float: right; margin-top: -25px; margin-left: 160px;"
                                                                    OnClientClick="return Productapplystyle(this);" CommandName="Product" Visible="false" /><%--OnClientClick="return applystyle();"--%>
                                                                <asp:HiddenField ID="HDProductID" runat="server" />
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="AutoCompleteProductRequest"
                                                                    ServicePath="~/Screens/AutoComplete.asmx" MinimumPrefixLength="1" CompletionInterval="100"
                                                                    EnableCaching="false" CompletionSetCount="10" TargetControlID="txtProduct" FirstRowSelected="false"
                                                                    UseContextKey="true" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                                    CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                </asp:AutoCompleteExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Buying Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBuyingPrice" runat="server" CssClass="form-control" Text='<%#Eval("BuyingPrice")%>'
                                                                 AutoPostBack="true" OnTextChanged="txtBuyingPrice_TextChanged"
                                                                    onkeyup="return calc(this);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="middle" ItemStyle-Width="30">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Text='<%#Eval("Quantity")%>'
                                                                      AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged" onkeyup="return Calculate(this);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tot.Buying Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtNetBuyingPrice" Enabled="false" runat="server" CssClass="form-control"
                                                                    Text='<%#Eval("NetBuyingPrice")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Selling Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProductValue" runat="server" CssClass="form-control"
                                                                    Text='<%#Eval("ProductValue")%>'  AutoPostBack="true" OnTextChanged="txtProductValue_TextChanged"
                                                                     onkeyup="return Calculate1(this);"></asp:TextBox>
                                                                <%--onblur="return Calculate1(this);" AutoPostBack="true" OnTextChanged="txtProductValue_ontextchanged"--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tot. Selling Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtGrossValue" runat="server" Enabled="false" CssClass="form-control"
                                                                    Text='<%#Eval("GrossProductValue")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="30">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="imgDeleteRow" runat="server" CommandName="DeleteRow"
                                                                    OnClientClick="return confirm('Do you want to Delete the record?');" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'><span class="fad fa-times"> </span></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div style="height: 10px;"></div>
                                            <div id="tbl" runat="server" align="left">
                                                <div class="row top">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Total Buying Price:</label>
                                                            <div class="col-md-8">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                    <asp:TextBox CssClass="form-control" ID="txtTotalPrice" runat="server" />
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
                                                                    <asp:TextBox CssClass="form-control" ID="txttotalgrossvalue" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row top">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Handling Charges:</label>
                                                            <div class="col-md-8">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                    <asp:TextBox CssClass="form-control" ID="txtHandlingCharges" runat="server" onkeyup="return mult();" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Total Product Value:</label>
                                                            <div class="col-md-8">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                    <asp:TextBox ID="txtTotalProductValue" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                <div runat="server">
                                                    <div class="form-group">
                                                        <div class="col-md-2" id="dvClear" runat="server">
                                                            <asp:Button ID="imgClear" runat="server" CssClass="btn btn-warning btn-block pull-left"
                                                                alt="Clear" Text="Clear" Style="margin-bottom: 5px;" />
                                                        </div>
                                                        <div class="col-md-2" id="dvisave" runat="server">
                                                            <asp:Button ID="imgSave" ValidationGroup="r" runat="server" CssClass="btn btn-success btn-block pull-right"
                                                                Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" OnClientClick="return NullValidation();"
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
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%--<div id="divprintarea" class="modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-position">
                    <div class="modal-content" id="printdiv">
                        <div class="modal-header">
                            <center>
                                <h2 class="modal-title">Goods Received</h2>
                            </center>
                        </div>

                        <div class="modal-body">
                            <div class="table-responsive">
                                <table width="800px" align="center" id="Table1" >
                                    <tr>
                                        <td style="width: 30px"></td>
                                        <td align="left">
                                            <label id="lblprintSupplierName" class="Label">
                                            </label>
                                            <br />
                                            <label id="Label17" class="Label">
                                                DeliveryNote No:</label>
                                            <label id="lblprintDeliveryNoteNo" class="Label">
                                            </label>
                                            <br />
                                            <label id="lblDelNoteDate" class="Label">
                                                DeliveryNote Date:</label>
                                            <label id="lblDeliveryNoteDate" class="Label">
                                            </label>
                                            <br />
                                            <label id="lblprintSupplierAddress" class="Label">
                                            </label>
                                            <br />
                                            <label id="lblprintSupplierPincode" class="Label">
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
                                </table>
                            </div>
                            <br />


                            <div id="printSupplierDeliveryNote">
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
                                                <th style="color: white; background-color: black;">Buying Price 
                                                </th>
                                                <th style="color: white; background-color: black;">Quantity
                                                </th>
                                                <th style="color: white; background-color: black;">Tot. Buying Price
                                                </th>
                                                <th style="color: white; background-color: black;">Selling Price
                                                </th>
                                                <th style="color: white; background-color: black;">Tot. Selling Price 
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


                        <div class="modal-footer">
                            <input type="button" id="btnPrint" value="Print" class="BtnEmptyStyle" title="Print"
                                onclick="printDiv()" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                CssClass="BtnEmptyStyle" OnClick="btnCancel_Click" OnClientClick="return printdivhide();" /><%--OnClientClick="return removestyle();"--%>
            <%-- </div>
                    </div>
                </div>
            </div>--%>

            <div id="dvProductPopUp" style="display: none; z-index: 1040;" class="modal fade in"
                role="dialog" tabindex="-1"
                aria-hidden="false" runat="server">
                <div class="modal-backdrop fade in" style="height: 100%;">
                </div>
                <div class="modal-dialog modal-lg ">
                    <div class="modal-content" style="height: 600px;">
                        <div class="modal-header">
                            <h4 class="modal-title">Product </h4>
                        </div>
                        <asp:Label ID="lblStatusProduct" runat="server" CssClass="MessageClass" Style="color: Red;"></asp:Label>
                        <div class="modal-body" style="height: 520px; overflow-y: auto;">

                            <table align="center" class="table table-bordered table-striped table-actions">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" CssClass="control-label">Category:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDLCategory" runat="server" CssClass="DropDownClass" Style="margin-left: -27px;">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="DDLCategory"
                                            InitialValue="0" ErrorMessage="Please Select Category" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" CssClass="control-label">Brand:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBrand" runat="server" CssClass="DropDownClass" Style="margin-left: -27px;">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator0" runat="server"
                                            ControlToValidate="ddlBrand"
                                            InitialValue="0" ErrorMessage="Please Select Brand" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblProductName" runat="server" CssClass="control-label">Product Name:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" Style="margin-left: -10px;"></asp:TextBox>

                                        <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtProductName" InitialValue=""
                                            ErrorMessage="Please Enter Product Name" Text="*"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" CssClass="control-label">Product Value (Selling Price):</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProductValue" runat="server" CssClass="form-control" onchange="return CheckValue(this);"
                                            Style="margin-left: -7px;"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="txtProductValue" InitialValue=""
                                            ErrorMessage="Please Enter Product value" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>
                                        <asp:Label ID="Label13" runat="server" CssClass="control-label" Visible="false">Default Discount%(Selling):</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDefaultSellingDiscount" runat="server" CssClass="form-control"
                                            Visible="false"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDefaultSellingDiscount" InitialValue=""  
                            ErrorMessage="Please Enter Selling Discount" Text="*"></asp:RequiredFieldValidator>  --%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" CssClass="control-label" Visible="true"> Maximum Discount % :</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaxDiscPer" runat="server" CssClass="form-control" Visible="true"
                                            Text="0.00" onblur="return validDecimal()" Style="margin-left: -15px;"></asp:TextBox>

                                    </td>

                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <asp:Label ID="Label15" runat="server" CssClass="control-label">IsActive:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkActiveProduct" runat="server" Checked="true" Style="margin-left: -170px;" />
                                    </td>
                                </tr>
                            </table>
                            <center>
                                <asp:Button ID="imgSaveProduct" runat="server" Text="Save" CssClass="btn btn-info"
                                    ValidationGroup="r"
                                    OnClick="imgSaveProduct_Click" />
                                <asp:Button ID="btnClearProduct" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                    OnClick="btnClearProduct_onclick" OnClientClick="return removestyle();" />
                            </center>
                            <center>
                                <div id="dvSummary1" align="center">
                                    <asp:ValidationSummary CssClass="validationSummary" ID="ValidationSummary1" ValidationGroup="r"
                                        runat="server" />
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
            <div id="dvBrandPopUp" style="display: none; z-index: 1040;" class="modal fade in"
                role="dialog" tabindex="-1" aria-hidden="false" runat="server">
                <div class="modal-backdrop fade in" style="height: 100%;">
                </div>
                <div class="modal-dialog modal-lg ">
                    <div class="modal-content" style="height: 600px;">
                        <div class="modal-header">
                            <h4 class="modal-title">Brand </h4>
                        </div>
                        <asp:Label ID="lblStatusBrand" runat="server" CssClass="MessageClass" Style="color: Red;"></asp:Label>
                        <div class="modal-body" style="height: 520px; overflow-y: auto;">
                            <table border="0" align="center" cellpadding="1" cellspacing="2" style="width: 400px; margin-bottom: 14px"
                                class="table table-bordered table-striped table-actions">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAddBrand" runat="server" Text="Brand Name" CssClass="control-label"></asp:Label>
                                        <asp:HiddenField ID="HiddenFieldBrandId" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddBrand" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAddBrand" runat="server" ValidationGroup="rr1"
                                            ControlToValidate="txtAddBrand"
                                            Text="*" ErrorMessage="Brand Name Can not be Empty">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="IsActive" CssClass="control-label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" Style="margin-left: -170px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="imgAddBrand" runat="server" Text="Save" CssClass="btn btn-info" ValidationGroup="rr1"
                                            OnClientClick="return removestyle();" OnClick="imgAddBrand_Click" />
                                        <asp:Button ID="btnCancelBrand" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                            OnClick="btnCancelBrand_onclick" OnClientClick="return removestyle();" />
                                    </td>
                                </tr>
                            </table>

                            <center>
                                <div id="dvSummary" align="center">
                                    <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="rr1"
                                        CssClass="validationSummary" />
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
            <form name="SubOrgForm" novalidate>
                <div class="modal" id="divprintarea" tabindex="-1" role="dialog" aria-hidden="true" style="height: 100%; overflow-y: auto;">
                    <div class="modal-dialog modal-lg modal-position" id="printdiv">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" style="display:none;"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                <center><h4 class="modal-title">Goods Received</h4></center>
                            </div>
                            <div class="modal-body">
                                <div class="row ">
                                    <div class="col-md-6" style="width:35%;float:left;margin-left:15%;">
                                        <div class="row">
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="input-group">
                                                            <label id="lblprintSupplierName" class="Label"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                           <%-- <div class="row">
                                                <div class="form-group">--%>
                                                    <label id="Label17" class="Label pull-left">DeliveryNote No:</label>
                                                   <%-- <div class="row">
                                                        <div class="input-group">--%>
                                                            <label id="lblprintDeliveryNoteNo" class="Label"></label>
                                                        <%--</div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                        </div>
                                        <div class="row">
                                            <%--<div class="row">
                                                <div class="form-group">--%>
                                                    <label id="lblDelNoteDate" class="Label pull-left">DeliveryNote Date:</label>
                                                   <%-- <div class="row">
                                                        <div class="input-group">--%>
                                                            <label id="lblDeliveryNoteDate" class="Label"></label>
                                                        <%--</div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                        </div>
                                        <div class="row">
                                            <div class="row">
                                                <div class="form-group">

                                                    <div class="row">
                                                        <div class="input-group">
                                                            <label id="lblprintSupplierAddress" class="Label"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">

                                                    <div class="col-md-12">
                                                        <div class="input-group">
                                                            <label id="lblprintSupplierPincode" class="Label"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">

                                                    <div class="col-md-12">
                                                        <div class="input-group">
                                                            <label id="lblprintCity" class="Label"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="width:46%;float:right;">
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
                                    <div class="panel-body panel-body-table">
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
                                                            <th style="color: white; background-color: black;">Buying Price 
                                                            </th>
                                                            <th style="color: white; background-color: black;">Quantity
                                                            </th>
                                                            <th style="color: white; background-color: black;">Tot. Buying Price
                                                            </th>
                                                            <th style="color: white; background-color: black;">Selling Price
                                                            </th>
                                                            <th style="color: white; background-color: black;">Tot. Selling Price 
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
                                <input type="button" id="btnPrint" value="Print" class="BtnEmptyStyle" title="Print"
                                    onclick="printDiv()" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    CssClass="BtnEmptyStyle" OnClick="btnCancel_Click" OnClientClick="return printdivhide();" />
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </form>


    
   
</asp:Content>
