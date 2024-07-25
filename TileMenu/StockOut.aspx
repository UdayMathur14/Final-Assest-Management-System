<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockOut.aspx.cs" EnableEventValidation="false" Inherits="TileMenu.StockOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <script type="text/javascript">
        function validate() {
            var ddlproduct = document.getElementById("<%=ddlproduct.ClientID %>");
            var sravl = document.getElementById("<%=Hidden1.ClientID %>");
            var ddlmake = document.getElementById("<%=ddlmake.ClientID %>");
            var ddltype = document.getElementById("<%=ddltype.ClientID %>");
            var ddlmodel = document.getElementById("<%=ddlmodel.ClientID %>");
            var ddlsrno = document.getElementById("<%=ddlsrno.ClientID %>");
            var ddlassetcode = document.getElementById("<%=ddlassetcode.ClientID %>");
            var ddlemployee = document.getElementById("<%=ddlemployee.ClientID %>");
            <%--var txtcostcenter = document.getElementById("<%=<%--txtcostcenter.ClientID--%> %> ");--%>
            var ddlOAC = document.getElementById("<%=ddlOAC.ClientID %>");

            var txtdate = document.getElementById("<%=txtdate.ClientID %>");
            var ddlissuetype = document.getElementById("<%=ddlissuetype.ClientID %>");
            var txterdate = document.getElementById("<%=txterdate.ClientID %>");
            var txtresven = document.getElementById("<%=txtresven.ClientID %>");
            var txtlocation = document.getElementById("<%=txtlocation.ClientID %>");
            var typemaster = document.getElementById("<%=typemaster.ClientID %>");
            var typemasterValue = $("#<%=typemaster.ClientID %> input:checked").val();

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
            if (ddlissuetype.value == "-1") {
                alert("Please Select Issue Type");
                ddlissuetype.focus();
                return false;
            }
            if (typemasterValue == "Hardware" && sravl.value == "Non Consumable") {
                if (ddlassetcode.value == "-1") {
                    alert("Asset Code can not be left blank !");
                    ddlassetcode.focus();
                    return false;
                }
            }

            if (ddlOAC.value == "Employee") {
                if (ddlemployee.value == "-1") {
                    alert("Employee can not be left blank !");
                    ddlemployee.focus();
                    return false;
                }

            }
            if (ddlOAC.value == "Internal" || ddlOAC.value == "Site" || ddlOAC.value == "Repair" || ddlOAC.value == "Return to Vendor") {
                if (txtresven.value == "-1") {
                    alert("Responsibility/Vendor can not be left blank !");
                    txtresven.focus();
                    return false;
                }
                if (txtlocation.value == "") {
                    alert("Location can not be left blank !");
                    txtlocation.focus();
                    return false;
                }

            }
            if (ddlOAC.value == "Reserved for user") {
                if (txtresven.value == "-1") {
                    alert("User can not be left blank !");
                    txtresven.focus();
                    return false;
                }
                if (txtlocation.value == "") {
                    alert("Reserved till date can not be left blank !");
                    txtlocation.focus();
                    return false;
                }

            }
            if (ddlissuetype.value == "Temporary") {
                if (txterdate.value == "") {
                    alert("Expected Return Date Can Not be left blank !")
                    txterdate.focus();
                    return false;
                }

            }
            if (typemasterValue == "Software") {
                if (ddlOAC.value == "Employee") {
                    if (ddlissuedhw.value == "-1") {
                        alert("Issued Asset Can Not be left blank !")
                        ddlissuedhw.focus();
                        return false;
                    }
                }

            }

            else
                return true;
        }
    </script>
    <style>
        div.divOAC {
            position: absolute;
            top: 220px;
            right: 25px;
        }

            div.divOAC td {
                text-align: left;
            }

            div.divOAC .opOAC {
                border: 0;
            }

                div.divOAC .opOAC label {
                    padding: 10px;
                    background-color: lightgray;
                    border-radius: 10px;
                    color: black;
                    text-align: left;
                    width: 95px;
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
            background-image: url('/Images/back1.jpg');
            background-repeat: no-repeat;
            background-size: cover;
            overflow:auto;
        }
    </style>
    <%--<asp:UpdatePanel runat="server" ID="updMain">
        <ContentTemplate>--%>

    <div class="divOAC">
        <asp:RadioButtonList ID="optOAC" runat="server" AutoPostBack="True" RepeatDirection="Vertical" CssClass="opOAC" OnSelectedIndexChanged="optOAC_SelectedIndexChanged">
            <asp:ListItem Text="Employee"></asp:ListItem>
            <asp:ListItem Text="Internal"></asp:ListItem>
            <asp:ListItem Text="Site"></asp:ListItem>
            <asp:ListItem Text="Repair"></asp:ListItem>
            <asp:ListItem Text="Standby"></asp:ListItem>
            <asp:ListItem Text="User Res" Value="Reserved for user"></asp:ListItem>
            <asp:ListItem Text="Tobe scrap" Value="To be scrap"></asp:ListItem>
            <asp:ListItem Text="Scrapped"></asp:ListItem>
            <asp:ListItem Text="Sold"></asp:ListItem>
            <asp:ListItem Text="Vendor Ret" Value="Return to Vendor"></asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <div class="uday">
        <div style="width: 70%; margin-inline: auto">
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Choose Item Type"></asp:Label>
                        <asp:RadioButtonList ID="typemaster" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="typemaster_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem Selected="True" Text="Hardware"></asp:ListItem>
                            <asp:ListItem Text="Software"></asp:ListItem>

                        </asp:RadioButtonList>
                    </div>
                </div>


            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Issue Date" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control datepicker" ReadOnly="false"></asp:TextBox>
                        <%-- <asp:ImageButton ID="ImageButton1" CssClass="calButton" runat="server" Style="display: none;" ImageUrl="~/Content/images/cal-image.png" Width="30px" OnClick="ImageButton1_Click" />
                <asp:Calendar ID="Calendar1" runat="server" Visible="False" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>--%>
                    </div>

                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="On Acount of" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlOAC" runat="server" AutoPostBack="true" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlOAC_SelectedIndexChanged">
                            <asp:ListItem Text="Employee"></asp:ListItem>
                            <asp:ListItem Text="Internal"></asp:ListItem>
                            <asp:ListItem Text="Site"></asp:ListItem>
                            <asp:ListItem Text="Repair"></asp:ListItem>
                            <asp:ListItem Text="Standby"></asp:ListItem>
                            <asp:ListItem Text="Reserved for user"></asp:ListItem>
                            <asp:ListItem Text="To be scrap"></asp:ListItem>
                            <asp:ListItem Text="Scrapped"></asp:ListItem>
                            <asp:ListItem Text="Sold"></asp:ListItem>
                            <asp:ListItem Text="Return to Vendor"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblproduct" runat="server" Text="Product" CssClass="form-label"></asp:Label><asp:TextBox ID="Hidden1" runat="server" Width="0px" Height="0px" />
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
                        <asp:DropDownList ID="ddlassetcode" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlassetcode_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label6" runat="server" Text="Issued To"></asp:Label><asp:RadioButtonList ID="optcat" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="optcat_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Employee</asp:ListItem>
                            <asp:ListItem>Common</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:DropDownList ID="ddlemployee" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlemployee_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label7" runat="server" Text="Name" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label12" runat="server" Text="" CssClass="form-label"></asp:Label>
                        <%--<asp:TextBox ID="txtcostcenter" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>--%>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label13" runat="server" Text="EMail" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label15" runat="server" Text="" Visible="false" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="txtresven" runat="server" Visible="false" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label16" runat="server" Text="Location" Visible="false" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtlocation" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label8" runat="server" Text="Issue Type"></asp:Label>
                        <asp:DropDownList ID="ddlissuetype" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlissuetype_SelectedIndexChanged">
                            <asp:ListItem Value="-1">--Select--</asp:ListItem>
                            <asp:ListItem>Permanent</asp:ListItem>
                            <asp:ListItem>Temporary</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label9" runat="server" Text="Exp.Rtn.Date" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txterdate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        <%-- <asp:ImageButton ID="ImageButton2" CssClass="calButton" runat="server" Style="display: none;" ImageUrl="~/Content/images/cal-image.png" Width="30px" OnClick="ImageButton2_Click" />
                <asp:Calendar ID="Calendar2" runat="server" Visible="False" OnSelectionChanged="Calendar2_SelectionChanged"></asp:Calendar>--%>
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
                        <asp:Label ID="Label11" runat="server" Text="Issued/Available Laptop/Desktop" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlissuedhw" runat="server" AutoPostBack="false" CssClass="form-control">
                        </asp:DropDownList>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="form-group">
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" CssClass="btn btn-primary btn-save"></asp:Button>

                    </div>
                </div>

            </div>

            <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
            <%-- <asp:UpdatePanel runat="server" ID="updGrid" UpdateMode="Conditional">
       
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-12">
                    <div class="form-group">
                        <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" DataKeyNames="StockOut_Id" CssClass="table table-striped">
                            <Columns>
                                <asp:TemplateField HeaderText="Return/Scrap" HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdStockOutId" runat="server" Value='<%# Eval("StockOut_Id") %>' ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdemp" runat="server" Value='<%# Eval("EmpName") %>' ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdproduct" runat="server" Value='<%# Eval("Product") %>' ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdserial" runat="server" Value='<%# Eval("SerialNo") %>' ClientIDMode="Static" />
                                        <asp:ImageButton OnClientClick='showReturnPopup(event,this,1)' ImageUrl="~/Content/images/return.png" Width="30px" runat="server" />
                                        <asp:ImageButton OnClientClick='showReturnPopup(event,this,2)' ImageUrl="~/Content/images/scrap.png" Width="30px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="true" ButtonType="Link" DeleteText="Move To Permanent" HeaderText="Move To Permanent" />
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>

            </div>

            <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>



            <div class="modal" tabindex="-1" role="dialog" id="divModalReturn">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h3 class="modal-title">Stock Return</h3>

                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField runat="server" ID="hdStockId" />
                            <asp:HiddenField runat="server" ID="hdRetunType" />
                            <h2>Asset Detail</h2>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblemp" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblproductname" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblserial" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-4 hd">
                                    <asp:RadioButtonList ID="rdlscrap" runat="server" AutoPostBack="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">To be scrap</asp:ListItem>
                                        <asp:ListItem>Scrapped</asp:ListItem>
                                        <asp:ListItem>Sold</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="row hd">
                                    <div class="col-md-6 col-sm-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label21" runat="server" Text="Remarks" CssClass="form-label"></asp:Label>
                                            <asp:TextBox ID="Txtscrapremarks" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr class="sh" style="border-top: 2px solid black" />
                            <h2 class="sh">Return Detail</h2>
                            <div class="row sh">

                                <div class="col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label14" runat="server" Text="Return Type" CssClass="form-label"></asp:Label>
                                        <asp:RadioButtonList ID="rdlsrno" runat="server" AutoPostBack="false" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">Same</asp:ListItem>
                                            <asp:ListItem>Replaced</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                                <div class="col-md-6 col-sm-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label17" runat="server" Text="New Serial No." CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="txtnewsrno" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row sh">
                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="Label18" runat="server" Text="Repaire Cost" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="txtcost" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="Label19" runat="server" Text="Bill No." CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="txtbillno" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row sh">
                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <asp:Label ID="Label20" runat="server" Text="Repaire Remarks" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="txtrepaireRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button Text="Return" runat="server" CssClass="btn btn-primary" ID="btnReturn" OnClick="btnReturn_Click" />
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
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

        function showReturnPopup(e, ele, returnType) {
            e.preventDefault();
            if (returnType == 1) {
                $('.sh').show();
                $('.hd').hide();
            }
            else {
                $('.sh').hide();
                $('.hd').show();
            }
            $('.modal-title').text(returnType == 1 ? "Stock Return" : "Stock Scrap");
            $('#<%= btnReturn.ClientID%>').val(returnType == 1 ? "Return" : "Submit");
            $('#<%= hdRetunType.ClientID %>').val(returnType);
            $('#<%= lblserial.ClientID %>').text($(ele).closest('td').find('#hdserial').val());
            $('#<%= hdStockId.ClientID %>').val($(ele).closest('td').find('#hdStockOutId').val());
            $('#<%= lblemp.ClientID %>').text($(ele).closest('td').find('#hdemp').val());
            $('#<%= lblproductname.ClientID %>').text($(ele).closest('td').find('#hdproduct').val());

            $("#divModalReturn").modal('show');
        }
    </script>
</asp:Content>
