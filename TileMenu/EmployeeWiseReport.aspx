<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeWiseReport.aspx.cs" Inherits="TileMenu.EmployeeWiseReport" %>

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
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <label>
                    Employee Code:</label>
                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control" Width="150px" AutoPostBack="True" OnTextChanged="txtEmpCode_TextChanged"></asp:TextBox>

            </div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div class="form-group">

                <label>
                    Employee Name:</label>
                <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control" Width="150px" AutoPostBack="True" OnTextChanged="txtEmpName_TextChanged"></asp:TextBox>

            </div>
        </div>

    </div>
    <div class="row">

        <div class="col-md-6 col-sm-12">
            <div class="form-group">

                <label>
                    Cost Center:</label>
                <asp:TextBox ID="txtcostcenter" runat="server" CssClass="form-control" Width="150px"
                    AutoPostBack="True" OnTextChanged="txtcostcenter_TextChanged"></asp:TextBox>

            </div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <label>
                    Product Name:</label>
                <asp:TextBox ID="txtpName" runat="server" CssClass="form-control" Width="150px" AutoPostBack="True" OnTextChanged="txtpName_TextChanged"></asp:TextBox>

            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 col-sm-12">
            <div class="form-group">

                <label>
                    Date From:</label>
                <asp:TextBox ID="txtdatefrom" CssClass="form-control datepicker" runat="server" Width="150px"></asp:TextBox>

            </div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div class="form-group">

                <label>
                    Date To:</label>
                <asp:TextBox ID="txtdateto" CssClass="form-control datepicker" runat="server" Width="150px"></asp:TextBox>

            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 col-sm-12">
            <div class="form-group">

                <label>
                    Asset Code:</label>
                <asp:TextBox ID="txtassetcode" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>

            </div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div class="form-group">

                <label>
                    Serial No.:</label>
                <asp:TextBox ID="txtsrno" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>

            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <div class="form-group">
                <asp:Button ID="btnsubmit" runat="server"
                    Text="Search"  OnClick="btnsubmit_Click" CssClass="btn btn-primary btn-save" />
            </div>
        </div>
    </div>






    <fieldset><legend runat="server" id="noncons" visible="false"><b>Item Out-Non Cosumable</b></legend></fieldset>
    <div class="row" style="width: 90%; margin-right:50px; text-align: center;">
        <div class="col-12">
            <div class="form-group">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" ForeColor="Black" GridLines="Both" BackColor="White"
                    BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" CssClass="table table-striped"
                    Width="100%" CellPadding="5" Visible="true" RowStyle-Height="45px" OnRowDataBound="GridView1_RowDataBound">
                    <FooterStyle BackColor="#E3E3E1" />
                    <RowStyle BackColor="#ffffcc" />
                    <Columns>
                    </Columns>
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <fieldset><legend runat="server" id="cons" visible="false"><b>Item Out-Cosumable</b></legend></fieldset>
    <div class="row">
        <div class="col-12">
            <div class="form-group">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="true" ForeColor="Black" GridLines="Both" BackColor="White"
                    BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" CssClass="table table-striped"
                    Width="100%" CellPadding="5" Visible="true" RowStyle-Height="45px" OnRowDataBound="GridView2_RowDataBound">
                    <FooterStyle BackColor="#E3E3E1" />
                    <RowStyle BackColor="#ffffcc" />
                    <Columns>
                    </Columns>
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>

            </div>
        </div>
    </div>
    <fieldset><legend runat="server" id="ret" visible="false"><b>Returned Item</b></legend></fieldset>
    <div class="row">
        <div class="col-12">
            <div class="form-group">
                <asp:GridView ID="grvItemOut" runat="server" AutoGenerateColumns="true" ForeColor="Black" GridLines="Vertical" BackColor="White"
                    BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" CssClass="table table-striped"
                    Width="100%" CellPadding="5" Visible="true" RowStyle-Height="45px">
                    <FooterStyle BackColor="#E3E3E1" />
                    <RowStyle BackColor="#ffffcc" />
                    <Columns>
                    </Columns>
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <div class="form-group">
                <asp:GridView ID="grvItemIn" runat="server" AutoGenerateColumns="true" ForeColor="Black" GridLines="Both" BackColor="White"
                    BorderColor="#666666" BorderWidth="1px" SortedDescendingHeaderStyle-BorderStyle="Dotted"
                    Width="100%" CellPadding="7" Visible="true">
                    <FooterStyle BackColor="#E3E3E1" />
                    <RowStyle BackColor="#ffffcc" />
                    <Columns>
                    </Columns>
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <div class="form-group">
                <asp:GridView ID="grvItemReturn" runat="server" AutoGenerateColumns="true" ForeColor="Black" GridLines="Vertical" BackColor="White"
                    BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px"
                    Width="100%" CellPadding="5" Visible="true" RowStyle-Height="45px">
                    <FooterStyle BackColor="#E3E3E1" />
                    <RowStyle BackColor="#ffffcc" />
                    <Columns>
                    </Columns>
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </div>
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
