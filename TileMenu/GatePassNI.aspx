<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GatePassNI.aspx.cs" Inherits="TileMenu.GatePassNI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
         function validate() {


             var txtIssuedate = document.getElementById("<%=txtIssuedate.ClientID %>");
             var txtIssuedTo = document.getElementById("<%=txtIssuedTo.ClientID %>");
             var txtCompanyName = document.getElementById("<%=txtCompanyName.ClientID %>");
             var txtAddress = document.getElementById("<%=txtAddress.ClientID %>");
             var txtmobile = document.getElementById("<%=txtmobile.ClientID %>");
             var txtreason = document.getElementById("<%=txtreason.ClientID %>");                   
             var txtqty = document.getElementById("<%=txtqty.ClientID %>");


             if (txtIssuedate.value == "") {
                 alert("Issue Date can not be left blank !");
                 txtIssuedate.focus();
                 return false;
             }
             if (txtIssuedTo.value == "") {
                 alert("Issued To can not be left blank !");
                 txtIssuedTo.focus();
                 return false;
             }
             if (txtCompanyName.value == "") {
                 alert("Company Name can not be left blank !");
                 txtCompanyName.focus();
                 return false;
             }
             if (txtAddress.value == "") {
                 alert("Address can not be left blank !");
                 txtAddress.focus();
                 return false;
             }
             if (txtmobile.value == "") {
                 alert("Mobile No. can not be left blank !");
                 txtmobile.focus();
                 return false;
             }
             if (txtreason.value == "") {
                 alert("Issued Reason can not be left blank !");
                 txtreason.focus();
                 return false;
             }
             if (txtqty.value == "" || txtqty.value == "0") {
                 alert("Issued Qty can not be left blank !");
                 txtqty.focus();
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
    <div class="row">
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Issued To" CssClass="form-label"></asp:Label>
               <asp:RadioButtonList ID="IssuedTo" runat="server" 
                            RepeatDirection="Horizontal" AutoPostBack="true"  CssClass="form-label" OnSelectedIndexChanged="IssuedTo_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="Repaire/Replacement/Sold" Value="Others"></asp:ListItem>
                                                   
                          </asp:RadioButtonList> 
            </div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Issue Type" CssClass="form-label"></asp:Label>
               <asp:RadioButtonList ID="IssueType" runat="server" 
                            RepeatDirection="Horizontal" CssClass="form-label">
                          <asp:ListItem Selected="True" Text="Returnable" Value="Returnable" ></asp:ListItem>
                          <asp:ListItem Text="Non-Returnable" Value="Non-Returnable"></asp:ListItem>
                         
                                                   
                          </asp:RadioButtonList>
            </div>
        </div>

    </div>
     <div class="row">
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="GatePass No." CssClass="form-label"></asp:Label>
               <asp:TextBox ID="TxtGatePassNo" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
            </div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="Label4" runat="server" Text="Issue Date" CssClass="form-label"></asp:Label>
               <asp:TextBox ID="txtIssuedate" runat="server" Enabled="false"  CssClass="form-control datepicker" ></asp:TextBox>
            </div>
        </div>

    </div>
    <div class="row">
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="Label5" runat="server" Text="Issued To" CssClass="form-label"></asp:Label>
               <asp:TextBox ID="txtIssuedTo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="Label6" runat="server" Text="Company Name" CssClass="form-label"></asp:Label>
               <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

    </div>
     <div class="row">
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="Label7" runat="server" Text="Address" CssClass="form-label"></asp:Label>
               <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="Label8" runat="server" Text="Mobile" CssClass="form-label"></asp:Label>
               <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

    </div>
     <div class="row">
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
                <asp:Label ID="Label9" runat="server" Text="Reason" CssClass="form-label"></asp:Label>
               <asp:TextBox ID="txtreason" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div class="form-group">
          <asp:Label ID="Label10" runat="server" Text="Qty" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtqty" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtqty_TextChanged"></asp:TextBox>
                 </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <asp:GridView ID="gvSerial" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowDataBound="gvSerial_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="SlNo" HeaderText="Sl. No." />
                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:TextBox ID="txtitem" runat="server"  CssClass="form-control" ></asp:TextBox>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SerialNo.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtserial" runat="server"  CssClass="form-control" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please fill serial No." ForeColor="Red" ControlToValidate="txtserial" ValidationGroup="GatePassNI" EnableClientScript="true" ></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <div class="form-group">
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" OnClientClick="return validate();" CausesValidation="true" ValidationGroup="GatePassNI"></asp:Button>

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
</asp:Content>
