    <%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TileMenu.Login" %>

<%--<%@ Master Language="C#" AutoEventWireup="true" CodeBehind="Site.master.cs" Inherits="TileMenu.SiteMaster" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockOut.aspx.cs" EnableEventValidation = false Inherits="TileMenu.StockOut" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <style>
        @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600;700&display=swap');

        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Montserrat', sans-serif;
        }


        body {
           background-image: linear-gradient(rgba(255,255,255,0.7), rgba(255,255,255,0.7)), url('../../Background-01.jpg');

            background-repeat: no-repeat !important;
            background-size: cover !important;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-direction: column;
            height: 100vh;
        }


        .logo {
            position: absolute;
            top: 20px;
            left: 20px;
            font-size: 18px;
        }


        .container {
            background-color: #fff;
            border-radius: 30px;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.35);
            position: relative;
            overflow: hidden;
            width: 768px;
            max-width: 100%;
            min-height: 480px;
        }


            .container p {
                font-size: 14px;
                line-height: 20px;
                letter-spacing: 0.3px;
                margin: 20px 0;
            }


            .container a {
                color: white;
                font-size: 13px;
                text-decoration: none;
                margin: 15px 0 10px;
            }

                .container a.btn_submit {
                    color: black;
                }

        #btnLogin {
            background-color: #512da8;
            color: #fff;
            font-size: 12px;
            padding: 10px 45px;
            border: 1px solid transparent;
            border-radius: 8px;
            font-weight: 600;
            letter-spacing: 0.5px;
            text-transform: uppercase;
            margin-top: 10px;
            cursor: pointer;
        }

            #btnLogin:hover {
                background-color: blue;
            }


        .container button.hidden {
            background-color: transparent;
            border-color: #fff;
        }


        .container form {
            background-color: whitesmoke;
            /*background-color: #fff;*/
            display: flex;
            align-items: center;
            justify-content: center;
            flex-direction: column;
            padding: 0 40px;
            height: 100%;
        }


        .container input {
            background-color: #eee;
            border: none;
            margin: 8px 0;
            padding: 10px 15px;
            font-size: 13px;
            border-radius: 8px;
            width: 100%;
            outline: none;
        }


        .form-container {
            position: absolute;
            top: 0;
            height: 100%;
            transition: all 0.6s ease-in-out;
        }


        .log-in {
            left: 0;
            width: 50%;
            z-index: 2;
        }


        .container.active .log-in {
            transform: translateX(100%);
        }


        .toggle-container {
            position: absolute;
            top: 0;
            left: 50%;
            width: 50%;
            height: 100%;
            overflow: hidden;
            transition: all 0.6s ease-in-out;
            border-radius: 0 0 0 200px;
            z-index: 1000;
        }


        .container.active .toggle-container {
            transform: translateX(-100%);
            border-radius: 0 150px 100px 0;
        }


        .toggle {
            height: 100%;
            background: linear-gradient(#638bff47,#ff9c9c);
            color: #fff;
            position: relative;
            left: -100%;
            height: 100%;
            width: 200%;
            transform: translateX(0);
            transition: all 0.6s ease-in-out;
        }


        .container.active .toggle {
            transform: translateX(50%);
        }


        .toggle-panel {
            position: absolute;
            width: 50%;
            height: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-direction: column;
            padding: 0 30px;
            text-align: center;
            top: 0;
            transform: translateX(0);
            transition: all 0.6s ease-in-out;
        }


        .toggle-right {
            right: 0;
            transform: translateX(0);
        }


        .container.active .toggle-right {
            transform: translateX(200%);
        }
        
    </style>

    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />
    <link href="BillLogo.css" rel="stylesheet" />
    <title>Login</title>

</head>
<body class="imageBackground">
    <div class="container" id="container">
        <div class="form-container log-in">
            <form runat="server">
                <h1>Log In</h1>
                <br />
                <asp:TextBox ID="txt_mail" runat="server" placeholder="Email" CssClass="form-control"></asp:TextBox>
                <asp:TextBox ID="txt_password" runat="server" placeholder="Password" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="loginClickBtn" OnClientClick="if (!check()) return false;"  />
                <br />
<p class="mb-0" style="color: #333; font-size: 14px; text-align: center;">
    <strong>Powered By</strong> 
    <a class="btn_submit" href="https://www.ennobleip.com/" target="_blank" style="color: red; font-size: 17px; font-weight: bold; border-bottom: 2px solid #007BFF;">ENNOBLE IP</a>
</p>
            </form>
        </div>
        <div class="toggle-container">
            <div class="toggle">
                <div class="toggle-panel toggle-right">
                    <img src="BillLogo.png" style="width:383px"/>
                </div>
            </div>
        </div>
    </div>
</body>
</html>

<script type="text/javascript">
    function check() {
        var txtEmail = document.getElementById("<%=txt_mail.ClientID %>");
        var txtPassword = document.getElementById("<%=txt_password.ClientID %>");

        if (txtEmail.value == "" || null) {
            alert("Email can not be left blank !");
            txtEmail.focus();
            return false;
        }
        if (txtPassword.value == "" || null) {
            alert("Password can not be left blank !");
            txtPassword.focus();
            return false;
        }


        return true;
    }
</script>

