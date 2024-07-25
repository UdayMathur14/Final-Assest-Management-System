<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GatePassApproval.aspx.cs" Inherits="TileMenu.GatePassApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        fieldset {
            border: 2px solid #d3d3d3;
            border-radius: 15px;
            padding: 20px;
            margin-bottom: 20px;
            background-color: #f9f9f9;
        }

        legend {
            font-size: 1.2em;
            font-weight: bold;
            color: #333;
        }

        table {
            width: 100%;
            border-collapse: separate;
            border-spacing: 15px;
        }

        table, th, td {
            border: 1px solid #ddd;
            border-radius: 10px;
        }

        th, td {
            padding: 10px;
            background-color: #ffffff;
        }

        th {
            background-color: #4CAF50;
            color: white;
        }

        .gridview-table {
            border-collapse: separate;
            border-spacing: 10px;
            width: 100%;
            background-color: #f1f1f1;
            border: 1px solid #ddd;
            border-radius: 10px;
        }

        .gridview-table th, .gridview-table td {
            padding: 15px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .gridview-table tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        .gridview-table tr:hover {
            background-color: #f1f1f1;
        }

        .button {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .button:hover {
            background-color: #45a049;
        }

        .panel {
            padding: 20px;
            background-color: #f9f9f9;
            border: 1px solid #ddd;
            border-radius: 15px;
            margin-bottom: 20px;
        }
    </style>
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
    <div class="panel">
        <asp:Panel ID="Panel1" runat="server"> 
            <fieldset>
                <legend>GatePass Report:</legend>
                <table>
                    <tr>
                        <td style="font-weight:bold"> <label>GatePass No.:</label></td>
                        <td><asp:TextBox ID="TxtGatePassNo" runat="server" Width="150px" AutoPostBack="True" ontextchanged="TxtGatePassNo_TextChanged"></asp:TextBox></td>
                        <td style="font-weight:bold"> <label>Company Name:</label></td>
                        <td><asp:TextBox ID="txtCompanyName" runat="server" Width="150px" AutoPostBack="True" ontextchanged="txtCompanyName_TextChanged"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="font-weight:bold"> <label>Issued Date Range (From):</label></td>
                        <td><asp:TextBox ID="txtIssuedateFrom" onfocus="showCalendarControl(this);" onkeyup="abc(this);" runat="server" Width="150px" AutoPostBack="true" ontextchanged="txtIssuedateFrom_TextChanged"></asp:TextBox></td>
                        <td style="font-weight:bold"> <label>To:</label></td>
                        <td><asp:TextBox ID="txtIssuedateTo" onfocus="showCalendarControl(this);" onkeyup="abc(this);" runat="server" Width="150px" AutoPostBack="true" ontextchanged="txtIssuedateTo_TextChanged"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="font-weight:bold"> <label>Issued To:</label></td>
                        <td><asp:TextBox ID="txtIssuedTo" runat="server" Width="150px" AutoPostBack="True" ontextchanged="txtIssuedTo_TextChanged"></asp:TextBox></td>
                        <td style="font-weight:bold"> <label>Issue Type:</label></td>
                        <td><asp:RadioButtonList ID="IssueType" runat="server" AutoPostBack="True" onselectedindexchanged="IssueType_SelectedIndexChanged" RepeatDirection="Horizontal" Width="260px">
                            <asp:ListItem Selected="True" Text="All" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Returnable" Value="Returnable"></asp:ListItem>
                            <asp:ListItem Text="Non-Returnable" Value="Non-Returnable"></asp:ListItem>
                            <asp:ListItem Text="WFH-Returnable" Value="WFH-Returnable"></asp:ListItem>
                        </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td style="font-weight:bold">Product Name</td>
                        <td><asp:TextBox ID="txtproduct" runat="server" Width="150px" AutoPostBack="True" ontextchanged="txtproduct_TextChanged"></asp:TextBox></td>
                        <td style="font-weight:bold">Product Detail/serial No.</td>
                        <td><asp:TextBox ID="txtdetail" runat="server" Width="150px" AutoPostBack="True" ontextchanged="txtdetail_TextChanged"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Search" CssClass="button" /></td>
                        <td></td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
    
    <div class="panel">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" CssClass="gridview-table" DataKeyNames="GatePass_Id" OnRowDataBound="GridView1_RowDataBound" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                <FooterStyle BackColor="#E3E3E1" />
                <RowStyle BackColor="#E3E3E1" Height="40px" BorderStyle="Solid" />
                <Columns>
                    <asp:TemplateField HeaderText="Approve/Cancel">   
                        <ItemTemplate>   
                            <asp:Button ID="btn_Edit" runat="server" Text="Approve/Cancel" CommandName="Edit" CssClass="button" />   
                        </ItemTemplate>   
                        <EditItemTemplate>   
                            <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" CssClass="button" />   
                            <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="button" />   
                        </EditItemTemplate>   
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">   
                        <ItemTemplate>   
                            <asp:Label ID="lbl_Status" runat="server" Text='<%#Eval("GatePass_Status") %>'></asp:Label>   
                        </ItemTemplate>   
                        <EditItemTemplate>   
                            <asp:DropDownList runat="server" ID="ddlstatus">
                                <asp:ListItem Text="Submitted"></asp:ListItem>
                                <asp:ListItem Text="Approved"></asp:ListItem>
                                <asp:ListItem Text="Cancelled"></asp:ListItem>
                            </asp:DropDownList>  
                        </EditItemTemplate>   
                    </asp:TemplateField> 
                    <asp:BoundField HeaderText="GPNo" DataField="GatePass_Id" ItemStyle-Font-Bold="false" />
                    <asp:BoundField HeaderText="Type" DataField="GatePass_Returnable" ItemStyle-Font-Bold="false" />
                    <asp:BoundField HeaderText="Date" DataField="IssueDate" ItemStyle-Font-Bold="false" />
                    <asp:BoundField HeaderText="IssuedTo" DataField="GatePass_IssuedTo" ItemStyle-Font-Bold="false" />
                    <asp:BoundField HeaderText="ContactNo" DataField="GatePass_IssuedContact" ItemStyle-Font-Bold="false" />
                    <asp:BoundField HeaderText="Company" DataField="GatePass_IssuedCompany" ItemStyle-Font-Bold="false" />
                    <asp:BoundField HeaderText="Reason" DataField="GatePass_IssuedReason" ItemStyle-Font-Bold="false" />
                    <asp:TemplateField HeaderText="Print" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Black">
                        <ItemTemplate>
                            <asp:HyperLink ID="PrintGP" runat="server" Target="_blank" Text="Print" NavigateUrl='<%# Eval("GatePass_Id", "GatepassPrint.aspx?GPId={0}") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Detail" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Black"> 
                        <ItemTemplate>
                            <asp:GridView ID="gvdetail" runat="server" AutoGenerateColumns="False" ForeColor="Black" GridLines="Both" CssClass="gridview-table" DataKeyNames="id" OnRowDeleting="gvdetail_RowDeleting" ShowHeader="True">
                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="GatePass_Detail_Item" HeaderText="Particulars" />
                                    <asp:BoundField DataField="GatePass_Detail_Type" HeaderText="Detail" />
                                    <asp:BoundField DataField="GatePass_Detail_ReturnStatus" HeaderText="Rtn Status" />
                                    <asp:BoundField DataField="RtnDate" HeaderText="Rtn Date" />
                                    <asp:CommandField HeaderText="Return" ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/imagesreturn.png" />  
                                    <asp:BoundField DataField="srd" HeaderText="Rtn Inventory" />
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#D72B37" Font-Bold="True" ForeColor="White" Height="20px" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </asp:Panel>
    </div>
    </div>
    </div>
</asp:Content>
