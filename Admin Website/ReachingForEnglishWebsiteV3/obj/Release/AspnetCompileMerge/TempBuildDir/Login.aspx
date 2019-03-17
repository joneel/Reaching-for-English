<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ReachingForEnglishWebsiteV3.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="~/favicon.ico" type="image/x-con" />
    <title></title>
    <style type="text/css">
        .ButtonStyle
        {
            background: #5685E5;
            color: #fff;
            border-style: double;
            border-color: grey;
            padding: 10px 15px;
            font-size: 20px;
            display: inline-block;
            text-transform: uppercase;
            margin-bottom: 18px;
            width: 30%;
        }
        .ButtonStyle:hover{
            text-shadow: 0 0 2em rgba(255,255,255,1);
            color: black;
            border-color: black;
        }
        .logo{
            display: block;
            margin-left: auto;
            margin-right: auto; 
            margin-top: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 75px; width:100%; text-align:center;">
            <asp:Label runat="server" Text="Reaching for English" Font-Size="40">
            </asp:Label>
        </div>
        <div style="text-align: center; vertical-align: middle; line-height: 90px; ">
            <asp:Label runat="server" Text="Username:" Font-Size="20"></asp:Label>
            <asp:TextBox id="user" runat="server" Font-Size="20"></asp:TextBox>
        </div>
        <div style="text-align: center; vertical-align: middle; line-height: 90px; ">
            <asp:Label runat="server" Text="Password: " Font-Size="20"></asp:Label>
            <asp:TextBox ID="pass" runat="server" TextMode="Password" Font-Size="20"></asp:TextBox>
        </div>
        <div style="margin-left: auto; margin-right: auto; text-align: center;">
            <asp:Button OnClick="Submit" Text="Submit" runat="server" CssClass="ButtonStyle" />
        </div>
        <div style="margin-left: auto; margin-right: auto; text-align: center;">
            <asp:Button OnClick="FileViewerButton_Click" Text="View Files" runat="server" CssClass="ButtonStyle" />
        </div>
        <img class="logo" src="../Images/logo.png" />
    </form>
</body>
</html>
