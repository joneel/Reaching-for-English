using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ReachingForEnglishWebsiteV3
{
    public partial class FileViewer : System.Web.UI.Page
    {
        #region Page Load

        Home SqlConn = new Home();
        protected void Page_Load(object sender, EventArgs e)
        {
            GridLessons(SqlConn.GetSqlConnection());
        }//end method
        #endregion Page Load
        #region Fill GridViews
        private void GridLessons(MySqlConnection connection)
        {
            try
            {
                connection.Open();

                String sql = "SELECT userType,env,tid,lid,path,filename FROM lessons ORDER BY userType,env,tid,lid ASC";
                String sql2 = "SELECT * FROM survey_url";

                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlCommand cmd2 = new MySqlCommand(sql2, connection);

                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                MySqlDataAdapter src = new MySqlDataAdapter(cmd);
                MySqlDataAdapter src2 = new MySqlDataAdapter(cmd2);
                src.Fill(dt);
                src2.Fill(dt2);
                gridLesson.DataSource = dt;
                gridSurvey.DataSource = dt2;
                gridLesson.DataBind();
                gridSurvey.DataBind();

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

        #endregion Fill GridView
        #region Button Click Events
        protected void Login_Click(object sender, EventArgs e)
        {
            if (Session["confirm"] == null || !(bool)Session["confirm"])
                Response.Redirect("Login.aspx");
            else
                Response.Redirect("Home.aspx");
        }//end method

        #endregion Button Click Events
    }//end class
}