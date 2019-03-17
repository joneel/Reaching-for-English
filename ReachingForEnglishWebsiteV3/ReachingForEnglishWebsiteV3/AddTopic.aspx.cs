using System;
using MySql.Data.MySqlClient;

namespace ReachingForEnglishWebsiteV3
{
    public partial class AddTopic : System.Web.UI.Page
    {
        Home SqlConn = new Home();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
        }
        protected void Submit(object sender, EventArgs e)
        {
            bool success = false;
            MySqlConnection connection = null;

            if (!SqlConn.Regex_LidTid(TopicTextBox.Text))
                Response.Write("<script>alert('Cannot be blank.');</script>");
            else
            {
                try
                {
                    Int32 pathCount = -1;
                    connection = SqlConn.GetSqlConnection();
                    connection.Open();

                    String sqlTopicCheck = "SELECT COUNT(tid) FROM topics WHERE tid = @tid";
                    using (MySqlCommand cmd = new MySqlCommand(sqlTopicCheck, connection))//Check if topic is associated with another lesson
                    {
                        cmd.Parameters.AddWithValue("@tid", TopicTextBox.Text);
                        pathCount = int.Parse(cmd.ExecuteScalar().ToString());
                    }

                    if (pathCount == 0)
                    {
                        String sql = "INSERT INTO topics (tid) VALUES (@tid)";
                        using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@tid", TopicTextBox.Text);
                            cmd.ExecuteNonQuery();
                        }
                        success = true;
                    }
                    else
                        Response.Write("<script>alert('Topic already exists.');</script>");

                    connection.Close();
                }
                catch (MySqlException mse)
                {
                    Response.Write("<script>alert('" + mse.Message + "');</script>");
                }
                catch (ArgumentException ae)
                {
                    Response.Write("<script>alert('" + ae.Message + "');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
                if(success)
                    Server.Transfer("Home.aspx", true);
                success = false;
            }
        }
        protected void Cancel(object sender, EventArgs e)
        {
            Server.Transfer("Home.aspx", true);
        }
    }
}