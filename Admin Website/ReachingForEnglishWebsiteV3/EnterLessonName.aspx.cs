using System;

namespace ReachingForEnglishWebsiteV3
{
    public partial class EnterLessonName : System.Web.UI.Page
    {
        Home reg = new Home();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
        }
        #region Enter Lesson Name
            protected void Submit(object sender, EventArgs e)
        {
            if (!reg.Regex_LidTid(LessonTextBox.Text))
            {
                Response.Write("<script>alert('Cannot be blank.');</script>");
                return;
            }
            else
            {
                Session["lid"] = LessonTextBox.Text;
                Server.Transfer("EnterText.aspx", true);
            }
        }
        #endregion Enter Lesson Name
    }
}