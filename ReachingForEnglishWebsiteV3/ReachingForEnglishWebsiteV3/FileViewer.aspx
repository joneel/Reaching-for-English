<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileViewer.aspx.cs" Inherits="ReachingForEnglishWebsiteV3.FileViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        <style type="text/css">
            .scrolling-table-container {
                display: inline-block;
                height: 100%;
                width: 100%;
                overflow-y: scroll;
                overflow-x: scroll;
            }
            div{
                padding-bottom: 5px;
                padding-left: 5px;
            }
            .ButtonStyle
            {
                background: #5685E5;
                color: #fff;
                border-style: double;
                border-color: grey;
                padding: 10px 15px;
                font-size: 20px;
                text-transform: uppercase;
                margin-bottom: 18px;
                width: 20%;
                
            }
            .ButtonStyle:hover{
                text-shadow: 0 0 2em rgba(255,255,255,1);
                color: black;
                border-color: black;
            }
            .ButtonStyleGrid
            {
                background: #5685E5;
                border-color: grey;
                color: #fff;
                border-style: double;
            }
            .ButtonStyleGrid:hover{
                text-shadow: 0 0 2em rgba(255,255,255,1);
                color: black;
                border-color: black;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: auto; margin-right: auto; text-align: center;">
            <asp:Label runat="server" Text="Reaching for English" Font-Size="40" style="text-align:center"></asp:Label>
        </div>
        <div style="margin-left: auto; margin-right: auto; text-align: center;">
            <asp:Button ID="btnLogin" runat="server" OnClick="Login_Click" Text="Login" CssClass="ButtonStyle" />
        </div>
       <div class="scrolling-table-container">
            <asp:GridView ID="gridSurvey" runat="server" AutoGenerateColumns="False" AutoPostBack="True" HorizontalAlign="Center">
                <HeaderStyle BackColor="#5685E5" Width="200px" ForeColor="White" Height="25px" />
                <AlternatingRowStyle BackColor="#90B2F7"/>
                <Columns>
                    <asp:TemplateField HeaderText="Please Take Our Survey">
			            <ItemTemplate>
                            <asp:HyperLink ID="surveyLink" runat="server" Target="HyperLink"
                                NavigateUrl='<%# String.Format("http://{0}", Eval("url").ToString()) %>' 
                                Text='<%# Bind("name") %>'></asp:HyperLink> 
			            </ItemTemplate>
		            </asp:TemplateField>
                </Columns>
           </asp:GridView>
        </div>
        <div class="scrolling-table-container">
            <asp:GridView ID="gridLesson" runat="server" AutoGenerateColumns="False" AutoPostBack="True" Width="100%">
                <HeaderStyle BackColor="#5685E5" Width="200px" ForeColor="White" Height="25px" />
                <AlternatingRowStyle BackColor="#90B2F7"/>
                <Columns>
                    <asp:BoundField HeaderText="User" DataField="userType"/>
                    <asp:BoundField HeaderText="Environment" DataField="env"/>
                    <asp:BoundField HeaderText="Topic" DataField="tid"/>
                    <asp:BoundField HeaderText="Lesson" DataField="lid"/>
                    <asp:TemplateField HeaderText="File Link">
			            <ItemTemplate>
				            <asp:HyperLink ID="fileLink" runat="server" Target="_blank" NavigateUrl='<%# Bind("path") %>' Text='<%# Bind("filename") %>'></asp:HyperLink>
			            </ItemTemplate>
		            </asp:TemplateField>
	            </Columns>
            </asp:GridView>
        </div>
    
    <asp:Label ID="lblerror" runat="server" style="z-index: 1; left: 500px; top: 500px; position: absolute"></asp:Label>
    </form>
</body>
</html>
