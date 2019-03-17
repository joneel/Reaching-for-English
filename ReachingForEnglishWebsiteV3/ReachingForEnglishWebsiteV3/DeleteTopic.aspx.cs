using System;
using MySql.Data.MySqlClient;

namespace ReachingForEnglishWebsiteV3
{
    public partial class DeleteTopic : System.Web.UI.Page
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
            catch (ArgumentException ae)
            {
                Response.Write("<script>alert('"+ae.Message+"');</script>");
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "');</script>");
            }

            connection.Close();
        }
        #endregion Get Environment Drop Down List
        protected void Submit(object sender, EventArgs e)
        {
            bool success = false;
            Int32 pathCount = -1;
            MySqlConnection connection = null;

            try
            {
                connection = SqlConn.GetSqlConnection();

                connection.Open();

                String sqlTopicCheck = "SELECT COUNT(lid) FROM lessons WHERE tid = @tid";
                using (MySqlCommand cmd = new MySqlCommand(sqlTopicCheck, connection))//Check if topic is associated with another lesson
                {
                    cmd.Parameters.AddWithValue("@tid", TopicDDL.SelectedValue.ToString());
                    pathCount = int.Parse(cmd.ExecuteScalar().ToString());
                }

                if (pathCount > 0)//topic is associated with lessons
                {
                    Response.Write("<script>alert('Cannot delete a topic while there are lessons within the topic.');</script>");
                }
                else// no other lessons associated with this topic 
                {
                    String sqlDelete = "DELETE FROM topics WHERE tid = @tid";
                    using (MySqlCommand cmd = new MySqlCommand(sqlDelete, connection))
                    {
                        cmd.Parameters.AddWithValue("@tid", TopicDDL.SelectedValue.ToString());
                        cmd.ExecuteNonQuery();
                    }
                    success = true;
                }
                connection.Close();
            }
            catch (MySqlException mse)
            {
                Response.Write("<script>alert('"+mse.Message+"');</script>");
            }
            catch (ArgumentException ae)
            {
                Response.Write("<script>alert('"+ae.Message+"');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
            if(success)
                Server.Transfer("Home.aspx", true);
            success = false;
        }
        protected void Cancel(object sender, EventArgs e)
        {
            Server.Transfer("Home.aspx", true);
        }
    }
}