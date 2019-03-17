<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterText.aspx.cs" Inherits="ReachingForEnglishWebsiteV3.EnterText" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type='text/css'>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <p style="height: 75px; width: 100%">
            <asp:Label runat="server" Text="Enter Text for Lesson" Font-Size="40" style="position:fixed; left:40%; z-index: 1; top: 15px; width: 70%;">
            </asp:Label>
        </p>
        <p>
            <asp:TextBox id="TextTextBox" runat="server" TextMode="multiline" rows="20" Width="100%" Font-Size="14" > 
            </asp:TextBox>
        </p>
        <p style="height: 75px; width: 100%">
            <asp:Button OnClick="Submit" Text="Submit" runat="server" CssClass="ButtonStyle"/>
        </p>
        <p style="height: 75px; width: 100%">
            <asp:Button OnClick="Skip" Text="Skip" runat="server" CssClass="ButtonStyle"/>
        </p>
    </form>
</body>
</html>
