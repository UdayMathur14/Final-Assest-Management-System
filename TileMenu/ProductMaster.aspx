<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductMaster.aspx.cs" Inherits="TileMenu.ProductMaster" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function validate() {

            var txtxtmaster = document.getElementById("<%=txtmaster.ClientID %>");


            if (txtxtmaster.value == "") {
                alert("Name can not be left blank !");
                txtxtmaster.focus();
                return false;
            }

            else
                return true;
        }
    </script>
    <div class="row">
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Choose Master"></asp:Label>
                <asp:RadioButtonList ID="rblMaster" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblMaster_SelectedIndexChanged" CssClass="form-control">
                    <asp:ListItem Selected="True">Make</asp:ListItem>
                    <asp:ListItem>Product</asp:ListItem>
                    <asp:ListItem>Product Type</asp:ListItem>
                    <asp:ListItem>Product Model</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="lblmaster" runat="server" Text="" CssClass="form-label"></asp:Label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtmaster"></asp:TextBox>
                <asp:Button runat="server" Text="Save" ID="Btnsave" OnClick="Btnsave_Click" OnClientClick="return validate();" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <asp:GridView runat="server" ID="grvdetail" AutoGenerateColumns="true" CssClass="table table-striped"></asp:GridView>
        </div>
    </div>
</asp:Content>
