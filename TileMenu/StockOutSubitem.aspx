<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockOutSubitem.aspx.cs" Inherits="TileMenu.StockOutSubitem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function validate() {
            var ddlproduct = document.getElementById("<%=ddlproduct.ClientID %>");
            var ddlmake = document.getElementById("<%=ddlmake.ClientID %>");
            var ddltype = document.getElementById("<%=ddltype.ClientID %>");
            var ddlmodel = document.getElementById("<%=ddlmodel.ClientID %>");
            var ddlsrno = document.getElementById("<%=ddlsrno.ClientID %>");
            var ddlassetctcode = document.getElementById("<%=ddlassetctcode.ClientID %>");
            var ddlemployee = document.getElementById("<%=ddlemployee.ClientID %>");

            var ddlOAC = document.getElementById("<%=ddlOAC.ClientID %>");

            var txtdate = document.getElementById("<%=txtdate.ClientID %>");
            var ddlissuedhw = document.getElementById("<%=ddlissuedhw.ClientID %>");


            if (txtdate.value == "") {
                alert("Issue Date can not be left blank !");
                txtdate.focus();
                return false;
            }

            if (ddlproduct.value == "-1") {
                alert("Please Select Product Name");
                ddlproduct.focus();
                return false;
            }
            if (ddlmake.value == "-1") {
                alert("Please Select Make Name");
                ddlmake.focus();
                return false;
            }
            if (ddltype.value == "-1") {
                alert("Please Select Product Type Name");
                ddltype.focus();
                return false;
            }
            if (ddlmodel.value == "-1") {
                alert("Please Select Model Name");
                ddlmodel.focus();
                return false;
            }
            if (ddlsrno.value == "-1") {
                alert("Serial No.can not be left blank !");
                ddlsrno.focus();
                return false;
            }

            if (ddlissuedhw.value == "-1") {
                alert("Issued Asset Can Not be left blank !")
                ddlissuedhw.focus();
                return false;
            }
            else
                return true;
        }
    </script>
    <style>
        div.divOAC {
            position: absolute;
            top: 190px;
            right: 5px;
        }

            div.divOAC td {
                text-align: center;
            }

            div.divOAC .opOAC {
                border: 0;
            }

                div.divOAC .opOAC label {
                    padding: 10px;
                    background-color: gray;
                    border-radius: 10px;
                    color: #fff;
                    text-align: center;
                    width: 170px;
                }

                div.divOAC .opOAC input[type=radio] {
                    visibility: hidden;
                }

                    div.divOAC .opOAC input[type=radio]:checked ~ label {
                        background-color: red;
                    }

        .uday {
            padding-top: 70px;
            min-height: 100vh;
            border: 2px solid black;
            width: 100%;
            background-image: url('/Images/inventory.jpg');
            background-repeat: repeat-y;
            background-size: 100%;
            overflow-y: auto;
        }
    </style>
    <div class="uday">
        <div style="width: 70%; margin-inline: auto">


            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label8" runat="server" Text="Issue Type"></asp:Label>
                        <asp:DropDownList ID="ddlissuetype" runat="server" AutoPostBack="true" CssClass="form-control">
                            <asp:ListItem>Permanent</asp:ListItem>
                            <asp:ListItem>Temporary</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Choose Item Type" Visible="true"></asp:Label>
                        <asp:RadioButtonList ID="typemaster" runat="server" AutoPostBack="true" Visible="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="typemaster_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem Selected="True" style="margin-right: 10px;">Hardware</asp:ListItem>
                            <asp:ListItem>Software</asp:ListItem>

                        </asp:RadioButtonList>
                    </div>
                </div>



            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Issue Date" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                        <%-- <asp:ImageButton ID="ImageButton1" CssClass="calButton" runat="server" Style="display: none;" ImageUrl="~/Content/images/cal-image.png" Width="30px" OnClick="ImageButton1_Click" />
                <asp:Calendar ID="Calendar1" runat="server" Visible="False" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>--%>
                    </div>

                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="On Acount of" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlOAC" runat="server" AutoPostBack="false" CssClass="form-control" Enabled="false">
                            <asp:ListItem Text="Subitem"></asp:ListItem>

                        </asp:DropDownList>
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
                        <asp:Label ID="lbltype" runat="server" Text="Type"></asp:Label>
                        <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddltype_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblmodel" runat="server" Text="Model" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlmodel" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlmodel_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="Serial No."></asp:Label>
                        <asp:DropDownList ID="ddlsrno" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlsrno_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server" Text="Asset Code" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlassetctcode" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlassetctcode_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label6" runat="server" Text="Employee Name"></asp:Label>
                        <asp:DropDownList ID="ddlemployee" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label11" runat="server" Text="Issued/Available Laptop/Desktop" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlissuedhw" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlissuedhw_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>
            </div>



            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label10" runat="server" Text="Remarks"></asp:Label>
                        <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control"></asp:TextBox>

                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label7" runat="server" Text="Choose Type"></asp:Label>
                        <asp:RadioButtonList ID="rbltype" runat="server" AutoPostBack="false" RepeatDirection="Horizontal" CssClass="form-control radioButton">
                            <asp:ListItem Selected="True">Additional</asp:ListItem>
                            <asp:ListItem>Replacement</asp:ListItem>
                            <asp:ListItem>Replacement Under warranty</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

            </div>

            <div class="row">

                <div class="col-12">

                    <div class="form-group">
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" OnClientClick="return validate();"></asp:Button>

                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-12">
                    <div class="form-group">
                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="true" OnRowDeleting="GridView3_RowDeleting" DataKeyNames="StockOut_Id" CssClass="table table-striped">
                            <Columns>
                                <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Content/images/return.png" HeaderText="Return" />
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>

            </div>

            <div class="modal" tabindex="-1" role="dialog" id="divModalReplaced">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Replaced Item from Below Asset</h5>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField runat="server" ID="hdProDetailId" />

                            <div class="row">
                                <div class="col-md-6 col-sm-12">
                                    <asp:Label ID="lblproductname" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-6 col-sm-12">
                                    <asp:Label ID="Label9" runat="server" Text="Check existance(Serial No.)"></asp:Label>
                                    <asp:TextBox ID="txtcheckserial" runat="server" CssClass="form-control"></asp:TextBox><asp:Button Text="Search" runat="server" CssClass="btn btn-primary" ID="btnsearch" OnClick="btnsearch_Click" />
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                                    <div class="row">
                                        <asp:Label ID="lblavl" runat="server" Visible="false" Text="Item available in Stock,you can keep as <b>USABLE</b> OR if Require can <b>To be Scrap</b> "></asp:Label>
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="true" ForeColor="Black" GridLines="Both" BackColor="White"
                                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px"
                                            Width="100%" CellPadding="5" Visible="true" RowStyle-Height="45px" Style="font-size: small;" DataKeyNames="ProductDetail_Id" OnRowDeleting="GridView2_RowDeleting">
                                            <FooterStyle BackColor="#E3E3E1" />
                                            <RowStyle BackColor="#ffffcc" />
                                            <Columns>
                                                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="To be scrap" />

                                            </Columns>
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        <asp:GridView ID="GridView1" Visible="false" runat="server" AutoGenerateColumns="true" CssClass="table table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Return/Scrap" HeaderStyle-Width="70px">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdStockOutId" runat="server" Value='<%# Eval("StockOut_Id") %>' ClientIDMode="Static" />
                                                        <asp:HiddenField ID="hdemp" runat="server" Value='<%# Eval("StockOut_EmpName") %>' ClientIDMode="Static" />

                                                        <asp:ImageButton OnClientClick='showReturnPopup(event,this,1)' ImageUrl="~/Content/images/return.png" Width="30px" runat="server" />
                                                        <asp:ImageButton OnClientClick='showReturnPopup(event,this,2)' ImageUrl="~/Content/images/scrap.png" Width="30px" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                        </asp:GridView>
                                        <asp:Label ID="lblusable" runat="server" Visible="false" Text="Item Not in inventory,please take in"></asp:Label>
                                        <table id="tblstockin" runat="server" visible="false" border="1">
                                            <tr>
                                                <td>Product:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlproductin" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlproductin_SelectedIndexChanged"></asp:DropDownList></td>
                                                <td>Make:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlmakein" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlmakein_SelectedIndexChanged"></asp:DropDownList></td>

                                            </tr>
                                            <tr>
                                                <td>Type:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddltypein" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddltypein_SelectedIndexChanged"></asp:DropDownList></td>
                                                <td>Model:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlmodelin" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList></td>

                                            </tr>
                                            <tr>
                                                <td>Serial No.</td>
                                                <td>
                                                    <asp:TextBox ID="txtserial" runat="server"></asp:TextBox></td>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnentry" runat="server" Text="Entry" OnClick="btnentry_Click" /></td>


                                            </tr>
                                        </table>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnsearch" EventName="Click" />
                                </Triggers>

                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">

                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal" tabindex="-1" role="dialog" id="divModalReturn">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Stock Return</h5>

                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField runat="server" ID="hdStockId" />
                            <asp:HiddenField runat="server" ID="hdRetunType" />
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblemp" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="Label12" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblserial" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button Text="Return" runat="server" CssClass="btn btn-primary btn-save" ID="btnReturn" OnClick="btnReturn_Click" />
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(function () {
            initDatepicker();

            $(document).on('change', '.radioButton input[type=radio]', function () {
                if ($(this).val().toLowerCase() == 'replacement') {
                    showReplacedPopup(event, this);
                }
            });
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

        function showReplacedPopup(e, ele) {
            e.preventDefault();


            $('#<%= hdProDetailId.ClientID %>').val($('#<%= ddlissuedhw.ClientID %>').val());

            $('#<%= lblproductname.ClientID %>').text($('#<%= ddlissuedhw.ClientID %> option:selected').text());

            $("#divModalReplaced").modal('show');
        }
        function showReturnPopup(e, ele, returnType) {
            e.preventDefault();
            $('.modal-title').text(returnType == 1 ? "Stock Return" : "Stock Scrap");
            $('#<%= btnReturn.ClientID%>').val(returnType == 1 ? "Return" : "Scrap");
            $('#<%= hdRetunType.ClientID %>').val(returnType);
            $('#<%= hdStockId.ClientID %>').val($(ele).closest('td').find('#hdStockOutId').val());
            $('#<%= lblemp.ClientID %>').text($(ele).closest('td').find('#hdemp').val());

            $("#divModalReturn").modal('show');
        }
    </script>
</asp:Content>
