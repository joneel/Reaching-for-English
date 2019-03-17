using System;
using MySql.Data.MySqlClient;

namespace ReachingForEnglishWebsiteV3
{
    public partial class SelectTopic : System.Web.UI.Page
    {
        Home SqlConn = new Home();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
            else if (!IsPostBack)
                GetTopicDDL(SqlConn.GetSqlConnection());
        }
        #region Get Environment Drop Down List
        private void GetTopicDDL(MySqlConnection connection)
        {
            try
            {
                TopicDDL.Items.Add("Select a topic...");
                connection.Open();
                String sql = "SELECT tid FROM topics ORDER BY tid ASC";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            TopicDDL.Items.Add(rdr.GetString("tid"));
                        }//end while
                    }
                }
            }
            catch (MySqlException mse)
            {
                Response.Write("<script>alert('"+mse.Message+"');</script>");
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('"+e.Message+"');</script>");
            }

            connection.Close();
        }
        protected void TopicDDL_Change(object sender, EventArgs e)
        {
            Session["tid"] = TopicDDL.SelectedValue;
            Server.Transfer("EnterLessonName.aspx", true);
        }
        #endregion Get Environment Drop Down List
        protected void Cancel(object sender, EventArgs e)
        {
            Server.Transfer("Home.aspx", true);
        }
    }
}