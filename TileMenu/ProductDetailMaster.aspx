<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetailMaster.aspx.cs" Inherits="TileMenu.ProductDetailMaster" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
         <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="lblproduct" runat="server" Text="Product" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlproduct" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged" ></asp:DropDownList>
            </div>
        </div>
        <div class="col-md-6 col-sm-12">
              <div class="form-group">
              <asp:Label ID="lblmake" runat="server" Text="Make"></asp:Label>
              <asp:DropDownList ID="ddlmake" runat="server" AutoPostBack="true" CssClass="form-control" ></asp:DropDownList>
             </div>
         </div>
        
    </div>
     <div class="row">
        <div class="col-md-6 col-sm-12">
              <div class="form-group">
              <asp:Label ID="lbltype" runat="server" Text="Type"></asp:Label>
              <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" CssClass="form-control" ></asp:DropDownList>
             </div>
         </div>
         <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="lblmodel" runat="server" Text="Model" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlmodel" runat="server" AutoPostBack="true" CssClass="form-control" ></asp:DropDownList>
            </div>
        </div>
    </div>
</asp:Content>
