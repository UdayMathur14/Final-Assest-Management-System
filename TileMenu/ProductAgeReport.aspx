<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation = "false" CodeBehind="ProductAgeReport.aspx.cs" Inherits="TileMenu.ProductAgeReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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
    </style>
    <div class="uday">
        <div style="width: 70%; margin-inline: auto">

    
    <div class="row">
                <div class="col-12">
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="Choose Item Type"></asp:Label>
                        <asp:RadioButtonList ID="typemaster" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="typemaster_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem Selected="True" Text="Hardware"></asp:ListItem>
                            <asp:ListItem Text="Software"></asp:ListItem>

                        </asp:RadioButtonList>
                    </div>
                </div>


            </div>
        <div class="row">
                
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="On Acount of" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlOAC" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlOAC_SelectedIndexChanged" Enabled="true">
                             <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                            
                            <asp:ListItem Text="Employee"></asp:ListItem>
                            <asp:ListItem Text="Internal"></asp:ListItem>
                            <asp:ListItem Text="Site"></asp:ListItem>
                            <asp:ListItem Text="Repair"></asp:ListItem>
                            <asp:ListItem Text="Standby"></asp:ListItem>
                            <asp:ListItem Text="Reserved for user"></asp:ListItem>
                            <asp:ListItem Text="To be scrap"></asp:ListItem>
                            <asp:ListItem Text="Scrapped"></asp:ListItem>
                            <asp:ListItem Text="Sold"></asp:ListItem>
                            <asp:ListItem Text="Return to Vendor"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label6" runat="server" Text="Issued To"></asp:Label><asp:RadioButtonList ID="optcat" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"  OnSelectedIndexChanged="optcat_SelectedIndexChanged">
                             <asp:ListItem Selected="True" >Employee</asp:ListItem>
                            <asp:ListItem>Common</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:DropDownList ID="ddlemployee" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblproduct" runat="server" Text="Product" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlproduct" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblmake" runat="server" Text="Make"></asp:Label>
                        <asp:DropDownList ID="ddlmake" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlmake_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lbltype" runat="server" Text="Type"></asp:Label>
                        <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddltype_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblmodel" runat="server" Text="Model" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlmodel" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlmodel_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>

     <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Cost Center/Serial No./Assetcode"></asp:Label>
                        <asp:TextBox ID="txtcostcenter" runat="server"  CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Old Year"></asp:Label>
                        <asp:TextBox ID="txtoldyear" runat="server"  CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>

          <div class="row">
          <div class="col-12">
            <div class="form-group">
                <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary btn-save" Text="Submit" OnClick="btnsubmit_Click"></asp:Button>
                <asp:Button ID="btnexport" runat="server" Text="Export To Excel" OnClick="btnexport_Click" CssClass="btn btn-primary btn-save"></asp:Button>
            </div>
              
          </div>
              

        </div>
                                
                
                     
         
                
    <div class="row">
                <div class="col-12">
                    <div class="form-group">
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ForeColor="Black" GridLines="Both"  BackColor="White" 
                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" CssClass="table table-striped"
                        Width ="90%" CellPadding="5" Visible="true" RowStyle-Height="15px" onrowdatabound="GridView1_RowDataBound" DataKeyNames="ProductDetail_Id" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
        <FooterStyle BackColor="#E3E3E1" />
         <RowStyle BackColor="#ffffcc" />
         <Columns>
         <asp:BoundField HeaderText="S.No." DataField="sno" ItemStyle-Font-Bold="true" ReadOnly="true"/>
