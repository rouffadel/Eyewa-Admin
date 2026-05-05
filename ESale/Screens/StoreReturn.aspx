<%@ Page Title="Store Return" Language="C#" MasterPageFile="~/Admin/Admin.master"
    AutoEventWireup="true" CodeFile="StoreReturn.aspx.cs" Inherits="Screens_StoreReturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:ScriptManager ID="tsm" runat="server"></asp:ScriptManager>--%>
    <asp:Label ID="lblTitle" runat="server" Visible="false">Store Return</asp:Label>
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
   <script src="../js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
      <script type="text/javascript">
          function StoreForPrint(storedeliverynoteid) {

              $.ajax({
                  type: "POST",
                  contentType: "application/json; charset=utf-8",
                  data: "{StoreReturnId:'" + storedeliverynoteid + "'}",
                  dataType: "json",
                  url: "StoreReturn.aspx/StoreForPrint",
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
    <script language="javascript">
        function Calculate1(aa) {
            if (isNaN(parseInt(aa.value))) {
                //alert("Please Enter Numeric values for quantity.");
                aa.value = "";
                CalculateTotal();
                return false;
            }
            else {
                var id = aa.id.split('_')[3].split("l")[1];
                if (isNaN(parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtProductValue").value))) {
                    alert("Product value cannot be empty.");
                    document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtProductValue").value = "0.00";
                    CalculateTotal();
                    return false;
                }
                var sp = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + id + "_txtProductValue").value);
                var qty = parseInt(aa.value);
                CalculateTotal();
            }
        }
        function CalculateTotal() {
            var grid = document.getElementById('<%=gvLineItems.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvLineItems.ClientID%>').rows.length;
            var totalsp = 0, totalqty = 0;
            for (var i = 2; i <= gridrowcount; i++) {
                if (i < 10) {
                    var sp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtProductValue").value;
                    var qty = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtQuantity").value;
                    if (!isNaN(parseFloat(sp)) && !isNaN(parseInt(qty))) {
                        totalsp = totalsp + (parseFloat(sp) * parseInt(qty));
                        totalqty = totalqty + parseInt(qty);
                    }
                }
                else {
                    var sp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtProductValue").value;
                    var qty = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtQuantity").value;
                    if (!isNaN(parseFloat(sp)) && !isNaN(parseInt(qty))) {
                        totalsp = totalsp + (parseFloat(sp) * parseInt(qty));
                        totalqty = totalqty + parseInt(qty);
                    }
                }
            }
            document.getElementById("ctl00_ContentPlaceHolder1_txtSellingprice").value = totalsp;
            document.getElementById("ctl00_ContentPlaceHolder1_txtTotalQuantity").value = totalqty;
        }
        function SaveValidation() {
            var grid = document.getElementById('<%=gvLineItems.ClientID%>');
            var gridrowcount = document.getElementById('<%=gvLineItems.ClientID%>').rows.length;
            var cat, brand, prod, sp, qty;
            for (var i = 2; i < gridrowcount; i++) {
                if (i < 10) {
                    cat = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_ddlCategory").value;
                    brand = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtBrand").value;
                    prod = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtProduct").value;
                    if (cat != "0" && brand != "0" && prod != "0") {
                        sp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtProductValue").value;
                        qty = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtQuantity").value;
                        if (sp == "" || sp == "0" || sp == "0.00" || sp == "0.0" || sp == "0.") {
                            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtProductValue").value = "";
                            // alert("Please Enter Selling price for row " + (i - 1));
                            //return false;
                        }
                        if (qty == "" || qty == "0") {
                            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl0" + i + "_txtQuantity").value = "";
                            alert("Please Enter Quantity for row " + (i - 1));
                            return false;
                        }
                    }
                }
                else {
                    cat = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_ddlCategory").value;
                    brand = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtBrand").value;
                    prod = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtProduct").value;
                    if (cat != "0" && brand != "0" && prod != "0") {
                        sp = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtProductValue").value;
                        qty = document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtQuantity").value;
                        if (sp == "" || sp == "0" || sp == "0.00" || sp == "0.0" || sp == "0.") {
                            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtProductValue").value = "";
                            //alert("Please Enter Selling price for row " + (i - 1));
                            // return false;
                        }
                        if (qty == "" || qty == "0") {
                            document.getElementById("ctl00_ContentPlaceHolder1_gvLineItems_ctl" + i + "_txtQuantity").value = "";
                            alert("Please Enter Quantity for row " + (i - 1));
                            return false;
                        }
                    }
                }
            }
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
                    <asp:PostBackTrigger ControlID="Search" />

                </Triggers>
                <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Store Return</strong>
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
                            <div id="panelSearchDeliveryNote" runat="server" class="panel-body">
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                Organisation</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlOraganisationSearch"
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
                                                <asp:DropDownList ID="ddlStoreSearch"
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
                                                Return No</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                    <asp:TextBox ID="txtReturnNoSearch" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                    <asp:Button ID="Search" runat="server" Text="Search"
                                                        CssClass="btn btn-info" OnClick="Search_Click" />&nbsp;&nbsp;
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
                                    <asp:GridView ID="gvStoreReturn" runat="server" AutoGenerateColumns="false"
                                        CssClass="table datatable  table-bordered table-striped table-actions"
                                        DataKeyNames="StoreReturnId"
                                        OnDataBound="gvStoreReturn_DataBound"
                                        OnRowCommand="gvStoreReturn_RowCommand" OnRowCreated="gvStoreReturn_RowCreated"
                                        OnRowDeleting="gvStoreReturn_RowDeleting" OnRowEditing="gvStoreReturn_RowEditing">
                                        <Columns>
                                            <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                DataField="StoreName" HeaderText="Store Name"></asp:BoundField>
                                            <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                DataField="ReturnNumber" HeaderText="Return No"></asp:BoundField>
                                            <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                DataField="ReturnDate" HeaderText="Return Date"></asp:BoundField>
                                            <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                DataField="TotalSellingPrice" HeaderText="Total Selling Price"></asp:BoundField>
                                            <asp:BoundField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                DataField="TotalQty" HeaderText="Total Quantity"></asp:BoundField>
                                            <asp:TemplateField ItemStyle-Width="90">
                                                <ItemTemplate>
                                                    <%--   <asp:ImageButton ID="lnkbtnView" runat="server" ImageUrl="~/images/hammer_screwdriver.png"
                                                        Text="View" ToolTip="View" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'
                                                        CommandName="View" ValidationGroup="False" />
                                                    <asp:ImageButton ID="lnkbtnEdit" runat="server" ImageUrl="~/images/pencil.png" ToolTip="Edit"
                                                        CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="EditRow"
                                                        Text="Edit" ValidationGroup="False" />
                                                    <asp:ImageButton ID="lnkbtnDel" runat="server" ImageUrl="~/images/cross.png" Text="Deleting"
                                                        ToolTip="Delete" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="Delete"
                                                        ValidationGroup="False" OnClientClick="return confirm('Do you want to Delete the record?');" />--%>
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
                                                    <asp:ImageButton ID="lnkbtnPrint" runat="server" ImageUrl="~/images/print.jpg" Width="20px"
                                                        Height="20px" ToolTip="Print" Text="Print" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                        CommandName="Print" ValidationGroup="False" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <%--  <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                        <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#E5E5E5" Height="25px" HorizontalAlign="Center" />--%>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div id="panelAddUser" runat="server" class="page-content-wrap">
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Organisation</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlOrganisation" runat="server" AutoPostBack="true"
                                                    class="form-control select"
                                                    Style="margin-bottom: 12px;">
                                                </asp:DropDownList>
                                            </div>
                                            <input type="hidden" id="lblOrganisationID" runat="server" />
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
                                                Return No</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtReturnNo" runat="server" Enabled="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Return Date</label>
                                            <div class="col-md-8">
                                                <div class="input-group pull-right">
                                                    <asp:TextBox ID="txtReturnDate"
                                                        CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvtxtDeliveryDate" runat="server" ControlToValidate="txtReturnDate"
                                                    ErrorMessage="Please Enter Delivey Date Note" ValidationGroup="r" Text="*"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="form-group">
                                        <div class="col-md-2" id="dvsave" runat="server">
                                            <asp:Button ID="btnSaveReturn" runat="server" CssClass="btn btn-success btn-block pull-right"
                                                Text="Save" Style="margin-bottom: 5px;" CausesValidation="true" OnClientClick="this.disabled = true;" 
                                             UseSubmitBehavior="false"
                                                OnClick="btnSaveDeliveryNote_Click" />
                                            <asp:HiddenField runat="server" ID="HDStoreReturnId" />
                                        </div>
                                        <div class="col-md-2" id="dvcancel" runat="server">
                                            <asp:Button ID="btncancel1" runat="server" CssClass="btn btn-danger btn-block pull-right"
                                                Text="Cancel" Style="margin-bottom: 5px;" OnClick="btncancel1_Click" />
                                        </div>

                                    </div>
                                </div>
                                <div id="divAddBatches" runat="server" class="table-responsive">
                                    <div id="divCustomerDeliveryNote" runat="server">
                                        <asp:GridView ID="gvLineItems" runat="server" AutoGenerateColumns="false"
                                            DataKeyNames="StoreReturnDetailId" CssClass="table table-bordered table-striped table-actions"
                                            OnRowCommand="gvLineItems_RowCommand" OnRowDataBound="gvLineItems_RowDataBound"
                                            OnRowDeleting="gvLineItems_RowDeleting" OnRowEditing="gvLineItems_RowEditing">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Category">

                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="form-control select"
                                                            TabIndex="-1">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="StoreReturnDetailId" runat="server" Value='<%#Eval("StoreReturnDetailId")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Brand">

                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="true" CssClass="form-control select"
                                                            OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" Visible="false">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control" AutoComplete="on"
                                                            TabIndex="-1"></asp:TextBox>
                                                        <asp:HiddenField ID="HDBrandID" runat="server" Value='<%#Eval("BrandID")%>' />
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
                                                        <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control" AutoComplete="on"
                                                            AutoPostBack="true" OnTextChanged="txtProduct_ontextchanged"></asp:TextBox>
                                                        <%-- " --%>
                                                        <asp:HiddenField ID="HDProductID" runat="server" Value='<%#Eval("ProductID")%>' />
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="ACStoreReturnProduct"
                                                            ServicePath="~/Screens/AutoComplete.asmx" MinimumPrefixLength="1" CompletionInterval="100"
                                                            EnableCaching="false" CompletionSetCount="10" TargetControlID="txtProduct" FirstRowSelected="false"
                                                            UseContextKey="true" CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </asp:AutoCompleteExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Selling Price">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtProductValue" runat="server" Enabled="false" CssClass="form-control select"
                                                            Text='<%#Eval("SellingPrice") %>'></asp:TextBox>
                                                        <asp:HiddenField ID="HDBuyingPrice" runat="server" Value='<%#Eval("BuyingPrice") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="100" HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control select" onkeyup="return Calculate1(this);"
                                                            Text='<%#Eval("Quantity") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="30">
                                                    <ItemTemplate>
                                                        <%-- <asp:ImageButton ID="imgDeleteRow" ToolTip="Delete Row" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
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
                                    <div id="STRCalculation" runat="server" visible="false">
                                        <div class="row top">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Total Selling Price</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox CssClass="form-control" ID="txtSellingprice" runat="server" ReadOnly="false" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">
                                                        Total Quantity</label>
                                                    <div class="col-md-8">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                            <asp:TextBox CssClass="form-control" ID="txtTotalQuantity" runat="server" />
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
                           

                        </div>
                    </form>
                </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
             <div id="divprintarea1" class="modal" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-lg modal-position">
                                    <div class="modal-content" id="printdiv">
                                        <div class="modal-header">
                                            <center>
                                                <h2 class="modal-title">Store Return</h2>
                                            </center>
                                            <br />
                                            <br />
                                            <table width="800px" align="center" id="Table1">
                                                <tr>
                                                    <td style="width: 30px"></td>
                                                    <td align="left">
                                                        <label id="lblprintStoreName1" class="Label">
                                                        </label>
                                                        <br />

                                                        <label id="Label17" class="Label">
                                                            Return No:</label>
                                                        <label id="lblprintDeliveryNoteNo1" class="Label">
                                                        </label>
                                                        <br />
                                                        <label id="lblDelNoteDate" class="Label">
                                                            Return Date:</label>
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
                                        </div>
                                        <br />
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
                                               <center> <h4 class="modal-title">Store Return</h4></center>
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
                                                            <%--<div class="row">
                                                                <div class="form-group">--%>
                                                                    <label id="Label1" class="Label pull-left">Return No:</label>
                                                                  <%--  <div class="row">
                                                                        <div class="input-group">--%>
                                                                            <label id="lblprintDeliveryNoteNo" class="Label"></label>
                                                                       <%-- </div>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                        <div class="row">
                                                           <%-- <div class="row">
                                                                <div class="form-group">--%>
                                                                    <label id="Label3" class="Label pull-left">Return Date:</label>
                                                                  <%--  <div class="row">
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
                                                    <div class="panel-body panel-body-table" style="border:hidden !important;">
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

