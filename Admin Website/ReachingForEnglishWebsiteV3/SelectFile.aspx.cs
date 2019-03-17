using System;
using MySql.Data.MySqlClient;

namespace ReachingForEnglishWebsiteV3
{
    public partial class SelectFile : System.Web.UI.Page
    {
        Home SqlConn = new Home();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
            else if (!IsPostBack)
                GetFileDDL(SqlConn.GetSqlConnection());
        }
        #region Get Environment Drop Down List
        private void GetFileDDL(MySqlConnection connection)
        {
            try
            {
                FileDDL.Items.Add("Select a file...");
                connection.Open();
                String sql = "SELECT filename, ext FROM lessons ORDER BY filename ASC";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            FileDDL.Items.Add(rdr.GetString("filename") +"."+ rdr.GetString("ext"));
                        }//end while
                    }
                }
            }
            catch (MySqlException mse)
            {
                Response.Write("<script>alert('" + mse.Message + "');</script>");
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "');</script>");
            }

            connection.Close();
        }
        protected void FileDDL_Change(object sender, EventArgs e)
        {
            Session["file"] = FileDDL.SelectedValue.ToString();
            Server.Transfer("ReplaceFile.aspx", true);
        }
        #endregion Get Environment Drop Down List
        protected void Cancel(object sender, EventArgs e)
        {
            Server.Transfer("Home.aspx", true);
        }
    }
}