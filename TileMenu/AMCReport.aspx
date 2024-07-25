<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AMCReport.aspx.cs" Inherits="TileMenu.AMCReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <style>
         .uday {
     padding-top: 70px;
     min-height: 100vh;
     border: 2px solid black;
     width: 100%;
     background-image: url('/Images/back1.jpg');
     background-image: url('/Images/back1.jpg');
     background-repeat: no-repeat;
     background-size: cover;
 }
    </style>
    <div class="uday">
    <div style="width: 70%; margin-inline: auto">
    <asp:Panel id="pnlData" runat="server" Visible="true">
     <table width="100%">
         <tr><td>
         <asp:RadioButtonList ID="optsearch" runat="server" 
                 RepeatDirection="Horizontal" 
                 onselectedindexchanged="optsearch_SelectedIndexChanged" AutoPostBack="true">
         <asp:ListItem Text="PO Number" Value="0"  Selected="True"></asp:ListItem>
         <asp:ListItem Text="Vendor" Value="1"  ></asp:ListItem>
         <asp:ListItem Text="Product" Value="2"  ></asp:ListItem>
         
         </asp:RadioButtonList></td></tr>

        <tr><td><asp:TextBox ID="txtsearch" runat="server" Visible="true" 
                 ontextchanged="txtsearch_TextChanged" AutoPostBack="true" height="25px" width="150px"></asp:TextBox> 
         </td></tr>
         
         
        <tr><td >
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" 
             ForeColor="Black" GridLines="Both"  BackColor="White" 
                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" 
                        Width ="90%" CellPadding="5" 
             
            >
            
        <FooterStyle BackColor="#E3E3E1" />
        <RowStyle BackColor="#E3E3E1" />
         
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#D72B37" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
         </asp:GridView>
         </td></tr>
        
         </table>
         
     </asp:Panel>

    </div>
    </div>
</asp:Content>
