<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteTopic.aspx.cs" Inherits="ReachingForEnglishWebsiteV3.DeleteTopic" %>

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
        .logo{
            display: block;
            margin-left: auto;
            margin-right: auto; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <p style="height: 75px; width: 100%">
            <asp:Label runat="server" Text="Select Topic to Delete" Font-Size="40" style="position:fixed; left:40%; z-index: 1; top: 15px; width: 70%;">
            </asp:Label>
        </p>
        <p>
            <asp:DropDownList AutoPostBack="true" ID="TopicDDL" runat="server" Height="100px" Width="100%" Font-Size="20" > 
            </asp:DropDownList>
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
