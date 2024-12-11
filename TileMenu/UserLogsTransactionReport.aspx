<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserLogsTransactionReport.aspx.cs" Inherits="TileMenu.UserLogsTransactionReport" %>

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
                        <asp:Label ID="lblUser" runat="server" Text="Select User:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlemail" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <%--<div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <label>
                            Date From:</label>
                        <asp:TextBox ID="txtFdate" CssClass="form-control datepicker" runat="server" Width="150px"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <label>
                            Date From:</label>
                        <asp:TextBox ID="txtTdate" CssClass="form-control datepicker" runat="server" Width="150px"></asp:TextBox>
                    </div>
                </div>--%>
            </div>

            <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_Click" CssClass="btn btn-primary mt-3" />

            <!-- StockIn Grid -->
            <div class="grid-container">
                <div class="grid-header">Stock In</div>
                <asp:GridView ID="GridViewStockIn" runat="server" CssClass="table table-bordered"></asp:GridView>
            </div>

            <!-- StockOut Grid -->
            <div class="grid-container">
                <div class="grid-header">Stock Out</div>
                <asp:GridView ID="GridViewStockOut" runat="server" CssClass="table table-bordered"></asp:GridView>
            </div>

            <!-- Logs Grid -->
            <div class="grid-container">
                <div class="grid-header">Logs</div>
                <asp:GridView ID="GridViewLogs" runat="server" CssClass="table table-bordered"></asp:GridView>
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

