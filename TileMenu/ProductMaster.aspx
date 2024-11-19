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
        

        .uday {
            /*max-width: 130vw;
            min-height: 100vh;
            background-image: url('Inventory.jpg');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;*/
            border:2px solid black;
            width:100%;
            background-image:url('/Images/back1.jpg');
            overflow:auto;
            
        }

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
            width:80%;
            text-align:center;
            margin-left:60px;
        }

        .grid-view .header-row {
            background-color: #E3E3E1; /* Dark background */
            color: #ffffff; /* White text */
        }

        .grid-view .header-row th {
                padding: 10px;
                text-align: center;
                color: white;
        }

        .grid-view .data-row {
            background-color: #e9ecef; /* Light background */
            color: #000000; /* Black text */
            width:80%;

        }

            .grid-view .data-row td {
                padding: 10px;
            }

            .grid-view .data-row:nth-child(even) {
                background-color: bisque; /* Alternate row background */
            }

            .grid-view .data-row:nth-child(odd) {
                background-color: lightgray; /* Odd row background */
            }
            /*#e9ecef*/

            .grid-view .data-row:hover {
                background-color: burlywood; /* Highlight color on hover */
            }

        .applyColor {
            background-color: darkslategrey !important;
            width:80%;
        }
    </style>

    <div class="uday">
        <div class="container" style="padding-top:90px">
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group" >
                        <asp:Label ID="Label1" runat="server" Text="Choose Master" style="font-weight: bold;color:black" CssClass="form-label"></asp:Label>
                        <div class="radio-buttons">
                            <asp:RadioButtonList ID="rblMaster" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblMaster_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem Selected="True" style="font-weight: bold;">Make</asp:ListItem>
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
            <div class="row table-container" style="border-radius:20px" >
                <div class="col-12">
                    <asp:GridView runat="server" ID="grvdetail" AutoGenerateColumns="true" CssClass="table table-striped table-bordered grid-view" style="border-radius:20px">
                        <HeaderStyle CssClass="header-row applyColor" />
                        <RowStyle CssClass="data-row" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
