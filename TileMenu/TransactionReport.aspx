<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransactionReport.aspx.cs" Inherits="TileMenu.TransactionReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
         <div class="row">
               
               <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblserial" runat="server" Text="Serial No." CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtserial"  runat ="server" Width ="150px" 
                            AutoPostBack="True" ontextchanged="txtserial_TextChanged" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
             <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Employee Name" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtEmpName"  runat ="server" Width ="150px" 
                            AutoPostBack="True" ontextchanged="txtEmpName_TextChanged" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

          </div>

    <div class="row">
                <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                 
                        <label>
                            Date From:</label>
                     <asp:TextBox ID="txtFdate" CssClass="form-control datepicker" runat ="server"  Width ="150px" ></asp:TextBox>
  
               </div>
                    </div>
              <div class="col-md-6 col-sm-12">
                    <div class="form-group">
                 
                        <label>
                            Date To:</label>
                     <asp:TextBox ID="txtTdate" CssClass="form-control datepicker" runat ="server"  Width ="150px" ></asp:TextBox>
  
               </div>
                  </div>    
        </div>
     <div class="row">
          <div class="col-12">
            <div class="form-group">
                    <asp:Button ID="btnsubmit" runat="server"  
                        Text="Search" Width="76px"  Height="26px" onclick="btnsubmit_Click"  />
                </div></div></div>
    <div >
     
     <asp:Panel id="pnlData" runat="server">
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ForeColor="Black" GridLines="Both"  BackColor="White" 
                            BorderColor="#666666" BorderStyle="1" BorderWidth="1px" 
                        Width ="100%" CellPadding="5" RowStyle-Height="45px" >
        <FooterStyle BackColor="#E3E3E1" />
        <RowStyle BackColor="#ffffcc" />
        
         <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
         <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="35px" />
        <AlternatingRowStyle BackColor="White" />
         </asp:GridView>
     </asp:Panel>
    
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
