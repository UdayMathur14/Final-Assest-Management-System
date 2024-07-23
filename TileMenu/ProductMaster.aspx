<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductMaster.aspx.cs" Inherits="TileMenu.ProductMaster" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function validate() {
            var txtmaster = document.getElementById("<%=txtmaster.ClientID %>");

            if (txtmaster.value == "") {
                alert("Name cannot be left blank!");
                txtmaster.focus();
                return false;
            }
            return true;
        }
    </script>

    <style>
        .form-group {
            margin-bottom: 20px;
        }

        .form-label {
            font-weight: bold;
            color: #007bff; /* Bootstrap primary color */
        }

        .radio-buttons {
            display: flex;
            justify-content: space-around;
        }

        .radio-buttons .form-control {
            margin: 0;
        }

        .btn-save {
            margin-top: 20px;
        }

        .table-container {
            margin-top: 30px;
        }

        .grid-view .header-row {
            background-color: #E3E3E1; /* Dark background */
            color: #ffffff; /* White text */
        }

        .grid-view .header-row th {
            padding: 10px;
            text-align: left;
            color:white;
        }

        .grid-view .data-row {
            background-color: #e9ecef; /* Light background */
            color: #000000; /* Black text */
        }

        .grid-view .data-row td {
            padding: 10px;
        }

        .grid-view .data-row:nth-child(even) {
            background-color: lightpink; /* Alternate row background */
        }
        
        .grid-view .data-row:nth-child(odd) {
            background-color: #fffccc; /* Odd row background */
        }
        /*#e9ecef*/

        .grid-view .data-row:hover {
            background-color: #d1ecf1; /* Highlight color on hover */
        }
        .applyColor{
            background-color:darkslategrey !important;
        }
    </style>

    <div class="container">
        <div class="row">
            <div class="col-md-6 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Choose Master" CssClass="form-label"></asp:Label>
                    <div class="radio-buttons">
                        <asp:RadioButtonList ID="rblMaster" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblMaster_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem Selected="True">Make</asp:ListItem>
                            <asp:ListItem>Product</asp:ListItem>
                            <asp:ListItem>Product Type</asp:ListItem>
                            <asp:ListItem>Product Model</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="lblmaster" runat="server" Text="" CssClass="form-label"></asp:Label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtmaster" Placeholder="Enter name"></asp:TextBox>
                    <asp:Button runat="server" Text="Save" ID="Btnsave" OnClick="Btnsave_Click" OnClientClick="return validate();" CssClass="btn btn-primary btn-save" />
                </div>
            </div>
        </div>
        <div class="row table-container">
            <div class="col-12">
                <asp:GridView runat="server" ID="grvdetail" AutoGenerateColumns="true" CssClass="table table-striped table-bordered grid-view">
                    <HeaderStyle CssClass="header-row applyColor" />
                    <RowStyle CssClass="data-row" />
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>
