using System;

namespace ReachingForEnglishWebsiteV3
{
    public partial class EnterText : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
        }
        #region Enter Text
            protected void Submit(object sender, EventArgs e)
        {
            Session["text"] = TextTextBox.Text;
            Server.Transfer("ChooseFile.aspx", true);
        }
        protected void Skip(object sender, EventArgs e)
        {
            Session["text"] = "";
            Server.Transfer("ChooseFile.aspx", true);
        }
        #endregion Enter Text
    }
}