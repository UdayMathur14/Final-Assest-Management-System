<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AMCDetail.aspx.cs" Inherits="TileMenu.AMCDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <style>
         .uday {
     padding-top: 70px;
     min-height: 100vh;
     border: 2px solid black;
     width: 100%;
     background-image: url('/Images/try3.jpeg');
     background-repeat: no-repeat;
     background-size: cover;
 }
    </style>
    <div class="uday">
    <div style="width: 70%; margin-inline: auto">
     <div class="logine">
         

         <asp:Panel ID="Panel1" runat="server" Width ="90%"> 
         
         <fieldset><legend><b>AMC Detail :</b></legend>
         
         <table border="1">
         <tr>
            <td><label>PO Number:</label></td>
            <td><asp:DropDownList ID="ddlPO"  runat ="server" Width ="150px" Height="25px"
                         AutoPostBack="True" onselectedindexchanged="ddlPO_SelectedIndexChanged"></asp:DropDownList></td>
            <td><label>Bill Start Date:</label></td>
             <td>
                 <asp:TextBox ID="txtStartdate" runat="server" class="datepicker" Width="150px" Height="25px"></asp:TextBox> dd/mm/yyyy
             </td>
         </tr>
         <tr>
            <td><label>Bill End Date:</label></td>
            <td> <asp:TextBox ID="txtenddate" class="datepicker" runat ="server" Width ="150px" Height="25px"></asp:TextBox>dd/mm/yyyy </td>
            <td><asp:Button ID="Button1" runat="server" Text="Calculate Period" onclick="Button1_Click" /></td>
            <td><asp:TextBox ID="txtPeriod"  runat ="server" Width ="150px" Height="25px" Enabled="False"></asp:TextBox>Days</td>
         </tr>
         <tr>
            <td><label>Invoice Number:</label></td>
            <td> <asp:TextBox ID="txtinvno"  runat ="server" Width ="150px" Height="25px"></asp:TextBox></td>
            <td><label>Invoice Date:</label></td>
            <td> <asp:TextBox ID="txtinvdate" class="datepicker" runat ="server" Width ="150px" Height="25px"></asp:TextBox>mm/dd/yyyy </td>
         </tr>
         <tr>
            <td> <label>Invoice Value:</label></td>
            <td> <asp:TextBox ID="txtordervalue"  runat ="server" Width ="150px" Height="25px"
                       AutoPostBack="True" ontextchanged="txtordervalue_TextChanged"  ></asp:TextBox></td>
            <td><label>Product Name :<label><label id="lbllp" runat="server" visible="false">Service Tax Loss Percentage(%):</label></td>
            <td><asp:TextBox ID="txtproduct"  runat ="server" Width ="200px" Height="25px"></asp:TextBox><asp:TextBox ID="txtservicelosspercent"  runat ="server" Width="150px" Height="25px" AutoPostBack="True" ontextchanged="txtservicelosspercent_TextChanged" visible="false"  ></asp:TextBox></td>
         </tr>
         <tr>
            <td> <label>CGST Percentage(%):</label></td>
            <td><asp:TextBox ID="txtservicepercent"  runat ="server" Width ="150px" Height="25px"
                         AutoPostBack="True" ontextchanged="txtservicepercent_TextChanged"  ></asp:TextBox></td>
            <td><label>CGST Amount:</label></td>
            <td><asp:TextBox ID="txtserviceamount"  runat ="server" Width ="150px" Height="25px"
                       Enabled="False"  ></asp:TextBox></td>
         </tr>
         <tr>
            <td><label>SGST Percentage(%):</label></td>
            <td><asp:TextBox ID="txtcesspercent"  runat ="server" Width ="150px" Height="25px" AutoPostBack="True" 
                         ontextchanged="txtcesspercent_TextChanged"  ></asp:TextBox></td>
            <td><label>SGST Amount:</label></td>
            <td><asp:TextBox ID="txtcessamount"  runat ="server" Width ="150px" Height="25px" Enabled="False"  ></asp:TextBox></td>
         </tr>
         <tr>
            <td><label>IGST Percentage(%):</label></td>
            <td><asp:TextBox ID="txtvatpercent"  runat ="server" Width ="150px" Height="25px" AutoPostBack="True" 
                         ontextchanged="txtvatpercent_TextChanged"  ></asp:TextBox></td>
            <td><label>IGST Amount:</label></td>
            <td><asp:TextBox ID="txtvatamount"  runat ="server" Width ="150px" Height="25px" Enabled="False"  ></asp:TextBox></td>
         </tr>
         <tr>
            <td> <label>Total GST Amount:</label></td>
            <td><asp:TextBox ID="txtservicelossamount"  runat ="server" Width ="150px" Height="25px"
                       Enabled="False"  ></asp:TextBox></td>
            <td><label>Total Value:</label></td>
            <td><asp:TextBox ID="txttotalalvalue"  runat ="server" Width ="150px" Height="25px" Enabled="False"  ></asp:TextBox></td>
         </tr>
       
         <tr>
            <td colspan="4" align="center"><asp:Button ID="btnsubmit" runat="server"  
                        Text="Submit" Width="76px"  Height="26px" onclick="btnsubmit_Click" /></td>
            
         </tr>
         </table>
     </fieldset>
      </asp:Panel>
     
     
     <asp:Panel id="pnlData" runat="server" >
    
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
             ForeColor="Black" GridLines="Both"  BackColor="White" 
                            BorderColor="#666666" BorderStyle="Solid" 
             BorderWidth="1px"  Width ="80%" CellPadding="1" DataKeyNames="AMC_ID" 
             onrowdatabound="GridView1_RowDataBound" >
        <FooterStyle BackColor="#E3E3E1" />
        <RowStyle BackColor="#E3E3E1" />
        <HeaderStyle BackColor="#D72B37" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
        <asp:BoundField  DataField="AMC_PONO" HeaderText="PO Number" />
        <asp:BoundField  DataField="PODate" HeaderText="PO Date" />
        <asp:BoundField  DataField="POStartDate" HeaderText="PO Start Date" />
        <asp:BoundField  DataField="POEndDate" HeaderText="PO End Date" />
        <asp:BoundField  DataField="AMC_InvNo" HeaderText="Invoice Number" />
        <asp:BoundField  DataField="InvDate" HeaderText="Invoice Date" />
        <asp:BoundField  DataField="StartDate" HeaderText="Invoice Start Date" />
        <asp:BoundField  DataField="Enddate" HeaderText="Invoice End Date" />
        <asp:BoundField  DataField="AMC_Period" HeaderText="Duration(Days)" />
        <asp:BoundField  DataField="Vendor_name" HeaderText="Vendor Name" />
        <asp:BoundField  DataField="AMC_Product" HeaderText="Product" />
        <asp:BoundField DataField="AMCMaster_POValue" HeaderText="PO Value" />
        <asp:BoundField DataField="AMC_OrderValue" HeaderText="Invoice Value" />
        <asp:BoundField DataField="AMC_STaxPer" HeaderText="CGST" />
        <asp:BoundField DataField="AMC_CTaxPer" HeaderText="SGST" />
        <asp:BoundField DataField="AMC_VATPer" HeaderText="IGST" />
        <asp:BoundField DataField="AMC_Total" HeaderText="Total" />

        
        <asp:TemplateField HeaderText="Detail">
         
            <ItemTemplate>
                  
                    <asp:GridView ID="gvdetail" runat="server" AutoGenerateColumns="false" Width="70%">
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />

                        <Columns>

                            <asp:BoundField  DataField="AMCTrans_Year" HeaderText="Year" />

                            <asp:BoundField  DataField="AMCTrans_Days" HeaderText="Days" />
                             <asp:BoundField  DataField="AMCTrans_Value" HeaderText="Value" />

                        </Columns>

                    </asp:GridView>

               
            </ItemTemplate>

        </asp:TemplateField>

    </Columns>
        
        
         </asp:GridView>
         
     </asp:Panel>
     
    </div>
    </div>
    </div>
    <script>
    $(function () {
        $('.datepicker').each(function () {
            $(this).datepicker({
                format: "dd/M/yyyy",
                clearBtn: true
            }).on('changeDate', function () {
                $(this).datepicker('hide');
            });
        })
    });
    </script>
</asp:Content>
