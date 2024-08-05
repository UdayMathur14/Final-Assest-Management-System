<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductWiseReport.aspx.cs" Inherits="TileMenu.ProductWiseReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
         .uday {
     padding-top: 70px;
     min-height: 100vh;
     border: 2px solid black;
     width: 100%;
     background-image: url('/Images/try2.jpg');
     background-repeat: no-repeat;
     background-size: cover;
 }
    </style>
    <div class="uday">
    <div style="width: 70%; margin-inline: auto">
    <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="Choose Item Type"></asp:Label>
                        <asp:RadioButtonList ID="typemaster" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="typemaster_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem Selected="True" Text="Hardware"></asp:ListItem>
                            <asp:ListItem Text="Software"></asp:ListItem>

                        </asp:RadioButtonList>
                    </div>
                </div>

         <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblproduct" runat="server" Text="Product" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlproduct" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>

         
           
          <div class="row">
           <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Available Qty Less Then or Equal:"></asp:Label>
                <asp:TextBox ID="txtqty"  runat ="server" Width ="150px" AutoPostBack="True" ontextchanged="txtqty_TextChanged" CssClass="form-control" ></asp:TextBox>

            </div>
          </div>

        </div>
    
                   
                     
  
              
     <div style="padding-left:0px;">
      <asp:Panel ID="pnldetail" runat="server" Visible="false">
     <table style="border:solid black; height:40px; width:1000px;" border="0">
