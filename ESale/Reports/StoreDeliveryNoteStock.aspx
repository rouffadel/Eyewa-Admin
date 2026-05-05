<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="StoreDeliveryNoteStock.aspx.cs"
    Inherits="Reports_StoreDeliveryNoteStock" Title="Store Delivery Note" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ToolkitScriptManager ID="tsm" runat="server"> </asp:ToolkitScriptManager>--%>
    <asp:Label ID="lblTitle" Text="Store Delivery Note Report" runat="server" CssClass="TitleClass"
        Visible="false"></asp:Label>
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function StoreForPrint(storedeliverynoteid) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: "{Storeid:'" + storedeliverynoteid + "'}",
                dataType: "json",
                url: "StoreDeliveryNoteStock.aspx/StoreForPrint",
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
                                $("#tblStorePrint > tbody").append("<tr style='font-size:12px;font-family:verdana;color:White;background-color:black;'><td><lable style='color:black;'>" + Supplierlist[i].CategoryName + "</label></td><td><lable style='color:black;'>" + Supplierlist[i].BrandName + "</label></td><td><lable style='color:black;'>" +
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


                            $("#tblStorePrint > tfoot").append("<tr style='background-color:White;height:20px'>" +
                                           "<td colspan='6'><b></b></td>" +
                                          "</tr>");
                            $("#tblStorePrint > tfoot").append("<tr>" +
                                           "<td align='center' colspan='6'>Details:</td>" +
                                          "</tr>");
                            $("#tblStorePrint > tfoot").append("<tr style='background-color:White;'>" +
                                                                 "<td align='left' colspan='6' rowspan='1'>Remarks:</td>" +

                                                                      "</tr>");

                            $("#tblStorePrint > tfoot").append("<tr>" +
                                                                 "<td align='left' colspan='4' rowspan='5'>" + Remarks + "</td>" +

                                                                      "</tr>");
                            $("#tblStorePrint > tfoot").append("<tr >" +
                                                               "<td align='right' colspan='1'>Total Selling Price:</td>" +
                                                                 "<td align='center' style='font-size:12px;font-family:verdana;'>" + TotalGrossValue + "</td>" +
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
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Store Delivery Note Report</strong>
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
                                            <label class="col-md-4 control-label">
                                                Store</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlStore" runat="server"
                                                    class="form-control select" >
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                     
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                StoreDeliveryNote No</label>
                                            <div class="col-md-8">
                                                <div class="input-group">
                                                     <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                    <asp:TextBox CssClass="form-control" ID="txtStoreDeliveryNoteNo" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                
                            </div>
                            <div class="row top">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label">
                                            Category
                                        </label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlCategory" runat="server"
                                                class="form-control select" Style="margin-bottom: 12px;" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label">
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
                                        <label class="col-md-4 control-label">
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
                                        <label class="col-md-4 control-label">
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
                                        <label class="col-md-4 control-label">
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
                                        <label class="col-md-4 control-label">
                                        </label>
                                        <div class="col-md-8">
                                            <asp:RadioButton ID="rbtnSummary" runat="server" Checked="true"
                                                CssClass="Label" GroupName="SDN"
                                                OnCheckedChanged="rbtnSummary_CheckedChanged" AutoPostBack="true" />
                                            <asp:Label ID="Label2" runat="server" Text="Summary" CssClass="Label"></asp:Label>
                                            <asp:RadioButton ID="rbtnDetailed" runat="server" Text=""
                                                Checked="false" CssClass="Label" GroupName="SDN"
                                                OnCheckedChanged="rbtnDetailed_CheckedChanged" AutoPostBack="true" />
                                            <asp:Label ID="Label4" runat="server" Text="Detailed" CssClass="Label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label">
                                        </label>
                                        <div class="col-md-8">
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
                                                <asp:Button ID="btnReport" runat="server" Text="Search"
                                                    CssClass="btn btn-info" OnClick="btnReport_Click" />&nbsp;&nbsp;
                                                        <asp:Button ID="imgbtnClear" runat="server" Text="Clear"
                                                            CssClass="btn btn-warning" OnClick="imgbtnClear_Click" />
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <br />
                            <div ID="pnlGrid" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvStock" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                        CssClass="table datatable table-bordered table-striped table-actions"
                                        OnRowDataBound="gvStock_RowDataBound" DataKeyNames="StoreDeliveryNoteID"
                                        OnRowCommand="gvStock_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="SNo" Visible="false" />
                                            <%-- <asp:BoundField DataField="StoreName" HeaderText="Store Name" />--%>
                                            <asp:TemplateField HeaderText="Store Name"
                                                ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStoreName" runat="server" Text='<%#Bind("StoreName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotal" runat="server" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StoreDeliveryNoteNo" HeaderText="Store DeliveryNote No"
                                                ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                            <%--<asp:BoundField DataField="StoreDeliveryNoteDate" HeaderText="Store DeliveryNote Date" />--%>
                                            <asp:TemplateField HeaderText="Store DeliveryNote Date" FooterStyle-HorizontalAlign="Right"
                                                ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStoreDeliveryNoteDate" runat="server" Text='<%#Bind("StoreDeliveryNoteDate") %>'></asp:Label>
                                                </ItemTemplate>
                                                <%-- <FooterTemplate>
                                                    <asp:Label ID="lbltotal" runat="server" Text="Total"></asp:Label>
                                                </FooterTemplate>--%>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalQuantity" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Selling Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSellingprice" runat="server" Text='<%# Bind("SellingPrice") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalSellingprice" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkbtnPrint" runat="server" ImageUrl="~/images/print.jpg" Width="20px"
                                                        Height="20px" ToolTip="Print" Text="Print" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                        CommandName="Print" ValidationGroup="False" />
                                                </ItemTemplate>
                                                <%-- <HeaderStyle />
                                                <ItemStyle HorizontalAlign="Center" />--%>
                                            </asp:TemplateField>
                                        </Columns>
                                        <%-- <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                        <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                        <HeaderStyle Height="25px" HorizontalAlign="Center" />--%>
                                    </asp:GridView>
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                        CssClass="table datatable table-bordered table-striped table-actions"
                                        OnRowDataBound="gvDetails_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="SNo" Visible="false" />
                                            <%--  <asp:BoundField DataField="StoreName" HeaderText="Store Name" />--%>
                                            <asp:TemplateField HeaderText="Store Name"
                                                ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStoreName" runat="server" Text='<%#Bind("StoreName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotal" runat="server" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StoreDeliveryNoteNo" HeaderText="Store DeliveryNote No"
                                                ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                            <asp:BoundField DataField="StoreDeliveryNoteDate" HeaderText="Store DeliveryNote Date"
                                                ItemStyle-CssClass="middle" ItemStyle-Width="30" />
                                            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-CssClass="middle"
                                                ItemStyle-Width="30" />
                                            <asp:BoundField DataField="BrandName" HeaderText="Brand Name" ItemStyle-CssClass="middle"
                                                ItemStyle-Width="30" />
                                            <asp:TemplateField HeaderText="Model No" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProductName") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblText" runat="server" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Selling Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSellingPrice" runat="server" Text='<%# Bind("SellingPrice") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalSellingPrice" runat="server" Visible="false"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalQuantity" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Tot.Selling Price" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetSellingPrice" runat="server" Text='<%# Bind("TotalSellingPrice") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalNetSellingPrice" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="middle" ItemStyle-Width="30"
                                                Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkbtnPrint" runat="server" ImageUrl="~/images/print.jpg" Width="20px"
                                                        Height="20px" ToolTip="Print" Text="Print" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"
                                                        CommandName="Print" ValidationGroup="False" /><%--OnClick="lnkbtnPrint_Click"--%>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                        </Columns>
                                       <%-- <RowStyle BackColor="#F3F3F3" ForeColor="Black" HorizontalAlign="Center" Height="20px" />
                                        <AlternatingRowStyle ForeColor="Black" BackColor="#ffffff" Height="20px" HorizontalAlign="Center" />
                                        <HeaderStyle Height="25px" HorizontalAlign="Center" />--%>
                                    </asp:GridView>
                                </div>
                                <center>
                                        <asp:Button ID="btnprint" runat="server" Text="Print"  class="btn btn-info" Visible="false"
                                            ToolTip="print report" OnClick="btnprint_Click" />
                                        <asp:Button ID="btnExport" runat="server" Text="Export" class="btn btn-danger dropdown-toggle btnexportprint"
                                            Visible="false" ToolTip="export report" OnClick="btnExport_Click" />
                                    </center>
                               <%-- <center>
                                    <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="BtnEmptyStyle" Visible="false"
                                        ToolTip="print report" OnClick="btnprint_Click" />
                                    <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="BtnEmptyStyle"
                                        Visible="false" ToolTip="export report" OnClick="btnExport_Click" />
                                </center>--%>
                            </div>
                            </div>
                            
                    </form>
                </div>
                <%--<div id="divprintarea" class="modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-position">
                    <div class="modal-content" id="printdiv">
                        <div class="modal-header">
                               
                                <center>
                                <h2 class="modal-title">Store Delivery Note
                                </h2>
                            </center>
                                <br />
                                <br />
                                <table width="800px" align="center" id="Table1">
                                    <tr>
                                        <td style="width: 30px"></td>
                                        <td align="left">
                                            <label id="lblprintStoreName" class="Label">
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
                                </table>
                                </div>
                                <br />
                              <div class="modal-body">
                                    <div id="printSupplierDeliveryNote" class="gridClass1" style="display: block;">
                                        <table width="750px" align="center" id="tblStorePrint" border="1" style="border-spacing: 0px;">
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
                                <asp:Button ID="Cancelbtn" runat="server" Text="Cancel" CssClass="BtnEmptyStyle"
                                    OnClientClick="return removestyle();" />
                                    </div>
                            </div>
                        </div>
                        </div>--%>
                <form name="SubOrgForm" novalidate>
                <div class="modal" id="divprintarea" tabindex="-1" role="dialog" aria-hidden="true" style="height: 100%; overflow-y: auto;">
                    <div class="modal-dialog modal-lg modal-position" id="Div3">
                        <div class="modal-content">
                                <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" style="display:none;"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                <h4 class="modal-title">Storedeliverynote</h4>
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
                                                    <label id="Label1" class="Label pull-left">DeliveryNote No:</label>
                                                   <%-- <div class="row">
                                                        <div class="input-group">--%>
                                                            <label id="lblprintDeliveryNoteNo" class="lblprintDeliveryNoteNo"></label>
                                                        <%--</div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                        </div>
                                        <div class="row">
                                            <%--<div class="row">
                                                <div class="form-group">--%>
                                                    <label id="Label3" class="Label pull-left">DeliveryNote Date:</label>
                                                    <%--<div class="row">
                                                        <div class="input-group">--%>
                                                            <label id="lblDeliveryNoteDate" class="lblDeliveryNoteDate"></label>
                                                       <%-- </div>
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

        </div>

    </form>
</asp:Content>


