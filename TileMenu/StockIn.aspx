<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockIn.aspx.cs" Inherits="TileMenu.StockIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function validate() {
            var ddlproduct = document.getElementById("<%=ddlproduct.ClientID %>");
            var capavl = document.getElementById("<%=hdncap.ClientID %>");
            var warravl = document.getElementById("<%=hdnwarr.ClientID %>");
            var ddlmake = document.getElementById("<%=ddlmake.ClientID %>");
            var ddltype = document.getElementById("<%=ddltype.ClientID %>");
            var ddlmodel = document.getElementById("<%=ddlmodel.ClientID %>");

            var ddlvendor = document.getElementById("<%=ddlvendor.ClientID %>");
            var txtdate = document.getElementById("<%=txtdate.ClientID %>");
            var txtchallan = document.getElementById("<%=txtchallan.ClientID %>");
            var txtqty = document.getElementById("<%=txtqty.ClientID %>");
            var txtcapdate = document.getElementById("<%=txtcapdate.ClientID %>");
            var txtexpirydate = document.getElementById("<%=txtexpirydate.ClientID %>");
            if (txtdate.value == "") {
                alert("Stock In Date can not be left blank !");
                txtdate.focus();
                return false;
            }
            else if (ddlvendor.value == "-1") {
                alert("Please Select Vendor");
                ddlvendor.focus();
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
            if (txtchallan.value == "") {
                alert("Challan No. can not be left blank !");
                txtchallan.focus();
                return false;
            }
            if (txtqty.value == "" || txtqty.value == 0) {
                alert("Qty can not be left blank !");
                txtqty.focus();
                return false;
            }
            if (capavl.value == "YES") {
                if (txtcapdate.value == "") {
                    alert("Capitalization not be left blank !");
                    txtcapdate.focus();
                    return false;
                }
            }
            if (warravl.value == "YES") {
                if (txtexpirydate.value == "") {
                    alert("Expiry not be left blank !");
                    txtexpirydate.focus();
                    return false;
                }
            }

            else
                return true;
        }

    </script>


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <div class="uday">
        <div style="width: 70%; margin-inline: auto">
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Choose Item Type"></asp:Label>
                        <asp:RadioButtonList ID="typemaster" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="typemaster_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem Selected="True">Hardware</asp:ListItem>
                            <asp:ListItem>Software</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Labelaseettype" runat="server" Text="Asset Type"></asp:Label>
                        <asp:DropDownList ID="ddlassettype" runat="server" CssClass="form-control">
                            <asp:ListItem Text="EIP Asset"></asp:ListItem>
                            <asp:ListItem Text="CIIR Asset"></asp:ListItem>
                            <asp:ListItem Text="SHEREAL Asset"></asp:ListItem>
                            <asp:ListItem Text="Vendor/Rental Asset"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Date" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        <%-- <asp:ImageButton ID="imgcaldate" CssClass="calButton" runat="server" Style="display: none;" ImageUrl="~/Content/images/cal-image.png" Width="30px" OnClick="imgcaldate_Click" />
            <asp:Calendar ID="caldate" runat="server"  Visible="False" OnSelectionChanged="caldate_SelectionChanged"></asp:Calendar>--%>
                    </div>

                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="Vendor" CssClass="form-label"></asp:Label>
                        <asp:DropDownList ID="ddlvendor" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblproduct" runat="server" Text="Product" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="hdncap" runat="server" Width="0px" Height="0px" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="hdnwarr" runat="server" Width="0px" Height="0px" Visible="false"></asp:TextBox>

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
                        <asp:DropDownList ID="ddlmodel" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="Challan No." CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtchallan" runat="server" CssClass="form-control"></asp:TextBox>

                    </div>
                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server" Text="Qty" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtqty" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtqty_TextChanged"></asp:TextBox>


                    </div>

                </div>

            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label6" runat="server" Text="Capitilize Date" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtcapdate" runat="server" CssClass="form-control  datepicker"></asp:TextBox>
                        <%--<asp:ImageButton ID="Imagecap" CssClass="calButton" runat="server" Style="display: none;" ImageUrl="~/Content/images/cal-image.png" Width="30px" OnClick="Imagecap_Click" />
            <asp:Calendar ID="Calcap" runat="server"  Visible="False" OnSelectionChanged="Calcap_SelectionChanged"></asp:Calendar>--%>
                    </div>

                </div>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label7" runat="server" Text="Expiry Date" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtexpirydate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                        <%--<asp:ImageButton ID="Imageexp" CssClass="calButton" runat="server" Style="display: none;" ImageUrl="~/Content/images/cal-image.png" Width="30px" OnClick="Imageexp_Click" />
            <asp:Calendar ID="Calexp" runat="server"  Visible="False" OnSelectionChanged="Calexp_SelectionChanged"></asp:Calendar>--%>
                    </div>
                </div>
                <%--<div class="mt10">
            <label>Pic Url</label>
            <button type="button" class="c-img btn btn-warning p-4 rounded-4 shadow-sm w-100">
                <img src="" width="40" id="im_gallery" alt="">
                <span class="d-block mt-3 fw-bold">Upload Picture</span>
            </button>
            <input type="file" class="d-none" id="pic_upload_image" multiple accept="image/x-png,image/gif,image/jpeg,image/jpg">
            <input type="hidden" id="hdn_pic_imageupload" />
        </div>--%>
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label8" runat="server" Text="Upload Document" CssClass="form-label"></asp:Label>
                        <asp:FileUpload ID="doc" runat="server"></asp:FileUpload>
                        <%--<asp:Button ID="btndoc123" runat="server" Text="Upload File " CssClass="form-control" OnClick="btnUpload_Click"></asp:Button>--%>
                        <asp:Label ID="lblmessage" runat="server" Font-Bold="false"></asp:Label>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-12">
                    <asp:GridView ID="gvSerial" runat="server" AutoGenerateColumns="False" CssClass="table">
                        <Columns>
                            <asp:BoundField DataField="SlNo" HeaderText="Sl. No." />
                            <asp:TemplateField HeaderText="Asset Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAssetCode" runat="server" Text='<%# Bind("AssetCode") %>' CssClass="form-control"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Serial Number">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSerial" runat="server" Text='<%# Bind("SerialNumber") %>' CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Fill Serial No." ControlToValidate="txtSerial" ValidationGroup="StockIn" EnableClientScript="true"></asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="form-group">
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" OnClientClick="return validate();" CausesValidation="true" ValidationGroup="StockIn" CssClass="btn btn-primary btn-save"></asp:Button>

                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-12">
                    <div class="form-group">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover" OnRowDataBound="GridView1_RowDataBound">
                                <HeaderStyle CssClass="thead-dark text-white applyColor" />
                                <RowStyle CssClass="text-center" />
                                <AlternatingRowStyle CssClass="bg-light" />
                                <FooterStyle CssClass="text-white bg-dark" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>

            </div>
        </div>

    </div>
    <script>
        $(function () {
            $('.datepicker').each(function () {
                $(this).datepicker({
                    format: "dd/M/yyyy",
                    clearBtn: true
                }).on('changeDate', function () {
                    $(this).datepicker('hide');
                });
            })
        });
    </script>
    <style>
        .table thead th {
            background-color: orange; /* Dark background for table header */
            color: black; /* White text for header */
        }

        .table tbody tr:nth-child(odd) {
            background-color: #e9ecef; /* Light background for odd rows */
        }

        .table tbody tr:nth-child(even) {
            background-color: #fffccc; /* Light background for odd rows */
        }

        .table tbody tr:hover {
            background-color: pink; /* Very light blue-gray background on hover */
        }

        .table th, .table td {
            text-align: center; /* Center-align text */
        }

        .applyColor {
            background-color: black !important;
            color: white;
        }

        .uday {
            padding-top: 70px;
            min-height: 100vh;
            border: 2px solid black;
            width: 100%;
            background-image: url('/Images/inventory.jpg'),url('/Images/try1.jpg');
            background-repeat: no-repeat;
            background-size: cover;
            overflow-y: auto;
        }

    </style>

</asp:Content>
