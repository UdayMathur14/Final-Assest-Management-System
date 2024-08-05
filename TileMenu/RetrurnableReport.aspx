<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RetrurnableReport.aspx.cs" Inherits="TileMenu.RetrurnableReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Panel id="pnlData" runat="server">
<style>
         .uday {
    padding-top: 70px;
    min-height: 100vh;
    border: 2px solid black;
    width: 100%;
    background-image: url('/Images/back1.jpg');
    background-repeat: no-repeat;
    background-size: cover;
    overflow:auto;
}
         .table td, .table th {
    text-align: center;
}
</style>
 
<div class="uday">


<div style="width: 80%; margin-inline: auto; text-align: center;">



 <fieldset><legend><b>Returnable Item Report(Inventory) :</b></legend>
<br>
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" ForeColor="Black" GridLines="Both"  BackColor="White" 
                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" 
                        Width ="100%" CellPadding="12" RowStyle-Height="30px" onrowdatabound="GridView1_RowDataBound">
        <FooterStyle BackColor="#E3E3E1" />
        <RowStyle BackColor="#ffffcc" />
         
         <SelectedRowStyle Width="100%" BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
         <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="45px" Width="100%" />
        <AlternatingRowStyle BackColor="White" />
         </asp:GridView>
</fieldset>
     </asp:Panel>
<asp:Panel id="pnlmanual" runat="server">
    <br />
    <br />
 <fieldset><legend><b>Returnable Item Report(Non Inventory) :</b></legend>
<br>
         <asp:GridView ID="GridViewNI" runat="server" AutoGenerateColumns="false" ForeColor="Black" GridLines="Both"  BackColor="White" 
                            BorderColor="#666666" style="text-align:center" BorderStyle="Solid" BorderWidth="1px" 
                        Width ="100%" CellPadding="5" RowStyle-Height="30px" OnRowDeleting="GridViewNI_RowDeleting" DataKeyNames="Id">
        <FooterStyle BackColor="#E3E3E1" />
        <RowStyle   BackColor="#ffffcc" />
         
         <SelectedRowStyle Width ="100%" BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
         <HeaderStyle Width ="100%" BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
        <AlternatingRowStyle BackColor="White" />
<Columns>
          <asp:BoundField  DataField="GatePassNo" HeaderText="GatePassNO." />
          <asp:BoundField  DataField="IssueDate" HeaderText="Issue Date" />
          <asp:BoundField  DataField="GatePass_IssuedTo" HeaderText="Issued To" />
          <asp:BoundField  DataField="GatePass_IssuedCompany" HeaderText="Issued Company" />
          <asp:BoundField  DataField="GatePass_Detail_Item" HeaderText="Issued Item" />
          
          <asp:BoundField  DataField="GatePass_Detail_Remarks" HeaderText="Remarks" />
          <asp:BoundField  DataField="EXPReturnDate" HeaderText="Exp Rtn Date" />
         

          <asp:BoundField  DataField="id" HeaderText="ID" />
          
          <asp:CommandField  HeaderText="Return" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/imagesreturn.png" />  
         </Columns>

         </asp:GridView>
</fieldset>
    </div>
    </div>
     </asp:Panel>
</asp:Content>