<asp:CommandField ShowEditButton="true" HeaderText="Modify" />  
         <asp:BoundField HeaderText="Product Name" DataField="Product_Name" ItemStyle-Font-Bold="true" ReadOnly="true" ItemStyle-Wrap="false"/>
             
                <asp:BoundField HeaderText="Make" DataField="Make_Name" ItemStyle-Font-Bold="true" ReadOnly="true"/>
             <asp:BoundField HeaderText="Type" DataField="ProdType_Name" ItemStyle-Font-Bold="true" ReadOnly="true" ItemStyle-Wrap="false"/>
             <asp:BoundField HeaderText="Model" DataField="ProdModel_Name" ItemStyle-Font-Bold="true" ReadOnly="true" ItemStyle-Wrap="false"/>
           <asp:BoundField HeaderText="Asset Code" DataField="ProductDetail_AssetCode" ItemStyle-Font-Bold="true"/>
           <asp:BoundField HeaderText="Serial No." DataField="ProductDetail_SerialNo" ItemStyle-Font-Bold="true" />
            <asp:BoundField HeaderText="Configuration" DataField="ProductDetail_Config" ItemStyle-Font-Bold="true" />
         <asp:BoundField HeaderText="Received On" DataField="InDate" ItemStyle-Font-Bold="true" ReadOnly="true" ItemStyle-Wrap="false"/>
         <asp:BoundField HeaderText="Challan No." DataField="StockIn_ChallanNo" ItemStyle-Font-Bold="true" ReadOnly="true"/>
             <asp:BoundField HeaderText="Captilized On" DataField="CapDate" ItemStyle-Font-Bold="true" ItemStyle-Wrap="false"/>
        <asp:BoundField HeaderText="Warranty Expiry" DataField="WED" ItemStyle-Font-Bold="true" ItemStyle-Wrap="false"/>
             <asp:BoundField HeaderText="Age(Year)" DataField="Age" ItemStyle-Font-Bold="true" ReadOnly="true"/>
        <asp:BoundField HeaderText="Age(Days)" DataField="days" ItemStyle-Font-Bold="true" ReadOnly="true"/>
       <asp:BoundField HeaderText="Employee Code" DataField="StockOut_EmpCode" ItemStyle-Font-Bold="true" ReadOnly="true"/>
       <asp:BoundField HeaderText="Employee Name" DataField="Stockout_empname" ItemStyle-Font-Bold="true" ReadOnly="true"/>
       <asp:BoundField HeaderText="CostCenter" DataField="StockOut_CostCenter" ItemStyle-Font-Bold="true" ReadOnly="true"/>
             <asp:BoundField HeaderText="CostCenter Name" DataField="Description" ItemStyle-Font-Bold="true" ReadOnly="true"/>
       <asp:BoundField HeaderText="Stock Issued On" DataField="IssueDate" ItemStyle-Font-Bold="true" ReadOnly="true" ItemStyle-Wrap="false"/>
      <asp:BoundField HeaderText="Issue Type" DataField="StockOut_IssueType" ItemStyle-Font-Bold="true" ReadOnly="true"/>
     <asp:BoundField HeaderText="OAC" DataField="StockOut_OAC" ItemStyle-Font-Bold="true" ReadOnly="true"/>
     <asp:BoundField HeaderText="Remarks" DataField="StockOut_Remarks" ItemStyle-Font-Bold="true" ReadOnly="true"/>
     <asp:BoundField HeaderText="Asset Type" DataField="StockIn_AssetType" ItemStyle-Font-Bold="true" ReadOnly="true"/>
    <asp:BoundField HeaderText="in_Id" DataField="StockIn_id" ItemStyle-Font-Bold="true" ReadOnly="true"/>
  <asp:BoundField HeaderText="prodId" DataField="product_Id" ItemStyle-Font-Bold="true" ReadOnly="true"/>
<asp:BoundField HeaderText="outid" DataField="stockout_id" ItemStyle-Font-Bold="true" ReadOnly="true"/>
 
         </Columns>
         <SelectedRowStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="black" Font-Bold="True" ForeColor="White" Height="35px" />
        <AlternatingRowStyle BackColor="Pink" />
      </asp:GridView>
     
     </div>
</div>
            </div>
</div>
        </div>
</asp:Content>

