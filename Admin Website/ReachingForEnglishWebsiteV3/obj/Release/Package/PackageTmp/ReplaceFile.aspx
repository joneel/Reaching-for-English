<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReplaceFile.aspx.cs" Inherits="ReachingForEnglishWebsiteV3.ReplaceFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type='text/css'>
        .UploadStyle
        {
            font-size: 20px;
            display: inline-block;
            text-transform: uppercase;
            margin-bottom: 18px;
            position: fixed;
            left:45%;
            width: 30%;
        }
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
            position: fixed;
            left:35%;
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
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 75px; width:100%; text-align:center;">
            <asp:Label runat="server" Text="Select New File To Replace Chosen File" Font-Size="40">
            </asp:Label>
        </div>
        <p style="height: 75px; width: 100%">
            <asp:FileUpload ID="FileUpload" runat="server" accept="audio/mpeg,application/pdf" CssClass="UploadStyle"/>
        </p>
        <p style="height: 75px; width: 100%">
            <asp:Button OnClick="Submit" Text="Submit" runat="server" CssClass="ButtonStyle"/>
        </p>
        <p style="height: 75px; width: 100%">
            <asp:Button OnClick="Cancel" Text="Cancel" runat="server" CssClass="ButtonStyle"/>
        </p>
        <img class="logo" src="../Images/logo.png" />
    </form>
</body>
</html>

