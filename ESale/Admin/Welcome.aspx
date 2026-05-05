<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="Admin_Welcome"
    MasterPageFile="~/Admin/Admin.master" %>


<%--<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolderTitle" >
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript">
       
    </script>
        <link href="../css/NewStyles.css" rel="stylesheet" type="text/css" />  
   
 </asp:Content>--%>
 <asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content4">
   
    <center>
     <asp:Label style="font-family: Georgia;font-size: xx-large;color: green;font-weight: bold;text-align: left;" ID="lblMsg"
      runat="server" Text=""></asp:Label><br /><br />
       <%-- <img src="../images/Logo.png" style="height:100px;"  />--%>
         <div >
                <asp:Image ID="imgStore" runat="server" style="height:50px;" AlternateText="Store Image" />
             <asp:Image ID="Image1" runat="server" style="height:50px;" AlternateText="Store Image" />
             <asp:Image ID="Image2" runat="server" style="height:50px;" AlternateText="Store Image" />
               </div>
    </center>
  
   <%-- <table align="center" style="display:none">
        <tr>
            <td><asp:Image ID="Image1" runat="server" style="height:175px;width:300px;" /></td>
            <td><asp:Image ID="Image2" runat="server" style="height:175px;width:300px;" /></td>
        </tr>
        <tr>
            <td><asp:Image ID="Image3" runat="server" style="height:175px;width:300px;"/></td>
            <td><asp:Image ID="Image4" runat="server" style="height:175px;width:300px;"/></td>
        </tr>
    </table>--%>
   
 </asp:Content>