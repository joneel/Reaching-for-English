using System;
using MySql.Data.MySqlClient;

namespace ReachingForEnglishWebsiteV3
{
    public partial class SelectEnvironment : System.Web.UI.Page
    {
        Home SqlConn = new Home();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
            else if (!IsPostBack)
                GetEnvironmentDDL(SqlConn.GetSqlConnection());
        }
        #region Get Environment Drop Down List
        private void GetEnvironmentDDL(MySqlConnection connection)
        {
            try
            {
                EnvironmentDDL.Items.Add("Select a environment...");
                connection.Open();
                String sql = "SELECT env FROM environment ORDER BY env ASC";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            EnvironmentDDL.Items.Add(rdr.GetString("env"));
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
        protected void EnvironmentDDL_Change(object sender, EventArgs e)
        {
            Session["env"] = EnvironmentDDL.SelectedValue;
            Server.Transfer("SelectTopic.aspx", true);
        }
        #endregion Get Environment Drop Down List
        protected void Cancel(object sender, EventArgs e)
        {
            Server.Transfer("Home.aspx", true);
        }
    }
}