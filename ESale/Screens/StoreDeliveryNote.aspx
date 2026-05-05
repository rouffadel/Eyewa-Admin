<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="StoreDeliveryNote.aspx.cs" Inherits="Screens_StoreDeliveryNote" Title="Customer Delivery Note" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblBanner" runat="server" Text="Store Delivery Note" Visible="false"></asp:Label>
    <%-- <asp:ToolkitScriptManager ID="tsm" runat="server">
    </asp:ToolkitScriptManager>--%>
    <style type="text/css">
        /*@media print {
    html, body {
        height: auto;    
    }
    .print:last-child {
     /*page-break-after: auto;*/
        /*   display:none;
}
}*/
        /*@media screen, print
   {
      
   }*/
        /*@media print and (width: 78.5mm) and (height: 12mm)
        {
            @page
            {
                margin: 3cm;
            }
        }*/

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
            color: #191919;
        }

        .itemHighlighted
        {
            background-color: #BDBABA;
        }
    </style>

    <style type="text/css">
        .ajax-loader
        {
            position: absolute;
            left: 50%;
            top: 50%;
            margin-left: -32px; /* -1 * image width / 2 */
            margin-top: -32px; /* -1 * image height / 2 */
            display: block;
        }
    </style>
    <style type="text/css" id="mystyle">
        .modalBackground
        {
            background-color: white;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 5px;
            border-style: solid;
            border-color: #696969;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }

        #tbltotal tr td
        {
            /*font-weight:bold;*/
            font-size: 12px;
            color: black;
        }
        /*	#tbltotal tr:nth-child(even) 
        	{
        		 background:white;
        		}
           #tbltotal tr:nth-child(odd) 
            {
            	background: #E2E1E1
	       
              }*/

        #tblremarks tr td
        {
            color: Black;
            font-weight: bold;
        }
    </style>
    <style>
        .modalOverlay
        {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0px;
            left: 0px;
            background-color: rgba(0,0,0,0.3); /* black semi-transparent */
        }
    </style>
   
  
      <script src="../js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>

     <script type="text/javascript">
         function StoreForPrint(storedeliverynoteid) {
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 data: "{Storeid:'" + storedeliverynoteid + "'}",
                 dataType: "json",
                 url: "StoreDeliveryNote.aspx/StoreForPrint",
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
                             $("#divprintarea").css("display", "block !important");
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
                         Supplierlist[i].GrossValue + "</label></td></tr>");

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
                                        "<td colspan='6'><b></b></td>" +
                                       "</tr>");
                             $("#tblStorePrint > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                        "<td align='center' colspan='6'>Details:</td>" +
                                       "</tr>");
                             $("#tblStorePrint > tfoot").append("<tr style='background-color:White;font-size:12px;font-family:verdana;'>" +
                                                              "<td align='left' colspan='6' rowspan='1'>Remarks:</td>" +

                                                                   "</tr>");

                             $("#tblStorePrint > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                                              "<td align='left' colspan='4' rowspan='5'>" + Remarks + "</td>" +

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

         function StoreForPrint1(storedeliverynotedetailid) {

             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 data: "{Storeid:'" + storedeliverynotedetailid + "'}",
                 dataType: "json",
                 url: "StoreDeliveryNote.aspx/StoreForPrint1",
                 success: function (data) {
                     if (data.d.length != 0) {

                         var Supplier = data.d.eStore;
                         var Supplierlist = data.d.eStoreList;

                         var TotalGrossValue = 0;
                         if (Supplierlist.length == 0) {
                             $("#divprintarea1").hide();
                             return false;
                         }
                         else {
                             $("body").append('<div id="modalOverlay" class="modalOverlay">');
                             $("#divprintarea1").show();
                             $("#divprintarea1").css("display", "block !important");
                             $("#lblprintStoreName1").html(Supplier[0].StoreName);
                             $("#lblprintDeliveryNoteNo1").html(Supplier[0].DeliveryNoteNo);
                             $("#lblprintVATID1 ").html(Supplier[0].Vatid);
                             $("#lblorganisationprintNam1e").html(Supplier[0].OrganisationName);
                             $("#lblownerprintAddress1").html(Supplier[0].Address);
                             $("#lblownerprintCity1").html(Supplier[0].City);
                             $("#lblContNum1").html(Supplier[0].ContactNum);

                             $("#lblprintProductName").html(Supplierlist[0].ProductName);
                             $("#lblprintproductvalue").html("SAR-" + Supplierlist[0].ProductValue);
                             $("#imgbarcode").attr("src", "../images/BarCodeImages/" + Supplier[0].imagename);
                             // $("<%=txtQuantityForPrint.ClientID%>").val();
                            document.getElementById("ctl00_ContentPlaceHolder1_txtQuantityForPrint").value = Supplierlist[0].Quantity
                            document.getElementById("ctl00_ContentPlaceHolder1_hdProductIdforprint").value = Supplierlist[0].ProductId;

                            $("#tblStorePrint1 > tbody").empty();
                            for (var i = 0; i < Supplierlist.length; i++) {
                                $("#tblStorePrint1 > tbody").append("<tr style='font-size:12px;font-family:verdana;'><td><lable style='color:black;'>" + Supplierlist[i].CategoryName + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].BrandName + "</label></td><td><lable style='color:black;'>" +
                        Supplierlist[i].ProductName + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].ProductValue + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].Quantity + "</label></td><td><lable style='color:black;'>" +
                        Supplierlist[i].GrossValue + "</label></td></tr>");

                            }

                            var TotalProductValue = parseFloat(Supplier[0].NetProductValue);
                            TotalGrossValue = parseFloat(Supplier[0].TotalGrossValue);
                            var TotalPrice = parseFloat(Supplier[0].TotalValue);
                            var HandlingCharges = parseFloat(Supplier[0].HandlingCharges);
                            var Remarks = Supplier[0].Remarks
                            if (Remarks == "" || Remarks == null) {
                                Remarks = "";
                            }


                            $("#tblStorePrint1 > tfoot").append("<tr style='background-color:White;height:20px;font-size:12px;font-family:verdana;'>" +
                                       "<td colspan='6'><b></b></td>" +
                                      "</tr>");
                            $("#tblStorePrint1 > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                       "<td align='center' colspan='6'><b>Details:</b></td>" +
                                      "</tr>");
                            $("#tblStorePrint1 > tfoot").append("<tr style='background-color:White;font-size:12px;font-family:verdana;'>" +
                                                             "<td align='left' colspan='6' rowspan='1'><b>Remarks:</b></td>" +

                                                                  "</tr>");

                            $("#tblStorePrint1 > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                                             "<td align='left' colspan='4' rowspan='5'>" + Remarks + "</td>" +

                                                                  "</tr>");
                            $("#tblStorePrint1 > tfoot").append("<tr style='font-size:12px;font-family:verdana;'>" +
                                                           "<td align='right' colspan='1'><b>Total Selling Price:</b></td>" +
                                                             "<td align='center'>" + TotalGrossValue + "</td>" +
                                                           "</tr>");

                            $("#tblStorePrint > tfoot").append("<tr>" +
                                     "<td   align='right' colspan='2'><b>Total ProductValue</b></td>" +
                                    "<td  align='center'>" + TotalProductValue + "</td>" +
                                       "</tr>");
                            $("#tblStorePrint > tfoot").append("<tr>" +
                                                           "<td align='right' colspan='1'><b>Handling Charges:</b></td>" +
                                                             "<td align='center'>" + HandlingCharges + "</td>" +
                                                           "</tr>");
                            $("#tblStorePrint > tfoot").append("<tr>" +
                                                           "<td align='right' colspan='1'><b>Total Price:</b></td>" +
                                                             "<td align='center'>" + TotalPrice + "</td>" +
                                                           "</tr>");
                            $("table#tblStorePrint1 tr:even").css("background-color", "#F3F3F3");
                            $("table#tblStorePrint1 tr:odd").css("background-color", "#ffffff");
                        }
                    }
                    else {
                        $("#divprintarea1").hide();
                        removestyle();
                    }
                },
                error: function (result)
                { }
            });
        }
        function printDiv() {
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
            $("#Button1").show();
            $("#<%=Cancelbtn.ClientID%>").show();

        }
        function VoucherSourcetoPrint(source) {
            return "<html><head><style>@media (max-width: 6in) {@page { size: letter;} }</style><script>function step1(){\n" +
                    "setTimeout('step2()', 10);}\n" +
                    "function step2(){window.print();;window.close()}\n" +
                    "</scri" + "pt></head><body  onload='step1()'>\n" +
                    "<div class='print'><table id='tblprint1'    width='30%;'><tr><td style='text-align:left;'><label>" + $("#lblprintProductName").html() + "</label><br/><label>" + $("#lblprintproductvalue").html() + "</label></td><td>" + "<img  style='width:120px;height:30px;' src='" + source + "' /></td></tr></table></div>"
            "</body></html>";
        }
        function printDiv1() {
            Pagelink = "";
            var pwa = window.open(Pagelink, "_new");
            pwa.document.open();
            var source = $("#imgbarcode").attr('src');
            pwa.document.write(VoucherSourcetoPrint(source));
            pwa.document.close();

            //$("#tblSalesPrint th").css("background-color", "black");
            //$("#tblSalesPrint th").css("color", "white");
            //$("#tblSalesPrint tr:even").css("background-color", "White");
            //$("#tblSalesPrint tr:odd").css("background-color", "#f2f2f2");

            //   var disp_setting="toolbar=yes,location=no,directories=yes,menubar=yes,";
            //   disp_setting+="scrollbars=yes,width=650, height=600, left=100, top=25";            
            //   var content_vlue = document.getElementById("newprintarea1").innerHTML;
            //   window.open();
            //   var docprint=window.open("","",disp_setting);
            //   docprint.document.open();
            //   docprint.document.write('<html><head><title>Inel Power'
            //   +'System</title>');
            //   docprint.document.write('</head><body'
            //+'  onLoad="self.print()"><center>');
            //   docprint.document.write(content_vlue);
            //   docprint.document.write('</center></body></html>');
            //   docprint.document.close();
            //   docprint.focus(); 

            //var divContents = $("#Table2").html();
            //var printWindow = window.open('', '', 'height=400,width=800');
            //printWindow.document.write('<html><head><title>DIV Contents</title>');
            //printWindow.document.write('</head><body >');
            //printWindow.document.write(divContents);
            //printWindow.document.write('</body></html>');
            //printWindow.document.close();
            //printWindow.print();

        }
    </script>

    <script type="text/javascript">
        function Calculate(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            if (aa.value != "" && (!isNaN(aa.value))) {

                var quantity = parseInt(aa.value);
                //                var availablequantity = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtAvailableQuantity").value);
                //                if (quantity > availablequantity) {
                //                    alert("Quantity has to be less than or equal to the AvailableQuantity.");
                //                    aa.value = "";
                //                    return false;
                //                }
                var productvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtProductValue").value);
                var buyingdiscount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtDefaultSellingDiscount").value);
                var netproductvalue, grossvalue, netdiscount, total;
                if (!isNaN(quantity)) {
                    netproductvalue = parseFloat(productvalue - ((productvalue * buyingdiscount) / 100));
                    grossvalue = parseFloat(productvalue * quantity);
                    netdiscount = parseFloat(quantity * (productvalue * buyingdiscount) / 100);
                    total = parseFloat(netproductvalue * quantity);
                }
                else {
                    netproductvalue = 0;
                    grossvalue = 0;
                    netdiscount = 0;
                    total = 0;
                }
                document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtNetValue").value = netproductvalue;
                document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtgrossValue").value = grossvalue;
                document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtNetDiscount").value = netdiscount;
                document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtTotal").value = total;
            }
            else {
                alert("Please enter Numeric value");
                aa.value = "0";
                document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtTotal").value = "0.00";
                return false;
            }
            var grid = document.getElementById('<%=gvLineItems.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvLineItems.ClientID%>').rows.length;
            var totalgrossvalue = 0, totaldiscount = 0, totalprice = 0, handlingcharges = 0, totalproductvalue = 0, quat = 0;
            for (var i = 2; i <= gridrowcount; i++) {


                if (i < 10) {
                    quat = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtQunatity").value);
                    if (!isNaN(quat) && quat != 0) {
                        totalgrossvalue = totalgrossvalue + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtgrossValue").value);
                        totaldiscount = totaldiscount + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtNetDiscount").value);
                        totalprice = totalprice + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtTotal").value);
                        totalproductvalue = totalproductvalue + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtTotal").value);
                    }
                }
                else {
                    quat = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtQuantity").value);
                    if (!isNaN(quat) && quat != 0) {
                        totalgrossvalue = totalgrossvalue + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtgrossValue").value);
                        totaldiscount = totaldiscount + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtNetDiscount").value);
                        totalprice = totalprice + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtTotal").value);
                        totalproductvalue = totalproductvalue + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtTotal").value);
                    }
                }
            }
            var handlingcharges = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtHandlingCharges").value);
            document.getElementById("ctl00_ContentPlaceHolder1_txttotalgrossvalue").value = totalgrossvalue.toFixed(2);
            document.getElementById("ctl00_ContentPlaceHolder1_txttotaldiscountvalue").value = totaldiscount.toFixed(2);
            document.getElementById("ctl00_ContentPlaceHolder1_txtTotalPrice").value = totalprice.toFixed(2);
            var valu = parseFloat(totalproductvalue.toFixed(2)) + parseFloat(handlingcharges.toFixed(2));
            if (!isNaN(handlingcharges))
                document.getElementById("ctl00_ContentPlaceHolder1_txtTotalProductValue").value = valu.toFixed(2);
            else
                document.getElementById("ctl00_ContentPlaceHolder1_txtTotalProductValue").value = totalproductvalue.toFixed(2);
            return false;
        }


        function Calculate1(aa) {
            if (aa.value != "" && (!isNaN(aa.value))) {
                var id = aa.id.split('_')[3].split("l")[1];
                //               // var availablequantity = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtAvailableQuantity").value;
                //                if (parseInt(aa.value) > parseInt(availablequantity)) {
                //                    alert("Quantity has to be less than or equal to available quantity.");
                //                    aa.value = 0;
                //                    return false;
                //                }
                //                else {
                var productvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtProductValue").value);
                var total = parseFloat(aa.value * productvalue);
                document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtTotal").value = total;
                // }
            }
            else {
                //alert("Please enter Numeric value");
                aa.value = "";
                var id = aa.id.split('_')[3].split("l")[1];
                var total = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtTotal").value);
                var totalgrossvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txttotalgrossvalue").value);
                //  var handlingcharges = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtHandlingCharges").value);
                var totalvalue = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtTotalProductValue").value);
                document.getElementById("ctl00_ContentPlaceHolder1_txttotalgrossvalue").value = totalgrossvalue - total;;
                //document.getElementById("ctl00_ContentPlaceHolder1_txttotaldiscountvalue").value =0;
                document.getElementById("ctl00_ContentPlaceHolder1_txtTotalProductValue").value = totalvalue - total;
                document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtTotal").value = "0.00";
                return false;
            }
            Calculate2();

        }
        function Calculate2() {
            var grid = document.getElementById('<%=gvLineItems.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvLineItems.ClientID%>').rows.length;
            var totalgrossvalue = 0;
            for (var i = 2; i <= gridrowcount; i++) {
                if (i < 10) {
                    quat = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtQunatity").value);
                    totalprice = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtTotal").value);
                    if (!isNaN(quat) && quat != 0 && totalprice != 0 && !isNaN(totalprice)) {
                        totalgrossvalue = totalgrossvalue + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtTotal").value);
                    }
                }
                else {
                    quat = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtQuantity").value);
                    totalprice = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtTotal").value);
                    if (!isNaN(quat) && quat != 0 && totalprice != 0 && !isNaN(totalprice)) {
                        totalgrossvalue = totalgrossvalue + parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtTotal").value);
                    }
                }
            }
            var handlingcharges = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_txtHandlingCharges").value);
            document.getElementById("ctl00_ContentPlaceHolder1_txttotalgrossvalue").value = totalgrossvalue.toFixed(2);
            var valu = 0;
            if (!isNaN(handlingcharges))
                valu = parseFloat(totalgrossvalue.toFixed(2)) + parseFloat(handlingcharges.toFixed(2));
            if (!isNaN(handlingcharges))
                document.getElementById("ctl00_ContentPlaceHolder1_txtTotalProductValue").value = valu.toFixed(2);
            else
                document.getElementById("ctl00_ContentPlaceHolder1_txtTotalProductValue").value = totalgrossvalue.toFixed(2);
        }
        function mult() {

            var HandlinCharges = document.getElementById('<%=txtHandlingCharges.ClientID %>').value;
            var TotalProductValue = document.getElementById('<%=txttotalgrossvalue.ClientID %>').value;
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
    </script>
   
    <script type="text/javascript">
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
    </script>
    <script type="text/javascript">
        function SaveValidation() {

            var grid = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems");
            var len = grid.rows.length;
            var quantity, category, brand, product, sellingprice, str;
            for (var i = 2; i < len; i++) {
                if (i < 10) {
                    quantity = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtQunatity").value;
                    category = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_ddlCategory").value;
                    brand = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtBrand").value;
                    product = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtProduct").value;
                    sellingprice = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtProductValue").value;
                    if (category != "0" && brand != "" && product != "") {
                        if (quantity == "" || quantity == "0") {
                            alert("Please Enter Quantity");
                            return false;
                        }// if
                        if (sellingprice == "0.00") {
                            // alert("Selling price cannot be 0.");
                            // return false;
                        }//if
                    }// outer if
                }// i if
                else {
                    quantity = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtQunatity").value;
                    category = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_ddlCategory").value;
                    brand = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtBrand").value;
                    product = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtProduct").value;
                    sellingprice = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtProductValue").value;
                    if (category != "0" && brand != "" && product != "") {
                        if (quantity == "" || quantity == "0") {
                            alert("Please Enter Quantity");
                            return false;
                        }
                        if (sellingprice == "0.00") {
                            // alert("Selling price cannot be 0.");
                            // return false;
                        }
                    }
                }//else

            } //for loop
            return true;
        }
    </script>

      <script type="text/javascript">
          function applystyle() {

              $("body").append('<div id="modalOverlay" class="modalOverlay">');
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
            <asp:PostBackTrigger ControlID="grdStoreDeliveryNote" />
                <asp:PostBackTrigger ControlID="btncancelgrid" />
               <asp:PostBackTrigger ControlID="btnClearSearch" />
                <asp:PostBackTrigger ControlID="searchDeliveryNote" />
        </Triggers>
                <ContentTemplate>
                 
                    <div class="row">
                        <div class="col-md-12">
                            <form class="form-horizontal">
                                <div class="panel panel-default">
                                    <!-- Screen Heading  Start-->
                                    <div class="panel-heading">
                                        <h3 class="panel-title">
                                            <strong>Store Delivery Note</strong>
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
                                    <div id="panelAddUser" runat="server" class="page-content-wrap">
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
                                                    <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="true" />
                                                    <asp:HiddenField ID="hiddenStoreDeliveryNoteId" runat="server" EnableViewState="true" />
                                                    <asp:RequiredFieldValidator ID="rfvSotre" runat="server" ControlToValidate="ddlStore"
                                                        ErrorMessage="Please Select Store" ValidationGroup="r" Text="*" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row top">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Delivery Note No</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox CssClass="form-control" ID="txtDeliveryNo" runat="server" Enabled="false" />
                                                        </div>
                                                        <%-- <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtDeliveryNoteNo" InitialValue="" ErrorMessage="Please Enter Delivery Note no."
                                                    Text="*"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Delivery Note Date</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group pull-right">
                                                            <asp:TextBox ID="txtDeliveryDate" onkeyup="datevalidation(this);"
                                                                CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                            <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfvtxtDeliveryDate" runat="server" ControlToValidate="txtDeliveryDate"
                                                            ErrorMessage="Please Enter Delivey Date Note" ValidationGroup="r" Text="*"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row top" style="display: none">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Payment Due Date</label>
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
                                            <div class="form-group">
                                                <div class="col-md-2" id="dvsavedeli" runat="server">
                                                    <asp:Button ID="btnSaveDeliveryNote" runat="server" CssClass="btn btn-success btn-block pull-right"
                                                        Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" ValidationGroup="r" OnClientClick="this.disabled = true;" 
                                             UseSubmitBehavior="false"
                                                        OnClick="btnSaveDeliveryNote_Click"  />
                                                </div>
                                                <input type="hidden" id="hidSDNID" />
                                                <div class="col-md-2" id="dvcancel" runat="server">
                                                    <asp:Button ID="btncancel1" runat="server" CssClass="btn btn-danger btn-block pull-right"
                                                        Text="Cancel" Style="margin-bottom: 5px;" OnClick="btncancel1_Click" />
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divAddBatches" runat="server">
                                            <div id="divCustomerDeliveryNote" class="table-responsive">
                                                <asp:GridView ID="gvLineItems" runat="server" AutoGenerateColumns="false" Style="width: 1081px;"
                                                    OnRowDataBound="gvLineItems_RowDataBound" OnRowCommand="gvLineItems_RowCommand"
                                                    CssClass="table  table-bordered table-striped table-actions"
                                                    OnRowEditing="gvLineItems_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Category">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="form-control select"
                                                                    OnSelectedIndexChanged="ddlCardType_SelectedIndexChanged" TabIndex="-1">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="HDStoreDeliveryNoteDetailID" runat="server" Value='<%#Eval("StoreDeliveryNoteDetailID")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Brand">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="true" CssClass="form-control select"
                                                                    Visible="false"
                                                                    OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control " AutoComplete="on"
                                                                    AutoPostBack="true" TabIndex="-1" OnTextChanged="txtBrand_ontextchanged"></asp:TextBox>
                                                                <asp:ImageButton ID="imgBrand" runat="server" ImageUrl="~/images/plus.png" Style="width: 20px;
                                                                    float: right; margin-top: -25px; margin-left: 185px; display: none;"
                                                                    OnClick="imgBrand_onclick" OnClientClick="return applystyle();" />
                                                                <asp:HiddenField ID="HDBrandID" runat="server" />
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
                                                                    Visible="false"
                                                                    OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control " AutoComplete="on"
                                                                    AutoPostBack="true" OnTextChanged="txtProduct_ontextchanged"></asp:TextBox>
                                                                <asp:ImageButton ID="imgProduct" runat="server" ImageUrl="~/images/plus.png" Style="width: 20px;
                                                                    float: right; margin-top: -25px; margin-left: 185px; display: none;"
                                                                    OnClick="imgProduct_onclick" OnClientClick="return applystyle();" /><%-- --%>
                                                                <asp:HiddenField ID="HDProductID" runat="server" />
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="AutoCompleteProductRequest"
                                                                    ServicePath="~/Screens/AutoComplete.asmx" MinimumPrefixLength="1" CompletionInterval="100"
                                                                    EnableCaching="false" CompletionSetCount="10" TargetControlID="txtProduct" FirstRowSelected="false"
                                                                    UseContextKey="true" CompletionListCssClass="completionList"
                                                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                </asp:AutoCompleteExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Selling Price">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtProductValue" runat="server" Enabled="false" CssClass="form-control "
                                                                    Text='<%#Eval("ProductValue") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQunatity" runat="server" CssClass="form-control " onkeyup="return Calculate1(this);"
                                                                    Text='<%#Eval("Quantity") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Tot.Selling Price">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtTotal" runat="server" class="form-control" Text='<%#Eval("NetProductTotal") %>'
                                                                    Enabled="false"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="30">
                                                            <ItemTemplate>
                                                                <%--   <asp:ImageButton ID="imgDeleteRow" ToolTip="Delete Row" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="DeleteRow" OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                                                runat="server" ImageUrl="../Images/cross.png" Style="width: 20px; height: 20px"
                                                                TabIndex="-1" />--%>
                                                                <asp:LinkButton ID="imgDeleteRow" runat="server" CommandName="DeleteRow"
                                                                    OnClientClick="return confirm('Do you want to Delete the record?');"
                                                                    CommandArgument='<%#((GridViewRow)Container).RowIndex%>'><span class="fad fa-times"> </span></asp:LinkButton>
                                                                <asp:ImageButton ID="ImgPrint" ToolTip="Print" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                                    CommandName="PrintRow" ImageUrl="~/images/print.jpg" runat="server" Style="width: 20px;display:none;
                                                                    height: 20px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="Label" HeaderText="CustomerDeliveryNoteDetailId"
                                                            Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerDeliveryNoteDetailId" runat="server" Text='<%#Eval("StoreDeliveryNoteDetailID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <div id="SDNCalculation" runat="server" visible="false">
                                                <div class="row top">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Total Selling Price</label>
                                                            <div class="col-md-8">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                    <asp:TextBox CssClass="form-control" ID="txttotalgrossvalue" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row top" style="display: none">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Handling Charges</label>
                                                            <div class="col-md-8">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                    <asp:TextBox CssClass="form-control" ID="txtHandlingCharges" runat="server" Text="0.00"
                                                                        onChange="return Numerics(this);" onkeyup="return mult()" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Total Value</label>
                                                            <div class="col-md-8">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                                    <asp:TextBox CssClass="form-control" ID="txtTotalProductValue" runat="server" />
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
                                                <div id="Div1" runat="server">
                                                    <div class="form-group">

                                                        <div class="col-md-2" id="dvisave" runat="server">
                                                            <asp:Button ID="imgSave" ValidationGroup="r" runat="server" CssClass="btn btn-success btn-block pull-right"
                                                                Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" OnClientClick="this.disabled = true;" 
                                                            UseSubmitBehavior="false"
                                                                OnClick="imgSave_Click" />
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
                                    <div id="panelSearchDeliveryNote" runat="server" class="panel-body">
                                        <div class="row top">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        Organisation</label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddlSearchOraganisation" AutoPostBack="true"
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
                                                        Delivery Note No</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                            <asp:TextBox ID="txtSearchDNNO" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                            <asp:TextBox ID="txtDeliveryNoteFromDate"
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
                                                            <asp:TextBox ID="txtDeliveryNoteToDate" CssClass="form-control datepicker"
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
                                            <asp:GridView ID="grdStoreDeliveryNote" runat="server" AutoGenerateColumns="false"
                                                DataKeyNames="StoreDeliveryNoteId" AlternatingRowStyle-Height="10px"
                                                OnPageIndexChanging="grdStoreDeliveryNote_PageIndexChanging" OnRowCommand="grdStoreDeliveryNote_RowCommand"
                                                OnRowCreated="grdStoreDeliveryNote_RowCreated" OnRowDataBound="grdStoreDeliveryNote_RowDataBound"
                                                OnRowDeleting="grdStoreDeliveryNote_RowDeleting" OnRowEditing="grdStoreDeliveryNote_RowEditing"
                                                CssClass="table datatable table-bordered table-striped table-actions">
                                                <Columns>
                                                    <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                        DataField="StoreName" HeaderText="Store Name"></asp:BoundField>
                                                    <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                        DataField="StoreDeliveryNoteNo" HeaderText="Delivery Note No"></asp:BoundField>
                                                    <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                        DataField="StoreDeliveryNoteDate" HeaderText="Delivert Note Date"></asp:BoundField>
                                                    <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                        DataField="TotalValue" HeaderText="Total Value"></asp:BoundField>
                                                    <asp:TemplateField ItemStyle-Width="90">
                                                        <ItemTemplate>
                                                            <%--   <asp:ImageButton ID="lnkbtnView" runat="server" ImageUrl="~/images/hammer_screwdriver.png"
                                                        Text="View" ToolTip="View" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                        CommandName="View" ValidationGroup="False" />
                                                    <asp:ImageButton ID="lnkbtnEdit" runat="server" ImageUrl="~/images/pencil.png" ToolTip="Edit"
                                                        CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="Editing"
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
                                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Editing" CommandArgument='<%#((GridViewRow)Container).RowIndex%>'
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
                                                <%--<RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                        <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#E5E5E5" Height="25px" HorizontalAlign="Center" />--%>
                                            </asp:GridView>
                                             <asp:HiddenField ID="RowIndex" runat="server" />
                                        </div>
                                    </div>
                                   


                                </div>
                            </form>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="dvBatchPopup" style="display: none; z-index: 1040;" class="modal fade in"
                role="dialog" tabindex="-1"
                aria-hidden="false" runat="server">
                <div class="modal-backdrop fade in" style="height: 100%;">
                </div>
                <div class="modal-dialog modal-lg ">
                    <div class="modal-content" style="height: 600px;">
                        <center>
                            <div class="modal-header">
                                <h4 class="modal-title">Popup</h4>
                            </div>
                            <div class="modal-body" style="height: 520px; overflow-y: auto;">
                                <asp:GridView ID="gvBatchPopup" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="Label">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                <asp:HiddenField ID="hdBatchid" runat="server" Value='<%#Eval("BatchID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Product" DataField="ProductName" ItemStyle-CssClass="Label" />
                                        <asp:TemplateField HeaderText="Batch From" ItemStyle-CssClass="Label">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBatchFrom" runat="server" Text='<%#Eval("BatchFrom") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Batch To" DataField="BatchTo" ItemStyle-CssClass="Label" />
                                        <asp:TemplateField HeaderText="Available Quantity" ItemStyle-CssClass="Label">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAvailableQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="Label">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                    <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#E5E5E5" Height="25px" HorizontalAlign="Center" />
                                </asp:GridView>

                                <asp:Button ID="btnSaveBatch" runat="server" CssClass="BtnEmptyStyle" Text="Save"
                                    OnClientClick="return popupvalidation();" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="BtnEmptyStyle" Text="Cancel" />
                            </div>
                        </center>
                    </div>
                </div>
            </div>
            <div id="divprintarea2" class="modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-position">
                    <div class="modal-content" id="printdiv">
                        <div class="modal-header">
                            <center>
                                <h2 class="modal-title">Store Delivery Note </h2>
                            </center>                           
                            <table width="800px" align="center" id="Table1">
                                <tr>
                                    <td style="width: 30px"></td>
                                    <td align="left">
                                        <label id="lblprintStoreName1" class="Label">
                                        </label>
                                        <br />
                                        <label id="Label17" class="Label">
                                            DeliveryNote No:</label>
                                        <label id="lblprintDeliveryNoteNo1" class="Label">
                                        </label>
                                        <br />
                                        <label id="lblDelNoteDate" class="Label">
                                            DeliveryNote Date:</label>
                                        <label id="lblDeliveryNoteDate1" class="Label">
                                        </label>
                                        <br />
                                        <label id="lblprintStoreAddress1" class="Label">
                                        </label>
                                        <br />
                                        <label id="lblprintStorePincode1" class="Label">
                                        </label>

                                        <label id="lblprintCity1" class="Label">
                                        </label>
                                        <br />
                                        <br />
                                        <br />

                                    </td>
                                    <td width="300px"></td>
                                    <td align="left">
                                        <label id="lblorganisationprintName1" class="Label">
                                        </label>
                                        <br />
                                        <label id="lblownerprintAddress1" class="Label">
                                        </label>
                                        <br />
                                        <label id="label20" class="Label">
                                        </label>
                                        <label id="lblownerprintCity1" class="Label">
                                        </label>
                                        <br />
                                        <label id="lblCountry1" class="Label">
                                            K.S.A</label>
                                        <br />
                                        <label id="lblContNum1" class="Label">
                                            Cont Num:
                                        </label>
                                        <label id="Label24" class="Label">
                                        </label>
                                        <br />

                                        <label id="lblEmail1" class="Label">
                                            Email:</label><label id="Label28" class="Label"></label>
                                        <br />

                                    </td>
                                </tr>
                            </table>
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
                                            <th style="color: white; background-color: black;">Model
                            No
                                            </th>
                                            <th style="color: white; background-color: black;">Selling
                            Price
                                            </th>
                                            <th style="color: white; background-color: black;">Quantity
                                            </th>
                                            <th style="color: white; background-color: black;">Tot Selling
                            Price
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
                            <input type="button" id="btnPrint1" value="Print" class="BtnEmptyStyle" title="Print"
                                onclick="printDiv()" />
                            <asp:Button ID="Cancelbtn2" runat="server" Text="Cancel" CssClass="BtnEmptyStyle"
                                OnClientClick="return removestyle();" />
                        </div>
                    </div>
                </div>
            </div>
            


         
            <div id="dvBrandPopUp" style="display: none; z-index: 1040;" class="modal fade in"
                role="dialog" tabindex="-1"
                aria-hidden="false" runat="server">
                <div class="modal-backdrop fade in" style="height: 100%;">
                </div>
                <div class="modal-dialog modal-lg ">
                    <div class="modal-content" style="height: 600px;">
                        <div class="modal-header">
                            <h4 class="modal-title">Brand</h4>
                        </div>
                        <center>
                            <%-- <h2 class="HeaderStyle" style="margin-top: 0px; height: auto">Brand</h2>--%>
                            <asp:Label ID="lblStatusBrand" runat="server" CssClass="MessageClass" Style="color: Red;"></asp:Label>
                        </center>
                        <div class="modal-body" style="height: 520px; overflow-y: auto;">
                            <table border="0" align="center" cellpadding="1" cellspacing="2" style="width: 400px;
                                margin-bottom: 14px">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAddBrand" runat="server" Text="Brand Name"
                                            CssClass="Label"></asp:Label>
                                        <asp:HiddenField ID="HiddenFieldBrandId" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddBrand" runat="server" CssClass="TextBoxStyle"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAddBrand" runat="server" ValidationGroup="rr"
                                            ControlToValidate="txtAddBrand"
                                            Text="*" ErrorMessage="Brand Name Can not be Empty">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="IsActive" CssClass="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" Style="margin-left: -170px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:ImageButton ID="imgAddBrand" runat="server" ValidationGroup="rr"
                                            ImageUrl="~/images/saveBtn.png" OnClick="imgAddBrand_Click" Style="margin-bottom: -17px;" /><%--OnClientClick="return removestyle();"--%>
                                        <%--<asp:ImageButton ID="imgUpdateBrand" runat="server" ValidationGroup="rr"
                        Visible="false"
                                ImageUrl="~/images/updateBtn.png" OnClick="imgUpdate_Click" />--%>
                                        <%--<asp:ImageButton ID="imgClearBrand" runat="server" ValidationGroup="rr"
                        ImageUrl="~/images/clearBtn.png"
                                 Height="40px" CausesValidation="false" onclick="imgClearBrand_Click" />--%>
                                        <asp:Button ID="btnCancelBrand" Text="Cancel" runat="server" CssClass="BtnEmptyStyle"
                                            OnClick="btnCancelBrand_onclick" OnClientClick="return removestyle();" />
                                    </td>
                                </tr>
                            </table>
                            <center>
                                <div id="dvSummary2" align="center">
                                    <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="rr"
                                        CssClass="validationSummary" />
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
            <div id="dvProductPopUp" style="display: none; z-index: 1040;" class="modal fade in"
                role="dialog" tabindex="-1"
                aria-hidden="false" runat="server">
                <div class="modal-backdrop fade in" style="height: 100%;">
                </div>
                <div class="modal-dialog modal-lg ">
                    <div class="modal-content" style="height: 600px;">
                        <div class="modal-header">
                            <h4 class="modal-title">Product</h4>
                        </div>
                         <asp:Label ID="lblStatusProduct" runat="server" CssClass="MessageClass" Style="color: Red;"></asp:Label>
                           <div class="modal-body" style="height: 520px; overflow-y: auto;">
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="Label10" runat="server" CssClass="Label">Category:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLCategory" runat="server" CssClass="DropDownClass"
                                        Style="margin-left: -27px;">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="gr" ID="RequiredFieldValidator4"
                                        runat="server"
                                        ControlToValidate="DDLCategory"
                                        InitialValue="0" ErrorMessage="Please Select Category" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" runat="server" CssClass="Label">Brand:</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="DropDownClass"
                                        Style="margin-left: -27px;">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ValidationGroup="gr" ID="RequiredFieldValidator0"
                                        runat="server"
                                        ControlToValidate="ddlBrand"
                                        InitialValue="0" ErrorMessage="Please Select Brand" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblProductName" runat="server" CssClass="Label">Product
                        Name:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProductName" runat="server" CssClass="TextBoxStyle"
                                        Style="margin-left: -10px;"></asp:TextBox>

                                    <asp:RequiredFieldValidator ValidationGroup="gr" ID="RequiredFieldValidator5"
                                        runat="server"
                                        ControlToValidate="txtProductName" InitialValue=""
                                        ErrorMessage="Please Enter Product Name" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label12" runat="server" CssClass="Label">Product
                        Value (Selling Price):</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProductValue" runat="server" CssClass="TextBoxStyle"
                                        onchange="return CheckValue(this);"
                                        Style="margin-left: -7px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="gr" ID="RequiredFieldValidator6"
                                        runat="server"
                                        ControlToValidate="txtProductValue" InitialValue=""
                                        ErrorMessage="Please Enter Product value" Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    <asp:Label ID="Label13" runat="server" CssClass="Label" Visible="false">Default
                        Discount%(Selling):</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefaultSellingDiscount" runat="server" CssClass="TextBoxStyle"
                                        Visible="false"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ValidationGroup="r" ID="RequiredFieldValidator4"
                        runat="server" ControlToValidate="txtDefaultSellingDiscount" InitialValue=""  
                            ErrorMessage="Please Enter Selling Discount" Text="*"></asp:RequiredFieldValidator>
                                    --%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label14" runat="server" CssClass="Label" Visible="true">
                        Maximum Discount % :</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMaxDiscPer" runat="server" CssClass="TextBoxStyle"
                                        Visible="true"
                                        Text="0.00" onblur="return validDecimal()" Style="margin-left: -15px;"></asp:TextBox>

                                </td>

                            </tr>
                            <tr style="display: none;">
                                <td>
                                    <asp:Label ID="Label15" runat="server" CssClass="Label">IsActive:</asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkActiveProduct" runat="server" Checked="true"
                                        Style="margin-left: -170px;" />
                                </td>
                            </tr>
                        </table>
                        <center>
                            <asp:ImageButton ID="imgSaveProduct" ValidationGroup="gr"
                                ImageUrl="../images/saveBtn.png" alt="Save" runat="server"
                                OnClick="imgSaveProduct_Click" Style="height: 40px; margin-bottom: -17px;" />
                            <asp:Button ID="btnClearProduct" Text="Cancel" runat="server" CssClass="BtnEmptyStyle"
                                OnClientClick="return removestyle();" OnClick="btnClearProduct_onclick" />
                        </center>
                        <center>
                            <div id="dvSummary1" align="center">
                                <asp:ValidationSummary CssClass="validationSummary" ID="ValidationSummary2"
                                    ValidationGroup="gr"
                                    runat="server" />
                            </div>                           
                        </center>
                        </div>
                    </div>
                </div>
            </div>
               <div id="divprintarea1" class="modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-position">
                    <div class="modal-content" id="Div2">
                        <div class="modal-header">

                            <div id="newprintarea1"  style="display:none;">
                                <table width="90%" align="center" id="Table2">

                                    <tr>
                                        <td style="text-align: left;">
                                            <label id="lblprintProductName" style="color: black; display: none;">
                                            </label>
                                            <label id="lblprintqtys" style="color: black;">Quantity</label>
                                            <br />
                                            <label id="lblprintproductvalue" style="color: black; display: none;">
                                            </label>
                                        </td>
                                        <td>
                                            <img id="imgbarcode" style="width: 150px; height: 40px; display: none;" />
                                            <input type="text" id="txtQuantityForPrint" runat="server"
                                                class="TextBoxStyle"></input>
                                            <input type="hidden" id="hdProductIdforprint" runat="server" />
                                        </td>

                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="modal-body">
                            <br />
                            <div>
                                <div id="printSupplierDeliveryNote1" class="gridClass1" >
                                    <table width="750px" align="center" id="Table3" border="1"
                                        style="border-spacing: 0px;">
                                        <thead>
                                            <tr>
                                                <th style="color: white; background-color: black;">Category
                                                </th>
                                                <th style="color: white; background-color: black;">Brand
                                                </th>
                                                <th style="color: white; background-color: black;">Model
                        No
                                                </th>
                                                <th style="color: white; background-color: black;">Selling
                        Price
                                                </th>
                                                <th style="color: white; background-color: black;">Quantity
                                                </th>
                                                <th style="color: white; background-color: black;">Tot
                        Selling Price
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
                        <br />
                        <div class="modal-footer">
                            <input type="button" id="Button2" value="Print" class="BtnEmptyStyle"
                                title="Print"
                                onclick="printDiv1()" style="display: none;" />
                            <asp:Button ID="btnPrintQuantity" runat="server" CssClass="BtnEmptyStyle"
                                ToolTip="Print"
                                OnClick="btnPrintQuantity_Click" Text="Print" />
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
                                <button type="button" class="close" style="display:none" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                               <center> <h4 class="modal-title">Storedeliverynote</h4></center>
                            </div>
                              <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6" style="width:35%;float:left;margin-left:15%;">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="col-md-12">
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
                                                    <label id="Label1" class="Label pull-left">DeliveryNote No:</label>
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
                                                    <label id="Label3" class="Label pull-left">DeliveryNote Date:</label>
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
                                <input type="button" id="Button1" value="Print" class="BtnEmptyStyle" title="Print"
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
