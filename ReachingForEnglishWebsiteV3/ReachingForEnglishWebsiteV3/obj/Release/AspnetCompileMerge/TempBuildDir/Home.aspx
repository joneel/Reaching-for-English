<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ReachingForEnglishWebsiteV3.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
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
                width: 15%;
                
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
            <asp:Label runat="server" Text="Reaching for English" Font-Size="40" style="text-align:center">
            </asp:Label>
        </div>
        <div style="margin-left: auto; margin-right: auto; text-align: center;">
            <asp:Button ID="FileViewerButton" runat="server" OnClick="FileViewerButton_Click" Text="View Files" CssClass="ButtonStyle" />
            <asp:Button ID="AddLessonButton" runat="server" OnClick="AddLessonButton_Click" Text="Add Lesson" CssClass="ButtonStyle" />
            <asp:Button ID="AddTopicButton" runat="server" OnClick="AddTopicButton_Click" Text="Add Topic" CssClass="ButtonStyle" />
            <asp:Button ID="DeleteTopicButton" runat="server" OnClick="DeleteLessonButton_Click" Text="Delete Topic" CssClass="ButtonStyle" />
            <asp:Button ID="SelectFileButton" runat="server" OnClick="SelectFileButton_Click" Text="Replace File" CssClass="ButtonStyle" />
            <asp:Button ID="AddDeleteSurvey" runat="server" OnClick="AddDeleteSurveyButton_Click" Text="Add/Delete Survey" CssClass="ButtonStyle" />
        </div>
        <div> 
            <asp:GridView ID="gridLesson" runat="server" AutoGenerateColumns="False" AutoPostBack="True" OnRowDataBound="Lesson_DataBound"
                OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting">
                <HeaderStyle  BackColor="#5685E5" ForeColor="White"/>
                <AlternatingRowStyle BackColor="#90B2F7" />
                <Columns>
                    <asp:TemplateField HeaderText="User" >
                        <ItemTemplate>
                            <asp:Label ID="user" runat="server" Text='<%# Eval("userType") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="UsersDDL" Width="100%" runat="server" AppendDataBoundItems="true" DataSource='<%#FillUsersDDL() %>'  SelectedValue='<%# Eval("userType") %>' >
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Environment" >
                        <ItemTemplate>
                            <asp:Label ID="env" runat="server" Text='<%# Eval("env") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="EnvDDL" Width="100%" runat="server" AppendDataBoundItems="true" DataSource='<%#FillEnvDDL() %>'  SelectedValue='<%# Eval("env") %>' >
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Topic" >
                        <ItemTemplate>
                            <asp:Label ID="tid" runat="server" Text='<%# Eval("tid") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="TopicDDL" Width="100%" runat="server" AppendDataBoundItems="true" DataSource='<%#FillTopicDDL() %>'  SelectedValue='<%# Eval("tid") %>' >
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lesson" >
                        <ItemTemplate>
                            <asp:Label ID="lid" runat="server" Text='<%# Eval("lid") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="LessonTextBox" runat="server" Width="100%" Text='<%# Eval("lid") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Scripts" >
                        <ItemTemplate>
                            <asp:Label ID="text" runat="server" Text='<%# Eval("text") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox" runat="server" Rows="6" TextMode="MultiLine" Width="100%" Text='<%# Eval("text") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField HeaderText="Path" DataField="path" ReadOnly="true" /> --%>                     
                    <asp:BoundField HeaderText="Filename" DataField="filename" ReadOnly="true" />
                    <asp:BoundField HeaderText="Ext" DataField="ext" ReadOnly="true" />
                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="5%">
			            <ItemTemplate>
				            <asp:Button ID="deleteTopic" runat="server" CommandName="Delete" Text="Delete" CssClass="ButtonStyleGrid" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('Are you sure you want to delete this lesson?');"/>
			            </ItemTemplate>
		            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" >
				        <itemtemplate>
					        <asp:Button id="btnEdit" CssClass="ButtonStyleGrid" runat="server" commandname="Edit" text="Edit" />
				        </itemtemplate>
				        <edititemtemplate>
					        <asp:Button id="btnUpdate" CssClass="ButtonStyleGrid" runat="server" commandname="Update" text="Update" />
					        <asp:Button id="btnCancel" CssClass="ButtonStyleGrid" runat="server" commandname="Cancel" text="Cancel" />
				        </edititemtemplate>
		            </asp:TemplateField>
	            </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
