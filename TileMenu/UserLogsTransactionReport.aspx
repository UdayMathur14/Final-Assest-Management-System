<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserLogsTransactionReport.aspx.cs" Inherits="TileMenu.UserLogsTransactionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .uday {
            padding-top: 70px;
            min-height: 100vh;
            border: 2px solid black;
            background-image: url('/Images/try2.jpg');
            background-repeat: repeat-y;
            background-size: 100%;
        }
    </style>
    <style>
    .grid-container {
        overflow-x: auto;
        width: 100%;
        margin-bottom: 30px;
        padding: 20px;
        background-color: #f8f9fa; /* Light background */
        border-radius: 10px;
        box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    }

    .grid-header {
        font-size: 20px;
        font-weight: bold;
        background-color: #007bff; /* Bootstrap primary */
        color: white;
        padding: 10px;
        border-radius: 8px 8px 0 0;
        margin-bottom: 10px;
        text-align: center;
    }

    .table-bordered {
        border-collapse: collapse !important;
        border: 1px solid #dee2e6;
    }

    .table-bordered th {
        background-color: #343a40 !important;
        color: white !important;
        text-align: center;
    }

    .table-bordered td, .table-bordered th {
        padding: 10px;
        border: 1px solid #dee2e6;
    }
</style>

    <div class="uday">
        <div style="width: 70%; margin-inline: auto ;margin-bottom:30px";>
            <div class="row" style="width: 70%; margin-inline: auto ;margin-bottom:30px";>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblUser" runat="server" Text="Select User:" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlemail" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                    </div>
                     <div class="col-md-6 col-sm-8" style="margin-left:15px" >

                     
     <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_Click" CssClass="btn btn-primary mt-3" />
                </div>
                    </div>
         
               

              </div>
            <!-- StockIn Grid -->
           
<!-- Stock In Grid -->
<div class="grid-container" style="overflow-x:scroll; width: 100%";>
    <div class="grid-header">Stock In</div>
    <asp:GridView ID="GridViewStockIn" runat="server" CssClass="table table-bordered table-striped table-hover"></asp:GridView>
</div>

<!-- Stock Out Grid -->
<div class="grid-container" style="overflow-x:scroll; width: 100%";>
    <div class="grid-header">Stock Out</div>
    <asp:GridView ID="GridViewStockOut" runat="server" CssClass="table table-bordered table-striped table-hover"></asp:GridView>
</div>

<%--<!-- Logs Grid -->
<div class="grid-container">
    <div class="grid-header" style="overflow-x:scroll; width: 100%";>Logs</div>
    <asp:GridView ID="GridViewLogs" runat="server" CssClass="table table-bordered table-striped table-hover"></asp:GridView>
</div>--%>
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

