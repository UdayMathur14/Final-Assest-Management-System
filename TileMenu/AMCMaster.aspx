<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AMCMaster.aspx.cs" Inherits="TileMenu.AMCMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">



        function validate() {
            var txtdescription = document.getElementById("<%=txtdescription.ClientID %>");
             var txtpo = document.getElementById("<%=txtpo.ClientID %>");
             var txtpodate = document.getElementById("<%=txtpodate.ClientID %>");
             var txtpostartdate = document.getElementById("<%=txtpostartdate.ClientID %>");
             var txtpoenddate = document.getElementById("<%=txtpoenddate.ClientID %>");
             var txtpovalue = document.getElementById("<%=txtpovalue.ClientID %>");
             var txtresponsetime = document.getElementById("<%=txtresponsetime.ClientID %>");
             var txtresolutiontime = document.getElementById("<%=txtresolutiontime.ClientID %>");

             var ddlvendor = document.getElementById("<%=ddlvendor.ClientID %>").value;

             var ddlresponsible = document.getElementById("<%=ddlresponsible.ClientID %>").value;
             var ddlprocurementtype = document.getElementById("<%=ddlprocurementtype.ClientID %>").value;
             var ddlalert = document.getElementById("<%=ddlalert.ClientID %>").value;




            if (!IsBlank(txtdescription, 'Description Can Not be left blank !'))
                return false;
            else if (!IsBlank(txtpo, 'PO Number Can Not be left blank !'))
                return false;
            else if (!IsBlank(txtpodate, 'PO Date Can Not be left blank !'))
                return false;
            else if (!IsBlank(txtpostartdate, 'PO Start Date Can Not be left blank !'))
                return false;
            else if (!IsBlank(txtpoenddate, 'PO End Date Can Not be left blank !'))
                return false;
            else if (!IsBlank(txtpovalue, 'PO Value Can Not be left blank !'))
                return false;

            else if (ddlvendor == "-1") {
                alert("Please Select Vendor");
                return false;
            }
            else if (!IsBlank(txtresponsetime, 'Response Time Can Not be left blank !'))
                return false;
            else if (!IsBlank(txtresolutiontime, 'Resolution Time Can Not be left blank !'))
                return false;

            else if (ddlprocurementtype == "-1") {
                alert("Please Select Warranty/AMC");
                return false;
            }
            else if (ddlalert == "-1") {
                alert("Please Select Alert Required Y/N");
                return false;
            }
            else if (ddlresponsible == "-1") {
                alert("Please Select Responsible Person Name");
                return false;
            }
            else
                return true;
        }
    </script>

    <style>
        .uday {
            padding-top: 70px;
            min-height: 100vh;
            border: 2px solid black;
            width: 100%;
            background-image: url('/Images/back1.jpg');
            background-image: url('/Images/back1.jpg');
            background-repeat: no-repeat;
            background-size: cover;
        }
    </style>
    <div class="uday">
        <div style="width: 70%; margin-inline: auto">
            <div class="logine">
                <asp:Panel ID="Panel1" runat="server">
                    <fieldset>
                        <legend><b>AMC Master :</b><asp:Label ID="lblid" runat="server"></asp:Label></legend>
                        <table style="height: 200px">
                            <tr>
                                <td>
                                    <table border="1">
                                        <tr>
                                            <td>
                                                <label><b>Title/Description:</b><span class="required" style="color: red">*</span></label></td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtdescription" runat="server" Enabled="True" Height="25px" Width="550px"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label><span style="color: green">@</span>PO Number:<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:TextBox ID="txtpo" runat="server" Enabled="True" Height="25px" Width="200px" AutoPostBack="True" OnTextChanged="txtpo_TextChanged"></asp:TextBox></td>
                                            <td>
                                                <label>PO Date:<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:TextBox ID="txtpodate" class="datepicker" runat="server" Enabled="True" Height="25px" Width="190px"></asp:TextBox></td>

                                        </tr>


                                        <tr>
                                            <td>
                                                <label>PO/Support Start Date:<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:TextBox ID="txtpostartdate" class="datepicker" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                            <td>
                                                <label>PO/Support End Date:<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:TextBox ID="txtpoenddate" class="datepicker" runat="server" Enabled="True" Height="25px" Width="190px"></asp:TextBox></td>

                                        </tr>


                                        <tr>
                                            <td>
                                                <label>PO Value:<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:TextBox ID="txtpovalue" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                            <td>
                                                <label>Location.:</label></td>
                                            <td>
                                                <asp:TextBox ID="txtlocation" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                        </tr>


                                        <tr>
                                            <td>
                                                <label><span style="color: green">@</span>Vendor/Supplier:<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlvendor" runat="server" AutoPostBack="true" Height="25px" Width="150px" OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged"></asp:DropDownList></td>
                                            <td>
                                                <label>OEM:</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddloem" runat="server" AutoPostBack="false" Height="25px" Width="150px"></asp:DropDownList></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label>Sales Manager:</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlsales" runat="server" Height="25px" Width="150px"></asp:DropDownList></td>
                                            <td>
                                                <label>Account Manager:</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlaccount" runat="server" Height="25px" Width="150px"></asp:DropDownList></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label>Response Time(In Hrs):<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:TextBox ID="txtresponsetime" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                            <td>
                                                <label>Resolution Time(In Hrs):<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:TextBox ID="txtresolutiontime" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label>SLA:</label></td>
                                            <td>
                                                <asp:TextBox ID="txtSLA" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                            <td>
                                                <label>Availability:</label></td>
                                            <td>
                                                <asp:TextBox ID="txtavailability" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                        </tr>

                                        <tr id="tr1" runat="server" visible="false">
                                            <td>
                                                <label>Support Start Date:<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:TextBox ID="txtsupportstartdate" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox>mm/dd/yyyy</td>
                                            <td>
                                                <label>Support End Date:<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:TextBox ID="txtsupportenddate" runat="server" Enabled="True" Height="25px" Width="190px"></asp:TextBox>mm/dd/yyyy</td>

                                        </tr>

                                        <tr>
                                            <td>
                                                <label>Product Type:</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlproducttype" runat="server" AutoPostBack="false" Height="25px" Width="150px">
                                                    <asp:ListItem Text="Hardware" Value="Hardware" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Software" Value="Software"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <label>Product Category:</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlproductcategory" runat="server" AutoPostBack="false" Height="25px" Width="150px">
                                                    <asp:ListItem Text="Client" Value="Client" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Data Center" Value="Data Center"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label><span style="color: green">@</span>Procurement Type:<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlprocurementtype" runat="server" AutoPostBack="true" Height="25px" Width="150px" OnSelectedIndexChanged="ddlprocurementtype_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Warranty" Value="Warranty"></asp:ListItem>
                                                    <asp:ListItem Text="AMC" Value="AMC"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <label>License Key:</label></td>
                                            <td>
                                                <asp:TextBox ID="txtlicense" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label><span style="color: green">@</span>Alert Required:<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlalert" runat="server" AutoPostBack="true" Height="25px" Width="150px" OnSelectedIndexChanged="ddlalert_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <label><span style="color: green">@</span>Responsibility:<span class="required" style="color: red">*</span></label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlresponsible" runat="server" AutoPostBack="true" Height="25px" Width="150px" OnSelectedIndexChanged="ddlresponsible_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Parshant Sharma" Value="parshant.sharma@khd.com"></asp:ListItem>
                                                    <asp:ListItem Text="Abhishek Kapoor" Value="Abhishek.kapoor@khd.com"></asp:ListItem>
                                                    <asp:ListItem Text="Santosh Roy" Value="santosh.roy@khd.com"></asp:ListItem>
                                                    <asp:ListItem Text="Rajnish Singh" Value="rajnish.singh@kjd.com"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <label>Status:</label></td>
                                            <td>
                                                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" Height="25px"
                                                    Width="150px" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                    <asp:ListItem Text="ACTIVE" Value="ACTIVE" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="INACTIVE" Value="INACTIVE"></asp:ListItem>

                                                </asp:DropDownList>

                                            </td>
                                            <td>
                                                <label>Attachment:</label></td>
                                            <td>
                                                <asp:TextBox ID="txtattachment" runat="server" Enabled="false" Height="25px" Width="200px"></asp:TextBox></td>
                                        </tr>





                                    </table>
                                </td>

                            </tr>
                            <br />
                            <tr>

                                <td style="margin-bottom: 10px" align="center">
                                    <asp:Button ID="btnsubmit" runat="server"
                                        Text="Save" CssClass="btn btn-primary btn-save" OnClick="btnsubmit_Click" OnClientClick="return validate();" />
                                    <asp:Button ID="btncancel" runat="server" CssClass="btn btn-primary btn-save" Text="Cancel" OnClick="btncancel_Click" />
                                </td>
                            </tr>
                        </table>






                    </fieldset>
                </asp:Panel>
                <asp:Panel ID="panel2" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        ForeColor="Black" GridLines="Both" BackColor="White"
                        BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px"
                        Width="90%" CellPadding="5" AlternatingRowStyle-BorderColor="AliceBlue" OnRowDeleting="GridView1_RowDeleting" DataKeyNames="AMCMaster_Id">
                        <FooterStyle BackColor="#E3E3E1" />
                        <RowStyle BackColor="#E3E3E1" />
                        <Columns>
                            <asp:BoundField DataField="sno" HeaderText="Sr.NO." />
                            <asp:BoundField DataField="AMCMaster_Description" HeaderText="Title/Description" />
                            <asp:BoundField DataField="AMCMaster_PONO" HeaderText="PO Number" />
                            <asp:BoundField DataField="POEndDate" HeaderText="PO_EndDate" />
                            <asp:BoundField DataField="OEM_Name" HeaderText="OEM" />
                            <asp:BoundField DataField="Vendor_Name" HeaderText="Vendor Name" />

                            <asp:BoundField DataField="AMCMaster_ProcurementType" HeaderText="Proc. Type" />
                            <asp:BoundField DataField="Responsible" HeaderText="Responsibility" />
                            <asp:BoundField DataField="AMCMaster_Event" HeaderText="Alert Req." />
                            <asp:BoundField DataField="AMCMaster_Status" HeaderText="Status" />
                            <asp:BoundField DataField="AMCMaster_Id" HeaderText="Id" />
                            <asp:CommandField HeaderText="UPDATE" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Update.png" />
                        </Columns>
                    </asp:GridView>

                </asp:Panel>
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
</asp:Content>
