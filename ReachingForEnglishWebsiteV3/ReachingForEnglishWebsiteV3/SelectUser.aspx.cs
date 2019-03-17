using System;
using MySql.Data.MySqlClient;

namespace ReachingForEnglishWebsiteV3
{
    public partial class SelectUser : System.Web.UI.Page
    {
        Home SqlConn = new Home();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
            else if (!IsPostBack)
                GetUsersDDL(SqlConn.GetSqlConnection());
        }
        #region Get Users Drop Down List
        private void GetUsersDDL(MySqlConnection connection)
        {
            try
            {
                UsersDDL.Items.Add("Select a user...");
                connection.Open();
                String sql = "SELECT userType FROM user ORDER BY userType ASC";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            UsersDDL.Items.Add(rdr.GetString("userType"));
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
        protected void UsersDDL_Change(object sender, EventArgs e)
        {
            Session["user"] = UsersDDL.SelectedValue;
            Server.Transfer("SelectEnvironment.aspx", true);
        }
        #endregion Get Users Drop Down List
        protected void Cancel(object sender, EventArgs e)
        {
            Server.Transfer("Home.aspx", true);
        }
    }
}