<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="PettyExpenses.aspx.cs" Inherits="Screens_PettyExpenses" Title="Petty Expenses" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


<script language="javascript" type="text/javascript">
    function show()
    {
        document.write("<head id="Head1" runat='server'></head>");
    }
</script>
    <style type="text/css">
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
    <script type="text/javascript">
        function applystyle() {
            //if (document.getElementById("ctl00_ContentPlaceHolder1_txtAddExpense").value == "")
            //    alert("Please Enter Expense type.");
            //return false;

        }
        function removestyle() {

        }
    </script>
    <script type="text/javascript">
        function validation() {
            var Store = document.getElementById('<%=ddlStore.ClientID %>').selectedIndex;
            var Month = document.getElementById('<%=ddlMonth.ClientID %>').selectedIndex;
            var Year = document.getElementById('<%=ddlyear.ClientID %>').selectedIndex;
            if (Store == 0) {
                alert('Please Select a Store');
                return false;
            }

            var expensetype;
            var gridData = document.getElementById('<%=gvPettyExpenses.ClientID %>');
            var gridRowcount = document.getElementById('<%=gvPettyExpenses.ClientID %>').rows.length;
            for (var i = 2; i < gridRowcount; i++) {
                if (i < 10) {
                    expensetype = document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl0" + i + "_ddlExpense").value;
                }
                else {
                    expensetype = document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + i + "_ddlExpense").value;
                }
                if (expensetype == "") {
                    alert('Please select a Expense');
                    return false;
                }
            }
        }

        function Payment(aa) {
            if (!isNaN(aa.value) && aa.value != "") {
                var gridpaymentData = document.getElementById('<%=gvPettyPayment.ClientID %>');
                var gridpaymentRows = document.getElementById('<%=gvPettyPayment.ClientID %>').rows.length;
                var Amount = document.getElementById('<%=txtPopupAmount.ClientID %>').value;
                var Balance;
                var PayAmount;
                var paidamount = parseFloat(aa.value);
                var id = aa.id.split('_')[3].split("l")[1];
                var tempid = parseInt(id);
                if (id == "02") {
                    if (paidamount > Amount) {
                        alert('Please Enter Valid Amount');
                        return false;
                    }
                    else {
                        Balance = parseFloat(Amount - paidamount);
                        document.getElementById("ctl00_ContentPlaceHolder1_gvPettyPayment_ctl" + id + "_txtPettyBalance").value = Balance;

                    }
                }
                else {
                    if (tempid < 10) {
                        var bal;
                        bal = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvPettyPayment_ctl0" + (tempid - 1) + "_txtPettyBalance").value);

                        if (paidamount > bal) {
                            alert('Payment amount Cannot be greater than balance.');
                            aa.value = "0.00";
                            return false;
                        }    //inner if end
                        document.getElementById("ctl00_ContentPlaceHolder1_gvPettyPayment_ctl" + id + "_txtPettyBalance").value = parseFloat(bal - paidamount).toFixed(2);
                    } //if end
                    else {
                        bal = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvPettyPayment_ctl" + (tempid - 1) + "_txtPettyBalance").value);
                        if (paidamount > bal) {
                            alert('Payment amount Cannot be greater than balance.');
                            aa.value = "0.00";
                            return false;
                        }
                        document.getElementById("ctl00_ContentPlaceHolder1_gvPettyPayment_ctl" + id + "_txtPettyBalance").value = parseFloat(bal - paidamount).toFixed(2);
                    } //else end
                } //outer else end
            } //outer if end
            else {
                alert("Enter Positive numeric value.");
                aa.value = "0.00";
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function CalculateBalance(aa) {
            var id = aa.id.split('_')[3].split("l")[1];
            var paidamount = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_hdamountpaid").value);
            if (aa.value != "" && (!isNaN(aa.value))) {
                id = aa.id.split('_')[3].split("l")[1];
                var balance = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_hdBalance").value);
                var amount = parseFloat(aa.value);

                if (paidamount == 0)
                    document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_txtAmountPaid").value = aa.value;
                else
                    document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_txtAmountPaid").value = parseFloat(aa.value) + paidamount;
                if (amount > balance) {
                    alert("Pay amount cannot be greater than Balance.");
                    aa.value = "";
                    if (paidamount == 0)
                        document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_txtAmountPaid").value = "0.00";
                    else
                        document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_txtAmountPaid").value = paidamount;
                    document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_txtBalance").value = balance;

                    return false;
                }
                if (balance - amount > 0) {
                    document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_txtBalance").value = (balance - amount).toFixed(2);


                }
                else {
                    document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_txtBalance").value = 0.00;
                }
            }
            else {
                aa.value = "";
                if (paidamount == 0)
                    document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_txtAmountPaid").value = "0.00";
                else
                    document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_txtAmountPaid").value = paidamount;
                var balance = parseFloat(document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_hdBalance").value);
                document.getElementById("ctl00_ContentPlaceHolder1_gvPettyExpenses_ctl" + id + "_txtBalance").value = balance;
            }
        }
        function CheckSaving() {
            var gridpaymentData = document.getElementById('<%=gvPettyPayment.ClientID %>');
            var gridpaymentRows = document.getElementById('<%=gvPettyPayment.ClientID %>').rows.length;
            var mode, pa;
            for (var i = 2; i < gridpaymentRows; i++) {
                if (i < 10) {
                    mode = document.getElementById("ctl00_ContentPlaceHolder1_gvPettyPayment_ctl0" + i + "_ddlPaymentMode").value;
                    pa = document.getElementById("ctl00_ContentPlaceHolder1_gvPettyPayment_ctl0" + i + "_txtPettyPaymentAmount").value;
                    if (mode == "0" && pa != "0.00") {
                        alert("Please Select Payment Mode.");
                        return false;
                    }
                    if (pa == "0.00" && mode != "0") {
                        alert("Please Enter Payment amount.");
                        return false;
                    }
                }
                else {
                    mode = document.getElementById("ctl00_ContentPlaceHolder1_gvPettyPayment_ctl" + i + "_ddlPaymentMode").value;
                    pa = document.getElementById("ctl00_ContentPlaceHolder1_gvPettyPayment_ctl" + i + "_txtPettyPaymentAmount").value;
                    if (mode == "0" && pa != "0.00") {
                        alert("Please Select Payment Mode.");
                        return false;
                    }
                    if (pa == "0.00" && mode != "0") {
                        alert("Please Enter Payment amount.");
                        return false;
                    }
                }
            }
            return true;
        }
    </script>

    <form runat="server" id="form">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <form class="form-horizontal">
                        <div class="panel panel-default">
                            <!-- Screen Heading  Start-->
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <strong>Petty Expenses</strong>
                                </h3>                                
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

                            <div id="pnlAdd" runat="server" class="page-content-wrap">
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Store</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlStore" runat="server"
                                                    class="form-control select" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"
                                                    Style="margin-bottom: 12px;">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Month</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlMonth" runat="server"
                                                    class="form-control select" AutoPostBack="true"
                                                    Style="margin-bottom: 12px;" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">
                                                Year</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="ddlyear" runat="server"
                                                    class="form-control select" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlyear_SelectedIndexChanged"
                                                    Style="margin-bottom: 12px;">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row top">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                        <div class="col-md-8">
                                            <label class=" control-label">
                                                Include Pending Payables Of Previous Month</label>
                                                <asp:CheckBox ID="chkOldPendings" runat="server" Checked="false"
                                                    AutoPostBack="True" OnCheckedChanged="chkOldPendings_CheckedChanged" />
                                                    </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div id="pnlGrid" runat="server" align="center" class="table-responsive">
                                    <div class="gridclass">
                                        <asp:GridView ID="gvPettyExpenses" runat="server" AutoGenerateColumns="false"
                                            DataKeyNames="PettyExpenseDetailsID" CssClass="table  table-bordered table-striped table-actions"
                                            OnRowCommand="gvPettyExpenses_RowCommand" OnRowDataBound="gvPettyExpenses_RowDataBound"
                                            OnRowDeleted="gvPettyExpenses_RowDeleted" Width="1081px"
                                            OnRowDeleting="gvPettyExpenses_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField  HeaderText="Expense"  ItemStyle-Width="160px">
                                                  <%--  <ControlStyle Width="170px" />
                                                    <ItemStyle Width="170px" />--%>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnpettyexpensedetailsID" runat="server"
                                                            Value='<%#Eval("PettyExpenseDetailsID")%>' />
                                                        <asp:DropDownList ID="ddlExpense" runat="server" AutoPostBack="true"
                                                            CssClass="form-control select"
                                                            Visible="false">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtExpenses" runat="server" CssClass="form-control"
                                                            AutoComplete="on" style="width:120px"
                                                             AutoPostBack="true" OnTextChanged="txtExpenses_ontextchanged"></asp:TextBox>
                                                        <asp:ImageButton ID="imgPettyExpenses" runat="server" ImageUrl="~/images/plus.png"
                                                        Style="width: 20px; float: right;"
                                                            OnClick="imgPettyExpenses_onclick" OnClientClick="return applystyle();" />
                                                        <asp:HiddenField ID="HDExpenseTypeID" runat="server" />
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1"
                                                            runat="server" ServiceMethod="AutoCompleteExpenseRequest"
                                                            ServicePath="~/Screens/AutoComplete.asmx" MinimumPrefixLength="1"
                                                            CompletionInterval="100"
                                                            EnableCaching="false" CompletionSetCount="10" TargetControlID="txtExpenses"
                                                            FirstRowSelected="false"
                                                            CompletionListCssClass="completionList"
                                                            CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </asp:AutoCompleteExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtInvoiceNo" runat="server" Text='<%#Eval("InvoiceNo")%>'
                                                            class="form-control"
                                                            Style="width: 90px;"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="180px">
                                                    <ItemTemplate>
                                                     <%--   <asp:TextBox ID="txtInvoiceDate" runat="server" Text='<%#Eval("InvoiceDate") %>'
                                                            class="form-control" Style="width: 80px;"></asp:TextBox>
                                                        <asp:Image ID="imgCal" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                        <asp:CalendarExtender ID="Calctrl" Format="dd-MM-yy"
                                                            runat="server" TargetControlID="txtInvoiceDate"
                                                            PopupButtonID="imgCal">
                                                        </asp:CalendarExtender>--%>
                                                         <div class="input-group pull-right">
                                                        <asp:TextBox ID="txtInvoiceDate" CssClass="form-control datepicker" Text='<%#Eval("InvoiceDate") %>'
                                                            runat="server"></asp:TextBox>
                                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payment Due Date" ItemStyle-Width="160px"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                   <%--     <asp:TextBox ID="" runat="server" Text='<%#Eval("PaymentDueDate") %>'
                                                            class="form-control" Style="width: 80px;"></asp:TextBox>
                                                        <asp:Image ID="imgCalDueDate" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                        <asp:CalendarExtender ID="Calctrl1" Format="dd-MM-yy"
                                                            runat="server" TargetControlID="txtPaymentDueDate"
                                                            PopupButtonID="imgCalDueDate">
                                                        </asp:CalendarExtender>--%>

                                                         <div class="input-group pull-right">
                                                        <asp:TextBox ID="txtPaymentDueDate" CssClass="form-control datepicker" Text='<%#Eval("PaymentDueDate") %>'
                                                            runat="server"></asp:TextBox>
                                                        <span class="input-group-addon"><span
                                                            class="fa fa-calendar"></span></span>
                                                    </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount"  ItemStyle-Width="160px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" Text='<%#Eval("ExpenseAmount") %>'
                                                            class="form-control"
                                                            AutoPostBack="True" OnTextChanged="txtAmount_TextChanged1"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pay Amount"  ItemStyle-Width="160px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPayAmount" runat="server" CssClass="form-control"
                                                            onkeyup="return CalculateBalance(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount Paid"  ItemStyle-Width="160px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmountPaid" runat="server" Text='<%#Eval("AmountPaid")%>'
                                                            Enabled="false"
                                                            class="form-control"></asp:TextBox>
                                                        <asp:HiddenField ID="hdamountpaid" runat="server" Value='<%#Eval("AmountPaid") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance"  ItemStyle-Width="160px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBalance" runat="server" Text='<%#Eval("Balance") %>'
                                                            Enabled="false"
                                                            class="form-control"></asp:TextBox>
                                                        <asp:HiddenField ID="hdBalance" runat="server" Value='<%#Eval("Balance")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status"  ItemStyle-Width="160px">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select"
                                                            Enabled="false">
                                                            <asp:ListItem Value="OPEN">OPEN</asp:ListItem>
                                                            <asp:ListItem Value="PARTIAL">PARTIAL</asp:ListItem>
                                                            <asp:ListItem Value="CLOSED">CLOSED</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="0px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPay" runat="server" Text="Details"
                                                            Style="color: Blue; text-decoration: underline; margin-left: 0px"
                                                            CommandName="pay" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:LinkButton>
                                                        <%--<asp:LinkButton ID="" runat="server" ImageUrl="~/Images/cross.png"
                                                            CommandName=""
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex%>"
                                                            OnClientClick='return confirm("Do you want to delete the Expense?");'
                                                            Style="margin-left: 7px;" />--%>
                                                        <asp:LinkButton ID="imgDelete" runat="server" CommandName="Delete"
                                                            OnClientClick="return confirm('Do you want to Delete the record?');" Style="margin-left: 7px;"
                                                            CommandArgument='<%#((GridViewRow)Container).RowIndex%>'>
                                                         <span class="fad fa-times"> </span></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                    </div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <div class="col-md-2" id="save" runat="server">
                                        <asp:Button ID="ImgSave" runat="server" CssClass="btn btn-success btn-block pull-right"
                                            Text="Save" Style="margin-bottom: 5px;" OnClientClick="return validation();this.disabled = true;"
                                            OnClick="ImgSave_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvclear" runat="server">
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-warning btn-block pull-left"
                                            alt="Clear" Text="Clear" Style="margin-bottom: 5px;"
                                            OnClick="btnClear_Click" />
                                    </div>
                                    <div class="col-md-2" id="dvcancel" runat="server">
                                        <asp:Button ID="btncan" runat="server" CssClass="btn btn-danger btn-block pull-left"
                                            Text="Cancel" Style="margin-bottom: 5px;" OnClick="btncan_Click" />
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
                                            <h4 class="modal-title">Petty Expenses Details</h4>
                                        </div>
                                        <asp:Label ID="lblmsg1" runat="server" Style="color: Red;"></asp:Label>
                                        <asp:Label ID="lblCapPaymentDetailsId" runat="server" Visible="false"></asp:Label>
                                        <div class="modal-body" style="height: 520px; overflow-y: auto;">
                                            <div class="row top">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            Amount</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                                <asp:TextBox ID="txtPopupAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5" style="display: none">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">
                                                            InvoiceNo</label>
                                                        <div class="col-md-8">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-search"></span></span>
                                                                <asp:TextBox ID="txtPopupInvoiceNo" runat="server" CssClass="form-control" Enabled="false"
                                                                    Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div id="gridclass" class="table-responsive">
                                                <asp:GridView ID="gvPettyPayment" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-actions"
                                                    DataKeyNames="PettyExpensePaymentsID" OnRowCommand="gvPettyPayment_RowCommand"
                                                    OnRowDataBound="gvPettyPayment_RowDataBound" PagerStyle-CssClass="pgr">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Payment Date" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnPettyExpensePaymentsID" runat="server"
                                                                    Value='<%#Eval("PettyExpensePaymentsID") %>' />
                                                                <%--       <asp:TextBox ID="" runat="server"
                                                        CssClass="TextBoxStyle"
                                                        Enabled="false" Style="width: 120px;" Text='<%#Eval("PaymentDate") %>'></asp:TextBox>
                                                    <asp:Image ID="imgCalPaymentDueDate" runat="server"
                                                        ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                                    <asp:CalendarExtender ID="CalPaymentDueDate" runat="server"
                                                        Format="dd-MM-yy"
                                                        PopupButtonID="imgCalPaymentDueDate" TargetControlID="txtPopupPaymentDate">
                                                    </asp:CalendarExtender>--%>
                                                                <div class="input-group pull-right">
                                                                    <asp:TextBox ID="txtPopupPaymentDate"
                                                                        CssClass="form-control datepicker" runat="server"></asp:TextBox><%--onchange="CompareDate()"--%>
                                                                    <span class="input-group-addon"><span
                                                                        class="fa fa-calendar"></span></span>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Mode" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlPaymentMode" runat="server"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMode_selectedindexchanged"
                                                                    CssClass="DropDownClass">
                                                                    <asp:ListItem Value="0">--Any--</asp:ListItem>
                                                                    <asp:ListItem Value="CASH">CASH</asp:ListItem>
                                                                    <asp:ListItem Value="CARD">CARD</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Amount" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPettyPaymentAmount" runat="server"
                                                                    CssClass="TextBoxStyle" Text='<%#Eval("PaymentAmount") %>'
                                                                    onchange="return Payment(this);"></asp:TextBox>
                                                                <asp:HiddenField ID="hdcount1" runat="server" Value="1" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Receipt No" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPettyRecieptNo" runat="server" CssClass="TextBoxStyle"
                                                                    Text='<%#Eval("ReceiptNo") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Balance" ItemStyle-CssClass="middle" ItemStyle-Width="30">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPettyBalance" runat="server" CssClass="TextBoxStyle"
                                                                    Enabled="false" Text='<%#Eval("Balance") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <div runat="server" align="center" class="row top">
                                                <asp:Button ID="btnSavePayments" runat="server" Text="Save" CssClass="btn btn-info"
                                                    OnClientClick="return CheckSaving();this.disabled = true;" OnClick="btnSavePayments_Click" />
                                                <asp:Button ID="btnCancelPayments" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                                    OnClick="btnCancelPayments_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="dvExpensePopUP" style="display: none; z-index: 1040;" class="modal fade in"
                                role="dialog" tabindex="-1"
                                aria-hidden="false" runat="server">
                                <div class="modal-backdrop fade in" style="height: 100%;">
                                </div>
                                <div class="modal-dialog modal-lg ">
                                    <div class="modal-content" style="height: 600px;">
                                        <div class="modal-header">
                                            <h4 class="modal-title">Expenses </h4>
                                        </div>

                                        <div class="modal-body" style="height: 520px; overflow-y: auto;">
                                            <div class="panel-body" id="Panel1">

                                                <div class="row top">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">
                                                                Expense Type</label>
                                                            <div class="col-md-8">
                                                                <div class="input-group">
                                                                    <%-- <span class="input-group-addon"><span class="fa fa-search"></span></span>--%>
                                                                    <asp:TextBox ID="txtAddExpense" runat="server" CssClass="form-control" ></asp:TextBox>
                                                                </div>
                                                                <asp:HiddenField ID="HiddenFieldExpeseTypeID" runat="server" />
                                                                <asp:RequiredFieldValidator ID="rfvAddExpense" runat="server" ValidationGroup="r"
                                                                    ControlToValidate="txtAddExpense" Text="*" ErrorMessage="Please Insert Expense Type.">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="Panel2" runat="server" align="center" class="row top">
                                                    <asp:Button ID="imgAddExpense" runat="server" Text="Save" CssClass="btn btn-info" ValidationGroup="r"
                                                        OnClientClick="return removestyle();this.disabled = true;" OnClick="imgAddExpenses_onclick" />
                                                    <asp:Button ID="btnExpensesCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                                        OnClick="btnExpensesCancel_onclick" OnClientClick="return removestyle();"/>
                                                </div>
                                          
                                                <center>
                                                    <div id="dvSummary" align="center">
                                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="r"
                                                            CssClass="validationSummary" />
                                                    </div>
                                                </center>
                                            </div>
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
