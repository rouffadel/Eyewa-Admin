<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Sales.aspx.cs" Inherits="Screens_Sales" Title="Sales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:ToolkitScriptManager ID="tsm" runat="server">
    </asp:ToolkitScriptManager>--%>

    <style type="text/css">
        #sides
        {
            margin: 0;
        }

        #left
        {
            float: left;
            width: 400px;
            overflow: auto;
        }

        #right
        {
            float: left;
            width: 500px;
            overflow: auto;
            margin-right: 70px;
            margin-left: 10px;
        }

        .completionList
        {
            border: solid 1px Gray;
            margin: 0px;
            padding: 3px;
            list-style-type: none;
            border-radius: 4px;
            background-color: #FFFFFF;
        }

        .listItem
        {
            background-color: #191919;
        }

        .itemHighlighted
        {
            background-color: #BDBABA;
        }
    </style>

   

    <%--<style>
        .modalOverlay {
    position: fixed;
    width: 100%;
    height: 100%;
    top: 0px;
    left: 0px;
    
   background-color: rgba(0,0,0,0.3); /* black semi-transparent */
    
}--%>
    <style type="text/css">
        .SupplierwiseInvoicePopupStyle
        {
            position: fixed;
            width: 1070px;
            z-index: 500; /*height: 445px;*/
            height: 530px;
            padding: 0px;
            background-color: #fff;
            border: solid 6px #666;
            margin: 0px 0px 0px -2%;
            top: 0;
            bottom: 0%;
            text-align: center;
        }
    </style>
      <style>
        .Overlay
        {
            visibility: hidden;
            position: absolute;
            left: 0px;
            top: 0px;
            font-family: verdana;
            font-weight: bold;
            padding: 40px;
            z-index: 100;
            background-image: url(Mask.png);
            _background-image: none;
            _filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled=true, sizingMethod=scale src='Mask.png');
        }

        .modalOverlay
        {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0px;
            left: 0px;
            background-color: rgba(0,0,0,0.3); 
        }
    </style>

  <script src="../js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript">

        function SalesForPrint(salesid) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{salesid:'" + salesid + "'}",
                dataType: "json",
                url: "Sales.aspx/SalesForPrint",
                success: function (data) {
                    if (data.d.length != 0) {
                        var sale = data.d.eSales;
                        var Salelist = data.d.eSaleslist;
                        var salepriscription = data.d.eSalePriscription;

                        if (Salelist.length != 0) {

                            $("body").append('<div id="modalOverlay"  class="modalOverlay">');
                            $("#divprintarea").show();
                            $("#divprintarea").css("display", "block !important");
                            $("#<%=lblStoreNamePopup.ClientID%>").html(sale[0].StoreName);
                            $("#<%=lblAddressPopup.ClientID%>").html(sale[0].Address);
                            $("#<%=lblCustomerNamePopup.ClientID%>").html(":" + sale[0].CustomerName);
                            $("#<%=lblCustomerNoPopup.ClientID%>").html(":" + sale[0].CustomerNo);
                            $("#<%=lblTotalGrossValue.ClientID%>").html(sale[0].GrossTotal);
                            $("#<%=lblDiscount.ClientID%>").html(sale[0].Discount);
                            $("#<%=lblNetValue.ClientID%>").html(sale[0].NetTotal);
                            $("#<%=lblLoginUser.ClientID%>").html(sale[0].LoginName);
                            $("#lblprintinvoiceno").html(":" + sale[0].InvoiceNo);
                            $("#lblprintinvoiceDate").html(":" + sale[0].InvoiceDate);
                            var AmountPaid = parseInt((sale[0].PaidAmount));

                            var Balance = parseInt(sale[0].Balance);
                            var paymentmode = sale[0].PaymentMode;
                            $("#<%=lblsavePopupAmountPaid.ClientID %>").html(AmountPaid);
                            $("#<%=lblsavePopupBal.ClientID %>").html(Balance);
                            $("#<%=lblPaymentmodeup.ClientID%>").html(paymentmode);
                            $("#tblSalesPrint > tbody").empty();
                            for (var i = 0; i < Salelist.length; i++) {
                                $("#tblSalesPrint > tbody").append("<tr style='font-size:12px;font-family:verdana;'><td style='text-align:center;'><lable style='color:black;'>" + Salelist[i].CategoryName + "</label></td><td style='text-align:center;'><lable style='color:black;'>" + Salelist[i].BrandName + "</label></td><td style='text-align:center;'><lable style='color:black;'>" +
                        Salelist[i].ProductName + "</label></td><td style='text-align:center;'><lable style='color:black;'>" + Salelist[i].ProductValue + "</label></td><td style='text-align:center;'><lable style='color:black;'>" + Salelist[i].Quantity + "</label></td><td style='text-align:center;'><lable style='color:black;'>" + Salelist[i].Discount + "</label></td><td style='text-align:center;'><lable style='color:black;'>" + Salelist[i].SellingPrice
                        + "</label></td>" + "</tr>");
                            }
                            for (var i = 0; i < Salelist.length; i++) {
                                var str = "LENSE";
                                var str2 = Salelist[i].CategoryName.toUpperCase();
                                $("#tblSalesPrint > tbody").append(
                                "<tr style='font-size:12px;font-family:verdana'>" +
                                "<td align='left' colspan='7'></td>" +
                               " <table id='tbllens' border='1' width='400px'><tr style='font-size:12px;font-family:verdana;color:White;background-color:black;'><th style='background-color:black;color:white;'>Prescription Details</th><th style='background-color:black;' >SPH</th><th style='background-color:black;'>CYL</th><th style='background-color:black;'>AXIS</th><th style='background-color:black;'>ADD</th><th style='background-color:black;'></th><th style='background-color:black;'></th></tr><tr style='font-size:12px;font-family:verdana;'><td>Right Eye</td><td>" + salepriscription[0].SPH_RightEye +
                               "</td><td>" + salepriscription[0].CYL_RightEye + "</td><td>" + salepriscription[0].AXIS_RightEye + "</td><td>" + salepriscription[0].ADD_RightEye + "</td><td></td><td></td></tr>" +
                               "<tr style='font-size:12px;font-family:verdana;'><td>Left Eye</td><td>" + salepriscription[0].SPH_LeftEye + "</td><td>" + salepriscription[0].CYL_LeftEye + "</td><td>" + salepriscription[0].AXIS_LeftEye + "</td><td>" + salepriscription[0].ADD_LeftEye + "</td><td></td><td></td></tr>" +
                                "<tr style='font-size:12px;font-family:verdana;'><td>IPD</td><td>" + salepriscription[0].SPH_IPD + "</td><td>" + salepriscription[0].CYL_IPD + "</td><td>" + salepriscription[0].AXIS_IPD + "</td><td>" + salepriscription[0].ADD_IPD + "</td><td></td><td></td></tr>" +
                              +"</table></td></tr>");
                                break;

                            }
                            $("table#tblSalesPrint tr:even").css("background-color", "#F3F3F3");
                            $("table#tblSalesPrint tr:odd").css("background-color", "#ffffff");
                        }
                    }
                    else {
                        $("#divprintarea").css("display", "none");
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
    $("#dvinv2").css("display", "none");
    $("#dvpopup2").css("display", "block");
    var printarea = $("#divprintarea").html();
    var w = window.open();
    $("#tblSalesPrint th").css("background-color", "black");
    $("#tblSalesPrint th").css("color", "white");
    $("#tblSalesPrint tr:even").css("background-color", "White");
    $("#tblSalesPrint tr:odd").css("background-color", "#f2f2f2");
    $("#tbllens th").css("background-color", "Black");
    $("#tbllens th").css("color", "white");
    $("#tbllens tr:even").css("background-color", "White");
    $("#tbllens tr:odd").css("background-color", "#f2f2f2");
    w.document.writeln(printarea);
    w.print();
    $("#btnPrint").show();
    $("#<%=btnCancel.ClientID%>").show();
    $("#dvinv2").css("display", "block");
    $("#dvpopup2").css("display", "none");
}


function SalesPrint(salesid) {

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
                var priscription = data.d.eSalePriscription


                $("#divPrint").show();
                $("#divPrint").css("display", "block !important");
                $("#lblprintStoreName").html(sale[0].StoreName);
                $("#lblCustomerprintName").html(":" + sale[0].CustomerName);
                $("#lblprintCustomerCustomerNo").html(":" + sale[0].CustomerNo);
                $("#lblCustomerInvoiceNO").html(":" + sale[0].InvoiceNo);
                $("#lblStoreprintAddress").html(sale[0].Address);
                $("#lblStoreVATID").html(sale[0].GrossTotal);
                $("#lblCountry").html(sale[0].Country);
                $("#lblStoreprintCity").html(sale[0].City);
                $("#lblContNum").html(sale[0].ContactNum);
                $("#lblEmail").html(sale[0].Email);
                $("#lblInvoiceNoteDate").html(":" + sale[0].InvoiceDate);
                if (data.d.eSaleslist.length == 0) {
                    $("#divPrint").hide();
                    $("#divPrint").css("display", "none");
                    //$("#divPrint").css("cursor", "pointer");
                    return false;
                }
                else {
                    $("body").append('<div id="Overlay" class="modalOverlay">');
                    $("#tblPrintSales > tbody").empty();
                    for (var i = 0; i < Salelist.length; i++) {
                        $("#tblPrintSales > tbody").append("<tr style='color:black;font-size:12px;font-family:verdana;'><td style='text-align:center;'><lable >" + Salelist[i].CategoryName + "</label></td><td style='text-align:center;'><lable style='color:black;'>" + Salelist[i].BrandName + "</label></td><td style='text-align:center;'><lable style='color:black;'>" +
                Salelist[i].ProductName + "</label></td><td style='text-align:center;'><lable style='color:black;'>" + Salelist[i].ProductValue + "</label></td><td style='text-align:center;'><lable style='color:black;'>" + Salelist[i].Quantity + "</label></td><td style='text-align:center;'><lable style='color:black;'>" + Salelist[i].Discount + "</label></td><td style='text-align:center;'><lable style='color:black;'>" + Salelist[i].SellingPrice);
                    }
                    for (var i = 0; i < Salelist.length; i++) {
                        var str = "LENSE";

                        var str2 = Salelist[i].CategoryName.toUpperCase();

                        if (priscription != null) {
                            if (priscription.length > 0) {
                                $("#tblPrintSales > tbody").append(
                    "<tr style='font-size:12px;font-family:verdana;'>" +
                    "<td align='left' colspan='7'></td>" +
                   " <table id='tblLense' border='1' width='400px' ><tr style='font-size:12px;font-family:verdana;background-color:black;color:White;'><th style='background-color:black;color:white;'>Prescription Details</th><th style='background-color:black;color:white;'>SPH</th><th style='background-color:black;color:white;'>CYL</th><th style='background-color:black;color:white;'>AXIS</th><th style='background-color:black;color:white;'>ADD</th><th style='background-color:black;color:white;'></th><th style='background-color:black;color:white;'></th></tr ><tr style='font-size:12px;font-family:verdana;'><td>Right Eye</td><td>" + priscription[0].SPH_RightEye +
                   "</td><td>" + priscription[0].CYL_RightEye + "</td><td>" + priscription[0].AXIS_RightEye + "</td><td>" + priscription[0].ADD_RightEye + "</td><td></td><td></td></tr>" +
                   "<tr style='font-size:12px;font-family:verdana;'><td>Left Eye</td><td>" + priscription[0].SPH_LeftEye + "</td><td>" + priscription[0].CYL_LeftEye + "</td><td>" + priscription[0].AXIS_LeftEye + "</td><td>" + priscription[0].ADD_LeftEye + "</td><td></td><td></td></tr>" +
                    "<tr style='font-size:12px;font-family:verdana;'><td>IPD</td><td>" + priscription[0].SPH_IPD + "</td><td>" + priscription[0].CYL_IPD + "</td><td>" + priscription[0].AXIS_IPD + "</td><td>" + priscription[0].ADD_IPD + "</td><td></td><td></td></tr>" +
                  +"</table></td></tr>");
                                break;
                            }

                        }

                    }
                    var Remarks = Salelist[0].Remarks
                    if (Remarks == "" || Remarks == null) {
                        Remarks = "";
                    }
                    var PaidAmount = parseFloat($("txtPaidAmount").value);
                    //$("#tblPrintSales > tfoot").append("<tr style='background-color:White;height:20px;font-weight:bold;font-size:12px;font-family:verdana;'>" +
                    //           "<td align='center' colspan='7'></td>" +
                    //          "</tr>");

                    //$("#tblPrintSales > tfoot").append("<tr style='font-weight:bold;font-size:12px;font-family:verdana;'>" +
                    //           "<td align='center' colspan='7'><b>Details:</b></td>" +
                    //          "</tr>");


                    //$("#tblPrintSales > tfoot").append("<tr style='background-color:White;font-weight:bold;font-size:12px;font-family:verdana;'>" +
                    //                              "<td align='left' colspan='7'><b>Remarks:</b></td>" +
                    //                                "</tr>");
                    $("#tblPrintSales > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                               "<td align='left' colspan='3' rowspan='7'>" + Remarks + "</td>" +
                              "</tr>");

                    $("#tblPrintSales > tfoot").append("<tr style='display:none;'>" +
                                                   "<td align='right' colspan='2'>Total Amount:</td>" +
                                                     "<td align='center'>" + Salelist[0].TotalGrossValue + "</td>" +
                                                   "</tr>");
                    $("#tblPrintSales > tfoot").append("<tr style='font-size:12px;font-family:verdana;'><td align='center' >Details:</td>" +
                             "<td   align='right' colspan='2'>Total Amount:</td>" +
                            "<td  align='center'>" + Salelist[0].NetValue + "</td>" +
                               "</tr>");
                    $("#tblPrintSales > tfoot").append("<tr style='display:none;'>" +

                               "</tr>");
                    $("#tblPrintSales > tfoot").append("<tr style='font-size:12px;font-family:verdana;'> " +
                            "<td   align='right'>Payment Mode</td>" +
                            "<td  align='center'>" + sale[0].PaymentMode + "</td>" +
                            "<td align='right' colspan='1'>Amount Paid:</td>" +
                            "<td align='center'>" + sale[0].PaidAmount + "</td>" +
                                "</tr>");
                    $("#tblPrintSales > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                                   "<td align='right' colspan='3'>Balance:</td>" +
                                                     "<td align='center'>" + Salelist[0].Balance + "</td>" +
                                                   "</tr>");
                    $("table#tblPrintSales tr:even").css("background-color", "#F3F3F3");
                    $("table#tblPrintSales tr:odd").css("background-color", "#ffffff");
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
    $("#<%=CancelBtn.ClientID%>").hide();
    $("#dvinv1").css("display", "none");
    $("#dvpopup").css("display", "block");
    $("#printSupplierDeliveryNote").css("height", "378px");
    var printarea = $("#divPrint").html();
    var w = window.open();
    $("#tblPrintSales th").css("background-color", "Black");
    $("#tblPrintSales tr:even").css("background-color", "White");
    $("#tblPrintSales tr:odd").css("background-color", "#f2f2f2");
    //$("#tblLense th").css("background-color", "Black");
    //$("#tblLense th").css("color", "White");
    //$("#tblLense th").css("background-color", "Black");
    //$("#tblLense tr:even").css("background-color", "White");
    //$("#tblLense tr:odd").css("background-color", "#f2f2f2");
    //$("#tblLense tr:even").css("background-color", "White");
    //$("#tblLense tr:odd").css("background-color", "#f2f2f2");
    w.document.writeln(printarea);
    w.print();
    $("#PrintBtn").show();
    $("#<%=CancelBtn.ClientID%>").show();
    $("#dvinv1").css("display", "block");
    $("#dvpopup").css("display", "none");
    $("#printSupplierDeliveryNote").css("height", "inherit");

}


    </script>
    <script type="text/javascript">

        function checkDecimals(fieldName, fieldValue) {
            decallowed = 0;
            if (isNaN(fieldValue) || fieldValue == "") {

                // $("#dvSummary>ul").append("<li>Enter valid Integer Numeric value.</li>");
                fieldName.select();
                fieldName.focus();
                return false;
            }
            else {
                if (fieldValue.indexOf('.') == -1) fieldValue += ".";
                dectext = fieldValue.substring(fieldValue.indexOf('.') + 1, fieldValue.length);

                if (dectext.length > decallowed) {

                    // $("#dvSummary>ul").append("<li>Enter Valid Integer Numeric value.</li> ");
                    fieldName.select();
                    fieldName.focus();
                    return false;
                }
                else {

                    return true;
                }
            }
        }
        function checkdiscount(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            var sp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtSellingPrice").value);
            var pv = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtProductValue").value);
            var qty = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtQuantity").value);
            var totalproductValue = parseFloat(pv * qty);
            var discountpercentage = parseFloat((totalproductValue - sp) / (pv * qty)) * 100;
            var hiddendiscount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_HDProductDiscount").value);
            if (discountpercentage > hiddendiscount) {
                alert("Discount cannot be more than " + hiddendiscount + " %");
                aa.value = qty * pv;
                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscount").value = 0;
                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscountvalue").value = 0;
                calculateTotal();
                CalculateFinalTotal();
                return false;
            }
        }
        function Calculate(aa) {
            if (aa.value != "" && (!isNaN(aa.value))) {
                var id = aa.id.split('_')[3].split("l")[1];
                var sp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtSellingPrice").value);
                var pv = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtProductValue").value);
                var qty = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtQuantity").value);
                var QtyCtrl = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtQuantity");
                //var sellingpriceperpiece = parseFloat(aa.value) / qty;
                var totalproductValue = parseFloat(pv * qty);
                var discountpercentage = parseFloat((totalproductValue - sp) / (pv * qty)) * 100;
                var hiddendiscount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_HDProductDiscount").value);

                var discountvalue = totalproductValue - sp;
                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscount").value = discountpercentage.toFixed(2);
                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscountvalue").value = discountvalue.toFixed(2);
                if (totalproductValue < sp) {
                    alert("Selling price cannot be more than " + totalproductValue);
                    aa.value = qty * pv;
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscount").value = 0;
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscountvalue").value = 0;
                    return false;

                }

                if (isNaN(qty) || qty == 0) {
                    alert("Please Enter Quantity.");
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtQuantity").value = 0;
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtSellingPrice").value = "0.00";
                    return;
                }
                //           
                if (sp > totalproductValue) {
                    aa.value = "";
                    alert("Selling price cannot be Greater than Product Value.");
                    return false;
                }
                else {
                    calculateTotal();
                    CalculateFinalTotal();
                }
            }
            else {
                aa.value = "";
                var id = aa.id.split('_')[3].split("l")[1];
                alert("Please Enter Selling Price Value.");
                // document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtQuantity").value = 0;
                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscount").value = 0;
                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscountvalue").value = 0;
                calculateTotal();
                CalculateFinalTotal();
                return false;
            }
        }


        function calculateTotal() {
            var olv = document.getElementById("ctl00_ContentPlaceHolder1_hdorderlensevalue").value;
            var gv = 0;
            if (olv != "") {
                gv = parseFloat(olv);
            }
            var ap = 0;
            var a = document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount").value;
            if (!isNaN(parseFloat(a))) {
                ap = parseFloat(a);
            }
            //else
            ap = 0;

            var grid = document.getElementById('<%=gvSaleDetails.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvSaleDetails.ClientID%>').rows.length;
            var totalgrossvalue = 0, totalnetvalue = 0, totalproductvalue = 0, totalsellingprice = 0;
            var quat, sellingprice;
            totalproductvalue = gv;
            for (var i = 2; i <= gridrowcount; i++) {
                if (i < 10) {
                    quat = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtQuantity").value);
                    sellingprice = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtSellingPrice").value);
                    pv = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtProductValue").value);
                    if (!isNaN(quat) && quat != 0 && sellingprice != 0 && !isNaN(sellingprice)) {
                        totalgrossvalue = totalgrossvalue + (parseInt(quat) * parseFloat(sellingprice));
                        totalproductvalue = totalproductvalue + (parseInt(quat) * pv);
                        totalsellingprice = totalsellingprice + parseFloat(sellingprice);
                    }
                }
                else {
                    quat = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtQuantity").value);
                    sellingprice = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtSellingPrice").value);
                    pv = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtProductValue").value);
                    if (!isNaN(quat) && quat != 0 && sellingprice != 0 && !isNaN(sellingprice)) {
                        totalgrossvalue = totalgrossvalue + (parseInt(quat) * parseFloat(sellingprice));
                        totalproductvalue = totalproductvalue + (parseInt(quat) * pv);
                        totalsellingprice = totalsellingprice + parseFloat(sellingprice);
                    }
                }
            } //for loop
            document.getElementById("ctl00_ContentPlaceHolder1_txttotalgrossvalue").value = totalproductvalue.toFixed(2);
            //document.getElementById("ctl00_ContentPlaceHolder1_txtTotalSellingPrice").value = totalsellingprice.toFixed(2);

            var disval = totalproductvalue - totalsellingprice;
            disval = (disval / totalproductvalue) * 100;
            totalnetvalue = totalsellingprice;
            if (!isNaN(disval)) {
                //document.getElementById("ctl00_ContentPlaceHolder1_txtDiscount").value = disval.toFixed(2);
            }
            else {
                //document.getElementById("ctl00_ContentPlaceHolder1_txtDiscount").value = 0;
            }
            var paidamount1 = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_hdpaidamount").value);
            if (isNaN(paidamount1)) {
                paidamount1 = 0;
            }
            document.getElementById("ctl00_ContentPlaceHolder1_txtNetValue").value = (totalnetvalue + gv);
            document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = totalnetvalue - paidamount1 + gv - ap; //"0.00";

        }
        function mult(aa) {
            if (aa.value != "") {
                if (checkDiscount()) {
                    var totalgrossvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txttotalgrossvalue").value);
                    var discount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtDiscount").value);
                    var discountval = (totalgrossvalue * discount) / 100;
                    var netvalue = totalgrossvalue - discountval;
                    var paidamount1 = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_hdpaidamount").value);
                    var amountpaid = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtPaidAmount").value);
                    var advanpaidamount = document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount");
                    if ((parseInt(netvalue)) < (parseInt(advanpaidamount.value))) {
                        advanpaidamount.value = netvalue;
                    }
                    if (isNaN(paidamount1) || paidamount1 == "") {
                        paidamount1 = 0;
                        //advanpaidamount.value = "0";
                    }
                    if (!isNaN(amountpaid)) {
                        //document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = parseFloat(netvalue - amountpaid).toFixed(2);
                        //document.getElementById("ctl00_ContentPlaceHolder1_hdBalance").value = parseFloat(netvalue - amountpaid).toFixed(2);
                    }
                    else {
                        //document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = netvalue.toFixed(2);
                    }
                    if (!isNaN(netvalue)) {
                        document.getElementById("ctl00_ContentPlaceHolder1_txtNetValue").value = netvalue.toFixed(2);
                        //document.getElementById("ctl00_ContentPlaceHolder1_txtPay").value = netvalue - paidamount1;
                        // document.getElementById("ctl00_ContentPlaceHolder1_txtPaidAmount").value = netvalue - paidamount1;
                    }
                    else {
                        document.getElementById("ctl00_ContentPlaceHolder1_txtNetValue").value = totalgrossvalue;
                    }

                    //  document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = document.getElementById("ctl00_ContentPlaceHolder1_txtNetValue").value;
                }
            }
            else {
                aa.value = "0.00";
                calculateTotal();
            }
            calculateDiscount();

            if ((!(isNaN(parseFloat(advanpaidamount.value)))) || (advanpaidamount.value != "")) {

                CalculateFinalTotal(advanpaidamount);
            }
            else {

                document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount").value = "0";
                CalculateFinalTotal(advanpaidamount);
            }

            //document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount").value;
            //document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount").value = "0";
        }

        function Numerics(aa) {
            if (isNaN(aa.value)) {
                aa.value = 0;
                alert("Pleas Enter Numeric value.");
                return false;
            }
        }
        function numeric(aa) {
            if (isNaN(aa.value)) {
                aa.value = "";
                alert("please Enter Numeric valu.");
                return false;
            }
            else {

            }

            return true;
        }
        function check(aa) {
            if (checkDecimals(aa, aa.value) == false) {
                //                aa.value = "";
                //              
                //                alert("Enter valid numeric value upto two decimals.");
                return false;
            }
        }
        function calculateorder(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            if (isNaN(aa.value) || aa.value == "") {
                aa.value = "";
                document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + id + "_txtTotal").value = "";
                //alert("Please Enter valid quantity value.");
                calculateorderTotal();
                return false;
            }
            else {

                var total;
                var price = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + id + "_txtPrice").value);
                if (isNaN(price)) {
                    alert("please enter price.");
                    aa.value = "";
                    calculateorderTotal();
                    return false;

                }
                else {
                    total = parseFloat(aa.value) * price.toFixed(2);
                    document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + id + "_txtTotal").value = total;
                    calculateorderTotal();
                }
            }

        }
        function calculateorderTotal() {
            var grid = document.getElementById('<%=gvOrderLense.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvOrderLense.ClientID%>').rows.length;
            var grosstotal = 0;
            var quat, sellingprice;
            var j = 0;
            for (var i = 2; i < gridrowcount; i++) {
                j = i;
                if (i < 10) {
                    var txttotal = document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl0" + i + "_txtTotal");
                    if (txttotal.value != "") {
                        grosstotal += parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl0" + i + "_txtTotal").value);
                    }

                }
                else {
                    if (document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl0" + i + "_txtTotal").value != "") {
                        grosstotal += parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + i + "_txtTotal").value);
                    }
                }
            } //for loop
            if (gridrowcount < 10)
                document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl0" + gridrowcount + "_lblOrderLenseTotal").value = grosstotal;
            else
                document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + gridrowcount + "_lblOrderLenseTotal").value = grosstotal;
        }
        function checkorderlense() {
            var grid = document.getElementById('<%=gvOrderLense.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvOrderLense.ClientID%>').rows.length;
            var orderlenseid, category, order, price, quantity;
            var j = 0;
            for (var i = 2; i < gridrowcount; i++) {
                j = i;
                if (i < 10) {
                    orderlenseid = document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl0" + i + "_hdOrderLenseID").value;
                    category = document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl0" + i + "_txtCategory").value;
                    order = document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl0" + i + "_txtOrderLense").value;
                    price = document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl0" + i + "_txtPrice").value;
                    quantity = document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl0" + i + "_txtQuantity").value;
                    if (orderlenseid == "0") {
                        if (category != "" && order != "") {
                            if (price == "") {
                                alert("Please Enter Price in row " + (i - 1) + "");
                                document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl0" + i + "_txtPrice").value = "";
                                return false;
                            }
                            if (quantity == "") {
                                alert("Please Enter Quantity in row " + (i - 1) + "");
                                document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl0" + i + "_txtQuantity").value = ""
                                return false;
                            }
                        }
                    }
                }
                else {
                    orderlenseid = document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + i + "_hdOrderLenseID").value;
                    category = document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + i + "_txtCategory").value;
                    order = document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + i + "_txtOrderLense").value;
                    price = document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + i + "_txtPrice").value;
                    quantity = document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + i + "_txtQuantity").value;
                    if (orderlenseid == "0") {
                        if (category != "" && order != "") {
                            if (price == "") {
                                alert("Please Enter Price in row" + (i - 1) + "");
                                document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + i + "_txtPrice").value = "";
                                return false;
                            }
                            if (quantity == "") {
                                alert("Please Enter Quantity in row" + (i - 1) + "");
                                document.getElementById("ctl00_ContentPlaceHolder1_gvOrderLense_ctl" + i + "_txtQuantity").value = ""
                                return false;
                            }
                        }
                    }
                }
            }
        }
        function calculateDiscount() {
            var grid = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails");
            var len = grid.rows.length;
            var discount;
            for (var i = 2; i < len; i++) {
                var category, brand, product, salesdetailid, sp;
                if (i < 10) {
                    salesdetailid = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_HDSalesDetailID").value);
                    discount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtDiscount").value);
                    if (salesdetailid == 0 || (isNaN(salesdetailid))) {
                        category = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_ddlCategory").value;
                        brand = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtBrand").value;
                        product = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtProduct").value;
                        if (category != "0" && brand != "" && product != "") {
                            if (!isNaN(discount)) {
                                sp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtSellingPrice").value);

                                var dv = sp - ((sp * discount) / 100);
                                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_hiddensellingprice").value = dv.toFixed(2);

                            }
                            else {
                                sp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtSellingPrice").value);
                                discount = 0;
                                var dv = sp - ((sp * discount) / 100);
                                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_hiddensellingprice").value = dv.toFixed(2);
                            }
                        } //1st if
                    } //2nd if
                } //outer if
                else {
                    salesdetailid = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_HDSalesDetailID").value);
                    discount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtDiscount").value);
                    if (salesdetailid == 0 || (isNaN(salesdetailid))) {
                        category = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_ddlCategory").value;
                        brand = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtBrand").value;
                        product = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtProduct").value;
                        if (category != "0" && brand != "" && product != "") {
                            if (!isNaN(discount)) {
                                sp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtSellingPrice").value);

                                var dv = sp - ((sp * discount) / 100);
                                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txthiddensellingprice").value = dv.toFixed(2);

                            }
                            else {
                                sp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtSellingPrice").value);
                                discount = 0;
                                var dv = sp - ((sp * discount) / 100);
                                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txthiddensellingprice").value = dv.toFixed(2);
                            }
                        } //1st if
                    }
                }
            }//for loop
        }
        function Numerics1(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            var qty = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtQuantity").value;
            var QtyCtrl = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtQuantity");
            var advanpaidamount = document.getElementById("txtadvancepaidamount");
            var txtbalance = document.getElementById("txtBalance");
            //            if (isNaN(advanpaidamount.value)) {
            //                advanpaidamount.value = 0;
            //            }
            //            else {
            //                
            //            }
            if (isNaN(aa.value)) {
                aa.value = "";
                //alert("Pleas Enter Numeric value.");
                return false;
            }
            if (aa.value != "") {
                if (!checkDecimals(QtyCtrl, qty)) {
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtQuantity").value = "";
                    //                    document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscount").value = "";
                    //                    document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl"+id+"_txtDiscountvalue").value = "";
                    //alert("Please Enter Numeric Positive Integer value.");
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtSellingPrice").value = "0.00";
                    calculateTotal();
                    return;
                }


                var productvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtProductValue").value);
                if (!isNaN(productvalue)) {
                    var discount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscount").value);
                    if (!isNaN(discount)) {
                        if (discount == 0) {
                            document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtSellingPrice").value = parseFloat(parseInt(qty) * productvalue).toFixed(2);
                            document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_hiddensellingprice").value = parseFloat(parseInt(qty) * productvalue).toFixed(2);
                        }
                        else {
                            document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscountvalue").value = parseFloat(parseInt(qty) * productvalue * discount / 100).toFixed(2);
                            document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_hiddensellingprice").value = parseFloat(parseInt(qty) * productvalue).toFixed(2);
                            document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtSellingPrice").value = parseFloat(parseInt(qty) * productvalue) - parseFloat(parseInt(qty) * productvalue * discount / 100).toFixed(2);
                        }
                    }
                }
                else {
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtSellingPrice").value = 0;
                }
                calculateTotal();
                var advpaidamount = document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount");
                if (!(isNaN(parseFloat(advpaidamount.value))) || advpaidamount.value != "") {
                    CalculateFinalTotal(advpaidamount);
                }
                else {
                    document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount").value = "0";
                    CalculateFinalTotal(advpaidamount);
                }
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_hiddensellingprice").value = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtProductValue").value;
                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtSellingPrice").value = "";
                calculateTotal();
            }
        }
        </script>
      

    <script type="text/javascript">
        function CalculateBalance(aa) {
            if (!isNaN(aa.value) && aa.value != "") {
                var id = aa.id.split('_')[3].split("l")[1];
                var tempid = parseInt(id);
                var mainbalance = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtPopupBalance").value);
                var hdinvoiceid = document.getElementById("ctl00_ContentPlaceHolder1_gvInvoicePayment_ctl" + id + "_HDInvoicePaymentID").value;
                var paidamount = parseFloat(aa.value);
                if (paidamount > mainbalance) {
                    alert("Pay amount cannot be greater than Balance.");
                    aa.value = "0.00";
                    return false;
                }
                if (id == "02") {
                    document.getElementById("ctl00_ContentPlaceHolder1_gvInvoicePayment_ctl" + id + "_txtBalance").value = parseFloat(mainbalance - paidamount).toFixed(2);
                }
                else {
                    var bal;
                    if (tempid < 10) {
                        bal = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvInvoicePayment_ctl0" + (tempid - 1) + "_txtBalance").value);
                        document.getElementById("ctl00_ContentPlaceHolder1_gvInvoicePayment_ctl" + id + "_txtBalance").value = parseFloat(bal - paidamount).toFixed(2);
                    }
                    else {
                        bal = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvInvoicePayment_ctl0" + (tempid - 1) + "_txtBalance").value);
                        document.getElementById("ctl00_ContentPlaceHolder1_gvInvoicePayment_ctl" + id + "_txtBalance").value = parseFloat(bal - paidamount).toFixed(2);
                    }

                } //outer else

            }
            else if (aa.value == "") {
                var id = aa.id.split('_')[3].split("l")[1];
                aa.value = "0.00";
                var tempid = parseInt(id);
                document.getElementById("ctl00_ContentPlaceHolder1_gvInvoicePayment_ctl" + id + "_txtBalance").value = "0.00";

            }

        }
        function NumericP(aa) {
            if (isNaN(aa.value)) {
                aa.value = "";
                alert("Pleas Enter Numeric value.");
                return false;
            }
        }
        function CheckUser() {
            var salesman = document.getElementById("ctl00_ContentPlaceHolder1_ddlSalesMan").value;
            var paidamount = document.getElementById("ctl00_ContentPlaceHolder1_txtPaidAmount").value;
            var customername = document.getElementById("ctl00_ContentPlaceHolder1_txtCustomerName").value;
            if (customername == "") {
                alert("Please Enter Customer Name.");
                return false;
            }
            //            if (paidamount == "") {
            //                alert("Please Make the payment.");
            //            }
            var grid = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails");
            var len = grid.rows.length;
            var quantity, category, brand, product, sellingprice, str, salesdetailsID, discount, totalsp;
            for (var i = 2; i < len; i++) {
                if (i < 10) {
                    salesdetailsID = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_HDSalesDetailID").value;
                    quantity = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtQuantity").value;
                    category = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_ddlCategory").value;
                    brand = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtBrand").value;
                    product = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtProduct").value;
                    sellingprice = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtProductValue").value;
                    discount = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtDiscount").value;
                    totalsp = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtSellingPrice").value;
                    if (category != "0" && brand != "" && product != "") {
                        if (quantity == "" || quantity == "0") {
                            alert("Please Enter Quantity");
                            return false;
                        }
                        if (sellingprice == "0.00") {
                            // alert("Selling price cannot be 0.");
                            // return false;
                        }
                        if (discount == "") {
                            alert("please enter discount.");
                            return false;
                            // document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtDiscount").value = 0;
                        }
                        if (totalsp == "") {
                            alert("please enter value in total selling price.");
                            return false;
                        }
                    }//outer if
                } // i if
                else {
                    salesdetailsID = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_HDSalesDetailID").value;
                    quantity = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtQuantity").value;
                    category = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_ddlCategory").value;
                    brand = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtBrand").value;
                    product = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtProduct").value;
                    sellingprice = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtProductValue").value;
                    discount = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtDiscount").value;
                    totalsp = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtSellingPrice").value;
                    //                    if (category == "0" && brand == "" && product == "" && salesdetailsID == 0) {


                    //                        alert("Please Fill Required Fields");
                    //                        return false;



                    //                    } //  if
                    if (category != "0" && brand != "" && product != "") {
                        if (quantity == "" || quantity == "0") {
                            alert("Please Enter Quantity");
                            return false;
                        }
                        if (sellingprice == "0.00") {
                            //alert("Selling price cannot be 0.");
                            // return false;
                        }
                        if (discount == "") {
                            alert("please enter discount.");
                            //document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtDiscount").value = 0;
                            return false;
                        }
                        if (totalsp == "") {
                            alert("please enter value in total selling price.");
                            return false;
                        }
                    } //outer if
                } //else

            } //for loop
            return true;
        }
        function checkDiscount() {

            var grid = document.getElementById('<%=gvSaleDetails.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvSaleDetails.ClientID%>').rows.length;
            var netvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtNetValue").value);
            var discountpercentage = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtDiscount").value);
            var totaldiscount = 0, totalproductvalue = 0;
            var disountvalue, productvalue, qty, salesdetailid;
            for (var i = 2; i <= gridrowcount; i++) {
                if (i < 10) {
                    salesdetailid = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_HDSalesDetailID").value);
                    // if (salesdetailid == 0) {
                    productvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtProductValue").value);
                    disountvalue = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_HDProductDiscount").value);
                    qty = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_txtQuantity").value);
                    if (productvalue != 0) {
                        productvalue = (productvalue * qty);
                        //totalgrossvalue += productvalue;
                        productvalue = productvalue - (productvalue * disountvalue) / 100;
                        totalproductvalue += productvalue;

                    }
                    // }

                }
                else {
                    salesdetailid = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_HDSalesDetailID").value);
                    // if (salesdetailid == 0) {
                    productvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "txtProductValue").value);
                    disountvalue = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_HDProductDiscount").value);
                    qty = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_txtQuantity").value);
                    if (productvalue != 0) {
                        productvalue = (productvalue * qty);
                        totalgrossvalue += productvalue;
                        productvalue = productvalue - (productvalue * disountvalue) / 100;
                        totalproductvalue += productvalue;

                    }
                    // }
                }
            }
            //netvalue = parseFloat(totalproductvalue - (totalproductvalue * discountpercentage) / 100).toFixed(2);
            if (!isNaN(discountpercentage)) {
                var totalgrossvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txttotalgrossvalue").value);
                totaldiscount = totalgrossvalue - (totalgrossvalue * discountpercentage) / 100;
                if (totaldiscount < totalproductvalue) {
                    var sp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtTotalSellingPrice").value);
                    sp = (sp * 100) / totalgrossvalue;
                    sp = 100 - sp;

                    document.getElementById("ctl00_ContentPlaceHolder1_txtDiscount").value = sp.toFixed(2);
                    //document.getElementById("ctl00_ContentPlaceHolder1_txtNetValue").value = document.getElementById("ctl00_ContentPlaceHolder1_txtTotalSellingPrice").value;
                    var netvalue = totalgrossvalue - (sp * totalgrossvalue) / 100;
                    document.getElementById("ctl00_ContentPlaceHolder1_txtNetValue").value = netvalue;
                    var paidamount1 = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_hdpaidamount").value);
                    if (isNaN(paidamount1)) {
                        paidamount1 = 0;
                    }
                    document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount").value = netvalue - paidamount1;
                    document.getElementById("ctl00_ContentPlaceHolder1_txtPaidAmount").value = netvalue - paidamount1;

                    //                    var paidamount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtPaidAmount").value);
                    //                    if (paidamount == 0) {
                    //                        document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = netvalue;
                    //                        document.getElementById("ctl00_ContentPlaceHolder1_hdBalance").value = netvalue;
                    //                    }
                    //                    else {
                    //                        document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = netvalue - paidamount;
                    //                        document.getElementById("ctl00_ContentPlaceHolder1_hdBalance").value = netvalue - paidamount;
                    //                    }
                    alert("Discount cannot be less than alloted value.");
                    return false;
                }
                else {
                    //                     paidamount1 = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_hdpaidamount").value);
                    //                    
                    //                    if (isNaN(paidamount1)) {
                    //                        paidamount1 = 0;
                    //                        //paidamount1 = document.getElementById("ctl00_ContentPlaceHolder1_txtNetValue").value;
                    //                    }
                    //                    document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount").value = netvalue;
                    //document.getElementById("ctl00_ContentPlaceHolder1_txtPaidAmount").value = netvalue - paidamount1;
                    return true;
                }
            }
            else
                return false;
        }
        function checkpescription(aa) {
            //On 18-Feb-2015

            var balance = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value);
            if (balance != 0) {
                document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount").value = balance;
                document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = 0.0;
            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount").value = 0.0;
            }


            //            var grid = document.getElementById('<%=gvSaleDetails.ClientID%>');
            //            var gridrowcount = document.getElementById('<%=gvSaleDetails.ClientID%>').rows.length;
            //            var str = "lenses";
            //            str = str.toUpperCase();
            //            var str1 = "Lense";
            //            str1 = str1.toUpperCase();
            //            var count = 0;
            //            for (var i = 2; i <= gridrowcount; i++) {
            //                if (i < 10) {
            //                    aa = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl0" + i + "_ddlCategory");
            //                }
            //                else {
            //                    aa = document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + i + "_ddlCategory");
            //                }
            //                if (aa.selectedOptions.item().text.toUpperCase() == str || aa.selectedOptions.item().text.toUpperCase() == str1) {

            //                    $("#<%=btnPrescription.ClientID%>").show();
            //                    count++;
            //                    break;
            //                }

            //            }
            //            if (count == 0) {
            //                alert("No Category of type Lense has been Selected.");
            //                $("#modalOverlay").remove();
            //                return false;
            //            }
            //            $("body").append('<div id="modalOverlay" class="modalOverlay">');
            //            return true;

        }

        function PrintPayment(SaleID) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{Saleid:'" + SaleID + "'}",
                dataType: "json",
                url: "Sales.aspx/PrintPayment",
                success: function (data) {
                    if (data.d.length != 0) {
                        var sale = data.d.eSales;
                        var salelist = data.d.eSaleslist;
                        if (salelist.length != 0) {
                            $("#divPaymentPrint").show();
                            $("#lblPaymentCustName").html(sale[0].CustomerName);
                            $("#lblPaymentCustNum").html(sale[0].CustomerNo);
                            $("#lblInvoiceNum").html(sale[0].InvoiceNo);
                            $("#lblPaymentStoreName").html(sale[0].StoreName);
                            $("#lblAmount").html(sale[0].NetTotal);
                            $("#lblBalance").html(sale[0].Balance);
                            $("#lblPaymentCountry").html(sale[0].Country);
                            $("#lblPaymentStoreCity").html(sale[0].City);
                            $("#lblPaymentContacNum").html(sale[0].ContactNum);
                            $("#lblPaymentEmail").html(sale[0].Email);
                            $("#lblPaymentInvoiceDate").html(sale[0].InvoiceDate);
                            $("#lblPaymentStoreAddress").html(sale[0].Address);
                            $("#tblPaymentPrint > tbody").empty();
                            for (var i = 0; i < salelist.length; i++) {
                                $("#tblPaymentPrint > tbody").append("<tr><td>" + salelist[i].PaymentDate + "</td><td>" + salelist[i].PaymentMode + "</td><td>" +
                        salelist[i].PaymentAmount + "</td><td>" + salelist[i].PaymentReceiptNum + "</td><td>" + salelist[i].IPBalance + "</td></tr>");
                            }

                        }
                    }
                    else {
                        $("#divPaymentPrint").hide();
                    }

                },
                error: function (result)
                { }
            });
        }
        function DivPaymentprint() {
            $("#btnPaymentPrint").hide();
            $("#<%=btnPaymentCancel.ClientID%>").hide();
            var printarea = $("#divPaymentPrint").html();
            var w = window.open();
            $("#tblPaymentPrint th").css("background-color", "#ccc");
            $("#tblPaymentPrint tr:even").css("background-color", "White");
            $("#tblPaymentPrint tr:odd").css("background-color", "#f2f2f2");

            w.document.writeln(printarea);
            w.print();
            $("#btnPaymentPrint").show();
            $("#<%=btnPaymentCancel.ClientID%>").show();

        }

    </script>

    <script type="text/javascript">
        function CalculateFinalTotal(aa) {
            var pay = parseFloat(aa.value);
            var paidamount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtPaidAmount").value);
            var balance = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value);
            var hdbalance = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_hdBalance").value);
            var paidamount1 = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_hdpaidamount").value);
            var netvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtNetValue").value);
            if (!isNaN(pay)) {
                // pay = "0";
                //                 balance = hdbalance - pay;
                //                 paidamount = pay;
                if (isNaN(paidamount1))
                    paidamount1 = 0;
                if (pay > (netvalue - paidamount1)) {
                    alert("Payment value cannot be greater than net value.");
                    document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = netvalue - paidamount1;
                    aa.value = netvalue - paidamount1;
                    document.getElementById("ctl00_ContentPlaceHolder1_txtadvancepaidamount").value = "0.00";
                    return false;
                }
                else {
                    if (isNaN(paidamount1))
                        paidamount1 = 0;
                    document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = netvalue - paidamount1 - pay;
                    document.getElementById("ctl00_ContentPlaceHolder1_txtPaidAmount").value = pay;
                }
                //aa.value = netvalue - paidamount1 - pay;
            }
            else {
                if (isNaN(paidamount1))
                    paidamount1 = 0;
                aa.value = "";
                // aa.value = netvalue - paidamount1;
                // document.getElementById("ctl00_ContentPlaceHolder1_txtPaidAmount").value = netvalue - paidamount1;
                document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = netvalue - paidamount1;

            }
            //            var paidamount = parseFloat(aa.value);
            //            if (!isNaN(aa.value) && aa.value != "") {
            //                var balanceamount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_hdBalance").value);
            //                balanceamount = balanceamount - paidamount;
            //                if (balanceamount < 0) {
            //                    alert("paid amount cannot be greater than balance");
            //                    aa.value = "";
            //                    document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = document.getElementById("ctl00_ContentPlaceHolder1_hdBalance").value;
            //                    //CalculateBalance();
            //                    return false;
            //                }
            //                else {
            //                    document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = balanceamount.toFixed(2);
            //                }
            //            }
            //            else {
            //                aa.value = "";
            //                document.getElementById("ctl00_ContentPlaceHolder1_txtBalance").value = document.getElementById("ctl00_ContentPlaceHolder1_hdBalance").value;
            //            }
        }
        function checkNumerics(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            if (isNaN(aa.value) || aa.value == "") {
                aa.value = "";
                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscountvalue").value = "";
                var sp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtProductValue").value);
                var qty = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtQuantity").value);
                document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtSellingPrice").value = (sp * qty);
                calculateTotal();
                return false;

            }
            else {
                var sp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtProductValue").value);
                var qty = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtQuantity").value);
                var discount = parseFloat(aa.value);
                var alloteddiscount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_HDProductDiscount").value);
                if (discount > alloteddiscount) {
                    alert("Dicount cannot be greater than alloted discount.");
                    aa.value = alloteddiscount;
                    var discountvalue = (sp * qty * alloteddiscount) / 100;
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscountvalue").value = discountvalue;
                    document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtSellingPrice").value = (sp * qty) - discountvalue.toFixed(2);
                    calculateTotal();
                    return;
                }
                else {
                    if (!isNaN(sp) && !isNaN(qty) && !isNaN(discount)) {
                        var discountvalue = (sp * qty * discount) / 100;
                        document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscountvalue").value = discountvalue;
                        document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtSellingPrice").value = (sp * qty) - discountvalue.toFixed(2);
                    }
                    else {
                        aa.value = "";
                        document.getElementById("ctl00_ContentPlaceHolder1_gvSaleDetails_ctl" + id + "_txtDiscountvalue").value = "";
                    }
                }
            }
            calculateTotal();
        }

        function CheckFields() {
            var Store = document.getElementById("ctl00_ContentPlaceHolder1_ddlStore").value;
            var Customer = document.getElementById("ctl00_ContentPlaceHolder1_txtCustomerName").value;
            if (Store != undefined && Customer != undefined && Customer != "" && Store != "0") {
                document.getElementById("ctl00_ContentPlaceHolder1_btnSave").disabled = true;
            }
        }
    </script>


    <script type="text/javascript">
        function applystyle() {

            $("body").append('<div id="modalOverlay"  class="modalOverlay">');
            return true;
        }
        function removestyle() {
            $("#modalOverlay").remove();
            return true;
        }
    </script>

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
                    <asp:PostBackTrigger ControlID="imgSearch" />
                    <asp:PostBackTrigger ControlID="btncancel1" />
                     <asp:PostBackTrigger ControlID="btncancelgrid" />
                                          
                </Triggers>
                <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Sales</strong>
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
                                                <label class="col-md-3 control-label">
                                                    Store</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlSearchStore" AutoPostBack="true"
                                                        runat="server" class="form-control select">
                                                        <%--  <asp:ListItem Value="">Select</asp:ListItem>--%>
                                                    </asp:DropDownList>
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
                                                        <asp:TextBox ID="txtSearchCustomerName" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Mobile No</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtSearchCustomerNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row top">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Invoice No</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtsearchinvoiceNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    Serial No</label>
                                                <div class="col-md-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                        <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                    <label class="help-block">
                                                    </label>
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
                                   <div id="pnlSearchGrid" runat="server">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvSales" runat="server"
                                            DataKeyNames="SaleID" AutoGenerateColumns="false" Style="background-color: none;"
                                            OnRowCommand="gvSales_RowCommand" OnRowCreated="gvSales_RowCreated"
                                            OnRowDataBound="gvSales_RowDataBound" OnRowDeleting="gvSales_RowDeleting"
                                            OnRowEditing="gvSales_RowEditing" OnRowUpdating="gvSales_RowUpdating"
                                            CssClass="table datatable  table-bordered table-striped table-actions"
                                            ShowFooter="true">
                                            <Columns>
                                                <%-- <asp:BoundField DataField="StoreName" HeaderText="Store Name"
                                                    ItemStyle-CssClass="middle" ItemStyle-Width="30"></asp:BoundField>--%>
                                                <asp:TemplateField HeaderText="Store Name" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStoreName" runat="server" Text='<%#Bind("StoreName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltotal" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No"
                                                    ItemStyle-CssClass="middle" ItemStyle-Width="30"></asp:BoundField>
                                                <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date"
                                                    ItemStyle-CssClass="middle" ItemStyle-Width="30"></asp:BoundField>
                                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name"
                                                    ItemStyle-CssClass="middle" ItemStyle-Width="30"></asp:BoundField>
                                                <asp:BoundField DataField="CustomerNo" HeaderText="Customer No"
                                                    ItemStyle-CssClass="middle" ItemStyle-Width="30"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Invoice Amount" ItemStyle-Width="30" ItemStyle-CssClass="middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvoiceamount" runat="server" Text='<%#Bind("NetTotal") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtinvoiceamount" runat="server" Text='<%#Bind("NetTotal") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="ftlbltotalinvoiceamount" runat="server" Font-Bold="true"
                                                            Style="float: right"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paid Amount" ItemStyle-Width="30" ItemStyle-CssClass="middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltotalpaidamount" runat="server"
                                                            Text='<%#Bind("PaymentAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txttotalpaidamount" runat="server"
                                                            Text='<%#Bind("PaymentAmount") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="ftlbltotalpaidamount" runat="server" Font-Bold="true"
                                                            Style="float: right"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance Amount" ItemStyle-Width="30" ItemStyle-CssClass="middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltotalbalance" runat="server" Text='<%#Bind("Balance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txttotalbalance" runat="server" Text='<%#Bind("Balance") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="ftlbltotalbalance" runat="server" Font-Bold="true"
                                                            Style="float: right"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="90">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnView" runat="server"
                                                            CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="View"
                                                            ToolTip="View" Style="text-decoration: none;" CssClass="fav fa-eye"
                                                            ValidationGroup="False" />
                                                        <asp:LinkButton ID="lnkbtnEdit" runat="server"
                                                            CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Editing"
                                                            ToolTip="Edit" Style="text-decoration: none;" CssClass="fae fa-pencil"
                                                            ValidationGroup="False" />
                                                        <asp:LinkButton ID="lnkbtnDel" runat="server"
                                                            CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Delete"
                                                            OnClientClick="return confirm('Do you want to Delete the record?');"
                                                            ToolTip="Delete" ValidationGroup="False"><span class="fad fa-times"> </span>
                                                        </asp:LinkButton>
                                                        <asp:ImageButton ID="lnkbtnPrint" runat="server"
                                                            CommandArgument="<%#((GridViewRow)Container).RowIndex %>" CommandName="Print"
                                                            ImageUrl="~/images/print.jpg" Style="height: 20px; width: 20px" Text="Print"
                                                            ToolTip="Print" ValidationGroup="False" />
                                                    </ItemTemplate>
                                                    <%--  <HeaderStyle />--%>
                                                </asp:TemplateField>
                                            </Columns>
                                            <%-- <RowStyle BackColor="#F3F3F3" ForeColor="Black" Height="20px"
                                                HorizontalAlign="Center" />
                                            <AlternatingRowStyle BackColor="#ffffff" ForeColor="Black" Height="20px"
                                                HorizontalAlign="Center" />
                                            <HeaderStyle Height="25px" HorizontalAlign="Center" />--%>
                                        </asp:GridView>
                                    </div>
                                    </div>
                                </div>
                            </div>
                            <div id="pnlAdd" runat="server" class="page-content-wrap">
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Store</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlStore" runat="server"
                                                    class="form-control select"
                                                    Style="margin-bottom: 12px;">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="HDSaleID" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfvOrganisation" runat="server" ControlToValidate="ddlStore"
                                                    InitialValue="0" ValidationGroup="r" ErrorMessage="Please Select Store" Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row ">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Customer Name</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtCustomerName" runat="server" onkeyup="datevalidation(this);" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCustomerName"
                                                    ErrorMessage="Please Enter Customer Name" ValidationGroup="r" Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row ">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Mobile No</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtCustomerNo" runat="server" Enabled="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Invoice No</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtInvoiceNo" runat="server" onkeyup="datevalidation(this);"
                                                        Enabled="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Invoice Date</label>
                                            <div class="col-md-8">
                                                <div class="input-group pull-right">
                                                    <asp:TextBox ID="txtInvoiceDate" onkeyup="datevalidation(this);"
                                                        CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row top">
                                    <div class="form-group">
                                        <div class="col-md-2" id="btn1" runat="server">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-block pull-right" 
                                                Text="Save" Style="margin-bottom: 5px;" ValidationGroup="r" OnClientClick="CheckFields();" UseSubmitBehavior="false"
                                                OnClick="btnSave_Click" />
                                        </div>
                                        <div class="col-md-2" id="dvcancel" runat="server">
                                            <asp:Button ID="btncancel1" runat="server" CssClass="btn btn-danger btn-block pull-right"
                                                Text="Cancel" Style="margin-bottom: 5px;" OnClick="btncancel1_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div id="dvSalesAdd" runat="server">
                                    <div id="dvSalesDetails" runat="server" class="table-responsive">
                                        <asp:GridView ID="gvSaleDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="SalesDetailsID"
                                            OnRowCommand="gvSaleDetails_RowCommand" OnRowDataBound="gvSaleDetails_RowDataBound"
                                            OnRowDeleting="gvSaleDetails_RowDeleting" OnRowEditing="gvSaleDetails_RowEditing"
                                            CellPadding="1" CssClass="table  table-bordered table-striped table-actions"
                                            OnRowUpdating="gvSaleDetails_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Category" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <%-- <ControlStyle Width="165px" />
                                                        <ItemStyle Width="165px" />--%>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="form-control select"
                                                            onchange="return CheckLense(this);" OnSelectedIndexChanged="ddlCardType_SelectedIndexChanged"
                                                            TabIndex="-1">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="HDSalesDetailID" runat="server" Value='<%#Eval("SalesDetailsID")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Brand" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <%--   <ItemStyle Width="160px" />--%>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="true" CssClass="form-control select"
                                                            Visible="false" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control" AutoComplete="on"
                                                            AutoPostBack="true" OnTextChanged="txtBrand_ontextchanged"
                                                            TabIndex="-1"></asp:TextBox>
                                                        <asp:ImageButton ID="imgBrand" runat="server" ImageUrl="~/images/plus.png" Style="width: 20px;
                                                            display: none;"
                                                            OnClick="imgBrand_onclick" TabIndex="-1" />
                                                        <asp:HiddenField ID="HDBrandID" runat="server" />
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="AutoCompleteAjaxRequest"
                                                            ServicePath="~/Screens/AutoComplete.asmx" MinimumPrefixLength="1" CompletionInterval="100"
                                                            EnableCaching="false" CompletionSetCount="10" TargetControlID="txtBrand" FirstRowSelected="false"
                                                            CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                            CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </asp:AutoCompleteExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Model No" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <%-- <ItemStyle Width="90px" />--%>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" CssClass="form-control select"
                                                            Visible="false" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="HDProductDiscount" runat="server" />
                                                        <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control" AutoCompleteType="Search"
                                                            AutoComplete="on" AutoPostBack="true" OnTextChanged="txtProduct_ontextchanged"></asp:TextBox>
                                                        <asp:ImageButton ID="imgProduct" runat="server" ImageUrl="~/images/plus.png" Style="width: 20px;
                                                            display: none;"
                                                            OnClick="imgProduct_onclick" />
                                                        <asp:HiddenField ID="HDProductID" runat="server" />
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="AutoCompleteProductRequest"
                                                            ServicePath="~/Screens/AutoComplete.asmx" MinimumPrefixLength="1" CompletionInterval="100"
                                                            EnableCaching="false" CompletionSetCount="10" TargetControlID="txtProduct" FirstRowSelected="false"
                                                            UseContextKey="true" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
                                                            CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </asp:AutoCompleteExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Selling Price" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <%--  <ControlStyle Width="80px" />
                                                        <ItemStyle Width="80px" />--%>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtProductValue" runat="server" Enabled="false" CssClass="form-control"
                                                            Text='<%#Eval("ProductValue") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <%--<ControlStyle Width="60px" />
                                                    <ItemStyle Width="60px" />--%>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" onkeyup="return Numerics1(this);"
                                                            Text='<%#Eval("Quantity") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Discount(%)" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <%--  <ControlStyle Width="80px" />
                                                    <ItemStyle Width="80px" />--%>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" onkeyup="return checkNumerics(this);"
                                                            Text='<%#Eval("Discount") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Discount">
                                                    <%--<ControlStyle Width="60px" />
                                                    <ItemStyle Width="60px" />--%>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDiscountvalue" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tot. Selling Price" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <%-- <ControlStyle Width="80px" />
                                                    <ItemStyle Width="80px" />--%>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSellingPrice" runat="server" class="form-control" Text='<%#Eval("SellingPrice") %>'
                                                            onkeyup="return Calculate(this);" onchange="checkdiscount(this)"></asp:TextBox>
                                                        <%--onkeyup="return Numerics(this);"--%>
                                                        <asp:HiddenField ID="hiddensellingprice" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="imgDeleteRow" ToolTip="Delete Row" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="DeleteRow" OnClientClick="return confirm('Are you sure you want to Delete this Sold Item?');"
                                                            runat="server" Style="width: 20px; height: 20px" TabIndex="-1"><span class="fad fa-times"> </span></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    &nbsp;
                                    <asp:Button ID="btnOrderLense" runat="server" Text="Order Lense"
                                        CssClass="btn btn-success" OnClick="btnOrderLense_Click" />
                                    <div>

                                        <asp:GridView ID="gvOrderLense1" Enabled="false" runat="server" class="table-responsive"
                                            AutoGenerateColumns="false" CssClass="table  table-bordered table-striped table-actions"
                                            ShowFooter="true"
                                            OnRowDataBound="gvOrderLense1_RowDataBound" Width="400px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Category" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <%--<asp:TextBox ID="txtCategory" runat="server" CssClass="TextBoxStyle" Style="width: 70px;"
                                                     Text='<%#Eval("Category")%>'></asp:TextBox>--%>
                                                        <asp:Label ID="txtCategory" runat="server" Text='<%#Eval("Category") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblorderlense" runat="server" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Lense" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <%--  <asp:TextBox ID="txtOrderLense" runat="server" CssClass="TextBoxStyle" Style="width: 70px;"
                                                     Text='<%#Eval("OrderLense")%>'></asp:TextBox>--%>
                                                        <asp:Label ID="txtOrderLense" runat="server" Text='<%#Eval("OrderLense") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <%--<asp:TextBox ID="txtPrice" runat="server" CssClass="TextBoxStyle" Style="width: 70px;" onkeyup="numeric(this);"
                                                  onchange="check(this);" OnTextChanged="txtPrice_change" AutoPostBack="true"   Text='<%#Eval("Price")%>'></asp:TextBox>--%>
                                                        <asp:Label ID="txtPrice" runat="server" Text='<%#Eval("Price") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <%-- <asp:TextBox ID="txtQuantity" runat="server" CssClass="TextBoxStyle" Style="width: 70px;"
                                                     Text='<%#Eval("Quantity")%>' onkeyup="calculateorder(this);"  ></asp:TextBox>--%> <%--onfocusout="calculateorder(this);"--%>
                                                        <asp:Label ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <%-- <asp:TextBox ID="txtTotal" runat="server" CssClass="TextBoxStyle" Style="width: 70px;" Enabled="false"
                                                     Text='<%#Eval("Total")%>'></asp:TextBox>--%>
                                                        <asp:Label ID="txtTotal" runat="server" Text='<%#Eval("Total") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblOrderLenseTotal" runat="server" Enabled="false"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>


                                        <asp:GridView ID="gvPrescription1" runat="server" AutoGenerateColumns="false" class="table-responsive"
                                            HorizontalAlign="Center" Enabled="false" CssClass="table  table-bordered table-striped table-actions"
                                            OnRowDataBound="gvPrescription1_RowDataBound" ShowFooter="true" Width="500px">
                                            <Columns>
                                                <asp:TemplateField HeaderText=" ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSection" runat="server" CssClass="Label2" Style="color: Black;"></asp:Label>
                                                    </ItemTemplate>
                                                    <%-- <FooterTemplate>
                                                <asp:Label ID="lblIPD" runat="server" Text="IPD"  Style="width: 70px;"></asp:Label>
                                            </FooterTemplate>--%>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SPH" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <%--<asp:TextBox ID="txtSPH" runat="server" CssClass="TextBoxStyle" Style="width: 100px;"
                                                    onchange="return NumericP(this);" Text='<%#Eval("SPH")%>'></asp:TextBox>--%>
                                                        <%-- <asp:DropDownList ID="ddlsph" runat="server" CssClass="DropDownClass"  Style="width: 70px;">                                                      
                                                    </asp:DropDownList>--%>
                                                        <asp:Label ID="ddlsph" runat="server" Text='<%#Eval("SPH")%>'>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>--%>
                                                    <%-- <asp:TextBox ID="txtsph" runat="server" CssClass="TextBoxStyle"  Style="width: 70px;" ></asp:TextBox>--%>
                                                    <%--<asp:Label ID="txtsph" runat="server"  Style="width: 70px;"></asp:Label>--%>
                                                    <%-- </FooterTemplate>--%>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CYL" ItemStyle-CssClass="middle" ItemStyle-Width="100">
                                                    <ItemTemplate>
                                                        <%--  <asp:TextBox ID="txtCYL" runat="server" CssClass="TextBoxStyle" Style="width: 100px;"
                                                    onchange="return NumericP(this);" Text='<%#Eval("CYL")%>'></asp:TextBox>--%>
                                                        <%--<asp:DropDownList ID="ddlcyl" runat="server" CssClass="DropDownClass"  Style="width: 70px;">                                                        
                                                    </asp:DropDownList>--%>
                                                        <asp:Label ID="ddlcyl" runat="server" Text='<%#Eval("CYL")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>
                                                <asp:TextBox ID="txtcyl" runat="server" CssClass="TextBoxStyle"  Style="width: 70px;"></asp:TextBox>
                                            </FooterTemplate>--%>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AXIS" ItemStyle-CssClass="middle">
                                                    <ItemTemplate>
                                                        <%--<asp:TextBox ID="txtAXIS" runat="server" CssClass="TextBoxStyle" Style="width: 100px;"
                                                    onchange="return NumericP(this);" Text='<%#Eval("AXIS")%>'></asp:TextBox>--%>
                                                        <%--<asp:DropDownList ID="ddlaxis" runat="server" CssClass="DropDownClass"  Style="width: 70px;">                                                        
                                                    </asp:DropDownList>--%>
                                                        <asp:Label ID="ddlaxis" runat="server" Text='<%#Eval("AXIS")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>
                                                <asp:TextBox ID="txtaxis" runat="server" CssClass="TextBoxStyle"  Style="width: 70px;"></asp:TextBox>
                                            </FooterTemplate>--%>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ADD" ItemStyle-CssClass="middle">
                                                    <ItemTemplate>
                                                        <%-- <asp:TextBox ID="txtADD" runat="server" CssClass="TextBoxStyle" Style="width: 100px;"
                                                    onchange="return NumericP(this);" Text='<%#Eval("AD")%>'></asp:TextBox>--%>
                                                        <%-- <asp:DropDownList ID="ddladd" runat="server" CssClass="DropDownClass"  Style="width: 70px;">                                                       
                                                    </asp:DropDownList>--%>
                                                        <asp:Label ID="ddladd" runat="server" Text='<%#Eval("AD")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%-- <FooterTemplate>
                                                <asp:TextBox ID="txtadd" runat="server" CssClass="TextBoxStyle"  Style="width: 70px;"></asp:TextBox>
                                            </FooterTemplate>--%>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                    <%-- <div style="height: 15px;">
                                    </div>--%>
                                    <br />
                                    <div id="SDNCalculation" runat="server" visible="false">
                                        <div class="row top">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        Total Amount</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox CssClass="form-control" ID="txttotalgrossvalue" runat="server" />
                                                        </div>
                                                        <asp:HiddenField ID="hdorderlensevalue" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        Payment</label>
                                                    <div class="col-md-8">
                                                        <asp:RadioButton ID="rbtnCash" runat="server" GroupName="grPay" Checked="true"
                                                            CssClass="Label2" /><asp:Label ID="Label1" runat="server" CssClass="Label2" Text="Cash"></asp:Label>
                                                        <asp:RadioButton ID="rbtnCard" runat="server" GroupName="grPay" CssClass="Label2" />
                                                        <asp:Label ID="Label7" runat="server" CssClass="Label2" Text="Card"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row top">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        Total Amount after Discount</label><%--<asp:Label ID="lblsd" runat="server" CssClass="Label2" Text=" Discount (%)" Visible="false">--%>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox CssClass="form-control" ID="txtNetValue" runat="server" />
                                                            <asp:TextBox class="TextBoxStyle" ID="txtDiscount" runat="server" Text="0.00" onkeyup="return mult(this);"
                                                                Visible="false"></asp:TextBox>
                                                        </div>
                                                        <asp:HiddenField ID="hdmaingridvalue" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        Advance</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox ID="txtadvancepaidamount" runat="server" CssClass="form-control" onkeyup="return CalculateFinalTotal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row ">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                    </label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <%--   <span class="input-group-addon"><span class="fa fa-pencil"></span></span>--%>
                                                            <asp:TextBox CssClass="form-control" ID="txtTotalSellingPrice" Text="0.00" runat="server"
                                                                Visible="false" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        Paid Amount</label>
                                                    <div class="col-md-8">
                                                        <%-- <div class="input-group">--%>
                                                        <%-- <span class="input-group-addon"><span class="fa fa-pencil"></span></span>--%>
                                                        <asp:Label CssClass="control-label" ID="txtPaidAmount" runat="server" />
                                                        <%--</div>--%>
                                                        <asp:HiddenField ID="hdpaidamount" runat="server" />
                                                    </div>
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
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        Balance</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox ID="txtBalance" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <asp:HiddenField ID="hdBalance" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row top">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        User</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddlUser" runat="server"
                                                            class="form-control select"
                                                            Style="margin-bottom: 12px;">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        Sales Man</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddlSalesMan" runat="server"
                                                            class="form-control select"
                                                            Style="margin-bottom: 12px;">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <br />
                                        <div class="row top">
                                            <div class="form-group">
                                                <div class="col-md-2" id="Div4" runat="server">
                                                    <asp:Button ID="imgSave" runat="server" CssClass="btn btn-success btn-block pull-right"
                                                        Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" ValidationGroup="r"
                                                        OnClick="imgSave_Click" OnClientClick="return CheckUser();this.disabled = true;" />
                                                </div>
                                                <div class="col-md-2" id="Div6" runat="server">
                                                    <asp:Button ID="btnPayment" runat="server" CssClass="btn btn-success btn-block pull-right"
                                                        Text="Paid Details" Style="margin-bottom: 5px;"
                                                        OnClick="btnPayment_Click" />
                                                </div>
                                                <div class="col-md-2" id="Div7" runat="server">
                                                    <asp:Button ID="btnPrescription" runat="server" CssClass="btn btn-success btn-block pull-right"
                                                        Text="Full Payment" Style="margin-bottom: 5px;"
                                                        OnClientClick="return checkpescription();" />
                                                </div>
                                                <div class="col-md-2" id="Divcan1" runat="server">
                                                    <asp:Button ID="btncancelgrid" runat="server" CssClass="btn btn-danger btn-block pull-left"
                                                        Text="Cancel" OnClick="btncancelgrid_Click" Style="margin-bottom: 5px;" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>


                                </div>
                                <table align="center" border="0">
                                    <tr>
                                        <td></td>
                                    </tr>
                                </table>
                                <center>
                                    <div id="dvSummary" align="center">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="r"
                                            CssClass="validationSummary" />
                                    </div>
                                </center>

                            </div>
                          
                </div>
                </form>
                </div>
            </div>
               </ContentTemplate>
            </asp:UpdatePanel>
              <div id="divprintarea" class="modal" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-lg modal-position">
                                    <div class="modal-content" id="printdiv1">
                                        <div class="modal-header">
                                            <center>
                                                <h2 class="modal-title">Invoice</h2>
                                            </center>

                                            <table width="800px" align="center" id="table1" class="table table-bordered table-striped table-actions table-responsive">
                                                <tr>
                                                    <td style="width: 30px;"></td>
                                                    <td align="left" class="Label2">

                                                        <asp:Label ID="lblStoreNamePopup" runat="server" Style="color: Black; display: none;"></asp:Label>
                                                        <br />

                                                        <label id="lblpinoviceno" class="control-label" style="color: black;">Invoice No</label>
                                                        <label id="lblprintinvoiceno" class="Label" style="margin-left: 10px;">
                                                        </label>
                                                       
                                                        <label id="Label32" class="control-label"" style="color: black;">Invoice Date</label>
                                                        <label id="lblprintinvoiceDate" class="Label">
                                                        </label>

                                                        <asp:Label ID="lblAddressPopup" runat="server" CssClass="control-label" Style="margin-top: auto;
                                                            color: Black; font-weight: normal"></asp:Label>

                                                    </td>

                                                    <td style="width: 300px;">
                                                        <table>
                                                            <tr>
                                                                <td style="width: 120px;">
                                                                    <label class="control-label" style="color: Black; float: left; margin-bottom: -5px;">
                                                                        Customer
                                                                            Name</label></td>
                                                                <td style="width: 160px;">
                                                                    <asp:Label ID="lblCustomerNamePopup" runat="server" Style="color: Black; float: left;
                                                                        margin-bottom: -5px;"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label class="Label" style="color: Black; float: left;">Contact No</label></td>
                                                                <td>
                                                                    <asp:Label ID="lblCustomerNoPopup" runat="server" CssClass="control-label" Style="color: Black;
                                                                        float: left;"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <br />
                                        <div class="modal-body">
                                            <table id="tblSalesPrint" width="750px" align="center" border="1px;" style="border-spacing: 1px;" class="table table-bordered table-striped table-actions table-responsive">
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
                                                        <th style="color: white; background-color: black;">Discount
                                                        </th>
                                                        <th style="color: white; background-color: black;">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>
                                                <tfoot>
                                                    <tr style="display: none;">
                                                        <td align="center">Details:
                                                        </td>
                                                        <td colspan="5" align="right">
                                                            <label id="Label2">
                                                                Total Amount:</label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTotalGrossValue" runat="server" CssClass="control-label" Style="margin-top: 0px;
                                                                color: Black;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="display: none;">
                                                        <td align="right" colspan="6">
                                                            <label id="Label4">
                                                                Discount (%):</label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDiscount" runat="server" CssClass="control-label" Style="margin-top: 0px;
                                                                color: Black;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">Details:
                                                        </td>
                                                        <td colspan="5" align="right">
                                                            <label id="Label5" style="float: right;">
                                                                Total Amount:</label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblNetValue" runat="server" CssClass="control-label" Style="margin-top: 0px;
                                                                color: Black;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>

                                                        <td align="right" colspan="3">
                                                            <label id="Label31" style="align: center;">Payment Mode:</label>
                                                        </td>
                                                        <td
                                                            <asp:Label ID="lblPaymentmodeup" runat="server" CssClass="control-label" Style="margin-top: 0px;
                                                                color: Black;"></asp:Label>
                                                        </td>
                                                        <td align="right" colspan="2">
                                                            <label id="LabelsavePopupAmountPaid">
                                                                Amount Paid:</label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblsavePopupAmountPaid" runat="server" CssClass="control-label" Style="margin-top: 0px;
                                                                color: Black;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbluse" runat="server" CssClass="Label2" Style="float: right; color: Black;"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLoginUser" runat="server" CssClass="control-label" Style="color: Black;"></asp:Label>
                                                        </td>

                                                        <td colspan="4" align="right">
                                                            <label id="LabelBalance">
                                                                Balance:</label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblsavePopupBal" runat="server" CssClass="control-label" Style="margin-top: 0px;
                                                                color: Black;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                        <div class="modal-footer">
                                            <input type="button" id="btnPrint" value="Print" class="BtnEmptyStyle" title="Print"
                                                onclick="printDiv()" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="BtnEmptyStyle"
                                                OnClick="btnCancel_Click" /><%--OnClientClick="return removestyle();"--%>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div id="dvPaymentDetails" style="display: none; z-index: 1040;" class="modal fade in"
                                role="dialog" tabindex="-1"
                                aria-hidden="false" runat="server">
                                <div class="modal-backdrop fade in" style="height: 100%;">
                                </div>
                                <div class="modal-dialog modal-lg ">
                                    <div class="modal-content" style="height: 600px;">
                                        <div class="modal-header">
                                            <center>
                                                <h4 class="modal-title">Payment Details</h4>
                                                <asp:Label ID="lblmsg1" runat="server"></asp:Label></center>
                                        </div>
                                        <div class="modal-body" style="height: 520px; overflow-y: auto;">
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblInvoiceNo" runat="server" CssClass="control-label" Style="color: Black;"
                                                            Text="Invoice No"></asp:Label>
                                                        <asp:TextBox ID="txtPopInvoiceNo" runat="server" CssClass="from-control" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblInvoiceDate" runat="server" CssClass="control-label" Style="color: Black;"
                                                            Text="Invoice Date"></asp:Label>
                                                        <asp:TextBox ID="txtPopInvoiceDate" runat="server" CssClass="from-control " Enabled="false"></asp:TextBox>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblAmount" runat="server" CssClass="control-label" Style="color: Black;"
                                                            Text="Amount"></asp:Label>
                                                        <asp:TextBox ID="txtPopupAmount" runat="server" CssClass="from-control" Enabled="false"> </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" CssClass="control-label" Style="color: Black;"
                                                            Text="Balance"></asp:Label>
                                                        <asp:TextBox ID="txtPopupBalance" runat="server" CssClass="from-control" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>


                                            <br />

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvInvoicePayment" runat="server" AutoGenerateColumns="false"
                                                    PagerStyle-CssClass="pgr" DataKeyNames="InvoicePaymentID" CssClass="table  table-bordered table-striped table-actions"
                                                    OnRowDataBound="gvInvoicePayment_RowDataBound">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Payment Date">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="HDInvoicePaymentID" runat="server" Value='<%#Eval("InvoicePaymentID")%>' />
                                                                <%--<asp:TextBox ID="" runat="server" Enabled="false" CssClass="TextBoxStyle"
                                                                    Text='<%#Eval("PaymentDate") %>' Style="width: 120px;" onkeyup="Javascript:return validatedate(this);"></asp:TextBox>
                                                                <asp:Image ID="imgCalPaymentDueDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                                <asp:CalendarExtender ID="CalPaymentDueDate" Format="dd-MM-yy" runat="server" TargetControlID="txtPaymentDate"
                                                                    PopupButtonID="imgCalPaymentDueDate">
                                                                </asp:CalendarExtender>--%>
                                                                <div class="input-group pull-right">
                                                                    <asp:TextBox ID="txtPaymentDate" Enabled="false" Text='<%#Eval("PaymentDate") %>'
                                                                        CssClass="form-control datepicker" runat="server" onkeyup="Javascript:return validatedate(this);"></asp:TextBox><%--onchange="CompareDate()"--%>
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Mode">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control select" OnSelectedIndexChanged="ddlPaymentMode_selectedindexchanged"
                                                                    AutoPostBack="True" Visible="false">
                                                                    <asp:ListItem Value="0">--Any--</asp:ListItem>
                                                                    <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                                                    <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                                                    <asp:ListItem Value="BankTransfer">Bank Transfer</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtPaymentMode" runat="server" CssClass="form-control" Text='<%#Eval("PaymentMode") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Amount">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hdcount1" Value="1" />
                                                                <asp:TextBox ID="txtPaymentAmount" runat="server" CssClass="form-control" onchange="return CalculateBalance(this);"
                                                                    Text='<%#Eval("PaymentAmount") %>' onblur="javascript:Numerics(this);"> </asp:TextBox>
                                                                <%-- ontextchanged="txtPaymentAmount_TextChanged" onblur="javascript:Numerics(this);"--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Receipt No"
                                                            Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRecieptNo" runat="server" CssClass="form-control" Text='<%#Eval("ReceiptNo") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Width="145px" HeaderText="Balance"
                                                            ItemStyle-Width="140px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBalance" Enabled="false" runat="server" CssClass="form-control"
                                                                    Text='<%#Eval("Balance") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <br />

                                            <table align="left" style="margin-left: 35px; margin-top: 10px;">
                                                <tr>
                                                    <td>
                                                        <label id="lblRemarks" runat="server" style="color: Black; float: left; display: none;"
                                                            class="Label2">
                                                            Remark</label>
                                                    </td>
                                                    <td style="width: 350px;">
                                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Enabled="True" CssClass="form-control datepicker"
                                                            Visible="false" Rows="8" Columns="25" Height="60" Width="400"></asp:TextBox>
                                                    </td>
                                                    <td style="margin-top: 40px;">
                                                        <asp:Button ID="btnSavePayments" runat="server" Text="Save" OnClientClick="return CheckSaving();"
                                                            CssClass="btn btn-success" OnClick="btnSavePayments_Click" Visible="false" />
                                                        <asp:Button ID="btnCancelPayments" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                                            OnClick="btnCancelPayments_Click" Visible="true" OnClientClick="return removestyle();" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div id="dvPrescription" runat="server" class="modal fade" role="dialog" aria-hidden="true"
                                style="display: none;">

                                <asp:Label ID="lblmsgprescriptin" runat="server"></asp:Label>

                                <center>
                                    <asp:Button ID="btnPrescriptionSave" runat="server" Text="Save" CssClass="btn btn-success"
                                        OnClick="btnPrescriptionSave_Click" />
                                    <asp:Button ID="btnPrescriptionCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                        OnClick="btnPrescriptionCancel_Click" OnClientClick="return removestyle();" />
                                </center>
                            </div>



                            <div id="dvOrderLense" style="display: none; z-index: 1040;" class="modal fade in"
                                role="dialog" tabindex="-1"
                                aria-hidden="false" runat="server">
                                <div class="modal-backdrop fade in" style="height: 163%;"></div>
                                <div class="modal-dialog modal-lg ">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title">Order Lense </h4>
                                        </div>


                                        <div class="modal-body" style="overflow-y: auto;">

                                            <asp:Label ID="Label27" runat="server"></asp:Label>
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvOrderLense" runat="server" AutoGenerateColumns="false" CssClass="table  table-bordered table-striped table-actions table-responsive"
                                                    ShowFooter="true" OnRowDataBound="gvOrderLense_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Category">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdOrderLenseID" runat="server" Value='<%#Eval("OrderLenseID")%>' />
                                                                <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control"
                                                                    Text='<%#Eval("Category")%>' Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="ddlCategoryOrd" runat="server" CssClass="form-control select">
                                                                    <asp:ListItem Text="--Any--" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="CR39" Value="CR39"></asp:ListItem>
                                                                    <asp:ListItem Text="Glass" Value="Glass"></asp:ListItem>
                                                                    <asp:ListItem Text="CR39PC" Value="CR39PC"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblorderlense" runat="server" Text="Total"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Brand">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOrderLense" runat="server" CssClass="form-control"
                                                                    Text='<%#Eval("OrderLense")%>'></asp:TextBox>
                                                                <%--OnTextChanged="txtPrice_change" AutoPostBack="true"--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Price">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"
                                                                    onkeyup="numeric(this);"
                                                                    onchange="check(this);" Text='<%#Eval("Price")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"
                                                                    Text='<%#Eval("Quantity")%>' AutoComplete="off" onkeyup="calculateorder(this);"></asp:TextBox>
                                                                <%--onfocusout="calculateorder(this);"--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control"
                                                                    Enabled="false"
                                                                    Text='<%#Eval("Total")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="lblOrderLenseTotal" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />

                                            <center>
                                                <b>Prescription Details</b>
                                            </center>

                                            <div class="table-responsive">
                                                <asp:GridView ID="gvPrescription" runat="server" AutoGenerateColumns="false" CssClass="table  table-bordered table-striped table-actions"
                                                    OnRowDataBound="gvPrescription_RowDataBound" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText=" ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSection" runat="server" CssClass="control-label" Style="color: Black;"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblIPD" runat="server" Text="IPD"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SPH">
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox ID="txtSPH" runat="server" CssClass="TextBoxStyle" Style="width: 100px;"
                                                    onchange="return NumericP(this);" Text='<%#Eval("SPH")%>'></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddlsph" runat="server" CssClass="form-control select">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtsph" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CYL">
                                                            <ItemTemplate>
                                                                <%--  <asp:TextBox ID="txtCYL" runat="server" CssClass="TextBoxStyle" Style="width: 100px;"
                                                    onchange="return NumericP(this);" Text='<%#Eval("CYL")%>'></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddlcyl" runat="server" CssClass="form-control select">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtcyl" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="AXIS">
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox ID="txtAXIS" runat="server" CssClass="TextBoxStyle" Style="width: 100px;"
                                                    onchange="return NumericP(this);" Text='<%#Eval("AXIS")%>'></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddlaxis" runat="server" CssClass="form-control select">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtaxis" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ADD">
                                                            <ItemTemplate>
                                                                <%-- <asp:TextBox ID="txtADD" runat="server" CssClass="TextBoxStyle" Style="width: 100px;"
                                                    onchange="return NumericP(this);" Text='<%#Eval("AD")%>'></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddladd" runat="server" CssClass="form-control select">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtadd" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <br />
                                            <asp:Button ID="btnSaveOrderLense" runat="server" Text="Save"
                                                CssClass="btn btn-success" OnClientClick="return checkorderlense();" OnClick="btnSaveOrderLense_Click" />
                                            <asp:Button ID="btnCancelOrderLense" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                                OnClick="btnCancelOrderLense_click" />

                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div id="dvBrandPopUp" runat="server" class="modal fade" role="dialog" aria-hidden="true"
                                style="display: none; height: 220px; width: 600px; overflow: auto;">
                                <div class="modal-dialog modal-lg modal-position">
                                    <div class="modal-content" id="Div9">
                                        <div class="modal-header">
                                            <center>
                                                <h2 class="modal-title">Brand</h2>
                                                <asp:Label ID="lblStatusBrand" runat="server" CssClass="MessageClass" Style="color: Red;"></asp:Label>
                                            </center>
                                        </div>
                                        <div class="modal-body">
                                            <table border="0" align="center" cellpadding="1" cellspacing="2" style="width: 400px;
                                                margin-bottom: 14px">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblAddBrand" runat="server" Text="Brand Name" CssClass="control-label"></asp:Label>
                                                        <asp:HiddenField ID="HiddenFieldBrandId" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAddBrand" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAddBrand" runat="server" ValidationGroup="rr"
                                                            ControlToValidate="txtAddBrand" Text="*" ErrorMessage="Brand Name Can not be Empty">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr style="display: none;">
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text="IsActive" CssClass="control-label"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" Style="margin-left: -170px;" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:Button ID="imgAddBrand" runat="server" ValidationGroup="rr" CssClass="btn btn-success"
                                                            OnClick="imgAddBrand_Click" Style="margin-bottom: -17px;" />
                                                        <asp:Button ID="btnCancelBrand" Text="Cancel" runat="server" CssClass="btn btn-danger"
                                                            OnClientClick="return removestyle();" OnClick="btnCancelBrand_onclick" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <center>
                                            <div id="Div1" align="center">
                                                <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="rr"
                                                    CssClass="validationSummary" />
                                            </div>
                                        </center>
                                    </div>
                                </div>
                            </div>

                            <div id="dvProductPopUp" runat="server" class="modal fade" role="dialog" aria-hidden="true"
                                style="display: none; height: 525px; overflow: auto;">
                                <div class="modal-dialog modal-lg modal-position">
                                    <div class="modal-content" id="Div10">
                                        <div class="modal-header">
                                            <center>
                                                <h2 class="modal-title">Product</h2>
                                                <asp:Label ID="lblStatusProduct" runat="server" CssClass="MessageClass" Style="color: Red;"></asp:Label>
                                            </center>
                                        </div>
                                        <div class="modal-body">
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label0" runat="server" CssClass="control-label">Category:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DDLCategory" runat="server" CssClass="form-control select" Style="margin-left: -27px;">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ValidationGroup="gr" ID="RequiredFieldValidator4" runat="server"
                                                            ControlToValidate="DDLCategory" InitialValue="0" ErrorMessage="Please Select Category"
                                                            Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server" CssClass="control-label">Brand:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control select" Style="margin-left: -27px;">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ValidationGroup="gr" ID="RequiredFieldValidator0" runat="server"
                                                            ControlToValidate="ddlBrand" InitialValue="0" ErrorMessage="Please Select Brand"
                                                            Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblProductName" runat="server" CssClass="Label">Product Name:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" Style="margin-left: -10px;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="gr" ID="RequiredFieldValidator5" runat="server"
                                                            ControlToValidate="txtProductName" InitialValue="" ErrorMessage="Please Enter Product Name"
                                                            Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label12" runat="server" CssClass="control-label">Product Value (Selling Price):</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtProductValue" runat="server" CssClass="form-control" onchange="return CheckValue(this);"
                                                            Style="margin-left: -7px;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ValidationGroup="gr" ID="RequiredFieldValidator6" runat="server"
                                                            ControlToValidate="txtProductValue" InitialValue="" ErrorMessage="Please Enter Product value"
                                                            Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr style="display: none">
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" CssClass="control-label" Visible="false">Default Discount%(Selling):</asp:Label>
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
                                                        <asp:Label ID="Label21" runat="server" CssClass="control-label">IsActive:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkActiveProduct" runat="server" Checked="true" Style="margin-left: -170px;" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <center>
                                                <div id="dvSummary1" align="center">
                                                    <asp:ValidationSummary CssClass="validationSummary" ID="ValidationSummary2" ValidationGroup="gr"
                                                        runat="server" />
                                                </div>
                                            </center>
                                        </div>
                                        <center>
                                            <asp:ImageButton ID="imgSaveProduct" ValidationGroup="gr" ImageUrl="../images/saveBtn.png"
                                                alt="Save" runat="server" OnClick="imgSaveProduct_Click" Style="height: 40px;
                                                margin-bottom: -17px;" />
                                            <%--<asp:ImageButton  ID="imgUpdateProduct" ValidationGroup="r" Visible="false" 
                        ImageUrl="~/images/updateBtn.png" alt="Update"  runat="server" onclick="imgupdate_Click" 
                         />--%>
                                            <%-- <asp:ImageButton  ID="imgClearProduct" ImageUrl="../images/clearBtn.png" alt="Clear" 
                        runat="server" onclick="imgClearProduct_Click"  />--%>
                                            <asp:Button ID="btnClearProduct" Text="Cancel" runat="server" CssClass="BtnEmptyStyle"
                                                OnClientClick="return removestyle();" OnClick="btnClearProduct_onclick" />
                                        </center>


                                    </div>
                                </div>
                            </div>


                            <div id="divPrint1" class="modal" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-lg modal-position">
                                    <div class="modal-content" id="Div5">
                                        <div class="modal-header">
                                            <center>
                                                <h2 class="modal-title">Invoice
                                                </h2>
                                            </center>

                                            <%--<div id="dvpopup" style="display:none;height:70px;"></div>--%>
                                            <br />
                                            <table width="800px" align="center" id="tableprint" class="table table-bordered table-striped table-actions">
                                                <tr>
                                                    <td style="width: 30px;"></td>

                                                    <td align="left">
                                                        <label id="Label8" class="Label" style="color: Black;">
                                                            Invoice No
                                                        </label>
                                                        <label id="lblCustomerInvoiceNO1" class="Label2" style="color: Black; margin-left: 10px;">
                                                        </label>
                                                        <br />
                                                        <label id="lblInvNoteDate" class="Label2" style="color: Black;">
                                                            Invoice Date</label>
                                                        <label id="lblInvoiceNoteDate1" class="Label2" style="color: Black;">
                                                        </label>
                                                        <br />
                                                        <label id="lblprintSupplierAddress" class="Label2" style="color: Black;">
                                                        </label>
                                                    </td>

                                                    <td style="width: 300px;">
                                                        <table>
                                                            <tr>
                                                                <td style="width: 120px;">
                                                                    <label class="Label2" style="color: Black; float: left; margin-bottom: -5px;">
                                                                        Customer
                                                                Name</label>
                                                                </td>
                                                                <td style="width: 160px;">
                                                                    <label id="lblCustomerprintName1" style="color: Black; float: left; margin-bottom: -5px;">
                                                                    </label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label class="Label" style="color: Black; float: left;">Contact No</label>
                                                                </td>
                                                                <td>
                                                                    <label id="lblprintCustomerCustomerNo1" style="color: Black; float: left;"></label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left" style="display: none;">
                                                        <label id="lblprintStoreName" class="Label2" style="color: Black;">
                                                        </label>
                                                        <br />
                                                        <label id="lblStoreprintAddress" class="Label2" style="color: Black;">
                                                        </label>
                                                        <br />
                                                        <label id="label20" class="Label2">
                                                        </label>
                                                        <label id="lblStoreprintCity" class="Label2" style="color: Black;">
                                                        </label>
                                                        <br />
                                                        <label id="lblCountry" class="Label2" style="color: Black;">
                                                            India.</label>
                                                        <br />
                                                        <label id="lblContNum" class="Label2" style="color: Black;">
                                                            Cont Num:
                                                        </label>
                                                        <label id="Label24" class="Label2" style="color: Black;">
                                                        </label>
                                                        <br />
                                                        <label id="lblEmail" class="Label2" style="color: Black;">
                                                            Email:</label><label id="Label28" class="Label2"></label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <br />
                                        <div class="modal-body">
                                            <div id="printSupplierDeliveryNote">
                                                <table width="750px" align="center" id="tblPrintSales1" border="1" style="border-spacing: 0px;">
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
                                                            <th style="color: white; background-color: black;">Discount
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
                                        <br />
                                        <div class="modal-footer">
                                            <input type="button" id="PrintBtn1" value="Print" class="BtnEmptyStyle" title="Print"
                                                onclick="Divprint()" />
                                            <asp:Button ID="CancelBtn1" runat="server" Text="Cancel" CssClass="BtnEmptyStyle" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <form name="SubOrgForm" novalidate>
                                <div class="modal" id="divPrint" tabindex="-1" role="dialog" aria-hidden="true" style="height: 100%; overflow-y: auto;">
                                    <div class="modal-dialog modal-lg modal-position" id="printdiv">
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
                                                                     
                                                                    <div class="row">                                                                       
                                                                        <div class="input-group">--%>
                                                                            <label id="Label19" class="Label">Invoice No</label>
                                                                            <label id="lblCustomerInvoiceNO" class="Label"></label>
                                                                        <%--</div>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                        <div class="row">
                                                           <%-- <div class="row">
                                                                <div class="form-group">
                                                                  
                                                                    <div class="row">
                                                                        <div class="input-group">--%>
                                                                              <label id="Label18" class="Label pull-left">Invoice Date</label>
                                                                            <label id="lblInvoiceNoteDate" class="Label"></label>
                                                                        <%--</div>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                       
                                                    </div>
                                                    <div class="col-md-6" style="width:46%;float:right;">
                                                        <div class="row " style="margin-top: 1%">
                                                            <div class="col-md-12">
                                                                <div class="form-group">                                                                    
                                                                    <div class="col-md-12">
                                                                        <div class="input-group">
                                                                             <label id="Label23" class="Label">Customer Name</label>
                                                                            <label id="lblCustomerprintName" class="Label"></label>
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
                                                                            <label id="lblprintCustomerCustomerNo" class="Label"></label>
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
                                                             <table width="750px" align="center" id="tblPrintSales" border="1" class="table table-bordered table-striped table-actions">
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
                                                                        <th style="color: white; background-color: black;">Discount
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
                                            <div class="modal-footer">
                                                <input type="button" id="PrintBtn" value="Print" class="BtnEmptyStyle" title="Print"
                                                    onclick="Divprint()" />
                                                <asp:Button ID="CancelBtn" runat="server" Text="Cancel" CssClass="BtnEmptyStyle" />
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                                </form>

                            <div id="divPaymentPrint" class="modal fade" role="dialog" aria-hidden="true" style="display: none;
                                height: 525px; overflow: auto;">
                                <div class="modal-dialog modal-lg modal-position">
                                    <div class="modal-content" id="Div8">
                                        <div class="modal-header">
                                            <center>
                                                <h2 class="modal-title">Payment Details:
                                                </h2>
                                            </center>
                                            <br />
                                            <br />
                                            <table width="800px" align="center" id="tblPayPrint">
                                                <tr>
                                                    <td style="width: 30px;"></td>
                                                    <td align="left">
                                                        <label id="lblPaymentCustName" class="Label2">
                                                        </label>
                                                        <br />
                                                        <label id="Label11" class="Label2">
                                                            Customer No:
                                                        </label>
                                                        <label id="lblPaymentCustNum" class="Label2">
                                                        </label>
                                                        <br />
                                                        <label id="Label13" class="Label2">
                                                            Invoice No:
                                                        </label>
                                                        <label id="lblInvoiceNum" class="Label2">
                                                        </label>
                                                        <br />
                                                        <label id="lable14" class="Label2">
                                                            Amount:</label>
                                                        <label id="Label15" class="Label2">
                                                        </label>
                                                        <br />
                                                        <label id="label16" class="Label2">
                                                            Balance:</label>
                                                        <label id="lblBalance" class="Label2">
                                                        </label>
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                    <td width="400px"></td>
                                                    <td align="left">
                                                        <label id="lblPaymentStoreName" class="Label2">
                                                        </label>
                                                        <br />
                                                        <label id="lblPaymentStoreAddress" class="Label2">
                                                        </label>
                                                        <br />
                                                        <label id="label17" class="Label2">
                                                        </label>
                                                        <label id="lblPaymentStoreCity" class="Label2">
                                                        </label>
                                                        <br />
                                                        <label id="lblPaymentCountry" class="Label2">
                                                            India.</label>
                                                        <br />
                                                        <label id="lblPaymentContacNum" class="Label2">
                                                            Cont Num:
                                                        </label>
                                                        <label id="Label22" class="Label2">
                                                        </label>
                                                        <br />
                                                        <label id="lblPaymentEmail" class="Label2">
                                                            Email:</label><label id="Label29" class="Label2"></label>
                                                        <br />
                                                        <label id="lblPayInvoicDate" class="Label2">
                                                            Invoice Date:</label>
                                                        <label id="lblPaymentInvoiceDate" class="Label2">
                                                        </label>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <br />
                                        <div class="modal-body">
                                            <div id="Payment" class="gridclass" style="display: block;">
                                                <table width="750px" align="center" id="tblPaymentPrint" border="1" style="border-spacing: 0px;">
                                                    <thead>
                                                        <tr>
                                                            <th>Payment Date
                                                            </th>
                                                            <th>Payment Mode
                                                            </th>
                                                            <th>Payment Amount
                                                            </th>
                                                            <th>Receipt No
                                                            </th>
                                                            <th>Balance
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
                                            <input type="button" id="btnPaymentPrint" value="Print" class="BtnEmptyStyle" title="Print"
                                                onclick="DivPaymentprint()" />
                                            <asp:Button ID="btnPaymentCancel" runat="server" Text="Cancel" CssClass="btn btn-empty" />
                                        </div>
                                    </div>
                                </div>
                            </div>
        </div>
        
    </form>
</asp:Content>
