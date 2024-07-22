<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UserMaster.aspx.cs" Inherits="TileMenu.UserMaster" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    .table thead th {
        background-color: #343a40; /* Dark background for table header */
        color: #ffffff; /* White text for header */
    }
    .table tbody tr:nth-child(odd) {
        background-color: #f9f9f9; /* Light background for odd rows */
    }
    .table tbody tr:hover {
        background-color: #e9ecef; /* Light grey background on hover */
    }
    .table th, .table td {
        text-align: center; /* Center-align text */
    }
</style>

    <script type="text/javascript">
        function check() {
            var txt_email = document.getElementById("<%=txt_email.ClientID %>");
            var txt_pwd = document.getElementById("<%=txt_pwd.ClientID %>");

            if (txt_email.value == "" || null) {
                alert("Email can not be left blank !");
                txt_email.focus();
                return false;
            }
            if (txt_pwd.value == "" || null) {
                alert("Password can not be left blank !");
                txt_pwd.focus();
                return false;
            }


            return true;
        }
        function check1() {
            var ddlemail = document.getElementById("<%=ddlemail.ClientID %>");
            var txtpwd1 = document.getElementById("<%=txtpwd1.ClientID %>");
            if (ddlemail.value == "") {
                alert("Email can not be left blank !");
                ddlemail.focus();
                return false;
            }
            if (txtpwd1.value == "") {
                alert("Pwd can not be left blank !");
                txtpwd1.focus();
                return false;
            }
            return true;
        }
    </script>
    <div class="col-sm-12">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#tab1" data-toggle="tab" aria-expanded="true">Create</a> </li>
            <li class=""><a href="#tab2" data-toggle="tab" aria-expanded="false">Update</a> </li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane fade active in" id="tab1">
                <br />
                <br />
                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="lblemail" runat="server" Text="User Name <span class='required'>*</span>" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txt_email" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="lblpwd" runat="server" Text="Password <span class='required'>*</span>"></asp:Label>
                            <asp:TextBox ID="txt_pwd" runat="server" CssClass="form-control" TextMode="Password" ></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="Label7" runat="server" Text="Status" CssClass="form-label"></asp:Label>
                            <asp:DropDownList CssClass=" form-control" ID="ddlStatus" runat="server">
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="De-Active" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="Label3" runat="server" Text="Role" CssClass="form-label"></asp:Label>
                            <asp:DropDownList CssClass=" form-control" ID="ddlRoleCreate" runat="server">
                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                <asp:ListItem Text="Standard" Value="Standard"></asp:ListItem>
                                <asp:ListItem Text="Viewer" Value="Viewer"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-12">
                    <div class="form-group">
                        <asp:Button runat="server" Text="Save" ID="Butt" OnClick="SubmitDetails" OnClientClick="if (!check()) return false;" />
                    </div>
                </div>
            </div>

            <div class="tab-pane fade" id="tab2">
                <div class="row">
                    <br />
                    <br />

                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="User Name <span class='required'>*</span>" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="ddlemail" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddluserChange" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="Password <span class='required'>*</span>"></asp:Label>
                            <asp:TextBox ID="txtpwd1" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>


                </div>


                <div class="row">
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="Label8" runat="server" Text="Status" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="status1" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                <asp:ListItem Text="De-Active" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        </div>
                    <div class="col-md-6 col-sm-12">
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="Role" CssClass="form-label"></asp:Label>
                            <asp:DropDownList CssClass=" form-control" ID="ddlRoleUpdate" runat="server">
                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                <asp:ListItem Text="Standard" Value="Standard"></asp:ListItem>
                                <asp:ListItem Text="Viewer" Value="Viewer"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
                <div class="row form-group">
                    <asp:Button runat="server" Text="Update" ID="Button2" OnClick="UpdateBtn" />
                    <asp:Button class="deletebtn" runat="server" Text="Delete" ID="Button3" OnClick="DeleteBtn" OnClientClick="if (!check1()) return false;" />
                </div>
                <div class="row">
    <div class="col-12">
        <div class="table-responsive">
            <asp:GridView runat="server" ID="tbluser" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover">
                <HeaderStyle CssClass="thead-dark text-white" />
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



</asp:Content>