<%--     <tr><td style="font-weight:bold;">Date From:<asp:TextBox ID="txtdatefrom"  
             onfocus="showCalendarControl(this);" onkeyup="abc(this);" runat ="server" 
             Width ="150px" ></asp:TextBox></td>
     <td style="font-weight:bold;">Date To:<asp:TextBox ID="txtdateto"  
             onfocus="showCalendarControl(this);" onkeyup="abc(this);" runat ="server" 
             Width ="150px" ></asp:TextBox></td>
             </tr>
             <tr><td style="font-weight:bold;">Employee Code:<asp:TextBox ID="txtempcode"  
              runat ="server" Width ="150px"></asp:TextBox></td>
     <td style="font-weight:bold;">Employee Name:<asp:TextBox ID="txtempname"  
             runat ="server" Width ="150px" ></asp:TextBox></td>
             </tr>
             <tr>
     <td style="font-weight:bold;">Cost Center:<asp:TextBox ID="txtcostcenter" 
             runat="server" ></asp:TextBox>
         <asp:Button
                 ID="Button1" runat="server" Text="Search" onclick="Button1_Click" /> </td></tr>--%>
     <tr><td colspan="3" style="font-weight:bold;font-size:large;"><asp:Label ID="lblproductname" runat="server"></asp:Label><asp:Label ID="lbldistinctrecord" runat="server"></asp:Label>
         <asp:ImageButton ID="btnclose" runat="server" ImageUrl="~/Close.png" 
             onclick="btnclose_Click" /></td></tr>
     <tr runat="server" visible="false" id="trinheader"><td colspan="3" style="font-weight:bold;color:#009933;" >Item In Detail</td></tr>
     <tr runat="server" visible="false" id="trindetail"><td colspan="3">
      <asp:GridView ID="grvItemIn" runat="server" AutoGenerateColumns="true" ForeColor="Black" GridLines="Both"  BackColor="White" 
                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" CssClass="table table-striped"
                        Width ="90%" CellPadding="5" Visible="true" RowStyle-Height="25px" >
        <FooterStyle BackColor="#E3E3E1" />
         <RowStyle BackColor="#ffffcc" />
         <Columns>
         </Columns>
         <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
      </asp:GridView>
     </td></tr>
    
     <tr runat="server" visible="false" id="troutdetail"><td colspan="3">
     <asp:GridView ID="grvItemOut" runat="server" AutoGenerateColumns="true" ForeColor="Black" GridLines="Both"  BackColor="White" 
                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" CssClass="table table-striped"
                        Width ="100%"  Visible="true" Font-Size="14px"  RowStyle-Height="45px" onrowdatabound="grvItemOut_RowDataBound">
        <FooterStyle BackColor="#E3E3E1" />
        <RowStyle BackColor="#ffffcc" />
         <Columns>
         </Columns>
         <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
      <AlternatingRowStyle BackColor="White" />
      </asp:GridView>
     </td></tr>

         <tr runat="server" visible="false" id="travailable"><td colspan="3">
     <asp:GridView ID="grvAvailable" runat="server" AutoGenerateColumns="true" ForeColor="Black" GridLines="Both"  BackColor="White" 
                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" CssClass="table table-striped"
                        Width ="100%"  Visible="true" Font-Size="14px"  RowStyle-Height="45px">
        <FooterStyle BackColor="#E3E3E1" />
        <RowStyle BackColor="#ffffcc" />
         <Columns>
         </Columns>
         <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
      <AlternatingRowStyle BackColor="White" />
      </asp:GridView>
     </td></tr>
     
          
     </table>
     </asp:Panel>
     </div>
     <div >
     
     <asp:Panel id="pnlData" runat="server">
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ForeColor="Black" GridLines="Both"  BackColor="White" 
                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" CssClass="table table-striped"
                        Width ="100%" CellPadding="5" RowStyle-Height="30px" onrowdatabound="GridView1_RowDataBound" ShowFooter="true">
        <FooterStyle BackColor="#E3E3E1" Font-Bold="True" Height="30px"/>
        <RowStyle BackColor="#ffffcc" />
         <Columns>
         <asp:BoundField HeaderText="Product Name" DataField="Product_Name" ItemStyle-Font-Bold="true"/>
         
                      
         <asp:TemplateField HeaderText="Total In" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Black" >
         <ItemTemplate>
               <asp:HyperLink ID="inid" runat="server"  Target="_blank" Text='<%# Bind("TotalInQty") %>' NavigateUrl='<%# Eval("Product_Id", "ProductwiseReport.aspx?ProductId={0}&Name=I") %>' ></asp:HyperLink>
           </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Employee /Internal /Site" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Black" HeaderStyle-Width="10px" HeaderStyle-Wrap="true">
         <ItemTemplate>
               <asp:HyperLink ID="outid" runat="server"  Target="_blank" Text='<%# Bind("TotalOut") %>' NavigateUrl='<%# Eval("Product_Id", "ProductwiseReport.aspx?ProductId={0}&Name=O") %>' ></asp:HyperLink>
           </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Unique" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Black" HeaderStyle-Width="10px" HeaderStyle-Wrap="true">
         <ItemTemplate>
               <asp:HyperLink ID="outid" runat="server" Target="_blank" Text='<%# Bind("uniqueout") %>' NavigateUrl='<%# Eval("Product_Id", "ProductwiseReport.aspx?ProductId={0}&Name=O") %>' ></asp:HyperLink>
           </ItemTemplate>
        </asp:TemplateField>
       
        <asp:TemplateField HeaderText="Repair /StandBy /UserReserve" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Black" HeaderStyle-Width="10px" HeaderStyle-Wrap="true">
         <ItemTemplate>
               <asp:HyperLink ID="repairid" runat="server"  Target="_blank" Text='<%# Bind("TotalOutRepair") %>' NavigateUrl='<%# Eval("Product_Id", "ProductwiseReport.aspx?ProductId={0}&Name=R") %>' ></asp:HyperLink>
           </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="To Be Scrap" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Black">
         <ItemTemplate>
               <asp:HyperLink ID="scrapid" runat="server"  Target="_blank" Text='<%# Bind("TotalOutScrap") %>' NavigateUrl='<%# Eval("Product_Id", "ProductwiseReport.aspx?ProductId={0}&Name=T") %>' ></asp:HyperLink>
           </ItemTemplate>
        </asp:TemplateField>
                  
         <asp:TemplateField HeaderText="Sold /Scrapped /VendorReturn" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Black" HeaderStyle-Width="10px" HeaderStyle-Wrap="true">
         <ItemTemplate>
               <asp:HyperLink ID="soldid" runat="server"  Target="_blank" Text='<%# Bind("TotalOutSold") %>' NavigateUrl='<%# Eval("Product_Id", "ProductwiseReport.aspx?ProductId={0}&Name=S") %>' ></asp:HyperLink>
           </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Available" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Black" >
         <ItemTemplate>
               <asp:HyperLink ID="Avlid" runat="server"  Target="_blank" Text='<%# Bind("Available") %>' NavigateUrl='<%# Eval("Product_Id", "ProductwiseReport.aspx?ProductId={0}&Name=A") %>' ></asp:HyperLink>
           </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="Product Id" DataField="Product_id" ItemStyle-Font-Bold="true"/>
        </Columns>
         <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
         <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
        <AlternatingRowStyle BackColor="White" />
         </asp:GridView>
     </asp:Panel>
    
     </div>
     </div>
     </div>
</asp:Content>
