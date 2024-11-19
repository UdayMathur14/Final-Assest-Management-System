<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WarrantyExpiryReport.aspx.cs" Inherits="TileMenu.WarrantyExpiryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .uday {
            padding-top: 70px;
            min-height: 100vh;
            border: 2px solid black;
            width: 100%;
            background-image: url('/Images/try2.jpg');
            background-repeat: repeat-y;
            background-size: 100%;
            overflow:auto;
        }
        .table-striped tbody tr:hover {
            background-color: #f0f8ff !important; /* Light blue background on hover */
        }

    </style>
    <div class="uday">
        <div style="width: 70%; margin-inline: auto">
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Choose Item Type"></asp:Label>
                        <asp:RadioButtonList ID="typemaster" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="form-control" OnSelectedIndexChanged="typemaster_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Text="Hardware"></asp:ListItem>
                            <asp:ListItem Text="Software"></asp:ListItem>

                        </asp:RadioButtonList>
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

                        <asp:Label ID="Label2" runat="server" Text="Expired on(Month)" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlEmonth" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlEmonth_SelectedIndexChanged">
                            <asp:ListItem Value="-1" Text="Any"></asp:ListItem>
                            <asp:ListItem Value="1" Text="January"></asp:ListItem>
                            <asp:ListItem Value="2" Text="February"></asp:ListItem>
                            <asp:ListItem Value="3" Text="March"></asp:ListItem>
                            <asp:ListItem Value="4" Text="April"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="June"></asp:ListItem>
                            <asp:ListItem Value="7" Text="July"></asp:ListItem>
                            <asp:ListItem Value="8" Text="August"></asp:ListItem>
                            <asp:ListItem Value="9" Text="September"></asp:ListItem>
                            <asp:ListItem Value="10" Text="October"></asp:ListItem>
                            <asp:ListItem Value="11" Text="November"></asp:ListItem>
                            <asp:ListItem Value="12" Text="December"></asp:ListItem>

                        </asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">

                        <asp:Label ID="Label3" runat="server" Text="Expired on(Year)" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlEyear" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlEyear_SelectedIndexChanged">
                            <asp:ListItem Value="-1" Text="Any"></asp:ListItem>
                            <asp:ListItem Value="-2" Text="Not Filled"></asp:ListItem>

                        </asp:DropDownList>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="form-group">
                        <asp:Button ID="btnsubmit" runat="server"
                            Text="Search" OnClick="btnsubmit_Click" CssClass="btn btn-primary btn-save" />
                    </div>
                </div>
            </div>
            <div>

               <asp:Panel ID="pnlData" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ForeColor="Black" GridLines="Both" BackColor="White"
        BorderColor="#666666" CssClass="table table-striped" BorderStyle="1" BorderWidth="1px"
        Width="100%" CellPadding="5" RowStyle-Height="45px">
        <FooterStyle BackColor="#E3E3E1" />
        <RowStyle BackColor="pink" />

        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
        <AlternatingRowStyle BackColor="#fffccc" />
    </asp:GridView>
</asp:Panel>

            </div>
        </div>
    </div>

</asp:Content>
