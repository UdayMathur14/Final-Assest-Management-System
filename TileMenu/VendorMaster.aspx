<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VendorMaster.aspx.cs" Inherits="TileMenu.VendorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function validate() {
            var vendorname = document.getElementById("<%=txtvendor.ClientID %>").value.trim();
            var address = document.getElementById("<%=txtaddress.ClientID %>").value.trim();
            var city = document.getElementById("<%=txtcity.ClientID %>").value.trim();
            var zip = document.getElementById("<%=txtzip.ClientID %>").value.trim();
            var region = document.getElementById("<%=txtregion.ClientID %>").value.trim

            if (vendorname === '') {
                alert('Vendor Name Can Not be left blank !');
                return false;
            }

            else if (address === '') {
                alert('Address Can Not be left blank !');
                return false;
            }
            else if (city === '') {
                alert('City Can Not be left blank !');
                return false;
            }
            else if (zip === '') {
                alert('Zip Code Can Not be left blank !');
                return false;
            }
            else if (region === '') {
                alert('Region Can Not be left blank !');
                return false;
            }
            else {
                return true;
            }
        }



        function validateSM() {
            var smname = document.getElementById("<%=txtsmname.ClientID %>").value.trim();
            var smmobile = document.getElementById("<%=txtsmmobile.ClientID %>").value.trim();
            var smemail = document.getElementById("<%=txtsmemail.ClientID %>").value.trim();

            if (smname === '') {
                alert('Sales Manager Name Can Not be left blank !');
                return false;
            }
            else if (smmobile === '') {
                alert('Sales Manager Mobile Can Not be left blank !');
                return false;
            }
            else if (smemail === '') {
                alert('Sales Manager Email Can Not be left blank !');
                return false;
            }
            else {
                return true;
            }
        }


        function validateAM() {
            var amname = document.getElementById("<%=txtamname.ClientID %>").value.trim();
            var ammobile = document.getElementById("<%=txtammobile.ClientID %>").value.trim();
            var amemail = document.getElementById("<%=txtamemail.ClientID %>").value.trim();

            if (amname === '') {
                alert('Account Manager Name Can Not be left blank !');
                return false;
            }
            else if (ammobile === '') {
                alert('Account Manager Mobile Can Not be left blank !');
                return false;
            }
            else if (amemail === '') {
                alert('Account Manager Email Can Not be left blank !');
                return false;
            }
            else {
                return true;
            }
        }

    </script>
    <style>
            .uday {
    padding-top:70px;
    min-height:100vh;
    border:2px solid black;
    width:100%;
    background-image:url('/Images/back1.jpg');
    background-image:url('/Images/back1.jpg');
    background-repeat: no-repeat;
    background-size: cover;
    
}
    </style>
    <div class="uday">

 
    <div class="logine" style="width:70%; margin-inline: auto">
        <asp:Panel ID="Panel1" runat="server">
            <fieldset>
                <legend><b>Vendor/Supplier Master :</b><asp:Label ID="lblid" runat="server"></asp:Label></legend>
                <table style="height: 200px">
                    <tr>
                        <td>
                            <table border="1">
                                <caption>
                                    <tr>
                                        <td>
                                            <label>Vendor/Supplier Name:<span class="required" style="color: red">*</span></label></td>
                                        <td>
                                            <asp:TextBox ID="txtvendor" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                        <td>
                                            <label>Vendor Code(SAP):</label></td>
                                        <td>
                                            <asp:TextBox ID="txtvendorcode" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>

                                    </tr>
                                </caption>



                                <caption>
                                    <tr>
                                        <td>
                                            <label>Business Phone:</label></td>
                                        <td>
                                            <asp:TextBox ID="txtbusinessphone" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                        <td>
                                            <label>Toll Free No.:</label></td>
                                        <td>
                                            <asp:TextBox ID="txttollfree" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                    </tr>
                                </caption>
                                <caption>
                                    <tr>
                                        <td>
                                            <label>Address:<span class="required" style="color: red">*</span></label></td>
                                        <td>
                                            <asp:TextBox ID="txtaddress" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                        <td>
                                            <label>City:<span class="required" style="color: red">*</span></label></td>
                                        <td>
                                            <asp:TextBox ID="txtcity" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                    </tr>
                                </caption>
                                <caption>
                                    <tr>
                                        <td>
                                            <label>Region:<span class="required" style="color: red">*</span></label></td>
                                        <td>
                                            <asp:TextBox ID="txtregion" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                        <td>
                                            <label>Zip:<span class="required" style="color: red">*</span></label></td>
                                        <td>
                                            <asp:TextBox ID="txtzip" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                    </tr>
                                </caption>
                                <caption>
                                    <tr>
                                        <td>
                                            <label>Support Page:</label></td>
                                        <td>
                                            <asp:TextBox ID="txtsupportpage" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                        <td>
                                            <label>Notes:</label></td>
                                        <td>
                                            <asp:TextBox ID="txtnotes" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                    </tr>
                                </caption>
                                <caption>
                                    <tr>
                                        <td>
                                            <label>Customer Id:</label></td>
                                        <td>
                                            <asp:TextBox ID="txtCustomerId" runat="server" Enabled="True" Height="25px" Width="200px"></asp:TextBox></td>
                                        <td>
                                            <label>
                                                Status:<span class="required" style="color: red">*</span></label></td>
                                        <td>
                                            <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="false" Height="25px"
                                                Width="150px">
                                                <asp:ListItem Text="ACTIVE" Value="ACTIVE" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="INACTIVE" Value="INACTIVE"></asp:ListItem>

                                            </asp:DropDownList>

                                        </td>
                                    </tr>

                                </caption>
                            </table>
                        </td>


                    </tr>
                    
                    <td><br></td>

                    <tr>
                        <td colspan="4" align="center" style="display:flex;gap:2em;margin-inline:auto">
                            <asp:Button  ID="btnsubmit" runat="server" CssClass="btn btn-primary" 
                                Text="Save"  OnClick="btnsubmit_Click" OnClientClick="return validate(); " />

                            <asp:Button ID="btncancel" runat="server" Text="Cancle"  style="text-align:center;" OnClick="btncancel_Click" CssClass="btn btn-primary btn-save" />
                        </td>
                    </tr>
                </table>

                <br />





            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlSM" runat="server" Visible="true">
            <fieldset>
                <legend><b>Sales Manager:</b></legend>
                <table border="1" width="100%">
                    <tr style="background-color: #D72B37; color: White; height: 20px; font-weight: bold">
                        <td>Name</td>
                        <td>Mobile</td>
                        <td>Email</td>
                        <td>ADD</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtsmname" runat="server" Width="150px" Height="20px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtsmmobile" runat="server" Width="150px" Height="20px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtsmemail" runat="server" Width="150px" Height="20px"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="btnaddSM" runat="server" Text="ADD" OnClick="btnaddSM_Click"
                                OnClientClick="return validateSM();" Width="67px"></asp:Button></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvSMnew" runat="server" AutoGenerateColumns="False" ForeColor="Black" GridLines="Both" BackColor="White"
                                BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px"
                                CellPadding="5" RowStyle-Height="30px" Width="100%" DataKeyNames="id">

                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />

                                <Columns>
                                    <asp:BoundField DataField="SMName" HeaderText="SM Name" />
                                    <asp:BoundField DataField="SMMobile" HeaderText="Mobile" />
                                    <asp:BoundField DataField="SMemail" HeaderText="SM Email" />



                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <div class="clearH10"></div>




        </fieldset>
      
      </asp:Panel>
     <asp:Panel ID="pnlAM" runat="server" Visible="true">
         <fieldset>
             <legend><b>Account Manager:</b></legend>
             <table border="1" width="100%">
                 <tr style="background-color: #D72B37; color: White; height: 20px; font-weight: bold">
                     <td>Name</td>
                     <td>Mobile</td>
                     <td>Email</td>
                     <td>ADD</td>
                 </tr>
                 <tr>
                     <td>
                         <asp:TextBox ID="txtamname" runat="server" Width="150px" Height="20px"></asp:TextBox></td>
                     <td>
                         <asp:TextBox ID="txtammobile" runat="server" Width="150px" Height="20px"></asp:TextBox></td>
                     <td>
                         <asp:TextBox ID="txtamemail" runat="server" Width="150px" Height="20px"></asp:TextBox></td>
                     <td>
                         <asp:Button ID="btnaddAM" runat="server" Text="ADD" OnClick="btnaddAM_Click"
                             OnClientClick="return validateAM();" Width="67px"></asp:Button></td>
                 </tr>
                 <tr>
                     <td colspan="4">
                         <asp:GridView ID="gvAMnew" runat="server" AutoGenerateColumns="False" ForeColor="Black" GridLines="Both" BackColor="White"
                             BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px"
                             CellPadding="5" RowStyle-Height="30px" Width="100%" DataKeyNames="id">

                             <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />

                             <Columns>
                                 <asp:BoundField DataField="AMName" HeaderText="SM Name" />
                                 <asp:BoundField DataField="AMMobile" HeaderText="Mobile" />
                                 <asp:BoundField DataField="AMemail" HeaderText="SM Email" />



                             </Columns>
                         </asp:GridView>
                     </td>
                 </tr>
             </table>
         </fieldset>
     </asp:Panel>
        <div class="clearH10"></div>




        </fieldset>
      
      </asp:Panel>
      <asp:Panel ID="panel2" runat="server">
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
              ForeColor="Black" GridLines="Both" BackColor="White"
              BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px"
              Width="100%" CellPadding="5" AlternatingRowStyle-BorderColor="AliceBlue" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" DataKeyNames="Vendor_Id">
              <FooterStyle BackColor="#E3E3E1" />
              <RowStyle BackColor="#E3E3E1" />
              <Columns>

                  <asp:BoundField DataField="Vendor_Name" HeaderText="Name" />
                  <asp:BoundField DataField="Vendor_SAPCode" HeaderText="SAP Code" />
                  <asp:BoundField DataField="Vendor_CustomerId" HeaderText="Customer Id" />
                  <asp:BoundField DataField="Vendor_BusinessPhone" HeaderText="Business Phone" />
                  <asp:BoundField DataField="Vendor_TollFreeNo" HeaderText="TollFree" />


                  <asp:TemplateField HeaderText="Sales Manager" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Black">
                      <ItemTemplate>
                          <asp:GridView ID="gvSM" runat="server" AutoGenerateColumns="False" ForeColor="Black" GridLines="Both" BackColor="White"
                              BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px"
                              CellPadding="5" RowStyle-Height="30px" Width="90%" DataKeyNames="id">

                              <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />

                              <Columns>
                                  <asp:BoundField DataField="SMName" HeaderText="SM Name" />
                                  <asp:BoundField DataField="SMMobile" HeaderText="Mobile" />



                              </Columns>
                          </asp:GridView>
                      </ItemTemplate>

                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Account Manager" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Black">
                      <ItemTemplate>
                          <asp:GridView ID="gvAM" runat="server" AutoGenerateColumns="False" ForeColor="Black" GridLines="Both" BackColor="White"
                              BorderColor="#666666" BorderStyle="Solid" BorderWidth="2px"
                              CellPadding="5" RowStyle-Height="30px" Width="90%" DataKeyNames="id">

                              <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />

                              <Columns>
                                  <asp:BoundField DataField="AMName" HeaderText="AM Name" />
                                  <asp:BoundField DataField="AMMobile" HeaderText="Mobile" />



                              </Columns>
                          </asp:GridView>
                      </ItemTemplate>

                  </asp:TemplateField>

                  <asp:CommandField HeaderText="UPDATE" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Update.png" />
                  <asp:BoundField DataField="Vendor_Id" HeaderText="Id" />
              </Columns>
          </asp:GridView>

      </asp:Panel>
    </div>
           </div>
</asp:Content>
