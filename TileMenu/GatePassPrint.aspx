<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GatePassPrint.aspx.cs" Inherits="TileMenu.GatePassPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <script type="text/javascript">

    
    function CountFun()
    {
     
    }
    function printpage() 
{
     var cnt=0;
     cnt=parseInt(cnt)+parseInt(1);
    var divData=document.getElementById("showCount");
     divData.Text="Number of Downloads: ("+cnt +")";
      
        //Get the print button and put it into a variable
        var printButton = document.getElementById("printButton");
        //Set the print button visibility to 'hidden' 
        printButton.style.visibility = 'hidden';
        //Print the page content
        window.print()
        //Set the print button to 'visible' again 
        //[Delete this line if you want it to stay hidden after printing]
        printButton.style.visibility = 'visible';
   

    }

    
    </script>

    <form id="form1" runat="server">
       <asp:Label ID="showCount" runat="server" Text="0"></asp:Label>

    <div id="gatepass">
     
  
    <table style="width:95%;padding-left:50px">
    <tr>
    <td align="center" >
    <asp:Image  ImageAlign="Middle" ImageUrl="~/BillLogo.png"  runat="server" 
            Height="220px" Width="300px" />
    </td>
    </tr>
    <tr>
    <td align="center" style="font-weight:bold" >
    <%--A-36,Mehtab House,Mohan Co-operative Industrial Estate,Mathura Road--%>
        Ennoble IP B-17, Authority, near Noida, Sector 6, Noida, Uttar Pradesh 201301
    </td>
    </tr>
    <tr>
    <td align="center" style="font-weight:bold" >
     New Delhi - 110044 (India)
    </td>
    </tr>
    <tr>
    <td align="center" style="font-weight:bold" >
    Tel. : +91-11-42101100/1200&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    Website : www.khd.com
    </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    
    <tr>
    <td align="center" style="font-weight:bold;font-size:x-large;text-decoration:underline" >
    GATE PASS
    </td>
    </tr>
    <tr>
    <td align="center" style="font-weight:bold" >
    <asp:Label ID="lblGPtype" runat="server"></asp:Label>
    </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
    <td align="center" style="font-weight:bold;font-size:large;text-decoration:underline">
    <asp:CheckBox ID="O" runat="server" Text="ORIGINAL" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="D" runat="server" Text="DUPLICATE" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="V" runat="server" Text="VENDOR/SITE" />                   
    </td>
    </tr>
    
    <tr><td>&nbsp;</td></tr>
   <tr><td align="right" style="font-weight:bold">Gate Pass No. : <asp:Label runat="server" ID="lblGPNumber"></asp:Label></td></tr>
   <tr><td align="right">Date : <asp:Label runat="server" ID="lbldate"></asp:Label></td></tr>
    <tr><td>To,</td></tr>
    <tr><td style="font-weight:bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Security Guard,</td>
    </tr>
    <tr><td style="font-weight:bold" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Main Gate,</td></tr>
    <tr><td style="font-weight:bold" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Delhi Office</td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td class="style1" ><b>Please allow  Mr.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        : </b>&nbsp;<asp:Label runat="server" ID="lblissuedto" ></asp:Label></td></tr>
    <tr><td ><b>Company Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        :&nbsp; </b><asp:Label runat="server" ID="lblcompany" ></asp:Label></td></tr>
    <tr><td ><b>Delivery Address&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        : </b>&nbsp;<asp:Label runat="server" ID="lbladdress" ></asp:Label></td></tr>
    <tr><td><b>Contact/Mobile&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        :&nbsp; </b><asp:Label runat="server" ID="lblcontact" ></asp:Label></td></tr>
    <tr><td class="style1" ><b>To take out following items for&nbsp; :&nbsp; </b><asp:Label runat="server" ID="lblreason" ></asp:Label></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
    <td >
     <asp:Panel id="pnlData" runat="server" >
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ForeColor="Black" GridLines="Both"  BackColor="White" 
                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px" 
                         CellPadding="5" RowStyle-Height="30px" Width="100%" OnRowDataBound="GridView1_RowDataBound" >
       <Columns>

    <asp:TemplateField HeaderText = "Sr.No." ItemStyle-Width="100">

        <ItemTemplate>

            <asp:Label ID="lblRowNumber" runat="server" />

        </ItemTemplate>

    </asp:TemplateField>

   

</Columns>
        <RowStyle  Height="40px" BorderStyle="Solid" VerticalAlign="Middle" HorizontalAlign="Center"  />
        
         
         <HeaderStyle  Font-Bold="True" ForeColor="Black" Height="35px"  />
    
         </asp:GridView>
     </asp:Panel>
    </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td align="right" >For <b>HUMBOLDT WEDAG INDIA PVT. LTD.</b></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td align="right"><asp:Image ID="Image1"  ImageUrl ="~/RajnishSingh.bmp" runat="server" />
</td></tr>
    <tr><td align="right" >Authorised Signatory</td></tr>
    <tr><td >Signature of Indentor</td></tr>
    <tr><td>&nbsp;</td></tr>
    </table>
    
    <asp:Button runat="server" id="printButton" Text="Print" OnClientClick="printpage();"/>

  
    </div>
    </form>
</body>
</html>
