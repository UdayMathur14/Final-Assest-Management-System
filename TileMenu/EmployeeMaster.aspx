<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeMaster.aspx.cs" Inherits="TileMenu.EmployeeMaster" %>

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
            overflow: auto;
        }

        .applyColor {
            background-color: darkslategrey !important;
            color: white !important;
        }

        .table {
            border-radius: 2.5%; /* Rounded corners */
            border-collapse: separate;
            border-spacing: 0;
            margin-bottom: 1rem;
            width: 100%;
        }

        .thead-dark th {
            background-color: #343a40; /* Dark header background */
            color: white; /* White text */
            text-align: center; /* Center align header text */
            padding: 0.75rem; /* Padding for header cells */
        }

        .table td, .table th {
            vertical-align: middle; /* Vertical alignment for cells */
            padding: 0.75rem; /* Padding for cells */
            text-align: center; /* Center align text */
        }

        .table tbody tr:nth-child(odd) {
            background-color: Pink; /* Alternating row colors */
        }

        .table tbody tr:nth-child(even) {
            background-color: #fffccc; /* Even row background */
        }

        .table-responsive {
            overflow-x: auto; /* Enable horizontal scrolling on smaller screens */
        }


        .bg-light {
            background-color: lightgrey !important; /* Light background for alternating rows */
        }

        .bg-dark {
            background-color: #343a40; /* Dark background for footer */
        }

        .text-white {
            color: white; /* White text color */
        }

        .text-center {
            text-align: center; /* Center align text */
        }

        .table tbody tr:hover {
            background-color: pink; /* Slightly darker grey background on hover */
            transition: background-color; /* Smooth transition for hover effect */
        }

    </style>
    <style>
/* Add border and rounded corners to the tab container */
.custom-tabs {
    border: 2px solid #007BFF; /* Primary color for border */
    border-radius: 8px; /* Rounded corners for the container */
    padding: 5px; /* Padding inside the container */
    background-color: black; /* Light gray background */
}

/* Individual tab styling */
.custom-tabs > li {
    margin-right: 5px; /* Space between tabs */
    transition: all 0.3s ease-in-out; /* Smooth hover effect */
}

/* Styling for tab links */
.custom-tabs > li > a {
    font-size: 16px;
    font-weight: bold;
    color: black; /* Primary color for text */
    text-decoration: none;
    padding: 10px 15px; /* Padding for tab links */
    border: 1px solid double black; /* Add border around tabs */
    border-radius: 5px; /* Rounded corners for tabs */
    background-color: red; /* White background for tabs */
    display: inline-block;
}

/* Active tab styling */
.custom-tabs > li.active > a {
    background-color: greenyellow; /* Highlight active tab */
    color: green !important; /* White text */
    border-color: #0056B3; /* Darker blue border */
    border-radius:5px;
}

/* Hover effect for tabs */
.custom-tabs > li > a:hover {
    background-color: greenyellow; /* Light blue hover effect */
    color: black; /* Darker blue text on hover */
    border-color: white; /* Keep the border consistent */
    border-radius:5px;
}

/* Last tab margin cleanup */
.custom-tabs > li:last-child {
    margin-right: 0;
}
</style>

    <script type="text/javascript">
        function check() {
            var txtName = document.getElementById("<%=txt_name.ClientID %>");
            var txtcode = document.getElementById("<%=txt_code.ClientID %>");
            var txt_mail = document.getElementById("<%=txt_mail.ClientID %>");
            if (txtName.value == "") {
                alert("Name can not be left blank !");
                txtName.focus();
                return false;
            }
            if (txtcode.value == "" || null) {
                alert("Code can not be left blank !");
                txtcode.focus();
                return false;
            }
            if (txt_mail.value == "" || null) {
                alert("Email can not be left blank !");
                txt_mail.focus();
                return false;
            }

            return true;
        }
        function check1() {
            var txtcode1 = document.getElementById("<%=txtcode1.ClientID %>");
            var ddlname = document.getElementById("<%=ddlname.ClientID %>");
            if (ddlname.value == "") {
                alert("Name can not be left blank !");
                ddlname.focus();
                return false;
            }
            if (txtcode1.value == "") {
                alert("Code can not be left blank !");
                txtcode1.focus();
                return false;
            }
            return true;
        }
    </script>
    <div class="uday">
        <div class="col-sm-12" style="height:100%" >
        <ul class="nav nav-tabs custom-tabs" style="width: 15%; margin-inline: auto;">
    <li class="active">
        <a href="#tab1" data-toggle="tab" aria-expanded="true">Create</a>
    </li>
    <li>
        <a href="#tab2" data-toggle="tab" aria-expanded="false">Update</a>
    </li>
</ul>
            <div class="tab-content" style="width:70%; margin-inline: auto";>
                <div class="tab-pane fade active in" id="tab1">
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="lblemployee" runat="server" Text="Employee Name <span class='required'>*</span>" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_name" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="lblcode" runat="server" Text="Employee Code <span class='required'>*</span>"></asp:Label>
                                <asp:TextBox ID="txt_code" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="lblDeptment" runat="server" Text="Department" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_dept" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="lblDesignation" runat="server" Text="Designation <span class='required'>*</span>" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_designation" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="lblphone" runat="server" Text="Phone Number "></asp:Label>
                                <asp:TextBox ID="txt_number" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="lblemail" runat="server" Text="Email <span class='required'>*</span>" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_mail" runat="server" CssClass="form-control"></asp:TextBox>
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
                    </div>
                    <div class="col-12">
                        <div class="form-group">
                            <asp:Button runat="server" CssClass="btn btn-primary btn-save" Text="Save" ID="Button1" OnClick="Btnsave_Click" OnClientClick="if (!check()) return false;" />
                        </div>
                    </div>
                </div>

                <div class="tab-pane fade" id="tab2">
                    <div class="row">
                        <br />
                        <br />

                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Employee Name <span class='required'>*</span>" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="ddlname" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlemplChange" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Employee Code <span class='required'>*</span>"></asp:Label>
                                <asp:TextBox ID="txtcode1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" Text="Department" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txt_department" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" Text="Designation <span class='required'>*</span>" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" Text="Phone Number "></asp:Label>
                                <asp:TextBox ID="phone1" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" Text="Email <span class='required'>*</span>" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="email1" runat="server" CssClass="form-control"></asp:TextBox>
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

                    </div>
                    <div class="row form-group">
                        <asp:Button runat="server" Text="Update" ID="Button2" OnClick="UpdateBtn" CssClass="btn btn-primary btn-save" />
                        <asp:Button CssClass="btn btn-primary btn-save deletebtn" runat="server" Text="Delete" ID="Button3" OnClick="DeleteBtn" OnClientClick="if (!check1()) return false;" />

                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="table-responsive">
                                <asp:GridView runat="server" ID="tblemp" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover">
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
    </div>


</asp:Content>

