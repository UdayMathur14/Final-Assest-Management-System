<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepairReport.aspx.cs" Inherits="TileMenu.RepairReport" %>

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
            overflow-y: auto;
        }
    </style>
    <div class="uday">


        <asp:Panel ID="pnlData" runat="server">
            <fieldset>
                <legend><b>Repair Report :</b></legend>
                <br>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ForeColor="Black" GridLines="Both" BackColor="White"
                    BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px"
                    Width="100%" CellPadding="5" RowStyle-Height="30px">
                    <FooterStyle BackColor="#E3E3E1" />
                    <RowStyle BackColor="#ffffcc" />

                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
