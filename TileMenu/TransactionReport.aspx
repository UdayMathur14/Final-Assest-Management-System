<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransactionReport.aspx.cs" Inherits="TileMenu.TransactionReport" %>

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
        }
    </style>
    <div class="uday">
        <div style="width: 70%; margin-inline: auto">
            <div class="row">

                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblserial" runat="server" Text="Serial No." CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtserial" runat="server" Width="150px"
                            AutoPostBack="True" OnTextChanged="txtserial_TextChanged" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Employee Name" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtEmpName" runat="server" Width="150px"
                            AutoPostBack="True" OnTextChanged="txtEmpName_TextChanged" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">

                        <label>
                            Date From:</label>
                        <asp:TextBox ID="txtFdate" CssClass="form-control datepicker" runat="server" Width="150px"></asp:TextBox>

                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">

                        <label>
                            Date To:</label>
                        <asp:TextBox ID="txtTdate" CssClass="form-control datepicker" runat="server" Width="150px"></asp:TextBox>

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
                        BorderColor="#666666" BorderStyle="1" BorderWidth="1px"
                        Width="100%" CellPadding="5" RowStyle-Height="45px">
                        <FooterStyle BackColor="#E3E3E1" />
                        <RowStyle BackColor="#ffffcc" />

                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </asp:Panel>

            </div>
        </div>
    </div>
    <script>
        $(function () {
            initDatepicker();
        });

        function pageLoad() {
            initDatepicker();
        }

        function initDatepicker() {
            $('.datepicker').each(function () {
                $(this).datepicker({
                    format: "dd/M/yyyy",
                    clearBtn: true
                }).on('changeDate', function () {
                    $(this).datepicker('hide');
                });
            });
        }
    </script>
</asp:Content>
