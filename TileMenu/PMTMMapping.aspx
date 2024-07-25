<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PMTMMapping.aspx.cs" Inherits="TileMenu.PMTMMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .uday{
            border:2px solid black;
            padding-top:70px;
            min-height:100vh;
            background-image:url('/Images/back1.jpg');
            background-repeat: no-repeat;
            background-size: cover;
            width:100%;
        }
        .form-label {
            font-weight: bold;
        }

        .form-group {
            margin-bottom: 1.5rem;
        }

        .btn-custom {
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            border-radius: 5px;
            border: none;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            transition: background-color 0.3s, transform 0.3s;
        }

            .btn-custom:hover {
                background-color: #0056b3;
                color: white;
                transform: translateY(-2px);
            }

        .center-button {
            display: flex;
            justify-content: center;
            align-items: center;
        }
    </style>

    <div class="container mt-4 uday" >
        <div style="width:70%; margin-inline: auto">
        <div class="row mb-4">
            <div class="col-md-6 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="lblproduct" runat="server" Text="Product" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="ddlproduct" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="lblmake" runat="server" Text="Make" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="ddlmake" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row mb-4">
            <div class="col-md-6 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="lbltype" runat="server" Text="Type" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 col-sm-12">
                <div class="form-group">
                    <asp:Label ID="lblmodel" runat="server" Text="Model" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="ddlmodel" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12 center-button">
                <asp:Button ID="btnsubmit" runat="server" Text="Map" OnClick="btnsubmit_Click" CssClass="btn btn-custom" Style="margin-left: -195px" />
            </div>
        </div>
            </div>
    </div>
</asp:Content>
