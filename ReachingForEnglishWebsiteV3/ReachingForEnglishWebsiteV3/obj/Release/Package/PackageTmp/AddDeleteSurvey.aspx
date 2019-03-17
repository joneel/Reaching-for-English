<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDeleteSurvey.aspx.cs" Inherits="ReachingForEnglishWebsiteV3.AddDeleteSurvey" %>

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
        <div style="margin-left: auto; margin-right: auto; text-align: center;">
            <asp:Label runat="server" Text="Select Survey to Delete" Font-Size="40">
            </asp:Label>
        </div>
        <p>
            <asp:DropDownList AutoPostBack="true" ID="SurveyDDL" runat="server" Height="100px" Width="100%" Font-Size="20" > 
            </asp:DropDownList>
        </p>
        <p style="height: 75px; width: 100%">
            <asp:Button OnClick="SubmitDelete" Text="Submit" runat="server" CssClass="ButtonStyle"/>
        </p>
        <div style="margin-left: auto; margin-right: auto; text-align: center;">
            <asp:Label runat="server" Text="Enter Survey to Add" Font-Size="40">
            </asp:Label>
        </div>
        <div style="margin-left: auto; margin-right: auto; text-align: left;">
            <asp:Label runat="server" Text="Display of link:" Font-Size="20" ></asp:Label>
            <asp:TextBox id="SurveyName" runat="server" Height="100px" Width="89%" Font-Size="20" > 
            </asp:TextBox>
        </div>
        <div style="margin-left: auto; margin-right: auto; text-align: left;">
            <asp:Label runat="server" Text="URL of survey:" Font-Size="20"></asp:Label>
            <asp:TextBox id="SurveyURL" runat="server" Height="100px" Width="89%" Font-Size="20" > 
            </asp:TextBox>
        </div>
        <p style="height: 75px; width: 100%">
            <asp:Button OnClick="SubmitAdd" Text="Submit" runat="server" CssClass="ButtonStyle"/>
        </p>
        <p style="height: 75px; width: 100%">
            <asp:Button OnClick="Cancel" Text="Cancel" runat="server" CssClass="ButtonStyle"/>
        </p>
    </form>
</body>
</html>
